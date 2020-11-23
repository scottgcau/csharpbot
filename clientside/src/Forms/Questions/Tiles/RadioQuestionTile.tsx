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
import { QuestionType, ValidatorType } from 'Forms/Schema/Question';
import { RadioButtonGroup, RadioAlignment } from 'Views/Components/RadioButton/RadioButtonGroup';
import { IQuestionTile, QuestionTileOptionsProps } from '../QuestionTile';
import TileOptions from '../TileOptions';
import CompareText from 'Forms/Conditions/CompareText';
import { ValidateRadio } from 'Forms/Validators/ValidateRadio';
import { QuestionComponent, QuestionComponentProps } from 'Forms/Questions/QuestionComponent';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

@observer
export class RadioQuestionTileOptions extends Component<QuestionTileOptionsProps> {
	// % protected region % [Add extra options class properties here] off begin
	// % protected region % [Add extra options class properties here] end

	public render() {
		// % protected region % [Customize options render here] off begin
		const { question, schema } = this.props;

		return (
			<TileOptions
				question={question}
				schema={schema}
				hasShowConditions
				hasOptions
				hasValidators
				hasStylingOptions
				hasTooltip />
		);
		// % protected region % [Customize options render here] end
	}
}

interface RadioQuestionTileQuestionOptions {
	values?: {
		id: string;
		value: string;
	}[];
	align?: RadioAlignment;
}

export interface IRadioQuestionTileProps<T> extends QuestionComponentProps<T> {
	options: RadioQuestionTileQuestionOptions;
	// % protected region % [Add extra props here] off begin
	// % protected region % [Add extra props here] end
}

@observer
export class RadioQuestionTile<T> extends QuestionComponent<T, IRadioQuestionTileProps<T>> implements IQuestionTile {
	// % protected region % [Customize static form vars here] off begin
	static displayName = 'Radio Button';

	static questionType: QuestionType = 'radiobutton';

	static optionsMenu = RadioQuestionTileOptions;

	static compareFunction = CompareText;

	static validateFunction = ValidateRadio;

	static conditionOptions = [
		{ display: 'Equal', value: 'equal' },
	];

	static stylingOptions = [
		{ display: 'Vertical', value: 'radio-group--vertical' },
		{ display: 'Horizontal', value: 'radio-group--horizontal' },
	];

	static validatorOptions: { display: string, value: ValidatorType }[] = [
		{ display: 'Required', value: 'required' },
	];
	// % protected region % [Customize static form vars here] end

	// % protected region % [Add extra class properties here] off begin
	// % protected region % [Add extra class properties here] end

	public render() {
		// % protected region % [Customize render here] off begin
		const { title, options, id, model, isReadOnly, className, toolTip } = this.props;

		return (
			<RadioButtonGroup
				uuidKey
				name={id}
				model={model}
				modelProperty={id}
				label={title}
				tooltip={toolTip}
				isReadOnly={isReadOnly}
				options={options && options.values
					? options.values.map(value => ({ display: value.value, value: value.value }))
					: []}
				className={className}
			/>
		);
		// % protected region % [Customize render here] end
	}
}

// % protected region % [Add extra methods here] off begin
// % protected region % [Add extra methods here] end
