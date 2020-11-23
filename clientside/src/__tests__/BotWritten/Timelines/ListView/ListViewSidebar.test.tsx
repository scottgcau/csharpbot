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
import React from "react";
import { mount } from 'enzyme';
import moment from 'moment';
import ListViewQuickJumpSidebar from 'Timelines/TimelineListView/ListViewSidebar';
import {ITimelineFilter} from 'Timelines/TimelineTile';
import * as Models from 'Models/Entities';

const axios = require('axios');
jest.mock('axios');


describe('List View Quick Jump Sidebar Component', () => {
	it('returns the start dates in order with the correct date format', async () => {
		// arrange
		let returnedDataArray = [
			{ startDate: "2020-06-01T00:00:00", endDate: "2020-07-01T00:00:00" },
			{ startDate: "2020-05-01T00:00:00", endDate: "2020-06-01T00:00:00" },
			{ startDate: "2020-04-01T00:00:00", endDate: "2020-05-01T00:00:00" },
			{ startDate: "2020-03-01T00:00:00", endDate: "2020-04-01T00:00:00" },
			{ startDate: "2020-02-01T00:00:00", endDate: "2020-03-01T00:00:00" },
			{ startDate: "2020-01-01T00:00:00", endDate: "2020-02-01T00:00:00" },
			{ startDate: "2019-12-01T00:00:00", endDate: "2020-01-01T00:00:00" },
		];

		let mappedDates = returnedDataArray.map(x => x.startDate);

		axios.post.mockResolvedValue({
			data: returnedDataArray
		});

		let filter: ITimelineFilter = {
			searchTerm: "",
			instanceIds: [],
			startDate: undefined,
			endDate: undefined,
			selectedActionTypes: [],
			actionTypeOptions: [],
			selectedTimelineEntity: Models.BookEntity,
			timelineEntityOptions: [],
			
		};

		//act
		const component = mount(<ListViewQuickJumpSidebar timelineFilter={filter} onQuickJump={() => null}/>);

		return Promise
			.resolve(component)
			.then(() => {
				component.update()
				for (let index = 0; index < mappedDates.length; index++) {
					// assert
					expect(component.find("li.bold").at(index + 1).text()).toEqual(moment(mappedDates[index]).format('MM/YYYY'));
				}
			});
	})
});