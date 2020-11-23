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
import * as Models from '../Models/Entities';
import { Model } from '../Models/Model';

// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end


/**
 * Tests cases for validating the source optional = false property of a reference
 */
test.each([
	['games', 'schedule', 'GameEntity', 'ScheduleEntity', new Models.GameEntity(), 'oneToMany'],
	['rosterassignments', 'person', 'RosterassignmentEntity', 'PersonEntity', new Models.RosterassignmentEntity(), 'oneToMany'],
	['rosters', 'season', 'RosterEntity', 'SeasonEntity', new Models.RosterEntity(), 'oneToMany'],
	['schedules', 'season', 'ScheduleEntity', 'SeasonEntity', new Models.ScheduleEntity(), 'oneToMany'],
	['seasons', 'league', 'SeasonEntity', 'LeagueEntity', new Models.SeasonEntity(), 'oneToMany'],
	['leagues', 'sport', 'LeagueEntity', 'SportEntity', new Models.LeagueEntity(), 'oneToMany'],
	['formPage', 'form', 'ScheduleEntityFormTileEntity', 'ScheduleEntity', new Models.ScheduleEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'SeasonEntityFormTileEntity', 'SeasonEntity', new Models.SeasonEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'VenueEntityFormTileEntity', 'VenueEntity', new Models.VenueEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'GameEntityFormTileEntity', 'GameEntity', new Models.GameEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'SportEntityFormTileEntity', 'SportEntity', new Models.SportEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'LeagueEntityFormTileEntity', 'LeagueEntity', new Models.LeagueEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'TeamEntityFormTileEntity', 'TeamEntity', new Models.TeamEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'PersonEntityFormTileEntity', 'PersonEntity', new Models.PersonEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'RosterEntityFormTileEntity', 'RosterEntity', new Models.RosterEntityFormTileEntity(), 'oneToMany'],
	['formPage', 'form', 'RosterassignmentEntityFormTileEntity', 'RosterassignmentEntity', new Models.RosterassignmentEntityFormTileEntity(), 'oneToMany'],
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

/**
 * Tests cases for validating the target optional = false property of a reference
 */
test.each([
	['rosterassignments', 'roster', 'RosterassignmentEntity', 'RosterEntity', new Models.RosterEntity()],
])(
	'Target Optional for the %s %s relation between %s and %s',
	(...args: Array<string | Model>) => {
		expect.assertions(1);
		const [name, oppositeName, source, target, model] = args;

		return (model as Model).validate()
		.then(result => {
			expect(result).toMatchObject({
				[name + 's']: {
					errors: { length: ['The length of this field is not greater than 1. Actual Length: 0'] }
				}
			});
		})
	});


// % protected region % [Add any extra reference optional tests here] off begin
// % protected region % [Add any extra reference optional tests here] end
