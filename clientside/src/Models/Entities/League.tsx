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
import { UserLeagueEntity } from 'Models/Security/Acl/UserLeagueEntity';
import { VisitorsLeagueEntity } from 'Models/Security/Acl/VisitorsLeagueEntity';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/CRUD/EntityAttributeList';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ILeagueAttributes extends IModelAttributes {
	id: number;
	name: string;
	sportid: number;
	shortname: string;

	sportId: string;
	sport: Models.Sport | Models.ISportAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('League')
export default class League extends Model implements ILeagueAttributes {
	public static acls: IAcl[] = [
		new UserLeagueEntity(),
		new VisitorsLeagueEntity(),
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

	/* Sport Id */
	@Validators.Required()
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'SportId',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public sportid: number;

	/* Short name */
	@observable
	@attribute()
	@CRUD({
		name: 'ShortName',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public shortname: string;

	/* Sport leagues */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Sport',
		displayType: 'reference-combobox',
		referenceTypeFunc: () => Models.Sport,
	})
	public sportId: string;
	@observable
	@attribute({isReference: true})
	public sport: Models.Sport;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ILeagueAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<ILeagueAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.id) {
				this.id = attributes.id;
			}
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.sportid) {
				this.sportid = attributes.sportid;
			}
			if (attributes.shortname) {
				this.shortname = attributes.shortname;
			}
			if (attributes.sport) {
				if (attributes.sport instanceof Models.Sport) {
					this.sport = attributes.sport;
					this.sportId = attributes.sport.id;
				} else {
					this.sport = new Models.Sport(attributes.sport);
					this.sportId = this.sport.id;
				}
			} else if (attributes.sportId !== undefined) {
				this.sportId = attributes.sportId;
			}
		}
	}

	public defaultExpands = `
	`;


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