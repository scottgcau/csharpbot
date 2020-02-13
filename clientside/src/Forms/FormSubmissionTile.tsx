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
import { gql } from 'apollo-boost';
import { lowerCaseFirst } from 'Util/StringUtils';
import { observer } from 'mobx-react';
import { IConditionalFetchArgs, IModelType, Model } from 'Models/Model';
import { action, computed, observable } from 'mobx';
import { FormEntity } from 'Forms/FormEntity';
import { AccordionSection } from 'Views/Components/Accordion/Accordion';
import { store } from 'Models/Store';
import { getModelName } from 'Util/EntityUtils';
import { FormEntityTile } from 'Forms/FormEntityTile';
import Spinner from 'Views/Components/Spinner/Spinner';
import { Button } from 'Views/Components/Button/Button';

function getFormTileQuery(modelName: string) {
	return gql`query views($args: [WhereExpressionGraph]) {
		model: ${lowerCaseFirst(modelName)}FormTiles(where: $args) {
			id
			created
			modified
			formId
			tile
			form {
				id
				created
				modified
				name
				publishedVersion {
					id
					created
					modified
					formData
					version
				}
			}
		}
	}`;
}

export interface FormSubmissionTileProps {
	modelType: IModelType;
	tileId: string;
}

@observer
export class FormSubmissionTile extends React.Component<FormSubmissionTileProps> {
	@observable
	private requestState: 'pending' | 'error' | 'success' = 'pending';
	
	@observable
	private formState: 'before' | 'during' | 'after' = 'before';

	@observable
	private entity?: Model & FormEntity;

	@observable
	private error?: React.ReactNode;

	@computed
	private get requestArgs(): IConditionalFetchArgs<any> {
		return {
			args: [[{
				path: 'formId',
				comparison: 'equal',
				value: this.props.tileId
			}]]
		};
	}

	@action
	private updateFormSchema = (form?: any) => {
		if (form) {
			this.entity = form;
		}
		this.requestState = 'success';
	}

	@action
	private updateError = (error: any) => {
		console.error(error);
		this.error = (
			<div>
				There was an error fetching this form;
				<AccordionSection name="Detailed Errors" component={JSON.stringify(error)} key="form-errors"/>
			</div>
		);
		this.requestState = 'error';
	}
	
	@action
	private setFormState = (state: 'before' | 'during' | 'after') => {
		this.formState = state;
	}

	public componentDidMount(): void {
		store.apolloClient
			.query({
				query: getFormTileQuery(getModelName(this.props.modelType)),
				variables: {
					"args": [{"path": "tile", "comparison": "equal", "value": this.props.tileId}]
				},
				fetchPolicy: 'network-only',
			})
			.then(d => {
				if (d.data.model[0]) {
					return this.updateFormSchema(new this.props.modelType(d.data.model[0].form));
				}
				return this.updateFormSchema();
			})
			.catch(e => {
				this.updateError(e);
			})
	}

	private renderSuccess = () => {
		if (this.entity) {
			switch (this.formState) {
				case 'before': return (
					<>
						<h3>
							{this.entity.name}
						</h3>
						<Button onClick={() => this.setFormState('during')}>Open Form</Button>
					</>
				);
				case 'during': return <FormEntityTile model={this.entity} onAfterSubmit={() => this.setFormState('after')} />;
				case 'after': return <div>Thank you form submitting this form</div>;
			}
		}
		return (
			<div>
				There is no entity associated with this form tile
			</div>
		);
	}

	public render() {
		switch (this.requestState) {
			case 'pending': return <Spinner />;
			case 'error': return this.error;
			case 'success': return this.renderSuccess();
		}
	}
}