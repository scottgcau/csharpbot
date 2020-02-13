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
import uuid from 'uuid';
import { action } from 'mobx';
import { observer } from 'mobx-react';
import { contextMenu } from 'react-contexify';
import { Form, QuestionType, Slide } from '../Schema/Question';
import { ContextMenu, IContextMenuItemProps } from 'Views/Components/ContextMenu/ContextMenu';
import { Button } from 'Views/Components/Button/Button';
import { FormDesignerState } from 'Forms/Designer/FormSlideBuilder';
import { questions } from 'Forms/Questions/QuestionUtils';

@observer
export class FormDesignerNewQuestion extends React.Component<{schema: Form, slide: Slide, designerState: FormDesignerState}> {
	private componentId = uuid.v4();

	public render() {
		const contextMenuItems: IContextMenuItemProps[] = questions.map(q => ({
			label: q.displayName,
			onClick: this.onNewComponentClicked(q.questionType),
		}));

		return (
			<div className="form-designer-add-question">
				<Button icon={{icon: 'plus', iconPos: 'icon-left'}} onClick={this.onClick}>
					Add a new question
				</Button>
				<ContextMenu location="admin" actions={contextMenuItems} menuId={'forms-menu' + this.componentId} />
			</div>
		);
	}

	private onClick: React.MouseEventHandler = (event) => {
		contextMenu.show({
			id: 'forms-menu' + this.componentId,
			event: event,
		});
	}

	private onNewComponentClicked = (questionType: QuestionType) => () => this.addQuestion(questionType);

	@action
	private addQuestion = (questionType: QuestionType) => {
		this.props.slide.contents.push({
			id: uuid.v4(),
			title: `New ${questionType}`,
			questionType: questionType
		})
	}
}