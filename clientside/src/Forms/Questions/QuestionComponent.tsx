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
import { IQuestionTileProps } from 'Forms/Questions/QuestionTile';
import React from 'react';
import { Condition, QuestionType, Validator, ValidatorType } from 'Forms/Schema/Question';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface QuestionComponentProps<T> extends IQuestionTileProps<T> {
	checkValidation: (...args: any[]) => void;
	// % protected region % [Add any extra question props here] off begin
	// % protected region % [Add any extra question props here] end
}

/**
 * The superclass for forms question components.
 * This class only exists to provide types for the static variables required by questions.
 */
export class QuestionComponent<T, P extends QuestionComponentProps<T>, S = any> extends React.Component<P, S> {
	/**
	 * The name to display on the user interface for the question
	 */
	public static displayName: string;
	/**
	 * The type of the question that is encoded in the form
	 */
	public static questionType: QuestionType;
	/**
	 * The options menu for the question
	 */
	public static optionsMenu: typeof React.Component;
	/**
	 * The function used by the conditional logic to compare this questions result with a string
	 */
	public static compareFunction?: (condition: Condition, conditionalValue: string) => boolean;
	/**
	 * The function used to apply validators to the question
	 */
	public static validateFunction?: <T>(validator: Validator, model: T) => string;
	/**
	 * The types of conditions that can be applied to this question
	 */
	public static conditionOptions?: { display: string, value: string }[];
	/**
	 * The styling options for this question
	 */
	public static stylingOptions?: { display: string, value: string }[];
	/**
	 * The validator options for this question
	 */
	public static validatorOptions?: { display: string, value: ValidatorType }[];

	// % protected region % [Add any extra question members here] off begin
	// % protected region % [Add any extra question members here] end
}

// % protected region % [Add any extra methods here] off begin
// % protected region % [Add any extra methods here] end