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
import { IModelType, Model } from 'Models/Model';
import {observer} from "mobx-react";
import {action, computed, observable, runInAction} from 'mobx';
import { TimelineModel } from 'Timelines/TimelineModel';
import TimelineListView from "./TimelineListView/TimelineListView";
import TimelineGraphView from "./TimelineGraphView/TimelineGraphView";
import {getTimelineActionOptions, TimelineEntities} from 'Util/TimelineUtils';
import moment, {Moment} from "moment";
import Spinner from 'Views/Components/Spinner/Spinner';
import { IWhereCondition } from 'Views/Components/ModelCollection/ModelQuery';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export type TTimelineEntityOption = IModelType<Model & TimelineModel>

export interface ITimelineTileProps {
	// % protected region % [Override ITimelineTileProps here] off begin
	timelineEntity?: TTimelineEntityOption
	entityId?: string;
	component?: 'list' | 'graph'
	onClickViewItem?: (entity: string, id: string) => void;
	onRouteToListView?: (date?: Date) => void;
	onRouteToGraphView?: () => void;
	timelineViewDate?: Moment;
	timelineFilterProps?: Partial<ITimelineFilter>
	// % protected region % [Override ITimelineTileProps here] end
}

export interface ITimelineFilter {
	// % protected region % [Override ITimelineFilter here] off begin
	searchTerm: string,
	instanceIds: string[],
	startDate: Date | undefined,
	endDate: Date | undefined,
	selectedActionTypes: string[],
	actionTypeOptions: string[],
	selectedTimelineEntity: TTimelineEntityOption
	timelineEntityOptions?: TTimelineEntityOption[]
	canChangeTimelineEntity?: boolean
	initialSearchConditions?: Array<IWhereCondition<any>>;
	// % protected region % [Override ITimelineFilter here] end
}

@observer
export default class TimelineTile extends React.Component<ITimelineTileProps> {

	@observable
	private component = this.props.component ?? 'graph';

	@observable
	private loadingStatus: 'Loading' | 'Done' = 'Loading';

	@observable
	private internalTimelineViewDate: Moment | undefined = undefined;

	// % protected region % [Add extra component variables here] off begin
	// % protected region % [Add extra component variables here] end

	@computed
	private get timelineViewDate() {
		// % protected region % [Override timelineViewDate here] off begin
		if (this.props.timelineViewDate) {
			return this.props.timelineViewDate;
		}
		return this.internalTimelineViewDate;
		// % protected region % [Override timelineViewDate here] end
	}

	@observable
	private timelineFilter: ITimelineFilter = {
		// % protected region % [Override timelineFilter here] off begin
		searchTerm: this.props.timelineFilterProps?.searchTerm ?? '',
		instanceIds: this.props.entityId ? [this.props.entityId] : [],
		startDate: this.props.timelineFilterProps?.startDate ?? undefined,
		endDate: this.props.timelineFilterProps?.endDate ?? undefined,
		selectedActionTypes: this.props.timelineFilterProps?.selectedActionTypes ?? [],
		actionTypeOptions: this.props.timelineFilterProps?.actionTypeOptions ?? [],
		selectedTimelineEntity: this.props.timelineEntity ?? TimelineEntities[0],
		timelineEntityOptions: this.props.timelineFilterProps?.timelineEntityOptions ?? TimelineEntities,
		canChangeTimelineEntity: this.props.timelineFilterProps?.canChangeTimelineEntity ?? false,
		initialSearchConditions: this.props.timelineFilterProps?.initialSearchConditions ?? []
		// % protected region % [Override timelineFilter here] end
	};

	componentDidMount(): void {
		// % protected region % [Override componentDidMount here] off begin
		this.getActionOptions();
		// % protected region % [Override componentDidMount here] end
	}

	private getActionOptions = () => {
		// % protected region % [Override getActionOptions here] off begin
		getTimelineActionOptions(this.timelineFilter.selectedTimelineEntity)
			.then(options => runInAction(() => {
				this.timelineFilter.actionTypeOptions = options;
				this.loadingStatus = 'Done';
			}))
		// % protected region % [Override getActionOptions here] end
	};

	@action
	private onSwitchToGraphView = () => {
		// % protected region % [Override onSwitchToGraphView here] off begin
		if (this.props.onRouteToGraphView){
			this.props.onRouteToGraphView()
		} else {
			this.component = 'graph';
		}
		// % protected region % [Override onSwitchToGraphView here] end
	};

	@action
	private onSwitchToListView = (date?: Date) => {
		// % protected region % [Override onSwitchToListView here] off begin
		if (this.props.onRouteToListView){
			this.props.onRouteToListView(date)
		} else {
			this.component = 'list';
			this.internalTimelineViewDate = date ? moment(date) : undefined;
		}
		// % protected region % [Override onSwitchToListView here] end
	};

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	public render() {
		// % protected region % [Override render here] off begin
		if (this.loadingStatus == 'Loading') {
			return <Spinner/>;
		}

		switch (this.props.component ?? this.component){
			case 'list':
				return <TimelineListView
					timelineViewDate={this.timelineViewDate}
					onClickViewItem={this.props.onClickViewItem}
					onSwitchToGraphView={this.onSwitchToGraphView}
					timelineFilter={this.timelineFilter}/>;
			case 'graph':
				return <TimelineGraphView
					onClickViewItem={this.props.onClickViewItem}
					onSwitchToListView={this.onSwitchToListView}
					timelineFilter={this.timelineFilter}/>;
		}
		// % protected region % [Override render here] end
	}
}