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
import { observer } from 'mobx-react';
import { ActionMeta, ValueType } from 'react-select/lib/types';
import { action } from 'mobx';
import { Combobox, IComboboxProps } from './Combobox';

export interface IMultiComboboxProps<T, I> extends IComboboxProps<T, I> { }

/**
 * A MultiCombobox is a view that allows allows selection of many elements from a dropdown menu
 */
@observer
export class MultiCombobox<T, I> extends React.Component<IMultiComboboxProps<T, I>, {}> {
	static defaultProps = {
		styles: {}
	}

	public render() {
		return (
			<Combobox
				getOptionValue={this.getOptionValue}
				{...this.props}
				inputProps={{
					isMulti: true,
					...this.props.inputProps,
				}}
				onChange={this.onChange} />
		);
	}

	private getOptionValue = (option: I) => {
		if (this.props.getOptionValue) {
			return this.props.getOptionValue(option);
		}
		return option['value'];
	}

	private isArray(input: ValueType<I>): input is I[] {
		return Array.isArray(input);
	}

	@action
	private onChange = (value: ValueType<I>, action: ActionMeta) => {
		// If the onChange is overwritten then we should just use that one instead
		if (this.props.onChange) {
			return this.props.onChange(value, action);
		}

		if (this.isArray(value)) {
			this.props.model[this.props.modelProperty] = value.map(option => this.getOptionValue(option));
		} else {
			console.warn("Unexpected non array return value in multi combobox");
		}

		// If there is any logic to be done after the change of the combobox, do it here
		if (this.props.onAfterChange) {
			this.props.onAfterChange(value, action);
		}
	}
}