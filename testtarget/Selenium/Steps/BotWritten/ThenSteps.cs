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

using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.PageObjects;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten
{
	[Binding]
	public sealed class ThenSteps
	{
		private readonly GenericEntityPage _genericEntityPage;
		private readonly ContextConfiguration _contextConfiguration;

		 public ThenSteps(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_genericEntityPage = new GenericEntityPage(_contextConfiguration);
			// % protected region % [Add any additional setup options here] off begin
			// % protected region % [Add any additional setup options here] end
		}

		[Then("I assert that I am on the (.*) backend page")]
		public void GivenINavigateToTheBackendPage(string pageName)
		{
			var page = new GenericEntityPage(pageName.RemoveWordsSpacing(), _contextConfiguration);
			page.AssertOnPage();
		}

		[Then("(.*) entities on current page should be selected")]
		public void numEntitiesOnCurrentPageShouldBeSelected(int numSelected)
		{
			int NumCheckedEntitiesOnPage = _genericEntityPage.NumCheckedEntitiesOnPage();
			Assert.Equal(numSelected, NumCheckedEntitiesOnPage);
		}

		[Then(@"I click the bulk bar cancel button")]
		public void ThenIClickTheCancelButtonOnTheBar()
		{
			_genericEntityPage.BulkCancelButton.Click();
		}

		[Then("The bulk options bar shows up with correct information")]
		public void TheBulkOptionsBarShowsUpWIthCorrectInformation()
		{
			WaitUtils.elementState(_contextConfiguration.WebDriverWait, _genericEntityPage.GetWebElementBy("BulkDeleteButton"), ElementState.EXISTS);
			WaitUtils.elementState(_contextConfiguration.WebDriverWait, _genericEntityPage.GetWebElementBy("BulkExportButton"), ElementState.EXISTS);
			Assert.Equal(_genericEntityPage.NumEntitiesOnPage(), _genericEntityPage.NumberOfItemsSelected());
		}

		[Then(@"I assert that the Created and Modified datepickers on the (.*) page are readonly")]
		public void ThenIAssertThatTheCreatedAndModifiedDatepickersOnThePageAreReadonly(string entityName)
		{
			var entityOnEditPage = new GenericEntityEditPage(entityName, _contextConfiguration);
			Assert.True(WebElementUtils.IsReadonly(entityOnEditPage.CreateAtDatepickerField));
			Assert.True(WebElementUtils.IsReadonly(entityOnEditPage.ModifiedAtDatepickerField));
		}

		[Then("The filter panel shows up with correct information")]
		public void TheFilterPanelShowsUpWithCorrectInformation()
		{
			Assert.True(_genericEntityPage.ElementExists("CollectionFilterPanel"));
			Assert.True(_genericEntityPage.ElementExists("FilterCreatedInput"));
			Assert.True(_genericEntityPage.ElementExists("FilterModifiedInput"));
			Assert.True(_genericEntityPage.ElementExists("ApplyFilterButton"));
			Assert.True(_genericEntityPage.ElementExists("ClearFilterButton"));
		}

		[Then(@"I assert that I can see a popup displays a message: (.*)")]
		public void ThenIAssertThatICanSeeTheToasterWithAEntityAdddedSuccessMessage( string expectedSuccessMsg)
		{
			var toaster = new ToasterAlert(_contextConfiguration);
			_contextConfiguration.WebDriverWait.Until(_ => toaster.ToasterBody.Displayed);
			Assert.Equal(expectedSuccessMsg, toaster.ToasterBody.Text);
		}
	}
}
