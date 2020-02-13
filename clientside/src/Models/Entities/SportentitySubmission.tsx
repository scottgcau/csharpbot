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
import { VisitorsSportentitySubmission } from 'Models/Security/Acl/VisitorsSportentitySubmission';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/CRUD/EntityAttributeList';
import { FormVersion } from 'Forms/FormVersion';
import { FormEntity, SubmissionEntity } from 'Forms/FormEntity';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISportentitySubmissionAttributes extends IModelAttributes, SubmissionEntity {

	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('SportentitySubmission')
export default class SportentitySubmission extends Model implements ISportentitySubmissionAttributes {
	public static acls: IAcl[] = [
		new VisitorsSportentitySubmission(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	@observable
	@attribute()
	@CRUD({
		name: 'Submission Data',
		displayType: 'form-data',
		headerColumn: false,
		searchable: false,
	})
	public submissionData: {[key: string]: any} = {};

	@observable
	@attribute()
	@CRUD({
		name: 'Form Version',
		displayType: 'reference-combobox',
		isJoinEntity: true,
		readFieldType: 'hidden',
		updateFieldType: 'hidden',
		referenceTypeFunc: () => Models.Sportentity,
		onAfterChange: (model) => {
			const formEntity = model['formVersionId'] as FormEntity;
			model['formVersionId'] = formEntity.publishedVersionId;
			model['formVersion'] = formEntity.publishedVersion;
		}
	})
	formVersionId: string;

	@observable
	@attribute({isReference: true})
	formVersion: FormVersion;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISportentitySubmissionAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<ISportentitySubmissionAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if(attributes.submissionData){
				if (typeof attributes.submissionData === 'string') {
					attributes.submissionData = JSON.parse(attributes.submissionData) as {[key: string]: any};
				}
				this.submissionData = attributes.submissionData;
			}
			if(attributes.formVersion){
				if (typeof attributes.formVersion.formData === 'string') {
					attributes.formVersion.formData = JSON.parse(attributes.formVersion.formData);
				}
				this.formVersion = attributes.formVersion;
			}
			if(attributes.formVersionId){
				this.formVersionId = attributes.formVersionId;
			}
		}
	}

	public defaultExpands = `
		formVersion {
			id
			created
			modified
			formData
			form {
				id
				name
			}
		}
	`;

	private submissionTransform: jsonReplacerFn = (input) => {
		const submissionData = input['submissionData'];
		if (submissionData) {
			input['submissionData'] = JSON.stringify(submissionData);
		}
		return input;
	}

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			submissionData: {},
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
				jsonTransformFn: this.submissionTransform,
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