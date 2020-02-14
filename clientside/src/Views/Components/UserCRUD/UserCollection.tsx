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
import Collection from '../Collection/Collection';
import { Button } from '../Button/Button';
import { observer } from 'mobx-react';
import { getModelName } from 'Util/EntityUtils';
import EntityCollection from '../CRUD/EntityCollection';
import User from 'Models/Entities/User';
import { getUsers } from './UserService';
import { observable, action, runInAction } from 'mobx';

@observer
class UserCollection extends EntityCollection<User> {
	@observable
	private users: User[] = [];
	public componentDidMount() {
		getUsers()
			.then(this.setUsers)
			.catch(() => console.log("An error occurred fetching users"));
	}

	public render() {
		const { modelType } = this.props;
		const modelName = getModelName(modelType);
		const tableHeaders = this.getHeaders().map(header => {return {...header, sortable: false}});
		const tableActions = this.getTableActions(() => {});

		return (
			<>
				<Collection
					headers={tableHeaders}
					actions={tableActions}
					collection={this.users}
				/>
			</>
		);
	}

	@action
	protected onUserDeleted = (id: string) => {
		runInAction(() => {
			this.users = this.users.filter(user => user.id !== id);
		});
	}

	@action
	private setUsers = (users: User[]) => {
		this.users = users;
	}


}

export default UserCollection;