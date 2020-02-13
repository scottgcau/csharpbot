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
import * as React from "react";
import { observer } from 'mobx-react';
import { action, observable } from 'mobx';
import _ from 'lodash';
import classNames from 'classnames';
import InputWrapper from '../Inputs/InputWrapper';
import ReactSelect from 'react-select';
import AsyncReactSelect from 'react-select/lib/Async';
import { ActionMeta, ValueType } from 'react-select/lib/types';
import { Props } from 'react-select/lib/Select';
import { StylesConfig, styleFn, Styles } from 'react-select/lib/styles';
import { DisplayType } from '../Models/Enums';
import { getComboboxStyles } from './Styles';

export interface IComboboxProps<T, I> {
	/** The model to change the attribute of */
	model: T;
	/** The property on the model to change */
	modelProperty: string;
	/** 
	 * The options on the dropdown 
	 * Can either be an array of JSON objects, or a function that returns an promise resolving to an array
	 */
	options: I[] | ((input: string | string[]) => Promise<I[]>);
	/** 
	 * A function that gets the label from the options object 
	 * The default fetches the 'display' key from the object
	 */
	getOptionLabel?: (option: I) => string;
	/** 
	 * A function that gets the value from the options object
	 * The default fetches the 'value' key from the object
	 */
	getOptionValue?: (option: I) => any;
	/**
	 * A function to compare the model property to the selected option
	 * @param modelProperty The model property to compare
	 * @param option The option from the combobox
	 */
	optionEqualFunc?: (modelProperty: any, option: string) => boolean;
	/** The to display around the combobox */
	label: string;
	/** Weather the label is visible */
	labelVisible?: boolean;
	/** The tooltip to display */
	tooltip?: string;
	/** The display type to use */
	displayType?: DisplayType;
	/** The classname for to combobox */
	className?: string;
	/** Raw props that are passed through to the react-select component */
	inputProps?: Props<I>;
	/** The placeholder text when the combobox is empty */
	placeholder?: string;
	/** A list of errors that are to be displayed around the combobox */
	errors?: string | string[];
	/** If the combobox is searchable */
	searchable?: boolean;
	/** The minimum length of search string with can be searched, default to 1 */
	minSearchLength?: number;
	/** If the combobox is isDisabled */
	isDisabled?: boolean;
	/** If the field is required */
	isRequired?: boolean;
	/** Override of the onChange function. Using this will remove the model binding logic of the component */
	onChange?: (value: ValueType<I>, action: ActionMeta) => void;
	/** Action to perform after the onChange method is called */
	onAfterChange?: (value: ValueType<I>, action: ActionMeta) => void;
	/** The emotion styles to use on this component */
	styles: StylesConfig;
	/* Is the select value clearable */
	isClearable?: boolean;
}

type MaybeArray<I> = I | ReadonlyArray<I>;
function isSingle<I>(input: MaybeArray<I>) : input is I {
	return !Array.isArray(input);
}

/**
 * A Combobox is a view that allows for the selection of elements from a dropdown menu
 */
@observer
export class Combobox<T, I> extends React.Component<IComboboxProps<T, I>, any> {
	static defaultProps = {
		getOptionLabel: (options: any) => options['display'],
		getOptionValue: (options: any) => options['value'],
		styles: {},
		labelVisible: true,
		minSearchLength: 1
	};

	@observable
	private options: I[] = [];

	@observable
	private selectedItem?: I | null;

	private isAsync: boolean;

	constructor(props: IComboboxProps<T, I>, context: any) {
		super(props, context);
		this.isAsync = typeof(this.props.options) === 'function';
		if(!this.isAsync){
			this.setOptions(this.props.options as I[]);
		}else{
			this.populateOptions('');
		}

		if(this.props.model[this.props.modelProperty]){
			const modelProperty = this.props.model[this.props.modelProperty];
			if(this.options && this.options.length){
				this.selectedItem = this.options.find(option => {
					if (Array.isArray(modelProperty)) {
						return modelProperty.indexOf(this.getOptionValue(option)) > -1;
					}
					return this.getOptionValue(option) == this.props.model[this.props.modelProperty];
				});
			}
		}
	}

	public render() {
		let selectedItem: I | I[] | undefined;
		const modelProperty = this.props.model[this.props.modelProperty];

		if (modelProperty) {
			if (this.props.inputProps && this.props.inputProps.isMulti && Array.isArray(modelProperty)) {
				selectedItem = this.options.filter(option => {
					return _.some(modelProperty, modelProp => this.optionsEqual(modelProp, option));
				});
			} else {
				// If there is a value already selected in the model then we want that one to be selected
				selectedItem = this.options.find(option => {
					return this.optionsEqual(modelProperty, option);
				});
			}
		}

		const styles = getComboboxStyles(this.props.styles);
		let component;
		if (this.isAsync) {
			component = <AsyncReactSelect
				value={selectedItem}
				options={this.options}
				getOptionLabel={this.getOptionLabel}
				getOptionValue={this.getOptionValue}
				name={this.props.modelProperty}
				onChange={this.onChange}
				placeholder={this.props.placeholder}
				isSearchable={this.props.searchable}
				isDisabled={this.props.isDisabled}
				loadOptions={this.populateOptions}
				styles={styles}
				isClearable={this.props.isClearable}
				classNamePrefix="dropdown"
				className="dropdown__container"
				{...this.props.inputProps} />;
		} else {
			component = <ReactSelect
				value={selectedItem}
				options={this.options}
				getOptionLabel={this.getOptionLabel}
				getOptionValue={this.getOptionValue}
				name={this.props.modelProperty}
				onChange={this.onChange}
				placeholder={this.props.placeholder}
				isSearchable={this.props.searchable}
				isDisabled={this.props.isDisabled}
				styles={styles}
				isClearable={this.props.isClearable}
				classNamePrefix="dropdown"
				className="dropdown__container"
				{...this.props.inputProps} />;
		}

		return (
			<InputWrapper 
				className={classNames('input-group__dropdown', this.props.className)}
				label={this.props.label}
				errors={this.props.errors}
				labelVisible={this.props.labelVisible}
				tooltip={this.props.tooltip}
				displayType={this.props.displayType}
				inputName={this.props.modelProperty}
				isRequired={this.props.isRequired}>
				{component}
			</InputWrapper>
		);
	}

	private optionsEqual(modelProp: any, option: I) {
		if (this.props.optionEqualFunc) {
			return this.props.optionEqualFunc(modelProp, this.getOptionValue(option));
		}
		return this.getOptionValue(option) == modelProp;
	}

	private populateOptions = (input: string) => {
		if (typeof (this.props.options) === 'function') {
			if(!input && !!this.props.model[this.props.modelProperty]){
				return this.props.options(this.props.model[this.props.modelProperty])
					.then(data => {
						this.setOptions(data);
						return data;
					})
					.catch(error => {
						console.warn('Error fetching data from options', error)
					});
			}

			if(!!input && input.length >= (this.props.minSearchLength || 1)){
				return this.props.options(input)
					.then(data => {
						this.setOptions(data);
						return data;
					})
					.catch(error => {
						console.warn('Error fetching data from options', error)
					});
			}

			return new Promise<Array<I>>((resolve) => {
				this.setOptions([])
				resolve(this.options);
			})
		} else {
			return new Promise<Array<I>>((resolve) => {
				this.setOptions(this.options)
				resolve(this.options);
			})
		}
	}

	@action
	private setOptions = (options: I[]) => {
		this.options = options;
	}

	private getOptionLabel = (option: I) => {
		if (this.props.getOptionLabel) {
			return this.props.getOptionLabel(option);
		}
		return option['display'];
	}

	private getOptionValue = (option: I) => {
		if (this.props.getOptionValue) {
			return this.props.getOptionValue(option);
		}
		return option['value'];
	}

	@action
	private onChange = (value: ValueType<I>, action: ActionMeta) => {
		// If the onChange is overwritten then we should just use that one instead
		if (this.props.onChange) {
			this.props.onChange(value, action);
		} else {
			if (value !== undefined && value !== null) {
				if (isSingle(value)) {
					this.props.model[this.props.modelProperty] = this.getOptionValue(value);
					this.selectedItem = value;
				} else {
					console.warn('Unexpected array return', value);
				}
			}
		}

		// If there is any logic to be done after the change of the combobox, do it here
		if (this.props.onAfterChange) {
			this.props.onAfterChange(value, action);
		}
	}
}