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
	public class SportEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By LeaguessElementBy => By.XPath("//*[contains(@class, 'leagues')]//div[contains(@class, 'dropdown__container')]/a");
		private static By LeaguessInputElementBy => By.XPath("//*[contains(@class, 'leagues')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly SportEntity _sportEntity;

		//Attribute Header Titles
		private IWebElement OrderHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Order']"));
		private IWebElement FullnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='FullName']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public SportEntityDetailSection(ContextConfiguration contextConfiguration, SportEntity sportEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_sportEntity = sportEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("OrderElement", (selector: "//div[contains(@class, 'order')]//input", type: SelectorType.XPath));
			selectorDict.Add("FullnameElement", (selector: "//div[contains(@class, 'fullname')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("LeaguesElement", (selector: ".input-group__dropdown.leaguess > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement OrderElement => FindElementExt("OrderElement");
		private IWebElement FullnameElement => FindElementExt("FullnameElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Order" => OrderHeaderTitle,
				"FullName" => FullnameHeaderTitle,
				"Name" => NameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Name":
					return NameElement;
				case "Order":
					return OrderElement;
				case "FullName":
					return FullnameElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Name":
					SetName(value);
					break;
				case "Order":
					int? order = null;
					if (int.TryParse(value, out var intOrder))
					{
						order = intOrder;
					}
					SetOrder(order);
					break;
				case "FullName":
					SetFullname(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "//div[contains(@class, 'name')]"),
				"Order" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.order > div > p"),
				"FullName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.fullname > div > p"),
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
			SetName(_sportEntity.Name);
			SetOrder(_sportEntity.Order);
			SetFullname(_sportEntity.Fullname);

			if (_sportEntity.LeaguesIds != null)
			{
				SetLeaguess(_sportEntity.LeaguesIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
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

		// set associations
		private void SetLeaguess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, LeaguessInputElementBy, ElementState.VISIBLE);
			var leaguessInputElement = _driver.FindElementExt(LeaguessInputElementBy);

			foreach(var id in ids)
			{
				leaguessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				leaguessInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetLeaguess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, LeaguessElementBy, ElementState.VISIBLE);
			var leaguessElement = _driver.FindElements(LeaguessElementBy);

			foreach(var element in leaguessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetOrder (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "order", intValue.ToString(), _isFastText);
			}
		}

		private int? GetOrder =>
			int.Parse(OrderElement.Text);

		private void SetFullname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "fullname", value, _isFastText);
			FullnameElement.SendKeys(Keys.Tab);
			FullnameElement.SendKeys(Keys.Escape);
		}

		private String GetFullname =>
			FullnameElement.Text;


		// Set Name for form entity
		private void SetName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
		}

		private String GetName => NameElement.Text;
		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}