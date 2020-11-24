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
import classNames from 'classnames';
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

/**
 * Elements that are defined on the body content context.
 */
export interface BodyContentContextElements {
	/** The body content div element ref */
	element: HTMLElement | null;
	// % protected region % [Add any extra BodyContentContextElements here] off begin
	// % protected region % [Add any extra BodyContentContextElements here] end
}

/**
 * The body content context. This is provided to allow children of the body content access the body content tag.
 */
export const BodyContentContext = React.createContext<BodyContentContextElements>({
	element: null,
	// % protected region % [Add any extra BodyContentContext defaults here] off begin
	// % protected region % [Add any extra BodyContentContext defaults here] end
});

/**
 * Props for the body content.
 */
export interface BodyContentProps extends React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement> {
	// % protected region % [Add any extra BodyContentProps here] off begin
	// % protected region % [Add any extra BodyContentProps here] end
}

/**
 * The body content component. This contains the content of every page.
 */
const BodyContent = (props: BodyContentProps) => {
	// % protected region % [Customise the BodyContent component here] off begin
	const [ el, setEl ] = React.useState<HTMLDivElement | null>(null)

	return (
		<div {...props} className={classNames("body-content", props.className)} ref={ref => setEl(ref)}>
			<BodyContentContext.Provider value={{element: el}}>
				{props.children}
			</BodyContentContext.Provider>
		</div>
	);
	// % protected region % [Customise the BodyContent component here] end
}

// % protected region % [Customise the default export here] off begin
export default BodyContent;
// % protected region % [Customise the default export here] end
