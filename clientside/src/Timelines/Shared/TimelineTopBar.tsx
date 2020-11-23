/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
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
import SearchForm from "../../Views/Components/SearchForm/SearchForm";
import {Alignment, ButtonGroup} from 'Views/Components/Button/ButtonGroup';
import {Button, Display} from 'Views/Components/Button/Button';
import {action, observable, runInAction} from "mobx";
import {MultiCombobox} from 'Views/Components/Combobox/MultiCombobox';
import AwesomeDebouncePromise from "awesome-debounce-promise";
import {DatePicker} from 'Views/Components/DatePicker/DatePicker';
import {observer} from "mobx-react";
import {Combobox} from 'Views/Components/Combobox/Combobox';
import {getFetchAllConditional, getModelDisplayName, getModelName} from 'Util/EntityUtils';
import {dropdownData} from 'Views/Components/CRUD/Attributes/AttributeReferenceMultiCombobox';
import {store} from 'Models/Store';
import {lowerCaseFirst} from 'Util/StringUtils';
import cloneDeep from 'lodash/cloneDeep';
import { getTimelineActionOptions, getTimelineEntityFromName, TimelineEntities } from 'Util/TimelineUtils';
import {ITimelineFilter} from 'Timelines/TimelineTile';
import _ from 'lodash';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

interface ITimelineTopBarProps {
	// % protected region % [Override ITimelineTopBarProps here] off begin
	timelineFilter: ITimelineFilter
	// % protected region % [Override ITimelineTopBarProps here] end
}

@observer
export default class TimelineTopBar extends React.Component<ITimelineTopBarProps> {
	
	@observable
	private timelineFilterClone: ITimelineFilter = cloneDeep(this.props.timelineFilter);
	
	@observable
	private showAdvancedFilters = false;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	@observable
	private timelineEntityConfiguration = {
		// % protected region % [Override timelineEntityConfiguration here] off begin
		selectedTimelineEntityName: getModelDisplayName(this.props.timelineFilter.selectedTimelineEntity)
		// % protected region % [Override timelineEntityConfiguration here] end
	};

	private fetchInstanceOptions = (queryString: string | string[]): Promise<dropdownData> => {
		// % protected region % [Override fetchInstanceOptions here] off begin
		const modelName = getModelName(this.timelineFilterClone.selectedTimelineEntity);
		const searchConditions = (new this.timelineFilterClone.selectedTimelineEntity).getSearchConditions(!Array.isArray(queryString) ? queryString : '');
		return store.apolloClient
			.query({
				query: getFetchAllConditional(this.timelineFilterClone.selectedTimelineEntity),
				variables: { args: searchConditions, take: 10 },
				fetchPolicy: 'network-only',
			})
			.then((data) => {
				const associatedObjects: any[] = data.data[`${lowerCaseFirst(modelName)}s`];
				let comboOptions: dropdownData = [];
				if (associatedObjects) {
					associatedObjects.map(obj => new this.timelineFilterClone.selectedTimelineEntity(obj))
						.map(obj => comboOptions.push({ display: obj.getDisplayName(), value: obj.id }));
				}
				this.timelineFilterClone.instanceIds.map(id => {
					if (!comboOptions.some(option => option.value == id)){
						comboOptions.push({display: id, value: id})
					}
				});
				return comboOptions;
			});
		// % protected region % [Override fetchInstanceOptions here] end
	};
	
	@action
	private onApplyFilter = () => {
		// % protected region % [Override onApplyFilter here] off begin
		this.props.timelineFilter.searchTerm = this.timelineFilterClone.searchTerm;
		this.props.timelineFilter.instanceIds = this.timelineFilterClone.instanceIds;
		this.props.timelineFilter.selectedActionTypes = this.timelineFilterClone.selectedActionTypes;
		this.props.timelineFilter.startDate = this.timelineFilterClone.startDate;
		this.props.timelineFilter.endDate = this.timelineFilterClone.endDate;
		this.props.timelineFilter.selectedTimelineEntity = this.timelineFilterClone.selectedTimelineEntity;
		this.props.timelineFilter.actionTypeOptions = this.timelineFilterClone.actionTypeOptions;
		this.timelineFilterClone = cloneDeep(this.props.timelineFilter);
		// % protected region % [Override onApplyFilter here] end
	};

	@action
	private onClearFilter = () => {
		// % protected region % [Override onClearFilter here] off begin
		this.props.timelineFilter.instanceIds = [];
		this.props.timelineFilter.selectedActionTypes = [];
		this.props.timelineFilter.startDate = undefined;
		this.props.timelineFilter.endDate = undefined;
		this.timelineFilterClone = cloneDeep(this.props.timelineFilter);
		// % protected region % [Override onClearFilter here] end
	};
	
	@action
	private onChangeEntity = () => {
		// % protected region % [Override onChangeEntity here] off begin
		const entity = getTimelineEntityFromName(this.timelineEntityConfiguration.selectedTimelineEntityName);
		if (entity) {
			this.timelineFilterClone.selectedTimelineEntity = entity;
		}
		this.timelineFilterClone.instanceIds = [];
		this.timelineFilterClone.selectedActionTypes = [];
		getTimelineActionOptions(this.timelineFilterClone.selectedTimelineEntity)
			.then(options => runInAction(() => this.timelineFilterClone.actionTypeOptions = options))
		// % protected region % [Override onChangeEntity here] end
	};
	
	private getAdvancedFilters = () => {
		// % protected region % [Override getAdvancedFilters here] off begin
		if (! this.showAdvancedFilters) {
			return null;
		}
		let entityOptionsDropDown = null;
		if (this.timelineFilterClone.timelineEntityOptions) {
			entityOptionsDropDown = (
				<Combobox
				model={this.timelineEntityConfiguration}
				label={'Entity'}
				options={this.timelineFilterClone.timelineEntityOptions.map(x => {
					return {
						display: getModelDisplayName(x),
						value: getModelDisplayName(x)
					}
				})}
				onAfterChange={this.onChangeEntity}
				modelProperty={'selectedTimelineEntityName'}/>
				)
		}

		return (
			<section className={'timelines__filter'}>
				{entityOptionsDropDown}
				<MultiCombobox
					key={_.findIndex(TimelineEntities, x => x == this.timelineFilterClone.selectedTimelineEntity)}
					initialOptions={() => this.fetchInstanceOptions('')}
					model={this.timelineFilterClone}
					modelProperty={'instanceIds'}
					options={AwesomeDebouncePromise(this.fetchInstanceOptions, 500)}
					label="Instances"
					label-visible="true" />
				<DatePicker
					model={this.timelineFilterClone}
					modelProperty={'startDate'}
					label={'Start date'}
				/>
				<DatePicker
					model={this.timelineFilterClone}
					modelProperty={'endDate'}
					label={'End date'}
				/>
				<MultiCombobox
					model={this.timelineFilterClone}
					modelProperty={'selectedActionTypes'}
					options={this.timelineFilterClone.actionTypeOptions.map(x => {return {display: x, value: x}})}
					label="Type of change"
					label-visible="true" />

				<ButtonGroup alignment={Alignment.HORIZONTAL}>
					<Button
						className="clear-filters"
						display={Display.Outline}
						onClick={this.onClearFilter}>
						Clear Filters
					</Button>
					<Button
						className="apply-filters"
						display={Display.Solid}
						onClick={this.onApplyFilter}>
						Apply Filters
					</Button>
				</ButtonGroup>
			</section>
		);
		// % protected region % [Override getAdvancedFilters here] end
	};
	
	public render() {
		// % protected region % [Override render here] off begin
		return (
			<>
				<div className='behaviour-header'>
					<h2>Timelines</h2>
				</div>
				<section aria-label="timelines menu" className="timelines__menu">
					<SearchForm
						clickToClear={false}
						model={this.timelineFilterClone}
						onSubmit={this.onApplyFilter}
						label='Search'
						className='search timelines__search'
						classNameSuffix="collection" />
					<ButtonGroup className="timelines__menu--actions btn-group--horizontal">
						<Button
							onClick={action(() => this.showAdvancedFilters = !this.showAdvancedFilters)}
							icon={{ icon: "filter", iconPos: "icon-top" }}
							className="btn--solid btn--primary">
							Filter
						</Button>
						{this.props.children}
					</ButtonGroup>
				</section>
				{this.showAdvancedFilters ? this.getAdvancedFilters() : null}
			</>
		);
		// % protected region % [Override render here] end
	}
}