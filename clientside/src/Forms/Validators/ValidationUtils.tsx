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
import React, { ReactNode } from 'react';
import _ from 'lodash';
import { DateTimePicker } from 'Views/Components/DateTimePicker/DateTimePicker';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { NumberTextField } from 'Views/Components/NumberTextBox/NumberTextBox';
import { Validator, Form, Validators } from '../Schema/Question';
import { questions } from 'Forms/Questions/QuestionUtils';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

//Takes a validator and returns an error message if the validators condition is not satisfied, returns null otherwise
export function checkValidationConditions<T>(validator: Validator, model: T, schema: Form): string | null {
	// Find the question the validator maps to in the schema
	const questionMap = _.flatMap(schema, s => {
		return s.contents.find(q => {
			return q.id === validator.path;
		});
	});

	const question = questionMap.find(q => {
		if (q !== undefined) {
			return q.id === validator.path;
		}

		return false;
	});

	if (question !== undefined) {
		const questionObject = questions.find(q => q.questionType === question.questionType)

		if (questionObject && questionObject.compareFunction && questionObject.validateFunction) {
			return questionObject.validateFunction(validator, model);
		}
	}

	return 'Could not find the question this validator maps to\n';
}

// Builds an array of validation errors from an array of validators
export function buildValidationErrorMessage<T>(validators: Validators | undefined, model: T, schema: Form, disableValidation: boolean): string[] {
	const errors: string[] = [];

	if (validators !== undefined && !disableValidation) {
		validators.forEach(validator => {
			const error = checkValidationConditions(validator, model, schema);
			if (error != null) {
				errors.push(error);
			}
		});
	}
	return errors;
}

// Returns true if a required validator exists within the array of validators, false otherwise
export const hasRequiredValidator = (validators: Validators | undefined): boolean => {
	if (validators) {
		const required = validators.find(v => { return v.validator === 'required'; });
		return required !== undefined;
	}
	return false;
};

// Returns an element to render to allow for inputting the additional validation input
export function getAdditionalAttributeElement(validator: Validator): ReactNode {
	let label = '';
	switch (validator.validator) {
		case 'before':
			label = 'Before When';
		case 'after':
			label = 'After When';
			return (
				<DateTimePicker
					model={validator}
					modelProperty="additionalData"
					label={label}
					labelVisible
				/>
			);
		case 'custom':
			label = 'Custom Regex';
			return (
				<TextField
					model={validator}
					modelProperty="additionalData"
					label={label}
					labelVisible
				/>
			);
		case 'length':
			label = 'Number of Characters';
			return (
				<NumberTextField
					model={validator}
					modelProperty="additionalData"
					label={label}
					labelVisible
				/>
			);
		case 'equalNumber':
		case 'notEqualNumber':
		case 'greaterThan':
		case 'greaterThanOrEqual':
		case 'lessThan':
		case 'lessThanOrEqual':
			label = 'Value';
			return <NumberTextField
				model={validator}
				modelProperty="additionalData"
				label={label}
				labelVisible
			/>;
		// % protected region % [Add any extra validator additional fields here] off begin
		// % protected region % [Add any extra validator additional fields here] end
		default:
			return null;
	}
}

// % protected region % [Add any extra methods here] off begin
// % protected region % [Add any extra methods here] end
