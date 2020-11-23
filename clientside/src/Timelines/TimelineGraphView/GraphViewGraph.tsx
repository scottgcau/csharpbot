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
import {action, autorun, IReactionDisposer, observable, runInAction} from "mobx";
import axios from 'axios';
import moment, {Moment} from "moment";
import {observer} from "mobx-react";
import classNames from "classnames";
import {RouteComponentProps} from "react-router";
import GraphViewItem from "./GraphViewItem";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import {
	DateRange,
	getEventEntityNameFromTimelineEntity,
	getTimelineEventEntity,
	getTimelineQueryWhereConditions,
	TimelineEvent
} from 'Util/TimelineUtils';
import {getFetchAllConditional, getModelDisplayName, getModelName} from 'Util/EntityUtils';
import {store} from 'Models/Store';
import {lowerCaseFirst} from 'Util/StringUtils';
import {Button} from 'Views/Components/Button/Button';
import cloneDeep from 'lodash/cloneDeep';
import Spinner from 'Views/Components/Spinner/Spinner';
import { formatConditionsForRest } from 'Util/FetchUtils';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface GraphWindowLimits {
	// % protected region % [Override GraphWindowLimits here] off begin
	startDate: Moment,
	endDate: Moment
	// % protected region % [Override GraphWindowLimits here] end
}

export interface TimelineGraphViewProps {
	// % protected region % [Override TimelineGraphViewProps here] off begin
	onPanLeft: () => void;
	onPanRight: () => void;
	timelineFilter: ITimelineFilter;
	graphWindowLimits: GraphWindowLimits
	onClickViewInListView: (date: Date) => void;
	onClickViewItem?: (entity: string, id: string) => void;
	animationClassName: string;
	onClickZoomItem: (range: DateRange) => void;
	// % protected region % [Override TimelineGraphViewProps here] end
}

export interface TimelineGroupDateQueryResult {
	// % protected region % [Override TimelineGroupDateQueryResult here] off begin
	firstResult: TimelineEvent,
	numberOfResults: number,
	dateTimeGroup: DateRange,
	showInformation: boolean
	// % protected region % [Override TimelineGroupDateQueryResult here] end
}

@observer
export default class GraphViewGraph extends React.Component<TimelineGraphViewProps> {

	@observable
	private timelineGroupDateQueryResults: Array<TimelineGroupDateQueryResult> = [];

	@observable
	private recentEvents: Array<TimelineEvent> = [];

	@observable
	private timelineEventsForCardDisplay : Array<TimelineEvent> = [];
	
	@observable
	private loadingState: 'Loading' | 'Done' | 'Error' = 'Loading';

	private readonly disposer : IReactionDisposer;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	constructor(props: TimelineGraphViewProps & RouteComponentProps) {
		// % protected region % [Override constructor here] off begin
		super(props);
		this.disposer = autorun(this.fetchGraphData)
		// % protected region % [Override constructor here] end
	}

	componentWillUnmount(): void {
		// % protected region % [Override componentWillUnmount here] off begin
		this.disposer();
		// % protected region % [Override componentWillUnmount here] end
	}

	private fetchTimelineEventsForCardDisplay = (limits: GraphWindowLimits) => {
		// % protected region % [Override fetchTimelineEventsForCardDisplay here] off begin
		const eventEntityModel = getTimelineEventEntity(this.props.timelineFilter.selectedTimelineEntity);
		const filter = cloneDeep(this.props.timelineFilter);
		filter.startDate = limits.startDate.toDate();
		filter.endDate = limits.endDate.toDate();
		const searchConditions = getTimelineQueryWhereConditions(filter);
		if (eventEntityModel) {
			const eventEntityName = getModelName(eventEntityModel);
			store.apolloClient
				.query({
					query: getFetchAllConditional(eventEntityModel),
					variables: { args: searchConditions, take: 2, orderBy: [{ descending: true, path: 'Created' }] },
					fetchPolicy: 'network-only',})
				.then(action((data) => this.timelineEventsForCardDisplay = data.data[`${lowerCaseFirst(eventEntityName)}s`]))
				.catch(action(() => this.loadingState = 'Error' ))
		}
		// % protected region % [Override fetchTimelineEventsForCardDisplay here] end
	};
	
	private fetchGraphData = () => {
		// % protected region % [Override fetchGraphData here] off begin
		runInAction(() => this.loadingState = 'Loading');
		const timelineFilter = {
			conditions: formatConditionsForRest(getTimelineQueryWhereConditions(this.props.timelineFilter)),
			dateRange: {
				startDate: this.props.graphWindowLimits.startDate.format('YYYY-MM-DD HH:mm:ss'),
				endDate: this.props.graphWindowLimits.endDate.format('YYYY-MM-DD HH:mm:ss'),
			}
		};
		axios
			.post<Array<TimelineGroupDateQueryResult>>(
			`/api/entity/${getEventEntityNameFromTimelineEntity(this.props.timelineFilter.selectedTimelineEntity)}/timeline-graph-data`, timelineFilter)
			.then(action((data) => {
					this.timelineGroupDateQueryResults = data.data;
					this.loadingState = 'Done';
			}))
			.catch(action(() => this.loadingState = 'Error' ))
		// % protected region % [Override fetchGraphData here] end
	};

	@action
	private toggleGraphItemMoreInformationView = (graphItem: TimelineGroupDateQueryResult) => {
		// % protected region % [Override toggleGraphItemMoreInformationView here] off begin
		const showInformation = !graphItem.showInformation;
		this.timelineGroupDateQueryResults.map(x => x.showInformation = false);
		graphItem.showInformation = showInformation;
		this.timelineEventsForCardDisplay = [];
		this.fetchTimelineEventsForCardDisplay({
			startDate: moment(graphItem.dateTimeGroup.startDate),
			endDate: moment(graphItem.dateTimeGroup.endDate)
		});
		// % protected region % [Override toggleGraphItemMoreInformationView here] end
	};

	private getDateDisplayIndices = () => {
		// % protected region % [Override getDateDisplayIndices here] off begin
		const size = this.timelineGroupDateQueryResults.length;
		const increment = Math.round(size / 6);
		const displayIndices = [];
		for (let i = 0; i < size; i ++) {
			if(!(i % increment)) {
				displayIndices.push(i)
			}
		}
		return displayIndices;
		// % protected region % [Override getDateDisplayIndices here] end
	};

	private getDateFormatString = (dateDisplayIndices: number[]) => {
		// % protected region % [Override getDateFormatString here] off begin
		let dateFormatString = 'DD/MM/YYYY HH:mm';
		
		if (!dateDisplayIndices[2]) {
			return dateFormatString;
		}
		
		const startDate = moment(this.timelineGroupDateQueryResults[dateDisplayIndices[1]].dateTimeGroup.startDate);
		const endDate = moment(this.timelineGroupDateQueryResults[dateDisplayIndices[2]].dateTimeGroup.startDate);
		const hoursDiff = endDate.diff(startDate, "hours");

		switch (true) {
			case (hoursDiff >= (24 * 2) && hoursDiff < (28 * 24)):
				dateFormatString = 'DD/MM/YYYY';
				break;
			case (hoursDiff >= (28 * 24) && hoursDiff < (24 * 360)):
				dateFormatString = 'MM/YYYY';
				break;
			case (hoursDiff >= (24 * 360)):
				dateFormatString = 'MM/YYYY';
				break;
		}
		return dateFormatString;
		// % protected region % [Override getDateFormatString here] end
	};

	private onClickViewItem = (entity: string, id: string) => {
		// % protected region % [Override onClickViewItem here] off begin
		this.props.onClickViewItem?.(entity, id)
		// % protected region % [Override onClickViewItem here] end
	};

	public render() {
		// % protected region % [Override render here] off begin
		
		if (this.loadingState == 'Error'){
			return 'Error';
		}
		
		if (this.loadingState == 'Loading'){
			return <Spinner/>
		}
		
		const dateDisplayIndices = this.getDateDisplayIndices();
		const dateFormatString = this.getDateFormatString(dateDisplayIndices);
		
		return (
			<>
				<section aria-label="timelines view" className="timelines__view">
					<Button onClick={this.props.onPanLeft} icon={{ icon: 'chevron-left', iconPos: 'icon-left' }} />
					<section className="timelines">
						<section className={classNames("timelines__dates", this.props.animationClassName)}>
							{this.timelineGroupDateQueryResults.map((graphItem, index) => 
								<GraphViewItem 
									key={index} 
									onClickZoom={this.props.onClickZoomItem}
									dateFormatString={dateFormatString}
									showDate={dateDisplayIndices.includes(index)}
									onClickViewItem={(id: string) => this.onClickViewItem(this.props.timelineFilter.selectedTimelineEntity.name, id)}
									onClickViewInListView={this.props.onClickViewInListView}
									graphWindowLimits={this.props.graphWindowLimits}
									cardEvents={graphItem.showInformation ? this.timelineEventsForCardDisplay : undefined}
									onClick={this.toggleGraphItemMoreInformationView} 
									graphViewEntry={graphItem}/>
							)}
						</section>
					</section>
					<Button onClick={this.props.onPanRight} icon={{ icon: 'chevron-right', iconPos: 'icon-right' }} />
				</section>
			</>
		);
		// % protected region % [Override render here] end
	}
}