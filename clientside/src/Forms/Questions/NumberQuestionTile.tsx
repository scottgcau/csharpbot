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
import { computed } from "mobx";
import { observer } from 'mobx-react';
import { IQuestionTile, IQuestionTileProps, QuestionTileOptionsProps } from './QuestionTile';
import { CheckDisplayConditions } from "../Conditions/ConditionUtils"
import { NumberTextField } from 'Views/Components/NumberTextBox/NumberTextBox';
import { Form, Question, QuestionType } from 'Forms/Schema/Question';
import { TextField } from 'Views/Components/TextBox/TextBox';

@observer
export class NumberQuestionTileOptions extends React.Component<QuestionTileOptionsProps> {
	public render() {
		return (
			<TextField model={this.props.question} modelProperty="title" label="Question Name" />
		);
	}
}

export interface INumberQuestionTileProps<T> extends IQuestionTileProps<T> {

}

@observer
export class NumberQuestionTile<T> extends React.Component<INumberQuestionTileProps<T>> implements IQuestionTile {
	static displayName = 'Number';
	static questionType: QuestionType = 'number';
	static optionsMenu = NumberQuestionTileOptions;

	@computed
	public get isConditionSatisfied() {
		if (this.props.showConditions !== undefined && !this.props.disableShowConditions) {
			return this.props.showConditions.every(condition => CheckDisplayConditions(condition, this.props.model, this.props.schema));
		}
		return true;
	};

	public render() {
		if (this.isConditionSatisfied) {
			return (
				<>
					<p className="question__content">{this.props.title}</p>
					<NumberTextField
						model={this.props.model}
						modelProperty={this.props.id}
						label={this.props.title}
						labelVisible={false}
						isReadOnly={this.props.isReadOnly} />
				</>
			);
		} else {
			return null
		}
	}
}