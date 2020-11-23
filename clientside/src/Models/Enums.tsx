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

export type scheduletype =
	// % protected region % [Override scheduletype keys here] off begin
	'PRESEASON' |
		'REGULAR' |
		'PLAYOFFS' |
		'REPRESENTATIVE';
	// % protected region % [Override scheduletype keys here] end

export const scheduletypeOptions: { [key in scheduletype]: string } = {
	// % protected region % [Override scheduletype display fields here] off begin
	PRESEASON: 'Preseason',
	REGULAR: 'Regular',
	PLAYOFFS: 'Playoffs',
	REPRESENTATIVE: 'Representative',
	// % protected region % [Override scheduletype display fields here] end
};

export type roletype =
	// % protected region % [Override roletype keys here] off begin
	'PLAYER' |
		'COACH' |
		'ASSISTANT' |
		'MANAGER';
	// % protected region % [Override roletype keys here] end

export const roletypeOptions: { [key in roletype]: string } = {
	// % protected region % [Override roletype display fields here] off begin
	PLAYER: 'Player',
	COACH: 'Coach',
	ASSISTANT: 'Assistant',
	MANAGER: 'Manager',
	// % protected region % [Override roletype display fields here] end
};

// % protected region % [Add any extra enums here] off begin
// % protected region % [Add any extra enums here] end
