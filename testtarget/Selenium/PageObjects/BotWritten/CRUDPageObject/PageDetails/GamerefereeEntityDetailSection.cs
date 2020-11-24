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
using System.Linq;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class GamerefereeEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By GameIdElementBy => By.XPath("//*[contains(@class, 'game')]//div[contains(@class, 'dropdown__container')]");
		private static By GameIdInputElementBy => By.XPath("//*[contains(@class, 'game')]/div/input");
		private static By PersonIdElementBy => By.XPath("//*[contains(@class, 'person')]//div[contains(@class, 'dropdown__container')]");
		private static By PersonIdInputElementBy => By.XPath("//*[contains(@class, 'person')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly GamerefereeEntity _gamerefereeEntity;

		//Attribute Header Titles
		private IWebElement HeadrefereeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HeadReferee']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public GamerefereeEntityDetailSection(ContextConfiguration contextConfiguration, GamerefereeEntity gamerefereeEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_gamerefereeEntity = gamerefereeEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("HeadrefereeElement", (selector: "//div[contains(@class, 'headreferee')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("GameElement", (selector: ".input-group__dropdown.gameId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("PersonElement", (selector: ".input-group__dropdown.person > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement GameElement => FindElementExt("GameElement");

		//Attribute web Elements
		private IWebElement HeadrefereeElement => FindElementExt("HeadrefereeElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"HeadReferee" => HeadrefereeHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "HeadReferee":
					return HeadrefereeElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "HeadReferee":
					SetHeadreferee(bool.Parse(value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"HeadReferee" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.headreferee > div > p"),
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = GetErrorAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public void Apply()
		{
			// % protected region % [Configure entity application here] off begin
			SetHeadreferee(_gamerefereeEntity.Headreferee);

			SetGameId(_gamerefereeEntity.GameId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "game":
					return new List<Guid>() {GetGameId()};
				case "person":
					return new List<Guid>() {GetPersonId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetGameId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, GameIdInputElementBy, ElementState.VISIBLE);
			var gameIdInputElement = _driver.FindElementExt(GameIdInputElementBy);

			if (id != null)
			{
				gameIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				gameIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetGameId()
		{
			WaitUtils.elementState(_driverWait, GameIdElementBy, ElementState.VISIBLE);
			var gameIdElement = _driver.FindElementExt(GameIdElementBy);
			return new Guid(gameIdElement.GetAttribute("data-id"));
		}
		private Guid GetPersonId()
		{
			WaitUtils.elementState(_driverWait, PersonIdElementBy, ElementState.VISIBLE);
			var personIdElement = _driver.FindElementExt(PersonIdElementBy);
			return new Guid(personIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetHeadreferee (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (HeadrefereeElement.Selected != boolValue) {
					HeadrefereeElement.Click();
				}
			}
		}

		private Boolean? GetHeadreferee =>
			HeadrefereeElement.Selected;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}