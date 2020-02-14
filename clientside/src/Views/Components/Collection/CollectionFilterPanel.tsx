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
import { observer } from 'mobx-react';
import * as React from 'react';
import { Comparators } from '../ModelCollection/ModelQuery';
import { displayType } from 'Models/CRUDOptions';
import { Button, Display } from '../Button/Button';
import FilterDateRange from './CollectionFilterAttributes/FilterDateRangePicker';
import FilterEnumComboBox from './CollectionFilterAttributes/FilterEnumComboBox';
import _ from 'lodash';
import { ButtonGroup, Alignment } from '../Button/ButtonGroup';

export interface IFilter<T> {
	/** column name */
	path: string;
	/** comparison operator */
	comparison: Comparators | 'range';
	/** operand 1 */
	value1: string | string[] | Date | number | undefined;
	/** operand 2. only valid for 'range' type comparison for now */
	value2: string | Date | number | undefined;
	/** this is specifically for the model of date range */
	active: boolean;
	/** display type of the filter*/
	displayType: displayType;
	/** display name of the filter */
	displayName: string;
	/** the function to resolve and return the options of the enum-combobox(for now only enum-combobox) */
	referenceResolveFunction?: Array<{display: string, value: string}>;
}

export interface ICollectionFilterPanelProps<T> {
	filters: IFilter<T>[];
	onClearFilter: () => void;
	onApplyFilter: () => void;
	onFilterChanged?: () => void;
}

@observer
class CollectionFilterPanel<T> extends React.Component<ICollectionFilterPanelProps<T>> {

	public render() {
		const { filters } = this.props;
		
		if (filters == undefined || !filters.length){
			return null;
		} else {
			return (
					<>
						<div className="collection-filter-form__container">
							{
								filters.map(filter => {
									switch (filter.displayType) {
										case 'datepicker':
											if (filter.comparison === 'range') {
												return (
													<FilterDateRange
														filter={filter}
														className={'filter-' + filter.path}
														key={'filter-' + filter.path}
														onAfterChange={() => {
															if(this.props.onFilterChanged){
																this.props.onFilterChanged();
															}
														}}
													/>
												);
											}
										case 'enum-combobox':
											return <FilterEnumComboBox
												filter={filter}
												className={'filter-' + filter.path}
												key={'filter-' + filter.path}
												onAfterChange={() => {
													if(this.props.onFilterChanged){
														this.props.onFilterChanged();
													}
												}}
											/>
										default:
											return "";
									}
								})
							}
						</div>
						<div className="collection-filter-form__actions">
							<ButtonGroup alignment={Alignment.HORIZONTAL}>
								<Button className="clear-filters"
									display={Display.Outline}
									onClick={this.props.onClearFilter}
								>Clear Filters</Button>
								<Button className="apply-filters"
									display={Display.Solid}
									onClick={this.props.onApplyFilter}
								>Apply Filters</Button>
							</ButtonGroup>
						</div>
					</>
			)
		}

	}
}

export default CollectionFilterPanel;