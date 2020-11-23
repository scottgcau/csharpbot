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
	CompareDateString, getQuickJumpConditional, getTimelineEventEntity,
	getTimelineQueryWhereConditions, GroupEventsByDate, QuickJumpConditionalResult,
	SortEvents, TimelineEvent
} from 'Util/TimelineUtils';
import moment, {Moment} from "moment";
import {Waypoint} from "react-waypoint";
import {RefObject} from "react";
import {createRef} from "react";
import { action, IReactionDisposer, observable, reaction} from "mobx";
import {getFetchAllConditional, getModelName} from 'Util/EntityUtils';
import {store} from 'Models/Store';
import {lowerCaseFirst} from 'Util/StringUtils';
import {RouteComponentProps} from "react-router";
import ListViewItems from "./ListViewItems";
import cloneDeep from "lodash/cloneDeep";
import _ from "lodash";
import {observer} from "mobx-react";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import Spinner from 'Views/Components/Spinner/Spinner';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface IListViewListProps {
	// % protected region % [Override IListViewListProps here] off begin
	timelineFilter: ITimelineFilter;
	quickJumpDate: Moment | undefined;
	onClickViewItem?: (entity: string, id: string) => void;
	// % protected region % [Override IListViewListProps here] end
}

interface ScrollCorrectionSettings {
	// % protected region % [Override ScrollCorrectionSettings here] off begin
	doScrollCorrection: boolean;
	scrollHeightOffset: number;
	targetEventIndex: number | undefined;
	scrollCorrectionType: 'Quick-Jump' | 'Load-More' | undefined;
	// % protected region % [Override ScrollCorrectionSettings here] end
}

@observer
export default class ListViewList extends React.Component<IListViewListProps> {

	@observable
	private timelineEvents: Array<TimelineEvent> = [];

	@observable
	private loadingState: 'Loading' | 'Done' | 'Error' = 'Done';

	private orderBy =  [{ descending: true, path: 'Created' }];

	private scrollSectionRef: RefObject<HTMLElement> = createRef<HTMLElement>();
	private eventComponentsRefs: Array<RefObject<HTMLLIElement>> = [];
	private listHeadingRefs: Array<RefObject<HTMLHeadingElement>> = [];

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end

	private scrollCorrectionSettings: ScrollCorrectionSettings = {
		// % protected region % [Override scrollCorrectionSettings here] off begin
		doScrollCorrection: false,
		scrollHeightOffset: 0,
		targetEventIndex: undefined,
		scrollCorrectionType: undefined
		// % protected region % [Override scrollCorrectionSettings here] end
	};

	private readonly disposerQuickJump : IReactionDisposer;
	private readonly disposerFilter : IReactionDisposer;

	constructor(props: IListViewListProps & RouteComponentProps) {
		// % protected region % [Override constructor here] off begin
		super(props);
		this.updateComponent();
		this.disposerQuickJump = reaction(() => this.props.quickJumpDate, this.updateComponent);
		this.disposerFilter = reaction(() => ({...this.props.timelineFilter}), this.updateComponent);
		// % protected region % [Override constructor here] end
	}

	componentWillUnmount(): void {
		// % protected region % [Override componentWillUnmount here] off begin
		this.disposerQuickJump();
		this.disposerFilter();
		// % protected region % [Override componentWillUnmount here] end
	}
	
	private updateComponent = () => {
		// % protected region % [Override updateComponent here] off begin
		if (this.loadingState == 'Loading'){
			return;
		}
		this.onQuickJump(this.props.quickJumpDate);
		// % protected region % [Override updateComponent here] end
	};

	componentDidUpdate(): void {
		// % protected region % [Override componentDidUpdate here] off begin
		if (this.scrollCorrectionSettings.doScrollCorrection) {
			this.doScrollCorrection()
		}
		// % protected region % [Override componentDidUpdate here] end
	}

	/**
	 * Will run scroll correction after quick-jumping or infinite scrolling upwards
	 */
	private doScrollCorrection = () => {
		// % protected region % [Override doScrollCorrection here] off begin
		const scrollOffset = this.calculateScrollTotalOffset();
		this.scrollSectionRef.current?.scrollTo(0, scrollOffset);
		this.scrollCorrectionSettings = {
			doScrollCorrection: false,
			scrollHeightOffset: 0,
			targetEventIndex: undefined,
			scrollCorrectionType: undefined,
		}
		// % protected region % [Override doScrollCorrection here] end
	};

	/**
	 * Calculates in pixes how far to scroll to focus on an event. Is used when quick-jumping in the list view,
	 * or infinite scrolling upwards.
	 */
	private calculateScrollTotalOffset = (): number => {
		// % protected region % [Override calculateScrollTotalOffset here] off begin
		const orderedTimelineEvents = this.timelineEvents.slice().sort(SortEvents);

		const eventsAboveTarget = _.slice(orderedTimelineEvents, 0 , this.scrollCorrectionSettings.targetEventIndex);
		const dateGroupedEventsAboveTarget = GroupEventsByDate(eventsAboveTarget);
		const dateGroupKeys = Object.keys(dateGroupedEventsAboveTarget);

		// height of all the list entries/events shown in the scroll view above the target event/entry
		let eventsAboveTargetTotalHeight =  _.chain(this.eventComponentsRefs)
			.slice(0, eventsAboveTarget.length)
			.map(x => x.current?.getBoundingClientRect().height)
			.sum()
			.value();

		// quick jump should quick jump to the date heading, whereas scroll up/ load more
		// is not focused on headings.
		let numHeadingsAbove = dateGroupKeys.length;

		if (this.scrollCorrectionSettings.scrollCorrectionType === 'Quick-Jump'){
			numHeadingsAbove += 1;
		}

		// sum the heights of all the headings, but only include margin for the last heading.
		let headingsAboveTargetTotalHeight = _.chain(this.listHeadingRefs)
			.slice(0, numHeadingsAbove)
			.map((x, index) => {
				let marginHeight = 0;
				if (x.current) {
					marginHeight =Number(window.getComputedStyle(x.current)['marginTop'].replace('px', ''))
				}
				if (index == numHeadingsAbove - 1){
					return marginHeight;
				}
				return x.current? x.current.getBoundingClientRect().height + marginHeight : 0;
			})
			.sum()
			.valueOf();

		return eventsAboveTargetTotalHeight + headingsAboveTargetTotalHeight + this.scrollCorrectionSettings.scrollHeightOffset;
		// % protected region % [Override calculateScrollTotalOffset here] end
	};

	/**
	 * Called when a quick jump item has been selected.
	 */
	 @action
	private onQuickJump = (option: Moment | undefined) => {
		// % protected region % [Override onQuickJump here] off begin
		this.loadingState = 'Loading';
		if ( !option ) {
			this.quickJumpBackToTop()
		} else {
			this.quickJumpToDate(option);
		}
		// % protected region % [Override onQuickJump here] end
	};

	/**
	 * Executes a quick jump to the top of the list
	 */
	private quickJumpBackToTop = () => {
		// % protected region % [Override quickJumpBackToTop here] off begin
		const filter = cloneDeep(this.props.timelineFilter);
		filter.endDate = undefined;
		this.fetchEvents(filter)
			?.then(action((events) => {
				this.timelineEvents = events;
				this.loadingState = 'Done';
				this.scrollCorrectionSettings = {
					doScrollCorrection: true,
					scrollHeightOffset: 0,
					targetEventIndex: 0,
					scrollCorrectionType: 'Quick-Jump',
				};
			}));
		// % protected region % [Override quickJumpBackToTop here] end
	};

	private getQuickJumpTargetEvents = (date: Moment): Promise<Array<TimelineEvent>> | undefined   => {
		// % protected region % [Override getQuickJumpTargetEvents here] off begin
		const eventEntityModel = getTimelineEventEntity(this.props.timelineFilter.selectedTimelineEntity);
		const filter = cloneDeep(this.props.timelineFilter);
		filter.startDate = date.toDate();
		const searchConditions = getTimelineQueryWhereConditions(filter);
		if (eventEntityModel) {
			const eventEntityName = getModelName(eventEntityModel);
			return store.apolloClient
				.query({
					query: getFetchAllConditional(eventEntityModel),
					variables: { args: searchConditions, take: 1, orderBy: [{ descending: false, path: 'Created' }] },
					fetchPolicy: 'network-only',
				})
				.then((data) => {
					return data.data[`${lowerCaseFirst(eventEntityName)}s`];
				})
				.catch(action(() => this.loadingState = 'Error' ));
		}
		return undefined;
		// % protected region % [Override getQuickJumpTargetEvents here] end
	};
	
	private quickJumpToDate = (date: Moment) => {
		// % protected region % [Override quickJumpToDate here] off begin
		this.getQuickJumpTargetEvents(date)?.then(action((events) => {
			if (events.length === 0){
				this.loadingState = 'Done';
				return;
			}
			this.sendQuickJumpQuery(moment(events[0].created).endOf('day'))
		}))
		// % protected region % [Override quickJumpToDate here] end
	};

	/**
	 * Fetches events for a given quick-jump date
	 */
	private sendQuickJumpQuery = (date: Moment) => {
		// % protected region % [Override sendQuickJumpQuery here] off begin
		const preDateFilter = cloneDeep(this.props.timelineFilter);
		preDateFilter.endDate = date.toDate();
		const preDateSearchConditions = getTimelineQueryWhereConditions(preDateFilter);

		const postDateFilter = cloneDeep(this.props.timelineFilter);
		postDateFilter.startDate = date.toDate();
		const postDateSearchConditions = getTimelineQueryWhereConditions(postDateFilter);

		const eventEntityModel = getTimelineEventEntity(this.props.timelineFilter.selectedTimelineEntity);

		if (eventEntityModel) {
			store.apolloClient
				.query({
					query: getQuickJumpConditional(eventEntityModel),
					variables: {
						argsPre: preDateSearchConditions,
						argsPost: postDateSearchConditions,
						take: 10,
						orderByPre: [{ descending: true, path: 'Created' }],
						orderByPost: [{ descending: false, path: 'Created' }],
					},
					fetchPolicy: 'network-only',
				})
				.then(action((data) => {
					this.loadingState = 'Done';
					this.processQuickJumpEventQueryResults(data.data);
				}))
				.catch(action(() => this.loadingState = 'Error' ));
		}
		// % protected region % [Override sendQuickJumpQuery here] end
	};

	/*
	* Updates component state with new events and sets up a scroll correction event
	* */
	@action
	private processQuickJumpEventQueryResults = (preAndPostEvents : QuickJumpConditionalResult) => {
		// % protected region % [Override processQuickJumpEventQueryResults here] off begin
		const preEvents = preAndPostEvents.eventsPre;
		const postEvents = preAndPostEvents.eventsPost;
		this.loadingState = 'Done';
		this.timelineEvents = _.uniqBy(preEvents.concat(postEvents), 'id');
		if (postEvents.length > 0){

			// we want to quick jump to the first entry for the date, not the last
			this.scrollCorrectionSettings = {
				doScrollCorrection: true,
				scrollHeightOffset: 0,
				targetEventIndex: postEvents.length,
				scrollCorrectionType: 'Quick-Jump',
			}
		}
		// % protected region % [Override processQuickJumpEventQueryResults here] end
	};

	/**
	 * Fetches timeline events from the server
	 * @param timelineFilter a filter to apply to the fetch events query
	 */
	private fetchEvents = (timelineFilter: ITimelineFilter): Promise<Array<TimelineEvent>> | undefined => {
		// % protected region % [Override fetchEvents here] off begin
		const eventEntityModel = getTimelineEventEntity(timelineFilter.selectedTimelineEntity);
		const searchConditions = getTimelineQueryWhereConditions(timelineFilter);
		if (eventEntityModel) {
			const eventEntityName = getModelName(eventEntityModel);
			return store.apolloClient
				.query({
					query: getFetchAllConditional(eventEntityModel),
					variables: { args: searchConditions, take: 10, orderBy: this.orderBy },
					fetchPolicy: 'network-only',
				})
				.then(action((data) => {
					this.loadingState = 'Done';
					return data.data[`${lowerCaseFirst(eventEntityName)}s`];
				}))
				.catch(action(() => this.loadingState = 'Error' ));
		}
		return undefined;
		// % protected region % [Override fetchEvents here] end
	};

	/*
	* Called when a user has scrolled to the top or bottom of the list view
	* */
	private onScrollToExtreme = (waypoint: Waypoint.CallbackArgs, date: Date, extremity: 'Bottom' | 'Top') => {
		// % protected region % [Override onScrollToExtreme here] off begin
		if ((waypoint.currentPosition != Waypoint.invisible)){
			const timelineFilter = cloneDeep(this.props.timelineFilter);
			if (extremity == 'Bottom'){
				timelineFilter.endDate = date;
				this.fetchEvents(timelineFilter)?.then(events => this.processInfiniteScrollDownwardsResults(events));
			} else if (waypoint.previousPosition === Waypoint.above) {
				timelineFilter.startDate = date;
				this.orderBy =  [{ descending: false, path: 'Created' }];
				this.fetchEvents(timelineFilter)?.then(events => this.processInfiniteScrollUpwardsResults(events));
			}
		}
		// % protected region % [Override onScrollToExtreme here] end
	};

	/*
	* Adds more events to the current list of timeline events so user can scroll down further
	* */
	@action
	private processInfiniteScrollDownwardsResults = (events: TimelineEvent[]) => {
		// % protected region % [Override processInfiniteScrollDownwardsResults here] off begin
		this.timelineEvents = _.uniqBy(this.timelineEvents.concat(events), 'id');
		// % protected region % [Override processInfiniteScrollDownwardsResults here] end
	};

	/*
	* Adds more events to the top of the list and set up a scroll correction event
	* */
	@action
	private processInfiniteScrollUpwardsResults = (events: TimelineEvent[]) => {
		// % protected region % [Override processInfiniteScrollUpwardsResults here] off begin
		if (events.length > 0){
			const prevTimelineEvents = cloneDeep(this.timelineEvents);
			this.orderBy =  [{ descending: true, path: 'Created' }];
			this.timelineEvents = _.uniqBy(this.timelineEvents.concat(events), 'id');
			this.scrollCorrectionSettings = {
				doScrollCorrection: true,
				scrollHeightOffset: (this.scrollSectionRef.current?.scrollTop ?? 0),
				targetEventIndex: this.timelineEvents.length - prevTimelineEvents.length,
				scrollCorrectionType: "Load-More"
			};
		}
		// % protected region % [Override processInfiniteScrollUpwardsResults here] end
	};
	
	private onClickViewItem = (entity: string, id: string) => {
		// % protected region % [Override onClickViewItem here] off begin
		if (this.props.onClickViewItem) {
			this.props.onClickViewItem(entity, id)
		}
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
		this.eventComponentsRefs = this.timelineEvents.map(event => createRef<HTMLLIElement>());
		this.listHeadingRefs = Object.keys(GroupEventsByDate(this.timelineEvents)).map(key => createRef<HTMLHeadingElement>());
		return (
			<ListViewItems 
				timelineEvents={this.timelineEvents} 
				onScrollToExtreme={this.onScrollToExtreme} 
				onClickViewItem={(id: string) => this.onClickViewItem(this.props.timelineFilter.selectedTimelineEntity.name, id)}
				scrollSectionRef={this.scrollSectionRef} 
				eventComponentsRefs={this.eventComponentsRefs} 
				listHeadingRefs={this.listHeadingRefs} />
		);
		// % protected region % [Override render here] end
	}
}