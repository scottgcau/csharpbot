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
import { computed } from "mobx";
import { observer } from 'mobx-react';
import { IQuestionTile, IQuestionTileProps, QuestionTileOptionsProps } from './QuestionTile';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { CheckDisplayConditions } from "../Conditions/ConditionUtils"
import { Form, Question, QuestionType } from 'Forms/Schema/Question';

@observer
export class TextQuestionTileOptions extends React.Component<QuestionTileOptionsProps> {
	public render() {
		return (
			<TextField model={this.props.question} modelProperty="title" label="Question Name" />
		);
	}
}

export interface ITextQuestionTileProps<T> extends IQuestionTileProps<T> {

}

@observer
export class TextQuestionTile<T> extends React.Component<ITextQuestionTileProps<T>> implements IQuestionTile {
	static displayName = 'Textbox';
	static questionType: QuestionType = 'text';
	static optionsMenu = TextQuestionTileOptions;

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
					<TextField
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