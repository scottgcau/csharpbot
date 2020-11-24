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
import { VisitorsDivisionEntity } from 'Models/Security/Acl/VisitorsDivisionEntity';
import { SystemuserDivisionEntity } from 'Models/Security/Acl/SystemuserDivisionEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IDivisionEntityAttributes extends IModelAttributes {
	fullname: string;
	shortname: string;

	teamss: Array<Models.TeamEntity | Models.ITeamEntityAttributes>;
	seasonId?: string;
	season?: Models.SeasonEntity | Models.ISeasonEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('DivisionEntity', 'Division')
// % protected region % [Customise your entity metadata here] end
export default class DivisionEntity extends Model implements IDivisionEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsDivisionEntity(),
		new SystemuserDivisionEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'FullName'] off begin
	/**
	 * Full name
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'FullName',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public fullname: string;
	// % protected region % [Modify props to the crud options here for attribute 'FullName'] end

	// % protected region % [Modify props to the crud options here for attribute 'ShortName'] off begin
	/**
	 * Short name / abbreviation
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'ShortName',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public shortname: string;
	// % protected region % [Modify props to the crud options here for attribute 'ShortName'] end

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Teams'] off begin
		name: "Teamss",
		displayType: 'reference-multicombobox',
		order: 30,
		referenceTypeFunc: () => Models.TeamEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'teamss',
			oppositeEntity: () => Models.TeamEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Teams'] end
	})
	public teamss: Models.TeamEntity[] = [];

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Season'] off begin
		name: 'Season',
		displayType: 'reference-combobox',
		order: 40,
		referenceTypeFunc: () => Models.SeasonEntity,
		// % protected region % [Modify props to the crud options here for reference 'Season'] end
	})
	public seasonId?: string;
	@observable
	@attribute({isReference: true})
	public season: Models.SeasonEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IDivisionEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IDivisionEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.fullname) {
				this.fullname = attributes.fullname;
			}
			if (attributes.shortname) {
				this.shortname = attributes.shortname;
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
			if (attributes.season) {
				if (attributes.season instanceof Models.SeasonEntity) {
					this.season = attributes.season;
					this.seasonId = attributes.season.id;
				} else {
					this.season = new Models.SeasonEntity(attributes.season);
					this.seasonId = this.season.id;
				}
			} else if (attributes.seasonId !== undefined) {
				this.seasonId = attributes.seasonId;
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
		teamss {
			${Models.TeamEntity.getAttributes().join('\n')}
		}
		season {
			${Models.SeasonEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			teamss: {},
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
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}