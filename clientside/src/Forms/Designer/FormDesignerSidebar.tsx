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
import classNames from 'classnames';
import { action, computed } from 'mobx';
import { observer } from 'mobx-react';
import { Button, Display } from 'Views/Components/Button/Button';
import { Form, Slide } from '../Schema/Question';
import { getQuestionType } from 'Forms/Questions/QuestionUtils';
import AccordionGroup, {IAccordionConfig} from "Views/Components/Accordion/Accordion";
import { ContextMenu, IContextMenuItemProps } from 'Views/Components/ContextMenu/ContextMenu';
import { contextMenu } from 'react-contexify';
import { MenuItemEventHandler } from 'react-contexify/src/types/index';
import { TextField } from 'Views/Components/TextBox/TextBox';
import { confirmModal } from 'Views/Components/Modal/ModalUtils';
import { FormDesignerState } from 'Forms/Designer/FormSlideBuilder';

@observer
export class FormDesignerSidebar extends React.Component<{schema: Form, designerState: FormDesignerState}> {
	private menuId = uuid.v4();

	@computed
	private get accordionCollection(): IAccordionConfig[] {
		return this.props.schema.map((slide, i) => {
			const contents = slide.contents.map(question => (
				<li className={question.id} key={question.id}>{question.title}</li>
			));
			return {
				name: slide.name,
				key: i.toString(),
				className: classNames(slide.name, "slide-" + i),
				afterTitleContent: (
					<>
						<Button
							labelVisible={false}
							onClick={this.onContextMenuClick(this.menuId, slide, i)}
							icon={{icon: 'more-horizontal', iconPos: 'icon-left'}}>
							More
						</Button>
					</>
				),
				disabled: contents.length === 0,
				component: <ol>{contents}</ol>,
			}
		});
	}

	@computed
	private get closeButton() {
		return <Button className="close-sidebar" display={Display.Solid} onClick={this.props.designerState.reset}>Close</Button>;
	}

	private onContextMenuClick = (menuId: string, slide: Slide, slideIndex: number) => (event: React.MouseEvent) =>
		contextMenu.show({
			id: menuId,
			event: event,
			props: {slideIndex: slideIndex, slide: slide}
		});

	@action
	private onNewSlide = () => {
		this.props.schema.push({name: "New Slide", contents: []});
	}

	@action
	private duplicateSlide = (args: MenuItemEventHandler) => {
		if (args.props) {
			this.props.designerState.reset();
			const slideClone = JSON.parse(JSON.stringify(this.props.schema[args.props['slideIndex']])) as Slide;

			// Reassign the ids of the cloned questions
			for (const question of slideClone.contents) {
				question.id = uuid.v4();
			}

			this.props.schema.push(slideClone);
		}
	}

	@action
	private deleteSlideConfirm = (args: MenuItemEventHandler) => {
		confirmModal('Confirm', 'Do you want to delete this slide')
			.then(() => {
				if (args.props) {
					this.deleteSlide(args.props['slideIndex']);
				}
			});
	}

	@action
	private deleteSlide = (slideIndex: number) => {
		this.props.designerState.reset();
		this.props.schema.splice(slideIndex, 1);
	}

	@action
	private editSlide = (args: MenuItemEventHandler) => {
		if (args.props) {
			this.props.designerState.selectedSlide = args.props['slide'];
			this.props.designerState.mode = 'edit-slide';
		}
	}

	@action
	private moveSlideUp = (args: MenuItemEventHandler) => {
		if (args.props) {
			const slideIndex = args.props['slideIndex'] as number;
			if (slideIndex > 0) {
				this.props.designerState.reset();
				const slide = this.props.schema.splice(slideIndex, 1);
				this.props.schema.splice(slideIndex - 1, 0, ...slide);
			}
		}
	}

	@action
	private moveSlideDown = (args: MenuItemEventHandler) => {
		if (args.props) {
			const slideIndex = args.props['slideIndex'] as number;
			if (slideIndex < this.props.schema.length - 1) {
				this.props.designerState.reset();
				const slide = this.props.schema.splice(slideIndex, 1);
				this.props.schema.splice(slideIndex + 1, 0, ...slide);
			}
		}
	}

	public render() {
		switch (this.props.designerState.mode) {
			case 'view': return this.renderViewMode();
			case 'edit': return this.renderEditMode();
			case 'edit-slide': return this.renderSlideEditMode();
		}
	}

	private renderViewMode = () => {
		const contextMenuActions: IContextMenuItemProps[] = [
			{label: 'Edit Slide', onClick: this.editSlide},
			{label: 'Delete', onClick: this.deleteSlideConfirm},
			{label: 'Duplicate', onClick: this.duplicateSlide},
			{label: 'Move Slide Up', onClick: this.moveSlideUp},
			{label: 'Move Slide Down', onClick: this.moveSlideDown},
		];
		return (
			<div className="slide-builder__list slide-builder__list--view-slide">
				<ContextMenu location="admin" menuId={this.menuId} actions={contextMenuActions} />
				<AccordionGroup accordions={this.accordionCollection} />
				<Button
					display={Display.Text}
					icon={{icon: 'plus', iconPos: 'icon-left'}}
					onClick={this.onNewSlide}>
					Add a new slide
				</Button>
			</div>
		);
	};

	private renderEditMode = () => {
		const { selectedQuestion } = this.props.designerState;
		if (selectedQuestion) {
			const questionTile = getQuestionType(selectedQuestion.questionType);
			if (questionTile) {
				return (
					<div className="slide-builder__list slide-builder__list--edit-question">
						<questionTile.optionsMenu schema={this.props.schema} question={selectedQuestion}/>
						{this.closeButton}
					</div>
				);
			}
			return this.renderViewMode();
		}
		return this.closeButton;
	}

	private renderSlideEditMode = () => {
		const { selectedSlide } = this.props.designerState;
		if (selectedSlide) {
			return (
				<div className="slide-builder__list slide-builder__list--edit-slide">
					<TextField label="Slide Name" model={selectedSlide} modelProperty="name" />
					{this.closeButton}
				</div>
			);
		}
		return this.closeButton;
	}
}