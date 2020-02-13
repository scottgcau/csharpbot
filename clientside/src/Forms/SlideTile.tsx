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
import classNames from 'classnames';
import { observer } from 'mobx-react';
import { Form, Question, Slide } from './Schema/Question';
import { getQuestion } from './Questions/QuestionUtils';

export interface ISlideTileProps<T> extends Slide {
	model: T
	schema: Form;
	isReadOnly?: boolean;
	className?: string;
	disableShowConditions?: boolean;
	beforeQuestionContent?: (question: Question, slide: Slide, index: number) => React.ReactNode;
	afterQuestionContent?: (question: Question, slide: Slide, index: number) => React.ReactNode;
	beforeSlideContent?: (slide: Slide) => React.ReactNode;
	afterSlideContent?: (slide: Slide) => React.ReactNode;
}

@observer
export class SlideTile<T> extends React.Component<ISlideTileProps<T>> {
	public render() {
		return (
			<div className={classNames('form-slide', this.props.className)}>
				{this.props.beforeSlideContent ? this.props.beforeSlideContent(this.props) : undefined}
				<h3>{this.props.name}</h3>
				{this.props.contents.map((question, i) => (
					<div className="form__question-container" id={question.id} key={question.id}>
						{this.props.beforeQuestionContent ? this.props.beforeQuestionContent(question, this.props, i) : undefined}
						{getQuestion({
							schema: this.props.schema,
							model: this.props.model,
							isReadOnly: this.props.isReadOnly,
							disableShowConditions: this.props.disableShowConditions,
							...question
						})}
						{this.props.afterQuestionContent ? this.props.afterQuestionContent(question, this.props, i) : undefined}
					</div>
				))}
				{this.props.afterSlideContent ? this.props.afterSlideContent(this.props) : undefined}
			</div>
		);
	}
}