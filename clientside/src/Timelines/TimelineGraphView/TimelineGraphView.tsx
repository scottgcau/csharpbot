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
import TimelineTopBar from "../Shared/TimelineTopBar";
import GraphViewTopBarControls from "./GraphViewControls";
import moment, {Moment} from "moment";
import {observer} from "mobx-react";
import {action, observable} from 'mobx';
import cloneDeep from "lodash/cloneDeep";
import GraphViewSidebar from "./GraphViewSidebar";
import GraphViewGraph from "./GraphViewGraph";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import {DateRange, getTimelineEventEntity, getTimelineQueryWhereConditions, TimelineEvent} from 'Util/TimelineUtils';
import {getFetchAllConditional, getModelName} from 'Util/EntityUtils';
import {store} from 'Models/Store';
import {lowerCaseFirst} from 'Util/StringUtils';
import {Model} from 'Models/Model';
import Spinner from 'Views/Components/Spinner/Spinner';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface ITimelineGraphViewProps {
	// % protected region % [Override ITimelineGraphViewProps here] off begin
	timelineFilter: ITimelineFilter
	onClickViewItem?: (entity: string, id: string) => void;
	onSwitchToListView: (date?: Date) => void;
	// % protected region % [Override ITimelineGraphViewProps here] end
}

export interface GraphWindowLimits {
	// % protected region % [Override GraphWindowLimits here] off begin
	startDate: Moment,
	endDate: Moment
	// % protected region % [Override GraphWindowLimits here] end
}

@observer
export default class TimelineGraphView extends React.Component<ITimelineGraphViewProps> {

	@observable
	public graphWindowLimits: GraphWindowLimits = {
		// % protected region % [Override graphWindowLimits here] off begin
		startDate: moment(Date.now()).subtract(1, "month"),
		endDate: moment(Date.now())
		// % protected region % [Override graphWindowLimits here] end
	};
	
	@observable
	private loadingState: 'Loading' | 'Done' | 'Error' = 'Loading';

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	componentDidMount(): void {
		// % protected region % [Override componentDidMount here] off begin
		this.onZoomBackToDefault();
		// % protected region % [Override componentDidMount here] end
	}

	public animationClassName = '';

	@action
	private onZoomIn = () => {
		// % protected region % [Override onZoomIn here] off begin
		const graphWindowLimits = cloneDeep(this.graphWindowLimits);
		const ticks = graphWindowLimits.endDate.diff(graphWindowLimits.startDate);
		const startDate = graphWindowLimits.startDate.add(ticks/4, "ms");
		const endDate = graphWindowLimits.endDate.add(-ticks/4, "ms");
		const zoomInTickLimit = moment(Date.now()).add(4, 'hours').diff(moment(Date.now()));
		if(endDate.diff(startDate) > zoomInTickLimit) {
			this.graphWindowLimits = {startDate: startDate, endDate: endDate};
			this.animationClassName = this.animationClassName === 'view-zoom' ? 'zoom-view' : 'view-zoom';
		}
		// % protected region % [Override onZoomIn here] end
	};

	@action
	private onZoomOut = () => {
		// % protected region % [Override onZoomOut here] off begin
		const graphWindowLimits = cloneDeep(this.graphWindowLimits);
		const ticks = graphWindowLimits.endDate.diff(graphWindowLimits.startDate);
		const startDate = graphWindowLimits.startDate.add(-ticks/2, "ms");
		const endDate = graphWindowLimits.endDate.add(ticks/2, "ms");
		const zoomOutTickLimit = moment(Date.now()).add(10, 'years').diff(moment(Date.now()));
		if(endDate.diff(startDate) < zoomOutTickLimit) {
			this.graphWindowLimits = {startDate: startDate, endDate: endDate};
			this.animationClassName = this.animationClassName === 'view-zoom' ? 'zoom-view' : 'view-zoom';
		}
		// % protected region % [Override onZoomOut here] end
	};

	@action
	private onPanLeft = () => {
		// % protected region % [Override onPanLeft here] off begin
		const ticks = this.graphWindowLimits.endDate.diff(this.graphWindowLimits.startDate);
		const endDate = cloneDeep(this.graphWindowLimits.startDate)
		const startDate = this.graphWindowLimits.startDate.add(-ticks, "ms")
		this.graphWindowLimits = {startDate: startDate, endDate: endDate};
		this.animationClassName = this.animationClassName ===  'view-left' ? 'left-view' : 'view-left';
		// % protected region % [Override onPanLeft here] end
	};

	@action
	private onPanRight = () => {
		// % protected region % [Override onPanRight here] off begin
		const ticks = this.graphWindowLimits.endDate.diff(this.graphWindowLimits.startDate);
		const startDate = cloneDeep(this.graphWindowLimits.endDate);
		const endDate = this.graphWindowLimits.endDate.add(ticks, "ms");
		this.graphWindowLimits = {startDate: startDate, endDate: endDate};
		this.animationClassName = this.animationClassName ===  'view-right' ? 'right-view' : 'view-right';
		// % protected region % [Override onPanRight here] end
	};

	@action
	private onJumpToToday = () => {
		// % protected region % [Override onJumpToToday here] off begin
		this.graphWindowLimits = {
			startDate: moment(Date.now()).startOf("day"),
			endDate: moment(Date.now()).add(1, "day").startOf("day")
		}
		// % protected region % [Override onJumpToToday here] end
	};

	
	private onZoomBackToDefault = () => {
		// % protected region % [Override onZoomBackToDefault here] off begin
		const prevMonthDate = moment(Date.now()).add(-1, "month");
		const eventEntityModel = getTimelineEventEntity(this.props.timelineFilter.selectedTimelineEntity);
		if (eventEntityModel){
			this.fetchDefaultViewStartDate(eventEntityModel)
				.then(startDate => {
					if (!startDate || startDate > prevMonthDate) {
						startDate = prevMonthDate;
					}
					this.setDefaultZoom(startDate)
				});
		} else {
			this.setDefaultZoom(prevMonthDate)
		}
		// % protected region % [Override onZoomBackToDefault here] end
	};
	
	@action
	private setDefaultZoom  = (startDate: Moment) => {
		// % protected region % [Override setDefaultZoom here] off begin
		this.graphWindowLimits = {
			startDate: startDate,
			endDate: moment(Date.now())
		};
		if (this.loadingState == 'Loading'){
			this.loadingState = 'Done';
		}
		// % protected region % [Override setDefaultZoom here] end
	};

	private fetchDefaultViewStartDate = (eventEntityModel: { new(): Model }) => {
		// % protected region % [Override fetchDefaultViewStartDate here] off begin
		const filter = cloneDeep(this.props.timelineFilter);
		filter.startDate = undefined;
		filter.endDate = undefined;
		const searchConditions = getTimelineQueryWhereConditions(filter);
		const eventEntityName = getModelName(eventEntityModel);
		return store.apolloClient
			.query({
				query: getFetchAllConditional(eventEntityModel),
				variables: { args: searchConditions, take: 1, orderBy: [{ descending: false, path: 'Created' }] },
				fetchPolicy: 'network-only'
			})
			.then((data) => {
				const fetchedEntities: Array<TimelineEvent> = data.data[`${lowerCaseFirst(eventEntityName)}s`];
				return fetchedEntities.length > 0 ? moment(fetchedEntities[0].created) : undefined
			})
			.catch(action(() => {
				this.loadingState = 'Error';
				return undefined;
			}))
		// % protected region % [Override fetchDefaultViewStartDate here] end
	};
	
	@action
	private onClickZoomItem  = (range: DateRange) => {
		// % protected region % [Override onClickZoomItem here] off begin
		this.graphWindowLimits = {
			startDate: moment(range.startDate),
			endDate: moment(range.endDate)
		}
		// % protected region % [Override onClickZoomItem here] end
	};
	
	public render() {
		// % protected region % [Override render here] off begin
		if (this.loadingState == 'Error'){
			return 'Error';
		}

		if (this.loadingState == 'Loading'){
			return <Spinner/>
		}
		
		return (
			<section className="timelines-behaviour">
				<TimelineTopBar timelineFilter={this.props.timelineFilter}>
					<GraphViewTopBarControls
						onPanRight={this.onPanRight}
						onPanLeft={this.onPanLeft}
						onJumpToToday={this.onJumpToToday}
						onZoomBackToDefault={this.onZoomBackToDefault}
						onZoomIn={this.onZoomIn}
						onZoomOut={this.onZoomOut}
						onSwitchToListView={this.props.onSwitchToListView}
					/>
				</TimelineTopBar>
				<GraphViewGraph 
					onClickZoomItem={this.onClickZoomItem}
					onPanLeft={this.onPanLeft}
					onPanRight={this.onPanRight}
					onClickViewItem={this.props.onClickViewItem}
					onClickViewInListView={this.props.onSwitchToListView}
					graphWindowLimits={this.graphWindowLimits}
					timelineFilter={this.props.timelineFilter}
					animationClassName={this.animationClassName}
				/>
				<GraphViewSidebar timelineFilter={this.props.timelineFilter} />
			</section>
		);
		// % protected region % [Override render here] end
	}
}