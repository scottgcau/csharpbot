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
import * as queryString from 'querystring';

/**
 * Handles the response from a javascript fetch call
 * @param response The response to handle
 * @returns the response as a json
 */
export function handleFetchedResponse(response: Response) {
	const responseJson = response.json();
	if (!response.ok) {
		return responseJson.then(Promise.reject.bind(Promise));
	} 
	return responseJson;
}


/**
 * Builds a url from a route and query params
 * @param route The route of the url
 * @param params The query params to use
 * @returns The built url
 */
export function buildUrl(route: string, params?: {[key: string]: string}) {
	if (params) {
		return `${route}?${queryString.stringify(params)}`;
	}

	return route;
}

// % protected region % [Add any extra fetch utils here] off begin
// % protected region % [Add any extra fetch utils here] end