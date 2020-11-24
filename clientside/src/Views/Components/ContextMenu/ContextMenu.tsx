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
import { throttle } from 'lodash';
import classNames from 'classnames';
import { observer } from 'mobx-react';
import { contextMenu, Menu, Item, Submenu } from 'react-contexify';
import { Button } from '../Button/Button';
import { IconPositions } from '../Helpers/Common';
import { MenuItemEventHandler } from 'react-contexify/lib/types';
import { BodyContentContext } from '../PageWrapper/BodyContent';
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

/**
 * A context menu item is a clickable element in the context menu with a callback function.
 */
export interface IContextMenuItemProps {
	/** A label for the action button */
	label?: string;
	/** A class for the action button */
	buttonClass?: string;
	/** An icon class for the action button */
	icon?: string;
	/** The position of the icon */
	iconPos?: IconPositions;
	/** Should the label be displayed*/
	showLabel?: boolean;
	/** Callback function on click */
	onClick?: (args: MenuItemEventHandler) => any;
	/**
	 * A custom button that is displayed, instead of the one provided by the collection.
	 * If this field is provided then the label, icon and class related properties are ignored.
	 * */
	customItem?: React.ReactNode;
	// % protected region % [Add any extra IContextMenuItemProps here] off begin
	// % protected region % [Add any extra IContextMenuItemProps here] end
}

/**
 * Context menu group options. A context menu group is a submenu of actions.
 */
export interface IContextMenuItemGroup {
	/** The name of the menu group */
	groupName?: string;
	/** List of actions in this menu group */
	actions: IContextMenuActions;
	/** Menu expand icon */
	arrow?: string;
	/** A custom component to use for this item */
	customItem?: React.ReactNode;
	// % protected region % [Add any extra IContextMenuItemGroup here] off begin
	// % protected region % [Add any extra IContextMenuItemGroup here] end
}

/**
 * A context menu action can either be a nested group or a single item.
 */
export type IContextMenuActions = Array<IContextMenuItemProps | IContextMenuItemGroup>;

/**
 * Props for the context menu
 */
export interface IContextMenuProps {
	/** Unique id for this instance of the menu */
	menuId: string;
	/** The actions inside of this menu */
	actions: IContextMenuActions;
	/** Class name for the menu */
	className?: string;
	/** Parent element of the context menu. Will attempt to use the page context otherwise. */
	parentElement?: HTMLElement | null;
	/** The throttle time for the menu close scroll listener */
	throttleTime?: number;
	/** Location where the menu is hosted */
	location?: 'admin' | 'frontend';
	// % protected region % [Add any extra IContextMenuProps here] off begin
	// % protected region % [Add any extra IContextMenuProps here] end
}

/**
 * Determines weather a context menu item is a group of actions or not.
 * @param item The menu item to check.
 */
export function isItemGroup(item: IContextMenuItemProps | IContextMenuItemGroup): item is IContextMenuItemGroup {
	return item['groupName'] !== undefined;
}

/**
 * A context menu is a menu that is displayed at the location where a user clicks to invoke it.
 */
@observer
export class ContextMenu extends React.Component<IContextMenuProps> {
	static defaultProps: Partial<IContextMenuProps> = {
		location: 'frontend',
		throttleTime: 50,
		// % protected region % [Add any extra defaultProps here] off begin
		// % protected region % [Add any extra defaultProps here] end
	}

	// % protected region % [Customise the static context here] off begin
	/** Context for the current page to attach a scroll listener to if no parent is provided */
	static contextType = BodyContentContext;
	context: React.ContextType<typeof BodyContentContext> | undefined;
	// % protected region % [Customise the static context here] end

	/** Ref of the rendered menu */
	menuRef = React.createRef<Menu>();

	/** The parent element which the scroll listener is attached to */
	get parent() {
		// % protected region % [Customise the parent getter here] off begin
		return (this.props.parentElement ?? this.context?.element) ?? null;
		// % protected region % [Customise the parent getter here] end
	}

	/** The last element that has had the scroll listener attached to it */
	oldParent: HTMLElement | null = null;

	/** Parent scroll callback. This will close the current menu */
	onScroll = () => {
		// % protected region % [Customise the onScroll method here] off begin
		this.menuRef.current?.hide();
		// % protected region % [Customise the onScroll method here] end
	}

	/**
	 * Adds a scroll listener to the parent el to close the menu on scroll.
	 */
	addScrollListener = () => {
		// % protected region % [Customise the addScrollListener method here] off begin
		if (this.parent) {
			this.oldParent = this.parent;
			this.parent.addEventListener('scroll', throttle(this.onScroll, this.props.throttleTime), { passive: true });
		}
		// % protected region % [Customise the addScrollListener method here] end
	}

	/**
	 * Removes the scroll listener from the provided element.
	 * @param el The element to remove the scroll listener from.
	 */
	removeScrollListener = (el: HTMLElement | null) => {
		// % protected region % [Customise the removeScrollListener method here] off begin
		if (el) {
			el?.removeEventListener('scroll', this.onScroll)
		}
		// % protected region % [Customise the removeScrollListener method here] end
	};

	componentDidMount() {
		// % protected region % [Customise the componentDidMount method here] off begin
		this.addScrollListener();
		// % protected region % [Customise the componentDidMount method here] end
	}

	componentDidUpdate() {
		// % protected region % [Customise the componentDidUpdate method here] off begin
		if (this.oldParent !== this.parent) {
			this.removeScrollListener(this.oldParent);
			this.addScrollListener();
		}
		// % protected region % [Customise the componentDidUpdate method here] end
	}

	componentWillUnmount() {
		// % protected region % [Customise the componentWillUnmount method here] off begin
		this.removeScrollListener(this.parent);
		// % protected region % [Customise the componentWillUnmount method here] end
	}

	render() {
		// % protected region % [Customise the render method here] off begin
		const menuItems = this.getSubMenu(this.props.actions, this.props.menuId);
		return (
			<Menu id={this.props.menuId} className={classNames(this.props.location, this.props.className)} ref={this.menuRef}>
				{menuItems}
			</Menu>
		);
		// % protected region % [Customise the render method here] end
	}

	/**
	 * Gets the submenu items for this menu.
	 * @param subActions A list of actions for the context menu.
	 * @param parentItemKey The unique key for the menu parent.
	 */
	getSubMenu = (subActions: Array<IContextMenuItemProps | IContextMenuItemGroup>, parentItemKey: string) => {
		// % protected region % [Customise the getSubMenu method here] off begin
		return subActions.map((menuItem, index) => {
			const itemKey = `${parentItemKey}-${index}`;
			let menuItemNode = undefined;
			if (!isItemGroup(menuItem)) {
				if (menuItem.label) {
					const iconPos = menuItem.iconPos ?? 'icon-left'
					const icon = menuItem.icon ? { icon: menuItem.icon, iconPos: iconPos } : undefined;
					menuItemNode = <Button className={menuItem.buttonClass} icon={icon}>
						{menuItem.label}
					</Button>;
				} else {
					menuItemNode = menuItem.customItem ?? '-';
				}

				return <Item onClick={(args) => menuItem.onClick?.(args)} key={itemKey}>
					{menuItemNode}
				</Item>;
			} else {
				let groupItem: React.ReactNode;
				if (menuItem.customItem) {
					groupItem = menuItem.customItem;
				} else {
					groupItem = <Button
						className="menu-group"
						icon={{icon: menuItem.arrow ?? 'chevron-right', iconPos: 'icon-right'}}>
						{menuItem.groupName ?? '-'}
					</Button>
				}
				return <Submenu label={groupItem} arrow="" key={itemKey}>
					{this.getSubMenu(menuItem.actions, itemKey)}
				</Submenu>;
			}
		});
		// % protected region % [Customise the getSubMenu method here] end
	}

	/**
	 * Shows this menu at the location specified by the provided mouse event.
	 * @param e
	 */
	handleContextMenu = (e: React.MouseEvent<Element, MouseEvent>) => {
		// % protected region % [Customise the handleContextMenu method here] off begin
		// Always prevent default behavior
		e.preventDefault();

		contextMenu.show({
			id: this.props.menuId,
			event: e,
		});
		// % protected region % [Customise the handleContextMenu method here] end
	}

	// % protected region % [Add any extra methods here] off begin
	// % protected region % [Add any extra methods here] end
}
