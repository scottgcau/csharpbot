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
import { observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import _ from 'lodash';
import classNames from 'classnames';

import { Button } from 'Views/Components/Button/Button';
import If from 'Views/Components/If/If';
import { Form, Question } from './Schema/Question';
import { SlideTile } from './SlideTile';
import { buildValidationErrorMessage } from './Validators/ValidationUtils';
import CheckDisplayConditions from './Conditions/ConditionUtils';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IFormProps<T> {
	isReadOnly?: boolean;
	className?: string;
	submitText?: string;
	disableShowConditions? : boolean;
	// % protected region % [Add extra form tile props here] off begin
	// % protected region % [Add extra form tile props here] end
}

export interface IFormTileProps<T> extends IFormProps<T> {
	schema: Form;
	model: T;
	onSubmit?: (model: T) => void;
	// % protected region % [Add to IFormTileProps here] off begin
	// % protected region % [Add to IFormTileProps here] end
}

@observer
export class FormTile<T> extends Component<IFormTileProps<T>> {
	@observable
	private reValidate: boolean = false;
	
	static defaultProps: Partial<IFormProps<{}>> = {
		submitText: 'Submit',
		// % protected region % [Add to defaultProps here] off begin
		// % protected region % [Add to defaultProps here] end
	};

	// % protected region % [Add extra class fields here] off begin
	// % protected region % [Add extra class fields here] end

	private onSubmit = (): void => {
		// % protected region % [Override onSubmit here] off begin
		const { onSubmit, model } = this.props;
	
		if (onSubmit) {
			if (this.validateForm()) {
				onSubmit(model);
			} else {
				runInAction((): void => {
					this.reValidate = true;
				});
			}
		}
		// % protected region % [Override onSubmit here] end
	};

	private isQuestionShown = (question: Question): boolean => {
		// % protected region % [Override isQuestionShown here] off begin
		const { showConditions } = question;
		const { disableShowConditions, model, schema } = this.props;
		
		if (showConditions !== undefined && !disableShowConditions) {
			return showConditions.every(condition => { return CheckDisplayConditions(condition, model, schema); });
		}
		
		return true;
		// % protected region % [Override isQuestionShown here] end
	};

	private validateQuestion = (question: Question): boolean => {
		// % protected region % [Override validateQuestion here] off begin
		const { model, schema } = this.props;
		const { validators } = question;
		let errorMessage: string[] = [];

		if (this.isQuestionShown(question)) {
			if (question.validators !== undefined) {
				errorMessage = buildValidationErrorMessage(validators, model, schema, false);
				return _.filter(errorMessage, e => { return e; }).length === 0;
			}
		}
		
		return true;
		// % protected region % [Override validateQuestion here] end
	};

	private validateQuestions = (questions: Question[]): boolean => {
		// % protected region % [Override validateQuestions here] off begin
		let valid: boolean = true;
		
		questions.forEach(q => {
			valid = valid && this.validateQuestion(q);
		});
		
		return valid;
		// % protected region % [Override validateQuestions here] end
	};

	private validateForm = (): boolean => {
		// % protected region % [Override validateForm here] off begin
		const { schema } = this.props;
		const questions = _.flatMap(schema, o => { return o.contents; });
		
		return this.validateQuestions(questions);
		// % protected region % [Override validateForm here] end
	};

	public render(): ReactNode {
		// % protected region % [Customize render here] off begin
		const {
			className, schema, isReadOnly, disableShowConditions, submitText, onSubmit, model, 
		} = this.props;
		
		return (
			<div className={classNames('forms-tile', className)}>
				{schema.map((slide, i): ReactNode => {
					const key = `${slide.name}-${i}`;
					
					return (
						<SlideTile
							key={key}
							model={model}
							schema={schema}
							isReadOnly={isReadOnly}
							disableShowConditions={disableShowConditions}
							contents={slide.contents}
							name={slide.name}
							reValidate={this.reValidate}
						/>
					); 
				})}
				
				<If condition={onSubmit !== undefined}>
					<Button onClick={this.onSubmit}>{submitText}</Button>
				</If>
			</div>
		);
		// % protected region % [Customize render here] end
	}

	// % protected region % [Add any extra form tile methods here] off begin
	// % protected region % [Add any extra form tile methods here] end
}