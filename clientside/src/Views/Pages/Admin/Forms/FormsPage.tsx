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
import {Route, RouteComponentProps, Switch} from "react-router";
import FormsBuildPage from "Views/Pages/Admin/Forms/FormsBuildPage";
import FormsLandingPage from "Views/Pages/Admin/Forms/FormsLandingPage";

export default class FormsPage extends React.Component<RouteComponentProps>{
	public render(){
		const { match } = this.props;

		const formsBuildPage = (pageProps: RouteComponentProps) => {
			return (
					<FormsBuildPage {...pageProps}/>
			);
		};

		const formsLandingPage = (pageProps: RouteComponentProps) => {
			return (
				<FormsLandingPage {...pageProps}/>
			);
		};

		return (
			<div>
				<Switch>
					<Route exact={true} path={`${match.url}`} render={formsLandingPage} />
					<Route path={`${match.url}/build/:entity/:id`} render={formsBuildPage} />
					<Route exact={true} path={`${match.url}/build`} render={formsBuildPage} />
				</Switch>
			</div>
		);
	}
}