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
import {Condition, Form} from "../Schema/Question";
import {CompareText} from "./CompareText";
import {CompareNumber} from "./CompareNumber";
import {CompareBoolean} from "./CompareBoolean";
import _ from 'lodash';

export function CheckDisplayConditions<T>(condition: Condition, Model: T, schema: Form) : boolean {
	let conditionalValue = Model[condition.path];
	let conditionalQuestion = _.flatMap(schema, x => x.contents).find(q => q.id == condition.path);

	if (conditionalQuestion !== undefined){
		switch (conditionalQuestion.questionType) {
			case "text":
				return CompareText(condition, conditionalValue);
			case "number":
				return CompareNumber(condition, conditionalValue);
			case "checkbox":
				return CompareBoolean(condition, conditionalValue);
			default:
				return false;
		}
	}
	return false;
}