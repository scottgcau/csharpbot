﻿/*
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
import React, { Component, ReactNode } from 'react';
import { action, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import _ from 'lodash';
import * as uuid from 'uuid';

import { questions } from 'Forms/Questions/QuestionUtils';
import { Combobox } from 'Views/Components/Combobox/Combobox';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { Button } from 'Views/Components/Button/Button';
import { DateTimePicker } from 'Views/Components/DateTimePicker/DateTimePicker';
import { Form, Question } from '../Schema/Question';
import { getAdditionalAttributeElement } from '../Validators/ValidationUtils';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

interface ComboBoxOptions {
	display: string;
	value: string;
	// % protected region % [Add to ComboBoxOptions here] off begin
	// % protected region % [Add to ComboBoxOptions here] end
}

export interface TileOptionsProps {
	schema: Form;
	question: Question;
	hasShowConditions?: boolean;
	hasStylingOptions?: boolean;
	hasOptions?: boolean;
	hasValidators?: boolean;
	hasTooltip?: boolean;
	// % protected region % [Add to TileOptionsProps here] off begin
	// % protected region % [Add to TileOptionsProps here] end
}

@observer
export default class TileOptions extends Component<TileOptionsProps> {
	// % protected region % [Add extra class fields here] off begin
	// % protected region % [Add extra class fields here] end

	public componentDidMount(): void {
		this.initConditions();
		this.initValidators();
		// % protected region % [Add additional initializers here] off begin
		// % protected region % [Add additional initializers here] end
	}

	private getQuestionType(): string {
		// % protected region % [Override getQuestionType here] off begin
		const { question, schema } = this.props;
		let questionType: string = '';

		if (question.showConditions) {
			question.showConditions.forEach(c => {
				const questionTypes = _.flatMap(schema, x => { return x.contents; }).find(q => { return q.id === c.path; });
				if (questionTypes) {
					questionType = questionTypes.questionType;
				}
			});
		}

		return questionType;
		// % protected region % [Override getQuestionType here] end
	}

	private getConditionOptions(): Array<ComboBoxOptions> {
		// % protected region % [Override getConditionOptions here] off begin
		const questionObject = questions.find(q => { return q.questionType === this.getQuestionType(); });

		if (questionObject && questionObject.conditionOptions) {
			return questionObject.conditionOptions;
		}

		return [{ display: '', value: '' }];
		// % protected region % [Override getConditionOptions here] end
	}

	private static getValidatorOptions(question: Question): Array<ComboBoxOptions> {
		// % protected region % [Override getValidatorOptions here] off begin
		const questionObject = questions.find(q => { return q.questionType === question.questionType; });

		if (questionObject && questionObject.validatorOptions) {
			return questionObject.validatorOptions;
		}

		return [{ display: '', value: '' }];
		// % protected region % [Override getValidatorOptions here] end
	}

	private static getStylingOptions(question: Question): Array<ComboBoxOptions> {
		// % protected region % [Override getStylingOptions here] off begin
		const questionObject = questions.find(q => { return q.questionType === question.questionType; });

		if (questionObject && questionObject.stylingOptions) {
			return questionObject.stylingOptions;
		}

		return [{ display: '', value: '' }];
		// % protected region % [Override getStylingOptions here] end
	}

	private static getQuestionTypeClean(questionType: string): string {
		// % protected region % [Override getQuestionTypeClean here] off begin
		const questionName = questions.find(q => { return q.questionType === questionType; });

		return questionName ? questionName.displayName : 'Question';
		// % protected region % [Override getQuestionTypeClean here] end
	}

	@action
	public initConditions = (): void => {
		// % protected region % [Override initConditions here] off begin
		const { question } = this.props;

		if (question.options && !question.options.values) {
			question.options.values = [];
		}

		if (!question.showConditions) {
			question.showConditions = [];
		}
		// % protected region % [Override initConditions here] end
	};

	@action
	public initValidators = (): void => {
		// % protected region % [Override initValidators here] off begin
		const { question } = this.props;

		if (!question.validators) {
			question.validators = [];
		}
		// % protected region % [Override initValidators here] end
	};

	@action
	public addCondition = (): void => {
		// % protected region % [Override addCondition here] off begin
		const { question } = this.props;

		if (question.showConditions) {
			question.showConditions.push({
				path: '',
				condition: 'equal',
				value: '',
			});
		}
		// % protected region % [Override addCondition here] end
	};

	@action
	public addRadioOption = (): void => {
		// % protected region % [Override addRadioOption here] off begin
		const { question } = this.props;
		if (question.options) {
			question.options.values.push({
				value: '',
				id: uuid.v4(),
			});
		}
		// % protected region % [Override addRadioOption here] end
	};

	@action
	public addValidator = (): void => {
		// % protected region % [Override addValidator here] off begin
		const { question } = this.props;

		if (!question.validators) {
			question.validators = [];
		}

		question.validators.push({
			additionalData: '',
			path: question.id,
			validator: 'required',
		});
		// % protected region % [Override addValidator here] end
	};

	public removeCondition = (index: number): () => void => {
		// % protected region % [Override removeCondition here] off begin
		const { question } = this.props;

		return (): void => {
			runInAction(() => {
				if (question.showConditions) {
					question.showConditions.splice(index, 1);
				}
			});
		};
		// % protected region % [Override removeCondition here] end
	};

	public removeRadioOptions = (index: number) => {
		// % protected region % [Override removeRadioOptions here] off begin
		return (): void => {
			const { question } = this.props;

			runInAction(() => {
				if (question.options && question.options.values) {
					question.options.values.splice(index, 1);
				}
			});
		};
		// % protected region % [Override removeRadioOptions here] end
	};

	public removeValidator = (index: number): () => void => {
		// % protected region % [Override removeValidator here] off begin
		const { question } = this.props;

		return (): void => {
			runInAction(() => {
				if (question.validators) {
					question.validators.splice(index, 1);
				}
			});
		};
		// % protected region % [Override removeValidator here] end
	};

	// % protected region % [customize the TileOptions renderConditions function] off begin
	private renderConditions = (): ReactNode => {
		const { question, schema } = this.props;

		if (question.showConditions) {
			return question.showConditions.map((c, i) => {
				return (
					<React.Fragment key={i}>
						<Combobox
							model={c}
							modelProperty="path"
							label="Field"
							options={_.flatMap(schema, s => { return s.contents.map(q => { return { display: q.title, value: q.id }; }); })}
						/>

						<Combobox
							// Have to change key when options change as a change of options won't force a re-render
							key={JSON.stringify(this.getConditionOptions())}
							model={c}
							modelProperty="condition"
							label="Is"
							options={this.getConditionOptions()}
						/>

						{this.getQuestionType() === 'datetime' ? (
							<DateTimePicker
								model={c}
								modelProperty="value"
								labelVisible={false}
							/>
						)
							: <TextField model={c} modelProperty="value" />}

						<Button onClick={this.removeCondition(i)}>Remove Condition</Button>
					</React.Fragment>
				);
			});
		}

		return null;
	};
	// % protected region % [customize the TileOptions renderConditions function] end

	// % protected region % [customize the TileOptions renderRadioOptions function] off begin
	private renderRadioOptions = (): ReactNode => {
		const { question } = this.props;

		const radioOptions = question.options && question.options.values
			? question.options.values.map((model: { id: string; value: string }, i: number) => {
				return (
					<React.Fragment key={model.id}>
						<TextField model={model} modelProperty="value" label={`Option ${i + 1}`} />
						<Button onClick={this.removeRadioOptions(i)}>Remove Radio Option</Button>
					</React.Fragment>
				);
			})
			: <></>;
		return (
			<>
				{radioOptions}
			</>
		);
	};
	// % protected region % [customize the TileOptions renderRadioOptions function] end

	// % protected region % [customize the TileOptions renderStylingOptions function] off begin
	private renderStylingOptions(): ReactNode {
		const { question } = this.props;

		return (
			<>
				<Combobox
					model={question}
					label="Style"
					modelProperty="className"
					options={TileOptions.getStylingOptions(question)}
				/>
			</>
		);
	}
	// % protected region % [customize the TileOptions renderStylingOptions function] end

	// % protected region % [customize the TileOptions renderValidators function] off begin
	private renderValidators = (): ReactNode => {
		const { question } = this.props;

		if (question.validators) {
			return question.validators.map((v, i) => {
				return (
					<React.Fragment key={i}>
						<h4>Validator</h4>

						<Combobox
							key={JSON.stringify(TileOptions.getValidatorOptions(question))}
							model={v}
							modelProperty="validator"
							label="Validation Type"
							options={TileOptions.getValidatorOptions(question)}
						/>

						{getAdditionalAttributeElement(v)}

						<Button onClick={this.removeValidator(i)}>Remove Validator</Button>
					</React.Fragment>
				);
			});
		}

		return null;
	};
	// % protected region % [customize the TileOptions renderValidators function] end

	// % protected region % [customize the TileOptions render function] off begin
	public render(): ReactNode {
		const {
			question, hasShowConditions, hasValidators, hasOptions, hasStylingOptions, children, hasTooltip
		} = this.props;

		const questionTypeTitle = `${TileOptions.getQuestionTypeClean(question.questionType)} name`;

		return (
			<>
				<TextField model={question} modelProperty="title" label={questionTypeTitle} />

				{hasTooltip && <TextField model={question} modelProperty="toolTip" label="Tool Tip" />}

				{hasShowConditions && this.renderConditions()}

				{hasShowConditions && <Button className="btn--primary btn--outline" onClick={this.addCondition}>Add new condition</Button>}

				{hasStylingOptions && this.renderStylingOptions()}

				{hasOptions && this.renderRadioOptions()}

				{hasOptions && <Button className="btn--primary btn--outline" onClick={this.addRadioOption}>Add new option</Button>}

				{hasValidators && this.renderValidators()}

				{hasValidators && TileOptions.getValidatorOptions(question).length && <Button className="btn--primary btn--outline" onClick={this.addValidator}>Add new validator</Button>}

				{children && children}
			</>
		);
	}
	// % protected region % [customize the TileOptions render function] end
}
