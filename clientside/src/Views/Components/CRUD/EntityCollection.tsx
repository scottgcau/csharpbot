/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from 'react';
import Collection, { ICollectionItemActionProps, expandFn, ICollectionBulkActionProps } from '../Collection/Collection';
import { Button, Display } from '../Button/Button';
import { observer } from 'mobx-react';
import { RouteComponentProps } from 'react-router';
import { Model, IModelType } from 'Models/Model';
import { getModelName, getAttributeCRUDOptions, exportAll } from 'Util/EntityUtils';
import {observable, action, runInAction, computed} from 'mobx';
import Spinner from '../Spinner/Spinner';
import { ICollectionHeaderProps } from '../Collection/CollectionHeaders';
import ModelQuery, { IWhereCondition, IOrderByCondition, IWhereConditionApi } from '../ModelCollection/ModelQuery';
import { IFilter } from '../Collection/CollectionFilterPanel';
import PaginationData, { PaginationQueryOptions } from 'Models/PaginationData';
import { QueryResult } from 'react-apollo';
import { lowerCaseFirst } from 'Util/StringUtils';
import { SecurityService } from 'Services/SecurityService';
import { OperationVariables } from 'apollo-boost';
import * as _ from 'lodash';
import classNames from 'classnames';
import { confirmModal } from '../Modal/ModalUtils';
import alert from '../../../Util/ToastifyUtils';
import { IEntityContextMenuActions } from '../EntityContextMenu/EntityContextMenu';
import { convertCaseComparisonToPascalCase } from 'Util/GraphQLUtils';
import { ICollectionFilterPanelProps } from '../Collection/CollectionFilterPanel';
import moment from 'moment';

interface IEntityCollectionProps<T extends Model> extends RouteComponentProps {
	modelType: IModelType;
	expandList?: expandFn<T>;
	additionalBulkActions?: Array<ICollectionBulkActionProps<T>>;
	filters?: Array<IFilter<T>>;
	perPage?: number;
	orderBy?: IOrderByCondition<T>;
	actionsMore?: IEntityContextMenuActions<T>;
}

interface ISearch {
	searchTerm: string;
}
@observer
class EntityCollection<T extends Model> extends React.Component<IEntityCollectionProps<T>, any> {

	@observable
	private search: ISearch = { searchTerm: "" };

	@observable
	private filterConfig: ICollectionFilterPanelProps<T>;

	@observable
	private filterApplied: boolean = false;

	@observable
	private orderBy: IOrderByCondition<T> | undefined;

	@computed
	private get _orderBy() {
		if (this.orderBy === undefined) {
			//set the default order by to display the options in reverse creation order
			return { path: "Created", descending: true };
		}
		return this.orderBy;
	}

	@observable
	private paginationQueryOptions: PaginationQueryOptions = new PaginationQueryOptions();

	@observable
	private allSelectedItemIds: string[] = new Array<string>();

	@observable
	private allExcludedItemIds: string[] = new Array<string>();

	@observable
	private allPagesSelected: boolean = false;

	@computed
	private get collectionFilters() {
		let conditions = this.getSearchConditions();
		let filterConditions: IWhereCondition<Model>[][] | undefined;

		if(this.filterApplied){
			filterConditions = new this.props.modelType().getFilterConditions(this.filterConfig);
		}

		if (filterConditions && !!filterConditions.length) {
			conditions = [...conditions, ...filterConditions]
				.map(andCondition => andCondition.map(orCondition => ({
					...orCondition,
					value: [orCondition.value]
				})));
		}

		return conditions;
	}

	private models: T[] = [];

	constructor(props: IEntityCollectionProps<T>, context: any) {
		super(props, context);
		this.filterConfig = {
			filters: this.getFilters(),
			onClearFilter: this.onClearFilter,
			onApplyFilter: this.onApplyFilter,
			onFilterChanged: this.onFilterChanged
		}
		const defaultOrderBy = props.modelType.getOrderByField ? props.modelType.getOrderByField() : undefined;
		this.orderBy = defaultOrderBy ? defaultOrderBy : props.orderBy;
	}

	componentDidUpdate() {
		runInAction(() => {
			this.paginationQueryOptions.page = 0;
		});
	}

	public render() {
		runInAction(() => this.paginationQueryOptions.perPage = this.props.perPage || 10);
		return (
			<>
				<ModelQuery
					model={this.props.modelType}
					pagination={this.paginationQueryOptions}
					orderBy={this._orderBy}
					conditions={this.collectionFilters}>
					{this.renderCollection}
				</ModelQuery>
			</>
		);
	}

	protected renderCollection = (result: QueryResult<any, OperationVariables>) : JSX.Element => {
		const { loading, error, data, refetch } = result;
		if (error) {
			return(
				<div>
					<h2>An unexpected error occurred:</h2>
					{JSON.stringify(error.message)}
				</div>
			);
		}

		const { modelType } = this.props;
		const modelName = getModelName(modelType);

		const tableHeaders = this.getHeaders();
		const tableActions = this.getTableActions(refetch);

		this.models = [];
		const dataModelName = lowerCaseFirst(modelName + 's');
		if (data[dataModelName]) {
			this.models = data[dataModelName].map((e: any) => new modelType(e));
		}

		const countName = `count${modelName}s`;
		let totalRecords = 0;
		if (data[countName]) {
			totalRecords = data[countName]['number'];
		}

		let additionalActions: React.ReactNode[] = [];
		if(SecurityService.canCreate(this.props.modelType)){
			additionalActions.push (this.renderCreateButton());
		}

		let menuCountFunction = () => {
			if(this.allPagesSelected){
				return totalRecords - this.allExcludedItemIds.length;
			}else{
				return this.allSelectedItemIds.length;
			}
		};

		const selectedBulkActions :Array<ICollectionBulkActionProps<T>> = [];
		if(SecurityService.canRead(this.props.modelType)){
			selectedBulkActions.push({
				bulkAction: this.exportItems,
				label: "Export",
				showIcon: true,
				icon: "export",
				iconPos: 'icon-left',
			});
		}
		if (SecurityService.canDelete(this.props.modelType)) {
			selectedBulkActions.push({
				bulkAction: () => {
					confirmModal('Please confirm', "Are you sure you want to delete all the selected items?").then(()=>{
						let idsToDelete: Array<string> | undefined = [];
						let conditions: Array<Array<IWhereCondition<Model>>> | undefined;
						if (this.allPagesSelected) {
							conditions = this.getSearchConditions() as Array<Array<IWhereCondition<Model>>>;
							if(!conditions){
								conditions = [];
							}
							let idsCondition = new Array<IWhereCondition<Model>>();
							idsCondition.push({path:'id', comparison:'notIn', value: this.allExcludedItemIds });
							(conditions as Array<Array<IWhereCondition<Model>>>).push(idsCondition);
							idsToDelete = undefined;
						} else {
							idsToDelete = this.allSelectedItemIds;
							conditions = this.getSearchConditions() as Array<Array<IWhereCondition<Model>>>;
						}
						new this.props.modelType().deleteWhere(conditions, idsToDelete).then((result) => {
							if(!!result && result['value'] === true){
								refetch();
								this.cancelAllSelection();
								alert('All selected items are deleted successfully', 'success');
							}
						}).catch((errorMessage) => {
							alert(
								<div className="delete-error">
									<p className="user-error">These records could not be deleted because of an association</p>
									<p className="internal-error-title">Message:</p>
									<p className="internal-error">{errorMessage}</p>
								</div>, 
								'error'
							);
						});
					});
				},
				label: "Delete",
				showIcon: true,
				icon: "bin-full",
				iconPos: 'icon-left',
			});
		}

		return(
			<>
				{loading && <Spinner/>}
				<Collection
					selectableItems={true}
					additionalActions={additionalActions}
					headers={tableHeaders}
					actions={tableActions}
					actionsMore={this.props.actionsMore}
					selectedBulkActions={selectedBulkActions.concat(this.props.additionalBulkActions || [])}
					onSearchTriggered={this.onSearchTriggered}
					menuFilterConfig={this.filterConfig}
					collection={this.models}
					pagination={{totalRecords, queryOptions: this.paginationQueryOptions }}
					itemSelectionChanged={this.itemSelectionChanged}
					cancelAllSelection={this.cancelAllSelection}
					menuCountFunction={menuCountFunction}
					expandList={this.props.expandList}
					getSelectedItems={this.getSelectedItems}
					onCheckedAllPages={this.onCheckedAllPages}
					idColumn="id"
					dataFields={row => ({
						created: moment(row.created).format('YYYY-MM-DD'),
						modified: moment(row.modified).format('YYYY-MM-DD'),
					})}
					orderBy={this.orderBy}
				/>
			</>
		);
	}

	private getSelectedItems = () => {
		return this.models.filter(model => {
			if (this.allPagesSelected) {
				return !this.allExcludedItemIds.some(id => {
					return model.id === id;
				});
			} else {
				return this.allSelectedItemIds.some(id => {
					return model.id === id;
				});
			}
		});
	};

	@action
	private onCheckedAllPages = (checked: boolean) => {
		this.allPagesSelected = checked;
		if(checked){
			this.allExcludedItemIds = [];
			let changedIds = this.models.map(item => item.id);
			if (checked) {
				this.allSelectedItemIds = _.union(this.allSelectedItemIds, changedIds);
			} else {
				this.allSelectedItemIds = _.pull(this.allSelectedItemIds, ...changedIds);
			}
			let selectedItems = (new Array<T>());
			selectedItems.push(...this.models);
			return selectedItems;
		}else{
			this.allSelectedItemIds = [];
			let changedIds = this.models.map(item => item.id);
			if (!checked) {
				this.allExcludedItemIds = _.union(this.allSelectedItemIds, changedIds);
			} else {
				this.allExcludedItemIds = _.pull(this.allSelectedItemIds, ...changedIds);
			}
			return [];
		}
	}

	@action
	private itemSelectionChanged = (checked: boolean, changedItems: Model[]) => {
		let changedIds = changedItems.map(item => item.id);
		if (this.allPagesSelected) {
			if (!checked) {
				this.allExcludedItemIds = _.union(this.allExcludedItemIds, changedIds);
			} else {
				this.allExcludedItemIds = _.pull(this.allExcludedItemIds, ...changedIds);
			}
		} else {
			if (checked) {
				this.allSelectedItemIds = _.union(this.allSelectedItemIds, changedIds);
			} else {
				this.allSelectedItemIds = _.pull(this.allSelectedItemIds, ...changedIds);
			}
		}
		return this.models.filter((m: Model) => {
			if (this.allPagesSelected) {
				return !this.allExcludedItemIds.some(id => {
					return m.id === id;
				}) ;
			} else {
				return this.allSelectedItemIds.some(id => {
					return m.id === id;
				}) ;
			}
		});
	}

	protected renderCreateButton(): React.ReactNode {
		const { modelType } = this.props;
		const modelName = getModelName(modelType);
		return (
			<Button
				key="create"
				className={classNames(Display.Solid)}
				icon={{icon: 'create', iconPos: 'icon-right'}}
				buttonProps={{ onClick: () => { this.props.history.push(`${this.props.match.url}/create`); } }}>
				Create {modelName}
			</Button>
		);
	}

	protected getHeaders = (): Array<ICollectionHeaderProps<T>> => {
		const attributeOptions = getAttributeCRUDOptions(this.props.modelType);
		return attributeOptions.filter(attributeOption => attributeOption.headerColumn)
			.map(attributeOption => {
					const headers: ICollectionHeaderProps<T> = {
						name: attributeOption.attributeName,
						displayName: attributeOption.displayName,
						sortable: true,
						sortClicked: () => {
							if (this.orderBy && this.orderBy.path === attributeOption.attributeName) {
								if (this.orderBy.descending) {
									const descending = !this.orderBy.descending;
									runInAction(() => this.orderBy = { path: attributeOption.attributeName, descending });
								} else if (!this.orderBy.descending) {
									runInAction(() => this.orderBy = undefined);
								}
								return this.orderBy;
							} else {
								runInAction(() => this.orderBy = { path: attributeOption.attributeName, descending: true });
								return this.orderBy;
							}
						},
					};
					if (attributeOption.displayFunction) {
						headers.transformItem = (item: any) => {
							if (attributeOption.displayFunction) {
								return attributeOption.displayFunction(item[attributeOption.attributeName])
							}
							return item[attributeOption.name];
						};
					}
					return headers;
				}
			);
	}

	protected getFilters = (): Array<IFilter<T>> => {
		let filters = new Array<IFilter<T>>();

		filters.push({
			path: "created",
			comparison: "range",
			value1: undefined,
			value2: undefined,
			active: false,
			displayType: "datepicker",
			displayName: "Range of Date Created"
		} as IFilter<T>);

		filters.push({
			path: "modified",
			comparison: "range",
			value1: undefined,
			value2: undefined,
			displayType: "datepicker",
			displayName: "Range of Date Last Modified",
		} as IFilter<T>);

		filters = [...filters ,..._.cloneDeep(this.props.filters) || []];

		const enumFilters = this.getEnumFilters();

		filters = [...filters, ...enumFilters];

		return filters;
	}

	protected getEnumFilters = () => {
		const attributeOptions = getAttributeCRUDOptions(this.props.modelType);
		return attributeOptions
			.filter(attributeOption => attributeOption.displayType === 'enum-combobox')
			.map(attributeOption => {
				return {
					path: attributeOption.attributeName,
					comparison: 'equal',
					value1: [] as string[],
					displayName: attributeOption.displayName,
					displayType: 'enum-combobox',
					referenceResolveFunction: attributeOption.enumResolveFunction
				} as IFilter<T>;
			});
	}

	@action
	protected onClearFilter = () => {
		this.filterConfig.filters = this.getFilters();
		this.filterApplied = false;
	};

	@action
	protected onApplyFilter = () => {
		this.filterApplied = true;
	};

	@action
	protected onFilterChanged = () => {
		this.filterApplied = false;
	}

	private exportItems = () => {
		let conditions: IWhereConditionApi<Model>[][];
		if (this.allPagesSelected && this.collectionFilters) {
			conditions = [
				...this.collectionFilters.map(andCondition => andCondition.map(orCondition => {
					if (orCondition.case) {
						return {
							...orCondition,
							case: convertCaseComparisonToPascalCase(orCondition.case)
						}
					}
					return orCondition as IWhereConditionApi<Model>;
				})),
				[{
					path: "id",
					comparison: "notIn",
					value: this.allExcludedItemIds,
				}]
			];
		} else {
			conditions = [[
				{
					path: "id",
					comparison: "in",
					value: this.allSelectedItemIds
				}
			]];
		}
		return exportAll(this.props.modelType, conditions);
	}

	protected getTableActions = (refetch: () => void) => {
		const tableActions: Array<ICollectionItemActionProps<T>> = [];
		if(SecurityService.canRead(this.props.modelType)){
			tableActions.push({
				action: (item) => {
					this.props.history.push({ pathname: `${this.props.match.url}/view/${item['id']}` });
				},
				label: "View",
				showIcon: true,
				icon: "look",
				iconPos: 'icon-top',
			});
		}
		if(SecurityService.canUpdate(this.props.modelType)){
			tableActions.push({
				action: (item) => {
					this.props.history.push({ pathname: `${this.props.match.url}/edit/${item['id']}` });
				},
				label: "Edit",
				showIcon: true,
				icon: "edit",
				iconPos: 'icon-top',
			});
		}
		if (SecurityService.canDelete(this.props.modelType)) {
			tableActions.push({
				action: (item) => {
					confirmModal('Please confirm', "Are you sure you want to delete this item?").then(() => {
						new this.props.modelType(item).delete().then(() => {
							refetch();
							alert('Deleted successfully', 'success');
						}).catch((errorMessage) => {
							alert(
								<div className="delete-error">
									<p className="user-error">This record could not be deleted because of an association</p>
									<p className="internal-error-title">Message:</p>
									<p className="internal-error">{errorMessage}</p>
								</div>
								, 'error'
							);
						});
					});
				},
				label: "Delete",
				showIcon: true,
				icon: "bin-full",
				iconPos: 'icon-top',
			});
		}
		return tableActions;
	}


	@action
	protected onSearchTriggered = (searchTerm: string) => {
		this.search.searchTerm = searchTerm;
	}

	@action
	protected cancelAllSelection = () => {
		this.allPagesSelected = false;
		this.allSelectedItemIds = [];
		this.allExcludedItemIds = [];
	}

	private getSearchConditions() {
		return new this.props.modelType().getSearchConditions(this.search.searchTerm);
	}
}

export default EntityCollection;