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
import * as uuid from 'uuid';
import moment from 'moment';
import classNames from 'classnames';
import { FormSlideBuilder } from 'Forms/Designer/FormSlideBuilder';
import { FormEntityData } from 'Forms/FormEntityData';
import { action, computed, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import { ButtonGroup } from 'Views/Components/Button/ButtonGroup';
import { Button } from 'Views/Components/Button/Button';
import If from 'Views/Components/If/If';
import { FormVersion } from 'Forms/FormVersion';
import { FormTile } from 'Forms/FormTile';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface GenericFormDesignerProps {
	form: FormEntityData;
	initialSelectedVersion?: string;
	className?: string;
	// % protected region % [Add any additional props here] off begin
	// % protected region % [Add any additional props here] end
}

export interface FormDesignerProps extends GenericFormDesignerProps {
	onCancel?: () => void;
	onSaveDraft?: (version: FormVersion) => void;
	onSavePublish?: (version: FormVersion) => void;
	// % protected region % [Add any additional generic props  here] off begin
	// % protected region % [Add any additional generic props  here] end
}

// % protected region % [Modify tab types here] off begin
type tabTypes = 'build' | 'preview';
// % protected region % [Modify tab types here] end

@observer
// % protected region % [Modify class declaration here] off begin
export class FormDesigner extends React.Component<FormDesignerProps> {
// % protected region % [Modify class declaration here] end

	// % protected region % [override default values here] off begin
	@observable
	private selectedVersionId = this.props.initialSelectedVersion || this.props.form.publishedVersionId;

	@observable
	private selectedTab: tabTypes = 'build';

	@observable
	private previewData = {};
	// % protected region % [override default values here] end

	// % protected region % [Add any additional variables here] off begin
	// % protected region % [Add any additional variables here] end

	// % protected region % [Override isPublishedVersion here] off begin
	@computed
	private get isPublishedVersion() {
		return !!(this.selectedVersion && this.selectedVersion.id === this.props.form.publishedVersionId);
	}
	// % protected region % [Override isPublishedVersion here] end

	// % protected region % [Override selectedVersion here] off begin
	@computed
	private get selectedVersion() {
		const versions = this.props.form.formVersions;
		if (versions && this.selectedVersionId) {
			return versions.find(v => v.id === this.selectedVersionId);
		}
		return undefined;
	}
	// % protected region % [Override selectedVersion here] end

	// % protected region % [Override selectVersion here] off begin
	@action
	public selectVersion = (id: string) => {
		this.selectedVersionId = id;
	}
	// % protected region % [Override selectVersion here] end

	// % protected region % [Override addEmptyVersion here] off begin
	@action
	private addEmptyVersion = () => {
		if (this.props.form.formVersions) {
			this.props.form.formVersions.push({
				formData: [],
				id: uuid.v4(),
				version: 1,
			});
		}
	}
	// % protected region % [Override addEmptyVersion here] end

	// % protected region % [Override change tab functionality here] off begin
	private changeTab = (tabType: tabTypes) => (event: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => {
		event.preventDefault();
		runInAction(() => this.selectedTab = tabType)
	};
	// % protected region % [Override change tab functionality here] end

	// % protected region % [Override getOrAddLatestVersion here] off begin
	public getOrAddLatestVersion = async () => {
		await this.props.form.getAllVersions();

		if (!this.props.form.formVersions) {
			this.props.form.formVersions = [];
		}

		if (this.props.form.formVersions.length === 0) {
			this.addEmptyVersion();
		}

		return this.props.form.formVersions.slice().sort((a, b) => b.version - a.version)[0];
	}
	// % protected region % [Override getOrAddLatestVersion here] end

	// % protected region % [Override onSaveDraft here] off begin
	private onSaveDraft = () => {
		if (this.props.onSaveDraft && this.selectedVersion) {
			this.props.onSaveDraft(this.selectedVersion);
		}
	}
	// % protected region % [Override onSaveDraft here] end

	// % protected region % [override on save publish functionality here] off begin
	private onSavePublish = () => {
		if (this.props.onSavePublish && this.selectedVersion) {
			this.props.onSavePublish(this.selectedVersion);
		}
	}
	// % protected region % [override on save publish functionality here] end

	// % protected region % [Add any additional functions here] off begin
	// % protected region % [Add any additional functions here] end

	public componentDidMount() {
		this.getOrAddLatestVersion().then(version => this.selectVersion(version.id));

		// % protected region % [Add any additional componentDidMount logic here] off begin
		// % protected region % [Add any additional componentDidMount logic here] end
	}

	public render() {
		// % protected region % [override render function here] off begin
		if (this.selectedVersion) {
			return (
				<section className={classNames('forms-behaviour', this.props.className)}>
					<section className='header-bar'>
						<div className='version-details'>
							<h2>Create forms</h2>
						</div>
						<div className='tabs'>
							<ul>
								<li className={this.selectedTab === 'build' ? 'active' : ''}><a onClick={this.changeTab('build')}>Build</a></li>
								<li className={this.selectedTab === 'preview' ? 'active' : ''}><a onClick={this.changeTab('preview')}>Preview</a></li>
							</ul>
						</div>
					</section>

					{this.renderCenter(this.selectedVersion)}

					<section aria-label="action-bar" className="action-bar">
						<div className='form-information'>
							{/*<p> Form status <span className={classNames('form-status', this.isPublishedVersion ? 'published' : 'draft')}>{this.isPublishedVersion ? 'Published ' : 'Draft '}</span> </p>*/}
							<If condition={this.selectedVersion.modified !== undefined}>
								<p> Last updated <span className='italics'>{moment(this.selectedVersion.modified).format('YYYY-MM-DD HH:mm:ss')}</span> </p>
							</If>
						</div>
						<ButtonGroup>
							<If condition={this.props.onCancel !== undefined}>
								<Button onClick={this.props.onCancel} className='btn--outline'>Cancel</Button>
							</If>
							<If condition={this.props.onSaveDraft !== undefined}>
								<Button onClick={this.onSaveDraft} className='btn--outline'>Save Draft</Button>
							</If>
							<If condition={this.props.onSavePublish !== undefined}>
								<Button onClick={this.onSavePublish} className='btn--solid'>Save and Publish</Button>
							</If>
						</ButtonGroup>
					</section>
				</section>
			);
		}

		return "No Version";
		// % protected region % [override render function here] end
	}

	// % protected region % [Modify renderCenter here] off begin
	private renderCenter = (version: FormVersion) => {
		switch (this.selectedTab) {
			case 'build': return <FormSlideBuilder formVersion={version}/>;
			case 'preview': return <FormTile model={this.previewData} schema={version.formData} className="forms-preview" />
		}
	}
	// % protected region % [Modify renderCenter here] end
}
