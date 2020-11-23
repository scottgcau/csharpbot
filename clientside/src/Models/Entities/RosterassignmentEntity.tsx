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
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { FormEntityData, FormEntityDataAttributes, getAllVersionsFn, getPublishedVersionFn } from 'Forms/FormEntityData';
import { FormVersion } from 'Forms/FormVersion';
import { fetchFormVersions, fetchPublishedVersion } from 'Forms/Forms';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IRosterassignmentEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	datefrom: Date;
	dateto: Date;
	roletype: Enums.roletype;

	rosterId?: string;
	roster?: Models.RosterEntity | Models.IRosterEntityAttributes;
	personId: string;
	person: Models.PersonEntity | Models.IPersonEntityAttributes;
	formPages: Array<Models.RosterassignmentEntityFormTileEntity | Models.IRosterassignmentEntityFormTileEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('RosterassignmentEntity', 'RosterAssignment')
// % protected region % [Customise your entity metadata here] end
export default class RosterassignmentEntity extends Model implements IRosterassignmentEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsRosterassignmentEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;
	// % protected region % [Modify props to the crud options here for attribute 'Name'] end

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
		order: 20,
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
		order: 30,
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
		order: 40,
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

	@observable
	@attribute({isReference: true})
	public formVersions: FormVersion[] = [];

	@observable
	@attribute()
	public publishedVersionId?: string;

	@observable
	@attribute({isReference: true})
	public publishedVersion?: FormVersion;

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

	/**
	 * Roster assignments
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Person'] off begin
		name: 'Person',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.PersonEntity,
		// % protected region % [Modify props to the crud options here for reference 'Person'] end
	})
	public personId: string;
	@observable
	@attribute({isReference: true})
	public person: Models.PersonEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] off begin
		name: "Form Pages",
		displayType: 'hidden',
		order: 70,
		referenceTypeFunc: () => Models.RosterassignmentEntityFormTileEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'formPages',
			oppositeEntity: () => Models.RosterassignmentEntityFormTileEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] end
	})
	public formPages: Models.RosterassignmentEntityFormTileEntity[] = [];

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
			if (attributes.publishedVersion) {
				if (typeof attributes.publishedVersion.formData === 'string') {
					attributes.publishedVersion.formData = JSON.parse(attributes.publishedVersion.formData);
				}
				this.publishedVersion = attributes.publishedVersion;
				this.publishedVersionId = attributes.publishedVersion.id;
			} else if (attributes.publishedVersionId !== undefined) {
				this.publishedVersionId = attributes.publishedVersionId;
			}
			if (attributes.formVersions) {
				this.formVersions.push(...attributes.formVersions);
			}
			if (attributes.name) {
				this.name = attributes.name;
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
			if (attributes.formPages) {
				for (const model of attributes.formPages) {
					if (model instanceof Models.RosterassignmentEntityFormTileEntity) {
						this.formPages.push(model);
					} else {
						this.formPages.push(new Models.RosterassignmentEntityFormTileEntity(model));
					}
				}
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
		publishedVersion {
			id
			created
			modified
			formData
		}
		roster {
			${Models.RosterEntity.getAttributes().join('\n')}
		}
		person {
			${Models.PersonEntity.getAttributes().join('\n')}
		}
		formPages {
			${Models.RosterassignmentEntityFormTileEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			formPages: {},
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
		return this.name;
		// % protected region % [Customise the display name for this entity] end
	}

	/**
	 * Gets all the versions for this form.
	 */
	public getAllVersions: getAllVersionsFn = (includeSubmissions?, conditions?) => {
		// % protected region % [Modify the getAllVersionsFn here] off begin
		return fetchFormVersions(this, includeSubmissions, conditions)
			.then(d => {
				runInAction(() => this.formVersions = d);
				return d.map(x => x.formData)
			});
		// % protected region % [Modify the getAllVersionsFn here] end
	};

	/**
	 * Gets the published version for this form.
	 */
	public getPublishedVersion: getPublishedVersionFn = includeSubmissions => {
		// % protected region % [Modify the getPublishedVersionFn here] off begin
		return fetchPublishedVersion(this, includeSubmissions)
			.then(d => {
				runInAction(() => this.publishedVersion = d);
				return d ? d.formData : undefined;
			});
		// % protected region % [Modify the getPublishedVersionFn here] end
	};

	/**
	 * Gets the submission entity type for this form.
	 */
	public getSubmissionEntity = () => {
		// % protected region % [Modify the getSubmissionEntity here] off begin
		return Models.RosterassignmentSubmissionEntity;
		// % protected region % [Modify the getSubmissionEntity here] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}