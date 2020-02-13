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
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class SportDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// many associations web elements
		private static By leaguessElementBy => By.XPath("//*[contains(@class, 'leagues')]//div[contains(@class, 'dropdown__multi-value')]/span");
		private static By leaguessInputElementBy => By.XPath("//*[contains(@class, 'leagues')]/div/div//*/input");

		// one association web elements
		private static By leaguesElementBy => By.XPath("//*[contains(@class, 'leagues')]//div[contains(@class, 'dropdown__single-value')]/span");
		private static By leaguesInputElementBy => By.XPath("//*[contains(@class, 'leagues')]/div/div//*/input");

		// self reference web element

		//FlatPickr Elements

		//Attribute Headers
		//private IWebElement AllHeaders => _driver.FindElementExt(By.XPath("//tr[@class='list__header']"));
		private readonly Sport _sport;

		//Attribute Header Titles
		private IWebElement IdHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Id']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public SportDetailSection(ContextConfiguration ContextConfiguration, Sport Sport = null) : base(ContextConfiguration)
		{
			_driver = ContextConfiguration.WebDriver;
			initializeSelectors();
			_driverWait = ContextConfiguration.WebDriverWait;
			_isFastText = ContextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_sport = Sport;
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void initializeSelectors()
		{
			//outgoing Reference web elements

			//Attribute web Elements
			selectorDict.Add("IdElement", (selector: "//div[contains(@class, 'id')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));



			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement IdElement => FindElementExt("IdElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Id" => IdHeaderTitle,
				"Name" => NameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Id":
					return IdElement;
				case "Name":
					return NameElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Id":
					setId(int.Parse(value));
					break;
				case "Name":
					setName(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By getAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Id" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'id')]"),
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'name')]"),
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = getAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public Sport extractEntity()
		{
			var Sport = new Sport
			{
				Id = getId,
				Name = getName,
			};

			// % protected region % [Add any extra steps to extract an entity] off begin

			// % protected region % [Add any extra steps to extract an entity] end

			return Sport;
		}

		public void Apply()
		{
			setId(_sport.Id);
			setName(_sport.Name);
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "leagues":
					return GetLeaguess();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// get/set self associations




		// get/set many associations

		private List<Guid> GetLeaguess ()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, leaguessElementBy, ElementState.VISIBLE);
			var leaguessElement = _driver.FindElements(leaguessElementBy);

			foreach(var element in leaguessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		private void SetLeaguess(List<Guid> guids)
		{
			WaitUtils.elementState(_driverWait, leaguessInputElementBy, ElementState.VISIBLE);
			var leaguessInputElement = _driver.FindElementExt(leaguessInputElementBy);

			foreach(var guid in guids)
			{
				leaguessInputElement.SendKeys(guid.ToString());
				WaitForDropdownOptions();
				leaguessInputElement.SendKeys(Keys.Return);
			}
		}

		// get/set one associations

		private Guid GetLeagues ()
		{
			WaitUtils.elementState(_driverWait, leaguesElementBy, ElementState.VISIBLE);
			var leaguesElement = _driver.FindElementExt(leaguesElementBy);
			return new Guid(leaguesElement.GetAttribute("data-id"));
		}

		private void SetLeagues(Guid guid)
		{
			WaitUtils.elementState(_driverWait, leaguesInputElementBy, ElementState.VISIBLE);
			var leaguesInputElement = _driver.FindElementExt(leaguesInputElementBy);

			leaguesInputElement.SendKeys(guid.ToString());
			WaitForDropdownOptions();
			leaguesInputElement.SendKeys(Keys.Return);
		}
		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = $"//div[contains(@class, 'dropdown__menu-list')]/div[contains(@class, 'dropdown__option')]";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void setId (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "id", intValue.ToString(), _isFastText);
			}
		}

		private int? getId =>
			int.Parse(IdElement.Text);

		private void setName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
		}

		private String getName =>
			NameElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}