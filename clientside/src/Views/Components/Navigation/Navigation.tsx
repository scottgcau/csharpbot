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
import { RouteComponentProps, Link } from 'react-router-dom';
import { IIconProps } from '../Helpers/Common';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import classNames from 'classnames';
import NavigationLinks from './NavigationLinks';

export enum Orientation {
	VERTICAL,
	HORIZONTAL,
}

export interface ILink extends IIconProps {
	path: string;
	label: string;
	onClick?: (event?: any) => void;
	subLinks?: ILink[];
	subLinksFilter?: (link: ILink) => boolean;
	isDisabled?: boolean;
}

export interface INavigationProps<T extends ILink> extends RouteComponentProps {
	className?: string;
	orientation: Orientation;
	linkGroups: Array<Array<T>>;
	filter?: (link: T) => boolean;
}

@observer
class Navigation<T extends ILink> extends React.Component<INavigationProps<T>> {
	@observable
	private navCollapsed: boolean = true;

	public render() {
		const { className, linkGroups, ...routerProps } = this.props;

		const expandButton = (
			<a className={classNames('link-rm-txt-dec expand-icon', this.navCollapsed ? 'icon-menu' : 'icon-menu', 'icon-left')} onClick={this.onClickNavCollapse} />
		);

		return (
			<nav className={classNames('nav', this.navCollapsed?'nav--collapsed':'nav--expanded', this.getOrientationClassName())}>
				{linkGroups.map((links, index) => (
					<NavigationLinks
						key={index}
						{...routerProps}
						links={links}
					/>
				))}
				{expandButton}
			</nav>
		);
	}

	private getOrientationClassName = () => {
		switch (this.props.orientation) {
			case Orientation.HORIZONTAL:
				return 'nav--horizontal';
			case Orientation.VERTICAL:
				return 'nav--vertical';
		}
		return '';
	}

	@action
	private onClickNavCollapse = () => {
		this.navCollapsed = !this.navCollapsed;
	}
}

export default Navigation;