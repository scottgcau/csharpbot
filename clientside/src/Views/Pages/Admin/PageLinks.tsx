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
import Navigation, { Orientation, ILink } from 'Views/Components/Navigation/Navigation';
import { RouteComponentProps } from 'react-router';
import * as Models from 'Models/Entities';
import { Model } from 'Models/Model';
import { IIconProps } from "Views/Components/Helpers/Common";
import { SecurityService } from "Services/SecurityService";
import { store } from "Models/Store";
import { getModelDisplayName } from 'Util/EntityUtils';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

interface AdminLink extends IIconProps {
	path: string;
	label: string;
	entity: {new (args: any): Model};
	isMember?: boolean;
	// % protected region % [Add extra AdminLink fields here] off begin
	// % protected region % [Add extra AdminLink fields here] end
}

const getPageLinks = (): AdminLink[] => [
	{
		// % protected region % [Override navigation link for LadderEntity here] off begin
		path: '/admin/ladderentity',
		label: getModelDisplayName(Models.LadderEntity),
		entity: Models.LadderEntity,
		isMember: false
		// % protected region % [Override navigation link for LadderEntity here] end
	},
	{
		// % protected region % [Override navigation link for ScheduleEntity here] off begin
		path: '/admin/scheduleentity',
		label: getModelDisplayName(Models.ScheduleEntity),
		entity: Models.ScheduleEntity,
		isMember: false
		// % protected region % [Override navigation link for ScheduleEntity here] end
	},
	{
		// % protected region % [Override navigation link for LaddereliminationEntity here] off begin
		path: '/admin/laddereliminationentity',
		label: getModelDisplayName(Models.LaddereliminationEntity),
		entity: Models.LaddereliminationEntity,
		isMember: false
		// % protected region % [Override navigation link for LaddereliminationEntity here] end
	},
	{
		// % protected region % [Override navigation link for LadderwinlossEntity here] off begin
		path: '/admin/ladderwinlossentity',
		label: getModelDisplayName(Models.LadderwinlossEntity),
		entity: Models.LadderwinlossEntity,
		isMember: false
		// % protected region % [Override navigation link for LadderwinlossEntity here] end
	},
	{
		// % protected region % [Override navigation link for RoundEntity here] off begin
		path: '/admin/roundentity',
		label: getModelDisplayName(Models.RoundEntity),
		entity: Models.RoundEntity,
		isMember: false
		// % protected region % [Override navigation link for RoundEntity here] end
	},
	{
		// % protected region % [Override navigation link for GameEntity here] off begin
		path: '/admin/gameentity',
		label: getModelDisplayName(Models.GameEntity),
		entity: Models.GameEntity,
		isMember: false
		// % protected region % [Override navigation link for GameEntity here] end
	},
	{
		// % protected region % [Override navigation link for DivisionEntity here] off begin
		path: '/admin/divisionentity',
		label: getModelDisplayName(Models.DivisionEntity),
		entity: Models.DivisionEntity,
		isMember: false
		// % protected region % [Override navigation link for DivisionEntity here] end
	},
	{
		// % protected region % [Override navigation link for VenueEntity here] off begin
		path: '/admin/venueentity',
		label: getModelDisplayName(Models.VenueEntity),
		entity: Models.VenueEntity,
		isMember: false
		// % protected region % [Override navigation link for VenueEntity here] end
	},
	{
		// % protected region % [Override navigation link for TeamEntity here] off begin
		path: '/admin/teamentity',
		label: getModelDisplayName(Models.TeamEntity),
		entity: Models.TeamEntity,
		isMember: false
		// % protected region % [Override navigation link for TeamEntity here] end
	},
	{
		// % protected region % [Override navigation link for GamerefereeEntity here] off begin
		path: '/admin/gamerefereeentity',
		label: getModelDisplayName(Models.GamerefereeEntity),
		entity: Models.GamerefereeEntity,
		isMember: false
		// % protected region % [Override navigation link for GamerefereeEntity here] end
	},
	{
		// % protected region % [Override navigation link for SeasonEntity here] off begin
		path: '/admin/seasonentity',
		label: getModelDisplayName(Models.SeasonEntity),
		entity: Models.SeasonEntity,
		isMember: false
		// % protected region % [Override navigation link for SeasonEntity here] end
	},
	{
		// % protected region % [Override navigation link for PersonEntity here] off begin
		path: '/admin/personentity',
		label: getModelDisplayName(Models.PersonEntity),
		entity: Models.PersonEntity,
		isMember: false
		// % protected region % [Override navigation link for PersonEntity here] end
	},
	{
		// % protected region % [Override navigation link for SystemuserEntity here] off begin
		path: '/admin/systemuserentity',
		label: getModelDisplayName(Models.SystemuserEntity),
		entity: Models.SystemuserEntity,
		isMember: true
		// % protected region % [Override navigation link for SystemuserEntity here] end
	},
	{
		// % protected region % [Override navigation link for SportEntity here] off begin
		path: '/admin/sportentity',
		label: getModelDisplayName(Models.SportEntity),
		entity: Models.SportEntity,
		isMember: false
		// % protected region % [Override navigation link for SportEntity here] end
	},
	{
		// % protected region % [Override navigation link for LeagueEntity here] off begin
		path: '/admin/leagueentity',
		label: getModelDisplayName(Models.LeagueEntity),
		entity: Models.LeagueEntity,
		isMember: false
		// % protected region % [Override navigation link for LeagueEntity here] end
	},
	{
		// % protected region % [Override navigation link for RosterEntity here] off begin
		path: '/admin/rosterentity',
		label: getModelDisplayName(Models.RosterEntity),
		entity: Models.RosterEntity,
		isMember: false
		// % protected region % [Override navigation link for RosterEntity here] end
	},
	{
		// % protected region % [Override navigation link for RosterassignmentEntity here] off begin
		path: '/admin/rosterassignmententity',
		label: getModelDisplayName(Models.RosterassignmentEntity),
		entity: Models.RosterassignmentEntity,
		isMember: false
		// % protected region % [Override navigation link for RosterassignmentEntity here] end
	},
	{
		// % protected region % [Override navigation link for RosterTimelineEventsEntity here] off begin
		path: '/admin/rostertimelineeventsentity',
		label: getModelDisplayName(Models.RosterTimelineEventsEntity),
		entity: Models.RosterTimelineEventsEntity,
		isMember: false
		// % protected region % [Override navigation link for RosterTimelineEventsEntity here] end
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
		// % protected region % [Add custom logic before all here] off begin
		// % protected region % [Add custom logic before all here] end

		const links = getPageLinks();
		let userLinks = links.filter(link => link.isMember).filter(this.filter);
		let entityLinks = links.filter(link => ! link.isMember).filter(this.filter);

		let linkGroups: ILink[][] = [];

		// % protected region % [Add any custom logic here before groups are made] off begin
		// % protected region % [Add any custom logic here before groups are made] end

		const homeLinkGroup: ILink[] = [
			{ path: '/admin', label: 'Home', icon: 'home', iconPos: 'icon-left' },
			// { path: '/admin/settings', label: 'Settings', icon: 'settings', iconPos: 'icon-left', isDisabled: true }

			// % protected region % [Updated your home link group here] off begin
			// % protected region % [Updated your home link group here] end
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
					subLinks: [
						{path: "/admin/user", label: "All Users"},
						...userLinks.map(link => ({path: link.path, label: link.label}))
					]
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

		// % protected region % [Add any new link groups here before other and bottom] off begin
		// % protected region % [Add any new link groups here before other and bottom] end

		// Removed these links until these behaviours are activated in the future
		const otherlinkGroup: ILink[] = [];
		//otherlinkGroup.push({ path: '/admin/dashboards', label: 'Dashboards', icon: 'dashboard', iconPos: 'icon-left', isDisabled: true });
		//otherlinkGroup.push({ path: '/admin/timelines', label: 'Timelines', icon: 'timeline', iconPos: 'icon-left', isDisabled: true });
		otherlinkGroup.push({ path: '/admin/timelines', label: 'Timelines', icon: 'timeline', iconPos: 'icon-left', isDisabled: false });
		if (otherlinkGroup.length > 0) {
			linkGroups.push(otherlinkGroup);
		}

		const bottomlinkGroup: ILink[] = [];
		// bottomlinkGroup.push({ path: '/admin/documentation', label: 'Documentation', icon: 'help', iconPos: 'icon-left', isDisabled: true });
		bottomlinkGroup.push(
			{
				path: '/admin/',
				label: 'Utility',
				icon: 'file',
				iconPos: 'icon-left',
				subLinks: [
					{
						path: '/admin/graphiql',
						label: 'GraphiQL',
						useATag: true,
					},
					{
						path: '/api/swagger',
						label: 'OpenAPI',
						useATag: true,
					},
					{
						path: 'https://codebots.app/app-settings-and-details/git',
						label: 'Git',
						useATag: true,
					},
				],
			}
		);
		bottomlinkGroup.push({ path: '/logout', label: 'Logout', icon: 'room', iconPos: 'icon-left' });
		linkGroups.push(bottomlinkGroup);

		// % protected region % [Modify your link groups here before returning] off begin
		// % protected region % [Modify your link groups here before returning] end

		return linkGroups;
	}

	// % protected region % [Add custom methods here] off begin
	// % protected region % [Add custom methods here] end
}