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
import {Redirect, Route, RouteComponentProps, Switch} from "react-router";
import moment, {Moment} from "moment";
import TimelineTile from 'Timelines/TimelineTile';
import {observer} from 'mobx-react';
import {observable} from 'mobx';
import {getTimelineEntityFromName} from 'Util/TimelineUtils';
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

@observer
export default class TimelinePage extends React.Component<RouteComponentProps> {

	@observable
	private timelineViewDate: Moment | undefined = undefined;

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	public render() {
		// % protected region % [Override render here] off begin
		const timelineTile = (pageProps: RouteComponentProps) => {
			const params = pageProps.match.params;
			const entityName = params['entity'];
			const entityId = params['id'];
			const component = params['component'];
			
			const routeToComponent = (component: 'list' | 'graph') => {
				let route = this.props.match.url + `/${component}`;
				if (entityName) {
					route += `/${entityName}`
				} if (entityId) {
					route += `/${entityId}`
				}
				this.props.history.push(route);
			};
			
			const routeToListView = (date? : Date) => {
				this.timelineViewDate = date ? moment(date) : undefined;
				routeToComponent('list');
			};

			const onClickViewItem = (entity: string, id: string) => {
				if (id) {
					this.props.history.push(`/admin/${entity}/view/${id}`)
				}
			};
			
			return (
				<TimelineTile 
					timelineViewDate={this.timelineViewDate}
					entityId={entityId}
					component={component}
					onClickViewItem={onClickViewItem}
					onRouteToListView={routeToListView}
					onRouteToGraphView={() => routeToComponent('graph')}
					canChangeTimelineEntity={true}
					timelineEntity={getTimelineEntityFromName(entityName)} />
			);
		};

		return (
			<Switch>
				<Route exact={true} path={`${this.props.match.url}/:component/:entity?/:id?`} render={timelineTile} />
				<Redirect to={`${this.props.match.url}/graph`}/>
			</Switch>
		);
		// % protected region % [Override render here] end
	}
}