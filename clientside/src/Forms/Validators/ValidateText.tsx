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
import { isEmail } from '../../Validators/Functions/Email';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

// Validation function for determining if the input is a phone number
export function isPhone(phone: string): boolean {
	if (phone) {
		return /^(\d{8}|\d{10}|\(\+{0,1}\d{2}\)\s*\d{3}\s*\d{3}\s*\d{3}|\(\+{0,1}\d{2}\)\s*\d{4}\s*\d{4})$/.test(phone);
	}
	
	return true;
}

export function ValidateText<T>(validator: Validator, model: T): string {
	const value = model[validator.path];
	switch (validator.validator) {
		case 'required':
			if (!value) {
				return 'This field is required\n';
			}
			return '';
		case 'email':
			if (value && !isEmail(value)) {
				return 'Please enter a valid email\n';
			}
			return '';
		case 'phone':
			if (!isPhone(value)) {
				return 'This field should be a valid phone number\n';
			}
			return '';
		case 'custom':
			if (value && validator.additionalData && !(new RegExp(validator.additionalData).test(value))) {
				return 'The given input is invalid\n';
			}
			return '';
		// % protected region % [Add any further cases here] off begin
		// % protected region % [Add any further cases here] end
		default:
			return 'A validator on this field is not implemented\n';
	}
}
