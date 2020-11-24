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
import { VisitorsSeasonEntity } from 'Models/Security/Acl/VisitorsSeasonEntity';
import { SystemuserSeasonEntity } from 'Models/Security/Acl/SystemuserSeasonEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISeasonEntityAttributes extends IModelAttributes {
	startdate: Date;
	enddate: Date;
	fullname: string;
	shortname: string;

	divisionss: Array<Models.DivisionEntity | Models.IDivisionEntityAttributes>;
	leagueId?: string;
	league?: Models.LeagueEntity | Models.ILeagueEntityAttributes;
	rosterss: Array<Models.RosterEntity | Models.IRosterEntityAttributes>;
	scheduless: Array<Models.ScheduleEntity | Models.IScheduleEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('SeasonEntity', 'Season')
// % protected region % [Customise your entity metadata here] end
export default class SeasonEntity extends Model implements ISeasonEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsSeasonEntity(),
		new SystemuserSeasonEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'StartDate'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'StartDate',
		displayType: 'datepicker',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public startdate: Date;
	// % protected region % [Modify props to the crud options here for attribute 'StartDate'] end

	// % protected region % [Modify props to the crud options here for attribute 'EndDate'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'EndDate',
		displayType: 'datepicker',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public enddate: Date;
	// % protected region % [Modify props to the crud options here for attribute 'EndDate'] end

	// % protected region % [Modify props to the crud options here for attribute 'FullName'] off begin
	/**
	 * Name for the season
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'FullName',
		displayType: 'textfield',
		order: 30,
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
		order: 40,
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
		// % protected region % [Modify props to the crud options here for reference 'Divisions'] off begin
		name: "Divisionss",
		displayType: 'reference-multicombobox',
		order: 50,
		referenceTypeFunc: () => Models.DivisionEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'divisionss',
			oppositeEntity: () => Models.DivisionEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Divisions'] end
	})
	public divisionss: Models.DivisionEntity[] = [];

	/**
	 * Collection of seasons for a league
	 */
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'League'] off begin
		name: 'League',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.LeagueEntity,
		// % protected region % [Modify props to the crud options here for reference 'League'] end
	})
	public leagueId?: string;
	@observable
	@attribute({isReference: true})
	public league: Models.LeagueEntity;

	/**
	 * Team rosters for season
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Rosters'] off begin
		name: "Rosterss",
		displayType: 'reference-multicombobox',
		order: 70,
		referenceTypeFunc: () => Models.RosterEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'rosterss',
			oppositeEntity: () => Models.RosterEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Rosters'] end
	})
	public rosterss: Models.RosterEntity[] = [];

	/**
	 * Season schedules
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Schedules'] off begin
		name: "Scheduless",
		displayType: 'reference-multicombobox',
		order: 80,
		referenceTypeFunc: () => Models.ScheduleEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'scheduless',
			oppositeEntity: () => Models.ScheduleEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Schedules'] end
	})
	public scheduless: Models.ScheduleEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISeasonEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ISeasonEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.startdate) {
				this.startdate = moment(attributes.startdate).toDate();
			}
			if (attributes.enddate) {
				this.enddate = moment(attributes.enddate).toDate();
			}
			if (attributes.fullname) {
				this.fullname = attributes.fullname;
			}
			if (attributes.shortname) {
				this.shortname = attributes.shortname;
			}
			if (attributes.divisionss) {
				for (const model of attributes.divisionss) {
					if (model instanceof Models.DivisionEntity) {
						this.divisionss.push(model);
					} else {
						this.divisionss.push(new Models.DivisionEntity(model));
					}
				}
			}
			if (attributes.league) {
				if (attributes.league instanceof Models.LeagueEntity) {
					this.league = attributes.league;
					this.leagueId = attributes.league.id;
				} else {
					this.league = new Models.LeagueEntity(attributes.league);
					this.leagueId = this.league.id;
				}
			} else if (attributes.leagueId !== undefined) {
				this.leagueId = attributes.leagueId;
			}
			if (attributes.rosterss) {
				for (const model of attributes.rosterss) {
					if (model instanceof Models.RosterEntity) {
						this.rosterss.push(model);
					} else {
						this.rosterss.push(new Models.RosterEntity(model));
					}
				}
			}
			if (attributes.scheduless) {
				for (const model of attributes.scheduless) {
					if (model instanceof Models.ScheduleEntity) {
						this.scheduless.push(model);
					} else {
						this.scheduless.push(new Models.ScheduleEntity(model));
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
		divisionss {
			${Models.DivisionEntity.getAttributes().join('\n')}
		}
		league {
			${Models.LeagueEntity.getAttributes().join('\n')}
		}
		rosterss {
			${Models.RosterEntity.getAttributes().join('\n')}
		}
		scheduless {
			${Models.ScheduleEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			divisionss: {},
			rosterss: {},
			scheduless: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'divisionss',
							'rosterss',
							'scheduless',
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