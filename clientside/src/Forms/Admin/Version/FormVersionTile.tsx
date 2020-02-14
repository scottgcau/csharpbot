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
import {computed} from "mobx";
import {Link} from "react-router-dom";
import {Button} from "Views/Components/Button/Button";
import {noSpaces} from "Util/StringUtils";

export interface IFormVersionTileProps {
	formVersionName: string;
	formEntityName: string;
	id: string;
	currentVersion?: number | undefined;
	publishedVersion?: number | undefined;
}

export class FormVersionTile extends React.Component<IFormVersionTileProps> {
	private formItemVersions = () => {
		if(!this.props.publishedVersion && !this.props.currentVersion) {
			return null;
		}

		return (
			<div className='form-item__versions'>
				{this.currentVersionComponent}
				{this.publishedVersionComponent}
			</div>
		);
	};

	@computed
	private get publishedVersionComponent() {
		return this.props.publishedVersion
			? <p className="version__active"> Version {this.props.publishedVersion} </p>
			: null;
	};

	@computed
	private get currentVersionComponent() {
		return this.props.currentVersion
			? <p className="version__inactive"> Version {this.props.currentVersion} </p>
			: null;
	};

	private linkAddress = `/admin/forms/build/${noSpaces(this.props.formEntityName)}/${this.props.id}`;

	public render(){
		return (
			<Link to={this.linkAddress}>
				<div className='form-item'>
					<div className='form-item__heading'>
						<h3>{this.props.formVersionName}</h3>
						{/* <p className='form-responses'>Responses</p> */}
					</div>
					<div className='form-item__footer'>
						{/* this.formItemVersions() */}
						{/* <Button className="icon-more-horizontal icon-right"/> */}
					</div>
				</div>
			</Link>
		)
	}
}