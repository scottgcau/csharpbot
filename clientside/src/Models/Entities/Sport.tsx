/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import _ from 'lodash';
import moment from 'moment';
import { observable, runInAction } from 'mobx';
import { Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from '../../Util/EntityUtils';
import { UserSportEntity } from 'Models/Security/Acl/UserSportEntity';
import { VisitorsSportEntity } from 'Models/Security/Acl/VisitorsSportEntity';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/CRUD/EntityAttributeList';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISportAttributes extends IModelAttributes {
	id: number;
	name: string;

	leaguess: Models.League[] | Models.ILeagueAttributes[];
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('Sport')
export default class Sport extends Model implements ISportAttributes {
	public static acls: IAcl[] = [
		new UserSportEntity(),
		new VisitorsSportEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];
	/* The default order by field when the collection is loaded */
	public get orderByField(): IOrderByCondition<Model> | undefined {
		return {
			path: 'name',
			descending: false,
		};
	}

	/* Id */
	@Validators.Required()
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Id',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public id: number;

	/* Name */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;

	/* Sport leagues */
	@observable
	@attribute({isReference: true})
	@CRUD({
		name: "Leaguess",
		displayType: 'reference-multicombobox',
		referenceTypeFunc: () => Models.League,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'leaguess',
			oppositeEntity: () => Models.League,
		})
	})
	public leaguess: Models.League[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISportAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<ISportAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.id) {
				this.id = attributes.id;
			}
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.leaguess) {
				for (const model of attributes.leaguess) {
					if (model instanceof Models.League) {
						this.leaguess.push(model);
					} else {
						this.leaguess.push(new Models.League(model));
					}
				}
			}
		}
	}

	public defaultExpands = `
		leaguess {
			${Models.League.getAttributes().join('\n')}
		}
	`;


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
						]
					},
				],
			});
	}

	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}