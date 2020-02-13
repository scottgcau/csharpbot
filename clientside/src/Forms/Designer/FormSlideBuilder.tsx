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
import * as React from 'react'
import classNames from 'classnames';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import { FormVersion } from '../FormVersion';
import { Question, Slide } from '../Schema/Question';
import { FormDesignerContents } from './FormDesignerContents';
import { FormDesignerSidebar } from './FormDesignerSidebar';

export interface FormSlideBuilderProps {
	formVersion: FormVersion;
	className?: string;
}

export class FormDesignerState {
	@observable
	public mode: 'view' | 'edit' | 'edit-slide' = 'view';
	@observable
	public selectedQuestion?: Question;
	@observable
	public selectedSlide?: Slide;

	@action
	public reset = () => {
		this.mode = 'view';
		this.selectedQuestion = undefined;
		this.selectedSlide = undefined;
	}
}

@observer
export class FormSlideBuilder extends React.Component<FormSlideBuilderProps> {
	@observable
	private submissionData = {};

	@observable
	private designerState = new FormDesignerState();

	public render() {
		return (
			<section aria-label="slide-builder" className="slide-builder">
				<FormDesignerSidebar schema={this.props.formVersion.formData} designerState={this.designerState} />
				<FormDesignerContents schema={this.props.formVersion.formData} model={this.submissionData} designerState={this.designerState}/>
			</section>
		);
	}
}