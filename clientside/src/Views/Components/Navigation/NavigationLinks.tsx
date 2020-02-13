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
import { RouteComponentProps } from 'react-router-dom';
import { filter } from 'lodash';
import NavigationLink from './NavigationLink';
import { ILink } from './Navigation';
import { observer } from 'mobx-react';
import { action } from 'mobx';

export interface INavigationLinksProps<T extends ILink> extends RouteComponentProps {
	className?: string;
	links: Array<T>;
	filter?: (link: T) => boolean;
}

@observer
class NavigationLinks<T extends ILink> extends React.Component<INavigationLinksProps<T>> {
	public render() {
		const { className, links, ...routerProps } = this.props;
		const htmlLinks = filter(links, this.props.filter)
			.map((link) => <NavigationLink
				{...link}
				{...routerProps}
				path={link.path}
				label={link.label}
				icon={link.icon}
				iconPos={link.iconPos}
				key={link.path}
				isParent={!!link.subLinks}
				onClick={() => this.onClick(link)}
				isDisabled={link.isDisabled}
				subLinksFilter={link.subLinksFilter}
			/>);

		return (
			<ul className={className}>
				{htmlLinks}
			</ul>
		);
	}

	@action
	private onClick = (link: ILink) => {
		if (!!link.onClick) {
			link.onClick();
		}
	}
}

export default NavigationLinks;