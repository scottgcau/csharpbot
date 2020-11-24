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
import { VisitorsRosterassignmentEntity } from 'Models/Security/Acl/VisitorsRosterassignmentEntity';
import { SystemuserRosterassignmentEntity } from 'Models/Security/Acl/SystemuserRosterassignmentEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IRosterassignmentEntityAttributes extends IModelAttributes {
	datefrom: Date;
	dateto: Date;
	roletype: Enums.roletype;

	personId?: string;
	person?: Models.PersonEntity | Models.IPersonEntityAttributes;
	rosterId?: string;
	roster?: Models.RosterEntity | Models.IRosterEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('RosterassignmentEntity', 'RosterAssignment')
// % protected region % [Customise your entity metadata here] end
export default class RosterassignmentEntity extends Model implements IRosterassignmentEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsRosterassignmentEntity(),
		new SystemuserRosterassignmentEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'DateFrom'] off begin
	/**
	 * Date assigned to the roster
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'DateFrom',
		displayType: 'datepicker',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public datefrom: Date;
	// % protected region % [Modify props to the crud options here for attribute 'DateFrom'] end

	// % protected region % [Modify props to the crud options here for attribute 'DateTo'] off begin
	/**
	 * Date left the roster
	 */
	@observable
	@attribute()
	@CRUD({
		name: 'DateTo',
		displayType: 'datepicker',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public dateto: Date;
	// % protected region % [Modify props to the crud options here for attribute 'DateTo'] end

	// % protected region % [Modify props to the crud options here for attribute 'RoleType'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'RoleType',
		displayType: 'enum-combobox',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.roletypeOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.roletypeOptions),
		displayFunction: (attribute: Enums.roletype) => Enums.roletypeOptions[attribute],
	})
	public roletype: Enums.roletype;
	// % protected region % [Modify props to the crud options here for attribute 'RoleType'] end

	/**
	 * Roster assignments
	 */
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Person'] off begin
		name: 'Person',
		displayType: 'reference-combobox',
		order: 40,
		referenceTypeFunc: () => Models.PersonEntity,
		// % protected region % [Modify props to the crud options here for reference 'Person'] end
	})
	public personId?: string;
	@observable
	@attribute({isReference: true})
	public person: Models.PersonEntity;

	/**
	 * Roster assignments
	 */
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Roster'] off begin
		name: 'Roster',
		displayType: 'reference-combobox',
		order: 50,
		referenceTypeFunc: () => Models.RosterEntity,
		// % protected region % [Modify props to the crud options here for reference 'Roster'] end
	})
	public rosterId?: string;
	@observable
	@attribute({isReference: true})
	public roster: Models.RosterEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IRosterassignmentEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IRosterassignmentEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.datefrom) {
				this.datefrom = moment(attributes.datefrom).toDate();
			}
			if (attributes.dateto) {
				this.dateto = moment(attributes.dateto).toDate();
			}
			if (attributes.roletype) {
				this.roletype = attributes.roletype;
			}
			if (attributes.person) {
				if (attributes.person instanceof Models.PersonEntity) {
					this.person = attributes.person;
					this.personId = attributes.person.id;
				} else {
					this.person = new Models.PersonEntity(attributes.person);
					this.personId = this.person.id;
				}
			} else if (attributes.personId !== undefined) {
				this.personId = attributes.personId;
			}
			if (attributes.roster) {
				if (attributes.roster instanceof Models.RosterEntity) {
					this.roster = attributes.roster;
					this.rosterId = attributes.roster.id;
				} else {
					this.roster = new Models.RosterEntity(attributes.roster);
					this.rosterId = this.roster.id;
				}
			} else if (attributes.rosterId !== undefined) {
				this.rosterId = attributes.rosterId;
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
		person {
			${Models.PersonEntity.getAttributes().join('\n')}
		}
		roster {
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