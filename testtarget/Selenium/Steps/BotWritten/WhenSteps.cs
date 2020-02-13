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

using System;
using System.Linq;
using APITests.Tests.BotWritten;
using APITests.Setup;
using SeleniumTests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using APITests.Factories;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten
{
	[Binding]
	public sealed class WhenSteps
	{
		private readonly GenericEntityPage _genericEntityPage;
		private readonly ContextConfiguration _contextConfiguration;

		public WhenSteps(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_genericEntityPage = new GenericEntityPage(_contextConfiguration);
			// % protected region % [Add any additional setup options here] off begin
			// % protected region % [Add any additional setup options here] end
		}

		[StepDefinition("I insert a (.*) into the database")]
		public void InsertEntityToDatabase(string entityName)
		{
			var entityFactory = new EntityFactory(entityName);
			entityFactory.ConstructAndSave(_contextConfiguration.TestOutputHelper);
		}

		[When("I create a (.*) (.*)")]
		public void WhenICreateAValidEntity(string validStr, string entityName)
		{
			bool isValid;

			switch(validStr)
			{
				case "valid":
					isValid = true;
					break;
				case "invalid":
					isValid = false;
					break;
				default:
					throw new Exception("Please specify whether a 'valid' or 'invalid' entity is required");
			}

			var page = new GenericEntityEditPage(entityName, _contextConfiguration);
			var factory = new EntityDetailFactory(_contextConfiguration);
			factory.ApplyDetails(entityName, isValid);
			page.SubmitButton.Click();
		}

		[When("I delete the first (.*)")]
		public void WhenIDeleteTheFirstEntity(string entityName)
		{
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			page.Navigate();
			page.DeleteTopEntity();
		}

		[When("I click on the next page button and validate page content")]
		public void WhenIClickTheNextButton()
		{
			int pageNumberBeforeClickingNextPage = _genericEntityPage.CurrentPageNumber();
			_genericEntityPage.NextPageButton.Click();
			int pageNumberAfterClickingNextPage =  _genericEntityPage.CurrentPageNumber();
			// Check if you were already on the last page before clicking next
			if (pageNumberBeforeClickingNextPage == _genericEntityPage.NumberOfPages())
			{
				// and if you were, assert that the page number has not increased after
				// pressing the next page button.
				Assert.True(pageNumberAfterClickingNextPage == _genericEntityPage.NumberOfPages());
			}
			else
			{
				// Otherwise, assert that the page number has increased by 1.
				Assert.True(pageNumberBeforeClickingNextPage + 1 == pageNumberAfterClickingNextPage);
			}
		}

		[When("I click on the previous page button and validate page content")]
		public void WhenIClickThePreviousButton()
		{
			int pageNumberBeforeClickingNextPage =  _genericEntityPage.CurrentPageNumber();
			_genericEntityPage.PrevPageButton.Click();
			int pageNumberAfterClickingNextPage =  _genericEntityPage.CurrentPageNumber();
			// Check if you were already on the first page before clicking previous page button.
			if (pageNumberBeforeClickingNextPage == 1) {
				// and if you were, assert that the page number has not decreased
				// after clicking the prev page button.
				Assert.Equal(1,pageNumberAfterClickingNextPage);
			} else {
				// Otherwise, assert that the page number has decreased by 1.
				Assert.Equal(pageNumberAfterClickingNextPage+1, pageNumberBeforeClickingNextPage);
			}
		}

		[When("I click on the last page button and validate page content")]
		public void WhenIClickTheLastButton()
		{
			_genericEntityPage.LastPageButton.Click();
			Assert.Equal(_genericEntityPage.CurrentPageNumber(), _genericEntityPage.NumberOfPages());
		}

		[When("I click on the first page button and validate page content")]
		public void WhenIClickTheFirstButton()
		{
			_genericEntityPage.FirstPageButton.Click();
			Assert.Equal(1,_genericEntityPage.CurrentPageNumber());
		}

		[When("I select all entities on (.*)")]
		public void WhenIClickTheSelectAllCheckbox(string selectType)
		{
			if (selectType.Contains("all pages"))
			{
				_genericEntityPage.ClickSelectAllItemsButton();
			}
			else
			{
				_genericEntityPage.SelectAllCheckbox.Click();
			}
		}

		[When("I unselect (.*) entities")]
		public void WhenIUnselectEntities(int numEntities)
		{
			while(_genericEntityPage.NumUncheckedEntitiesOnPage() < numEntities)
			{
				_genericEntityPage.CheckedEntities().ToList()[0].SelectCheckbox.Click();
			}
		}

		[When("I click the select all check box")]
		public void WhenIClickTheSelectAllCheckBox()
		{
			_genericEntityPage.SelectAllCheckbox.Click();
		}

		[Then("I (.*) the confirmation box")]
		public void IPerformActionOnConfirmationBox(UserActionType userAction)
		{
			switch (userAction) {
				case UserActionType.ACCEPT:
					AlertBoxUtils.AlertBoxHandler(_contextConfiguration.WebDriver, true);
					break;
				case UserActionType.DISMISS:
				case UserActionType.CLOSE:
						AlertBoxUtils.AlertBoxHandler(_contextConfiguration.WebDriver, false);
					break;
				default:
					throw new Exception("Unable to determine required action on Alert box");
			}
		}

		[When("I insert a valid (.*), search for it and delete it")]
		public void IInsertAValidEntityAndSearchForIt(string entityName)
		{
			// Insert the row
			var entityFactory = new EntityFactory(entityName);
			var entity = entityFactory.ConstructAndSave(_contextConfiguration.TestOutputHelper);

			// Search for it using GUID
			_genericEntityPage.SearchInput.SendKeys(entity.Id.ToString());
			_genericEntityPage.SearchButton.Click();
			_contextConfiguration.WebDriverWait.Until(_ => _genericEntityPage.TotalEntities() == 1);
			Assert.Equal(1,_genericEntityPage.TotalEntities());

			// Delete it
			_genericEntityPage.DeleteTopEntity();
			_contextConfiguration.WebDriverWait.Until(_ => _genericEntityPage.TotalEntities() == 0);
			Assert.Equal(0, _genericEntityPage.TotalEntities());
		}

		[When("I open the create (.*) form And I fill it with valid data")]
		public void IOpenTheCreateEntityForm(string entityName)
		{
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			page.ClickCreateButton();
			var factory = new EntityDetailFactory(_contextConfiguration);
			factory.ApplyDetails(entityName, true);
		}

		[When("I click the filter Button on the (.*) page")]
		public void WhenIClickOnFilterButton(string entityName)
		{
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			page.FilterButton.Click();
		}

		[When("I enter the string (.*) to search and click filter button")]
		public void WhenIEnterStringToSearch(string stringToSearch)
		{
			_genericEntityPage.SearchInput.SendKeys(stringToSearch);
			_genericEntityPage.SearchButton.Click();
		}

		[When(@"I edit the first entity row")]
		public void WhenIEditTheFirstRowAndNavigateToEditPage()
		{
			var entityOnPage = new EntityOnPage(_contextConfiguration, _genericEntityPage.EntityTable);
			entityOnPage.EditButton.Click();
		}
	}
}