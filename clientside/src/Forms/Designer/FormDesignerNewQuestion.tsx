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
import * as uuid from 'uuid';
import { action, computed } from 'mobx';
import { observer } from 'mobx-react';
import { contextMenu } from 'react-contexify';
import { Form, QuestionType, Slide } from '../Schema/Question';
import { ContextMenu, IContextMenuItemProps } from 'Views/Components/ContextMenu/ContextMenu';
import { Button } from 'Views/Components/Button/Button';
import { FormDesignerState } from 'Forms/Designer/FormSlideBuilder';
import { getQuestionType, questions } from 'Forms/Questions/QuestionUtils';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface FormDesignerNewQuestionProps {
	schema: Form;
	slide: Slide;
	designerState: FormDesignerState;
	// % protected region % [Add extra props here] off begin
	// % protected region % [Add extra props here] end
}

@observer
export class FormDesignerNewQuestion extends React.Component<FormDesignerNewQuestionProps> {
	private componentId = uuid.v4();

	// % protected region % [Add extra methods here] off begin
	// % protected region % [Add extra methods here] end

	// % protected region % [Customize contextMenuItems method here] off begin
	@computed
	private get contextMenuItems(): IContextMenuItemProps[] {
		return questions.map(q => ({
			label: q.displayName,
			onClick: this.onNewComponentClicked(q.questionType),
		})).sort((q1, q2) => {
			if (q1.label > q2.label) return 1;
			if (q1.label < q2.label) return -1;
			return 0;
		});
	}
	// % protected region % [Customize contextMenuItems method here] end

	// % protected region % [Customize render method here] off begin
	public render() {
		return (
			<div className="form-designer-add-question">
				<Button icon={{icon: 'plus', iconPos: 'icon-left'}} onClick={this.onClick}>
					Add a new question
				</Button>
				<ContextMenu actions={this.contextMenuItems} menuId={'forms-menu' + this.componentId} />
			</div>
		);
	}
	// % protected region % [Customize render method here] end

	// % protected region % [Customize onClick method here] off begin
	private onClick: React.MouseEventHandler = (event) => {
		contextMenu.show({
			id: 'forms-menu' + this.componentId,
			event: event,
		});
	};
	// % protected region % [Customize onClick method here] end

	// % protected region % [Customize onNewComponentClicked method here] off begin
	private onNewComponentClicked = (questionType: QuestionType) => action(() => {
		const { slide } = this.props;
		const question = getQuestionType(questionType);

		slide.contents.push({
			id: uuid.v4(),
			title: `New ${question ? question.displayName : questionType}`,
			questionType: questionType,
			options: {},
		})
	});
	// % protected region % [Customize onNewComponentClicked method here] end
}