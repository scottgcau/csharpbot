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
import _ from 'lodash';
import moment from 'moment';
import { action, observable, runInAction } from 'mobx';
import { IAttributeGroup, Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from 'Util/EntityUtils';
import { VisitorsRosterEntity } from 'Models/Security/Acl/VisitorsRosterEntity';
import { SystemuserRosterEntity } from 'Models/Security/Acl/SystemuserRosterEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IRosterTimelineEventsEntityAttributes extends IModelAttributes {
	action: string;
	actionTitle: string;
	description: string;
	groupId: string;

	entityId?: string;
	entity?: Models.RosterEntity | Models.IRosterEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('RosterTimelineEventsEntity', 'Roster Timeline Events')
// % protected region % [Customise your entity metadata here] end
export default class RosterTimelineEventsEntity extends Model implements IRosterTimelineEventsEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsRosterEntity(),
		new SystemuserRosterEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [
		// % protected region % [Add any custom create exclusions here] off begin
		// % protected region % [Add any custom create exclusions here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = [
		// % protected region % [Add any custom update exclusions here] off begin
		// % protected region % [Add any custom update exclusions here] end
	];

	// % protected region % [Modify props to the crud options here for attribute 'Action'] off begin
	/**
	 * The action taken
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Action',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public action: string;
	// % protected region % [Modify props to the crud options here for attribute 'Action'] end

	// % protected region % [Modify props to the crud options here for attribute 'Action Title'] off begin
	/**
	 * The title of the action taken
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Action Title',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public actionTitle: string;
	// % protected region % [Modify props to the crud options here for attribute 'Action Title'] end

	// % protected region % [Modify props to the crud options here for attribute 'Description'] off begin
	/**
	 * Decription of the event
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'Description',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public description: string;
	// % protected region % [Modify props to the crud options here for attribute 'Description'] end

	// % protected region % [Modify props to the crud options here for attribute 'Group Id'] off begin
	/**
	 * Id of the group the events belong to
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Group Id',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseUuid,
	})
	public groupId: string;
	// % protected region % [Modify props to the crud options here for attribute 'Group Id'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Entity'] off begin
		name: 'Entity',
		displayType: 'reference-combobox',
		order: 50,
		referenceTypeFunc: () => Models.RosterEntity,
		// % protected region % [Modify props to the crud options here for reference 'Entity'] end
	})
	public entityId?: string;
	@observable
	@attribute({isReference: true})
	public entity: Models.RosterEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IRosterTimelineEventsEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IRosterTimelineEventsEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.action) {
				this.action = attributes.action;
			}
			if (attributes.actionTitle) {
				this.actionTitle = attributes.actionTitle;
			}
			if (attributes.description) {
				this.description = attributes.description;
			}
			if (attributes.groupId) {
				this.groupId = attributes.groupId;
			}
			if (attributes.entity) {
				if (attributes.entity instanceof Models.RosterEntity) {
					this.entity = attributes.entity;
					this.entityId = attributes.entity.id;
				} else {
					this.entity = new Models.RosterEntity(attributes.entity);
					this.entityId = this.entity.id;
				}
			} else if (attributes.entityId !== undefined) {
				this.entityId = attributes.entityId;
			}
			// % protected region % [Override assign attributes here] end

			// % protected region % [Add any extra assign attributes logic here] off begin
			// % protected region % [Add any extra assign attributes logic here] end
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */
	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
		entity {
			${Models.RosterEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
						]
					},
				],
			}
		);
	}
	// % protected region % [Customize Save From Crud here] end

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}