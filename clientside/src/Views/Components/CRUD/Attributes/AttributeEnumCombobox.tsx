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
import { AttributeCRUDOptions } from '../../../../Models/CRUDOptions';
import { Combobox } from '../../Combobox/Combobox';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import { IAttributeProps } from './IAttributeProps';
import { Model } from 'Models/Model';

export interface IAttributeEnumComboboxProps<T extends Model> extends IAttributeProps<T> {
}

@observer
export default class AttributeEnumCombobox<T extends Model> extends React.Component<IAttributeEnumComboboxProps<T>> {
	@observable
	private enumState: 'loading' | 'done' = 'loading';

	@observable
	private options: Array<{display: string, value: string}> = [];
	
	public componentDidMount() {
		if (this.props.options.enumResolveFunction) {
			this.props.options.enumResolveFunction('')
				.then(options => this.setOptions(options));
		}
	}
	
	@action
	private async setOptions(options: Array<{display: string, value: string}>) {
		this.enumState = 'done';
		this.options = options;
	}
	
	public render() {
		if (this.enumState === 'loading') {
			return 'Loading enumeration';
		}
		return <Combobox
			model={this.props.model}
			label={this.props.options.name}
			options={this.options}
			modelProperty={this.props.options.attributeName}
			className={this.props.className}
			isDisabled={this.props.isReadonly}
			isRequired={this.props.isRequired}
			errors={this.props.errors}
			/>;
	}
}