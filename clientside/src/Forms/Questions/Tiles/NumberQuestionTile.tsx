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
import React, { Component } from 'react';
import { observer } from 'mobx-react';
import { NumberTextField } from 'Views/Components/NumberTextBox/NumberTextBox';
import { QuestionType, ValidatorType } from 'Forms/Schema/Question';
import { IQuestionTile, QuestionTileOptionsProps } from '../QuestionTile';
import TileOptions from '../TileOptions';
import { hasRequiredValidator } from '../../Validators/ValidationUtils';
import CompareNumber from 'Forms/Conditions/CompareNumber';
import { ValidateNumber } from 'Forms/Validators/ValidateNumber';
import { QuestionComponent, QuestionComponentProps } from 'Forms/Questions/QuestionComponent';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

@observer
export class NumberQuestionTileOptions extends Component<QuestionTileOptionsProps> {
	// % protected region % [Add extra options class properties here] off begin
	// % protected region % [Add extra options class properties here] end

	public render() {
		// % protected region % [Customize options render here] off begin
		const { question, schema } = this.props;

		return (
			<TileOptions question={question} schema={schema} hasShowConditions hasValidators hasTooltip />
		);
		// % protected region % [Customize options render here] end
	}
}

export interface INumberQuestionTileProps<T> extends QuestionComponentProps<T> {
	// % protected region % [Add extra props here] off begin
	// % protected region % [Add extra props here] end
}

@observer
export class NumberQuestionTile<T> extends QuestionComponent<T, INumberQuestionTileProps<T>> implements IQuestionTile {
	// % protected region % [Customize static form vars here] off begin
	static displayName = 'Number';

	static questionType: QuestionType = 'number';

	static optionsMenu = NumberQuestionTileOptions;

	static compareFunction = CompareNumber;

	static validateFunction = ValidateNumber;

	static conditionOptions = [
		{ display: 'Equal', value: 'equal' },
		{ display: 'Greater than', value: 'greaterThan' },
		{ display: 'Less than', value: 'lessThan' },
		{ display: 'Not equal', value: 'notEqual' },
		{ display: 'Greater than or equal', value: 'greaterThanOrEqual' },
		{ display: 'Less than or equal', value: 'lessThanOrEqual' },
		{ display: 'Contains', value: 'contains' },
	];

	static validatorOptions: { display: string, value: ValidatorType }[] = [
		{ display: 'Required', value: 'required' },
		{ display: 'Equal', value: 'equalNumber' },
		{ display: 'Not Equal', value: 'notEqualNumber' },
		{ display: 'Greater Than', value: 'greaterThan' },
		{ display: 'Greater Than or Equal', value: 'greaterThanOrEqual' },
		{ display: 'Less Than', value: 'lessThan' },
		{ display: 'Less Than or Equal', value: 'lessThanOrEqual' },
	];

	static stylingOptions = undefined;
	// % protected region % [Customize static form vars here] end

	// % protected region % [Add extra class properties here] off begin
	// % protected region % [Add extra class properties here] end

	public render() {
		// % protected region % [Customize render here] off begin
		const { title, model, id, isReadOnly, validators, toolTip, className, checkValidation } = this.props;

		return (
			<NumberTextField
				model={model}
				modelProperty={id}
				label={title}
				tooltip={toolTip}
				labelVisible
				isReadOnly={isReadOnly}
				isRequired={hasRequiredValidator(validators)}
				onAfterChange={checkValidation}
				className={className}
			/>
		);
		// % protected region % [Customize render here] end
	}
}

// % protected region % [Add extra methods here] off begin
// % protected region % [Add extra methods here] end