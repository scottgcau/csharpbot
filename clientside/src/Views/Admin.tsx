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
import { action } from 'mobx';
import * as AdminPages from './Pages/Admin/Entity';
import Auth from "./Components/Auth/Auth";
import AllUsersPage from './Pages/Admin/AllUsersPage';
import AdminPage from './Pages/Admin/AdminPage';
import Topbar from "./Components/Topbar/Topbar";
import PageLinks from './Pages/Admin/PageLinks';
import Spinner from 'Views/Components/Spinner/Spinner';
import { Redirect, Route, RouteComponentProps, Switch } from 'react-router';
import { store } from "Models/Store";
import FormsPage from "./Pages/Admin/Forms/FormsPage";
import TimelinePage from "./Pages/Admin/Timelines/TimelinePage";
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Customize lazy imports here] off begin
const GraphiQlLazy = React.lazy(() => import("./Pages/Admin/Graphiql"));
// % protected region % [Customize lazy imports here] end

export default class Admin extends React.Component<RouteComponentProps> {
	
	private path = this.props.match.path === '/' ? '' : this.props.match.path;
	
	@action
	private setAppLocation = () => {
		store.appLocation = 'admin';
	}

	public componentDidMount() {
		this.setAppLocation();
	}

	public render() {
		return (
			<>
				<div className="body-container">
					{
					// % protected region % [Modify Topbar] off begin
					}
					<Topbar currentLocation="admin" />
					{
					// % protected region % [Modify Topbar] end
					}
					<div className="admin">
						<Auth {...this.props}>
							<Switch>
								{
								// % protected region % [Modify top level admin routing here] off begin
								}
								<Route path={`${this.path}/graphiql`}>
									<React.Suspense fallback={<Spinner />}>
										<GraphiQlLazy />
									</React.Suspense>
								</Route>
								<Route component={this.adminSwitch} />
								{
								// % protected region % [Modify top level admin routing here] end
								}
							</Switch>
						</Auth>
					</div>
				</div>
			</>
		);
	}

	private adminSwitch = () => {
		if (!store.userGroups.some(ug => ug.hasBackendAccess)) {
			return <Redirect to="/404" />;
		}

		return (
			<>
				{
				// % protected region % [Override contents here] off begin
				}
				<PageLinks {...this.props} />
				{
				// % protected region % [Override contents here] end
				}
				<div className="body-content">
					<Switch>
						{/* These routes require a login to view */}

						{/* Admin entity pages */}
						<Route exact={true} path={`${this.path}`} component={AdminPage} />
						<Route path={`${this.path}/User`} component={AllUsersPage} />
						<Route path={`${this.path}/forms`} component={FormsPage} />
						<Route path={`${this.path}/Timelines`} component={TimelinePage} />
						<Route path={`${this.path}/ScheduleEntity`} component={AdminPages.ScheduleEntityPage} />
						<Route path={`${this.path}/SeasonEntity`} component={AdminPages.SeasonEntityPage} />
						<Route path={`${this.path}/VenueEntity`} component={AdminPages.VenueEntityPage} />
						<Route path={`${this.path}/GameEntity`} component={AdminPages.GameEntityPage} />
						<Route path={`${this.path}/SportEntity`} component={AdminPages.SportEntityPage} />
						<Route path={`${this.path}/LeagueEntity`} component={AdminPages.LeagueEntityPage} />
						<Route path={`${this.path}/TeamEntity`} component={AdminPages.TeamEntityPage} />
						<Route path={`${this.path}/PersonEntity`} component={AdminPages.PersonEntityPage} />
						<Route path={`${this.path}/RosterEntity`} component={AdminPages.RosterEntityPage} />
						<Route path={`${this.path}/RosterassignmentEntity`} component={AdminPages.RosterassignmentEntityPage} />
						<Route path={`${this.path}/ScheduleSubmissionEntity`} component={AdminPages.ScheduleSubmissionEntityPage} />
						<Route path={`${this.path}/SeasonSubmissionEntity`} component={AdminPages.SeasonSubmissionEntityPage} />
						<Route path={`${this.path}/VenueSubmissionEntity`} component={AdminPages.VenueSubmissionEntityPage} />
						<Route path={`${this.path}/GameSubmissionEntity`} component={AdminPages.GameSubmissionEntityPage} />
						<Route path={`${this.path}/SportSubmissionEntity`} component={AdminPages.SportSubmissionEntityPage} />
						<Route path={`${this.path}/LeagueSubmissionEntity`} component={AdminPages.LeagueSubmissionEntityPage} />
						<Route path={`${this.path}/TeamSubmissionEntity`} component={AdminPages.TeamSubmissionEntityPage} />
						<Route path={`${this.path}/PersonSubmissionEntity`} component={AdminPages.PersonSubmissionEntityPage} />
						<Route path={`${this.path}/RosterSubmissionEntity`} component={AdminPages.RosterSubmissionEntityPage} />
						<Route path={`${this.path}/RosterassignmentSubmissionEntity`} component={AdminPages.RosterassignmentSubmissionEntityPage} />
						<Route path={`${this.path}/ScheduleEntityFormTileEntity`} component={AdminPages.ScheduleEntityFormTileEntityPage} />
						<Route path={`${this.path}/SeasonEntityFormTileEntity`} component={AdminPages.SeasonEntityFormTileEntityPage} />
						<Route path={`${this.path}/VenueEntityFormTileEntity`} component={AdminPages.VenueEntityFormTileEntityPage} />
						<Route path={`${this.path}/GameEntityFormTileEntity`} component={AdminPages.GameEntityFormTileEntityPage} />
						<Route path={`${this.path}/SportEntityFormTileEntity`} component={AdminPages.SportEntityFormTileEntityPage} />
						<Route path={`${this.path}/LeagueEntityFormTileEntity`} component={AdminPages.LeagueEntityFormTileEntityPage} />
						<Route path={`${this.path}/TeamEntityFormTileEntity`} component={AdminPages.TeamEntityFormTileEntityPage} />
						<Route path={`${this.path}/PersonEntityFormTileEntity`} component={AdminPages.PersonEntityFormTileEntityPage} />
						<Route path={`${this.path}/RosterEntityFormTileEntity`} component={AdminPages.RosterEntityFormTileEntityPage} />
						<Route path={`${this.path}/RosterassignmentEntityFormTileEntity`} component={AdminPages.RosterassignmentEntityFormTileEntityPage} />
						<Route path={`${this.path}/RosterTimelineEventsEntity`} component={AdminPages.RosterTimelineEventsEntityPage} />

						{
						// % protected region % [Add any extra page routes here] off begin
						}
						{
						// % protected region % [Add any extra page routes here] end
						}
					</Switch>
				</div>
				{
				// % protected region % [Add any admin footer content here] off begin
				}
				{
				// % protected region % [Add any admin footer content here] end
				}
			</>
		);
	}
}