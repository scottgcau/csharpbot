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

import { Condition } from '../Schema/Question';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

function CompareDateTime(condition: Condition, conditionalValue: string): boolean {
	// % protected region % [Customize CompareDateTime conditions here] off begin
	if (conditionalValue) {
		switch (condition.condition) {
			case 'equal':
				return moment(conditionalValue).isSame(moment(condition.value));
			case 'greaterThanOrEqual':
				return (moment(conditionalValue)).isSameOrAfter(moment(condition.value));
			case 'lessThan':
				return (moment(conditionalValue)).isBefore(moment(condition.value));
			case 'lessThanOrEqual':
				return (moment(conditionalValue)).isSameOrBefore(moment(condition.value));
			case 'notEqual':
				return !moment(conditionalValue).isSame(moment(condition.value));
			case 'greaterThan':
				return (moment(conditionalValue)).isAfter(moment(condition.value));
			default:
				return false;
		}
	}
	
	return false;
	// % protected region % [Customize CompareDateTime conditions here] end
}

export default CompareDateTime;
