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
import { VisitorsLadderwinlossEntity } from 'Models/Security/Acl/VisitorsLadderwinlossEntity';
import { SystemuserLadderwinlossEntity } from 'Models/Security/Acl/SystemuserLadderwinlossEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ILadderwinlossEntityAttributes extends IModelAttributes {
	played: number;
	won: number;
	lost: number;
	pointsfor: number;
	pointsagainst: number;
	homewon: number;
	homelost: number;
	homefor: number;
	homeagainst: number;
	awaywon: number;
	awaylost: number;
	awayfor: number;
	awayagainst: number;

	teamId?: string;
	team?: Models.TeamEntity | Models.ITeamEntityAttributes;
	ladderId?: string;
	ladder?: Models.LadderEntity | Models.ILadderEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('LadderwinlossEntity', 'LadderWinLoss')
// % protected region % [Customise your entity metadata here] end
export default class LadderwinlossEntity extends Model implements ILadderwinlossEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsLadderwinlossEntity(),
		new SystemuserLadderwinlossEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Played'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Played',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public played: number;
	// % protected region % [Modify props to the crud options here for attribute 'Played'] end

	// % protected region % [Modify props to the crud options here for attribute 'Won'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Won',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public won: number;
	// % protected region % [Modify props to the crud options here for attribute 'Won'] end

	// % protected region % [Modify props to the crud options here for attribute 'Lost'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Lost',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public lost: number;
	// % protected region % [Modify props to the crud options here for attribute 'Lost'] end

	// % protected region % [Modify props to the crud options here for attribute 'PointsFor'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'PointsFor',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public pointsfor: number;
	// % protected region % [Modify props to the crud options here for attribute 'PointsFor'] end

	// % protected region % [Modify props to the crud options here for attribute 'PointsAgainst'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'PointsAgainst',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public pointsagainst: number;
	// % protected region % [Modify props to the crud options here for attribute 'PointsAgainst'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeWon'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomeWon',
		displayType: 'textfield',
		order: 60,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public homewon: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomeWon'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeLost'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomeLost',
		displayType: 'textfield',
		order: 70,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public homelost: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomeLost'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeFor'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomeFor',
		displayType: 'textfield',
		order: 80,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public homefor: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomeFor'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeAgainst'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomeAgainst',
		displayType: 'textfield',
		order: 90,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public homeagainst: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomeAgainst'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayWon'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayWon',
		displayType: 'textfield',
		order: 100,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awaywon: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayWon'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayLost'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayLost',
		displayType: 'textfield',
		order: 110,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awaylost: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayLost'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayFor'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayFor',
		displayType: 'textfield',
		order: 120,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awayfor: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayFor'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayAgainst'] off begin
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayAgainst',
		displayType: 'textfield',
		order: 130,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awayagainst: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayAgainst'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Team'] off begin
		name: 'Team',
		displayType: 'reference-combobox',
		order: 140,
		referenceTypeFunc: () => Models.TeamEntity,
		// % protected region % [Modify props to the crud options here for reference 'Team'] end
	})
	public teamId?: string;
	@observable
	@attribute({isReference: true})
	public team: Models.TeamEntity;

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Ladder'] off begin
		name: 'Ladder',
		displayType: 'reference-combobox',
		order: 150,
		referenceTypeFunc: () => Models.LadderEntity,
		// % protected region % [Modify props to the crud options here for reference 'Ladder'] end
	})
	public ladderId?: string;
	@observable
	@attribute({isReference: true})
	public ladder: Models.LadderEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ILadderwinlossEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ILadderwinlossEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.played) {
				this.played = attributes.played;
			}
			if (attributes.won) {
				this.won = attributes.won;
			}
			if (attributes.lost) {
				this.lost = attributes.lost;
			}
			if (attributes.pointsfor) {
				this.pointsfor = attributes.pointsfor;
			}
			if (attributes.pointsagainst) {
				this.pointsagainst = attributes.pointsagainst;
			}
			if (attributes.homewon) {
				this.homewon = attributes.homewon;
			}
			if (attributes.homelost) {
				this.homelost = attributes.homelost;
			}
			if (attributes.homefor) {
				this.homefor = attributes.homefor;
			}
			if (attributes.homeagainst) {
				this.homeagainst = attributes.homeagainst;
			}
			if (attributes.awaywon) {
				this.awaywon = attributes.awaywon;
			}
			if (attributes.awaylost) {
				this.awaylost = attributes.awaylost;
			}
			if (attributes.awayfor) {
				this.awayfor = attributes.awayfor;
			}
			if (attributes.awayagainst) {
				this.awayagainst = attributes.awayagainst;
			}
			if (attributes.team) {
				if (attributes.team instanceof Models.TeamEntity) {
					this.team = attributes.team;
					this.teamId = attributes.team.id;
				} else {
					this.team = new Models.TeamEntity(attributes.team);
					this.teamId = this.team.id;
				}
			} else if (attributes.teamId !== undefined) {
				this.teamId = attributes.teamId;
			}
			if (attributes.ladder) {
				if (attributes.ladder instanceof Models.LadderEntity) {
					this.ladder = attributes.ladder;
					this.ladderId = attributes.ladder.id;
				} else {
					this.ladder = new Models.LadderEntity(attributes.ladder);
					this.ladderId = this.ladder.id;
				}
			} else if (attributes.ladderId !== undefined) {
				this.ladderId = attributes.ladderId;
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
		team {
			${Models.TeamEntity.getAttributes().join('\n')}
		}
		ladder {
			${Models.LadderEntity.getAttributes().join('\n')}
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