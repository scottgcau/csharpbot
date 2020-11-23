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

using SeleniumTests.Enums;
using SeleniumTests.PageObjects;
using SeleniumTests.PageObjects.BotWritten.Timelines;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Timelines
{
	[Binding]
	public sealed class TimelineSteps : BaseStepDefinition
	{
		private TimelineGraphPage _timelineGraphPage;
		private TimelineListPage _timelineListPage;
		private readonly AdminNavSection _adminNavSection;

		public TimelineSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_timelineGraphPage = new TimelineGraphPage(contextConfiguration);
			_timelineListPage = new TimelineListPage(contextConfiguration);
			_adminNavSection = new AdminNavSection(contextConfiguration);
		}
		
		[StepDefinition(@"A (.*) has a view in timeline option")]
		public void ExpectAnEntityToHaveAViewInTimelineOption(string entityName)
		{
			var genericEntityPage = new GenericEntityPage(entityName, _contextConfiguration);
			var entityOnPage = new EntityOnPage(_contextConfiguration, genericEntityPage.EntityTable);
			WaitUtils.elementState(_driverWait, genericEntityPage.EntityTable, entityOnPage.MoreButtonBy,
				ElementState.VISIBLE);
			entityOnPage.MoreButton.ClickWithWait(_driverWait);
			_driverWait.Until(x => entityOnPage.TimelineButton.Displayed);
		}
		
		[StepDefinition(@"A sidebar option for timeline is visible")]
		public void ExpectAdminSidebarTimelineOption()
		{
			var elementVisible = WaitUtils
				.elementState(_driverWait, _adminNavSection.GetWebElementBy("AdminNavIconTimelines"), ElementState.VISIBLE);
			Assert.True(elementVisible);
		}
		
		[StepDefinition(@"when I click the view in timeline option")]
		public void WhenIClickTheViewInTimelineOption()
		{
			var genericEntityPage = new GenericEntityPage("BookEntity", _contextConfiguration);
			var entityOnPage = new EntityOnPage(_contextConfiguration, genericEntityPage.EntityTable);
			entityOnPage.TimelineButton.ClickWithWait(_driverWait);
		}
		
		[StepDefinition(@"I am on the timeline graph view")]
		public void ExpectToBeOnTheTimelineGraphView()
		{
			WaitUtils.waitForPage(_driverWait); 
			Assert.Contains(_timelineGraphPage.Url,_driver.Url);
			Assert.True(_timelineGraphPage.CheckValidPageContents());
		}
		
		[StepDefinition(@"I am able to toggle from graph to list view")]
		public void ExpectToBeAbleToToggleFromGraphToListView()
		{
			_timelineGraphPage.ListViewButton.ClickWithWait(_driverWait);
			WaitUtils.waitForPage(_driverWait); 
			Assert.Contains(_timelineListPage.Url,_driver.Url);
			Assert.True(_timelineListPage.CheckValidPageContents());
		}
	}
}