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
import React, { Component, ReactNode } from 'react';
import { observable, computed, action } from 'mobx';
import { observer } from 'mobx-react';

import { Form, Question, ShowConditions, Validators } from '../Schema/Question';
import CheckDisplayConditions from '../Conditions/ConditionUtils';
import { buildValidationErrorMessage } from '../Validators/ValidationUtils';
import { QuestionComponent } from 'Forms/Questions/QuestionComponent';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface QuestionTileOptionsProps {
	schema: Form;
	question: Question;
	// % protected region % [Add extra question tile options props here] off begin
	// % protected region % [Add extra question tile options props here] end
}

export interface IQuestionTile {
	// % protected region % [Add extra question tile methods here] off begin
	// % protected region % [Add extra question tile methods here] end
}

export interface IQuestionTileProps<T> extends Question {
	model: T;
	schema: Form;
	isReadOnly?: boolean;
	disableShowConditions?: boolean;
	title: string;
	validators?: Validators;
	toolTip?: string;
	showConditions?: ShowConditions;
	reValidate?: boolean;
	selectedQuestion?: typeof QuestionComponent;
	// % protected region % [Add extra question tile props here] off begin
	// % protected region % [Add extra question tile props here] end
}

// % protected region % [Add any further Question Tile exports] off begin
@observer
export class QuestionTile<T> extends Component<IQuestionTileProps<T>> {
	// Used for checking if any custom show logic
	// Currently there is no way to set show logic in the UI
	@observable
	private validationErrors: string[] = [];
	
	@computed
	public get isConditionSatisfied(): boolean {
		const {
			showConditions, disableShowConditions, model, schema,
		} = this.props;

		if (showConditions !== undefined && !disableShowConditions) {
			return showConditions.every(condition => { return CheckDisplayConditions(condition, model, schema); });
		}
		
		return true;
	}

	@computed
	public get validationError() {
		const {
			validators, model, schema, disableValidation, 
		} = this.props;
		
		return buildValidationErrorMessage(validators, model, schema, disableValidation);
	}
	
	protected getErrorMessages(): string[] {
		const { reValidate } = this.props;

		if (reValidate && (this.validationErrors !== [])) {
			return this.validationError;
		} 
		
		return this.validationErrors;
	}

	@action
	public isValidationSatisfied = (): void => {
		const {
			validators, model, schema, disableValidation,
		} = this.props;

		this.validationErrors = buildValidationErrorMessage(validators, model, schema, disableValidation);
	};

	public render(): ReactNode {
		const errors: string[] = this.getErrorMessages();
		const SelectedQuestion = this.props.selectedQuestion;

		if (this.isConditionSatisfied) {
			return (
				<>
					{SelectedQuestion && <SelectedQuestion checkValidation={this.isValidationSatisfied} {...this.props} />}
					{errors.length !== 0 ? <p className="question__error">{errors.join('\n')}</p> : '' }
				</>
			);
		}
		
		return <></>;
	}
}
