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
import { Switch, Route, RouteComponentProps } from 'react-router';
import { Model, IModelType } from 'Models/Model';
import EntityCollection from './EntityCollection';
import EntityAttributeList from './EntityAttributeList';
import EntityEdit from './EntityEdit';
import { getModelDisplayName } from 'Util/EntityUtils';
import SecuredAdminPage from '../Security/SecuredAdminPage';
import { SecurityService } from 'Services/SecurityService';
import { expandFn } from '../Collection/Collection';
import { IEntityContextMenuActions } from '../EntityContextMenu/EntityContextMenu';
import { EntityFormMode } from '../Helpers/Common';

interface IEntityCRUDProps<T extends Model> extends RouteComponentProps {
	modelType: IModelType;
	expandList?: expandFn<T>;
	perPage?: number;
	actionsMore?: IEntityContextMenuActions<T>;
}

@observer
class EntityCRUD<T extends Model> extends React.Component<IEntityCRUDProps<T>> {
	public render() {
		const { match } = this.props;

		// Wrap the pages with secured page component
		const entityCollectionPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canRead(this.props.modelType)}>
					<this.renderEntityCollection {...pageProps}/>
				</SecuredAdminPage>
			);
		};

		const entityCreatePage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canCreate(this.props.modelType)}>
					<this.renderEntityCreate {...pageProps}/>
				</SecuredAdminPage>
			);
		};

		const entityViewPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canRead(this.props.modelType)}>
					<this.renderEntityView {...pageProps}/>
				</SecuredAdminPage>
			);
		};

		const entityEditPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canUpdate(this.props.modelType)}>
					<this.renderEntityEdit {...pageProps}/>
				</SecuredAdminPage>
			);
		};

		return (
			<div>
				<Switch>
					<Route exact={true} path={`${match.url}`} render={entityCollectionPage} />
					<Route path={`${match.url}/view/:id`} render={entityViewPage} />
					<Route exact={true} path={`${match.url}/create`} render={entityCreatePage} />
					<Route path={`${match.url}/edit/:id`} render={entityEditPage} />
				</Switch>
			</div>
		);
	}

	protected renderEntityCollection = (routeProps: RouteComponentProps) => {
		return <EntityCollection
			{...routeProps}
			modelType={this.props.modelType}
			expandList={this.props.expandList}
			perPage={this.props.perPage}
			actionsMore={this.props.actionsMore}
		/>;
	}

	protected renderEntityCreate = (routeProps: RouteComponentProps) => {
		const modelDisplayName = getModelDisplayName(this.props.modelType);
		return <EntityAttributeList
			{...routeProps}
			model={new this.props.modelType()}
			sectionClassName="crud__create"
			title={`Create New ${modelDisplayName}`}
			formMode={EntityFormMode.CREATE}
			modelType={this.props.modelType}
		/>;
	}

	protected renderEntityEdit = (routeProps: RouteComponentProps) => {
		return <EntityEdit {...routeProps} modelType={this.props.modelType} formMode={EntityFormMode.EDIT} />;
	}

	protected renderEntityView = (routeProps: RouteComponentProps) => {
		return <EntityEdit {...routeProps} modelType={this.props.modelType} formMode={EntityFormMode.VIEW} />;
	}

}
export default EntityCRUD;