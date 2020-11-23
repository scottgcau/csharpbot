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

using System;
using OpenQA.Selenium;
using SeleniumTests.Enums;
using SeleniumTests.Setup;

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegisterUserSelectionPage : BasePage
	{
		public IWebElement ScheduleEntityDropdownOption => FindElementExt("ScheduleEntityDropdownOption");
		public IWebElement SeasonEntityDropdownOption => FindElementExt("SeasonEntityDropdownOption");
		public IWebElement VenueEntityDropdownOption => FindElementExt("VenueEntityDropdownOption");
		public IWebElement GameEntityDropdownOption => FindElementExt("GameEntityDropdownOption");
		public IWebElement SportEntityDropdownOption => FindElementExt("SportEntityDropdownOption");
		public IWebElement LeagueEntityDropdownOption => FindElementExt("LeagueEntityDropdownOption");
		public IWebElement TeamEntityDropdownOption => FindElementExt("TeamEntityDropdownOption");
		public IWebElement PersonEntityDropdownOption => FindElementExt("PersonEntityDropdownOption");
		public IWebElement RosterEntityDropdownOption => FindElementExt("RosterEntityDropdownOption");
		public IWebElement RosterassignmentEntityDropdownOption => FindElementExt("RosterassignmentEntityDropdownOption");
		public IWebElement ScheduleSubmissionEntityDropdownOption => FindElementExt("ScheduleSubmissionEntityDropdownOption");
		public IWebElement SeasonSubmissionEntityDropdownOption => FindElementExt("SeasonSubmissionEntityDropdownOption");
		public IWebElement VenueSubmissionEntityDropdownOption => FindElementExt("VenueSubmissionEntityDropdownOption");
		public IWebElement GameSubmissionEntityDropdownOption => FindElementExt("GameSubmissionEntityDropdownOption");
		public IWebElement SportSubmissionEntityDropdownOption => FindElementExt("SportSubmissionEntityDropdownOption");
		public IWebElement LeagueSubmissionEntityDropdownOption => FindElementExt("LeagueSubmissionEntityDropdownOption");
		public IWebElement TeamSubmissionEntityDropdownOption => FindElementExt("TeamSubmissionEntityDropdownOption");
		public IWebElement PersonSubmissionEntityDropdownOption => FindElementExt("PersonSubmissionEntityDropdownOption");
		public IWebElement RosterSubmissionEntityDropdownOption => FindElementExt("RosterSubmissionEntityDropdownOption");
		public IWebElement RosterassignmentSubmissionEntityDropdownOption => FindElementExt("RosterassignmentSubmissionEntityDropdownOption");
		public IWebElement ScheduleEntityFormTileEntityDropdownOption => FindElementExt("ScheduleEntityFormTileEntityDropdownOption");
		public IWebElement SeasonEntityFormTileEntityDropdownOption => FindElementExt("SeasonEntityFormTileEntityDropdownOption");
		public IWebElement VenueEntityFormTileEntityDropdownOption => FindElementExt("VenueEntityFormTileEntityDropdownOption");
		public IWebElement GameEntityFormTileEntityDropdownOption => FindElementExt("GameEntityFormTileEntityDropdownOption");
		public IWebElement SportEntityFormTileEntityDropdownOption => FindElementExt("SportEntityFormTileEntityDropdownOption");
		public IWebElement LeagueEntityFormTileEntityDropdownOption => FindElementExt("LeagueEntityFormTileEntityDropdownOption");
		public IWebElement TeamEntityFormTileEntityDropdownOption => FindElementExt("TeamEntityFormTileEntityDropdownOption");
		public IWebElement PersonEntityFormTileEntityDropdownOption => FindElementExt("PersonEntityFormTileEntityDropdownOption");
		public IWebElement RosterEntityFormTileEntityDropdownOption => FindElementExt("RosterEntityFormTileEntityDropdownOption");
		public IWebElement RosterassignmentEntityFormTileEntityDropdownOption => FindElementExt("RosterassignmentEntityFormTileEntityDropdownOption");
		public IWebElement RosterTimelineEventsEntityDropdownOption => FindElementExt("RosterTimelineEventsEntityDropdownOption");
		public IWebElement UserTypeDropdown => FindElementExt("UserTypeDropdown");
		public IWebElement ConfirmButton => FindElementExt("ConfirmButton");
		public IWebElement CancelButton => FindElementExt("CancelButton");

		public RegisterUserSelectionPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("ScheduleEntityDropdownOption", (selector: "//div[contains(@data-id, 'ScheduleEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SeasonEntityDropdownOption", (selector: "//div[contains(@data-id, 'SeasonEntity')]", type: SelectorType.XPath));
			selectorDict.Add("VenueEntityDropdownOption", (selector: "//div[contains(@data-id, 'VenueEntity')]", type: SelectorType.XPath));
			selectorDict.Add("GameEntityDropdownOption", (selector: "//div[contains(@data-id, 'GameEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SportEntityDropdownOption", (selector: "//div[contains(@data-id, 'SportEntity')]", type: SelectorType.XPath));
			selectorDict.Add("LeagueEntityDropdownOption", (selector: "//div[contains(@data-id, 'LeagueEntity')]", type: SelectorType.XPath));
			selectorDict.Add("TeamEntityDropdownOption", (selector: "//div[contains(@data-id, 'TeamEntity')]", type: SelectorType.XPath));
			selectorDict.Add("PersonEntityDropdownOption", (selector: "//div[contains(@data-id, 'PersonEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterassignmentEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterassignmentEntity')]", type: SelectorType.XPath));
			selectorDict.Add("ScheduleSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'ScheduleSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SeasonSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'SeasonSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("VenueSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'VenueSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("GameSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'GameSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SportSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'SportSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("LeagueSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'LeagueSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("TeamSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'TeamSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("PersonSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'PersonSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterassignmentSubmissionEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterassignmentSubmissionEntity')]", type: SelectorType.XPath));
			selectorDict.Add("ScheduleEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'ScheduleEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SeasonEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'SeasonEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("VenueEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'VenueEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("GameEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'GameEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("SportEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'SportEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("LeagueEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'LeagueEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("TeamEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'TeamEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("PersonEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'PersonEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterassignmentEntityFormTileEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterassignmentEntityFormTileEntity')]", type: SelectorType.XPath));
			selectorDict.Add("RosterTimelineEventsEntityDropdownOption", (selector: "//div[contains(@data-id, 'RosterTimelineEventsEntity')]", type: SelectorType.XPath));
			selectorDict.Add("UserTypeDropdown", (selector: "//div[contains(@class, 'input-group__dropdown')]//input", type: SelectorType.XPath));
			selectorDict.Add("ConfirmButton", (selector: "confirm_type", type: SelectorType.ID));
			selectorDict.Add("CancelButton", (selector: "cancel_register", type: SelectorType.ID));
		}

		public RegisterUserBasePage Select (UserType userType)
		{
			UserTypeDropdown.Click();

			switch (userType)
			{
				default:
					throw new Exception($"Invalid user type {userType}");
			}
		}
	}
}
