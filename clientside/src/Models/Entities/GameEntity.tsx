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
import { VisitorsGameEntity } from 'Models/Security/Acl/VisitorsGameEntity';
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

export interface IGameEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	datestart: Date;
	hometeamid: number;
	awayteamid: number;

	venueId?: string;
	venue?: Models.VenueEntity | Models.IVenueEntityAttributes;
	scheduleId: string;
	schedule: Models.ScheduleEntity | Models.IScheduleEntityAttributes;
	refereess: Array<Models.PersonEntity | Models.IPersonEntityAttributes>;
	formPages: Array<Models.GameEntityFormTileEntity | Models.IGameEntityFormTileEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('GameEntity', 'Game')
// % protected region % [Customise your entity metadata here] end
export default class GameEntity extends Model implements IGameEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsGameEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'DateStart'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'DateStart',
		displayType: 'datetimepicker',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public datestart: Date;
	// % protected region % [Modify props to the crud options here for attribute 'DateStart'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeTeamId'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomeTeamId',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public hometeamid: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomeTeamId'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayTeamId'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayTeamId',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awayteamid: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayTeamId'] end

	@observable
	@attribute({isReference: true})
	public formVersions: FormVersion[] = [];

	@observable
	@attribute()
	public publishedVersionId?: string;

	@observable
	@attribute({isReference: true})
	public publishedVersion?: FormVersion;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Venue'] off begin
		name: 'Venue',
		displayType: 'reference-combobox',
		order: 50,
		referenceTypeFunc: () => Models.VenueEntity,
		// % protected region % [Modify props to the crud options here for reference 'Venue'] end
	})
	public venueId?: string;
	@observable
	@attribute({isReference: true})
	public venue: Models.VenueEntity;

	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Schedule'] off begin
		name: 'Schedule',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.ScheduleEntity,
		// % protected region % [Modify props to the crud options here for reference 'Schedule'] end
	})
	public scheduleId: string;
	@observable
	@attribute({isReference: true})
	public schedule: Models.ScheduleEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Referees'] off begin
		name: "Refereess",
		displayType: 'reference-multicombobox',
		order: 70,
		referenceTypeFunc: () => Models.PersonEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'refereess',
			oppositeEntity: () => Models.PersonEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Referees'] end
	})
	public refereess: Models.PersonEntity[] = [];

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] off begin
		name: "Form Pages",
		displayType: 'hidden',
		order: 80,
		referenceTypeFunc: () => Models.GameEntityFormTileEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'formPages',
			oppositeEntity: () => Models.GameEntityFormTileEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] end
	})
	public formPages: Models.GameEntityFormTileEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IGameEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IGameEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.datestart) {
				this.datestart = moment(attributes.datestart).toDate();
			}
			if (attributes.hometeamid) {
				this.hometeamid = attributes.hometeamid;
			}
			if (attributes.awayteamid) {
				this.awayteamid = attributes.awayteamid;
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
			if (attributes.venue) {
				if (attributes.venue instanceof Models.VenueEntity) {
					this.venue = attributes.venue;
					this.venueId = attributes.venue.id;
				} else {
					this.venue = new Models.VenueEntity(attributes.venue);
					this.venueId = this.venue.id;
				}
			} else if (attributes.venueId !== undefined) {
				this.venueId = attributes.venueId;
			}
			if (attributes.schedule) {
				if (attributes.schedule instanceof Models.ScheduleEntity) {
					this.schedule = attributes.schedule;
					this.scheduleId = attributes.schedule.id;
				} else {
					this.schedule = new Models.ScheduleEntity(attributes.schedule);
					this.scheduleId = this.schedule.id;
				}
			} else if (attributes.scheduleId !== undefined) {
				this.scheduleId = attributes.scheduleId;
			}
			if (attributes.refereess) {
				for (const model of attributes.refereess) {
					if (model instanceof Models.PersonEntity) {
						this.refereess.push(model);
					} else {
						this.refereess.push(new Models.PersonEntity(model));
					}
				}
			}
			if (attributes.formPages) {
				for (const model of attributes.formPages) {
					if (model instanceof Models.GameEntityFormTileEntity) {
						this.formPages.push(model);
					} else {
						this.formPages.push(new Models.GameEntityFormTileEntity(model));
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
		venue {
			${Models.VenueEntity.getAttributes().join('\n')}
		}
		schedule {
			${Models.ScheduleEntity.getAttributes().join('\n')}
		}
		refereess {
			${Models.PersonEntity.getAttributes().join('\n')}
		}
		formPages {
			${Models.GameEntityFormTileEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			refereess: {},
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
							'refereess',
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
		return Models.GameSubmissionEntity;
		// % protected region % [Modify the getSubmissionEntity here] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}