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
import { observer } from 'mobx-react';
import { FormDesigner, GenericFormDesignerProps } from 'Forms/Designer/FormDesigner';
import { store } from 'Models/Store';
import { FormEntityData, FormEntityDataAttributes } from 'Forms/FormEntityData';
import { IModelAttributes, Model } from 'Models/Model';
import { gql } from 'apollo-boost';
import { FormVersion } from 'Forms/FormVersion';
import { action, computed } from 'mobx';
import { lowerCaseFirst } from 'Util/StringUtils';
import alert from 'Util/ToastifyUtils';
import { FetchResult } from 'react-apollo';

export interface FormEntityDesignerProps extends GenericFormDesignerProps {
	form: FormEntityData & Model;
	returnRoute?: string;
	onCancel?: () => void;
	onSaveDraft?: (version: FormVersion) => void;
	onSavePublish?: (version: FormVersion) => void;
}

@observer
export class FormEntityDesigner extends React.Component<FormEntityDesignerProps> {
	private designerRef: FormDesigner | null;

	@computed
	private get versionName() {
		return lowerCaseFirst(this.props.form.getModelName()) + 'FormVersion';
	}

	@action
	private assignResults = (shouldPublish: boolean, result: FetchResult<any>) => {
		if (this.props.form.formVersions) {
			const newVersion = result.data.create[0];

			const formAttributes: Partial<FormEntityDataAttributes & IModelAttributes> = {formVersions: [newVersion]};
			if (shouldPublish) {
				formAttributes.publishedVersion = newVersion;
			}
			this.props.form.assignAttributes(formAttributes);

			if (this.designerRef) {
				this.designerRef.selectVersion(newVersion.id);
			}
		}
	}

	private save = (version: FormVersion, shouldPublish: boolean) => {
		const modelName = this.props.form.getModelName();
		const modelNameCamelCase = lowerCaseFirst(this.props.form.getModelName());

		return store.apolloClient
			.mutate({
				mutation: gql`mutation create($version: [${modelName}FormVersionInput]) {
					create: create${modelName}FormVersion(${modelNameCamelCase}FormVersions: $version) {
						id
						created
						modified
						formId
						formData
						version
					}
				}`,
				variables: {
					version: [_.omit({
						...version,
						formId: this.props.form.id,
						publishVersion: shouldPublish,
						formData: JSON.stringify(version.formData),
					}, ['__typename', 'id'])],
				},
			})
			.then(result => {
				this.assignResults(shouldPublish, result);
			})
			.catch(e => {
				alert('Could not save form version', 'error');
				console.error(e);
			});
	}

	private onCancel = () => {
		if (this.props.onCancel) {
			return this.props.onCancel();
		}

		if (this.props.returnRoute) {
			return store.routerHistory.push(this.props.returnRoute);
		}
		store.routerHistory.goBack();
	}

	private onSaveDraft = async (version: FormVersion) => {
		if (this.props.onSaveDraft) {
			return this.props.onSaveDraft(version);
		}

		await this.save(version, false);
		if (this.props.returnRoute) {
			return store.routerHistory.push(this.props.returnRoute);
		}
		store.routerHistory.goBack();
	}

	private onSavePublish = async (version: FormVersion) => {
		if (this.props.onSavePublish) {
			return this.props.onSavePublish(version);
		}

		await this.save(version, true);
		if (this.props.returnRoute) {
			return store.routerHistory.push(this.props.returnRoute);
		}
		store.routerHistory.goBack();
	}

	public render() {
		return <FormDesigner
			ref={ref => this.designerRef = ref}
			form={this.props.form}
			className={this.props.className}
			onCancel={this.onCancel}
			// onSaveDraft={this.onSaveDraft}
			onSavePublish={this.onSavePublish}
			initialSelectedVersion={this.props.initialSelectedVersion} />
	}
}