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
import {
	CompareDateString,
	GetTimelineGroupDateDisplay,
	GroupEventsByDate,
	SortEvents,
	TimelineEvent
} from 'Util/TimelineUtils';
import moment from "moment";
import {Waypoint} from "react-waypoint";
import {RefObject} from "react";
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface IListViewComponentProps {
	// % protected region % [Override IListViewComponentProps here] off begin
	timelineEvents: Array<TimelineEvent>
	onScrollToExtreme :(waypoint: Waypoint.CallbackArgs, date: Date, extremity: 'Bottom' | 'Top') => void;
	onClickViewItem: (id: string) => void;
	scrollSectionRef: RefObject<HTMLElement>;
	eventComponentsRefs: Array<RefObject<HTMLLIElement>>;
	listHeadingRefs: Array<RefObject<HTMLHeadingElement>>;
	// % protected region % [Override IListViewComponentProps here] end
}

export default class ListViewItems extends React.Component<IListViewComponentProps> {

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	public render() {

		const dateGroupedEvents = GroupEventsByDate(this.props.timelineEvents);
		const dateGroupKeys = Object.keys(dateGroupedEvents);
		let counter = 0;

		// % protected region % [Override render eventComponent here] off begin
		const eventComponent = (event: TimelineEvent, index: number) => {
			return (
				<li
					ref={this.props.eventComponentsRefs[counter - 1]}
					onClick={() => this.props.onClickViewItem(event.entityId)}
					className = "list-view__topic"
					key={index}>
					<div>
						<h5 className={"topic__title"}>{event.actionTitle}</h5>
						<p className={"topic__date"}>
							{moment(event.created).format('DD/MM/YYYY hh:mm:ss A')}
						</p>
						<p className={"topic__details"}>{event.description}</p>
					</div>
				</li>
			)
		};
		// % protected region % [Override render eventComponent here] end

		// % protected region % [Override render return here] off begin
		return (
			<section ref={this.props.scrollSectionRef} aria-label="timelines list view" className="timelines__list-view">
				<ol>
					{dateGroupKeys.sort(CompareDateString).map((date, key) =>
						<React.Fragment key={key}>
							<h4 ref={this.props.listHeadingRefs[key]}>
								{GetTimelineGroupDateDisplay(moment(date).toDate())}
							</h4>
							{dateGroupedEvents[date].sort(SortEvents).map((event, index) => {
									counter++;
									if (counter == 1 || counter == this.props.timelineEvents.length){
										const position = counter == 1? 'Top' : 'Bottom';
										return (
											<Waypoint
												key={index}
												onEnter={(waypoint) =>
													this.props.onScrollToExtreme(waypoint, event.created, position)}>
												{eventComponent(event, index)}
											</Waypoint>
										)
									}
									return eventComponent(event, index)
								}
							)}
						</React.Fragment>
					)}
				</ol>
			</section>
		);
		// % protected region % [Override render return here] end
	}
}