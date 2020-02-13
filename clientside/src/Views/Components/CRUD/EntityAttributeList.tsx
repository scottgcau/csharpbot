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
import * as queryString from "querystring";
import { Model } from 'Models/Model';
import { RouteComponentProps } from 'react-router';
import { observer, inject } from 'mobx-react';
import { ButtonGroup } from '../Button/ButtonGroup';
import { Button, Display } from '../Button/Button';
import { store } from 'Models/Store';
import { getAttributeComponent } from './Attributes/AttributeFactory';
import FormErrors from './FormErrors';
import { observable, action, computed } from 'mobx';
import { IEntityValidationErrors } from 'Validators/Util';
import { safeLookup } from 'Util/IterableUtils';
import alert from 'Util/ToastifyUtils';
import If from '../If/If';
import { SecurityService } from 'Services/SecurityService';
import { IAcl } from 'Models/Security/IAcl';
import * as AttrUtils from 'Util/AttributeUtils';
import { isRequired } from 'Util/EntityUtils';
import { AttributeCRUDOptions } from 'Models/CRUDOptions';

const VALIDATION_ERROR = "Some of the fields are missing or invalid, please check your form.";

export enum EntityFormMode {
	VIEW = 'view',
	CREATE = 'create',
	EDIT = 'edit'
}

interface IEntityCreateProps<T extends Model> extends RouteComponentProps {
	model: T;
	modelType: { acls?: IAcl[], new(...args: any[]): T };
	formMode: EntityFormMode;
	title: string;
	sectionClassName: string;
	customFields?: React.ReactNode;
	customRelationPath?: any
}

@observer
class EntityAttributeList<T extends Model> extends React.Component<IEntityCreateProps<T>, any> {
	@observable
	private _generalFormError: React.ReactNode;
	private validationPath: {} = {};
	private hasSubmittedOnce: boolean;

	@computed
	private get generalFormError(): React.ReactNode{
		if(this.props.model.hasValidationError){
			return VALIDATION_ERROR;
		}
		return this._generalFormError;
	};

	private set generalFormError(value: React.ReactNode) {
		this._generalFormError = value;
	}

	@observable
	private detailedFormError: React.ReactNode;

	@observable
	private fieldErrors?: IEntityValidationErrors = {};

	public render() {
		const { title, sectionClassName } = this.props;
		return (
			<div className="crud-component">
				<section className={sectionClassName}>
					<FormErrors error={this.generalFormError} detailedErrors={this.detailedFormError} />
						{SecurityService.canUpdate(this.props.modelType) && this.props.formMode === EntityFormMode.VIEW
							? (
								<div className="crud__header">
									<h2>{title}</h2>
									<Button display={Display.Outline} onClick={this.onEdit}>Edit</Button>
								</div>
							)
						: <h2>{title}</h2>}
					<form onSubmit={this.onSubmit}>
						<div className="crud__form-container">
							{this.constructAttributeCreateHtml()}
							{this.props.customFields}
						</div>

						<ButtonGroup>
							<Button display={Display.Outline} onClick={this.onCancel}>Cancel</Button>
							<If condition={SecurityService.canUpdate(this.props.modelType) && this.props.formMode !== EntityFormMode.VIEW}>
								<Button type='submit' display={Display.Outline}>Submit</Button>
							</If>
						</ButtonGroup>
					</form>
				</section>
			</div>
		);
	}

	protected onSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();

		const { model } = this.props;
		const modelName = model.getModelName();
		model.clearErrors();

		model.validate(this.props.customRelationPath).then(() => {
		if (model.hasValidationError) {
			this.hasSubmittedOnce = true;
			return;
		}

		return model.saveFromCrud(this.props.formMode)
			.then((result) => {
				const actionDone = this.props.formMode === EntityFormMode.CREATE? 'added'
					: (this.props.formMode === EntityFormMode.EDIT? 'edited': '');

				console.log(`Successfully ${actionDone} ${modelName}`, result);
				alert(`Successfully ${actionDone} ${modelName}`, 'success');

				const { redirect } = queryString.parse(this.props.location.search.substring(1));

				if (redirect && !Array.isArray(redirect)) {
					store.routerHistory.push(redirect.replace('{id}', model.id));
				} else {
					store.routerHistory.goBack();
				}
			})
			.catch((error) => {
				console.error(`Failed adding ${modelName}`, error);

				if (typeof error == 'string') {
					alert(error, 'error');
				}

				const errors: Array<{code: string, message: string}> | undefined = safeLookup(
					error,
					'networkError',
					'result',
					'errors',
					{map: (x: any) => {
						return {
							code: safeLookup(x, 'extensions', 'data', 'Code'),
							message: safeLookup(x, 'extensions', 'data', 'Detail'),
						};
					}}
				);

				if (errors && errors.length) {
					const errorCodes = errors.map(x => x.code);
					let detailedError = 'Could not save entity.';

					if (errorCodes.indexOf("23505") >= 0) {
						detailedError += ' A duplicate reference was found.';
					}

					this.setErrors(
						detailedError,
						{},
						errors.map(x => x.message).join(' '));
					return;
				}

				this.setErrors(error.message, {});
			});
		});
	}

	@action
	private setErrors(generalError: React.ReactNode, errors?: IEntityValidationErrors, detailedErrors?: React.ReactNode) {
		this.generalFormError = generalError;
		this.fieldErrors = errors;
		this.detailedFormError = detailedErrors;
	}

	@action
	private clearErrors() {
		this.generalFormError = "";
		this.fieldErrors = {};
	}

	private onCancel = (event: React.MouseEvent<Element, MouseEvent>) => {
		store.routerHistory.goBack();
	}

	private onEdit = (event: React.MouseEvent<Element, MouseEvent>) => {
		this.props.history.push(`../Edit/${this.props.model.id}`);
	}

	private constructAttributeCreateHtml = () => {
		let attributeOptions = this.props.model.getAttributeCRUDOptions();
		if(this.props.formMode !== 'create'){
			let createDateAttr = new AttributeCRUDOptions('created', {name:'Created', displayType: 'datepicker', headerColumn: false, searchable: true, searchFunction: 'equal', searchTransform: AttrUtils.standardiseDate});
			createDateAttr.isReadonly = true;
			let modifiedDateAttr = new AttributeCRUDOptions('modified', {name:'Modified', displayType: 'datepicker', headerColumn: false, searchable: true, searchFunction: 'equal', searchTransform: AttrUtils.standardiseDate});
			modifiedDateAttr.isReadonly = true;
			const defualtDateAttrs: AttributeCRUDOptions[] = [createDateAttr, modifiedDateAttr];
			attributeOptions = [...attributeOptions, ...defualtDateAttrs];
		}
		const model = this.props.model;

		return attributeOptions.map(attributeOption =>
			getAttributeComponent(
				attributeOption,
				model,
				this.props.model.getErrorsForAttribute(attributeOption.attributeName),
				this.props.formMode,
				isRequired(model, attributeOption.attributeName),
				this.onAttributeAfterChange,
				this.onAttributeChangeAndBlur
			));
	}

	private onAttributeAfterChange = (attributeName: string) => {
		const { model } = this.props;
		if(!!this.validationPath[attributeName] || this.hasSubmittedOnce){
			if(this.props.model.getErrorsForAttribute(attributeName).length > 0){
				model.validate(this.hasSubmittedOnce ? this.props.customRelationPath : this.validationPath).then(() => {
					if (!model.hasValidationError) {
						this.clearErrors();
					}
				});
			}
		}
	}

	private onAttributeChangeAndBlur = (attributeName: string) => {
		const { model } = this.props;
		this.validationPath = {...this.validationPath, [`${attributeName}`]: true};
		model.validate(this.hasSubmittedOnce ? this.props.customRelationPath : this.validationPath).then(() => {
			if (!model.hasValidationError) {
				this.clearErrors();
			}
		});
	}
}

export default EntityAttributeList;