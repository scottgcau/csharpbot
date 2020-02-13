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
import { Route, RouteComponentProps, Switch, Redirect } from 'react-router';
import * as Pages from './Pages';
import Logout from "./Components/Logout/Logout";
import Auth from "./Components/Auth/Auth";
import PageNotFound from './Pages/PageNotFound';
import Topbar from "./Components/Topbar/Topbar";
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

export default class Frontend extends React.Component<RouteComponentProps> {
	public render() {
		const path = this.props.match.path === '/' ? '' : this.props.match.path;
		return (
			<>
				<div className="body-container">
					<Topbar currentLocation="frontend" />
					<div className="frontend">
						{
						// % protected region % [Add any header content here] off begin
						}
						{
						// % protected region % [Add any header content here] end
						}
						<>
							<Switch>
								{/* Public routes */}
								<Route path="/login" component={Pages.LoginPage} />
								<Route path="/register" component={Pages.RegistrationPage} />
								<Route path="/confirm-email" component={Pages.RegistrationConfirmPage} />
								<Route path="/reset-password-request" component={Pages.ResetPasswordRequestPage} />
								<Route path="/reset-password" component={Pages.ResetPasswordPage} />
								<Route path="/logout" component={Logout} />
								<Route path={"/home"} component={Pages.HomePage} />
								<Redirect exact={true} from={`/`} to={`${path}/home`} />

								<Auth {...this.props}>
									<Switch>
										{/* These routes require a login to view */}

										{/* Pages from the ui model */}
										{
										// % protected region % [Add any extra page routes here] off begin
										}
										{
										// % protected region % [Add any extra page routes here] end
										}

										<Route component={PageNotFound} />
									</Switch>
								</Auth>
							</Switch>
						</>
						{
						// % protected region % [Add any footer content here] off begin
						}
						{
						// % protected region % [Add any footer content here] end
						}
					</div>
				</div>
			</>
		);
	}
}