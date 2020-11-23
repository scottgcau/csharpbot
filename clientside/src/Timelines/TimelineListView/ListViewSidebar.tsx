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
	DateRange,
	getEventEntityNameFromTimelineEntity,
	getTimelineQueryWhereConditions,
	QuickJumpTimeFrame,
} from 'Util/TimelineUtils';
import {Combobox} from 'Views/Components/Combobox/Combobox';
import moment from "moment";
import { action, autorun, IReactionDisposer, observable} from "mobx";
import {observer} from "mobx-react";
import axios from "axios";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import Spinner from 'Views/Components/Spinner/Spinner';
import { formatConditionsForRest } from 'Util/FetchUtils';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end


export interface IListViewQuickJumpSidebarProps {
	// % protected region % [Override IListViewQuickJumpSidebarProps here] off begin
	onQuickJump: (options: DateRange | undefined) => void;
	timelineFilter: ITimelineFilter;
	// % protected region % [Override IListViewQuickJumpSidebarProps here] end
}

@observer
export default class ListViewQuickJumpSidebar extends React.Component<IListViewQuickJumpSidebarProps> {

	@observable
	private quickJumpOptions: DateRange[] = [];
	
	@observable
	private timeFrameConfiguration = {
		selectedTimeFrame: QuickJumpTimeFrame.Months
	};

	@observable
	private loadingState: 'Loading' | 'Done' | 'Error' = 'Loading';
	
	private readonly disposer : IReactionDisposer;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	constructor(props: IListViewQuickJumpSidebarProps) {
		// % protected region % [Override constructor here] off begin
		super(props);
		this.disposer = autorun(this.getQuickJumpOptions)
		// % protected region % [Override constructor here] end
	}
	
	componentWillUnmount(): void {
		// % protected region % [Override componentWillUnmount here] off begin
		this.disposer();
		// % protected region % [Override componentWillUnmount here] end
	}

	/**
	 * Fetches quick-jump sidebar options from the server and updates the component state to
	 * display them
	 */
	private getQuickJumpOptions = () => {
		// % protected region % [Override getQuickJumpOptions here] off begin
		const timelineFilter = {
			timeFrame: this.timeFrameConfiguration.selectedTimeFrame,
			conditions: formatConditionsForRest(getTimelineQueryWhereConditions(this.props.timelineFilter))
		};
		const timelineEntity = this.props.timelineFilter.selectedTimelineEntity;
		const eventEntityName = getEventEntityNameFromTimelineEntity(timelineEntity);
		axios
			.post<Array<DateRange>>(`/api/entity/${eventEntityName}/quick-jump-options`, timelineFilter)
			.then(action((data) => {
				this.quickJumpOptions = data.data;
				this.loadingState = 'Done';
			}))
			.catch(action(() => this.loadingState = 'Error' ))
		// % protected region % [Override getQuickJumpOptions here] end
	};

	/*
	* Formats how quick jump options are displayed depending on the user specified timeframe
	* of weeks or months
	* */
	private formatQuickJumpOption = (date: Date, timeFrame: QuickJumpTimeFrame) => {
		// % protected region % [Override formatQuickJumpOption here] off begin
		switch(timeFrame) {
			case QuickJumpTimeFrame.Months:
				return moment(date).format('MM/YYYY');
			case QuickJumpTimeFrame.Weeks:
				return moment(date).format('DD/MM/YYYY')
		}
		// % protected region % [Override formatQuickJumpOption here] end
	};
	
	public render() {
		// % protected region % [Override render here] off begin
		if (this.loadingState == 'Error'){
			return 'Error';
		}

		if (this.loadingState == 'Loading'){
			return <Spinner/>
		}
		
		const quickJumpTimeFrameOptions = [
			{display: 'Months', value: QuickJumpTimeFrame.Months},
			{display: 'Weeks', value: QuickJumpTimeFrame.Weeks}
		];
		
		return (
			<section aria-label="timelines list view sidebar" className="timelines__sidebar">
				<div className="sidebar__list-view">
					<div className="sidebar__list-view__header">
						<h5> Quick Jump </h5>
						<Combobox
							model={this.timeFrameConfiguration}
							modelProperty={'selectedTimeFrame'}
							options={quickJumpTimeFrameOptions}
							className={'timelines__entities'}
							label={'Quick Jump TimeFrame'}
							labelVisible={false}/>
					</div>
					<ol>
						<a>
							<li onClick={() => this.props.onQuickJump(undefined)} className="bold">Back to top</li>
						</a>
						{this.quickJumpOptions.map((option, index) => {
							return (
								<a key={index} onClick={() => this.props.onQuickJump(option)}>
									<li className="bold">{this.formatQuickJumpOption(option.startDate, this.timeFrameConfiguration.selectedTimeFrame)}</li>
								</a>
							)
						})}
					</ol>
				</div>
			</section>
		);
		// % protected region % [Override render here] end
	}
}