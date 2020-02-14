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
import * as Models from 'Models/Entities';
import SecuredPage from 'Views/Components/Security/SecuredPage';
import Spinner from 'Views/Components/Spinner/Spinner';
import { RouteComponentProps } from 'react-router';
import { action, computed, observable } from 'mobx';
import { observer } from 'mobx-react';
import { FormEntityData } from 'Forms/FormEntityData';
import { Model } from 'Models/Model';
import { FormEntityDesigner } from 'Forms/Designer/FormEntityDesigner';
import {store} from "Models/Store";

type FormEntityModel = Model & FormEntityData;

@observer
export default class FormsBuildPage extends React.Component<RouteComponentProps>{
	@computed
	private get formEntity(): string {
		return this.props.match.params['entity'];
	}

	@computed
	private get versionId(): string {
		return this.props.match.params['id'];
	}

	@observable
	private entity: FormEntityModel;

	@observable
	private loadingState: 'loading' | 'error' | 'done' = 'loading';

	@observable
	private errors: object;

	@action
	private updateFormSchema = (entity?: FormEntityModel, errors?: object) => {
		if (entity) {
			this.entity = entity;
			this.loadingState = 'done';
		} else {
			this.errors = errors ? errors : {};
			this.loadingState = 'error';
		}
	};

	public componentDidMount(): void {
		const modelKey = Object.keys(Models).find(x => x.toLowerCase() === this.formEntity.toLowerCase());
		if (modelKey){
			Models[modelKey]
				.fetch<FormEntityModel>({ids: [this.versionId]})
				.then((d: FormEntityModel[]) => {
					this.updateFormSchema(d[0]);
				})
				.catch((e: any) => {
					this.updateFormSchema(undefined, e);
				});
		} else {
			store.routerHistory.push("/404")
		}
	}

	private renderContents = () => {
		switch (this.loadingState) {
			case 'loading': return <Spinner/>;
			case 'error': return 'Something went wrong with loading the data. ' + JSON.stringify(this.errors);
			case 'done': return <FormEntityDesigner form={this.entity} returnRoute="/admin/forms" />;
		}
	}

	public render() {
		return (
			<SecuredPage>
				<div className="body-content">
					{this.renderContents()}
				</div>
			</SecuredPage>
		);
	}
}
