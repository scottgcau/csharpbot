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
import { VisitorsSeasonSubmission } from 'Models/Security/Acl/VisitorsSeasonSubmission';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { FormVersion } from 'Forms/FormVersion';
import { FormEntityData, SubmissionEntityData } from 'Forms/FormEntityData';
import { SERVER_URL } from 'Constants';
import {SuperAdministratorScheme} from '../Security/Acl/SuperAdministratorScheme';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ISeasonSubmissionEntityAttributes extends IModelAttributes, SubmissionEntityData {

	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('SeasonSubmissionEntity', 'Season Submission')
// % protected region % [Customise your entity metadata here] end
export default class SeasonSubmissionEntity extends Model implements ISeasonSubmissionEntityAttributes {
	public static acls: IAcl[] = [
		new SuperAdministratorScheme(),
		new VisitorsSeasonSubmission(),
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

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'Submission Data'] off begin
		name: 'Submission Data',
		displayType: 'form-data',
		order: 10,
		headerColumn: false,
		searchable: false,
		// % protected region % [Modify props to the crud options here for attribute 'Submission Data'] end
	})
	public submissionData: {[key: string]: any} = {};

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'Form Version'] off begin
		name: 'Form Version',
		displayType: 'reference-combobox',
		order: 20,
		isJoinEntity: true,
		headerColumn: true,
		readFieldType: 'hidden',
		updateFieldType: 'hidden',
		referenceTypeFunc: () => Models.SeasonEntity,
		displayFunction: (attr, that) => (that as SeasonSubmissionEntity).formVersion.version.toString(),
		onAfterChange: (model) => {
			const formEntity = model['formVersionId'] as FormEntityData;
			model['formVersionId'] = formEntity.publishedVersionId;
			model['formVersion'] = formEntity.publishedVersion;
		},
		// % protected region % [Modify props to the crud options here for attribute 'Form Version'] end
	})
	formVersionId: string;

	@observable
	@attribute({isReference: true})
	formVersion: FormVersion;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ISeasonSubmissionEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ISeasonSubmissionEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
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
		formVersion {
			id
			created
			modified
			formData
			version
			form {
				id
				name
			}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	// % protected region % [Customize List Expands here] off begin
	public listExpands = `
		formVersion {
			id
			created
			modified
			formData
			version
			form {
				id
				name
			}
		}
	`;
	// % protected region % [Customize List Expands here] end

	/**
	 * The JSON transform function used to transform the submission data before
	 * sending it to the server.
	 */
	private submissionTransform: jsonReplacerFn = (input) => {
		// % protected region % [Modify submission transform here] off begin
		const submissionData = input['submissionData'];
		if (submissionData) {
			input['submissionData'] = JSON.stringify(submissionData);
		}
		return input;
		// % protected region % [Modify submission transform here] end
	}

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
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