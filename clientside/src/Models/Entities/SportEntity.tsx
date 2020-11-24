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
import { VisitorsSportEntity } from 'Models/Security/Acl/VisitorsSportEntity';
import { SystemuserSportEntity } from 'Models/Security/Acl/SystemuserSportEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISportEntityAttributes extends IModelAttributes {
	order: number;
	fullname: string;

	leaguess: Array<Models.LeagueEntity | Models.ILeagueEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('SportEntity', 'Sport')
// % protected region % [Customise your entity metadata here] end
export default class SportEntity extends Model implements ISportEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsSportEntity(),
		new SystemuserSportEntity(),
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

	/**
	 * The default order by field when the collection is loaded .
	 */
	public get orderByField(): IOrderByCondition<Model> | undefined {
		// % protected region % [Modify the order by field here] off begin
		return {
			path: 'order',
			descending: true,
		};
		// % protected region % [Modify the order by field here] end
	}

	// % protected region % [Modify props to the crud options here for attribute 'Order'] off begin
	/**
	 * Order
	 */
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Order',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public order: number;
	// % protected region % [Modify props to the crud options here for attribute 'Order'] end

	// % protected region % [Modify props to the crud options here for attribute 'FullName'] off begin
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

	/**
	 * Related leagues
	 */
	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Leagues'] off begin
		name: "Leaguess",
		displayType: 'reference-multicombobox',
		order: 30,
		referenceTypeFunc: () => Models.LeagueEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'leaguess',
			oppositeEntity: () => Models.LeagueEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Leagues'] end
	})
	public leaguess: Models.LeagueEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISportEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ISportEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.order) {
				this.order = attributes.order;
			}
			if (attributes.fullname) {
				this.fullname = attributes.fullname;
			}
			if (attributes.leaguess) {
				for (const model of attributes.leaguess) {
					if (model instanceof Models.LeagueEntity) {
						this.leaguess.push(model);
					} else {
						this.leaguess.push(new Models.LeagueEntity(model));
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
		leaguess {
			${Models.LeagueEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			leaguess: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'leaguess',
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