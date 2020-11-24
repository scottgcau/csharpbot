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
import { VisitorsTeamEntity } from 'Models/Security/Acl/VisitorsTeamEntity';
import { SystemuserTeamEntity } from 'Models/Security/Acl/SystemuserTeamEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ITeamEntityAttributes extends IModelAttributes {
	represents: string;
	fullname: string;
	shortname: string;

	ladderwinlossess: Array<Models.LadderwinlossEntity | Models.ILadderwinlossEntityAttributes>;
	divisionId?: string;
	division?: Models.DivisionEntity | Models.IDivisionEntityAttributes;
	laddereliminationss: Array<Models.LaddereliminationEntity | Models.ILaddereliminationEntityAttributes>;
	rosterss: Array<Models.RosterEntity | Models.IRosterEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('TeamEntity', 'Team')
// % protected region % [Customise your entity metadata here] end
export default class TeamEntity extends Model implements ITeamEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsTeamEntity(),
		new SystemuserTeamEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Represents'] off begin
	/**
	 * City or area represented
	 */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Represents',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public represents: string;
	// % protected region % [Modify props to the crud options here for attribute 'Represents'] end

	// % protected region % [Modify props to the crud options here for attribute 'FullName'] off begin
	/**
	 * Name of the team (sans city / area)
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
	 * Short name / abbreviation for the team
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
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'LadderWinLosses'] off begin
		name: "LadderWinLossess",
		displayType: 'reference-multicombobox',
		order: 40,
		referenceTypeFunc: () => Models.LadderwinlossEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'ladderwinlossess',
			oppositeEntity: () => Models.LadderwinlossEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'LadderWinLosses'] end
	})
	public ladderwinlossess: Models.LadderwinlossEntity[] = [];

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Division'] off begin
		name: 'Division',
		displayType: 'reference-combobox',
		order: 50,
		referenceTypeFunc: () => Models.DivisionEntity,
		// % protected region % [Modify props to the crud options here for reference 'Division'] end
	})
	public divisionId?: string;
	@observable
	@attribute({isReference: true})
	public division: Models.DivisionEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'LadderEliminations'] off begin
		name: "LadderEliminationss",
		displayType: 'reference-multicombobox',
		order: 60,
		referenceTypeFunc: () => Models.LaddereliminationEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'laddereliminationss',
			oppositeEntity: () => Models.LaddereliminationEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'LadderEliminations'] end
	})
	public laddereliminationss: Models.LaddereliminationEntity[] = [];

	/**
	 * Team rosters
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

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ITeamEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ITeamEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.represents) {
				this.represents = attributes.represents;
			}
			if (attributes.fullname) {
				this.fullname = attributes.fullname;
			}
			if (attributes.shortname) {
				this.shortname = attributes.shortname;
			}
			if (attributes.ladderwinlossess) {
				for (const model of attributes.ladderwinlossess) {
					if (model instanceof Models.LadderwinlossEntity) {
						this.ladderwinlossess.push(model);
					} else {
						this.ladderwinlossess.push(new Models.LadderwinlossEntity(model));
					}
				}
			}
			if (attributes.division) {
				if (attributes.division instanceof Models.DivisionEntity) {
					this.division = attributes.division;
					this.divisionId = attributes.division.id;
				} else {
					this.division = new Models.DivisionEntity(attributes.division);
					this.divisionId = this.division.id;
				}
			} else if (attributes.divisionId !== undefined) {
				this.divisionId = attributes.divisionId;
			}
			if (attributes.laddereliminationss) {
				for (const model of attributes.laddereliminationss) {
					if (model instanceof Models.LaddereliminationEntity) {
						this.laddereliminationss.push(model);
					} else {
						this.laddereliminationss.push(new Models.LaddereliminationEntity(model));
					}
				}
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
		ladderwinlossess {
			${Models.LadderwinlossEntity.getAttributes().join('\n')}
		}
		division {
			${Models.DivisionEntity.getAttributes().join('\n')}
		}
		laddereliminationss {
			${Models.LaddereliminationEntity.getAttributes().join('\n')}
		}
		rosterss {
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
			ladderwinlossess: {},
			laddereliminationss: {},
			rosterss: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'ladderwinlossess',
							'laddereliminationss',
							'rosterss',
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