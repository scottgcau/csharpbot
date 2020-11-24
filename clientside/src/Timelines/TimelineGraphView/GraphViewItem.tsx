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
import classNames from "classnames";
import moment from "moment";
import {GraphWindowLimits, TimelineGroupDateQueryResult} from './GraphViewGraph'
import {DateRange, getActionShapeClassName, TimelineEvent} from 'Util/TimelineUtils';
import {observer} from "mobx-react";
import {Button} from 'Views/Components/Button/Button';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface IGraphViewEntryProps {
	// % protected region % [Override IGraphViewEntryProps here] off begin
	graphWindowLimits: GraphWindowLimits
	className?: string;
	graphViewEntry: TimelineGroupDateQueryResult;
	onClick: (graphItem: TimelineGroupDateQueryResult) => void;
	cardEvents? : Array<TimelineEvent>;
	onClickViewInListView: (date: Date) => void;
	onClickViewItem: (id: string) => void;
	showDate: boolean;
	dateFormatString: string;
	onClickZoom: (range: DateRange) => void;
	// % protected region % [Override IGraphViewEntryProps here] end
}

@observer
export default class GraphViewItem extends React.Component<IGraphViewEntryProps> {

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	public render() {

		// % protected region % [Override render variables here] off begin
		const {numberOfResults} = this.props.graphViewEntry;
		const startDate = moment(this.props.graphViewEntry.dateTimeGroup.startDate);
		startDate.set("seconds", 0);
		startDate.set("minutes", 0);
		// % protected region % [Override render variables here] end

		// % protected region % [Override render item here] off begin
		const dateItemInformation = () => {
			const windowTicks = this.props.graphWindowLimits.endDate.diff(this.props.graphWindowLimits.startDate);
			const itemTicks = this.props.graphWindowLimits.endDate.diff(this.props.graphViewEntry.dateTimeGroup.startDate);

			let positionalClassName = itemTicks < windowTicks/2 ? 'right' : 'left';
			
			const zoomToViewMoreComponent = (
				<a onClick={() => this.props.onClickZoom( this.props.graphViewEntry.dateTimeGroup)}>
					{`Zoom to view ${numberOfResults == 2 ? '' : `${numberOfResults - 2} more`}`}
				</a>
			);
						
			return (
				<div className={classNames("item__info", positionalClassName)}>
					<Button icon={{icon: 'cross', iconPos:"icon-left"}} className="icon-only" />
					{this.props.cardEvents?.map((event,index) =>
						<React.Fragment key={index}>
							<div className="info">
								<div className="info__top-section">
									<h5> {event.actionTitle} </h5>
								</div>
								<div
									key={index}
									onClick={() => this.props.onClickViewItem(event.entityId)}
									className="info__middle-section">
									<p> {event.description} </p>
									<p> {moment(event.created).format('HH:mm DD/MM/YY')} </p>
								</div>
							</div>
						</React.Fragment>
					)}
					<div className="bottom-section">
						{this.props.graphViewEntry.numberOfResults > 1 ? zoomToViewMoreComponent : null}
						<a
							onClick={() => this.props.onClickViewInListView(this.props.graphViewEntry.dateTimeGroup.startDate)}>
							Click to view in list view
						</a>
					</div>
				</div>
			)
		};
		// % protected region % [Override render item here] end

		// % protected region % [Override render date here] off begin
		const dateComponent = this.props.showDate 
			? startDate.minutes(0).seconds(0).format(this.props.dateFormatString)
			: null;

		if (!this.props.graphViewEntry.firstResult) {
			return (
				<section className={classNames("date__item", !this.props.showDate ? "date__middle" : null)}>
					{this.horizontalAxisDateItem(dateComponent)}
				</section>
			)
		}
		// % protected region % [Override render date here] end

		// % protected region % [Override render return here] off begin
		return (
			<section onClick={() => this.props.onClick(this.props.graphViewEntry)} className="date__item">
				<div
					className={classNames(numberOfResults > 1 ? 'timelines__items' : 'timelines__item',
							getActionShapeClassName(this.props.graphViewEntry.firstResult?.action))}>
					<div className="item__amount">
						<span>{this.props.graphViewEntry.numberOfResults}</span>
					</div>
					{this.props.graphViewEntry.showInformation ? dateItemInformation() : null}
					<span className="item-line"> </span>
					<span className="bottom-circle"> </span>
				</div>
				{this.horizontalAxisDateItem(dateComponent)}
			</section>
		);
		// % protected region % [Override render return here] end
	}

	private horizontalAxisDateItem = (dateString: string | null) => <span className={'date'}>{dateString}</span>
}