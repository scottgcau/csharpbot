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
import moment from "moment";
import {getAttributeCRUDOptions, getAttributes, getModelName} from 'Util/EntityUtils';
import {IModelType, Model} from 'Models/Model';
import _ from "lodash";
import {modelName as modelNameSymbol} from "../Symbols";
import {lowerCaseFirst} from "./StringUtils";
import {gql} from "apollo-boost";
import * as Models from "../Models/Entities";
import {IWhereCondition} from 'Views/Components/ModelCollection/ModelQuery';
import axios from "axios";
import {ITimelineFilter} from 'Timelines/TimelineTile';
import { TimelineModel } from 'Timelines/TimelineModel';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

// % protected region % [Override DateRange here] off begin
export interface DateRange {
	startDate: Date,
	endDate: Date
}
// % protected region % [Override DateRange here] end

// % protected region % [Override QuickJumpTimeFrame here] off begin
export enum QuickJumpTimeFrame {
	'Weeks',
	'Months' 
}
// % protected region % [Override QuickJumpTimeFrame here] end

// % protected region % [Override TimelineEntities here] off begin
export const TimelineEntities = [
	Models.RosterEntity,
];
// % protected region % [Override TimelineEntities here] end

// % protected region % [Override GroupEventsByDate here] off begin
export const GroupEventsByDate = (events: TimelineEvent[]) => {
	return _(events)
		.groupBy(event => moment(event.created).format('YYYY-MM-DD'))
		.value();
};
// % protected region % [Override GroupEventsByDate here] end

// % protected region % [Override SortEvents here] off begin
export const SortEvents = (a: TimelineEvent, b:TimelineEvent) => {
	return CompareDateString(a.created, b.created)
};
// % protected region % [Override SortEvents here] end

// % protected region % [Override CompareDateString here] off begin
export const CompareDateString = (a: string | Date, b: string | Date) => {
	const dateA = new Date(a).getTime();
	const dateB = new Date(b).getTime();
	return dateB - dateA;
};
// % protected region % [Override CompareDateString here] end

// % protected region % [Override GetTimelineGroupDateDisplay here] off begin
export const GetTimelineGroupDateDisplay = (groupDate: Date) => {
	const date = moment();

	if (moment(date).isSame(groupDate, 'day')){
		return 'Today'
	} if (moment(date.subtract(1, 'day')).isSame(groupDate, 'day')) {
		return 'Yesterday'
	} return moment(groupDate).format('DD/MM/YYYY');
};
// % protected region % [Override GetTimelineGroupDateDisplay here] end

// % protected region % [Override getTimelineEventEntity here] off begin
export const getTimelineEventEntity = (model: IModelType<Model & TimelineModel>) => {
	return new model().getTimelineEventEntity()
};
// % protected region % [Override getTimelineEventEntity here] end

// % protected region % [Override getTimelineEntityFromName here] off begin
export const getTimelineEntityFromName = (modelName: string | undefined) => {
	if (!modelName) {
		return undefined;
	}
	switch (modelName.toLowerCase()) {
		case 'roster':
			return Models.RosterEntity;
		default:
			return undefined;
	}
};
// % protected region % [Override getTimelineEntityFromName here] end

// % protected region % [Override getTimelineActionOptions here] off begin
export const getTimelineActionOptions = async (timelineEntity: IModelType<Model & TimelineModel>) : Promise<string[]> => {
	const eventEntityName = getEventEntityNameFromTimelineEntity(timelineEntity);
	return axios
		.get<string[]>(`/api/entity/${eventEntityName}/action-types`)
		.then(data => {return data.data})
};
// % protected region % [Override getTimelineActionOptions here] end

// % protected region % [Override getEventEntityNameFromTimelineEntity here] off begin
export const getEventEntityNameFromTimelineEntity = (model: IModelType<Model & TimelineModel>) => {
	return getModelName(getTimelineEventEntity(model))
};
// % protected region % [Override getEventEntityNameFromTimelineEntity here] end

// % protected region % [Override getTimelineQueryWhereConditions here] off begin
export const getTimelineQueryWhereConditions = (timelineFilter: ITimelineFilter): Array<Array<IWhereCondition<any>>> => {
	const eventEntityModel = getTimelineEventEntity(timelineFilter.selectedTimelineEntity);
	if (eventEntityModel) {
		let searchConditions = new eventEntityModel().getSearchConditions(timelineFilter.searchTerm) ?? [];
		if (timelineFilter.startDate) {
			searchConditions.push([{ comparison: 'greaterThanOrEqual', path: 'created', value: [moment(timelineFilter.startDate).format('YYYY-MM-DD HH:mm:ss')]}])
		} if (timelineFilter.endDate) {
			searchConditions.push([{ comparison: "lessThanOrEqual", path: 'created', value: [moment(timelineFilter.endDate).format('YYYY-MM-DD HH:mm:ss')]}])
		} if (timelineFilter.selectedActionTypes.length > 0) {
			searchConditions.push(timelineFilter.selectedActionTypes.map(action => { return { comparison: "equal", path: 'Action', value: [action] } }));
		} if (timelineFilter.instanceIds.length > 0) {
			searchConditions.push(timelineFilter.instanceIds.map(id => { return { comparison: "equal", path: 'entityId', value: [id] } }));
		}
		return searchConditions;
	}
	return [];
};
// % protected region % [Override getTimelineQueryWhereConditions here] end

// % protected region % [Override getActionShapeClassName here] off begin
export const getActionShapeClassName = (action: string) => {
	switch (action) {
		case 'Created':
			return 'diamond';
		case 'Updated':
			return 'square';
		case 'Deleted':
			return 'circle';
		default:
			return 'default';
	}
};
// % protected region % [Override getActionShapeClassName here] end

// % protected region % [Override TimelineEvent here] off begin
export interface TimelineEvent {
	id: string,
	created: Date,
	modified: Date,
	action: string,
	actionTitle: string,
	description: string,
	entityId: string,
	groupId: string
}
// % protected region % [Override TimelineEvent here] end

// % protected region % [Override QuickJumpConditionalResult here] off begin
export interface QuickJumpConditionalResult {
	eventsPre: Array<TimelineEvent>,
	eventsPost: Array<TimelineEvent>
}
// % protected region % [Override QuickJumpConditionalResult here] end

// % protected region % [Override getQuickJumpConditional here] off begin
export function getQuickJumpConditional (modelType: {new() : Model}, expandString?: string) {
	const modelName: string = modelType[modelNameSymbol];
	const lowerModelName = lowerCaseFirst(modelName);

	return gql`
		query ${lowerModelName}($argsPre: [[WhereExpressionGraph]], $argsPost: [[WhereExpressionGraph]], $skip:Int, $take:Int, $orderByPre: [OrderByGraph], $orderByPost: [OrderByGraph], $ids: [ID] ) {
			eventsPre : ${lowerModelName}sConditional(conditions: $argsPre, skip:$skip, take:$take, orderBy: $orderByPre, ids: $ids) {
				${expandString ? expandString : ""}
				${getAttributes(modelType).join('\n')}
			}
			eventsPost : ${lowerModelName}sConditional(conditions: $argsPost, skip:$skip, take:$take, orderBy: $orderByPost, ids: $ids) {
				${expandString ? expandString : ""}
				${getAttributes(modelType).join('\n')}
			}
		}`;
}
// % protected region % [Override getQuickJumpConditional here] end