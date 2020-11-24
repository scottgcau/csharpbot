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
import { VisitorsPersonEntity } from 'Models/Security/Acl/VisitorsPersonEntity';
import { SystemuserPersonEntity } from 'Models/Security/Acl/SystemuserPersonEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IPersonEntityAttributes extends IModelAttributes {
	firstname: string;
	lastname: string;
	dateofbirth: Date;
	height: number;
	weight: number;

	rosterassignmentss: Array<Models.RosterassignmentEntity | Models.IRosterassignmentEntityAttributes>;
	systemuserId?: string;
	systemuser?: Models.SystemuserEntity | Models.ISystemuserEntityAttributes;
	gamerefereeId?: string;
	gamereferee?: Models.GamerefereeEntity | Models.IGamerefereeEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('PersonEntity', 'Person')
// % protected region % [Customise your entity metadata here] end
export default class PersonEntity extends Model implements IPersonEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsPersonEntity(),
		new SystemuserPersonEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'FirstName'] off begin
	/**
	 * First name
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'FirstName',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public firstname: string;
	// % protected region % [Modify props to the crud options here for attribute 'FirstName'] end

	// % protected region % [Modify props to the crud options here for attribute 'LastName'] off begin
	/**
	 * Last name
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'LastName',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public lastname: string;
	// % protected region % [Modify props to the crud options here for attribute 'LastName'] end

	// % protected region % [Modify props to the crud options here for attribute 'DateOfBirth'] off begin
	/**
	 * Date of birth
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'DateOfBirth',
		displayType: 'datepicker',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public dateofbirth: Date;
	// % protected region % [Modify props to the crud options here for attribute 'DateOfBirth'] end

	// % protected region % [Modify props to the crud options here for attribute 'Height'] off begin
	/**
	 * Height (cm)
	 */
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Height',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public height: number;
	// % protected region % [Modify props to the crud options here for attribute 'Height'] end

	// % protected region % [Modify props to the crud options here for attribute 'Weight'] off begin
	/**
	 * Weight (kg)
	 */
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Weight',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public weight: number;
	// % protected region % [Modify props to the crud options here for attribute 'Weight'] end

	/**
	 * Roster assignments
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'RosterAssignments'] off begin
		name: "RosterAssignmentss",
		displayType: 'reference-multicombobox',
		order: 60,
		referenceTypeFunc: () => Models.RosterassignmentEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'rosterassignmentss',
			oppositeEntity: () => Models.RosterassignmentEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'RosterAssignments'] end
	})
	public rosterassignmentss: Models.RosterassignmentEntity[] = [];

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'SystemUser'] off begin
		name: 'SystemUser',
		displayType: 'reference-combobox',
		order: 70,
		referenceTypeFunc: () => Models.SystemuserEntity,
		// % protected region % [Modify props to the crud options here for reference 'SystemUser'] end
	})
	public systemuserId?: string;
	@observable
	@attribute({isReference: true})
	public systemuser: Models.SystemuserEntity;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'GameReferee'] off begin
		name: 'GameReferee',
		displayType: 'reference-combobox',
		order: 80,
		referenceTypeFunc: () => Models.GamerefereeEntity,
		// % protected region % [Modify props to the crud options here for reference 'GameReferee'] end
	})
	public gamerefereeId?: string;
	@observable
	@attribute({isReference: true})
	public gamereferee: Models.GamerefereeEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IPersonEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IPersonEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.firstname) {
				this.firstname = attributes.firstname;
			}
			if (attributes.lastname) {
				this.lastname = attributes.lastname;
			}
			if (attributes.dateofbirth) {
				this.dateofbirth = moment(attributes.dateofbirth).toDate();
			}
			if (attributes.height) {
				this.height = attributes.height;
			}
			if (attributes.weight) {
				this.weight = attributes.weight;
			}
			if (attributes.rosterassignmentss) {
				for (const model of attributes.rosterassignmentss) {
					if (model instanceof Models.RosterassignmentEntity) {
						this.rosterassignmentss.push(model);
					} else {
						this.rosterassignmentss.push(new Models.RosterassignmentEntity(model));
					}
				}
			}
			if (attributes.systemuser) {
				if (attributes.systemuser instanceof Models.SystemuserEntity) {
					this.systemuser = attributes.systemuser;
					this.systemuserId = attributes.systemuser.id;
				} else {
					this.systemuser = new Models.SystemuserEntity(attributes.systemuser);
					this.systemuserId = this.systemuser.id;
				}
			} else if (attributes.systemuserId !== undefined) {
				this.systemuserId = attributes.systemuserId;
			}
			if (attributes.gamereferee) {
				if (attributes.gamereferee instanceof Models.GamerefereeEntity) {
					this.gamereferee = attributes.gamereferee;
					this.gamerefereeId = attributes.gamereferee.id;
				} else {
					this.gamereferee = new Models.GamerefereeEntity(attributes.gamereferee);
					this.gamerefereeId = this.gamereferee.id;
				}
			} else if (attributes.gamerefereeId !== undefined) {
				this.gamerefereeId = attributes.gamerefereeId;
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
		rosterassignmentss {
			${Models.RosterassignmentEntity.getAttributes().join('\n')}
		}
		systemuser {
			${Models.SystemuserEntity.getAttributes().join('\n')}
		}
		gamereferee {
			${Models.GamerefereeEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			rosterassignmentss: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'rosterassignmentss',
							'systemuser',
							'gamereferee',
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