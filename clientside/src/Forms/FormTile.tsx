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
import { observer } from 'mobx-react';
import { Form } from './Schema/Question';
import { SlideTile } from './SlideTile';
import { Button } from 'Views/Components/Button/Button';
import If from 'Views/Components/If/If';
import classNames from 'classnames';

export interface IFormProps<T> {
	isReadOnly?: boolean;
	className?: string;
	submitText?: string;
	disableShowConditions? : boolean
}

export interface IFormTileProps<T> extends IFormProps<T> {
	schema: Form;
	model: T;
	onSubmit?: (model: T) => void;
}

@observer
export class FormTile<T> extends React.Component<IFormTileProps<T>> {
	static defaultProps: Partial<IFormProps<{}>> = {
		submitText: 'Submit',
	};

	private onSubmit = () => {
		if (this.props.onSubmit) {
			this.props.onSubmit(this.props.model);
		}
	}

	public render() {
		return (
			<div className={classNames('forms-tile', this.props.className)}>
				{this.props.schema.map((slide, i) => (
					<SlideTile
						key={i}
						model={this.props.model}
						schema={this.props.schema}
						isReadOnly={this.props.isReadOnly}
						disableShowConditions={this.props.disableShowConditions}
						{...slide} />
				))}
				<If condition={this.props.onSubmit !== undefined}>
					<Button onClick={this.onSubmit}>{this.props.submitText}</Button>
				</If>
			</div>
		);
	}
}