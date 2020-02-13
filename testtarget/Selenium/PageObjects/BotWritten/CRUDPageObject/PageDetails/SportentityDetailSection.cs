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
	public class SportentityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// many associations web elements
		private static By formPagesElementBy => By.XPath("//*[contains(@class, 'formPage')]//div[contains(@class, 'dropdown__multi-value')]/span");
		private static By formPagesInputElementBy => By.XPath("//*[contains(@class, 'formPage')]/div/div//*/input");

		// one association web elements
		private static By formPageElementBy => By.XPath("//*[contains(@class, 'formPage')]//div[contains(@class, 'dropdown__single-value')]/span");
		private static By formPageInputElementBy => By.XPath("//*[contains(@class, 'formPage')]/div/div//*/input");

		// self reference web element

		//FlatPickr Elements

		//Attribute Headers
		//private IWebElement AllHeaders => _driver.FindElementExt(By.XPath("//tr[@class='list__header']"));
		private readonly Sportentity _sportentity;

		//Attribute Header Titles
		private IWebElement SportnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='SportName']"));
		private IWebElement OrderHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Order']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public SportentityDetailSection(ContextConfiguration ContextConfiguration, Sportentity Sportentity = null) : base(ContextConfiguration)
		{
			_driver = ContextConfiguration.WebDriver;
			initializeSelectors();
			_driverWait = ContextConfiguration.WebDriverWait;
			_isFastText = ContextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_sportentity = Sportentity;
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void initializeSelectors()
		{
			//outgoing Reference web elements

			//Attribute web Elements
			selectorDict.Add("SportnameElement", (selector: "//div[contains(@class, 'sportname')]//input", type: SelectorType.XPath));
			selectorDict.Add("OrderElement", (selector: "//div[contains(@class, 'order')]//input", type: SelectorType.XPath));

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));


			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement SportnameElement => FindElementExt("SportnameElement");
		private IWebElement OrderElement => FindElementExt("OrderElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"SportName" => SportnameHeaderTitle,
				"Order" => OrderHeaderTitle,
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
				case "SportName":
					return SportnameElement;
				case "Order":
					return OrderElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Name":
					setName(value);
					break;
				case "SportName":
					setSportname(value);
					break;
				case "Order":
					setOrder(int.Parse(value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By getAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'name')]"),
				"SportName" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'sportname')]"),
				"Order" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'order')]"),
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

		public Sportentity extractEntity()
		{
			var Sportentity = new Sportentity
			{
				Sportname = getSportname,
				Order = getOrder,
			};

			// % protected region % [Add any extra steps to extract an entity] off begin
			// % protected region % [Add any extra steps to extract an entity] end

			return Sportentity;
		}

		public void Apply()
		{
			setName(_sportentity.Name);
			setSportname(_sportentity.Sportname);
			setOrder(_sportentity.Order);
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "formpage":
					return GetFormPages();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// get/set self associations




		// get/set many associations

		private List<Guid> GetFormPages ()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, formPagesElementBy, ElementState.VISIBLE);
			var formPagesElement = _driver.FindElements(formPagesElementBy);

			foreach(var element in formPagesElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		private void SetFormPages(List<Guid> guids)
		{
			WaitUtils.elementState(_driverWait, formPagesInputElementBy, ElementState.VISIBLE);
			var formPagesInputElement = _driver.FindElementExt(formPagesInputElementBy);

			foreach(var guid in guids)
			{
				formPagesInputElement.SendKeys(guid.ToString());
				WaitForDropdownOptions();
				formPagesInputElement.SendKeys(Keys.Return);
			}
		}

		// get/set one associations

		private Guid GetFormPage ()
		{
			WaitUtils.elementState(_driverWait, formPageElementBy, ElementState.VISIBLE);
			var formPageElement = _driver.FindElementExt(formPageElementBy);
			return new Guid(formPageElement.GetAttribute("data-id"));
		}

		private void SetFormPage(Guid guid)
		{
			WaitUtils.elementState(_driverWait, formPageInputElementBy, ElementState.VISIBLE);
			var formPageInputElement = _driver.FindElementExt(formPageInputElementBy);

			formPageInputElement.SendKeys(guid.ToString());
			WaitForDropdownOptions();
			formPageInputElement.SendKeys(Keys.Return);
		}
		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = $"//div[contains(@class, 'dropdown__menu-list')]/div[contains(@class, 'dropdown__option')]";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void setSportname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "sportname", value, _isFastText);
			SportnameElement.SendKeys(Keys.Tab);
		}

		private String getSportname =>
			SportnameElement.Text;

		private void setOrder (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "order", intValue.ToString(), _isFastText);
			}
		}

		private int? getOrder =>
			int.Parse(OrderElement.Text);


		// Set Name for form entity
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