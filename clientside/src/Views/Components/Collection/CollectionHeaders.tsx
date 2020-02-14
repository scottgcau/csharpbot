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
import * as React from 'react';
import { observer } from 'mobx-react';
import { Checkbox } from '../Checkbox/Checkbox';
import If from '../If/If';
import { ICollectionItemActionProps, actionFilterFn } from './Collection';
import { observable, runInAction } from 'mobx';
import { IOrderByCondition } from '../ModelCollection/ModelQuery';
import { DisplayType } from '../Models/Enums';

type nameFn = (name: string) => (string | React.ReactNode);
type transformFn<T> = (item: T, name: string) => (string | React.ReactNode);

export interface ICollectionHeaderProps<T> {
	name: string;
	displayName: string | nameFn;
	sortable?: boolean;
	transformItem?: transformFn<T>;
	nullValue?: string;
	sortClicked?: (event: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>) => IOrderByCondition<T> | undefined | void;
}

export interface ICollectionHeaderPropsPrivate<T> extends ICollectionHeaderProps<T> {
	headerName?: string | React.ReactNode;
}

export interface ICollectionHeadersProps<T> {
	headers: Array<ICollectionHeaderPropsPrivate<T>>;
	actions?: Array<ICollectionItemActionProps<T>> | actionFilterFn<T>;
	selectableItems?: boolean;
	allChecked: boolean;
	onCheckedAll?: (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => void
	/** The default order by condition */
	orderBy?: IOrderByCondition<T> | undefined;
}

@observer
export default class CollectionHeaders<T> extends React.Component<ICollectionHeadersProps<T>> {
	@observable
	private orderBy: IOrderByCondition<T> | undefined | void;

	constructor(props: ICollectionHeadersProps<T>, context: any){
		super(props, context);
		this.orderBy = this.props.orderBy;
	}
	
	public render() {
		return (
			<thead>
				<tr className="list__header">
					<If condition={this.props.selectableItems}>
						<th className="select-box">
							<Checkbox 
								label="Select All" 
								modelProperty="checked" 
								name="selectall"
								model={{}}
								displayType={DisplayType.INLINE}
								inputProps={{
									checked: this.props.allChecked,
									onChange: event => {
										runInAction(() => {
											if (this.props.onCheckedAll) {
												this.props.onCheckedAll(event, event.target.checked);
											}
										});
									}
								}}
							/>
						</th>
					</If>
					{this.props.headers.map((header, idx) => {
						return (
							<th key={idx} scope="col" onClick={
								(event) => {
									runInAction(() => {
										if (header.sortClicked) {
											this.orderBy = header.sortClicked(event);
										}
									});
								}
							} 
								className={header.sortable ? ((!this.orderBy || this.orderBy.path !== header.name) ? 'sortable' : (this.orderBy.descending ? "sortable--des" : "sortable--asc")) : ''}>
								{header.headerName ? header.headerName : `Column ${idx}`}
							</th>
						);
					})}
					<If condition={this.props.actions != null}>
						<th scope="col" className="list__header--actions">Commands</th>
					</If>
				</tr>
			</thead>
		);
	}
}