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
import { Combobox } from '../../Combobox/Combobox';
import { observer } from 'mobx-react';
import { IAttributeProps } from './IAttributeProps';
import { Model } from 'Models/Model';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IFormTile {
	formModel: string;
	value: string;
	display:string;
}

export const formTiles = [
	// % protected region % [Add any extra form tiles here] off begin
	// % protected region % [Add any extra form tiles here] end
];

export interface AttributeFormTileProps<T extends Model> extends IAttributeProps<T> {
	// % protected region % [Add any custom props here] off begin
	// % protected region % [Add any custom props here] end
}

@observer
export default class AttributeFormTile<T extends Model> extends React.Component<AttributeFormTileProps<T>> {
	public render() {
		return <Combobox
			model={this.props.model}
			label={this.props.options.name}
			options={this.props.options.formTileFilterFn
				? formTiles.filter(this.props.options.formTileFilterFn) 
				: formTiles}
			modelProperty={this.props.options.attributeName}
			className={this.props.className}
			isDisabled={this.props.isReadonly}
			isRequired={this.props.isRequired}
			errors={this.props.errors} />;
	}
}