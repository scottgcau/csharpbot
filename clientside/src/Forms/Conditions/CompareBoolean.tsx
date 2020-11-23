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
import { Condition } from '../Schema/Question';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

function CompareBoolean(condition: Condition, conditionalValue: string): boolean {
	// % protected region % [Customize CompareBoolean conditions here] off begin
	// Cast the string representations to boolean
	const conditionValue = condition.value !== 'false';
	
	switch (condition.condition) {
		case 'equal':
			return Boolean(conditionalValue) === Boolean(conditionValue);
		default:
			return false;
	}
	// % protected region % [Customize CompareBoolean conditions here] end
}

export default CompareBoolean;
