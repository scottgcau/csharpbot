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
import { SystemuserGameEntity } from 'Models/Security/Acl/SystemuserGameEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IGameEntityAttributes extends IModelAttributes {
	datestart: Date;
	homepoints: number;
	awaypoints: number;
	hometeamid: string;
	awayteamid: string;

	roundId?: string;
	round?: Models.RoundEntity | Models.IRoundEntityAttributes;
	gamerefereess: Array<Models.GamerefereeEntity | Models.IGamerefereeEntityAttributes>;
	venueId?: string;
	venue?: Models.VenueEntity | Models.IVenueEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('GameEntity', 'Game')
// % protected region % [Customise your entity metadata here] end
export default class GameEntity extends Model implements IGameEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsGameEntity(),
		new SystemuserGameEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'DateStart'] off begin
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'DateStart',
		displayType: 'datetimepicker',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseDate,
	})
	public datestart: Date;
	// % protected region % [Modify props to the crud options here for attribute 'DateStart'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomePoints'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'HomePoints',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public homepoints: number;
	// % protected region % [Modify props to the crud options here for attribute 'HomePoints'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayPoints'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'AwayPoints',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public awaypoints: number;
	// % protected region % [Modify props to the crud options here for attribute 'AwayPoints'] end

	// % protected region % [Modify props to the crud options here for attribute 'HomeTeamId'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'HomeTeamId',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public hometeamid: string;
	// % protected region % [Modify props to the crud options here for attribute 'HomeTeamId'] end

	// % protected region % [Modify props to the crud options here for attribute 'AwayTeamId'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'AwayTeamId',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public awayteamid: string;
	// % protected region % [Modify props to the crud options here for attribute 'AwayTeamId'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Round'] off begin
		name: 'Round',
		displayType: 'reference-combobox',
		order: 60,
		referenceTypeFunc: () => Models.RoundEntity,
		// % protected region % [Modify props to the crud options here for reference 'Round'] end
	})
	public roundId?: string;
	@observable
	@attribute({isReference: true})
	public round: Models.RoundEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'GameReferees'] off begin
		name: "GameRefereess",
		displayType: 'reference-multicombobox',
		order: 70,
		referenceTypeFunc: () => Models.GamerefereeEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'gamerefereess',
			oppositeEntity: () => Models.GamerefereeEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'GameReferees'] end
	})
	public gamerefereess: Models.GamerefereeEntity[] = [];

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Venue'] off begin
		name: 'Venue',
		displayType: 'reference-combobox',
		order: 80,
		referenceTypeFunc: () => Models.VenueEntity,
		// % protected region % [Modify props to the crud options here for reference 'Venue'] end
	})
	public venueId?: string;
	@observable
	@attribute({isReference: true})
	public venue: Models.VenueEntity;

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
			if (attributes.homepoints) {
				this.homepoints = attributes.homepoints;
			}
			if (attributes.awaypoints) {
				this.awaypoints = attributes.awaypoints;
			}
			if (attributes.hometeamid) {
				this.hometeamid = attributes.hometeamid;
			}
			if (attributes.awayteamid) {
				this.awayteamid = attributes.awayteamid;
			}
			if (attributes.round) {
				if (attributes.round instanceof Models.RoundEntity) {
					this.round = attributes.round;
					this.roundId = attributes.round.id;
				} else {
					this.round = new Models.RoundEntity(attributes.round);
					this.roundId = this.round.id;
				}
			} else if (attributes.roundId !== undefined) {
				this.roundId = attributes.roundId;
			}
			if (attributes.gamerefereess) {
				for (const model of attributes.gamerefereess) {
					if (model instanceof Models.GamerefereeEntity) {
						this.gamerefereess.push(model);
					} else {
						this.gamerefereess.push(new Models.GamerefereeEntity(model));
					}
				}
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
		round {
			${Models.RoundEntity.getAttributes().join('\n')}
		}
		gamerefereess {
			${Models.GamerefereeEntity.getAttributes().join('\n')}
		}
		venue {
			${Models.VenueEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			gamerefereess: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'gamerefereess',
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