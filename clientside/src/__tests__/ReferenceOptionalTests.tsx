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
import * as Models from '../Models/Entities';
import { Model } from '../Models/Model';

/**
 * Tests cases for validating the source optional = false property of a reference 
 */
test.each([
	['leagues', 'sport', 'League', 'Sport', new Models.League(), 'oneToMany'],
])(
	'Source Optional for the %s %s relation between %s and %s',
	(...args: Array<string | Model>) => {
		expect.assertions(1);
		const [name, oppositeName, source, target, model, referenceType] = args;

		return (model as Model).validate()
			.then(result => {
				if (referenceType === 'manyToMany') {
					expect(result).toMatchObject({
						[oppositeName + 's']: {
							errors: { length: ['The length of this field is not greater than 1. Actual Length: 0'] }
						}
					});
				} else {
					expect(result).toMatchObject({
						[oppositeName + 'Id']: {
							errors: { required: ['This field is required'] }
						}
					});
				}
			})
	});

