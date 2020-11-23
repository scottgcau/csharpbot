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
import {Button} from "../../Views/Components/Button/Button";
// % protected region % [Add extra imports here] off begin
// % protected region % [Add extra imports here] end

export interface IGraphViewTopBarControlsProps {
	// % protected region % [Override IGraphViewTopBarControlsProps here] off begin
	onJumpToToday: () => void;
	onZoomBackToDefault: () => void;
	onZoomIn: () => void;
	onZoomOut: () => void;
	onPanLeft: () => void;
	onPanRight: () => void;
	onSwitchToListView: () => void;
	// % protected region % [Override IGraphViewTopBarControlsProps here] end
}


export default class GraphViewControls extends React.Component<IGraphViewTopBarControlsProps> {

	// % protected region % [Add extra component logic here] off begin
	// % protected region % [Add extra component logic here] end
	
	public render() {
		// % protected region % [Override render here] off begin
		return (
			<>
				<Button
					onClick={this.props.onJumpToToday}
					icon={{icon: 'calender', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="jump to today"> Today </Button>
				<Button
					onClick={this.props.onZoomBackToDefault}
					icon={{icon: 'refresh-ccw', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="zoom back to default"> Reset </Button>
				<Button
					onClick={this.props.onZoomIn}
					icon={{icon: 'zoom-in', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="zoom in"> Zoom in </Button>
				<Button
					onClick={this.props.onZoomOut}
					icon={{icon: 'zoom-out', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="zoom out"> Zoom out </Button>
				<Button
					onClick={this.props.onPanLeft}
					icon={{icon: 'arrow-left', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="move left"> View left </Button>
				<Button
					onClick={this.props.onPanRight}
					icon={{icon: 'arrow-right', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'} aria-label="move right"> View right </Button>
				<Button
					onClick={() => this.props.onSwitchToListView()}
					icon={{icon: 'ol-list', iconPos: 'icon-top'}}
					className={'btn--solid btn--secondary'}
					aria-label="list view"> List </Button>
			</>
		);
		// % protected region % [Override render here] end
	}
}