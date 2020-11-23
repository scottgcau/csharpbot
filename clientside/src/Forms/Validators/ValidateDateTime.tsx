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
import moment from 'moment';
import { Validator } from '../Schema/Question';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export function ValidateDateTime<T>(validator: Validator, model: T): string {
	const conditionalValue: string = model[validator.path];

	if (validator.validator === 'required') {
		if (!conditionalValue) {
			return 'This field is required\n';
		}
		return '';
	}
	
	if (!conditionalValue) {
		return '';
	}
	
	let since = null;
	
	if (validator.additionalData) {
		since = moment(validator.additionalData);
	}
	
	const valueDate = moment(conditionalValue);

	switch (validator.validator) {
		case 'past':
			if (valueDate.isAfter(moment())) {
				return 'This date time needs to be in the past\n';
			}
			return '';
		case 'future':
			if (valueDate.isBefore(moment())) {
				return 'This date time needs to be in the future\n';
			}
			return '';
		case 'after':
			if (!since) {
				return '';
			}
			
			if (valueDate.isBefore(since)) {
				return `This date time needs to be after ${since.format('LLL')}\n`;
			}
			return '';
		case 'before':
			if (!since) {
				return '';
			}
			
			if (valueDate.isAfter(since)) {
				return `This date time needs to be before ${since.format('LLL')}\n`;
			}
			return '';
		// % protected region % [Add any further cases here] off begin
		// % protected region % [Add any further cases here] end
		default:
			return 'A validator on this field is not implemented\n';
	}
}
