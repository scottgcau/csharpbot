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
import _ from 'lodash';

import { Condition, Form } from '../Schema/Question';
import { questions } from '../Questions/QuestionUtils';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

function CheckDisplayConditions<T>(condition: Condition, Model: T, schema: Form): boolean {
	const conditionalValue: string = Model[condition.path];

	const conditionalQuestion = _.flatMap(schema, x => { return x.contents; }).find(q => { return q.id === condition.path; });

	// % protected region % [Customize CheckDisplayConditions conditions here] off begin
	if (conditionalQuestion !== undefined) {
		const questionObject = questions.find(q => { return q.questionType === conditionalQuestion.questionType; });
		if (questionObject && questionObject.compareFunction) {
			return questionObject.compareFunction(condition, conditionalValue);
		}

		return false;
	}

	return false;
	// % protected region % [Customize CheckDisplayConditions conditions here] end
}

export default CheckDisplayConditions;
