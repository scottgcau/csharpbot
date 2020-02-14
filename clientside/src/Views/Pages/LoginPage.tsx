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
import { RouteComponentProps } from 'react-router';
import { Redirect } from 'react-router';
import { Button, Display, Colors, Sizes } from '../Components/Button/Button';
import { action, observable, runInAction } from 'mobx';
import { TextField } from '../Components/TextBox/TextBox';
import { IUserResult, store } from 'Models/Store';
import axios from 'axios';
import * as queryString from 'querystring';
import { ButtonGroup, Alignment } from 'Views/Components/Button/ButtonGroup';
import { Password } from 'Views/Components/Password/Password';
import _ from 'lodash';
import { isEmail } from 'Validators/Functions/Email';
import alert from '../../Util/ToastifyUtils';
import { getErrorMessages } from 'Util/GraphQLUtils';
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

interface ILoginState {
	username: string;
	password: string;
	errors: { [attr: string]: string };
	// % protected region % [Add extra login state properties here] off begin
	// % protected region % [Add extra login state properties here] end
}

const defaultLoginState: ILoginState = {
	username: '',
	password: '',
	errors: {},
	// % protected region % [Instantiate extra login state properties here] off begin
	// % protected region % [Instantiate extra login state properties here] end
};

@observer
export default class LoginPage extends React.Component<RouteComponentProps> {
	@observable
	private loginState: ILoginState = defaultLoginState;

	public render() {
		let contents = null;

		if (store.loggedIn) {
			// % protected region % [Override redirect here] off begin
			return <Redirect to="/" />;
			// % protected region % [Override redirect here] end
		}

		// % protected region % [Override contents here] off begin
		contents = (
			<div className="body-content">
				<form className="login" onSubmit={this.onLoginClicked}>
					<h2>Login</h2>
					<TextField
						id="login_username"
						className="login-username"
						model={this.loginState}
						modelProperty="username"
						label="Email Address"
						inputProps={{ autoComplete: 'username', type: "email" }}
						isRequired={true}
						errors={this.loginState.errors['username']} />
					<Password
						id="login_password"
						className="login-password"
						model={this.loginState}
						modelProperty="password"
						label="Password"
						inputProps={{ autoComplete: "current-password" }}
						isRequired={true}
						errors={this.loginState.errors['password']} />
					<ButtonGroup alignment={Alignment.HORIZONTAL} className="login-buttons">
						<Button type='submit' className="login-submit" display={Display.Solid} sizes={Sizes.Medium} buttonProps={{ id: "login_submit" }}>Login</Button>
					</ButtonGroup>
					<p>
						<a className='link-forgotten-password link-rm-txt-dec' onClick={this.onForgottenPasswordClick}>Forgot your password? </a>
					</p>
				</form>
			</div>
		);
		// % protected region % [Override contents here] end
		return contents;
	}

	@action
	private onLoginClicked = (event: React.FormEvent<HTMLFormElement>) => {
		// % protected region % [Override onLoginClicked here] off begin
		event.preventDefault();

		this.loginState.errors = {};

		if (!this.loginState.username) {
			this.loginState.errors['username'] = "Email Address is required";
		} else if (!isEmail(this.loginState.username)) {
			this.loginState.errors['username'] = "This is not a valid email address";
		}
		if (!this.loginState.password) {
			this.loginState.errors['password'] = "Password is required";
		} else if (this.loginState.password.length < 6) {
			this.loginState.errors['password'] = "The minimum length of password is 6";
		}

		if (Object.keys(this.loginState.errors).length > 0) {
			return;
		} else {
			axios.post(
				'/api/authorization/login',
				{
					username: this.loginState.username,
					password: this.loginState.password,
				})
				.then(({ data }) => {
					this.onLoginSuccess(data);
				})
				.catch(response => {
					const errorMessages = getErrorMessages(response).map((error: any) => (<p>{error.message}</p>));
					alert(
						<div>
							<h6>Login failed</h6>
							{errorMessages}
						</div>,
						'error'
					);
				});
		}
		// % protected region % [Override onLoginClicked here] end
	};

	@action
	private onStartRegisterClicked = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		// % protected region % [Override onStartRegisterClicked here] off begin
		const { redirect } = queryString.parse(this.props.location.search.substring(1));
		store.routerHistory.push(`/register?${!!redirect ? `redirect=${redirect}` : ''}`);
		// % protected region % [Override onStartRegisterClicked here] end
	};

	@action
	private onLoginSuccess = (userResult: IUserResult) => {
		// % protected region % [Override login success logic here] off begin
		store.setLoggedInUser(userResult);

		const { redirect } = queryString.parse(this.props.location.search.substring(1));

		if (redirect && !Array.isArray(redirect)) {
			store.routerHistory.push(redirect);
		} else {
			store.routerHistory.push('/');
		}
		// % protected region % [Override login success logic here] end
	};

	@action
	private onForgottenPasswordClick = (e: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => {
		// % protected region % [Override onForgottenPasswordClick here] off begin
		store.routerHistory.push(`/reset-password-request`);
		// % protected region % [Override onForgottenPasswordClick here] end
	};

	// % protected region % [Add class methods here] off begin
	// % protected region % [Add class methods here] end
}