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
import { VisitorsSportentityEntity } from 'Models/Security/Acl/VisitorsSportentityEntity';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/CRUD/EntityAttributeList';
import { formTiles } from 'Views/Components/CRUD/Attributes/AttributeFormTile';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISportentityFormTileAttributes extends IModelAttributes {
	tile: string;

	formId: string;
	form: Models.Sportentity | Models.ISportentityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('SportentityFormTile')
export default class SportentityFormTile extends Model implements ISportentityFormTileAttributes {
	public static acls: IAcl[] = [
		new VisitorsSportentityEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	/* The tile that the form is contained in */
	@Validators.Required()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		name: 'Tile',
		displayType: 'form-tile',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
		displayFunction: (attribute: string) => {
			const value = formTiles.find(t => t.value === attribute);
			return value ? value.display : '';
		},
	})
	public tile: string;

	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: 'Form',
		displayType: 'reference-combobox',
		referenceTypeFunc: () => Models.Sportentity,
	})
	public formId: string;
	@observable
	@attribute({isReference: true})
	public form: Models.Sportentity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISportentityFormTileAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<ISportentityFormTileAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.tile) {
				this.tile = attributes.tile;
			}
			if (attributes.form) {
				if (attributes.form instanceof Models.Sportentity) {
					this.form = attributes.form;
					this.formId = attributes.form.id;
				} else {
					this.form = new Models.Sportentity(attributes.form);
					this.formId = this.form.id;
				}
			} else if (attributes.formId !== undefined) {
				this.formId = attributes.formId;
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