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
import { VisitorsLeagueEntity } from 'Models/Security/Acl/VisitorsLeagueEntity';
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

export interface ILeagueEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	fullname: string;
	shortname: string;

	seasonss: Array<Models.SeasonEntity | Models.ISeasonEntityAttributes>;
	teamss: Array<Models.TeamEntity | Models.ITeamEntityAttributes>;
	sportId: string;
	sport: Models.SportEntity | Models.ISportEntityAttributes;
	formPages: Array<Models.LeagueEntityFormTileEntity | Models.ILeagueEntityFormTileEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('LeagueEntity', 'League')
// % protected region % [Customise your entity metadata here] end
export default class LeagueEntity extends Model implements ILeagueEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsLeagueEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'FullName'] off begin
	/**
	 * League name
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'FullName',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public fullname: string;
	// % protected region % [Modify props to the crud options here for attribute 'FullName'] end

	// % protected region % [Modify props to the crud options here for attribute 'ShortName'] off begin
	/**
	 * Short name / abbreviation for the league
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'ShortName',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public shortname: string;
	// % protected region % [Modify props to the crud options here for attribute 'ShortName'] end

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
	 * Collection of seasons for a league
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Seasons'] off begin
		name: "Seasonss",
		displayType: 'reference-multicombobox',
		order: 40,
		referenceTypeFunc: () => Models.SeasonEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'seasonss',
			oppositeEntity: () => Models.SeasonEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Seasons'] end
	})
	public seasonss: Models.SeasonEntity[] = [];

	/**
	 * Collection of teams for a league
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Teams'] off begin
		name: "Teamss",
		displayType: 'reference-multicombobox',
		order: 50,
		referenceTypeFunc: () => Models.TeamEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'teamss',
			oppositeEntity: () => Models.TeamEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Teams'] end
	})
	public teamss: Models.TeamEntity[] = [];

	/**
	 * Related leagues
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Sport'] off begin
		name: 'Sport',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.SportEntity,
		// % protected region % [Modify props to the crud options here for reference 'Sport'] end
	})
	public sportId: string;
	@observable
	@attribute({isReference: true})
	public sport: Models.SportEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] off begin
		name: "Form Pages",
		displayType: 'hidden',
		order: 70,
		referenceTypeFunc: () => Models.LeagueEntityFormTileEntity,
		disableDefaultOptionRemoval: true,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'formPages',
			oppositeEntity: () => Models.LeagueEntityFormTileEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Form Page'] end
	})
	public formPages: Models.LeagueEntityFormTileEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ILeagueEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ILeagueEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.fullname) {
				this.fullname = attributes.fullname;
			}
			if (attributes.shortname) {
				this.shortname = attributes.shortname;
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
			if (attributes.seasonss) {
				for (const model of attributes.seasonss) {
					if (model instanceof Models.SeasonEntity) {
						this.seasonss.push(model);
					} else {
						this.seasonss.push(new Models.SeasonEntity(model));
					}
				}
			}
			if (attributes.teamss) {
				for (const model of attributes.teamss) {
					if (model instanceof Models.TeamEntity) {
						this.teamss.push(model);
					} else {
						this.teamss.push(new Models.TeamEntity(model));
					}
				}
			}
			if (attributes.sport) {
				if (attributes.sport instanceof Models.SportEntity) {
					this.sport = attributes.sport;
					this.sportId = attributes.sport.id;
				} else {
					this.sport = new Models.SportEntity(attributes.sport);
					this.sportId = this.sport.id;
				}
			} else if (attributes.sportId !== undefined) {
				this.sportId = attributes.sportId;
			}
			if (attributes.formPages) {
				for (const model of attributes.formPages) {
					if (model instanceof Models.LeagueEntityFormTileEntity) {
						this.formPages.push(model);
					} else {
						this.formPages.push(new Models.LeagueEntityFormTileEntity(model));
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
		seasonss {
			${Models.SeasonEntity.getAttributes().join('\n')}
		}
		teamss {
			${Models.TeamEntity.getAttributes().join('\n')}
		}
		sport {
			${Models.SportEntity.getAttributes().join('\n')}
		}
		formPages {
			${Models.LeagueEntityFormTileEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			seasonss: {},
			teamss: {},
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
							'teamss',
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
		return Models.LeagueSubmissionEntity;
		// % protected region % [Modify the getSubmissionEntity here] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}