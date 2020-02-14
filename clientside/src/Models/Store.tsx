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
import { History } from 'history';
import { default as ApolloClient } from 'apollo-boost';
import { action, computed, observable } from 'mobx';
import { IGlobalModal } from '../Views/Components/Modal/GlobalModal';
// % protected region % [Add any extra store imports here] off begin
// % protected region % [Add any extra store imports here] end

export interface IGroupResult {
	name: string;
	hasBackendAccess: boolean;
}

export interface IUserResult {
	id: string;
	email: string;
	groups: IGroupResult[];
}

/**
 * A global singleton store that contains a global state of data
 */
export class Store {
	@observable
	private user?: IUserResult;

	/**
	 * The current location in the application
	 */
	@observable
	public appLocation: 'frontend' | 'admin' = 'frontend';

	/**
	 * The router history object for React Router
	 */
	public routerHistory: History;

	/**
	 * The client for Apollo
	 */
	public apolloClient: ApolloClient<{}>;

	/**
	 * The global modal that is stored in the app and can be called imperatively
	 */
	public modal: IGlobalModal;

	/**
	 * This signifies weather we are logged in or not
	 * Only ever set this value to true if there is a value set in this.token
	 */
	@computed
	public get loggedIn() {
		return this.user !== undefined; 
	}

	/**
	 * The user Id of the logged-in user
	 */
	@computed
	public get userId(): string | undefined {
		return this.user ? this.user.id : undefined;
	};
	
	/**
	 * The email of the current logged in user
	 */
	@computed
	public get email(): string | undefined {
		return this.user ? this.user.email : undefined;
	}

	/**
	 * The groups that the logged in user are a part of
	 */
	@computed
	public get userGroups(): IGroupResult[] {
		if (this.user) {
			return [...this.user.groups];
		}
		return [];
	};

	/**
	 * Does this user have access to the backend admin views
	 */
	@computed
	public get hasBackendAccess() {
		if (this.user) {
			return this.user.groups.some(ug => ug.hasBackendAccess);
		}
		return false;
	};

	/**
	 * Is the frontend in edit mode
	 */
	@observable
	public frontendEditMode = false;

	/**
	 * Sets the current logged in user in the store
	 * @param userResult
	 */
	@action
	public setLoggedInUser(userResult: IUserResult) {
		this.user = userResult;
	}
	
	/**
	 * Clears the logged in user data from the store
	 */
	@action clearLoggedInUser() {
		this.user = undefined;
	}

	// % protected region % [Add any extra store methods or properties here] off begin
	// % protected region % [Add any extra store methods or properties here] end
}

export const store = new Store();