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
import Navigation, { Orientation, ILink } from 'Views/Components/Navigation/Navigation';
import { RouteComponentProps } from 'react-router';
import * as Models from 'Models/Entities';
import { Model } from 'Models/Model';
import {IIconProps} from "Views/Components/Helpers/Common";
import {SecurityService} from "Services/SecurityService";
import {store} from "Models/Store";

interface AdminLink extends IIconProps {
	path: string;
	label: string;
	entity: {new (args: any): Model};
	isMember?: boolean;
}

const LINKS : AdminLink[] = [
	{
		path: '/admin/sportentity',
		label: 'SportEntity',
		entity: Models.Sportentity,
		isMember: false
	},
	{
		path: '/admin/sportentitysubmission',
		label: 'SportEntity Submission',
		entity: Models.SportentitySubmission,
		isMember: false
	},
	{
		path: '/admin/sportentityformtile',
		label: 'SportEntity Form Tile',
		entity: Models.SportentityFormTile,
		isMember: false
	},
	// % protected region % [Add any extra page links here] off begin
	// % protected region % [Add any extra page links here] end
];

export default class PageLinks extends React.Component<RouteComponentProps> {
	private filter = (link: AdminLink) => {
		return SecurityService.canRead(link.entity);
	}

	public render() {
		return <Navigation
			className='nav__admin'
			orientation={Orientation.VERTICAL}
			linkGroups={this.getAdminNavLinks()}
			{...this.props} />;
	}

	private getAdminNavLinks = () : ILink[][] => {
		const userLinks = LINKS.filter(link => link.isMember).filter(this.filter);
		const entityLinks = LINKS.filter(link => ! link.isMember).filter(this.filter);

		let linkGroups: ILink[][] = [];

		const homeLinkGroup: ILink[] = [
			{ path: '/', label: 'Home', icon: 'home', iconPos: 'icon-left' },
			// { path: '/admin/settings', label: 'Settings', icon: 'settings', iconPos: 'icon-left', isDisabled: true }
		];
		linkGroups.push(homeLinkGroup);

		const entityLinkGroup: ILink[] = [];
		if (userLinks.length > 0) {
			entityLinkGroup.push(
				{
					path: '/admin/users',
					label: 'Users',
					icon: 'person-group',
					iconPos: 'icon-left',
					subLinks: userLinks.map(link => {
						return {
							path: link.path,
							label: link.label,
						}
					})
				}
			);
		}
		if (entityLinks.length > 0) {
			entityLinkGroup.push(
				{
					path: '/admin/entities',
					label: 'Entities',
					icon: 'list',
					iconPos: 'icon-left',
					subLinks: entityLinks.map(link => {
						return {
							path: link.path,
							label: link.label,
						}
					})
				}
			);
		}
		linkGroups.push(entityLinkGroup);

		// Removed these links until these behaviours are activated in the future
		const otherlinkGroup: ILink[] = [];
		//otherlinkGroup.push({ path: '/admin/dashboards', label: 'Dashboards', icon: 'dashboard', iconPos: 'icon-left', isDisabled: true });
		//otherlinkGroup.push({ path: '/admin/timelines', label: 'Timelines', icon: 'timeline', iconPos: 'icon-left', isDisabled: true });
		const formsGroups: string[] = ['Visitors'];
		if (store.userGroups.some(ug => formsGroups.some(fg => fg == ug.name))){
			otherlinkGroup.push({ path: '/admin/forms', label: 'Forms', icon: 'forms', iconPos: 'icon-left', isDisabled: false });
		}
		if (otherlinkGroup.length > 0) {
			linkGroups.push(otherlinkGroup);
		}

		const bottomlinkGroup: ILink[] = [];
		// bottomlinkGroup.push({ path: '/admin/documentation', label: 'Documentation', icon: 'help', iconPos: 'icon-left', isDisabled: true });
		bottomlinkGroup.push({ path: '/logout', label: 'Logout', icon: 'logout', iconPos: 'icon-left' });
		linkGroups.push(bottomlinkGroup);

		return linkGroups;
	}
}