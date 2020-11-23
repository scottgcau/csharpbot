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

export function ValidateNumber<T>(validator: Validator, model: T): string {
	// % protected region % [Customize validate number here] off begin
	const value = model[validator.path] as number;

	if (validator.validator === 'required') {
		return value ? '' : 'This field is required\n';
	}

	if (value && validator.additionalData) {
		const additionalData = parseFloat(validator.additionalData);
		if (isNaN(additionalData)) {
			return '';
		}

		switch (validator.validator) {
			case 'equalNumber':
				return value === additionalData ? '' : `The value ${value} does not equal ${additionalData}`;
			case 'notEqualNumber':
				return value !== additionalData ? '' : `The value ${value} equals invalid value ${additionalData}`;
			case 'greaterThan':
				return value > additionalData ? '' : `The value ${value} is not greater than ${additionalData}`;
			case 'greaterThanOrEqual':
				return value >= additionalData ? '' : `The value ${value} is not greater than or equal to ${additionalData}`;
			case 'lessThan':
				return value < additionalData ? '' : `The value ${value} is not less than ${additionalData}`;
			case 'lessThanOrEqual':
				return value <= additionalData ? '' : `The value ${value} is not less than or equal to ${additionalData}`;
			default:
				return 'A validator on this field is not implemented\n';
		}
	}

	return '';
	// % protected region % [Customize validate number here] end
}

// % protected region % [Add extra methods here] off begin
// % protected region % [Add extra methods here] end