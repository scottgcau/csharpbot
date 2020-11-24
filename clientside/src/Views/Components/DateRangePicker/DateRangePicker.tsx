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
import { observer } from 'mobx-react';
import { DateTimePicker, IDateTimePickerProps } from '../DateTimePicker/DateTimePicker';
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

/** DateRangePicker-specific properties. Extend as necessary. */
export interface IDateRangePickerProps<T> extends IDateTimePickerProps<T> {}

/**
 * DateRangePicker Component. Wraps DateTimePicker, which in turn wraps Flatpickr.
 * See IDateTimePickerProps for root property definitions. Can pass Flatpickr
 * properties that are not implemented by this interface via this.flatpickrProps.
 */
@observer
export class DateRangePicker<T> extends React.Component<IDateRangePickerProps<T>> {

	// % protected region % [Override render here] off begin
	public render() {

		return (
			<DateTimePicker
				/* The two options below are only applied if the humanReadable
				 * property is set to true on Component instantiation. */
				altFormat="j F, Y"
				dateFormat="Y-m-d"
				/* Set the Flatpickr to allow selection of a date range. */
				mode="range"
				enableTime={false}
				{...this.props}
			/>
		);
	}
	// % protected region % [Override render here] end
}