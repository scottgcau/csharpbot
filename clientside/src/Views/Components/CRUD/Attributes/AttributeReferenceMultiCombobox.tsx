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
import * as React from 'react';
import { Model } from 'Models/Model';
import { AttributeCRUDOptions } from 'Models/CRUDOptions';
import { getFetchAllQuery, getModelName } from 'Util/EntityUtils';
import { lowerCaseFirst } from 'Util/StringUtils';
import { observer } from 'mobx-react';
import { action, computed, observable, runInAction } from 'mobx';
import { MultiCombobox } from '../../Combobox/MultiCombobox';
import { store } from '../../../../Models/Store';
import Spinner from '../../Spinner/Spinner';
import _ from 'lodash';
import { ActionMeta, ValueType } from 'react-select/lib/types';
import AwesomeDebouncePromise from 'awesome-debounce-promise';
import { IAttributeProps } from './IAttributeProps';

export type dropdownData = Array<{ display: string, value: any }>;
type state = 'loading' | 'error' | 'success';

export interface IAttributeReferenceComboboxProps<T extends Model> extends IAttributeProps<T> {
	/** A function that returns the type of model that is on the combobox */
	referenceType: { new(json?: {}): Model };
	/** A function to override loading of the data into the dropdown */
	referenceResolveFunction?: (search: string | string[], options: {model: T}) => Promise<dropdownData>;
	/** Should data be loaded immediately on placing this element into the DOM */
	synchronousLoad?: boolean;
	/** A function to compare an option value with a value */
	optionEqualFunc?: (modelProperty: Model, option: string) => boolean;
	/** Is the entity in this combobox a join entity */
	isJoinEntity?: boolean;
	/** Can default options be removed from the combobox */
	disableDefaultOptionRemoval?: boolean;
}

/**
 * A dropdown menu that populates itself with references to other objects
 */
@observer
export default class AttributeReferenceMultiCombobox<T extends Model> extends React.Component<IAttributeReferenceComboboxProps<T>> {
	static defaultProps: Partial<IAttributeReferenceComboboxProps<Model>> = {
		optionEqualFunc: (modelProperty, option) => modelProperty.id === option,
	};

	@observable
	public requestState: {state: state, data?: dropdownData} = {state: 'loading'};

	@observable
	public options: T[] = [];

	@computed
	private get modelName() {
		const modelName = getModelName(this.props.referenceType);
		return lowerCaseFirst(modelName) + "s";
	}

	@computed
	private get joinProp() {
		return this.props.options.attributeName.substring(0, this.props.options.attributeName.length - 1);
	}
	
	private defaultOptions: Model[] = [];

	@computed
	public get resolveFunc() {
		if (this.props.referenceResolveFunction) {
			return _.partial(this.props.referenceResolveFunction, _, {model: this.props.model});
		}

		const query = getFetchAllQuery(this.props.referenceType);
		return () => store.apolloClient.query({query: query, fetchPolicy: 'network-only'})
			.then((data) => {
				const associatedObjects: any[] = data[this.modelName];
				let comboOptions: dropdownData = [];
				if (associatedObjects) {
					comboOptions = associatedObjects.map(obj => new this.props.referenceType(obj))
						.map(obj => ({ display: obj.getDisplayName(), value: obj.id }));
				}
				return comboOptions;
			});
	}
	
	constructor(props: IAttributeReferenceComboboxProps<T>, context: any) {
		super(props, context);
		
		if (props.disableDefaultOptionRemoval) {
			this.defaultOptions = props.model[props.options.attributeName];
		}
	}

	public mutateOptions = (query: string | string[]) => {
		const { isJoinEntity } = this.props;
		return this.resolveFunc(query)
			.then(e => {
				runInAction(() => {
					this.options = _.unionBy(
						this.options,
						this.props.model[this.props.options.attributeName],
						e.map(x => x.value),
						isJoinEntity ? this.joinProp + 'Id' : 'id',
					);
				});
				return e.map(x => {
					const option = {
						display: isJoinEntity ? x.value[this.joinProp].getDisplayName() : x.value.getDisplayName(),
						value: isJoinEntity ? x.value[this.joinProp + 'Id'] : x.value.id,
						isFixed: false,
					};
					
					if (this.props.disableDefaultOptionRemoval) {
						if (_.find(this.defaultOptions, x.value)) {
							option.isFixed = true;
						}
					}
					
					return option;
				});
			});
	}

	public getOptions = () => {
		return AwesomeDebouncePromise(this.mutateOptions, 500);
	}

	@action
	private updateData = (data: dropdownData) => {
		this.requestState = {
			state: 'success',
			data,
		};
	}

	@action
	errorData = () => {
		this.requestState = {
			state: 'error',
		};
	}

	public componentDidMount() {
		if (this.props.synchronousLoad) {
			this.resolveFunc('')
				.then(this.updateData)
				.catch(this.errorData);
		}
	}

	public render() {
		if (this.props.synchronousLoad) {
			if (this.requestState.data && this.requestState.state === 'success') {
				return <MultiCombobox
					label={this.props.options.displayName}
					model={this.props.model}
					modelProperty={this.props.options.attributeName}
					options={this.requestState.data}
					errors={this.props.errors}
					optionEqualFunc={this.props.optionEqualFunc}
					className={this.props.className}
					isDisabled={this.props.isReadonly}
					isRequired={this.props.isRequired}
					inputProps={{
						noOptionsMessage: input => input.inputValue.length
							? 'No options found'
							: 'Start typing to search for entities',
						formatOptionLabel: option => <span data-id={option.value}>{option.display}</span>,
					}} />;
			} else if (this.requestState.state === 'error') {
				return (
					<div>
						<span>{this.props.options.attributeName}: </span>
						<span>Error loading reference data from server</span>
					</div>
				);
			}
			return <Spinner />;
		}

		return <MultiCombobox
			label={this.props.options.displayName}
			model={this.props.model}
			modelProperty={this.props.options.attributeName}
			options={this.getOptions()}
			errors={this.props.errors}
			optionEqualFunc={this.props.optionEqualFunc}
			onChange={this.onChange}
			className={this.props.className}
			isDisabled={this.props.isReadonly}
			onAfterChange={this.props.onAfterChange}
			isRequired={this.props.isRequired}
			inputProps={{
				noOptionsMessage: input => input.inputValue.length
					? 'No options found'
					: 'Start typing to search for entities',
				formatOptionLabel: option => <span data-id={option.value}>{option.display}</span>,
			}} />;
	}

	@action
	private onChange = (value: ValueType<{display: string, value: string, isFixed?: boolean}>, action: ActionMeta) => {
		if (action.action === 'remove-value' || action.action === 'pop-value') {
			const item = action['removedValue'];
			if (item && item.isFixed) {
				return;
			}
		}

		if (action.action === 'clear') {
			this.props.model[this.props.options.attributeName] = this.defaultOptions;
			return;
		}

		if (Array.isArray(value)) {
			if (this.props.isJoinEntity) {
				this.props.model[this.props.options.attributeName] = this.options
					.filter(x => value.map(v => v.value).find(v => v == x[this.joinProp + 'Id']));
			} else {
				this.props.model[this.props.options.attributeName] = this.options
					.filter(x => value.map(v => v.value).find(v => v == x.id));
			}
		} else {
			console.warn('Unexpected non array return value in multi combobox');
		}
	}
}