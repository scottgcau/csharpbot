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
import { DocumentNode } from 'graphql';
import { Model } from 'Models/Model';
import PaginationData, { PaginationQueryOptions } from 'Models/PaginationData';
import { getModelName, getFetchAllQuery } from 'Util/EntityUtils';
import { observer } from 'mobx-react';
import axios, { AxiosResponse } from 'axios';
import { observable, action } from 'mobx';
import { lowerCaseFirst } from 'Util/StringUtils';
import { modelName as modelNameSymbol } from 'Symbols';
import { IWhereCondition } from './ModelQuery';

type Comparators = 'contains' | 'endsWith' | 'equal' | 'greaterThan' | 'greaterThanOrEqual' | 'in' | 'notIn' | 'lessThan' | 'lessThanOrEqual' | 'like' | 'notEqual' | 'startsWith';

export interface IOrderByCondition<T> {
	path: string;
	descending?: boolean;
}

export interface IModelAPIQueryVariables<T> {
	skip?: number; 
	take?: number; 
	args?: Array<IWhereCondition<T>>;
	orderBy: Array<IOrderByCondition<T>>;
	ids?: string[];
}

export interface QueryResult {
	data: any;
	error?: string;
	success?: boolean;
	loading: boolean;
}

export interface IModelAPIQueryProps<T extends Model, TData = any> {
	children: (result: QueryResult) => React.ReactNode;
	model: {new(json?: {}): T};
	conditions?: Array<IWhereCondition<T>> | Array<Array<IWhereCondition<T>>>;
	ids?: string[];
	orderBy?: IOrderByCondition<T>;
	url: string;
	pagination: PaginationQueryOptions;
}

@observer
class ModelAPIQuery<T extends Model,TData = any> extends React.Component<IModelAPIQueryProps<T, TData>> {
	@observable
	private requestState: 'loading' | 'error' | 'done' = 'loading';

	private requestData: Array<T> | any;

	private requestError?: string;

	public componentDidMount = () => {
		const modelName: string = this.props.model[modelNameSymbol];
		const lowerModelName = lowerCaseFirst(modelName);
		const url = this.props.url || `/api/${lowerModelName}`;

		axios.get(url, {params: this.constructVariables()})
			.then(this.onSuccess)
			.catch(this.onError);
	}

	@action
	private onSuccess = (data: AxiosResponse) => {
		this.requestData = data.data;
		this.requestState = 'done';
	}

	@action
	private onError = (data: AxiosResponse) => {
		this.requestData = data.data;
		this.requestState = 'error';
		this.requestError = data.statusText;
	}

	public render() {
		const modelName = getModelName(this.props.model);
		return this.props.children({
			loading: this.requestState === 'loading', 
			success: this.requestState === 'done', 
			error: this.requestError,
			data: { 
				[`${lowerCaseFirst(modelName)}s`]: this.requestData,
				[`count${modelName}s`]: this.requestData ? (this.requestData instanceof Array ? this.requestData.length : 1) : 0
			},
		});
	}

	

	private constructVariables() {
		const { conditions, ids, orderBy : orderByProp, pagination } = this.props;
		const { page, perPage } = pagination;

		let orderBy: IOrderByCondition<T> = {path: new this.props.model().getDisplayAttribute(), descending: false};
		if (orderByProp) {
			orderBy = orderByProp;
		}

		return {
			skip: page * perPage, 
			take: perPage, 
			args: conditions,
			orderBy: [orderBy],
			ids,
		};
	}
}

export default ModelAPIQuery;