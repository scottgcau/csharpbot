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
import moment from "moment";
import classNames from "classnames";
import { action, autorun, IReactionDisposer, observable, runInAction } from 'mobx';
import {observer} from "mobx-react";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import {
	getActionShapeClassName,
	getTimelineEventEntity,
	getTimelineQueryWhereConditions,
	TimelineEvent
} from 'Util/TimelineUtils';
import {getFetchAllConditional, getModelName} from 'Util/EntityUtils';
import {store} from 'Models/Store';
import {lowerCaseFirst} from 'Util/StringUtils';
import cloneDeep from 'lodash/cloneDeep';
import {RouteComponentProps} from 'react-router';
import {TimelineGraphViewProps} from 'Timelines/TimelineGraphView/GraphViewGraph';
import Spinner from 'Views/Components/Spinner/Spinner';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface ITimelineGraphViewSidebarProps {
	// % protected region % [Override ITimelineGraphViewSidebarProps here] off begin
	timelineFilter: ITimelineFilter
	// % protected region % [Override ITimelineGraphViewSidebarProps here] end
}

@observer
export default class GraphViewSidebar extends React.Component<ITimelineGraphViewSidebarProps> {

	@observable
	private recentEvents: Array<TimelineEvent> = [];

	@observable
	private loadingState: 'Loading' | 'Done' | 'Error' = 'Loading';
	
	private readonly disposer : IReactionDisposer;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	constructor(props: TimelineGraphViewProps & RouteComponentProps) {
		// % protected region % [Override constructor here] off begin
		super(props);
		this.disposer = autorun(this.fetchRecentEvents)
		// % protected region % [Override constructor here] end
	}

	componentWillUnmount(): void {
		// % protected region % [Override componentWillUnmount here] off begin
		this.disposer();
		// % protected region % [Override componentWillUnmount here] end
	}

	private fetchRecentEvents = () => {
		// % protected region % [Override fetchRecentEvents here] off begin
		const eventEntityModel = getTimelineEventEntity(this.props.timelineFilter.selectedTimelineEntity);
		if (eventEntityModel) {
			const eventEntityName = getModelName(eventEntityModel);
			const filter = cloneDeep(this.props.timelineFilter);
			filter.startDate = undefined;
			filter.endDate = undefined;
			store.apolloClient
			.query({
				query: getFetchAllConditional(eventEntityModel),
				variables: { 
					args: getTimelineQueryWhereConditions(filter), 
					take: 7, 
					orderBy: [{ descending: true, path: 'Created' }] 
				},
				fetchPolicy: 'network-only'})
			.then(action((data) => {
					this.recentEvents = data.data[`${lowerCaseFirst(eventEntityName)}s`];
					this.loadingState = 'Done'}))
			.catch(action(() => this.loadingState = 'Error' ))
		}
		// % protected region % [Override fetchRecentEvents here] end
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
			<section aria-label="timelines recent activity and legend" className="timelines__sidebar">
				<div className="sidebar__graph-view">
					<div className="sidebar__recent-activity">
						<h5>Recent Activity</h5>
						<ul>
							{this.recentEvents.map((event, index) =>
								<li key={index} className={getActionShapeClassName(event.action)}>
									<span className="bold date">{moment(event.created).format('DD/MM/YYYY')}</span>
									{event.actionTitle}
								</li>
							)}
						</ul>
					</div>
					<div className="sidebar__legend">
						<h5>Legend</h5>
						<ul>
							{this.props.timelineFilter.actionTypeOptions.map((typeOfChange, index) =>
								<li
									key={index}
									className={classNames('bold', getActionShapeClassName(typeOfChange))}>
									{typeOfChange}
								</li>
							)}
						</ul>
					</div>
				</div>
			</section>
		);
		// % protected region % [Override render here] end
	}
}