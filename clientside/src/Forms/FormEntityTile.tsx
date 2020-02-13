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
import * as React from 'react';
import _ from 'lodash';
import { observer } from 'mobx-react';
import { action, computed, observable } from 'mobx';
import { jsonReplacerFn, Model } from 'Models/Model';
import { FormTile, IFormProps } from './FormTile';
import { FormEntity, SubmissionEntity } from './FormEntity';

export interface IFormEntityTileProps<T extends FormEntity> extends IFormProps<T> {
	model: T;
	onAfterSubmit?: (entity: Model & SubmissionEntity) => void;
}

/**
 * Managed form that handles submit getting published forms and submit logic automatically
 */
@observer
export class FormEntityTile<T extends FormEntity> extends React.Component<IFormEntityTileProps<T>> {
	@observable
	private submissionEntity: Model & SubmissionEntity = new (this.props.model.getSubmissionEntity())();

	private submissionTransform: jsonReplacerFn = (input) => {
		const submissionData = input['submissionData'];
		if (submissionData) {
			input['submissionData'] = JSON.stringify(submissionData);
		}
		return input;
	}

	@action
	private onSubmit = () => {
		if (this.formVersion) {
			this.submissionEntity.formVersionId = this.formVersion.id;
			const questions = _
				.flatMap(this.formVersion.formData, s => s.contents)
				.filter(q => q.property !== undefined);
			const excludedAttributes = ['id', 'created', 'modified', 'submissionData', 'formVersionId'];
			const submissionAttributes = _.without(this.submissionEntity.attributes, ...excludedAttributes);
			for (const question of questions) {
				const data = this.submissionEntity.submissionData[question.id];
				const property = question.property;
				if (data !== undefined && data !== null && property && submissionAttributes.indexOf(property) > -1) {
					this.submissionEntity.assignAttributes({[property]: data});
				}
			}
		}

		return this.submissionEntity.save({submissionData: {}}, {jsonTransformFn: this.submissionTransform})
			.then(() => {
				if (this.props.onAfterSubmit) {
					return this.props.onAfterSubmit(this.submissionEntity);
				}
			});
	}

	@computed
	public get formVersion() {
		return this.props.model.publishedVersion;
	}

	public render() {
		if (this.formVersion) {
			return <FormTile
				{...this.props}
				model={this.submissionEntity.submissionData}
				schema={this.formVersion.formData}
				onSubmit={this.onSubmit} />;
		}
		return <div>There is no published version for this form</div>;
	}
}