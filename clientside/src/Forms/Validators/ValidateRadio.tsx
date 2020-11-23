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
import { Validator } from '../Schema/Question';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export function ValidateRadio<T>(validator: Validator, model: T): string {
	const value = model[validator.path];
	switch (validator.validator) {
		case 'required':
			if (!value) {
				return 'This field is required\n';
			}
			return '';
		// % protected region % [Add any further cases here] off begin
		// % protected region % [Add any further cases here] end
		default:
			return 'A validator on this field is not implemented\n';
	}
}
