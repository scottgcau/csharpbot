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
import { IFilter } from '../CollectionFilterPanel';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import { IComboboxProps, Combobox } from '../../Combobox/Combobox';
import classnames from 'classnames';
import { MultiCombobox } from 'Views/Components/Combobox/MultiCombobox';

interface IFilterEnumComboBoxProps<T> extends Partial<IComboboxProps<T, { display: string, value: string }>> {
	filter: IFilter<T>;
	className?: string;
}

@observer
class FilterEnumComboBox<T> extends React.Component<IFilterEnumComboBoxProps<T>> {
	@observable
	private enumState: 'loading' | 'done' = 'loading';

	@observable
	private options: Array<{display: string, value: string}> = [];

	public componentDidMount() {
		if (this.props.filter.referenceResolveFunction) {
			this.props.filter.referenceResolveFunction('')
				.then(options => {
					this.setOptions(options)
				});
		}
	}
	
	@action
	private async setOptions(options: Array<{display: string, value: string}>) {
		this.enumState = 'done';
		this.options = options;
	}

	public render() {
		const { filter, className} = this.props;
		const classes = classnames('collection-filter-enum-combobox', className);

		if (this.enumState === 'loading') {
			return 'Loading enumeration';
		}

		return <MultiCombobox
			model={filter}
			modelProperty="value1"
			label={filter.displayName}
			className={classes}
			options={this.options}
			onAfterChange={(value, action) => {
				filter.active = !!filter.value1 && ((filter.value1 as string[]).length > 0);
				if (this.props.onAfterChange) {
					this.props.onAfterChange(value, action);
				}
			}}
			isClearable={true}
		/>;
	}
}

export default FilterEnumComboBox;