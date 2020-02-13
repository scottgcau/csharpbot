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
import { Form } from './Schema/Question';
import { FormVersion } from './FormVersion';
import { IFetchArgs, IModelAttributes, Model } from '../Models/Model';

export type getPublishedVersionFn = (includeSubmissions?: boolean) => Promise<Form | undefined>;
export type getAllVersionsFn = (includeSubmissions?: boolean, conditions?: IFetchArgs<{}>) => Promise<Form[]>;

export interface FormEntityAttributes {

	/**
	 * The name of the form
	 */
	name: string;
	/**
	 * All the versions for this form. This is not fetched by default.
	 */
	formVersions?: FormVersion[];
	/**
	 * The id of the published version
	 */
	publishedVersionId?: string;
	/**
	 * The published form version. This is fetched by default.
	 */
	publishedVersion?: FormVersion;

}

export interface FormEntity extends FormEntityAttributes{
	assignAttributes: (attributes?: Partial<IModelAttributes & FormEntityAttributes>) => void;
	/**
	 * Gets the published version of this form entity. This will refetch the version from the server.
	 *
	 * @param includeSubmissions Should the submissions be included with the fetched. If the current user does not have
	 * permissions to access the submissions this should not retrieve any but not error.
	 */
	getPublishedVersion: getPublishedVersionFn;
	/**
	 * Gets all versions of this form. This will fetch all the versions from the server.
	 *
	 * @param includeSubmissions Should the submissions be included with the fetched. If the current user does not have
	 * permissions to access the submissions this should not retrieve any but not error.
	 * @param conditions Any conditions that should apply to this fetch
	 */
	getAllVersions: getAllVersionsFn;
	/**
	 * Gets the submission entity constructor for this form entity
	 */
	getSubmissionEntity: () => {new(): Model & SubmissionEntity};
}

export interface SubmissionEntity {
	formVersionId: string;
	formVersion: FormVersion;
	submissionData: {[key: string]: any};
}