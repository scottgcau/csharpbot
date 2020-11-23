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
import ListViewList from "./ListViewList";
import {observer} from "mobx-react";
import {action, observable} from "mobx";
import {DateRange} from 'Util/TimelineUtils';
import moment, {Moment} from "moment";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import ListViewTopBarControls from 'Timelines/TimelineListView/ListViewControls';
import ListViewQuickJumpSidebar from 'Timelines/TimelineListView/ListViewSidebar';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface ITimelineListViewProps {
	// % protected region % [Override ITimelineListViewProps here] off begin
	timelineFilter: ITimelineFilter
	onClickViewItem?: (entity: string, id: string) => void;
	onSwitchToGraphView: () => void;
	timelineViewDate?: Moment;
	// % protected region % [Override ITimelineListViewProps here] end
}

@observer
export default class TimelineListView extends React.Component<ITimelineListViewProps> {
	
	@observable
	quickJumpDate: Moment | undefined = this.props.timelineViewDate;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	@action
	private onQuickJump = (quickJumpDate: DateRange | undefined) => {
		// % protected region % [Override onQuickJump here] off begin
		this.quickJumpDate = quickJumpDate ? moment(quickJumpDate.startDate) : undefined;
		// % protected region % [Override onQuickJump here] end
	};
	
	public render() {
		// % protected region % [Override render here] off begin
		return (
			<section className="timelines-behaviour">
				<TimelineTopBar 
					timelineFilter={this.props.timelineFilter}>
					<ListViewTopBarControls onClick={this.props.onSwitchToGraphView}/>
				</TimelineTopBar>
				<ListViewList
					onClickViewItem={this.props.onClickViewItem}
					quickJumpDate={this.quickJumpDate}
					timelineFilter={this.props.timelineFilter}/>
				<ListViewQuickJumpSidebar 
					onQuickJump={this.onQuickJump}
					timelineFilter={this.props.timelineFilter} />
			</section>
		);
		// % protected region % [Override render here] end
	}
}