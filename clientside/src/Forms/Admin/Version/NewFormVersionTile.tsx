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
import * as React from "react";
import {Link} from "react-router-dom";
import {lowerCaseNoSpaces} from "Util/StringUtils";
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface INewFormVersionTileProps {
	formName: string;
	formDisplayName: string;
	// % protected region % [Add any extra props here] off begin
	// % protected region % [Add any extra props here] end
}

export class NewFormVersionTile extends React.Component<INewFormVersionTileProps>{

	// % protected region % [Add class properties here] off begin
	// % protected region % [Add class properties here] end

	public render(){
		// % protected region % [Modify render function here] off begin
		return(
			<Link to= {`/admin/${lowerCaseNoSpaces(this.props.formName)}/create?redirect=/admin/forms/build/${lowerCaseNoSpaces(this.props.formName)}/{id}`} >
				<div className='form-item__new icon-plus icon-bottom'>
					<h3> New {this.props.formDisplayName} </h3>
				</div>
			</Link>
		)
		// % protected region % [Modify render function here] end
	}
}