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
import _ from 'lodash';
import moment from 'moment';
import { observable, runInAction } from 'mobx';
import { IAttributeGroup, Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from 'Util/EntityUtils';
import { VisitorsSportentityEntity } from 'Models/Security/Acl/VisitorsSportentityEntity';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { FormEntityData, FormEntityDataAttributes, getAllVersionsFn, getPublishedVersionFn } from 'Forms/FormEntityData';
import { FormVersion } from 'Forms/FormVersion';
import { fetchFormVersions, fetchPublishedVersion } from 'Forms/Forms';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISportentityEntityAttributes extends IModelAttributes, FormEntityDataAttributes {
	name: string;
	sportname: string;
	order: number;

	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('SportentityEntity', 'SportEntity')
// % protected region % [Customise your entity metadata here] end
export default class SportentityEntity extends Model implements ISportentityEntityAttributes, FormEntityData  {
	public static acls: IAcl[] = [
		new VisitorsSportentityEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];
	/* The default order by field when the collection is loaded */
	public get orderByField(): IOrderByCondition<Model> | undefined {
		return {
			path: 'order',
			descending: true,
		};
	}

	/* SportName */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'SportName'] off begin
		name: 'SportName',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
		// % protected region % [Modify props to the crud options here for attribute 'SportName'] end
	})
	public sportname: string;

	/* Order */
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'Order'] off begin
		name: 'Order',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
		// % protected region % [Modify props to the crud options here for attribute 'Order'] end
	})
	public order: number;

	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
		name: 'Name',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
		// % protected region % [Modify props to the crud options here for attribute 'Name'] end
	})
	public name: string;

	@observable
	@attribute({isReference: true})
	public formVersions: FormVersion[] = [];

	@observable
	@attribute()
	public publishedVersionId?: string;

	@observable
	@attribute({isReference: true})
	public publishedVersion?: FormVersion;
	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISportentityEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<ISportentityEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.sportname) {
				this.sportname = attributes.sportname;
			}
			if (attributes.order) {
				this.order = attributes.order;
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
		}
	}

	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
		publishedVersion {
			id
			created
			modified
			formData
		}
	`;
	// % protected region % [Customize Default Expands here] end


	public async saveFromCrud(formMode: EntityFormMode) {
		// % protected region % [Customize Save From Crud here] off begin
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
		// % protected region % [Customize Save From Crud here] end
	}

	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.name;
		// % protected region % [Customise the display name for this entity] end
	}

	public getAllVersions: getAllVersionsFn = (includeSubmissions?, conditions?) => {
		return fetchFormVersions(this, includeSubmissions, conditions)
			.then(d => {
				runInAction(() => this.formVersions = d);
				return d.map(x => x.formData)
			});
	};

	public getPublishedVersion: getPublishedVersionFn = includeSubmissions => {
		return fetchPublishedVersion(this, includeSubmissions)
			.then(d => {
				runInAction(() => this.publishedVersion = d);
				return d ? d.formData : undefined;
			})
	};

	public getSubmissionEntity = () => {
		return Models.SportentitySubmissionEntity;
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}