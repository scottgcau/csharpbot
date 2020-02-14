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
import { RouteComponentProps, matchPath } from "react-router";
import * as React from 'react';
import classNames from 'classnames';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react';
import { computed, action, observable } from 'mobx';
import { IIconProps } from '../Helpers/Common';
import { ILink } from './Navigation';
import NavigationLinks from './NavigationLinks';
import If from '../If/If';

interface INavigationLinkProps extends RouteComponentProps, IIconProps {
	path: string;
	className?: string;
	label: string;
	isParent?: boolean;
	subLinks?: ILink[];
	subLinksFilter?: (link: ILink) => boolean;
	onClick?: (event: React.MouseEvent<HTMLLIElement, MouseEvent>) => void;
	isActive?: boolean;
	isDisabled?: boolean;
}

@observer
class NavigationLink extends React.Component<INavigationLinkProps> {
	static defaultProps = {
		iconPos: 'left',
	}

	private liRef: HTMLLIElement | null = null;

	componentDidMount() {
		document.addEventListener('click', this.handleDocumentClick);
	}
	componentWillMount() {
		document.removeEventListener('click', this.handleDocumentClick);
	}
	componentWillUnmount() {
		document.removeEventListener('click', this.handleDocumentClick);
	}

	@observable
	private subLinksExpanded: boolean = false;

	@computed
	private get icon() {
		if (this.props.icon) {
			return `icon-${this.props.icon}`;
		}
		return undefined;
	}

	@computed
	private get iconPos() {
		if (this.icon) {
			return this.props.iconPos;
		}
		return undefined;
	}

	public render() {
		const { path, label, subLinks, subLinksFilter, ...routerProps } = this.props;
		let subLinksNode = null;
		if (this.props.isParent && !!this.props.subLinks) {
			const ulClazz = classNames('nav__sublinks', this.subLinksExpanded ? 'nav__sublinks--visible' : '');
			subLinksNode = (
				<NavigationLinks
					{...routerProps}
					className={ulClazz}
					links={subLinks || []}
					filter={subLinksFilter}
				/>
			);
		}

		const isActive = this.props.isActive ? this.props.isActive : (this.isActive(path));

		let textNode = !!this.icon ? <span>{label}</span> : label;

		return (
			<li
				ref={(ref: any) => {
					if (!!ref) {
						this.liRef = ref;
					}
				}}
				className={classNames({ 'nav__parent-link--active': this.subLinksExpanded && this.props.isParent, 'active': isActive && !this.props.isParent }, this.props.className)} key={path}
				onClick={this.onClick}
			>
				<If condition={(this.props.isParent || this.props.isDisabled)}>
					<a className={classNames(this.icon, this.iconPos)} aria-label={label}>{textNode}</a>
				</If>
				<If condition={!(this.props.isParent || this.props.isDisabled)}>
					<Link to={path} className={classNames(this.icon, this.iconPos)} aria-label={label}>{textNode}</Link>
				</If>
				{subLinksNode}
			</li>);
	}

	@action
	private onClick = (event: React.MouseEvent<HTMLLIElement, MouseEvent>) => {
		if (this.props.isParent) {
			this.subLinksExpanded = !this.subLinksExpanded;
		}
		if (this.props.onClick) {
			this.props.onClick(event);
		}
	}

	private isActive = (path: string) => {
		return !!matchPath(this.props.location.pathname, { path, exact: true });
	}

	@action
	private handleDocumentClick = (e: any) => {
		if (this.subLinksExpanded && !!this.liRef && (!this.liRef.contains(e.target) || (!!this.liRef.lastElementChild && this.liRef.lastElementChild.contains(e.target)))) {
			this.subLinksExpanded = !this.subLinksExpanded;
		}
	}
}

export default NavigationLink;