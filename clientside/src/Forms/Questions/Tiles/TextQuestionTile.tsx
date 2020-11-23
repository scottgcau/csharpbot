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
import { TextField } from 'Views/Components/TextBox/TextBox';
import { QuestionType, ValidatorType } from 'Forms/Schema/Question';
import { IQuestionTile, QuestionTileOptionsProps } from '../QuestionTile';
import { hasRequiredValidator } from '../../Validators/ValidationUtils';
import TileOptions from '../TileOptions';
import CompareText from 'Forms/Conditions/CompareText';
import { ValidateText } from 'Forms/Validators/ValidateText';
import { QuestionComponent, QuestionComponentProps } from 'Forms/Questions/QuestionComponent';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

@observer
export class TextQuestionTileOptions extends Component<QuestionTileOptionsProps> {
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

export interface ITextQuestionTileProps<T> extends QuestionComponentProps<T> {
	// % protected region % [Add extra props here] off begin
	// % protected region % [Add extra props here] end
}

@observer
export class TextQuestionTile<T> extends QuestionComponent<T, ITextQuestionTileProps<T>> implements IQuestionTile {
	// % protected region % [Customize static form vars here] off begin
	static displayName = 'Textbox';

	static questionType: QuestionType = 'text';

	static optionsMenu = TextQuestionTileOptions;

	static compareFunction = CompareText;

	static validateFunction = ValidateText;

	static conditionOptions = [
		{ display: 'Equal', value: 'equal' },
		{ display: 'Not equal', value: 'notEqual' },
		{ display: 'Contains', value: 'contains' },
	];

	static validatorOptions: { display: string, value: ValidatorType }[] = [
		{ display: 'Required', value: 'required' },
		{ display: 'Email', value: 'email' },
		{ display: 'Phone', value: 'phone' },
		{ display: 'Custom', value: 'custom' },
	];

	static stylingOptions = undefined;
	// % protected region % [Customize static form vars here] end

	// % protected region % [Add extra class properties here] off begin
	// % protected region % [Add extra class properties here] end

	public render() {
		// % protected region % [Customize render here] off begin
		const { title, model, id, isReadOnly, validators, toolTip, className, checkValidation } = this.props;

		return (
			<TextField
				model={model}
				modelProperty={id}
				label={title}
				labelVisible
				isReadOnly={isReadOnly}
				isRequired={hasRequiredValidator(validators)}
				tooltip={toolTip}
				onAfterChange={checkValidation}
				className={className}
			/>
		);
		// % protected region % [Customize render here] end
	}
}

// % protected region % [Add extra methods here] off begin
// % protected region % [Add extra methods here] end