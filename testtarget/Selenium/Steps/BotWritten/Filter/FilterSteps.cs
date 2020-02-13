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
using System.Collections.ObjectModel;
using APITests.Tests.BotWritten;
using APITests.Setup;
using APITests.EntityObjects.Models;
using APITests.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Filter
{
	public class FilterDateRange
	{
		public DateTime FromDate;
		public DateTime ToDate;
	}

	[Binding]
	public sealed class FilterSteps
	{
		private readonly GenericEntityPage _genericEntityPage;
		private readonly ContextConfiguration _contextConfiguration;
		private readonly IWebDriver _driver;
		private readonly IWait<IWebDriver> _driverWait;
		private readonly bool _isFastText;
		private readonly EntityFactory _entityFactory = null;
		private FilterDateRange createdDateRange = null;
		private FilterDateRange modifiedDateRange = null;
		private readonly BaseEntity _createdEntityForTestFiltering = null;
		//private string _entityName;

		public FilterSteps(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_genericEntityPage = new GenericEntityPage(_contextConfiguration);
			_driver = _contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
		}

		[When("I enter the current date plus (.*) days to filter created date and click apply filters")]
		public void WhenEnterFilterCreatedAndClickApply(int addDays = 0)
		{
			var today = DateTime.Now.Date;
			var date = today.AddDays(addDays);

			createdDateRange = new FilterDateRange { FromDate = date, ToDate = date };
			new DatePickerComponent(_contextConfiguration, "filter-created .flatpickr-input").SetDateRange(date, date);

			_genericEntityPage.ApplyFilterButton.Click();
		}

		[When("I enter the current date plus (.*) days to filter modified date and click apply filters")]
		public void WhenEnterFilterModifiedAndClickApply(int addDays = 0)
		{
			var today = DateTime.Now.Date;
			var date = today.AddDays(addDays);

			modifiedDateRange = new FilterDateRange { FromDate = date, ToDate = date };
			new DatePickerComponent(_contextConfiguration, "filter-modified .flatpickr-input").SetDateRange(date, date);

			_genericEntityPage.ApplyFilterButton.Click();
		}

		[When("I enter the current date plus (.*) days to filter created and modified date and click apply filters")]
		public void WhenEnterFilterCreatedAndModifedAndClickApply(int addDays = 0)
		{
			var today = DateTime.Now.Date;
			var date = today.AddDays(addDays);

			createdDateRange = new FilterDateRange { FromDate = date, ToDate = date };
			new DatePickerComponent(_contextConfiguration, "filter-created .flatpickr-input").SetDateRange(date, date);

			modifiedDateRange = new FilterDateRange { FromDate = date, ToDate = date };
			new DatePickerComponent(_contextConfiguration, "filter-modified .flatpickr-input").SetDateRange(date, date);

			_genericEntityPage.ApplyFilterButton.Click();
		}

		[StepDefinition("Each row is within the appllied current date range filters")]
		public void EarchRowIsWithinTheDataRangeFilters()
		{
			try
			{
				WaitUtils.elementState(_driverWait, By.XPath("//tr[contains(@class,'collection__item')]"), ElementState.EXISTS);
			}
			catch (NoSuchElementException)
			{
				Assert.True(false);
			}

			var rows = _genericEntityPage.CollectionTable.FindElements(By.CssSelector("tbody > tr"));

			var isAllWithinRanges = true;

			if (createdDateRange != null)
			{
				foreach (var row in rows)
				{
					var createdDate = DateTime.Parse(row.GetAttribute("data-created"));
					if(createdDate < createdDateRange.FromDate || createdDate > createdDateRange.ToDate)
					{
						isAllWithinRanges = false;
						break;
					}
				}
			}

			if (isAllWithinRanges && modifiedDateRange != null)
			{
				foreach (var row in rows)
				{
					var modifiedDate = DateTime.Parse(row.GetAttribute("data-modified"));
					if (modifiedDate < modifiedDateRange.FromDate || modifiedDate > modifiedDateRange.ToDate)
					{
						isAllWithinRanges = false;
						break;
					}
				}
			}

			Assert.True(isAllWithinRanges);
		}

		[StepDefinition("No row is within the appllied current date range filters")]
		public void NoRowIsWithinTheDataRangeFilters()
		{
			var isEmpty = WaitUtils.elementState(_driverWait, By.XPath("//tr[contains(@class,'collection__item')]"), ElementState.NOT_EXIST);
			Assert.True(isEmpty);
		}

		[When("I enter the enum filter (.*) with the same value in the entity just created and click")]
		public void WhenEnterEnumFilterWithTheValueCreated(string enumColumnName)
		{
			if (_entityFactory == null)
			{
				throw new Exception("_entityFactory has not been instantiated");
			}

			string enumValue = _entityFactory.GetEnumValue(_createdEntityForTestFiltering, enumColumnName);
			TypingUtils.InputEntityAttributeByClass(_driver, $"filter-{enumColumnName}", enumValue, _isFastText);

			var builder = new Actions(_contextConfiguration.WebDriver);
			builder.SendKeys(Keys.Enter).Perform();
			_genericEntityPage.ApplyFilterButton.Click();
		}

		[Then("The enum value created for (.*) is in each row of the the collection content")]
		public void TheStringToSearchIsInEachOfTheCollectionContent(string enumColumnName)
		{
			var enumValue = _entityFactory.GetEnumValue(_createdEntityForTestFiltering, enumColumnName);
			var isInEachRow = _genericEntityPage.TheEnumStringIsInEachOfTheRowContent(enumColumnName, enumValue, _genericEntityPage.CollectionTable);
			Assert.True(isInEachRow);
		}

		[Given("I have (.*) valid (.*) entities with fixed string values (.*)")]
		public void IHaveValidEntitiesWithFixedStrValues(int numEntities, string entityName, string fixedValues)
		{
			var _entityFactory = new EntityFactory(entityName, fixedValues);
			_entityFactory.ConstructAndSave(_contextConfiguration.TestOutputHelper, numEntities);
		}
	}
}