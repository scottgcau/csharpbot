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
import { AttributeCRUDOptions } from 'Models/CRUDOptions';
import { Model } from 'Models/Model';
import { Symbols } from '../../../../Symbols';
import AttributeTextField from './AttributeTextField';
import AttributeTextArea from './AttributeTextArea';
import AttributeReferenceCombobox from './AttributeReferenceCombobox';
import AttributeDatePicker from './AttributeDatePicker';
import AttributeTimePicker from './AttributeTimePicker';
import AttributeCheckbox from './AttributeCheckbox';
import AttributePassword from './AttributePassword';
import AttributeDisplayField from './AttributeDisplayField';
import AttributeReferenceMultiCombobox from './AttributeReferenceMultiCombobox';
import AttributeDateTimePicker from './AttributeDateTimePicker';
import AttributeEnumCombobox from './AttributeEnumCombobox';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import AttributeFormData from "./AttributeFormData";
import AttributeFormTile from 'Views/Components/CRUD/Attributes/AttributeFormTile';


export function getAttributeComponent (
	attributeOptions: AttributeCRUDOptions,
	model: Model,
	errors: string[],
	formMode: EntityFormMode = EntityFormMode.VIEW,
	isRequired: boolean = false,
	onAfterChange?: (attributeName: string) => void,
	onChangeAndBlur?: (attributeName: string) => void)
{
	const isReadonly = formMode === EntityFormMode.VIEW || attributeOptions.isReadonly;

	const displayType = {
		[EntityFormMode.VIEW]: attributeOptions.readFieldType,
		[EntityFormMode.CREATE]: attributeOptions.createFieldType,
		[EntityFormMode.EDIT]: attributeOptions.updateFieldType,
	}[formMode];

	switch (displayType) {
		case 'textfield':
			return <AttributeTextField
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onAfterChange) {
						onAfterChange(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				onChangeAndBlur={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'textarea':
			return <AttributeTextArea
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				/>;
		case 'password':
			return <AttributePassword
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'datepicker':
			return <AttributeDatePicker
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'timepicker':
			return <AttributeTimePicker
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'datetimepicker':
			return <AttributeDateTimePicker
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'checkbox':
			return <AttributeCheckbox
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'displayfield':
			return <AttributeDisplayField
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'reference-combobox':
			if (attributeOptions.referenceTypeFunc === undefined) {
				throw new Error('Must have a defined referenceType for display type' + attributeOptions.displayType);
			}
			return <AttributeReferenceCombobox
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				referenceType={attributeOptions.referenceTypeFunc()}
				errors={errors}
				optionEqualFunc={attributeOptions.optionEqualFunc}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				fetchReferenceEntity={attributeOptions.isJoinEntity}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'reference-multicombobox':
			if (attributeOptions.referenceTypeFunc === undefined) {
				throw new Error('Must have a defined referenceType for display type' + attributeOptions.displayType);
			}

			return <AttributeReferenceMultiCombobox
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				referenceType={attributeOptions.referenceTypeFunc()}
				referenceResolveFunction={attributeOptions.referenceResolveFunction}
				optionEqualFunc={attributeOptions.optionEqualFunc}
				errors={errors}
				isJoinEntity={attributeOptions.isJoinEntity}
				disableDefaultOptionRemoval={attributeOptions.disableDefaultOptionRemoval}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				{...attributeOptions.inputProps} />;
		case 'enum-combobox':
			if (attributeOptions.enumResolveFunction === undefined) {
				throw new Error('Must have a defined enumType for display type' + attributeOptions.displayType);
			}
			return <AttributeEnumCombobox
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				/>;
		case 'form-data':
			return <AttributeFormData
				key={attributeOptions.attributeName}
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				onAfterChange={() => {
					if (!!onAfterChange) {
						onAfterChange(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}
				onChangeAndBlur={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
				}}
				{...attributeOptions.inputProps}
				/>;
		case 'form-tile':
			if (attributeOptions.formTileFilterFn === undefined) {
				throw new Error('Must have a defined formTileFilterFn for display type' + attributeOptions.displayType);
			}
			return <AttributeFormTile
				model={model}
				options={attributeOptions}
				errors={errors}
				className={attributeOptions.attributeName}
				isReadonly={isReadonly}
				isRequired={isRequired}
				key={attributeOptions.attributeName}
				onAfterChange={() => {
					if (!!onChangeAndBlur) {
						onChangeAndBlur(attributeOptions.attributeName);
					}
					if (attributeOptions.onAfterChange) {
						attributeOptions.onAfterChange(model);
					}
				}}/>;
		case 'hidden':
			return null;
		// % protected region % [Add more customized cases here] off begin
		// % protected region % [Add more customized cases here] end
		default:
			throw new Error(`No attribute component is defined to handle ${attributeOptions.displayType}`);
	}
}