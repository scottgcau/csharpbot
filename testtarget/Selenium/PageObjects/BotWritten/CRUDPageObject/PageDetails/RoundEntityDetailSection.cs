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
	public class RoundEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By GamessElementBy => By.XPath("//*[contains(@class, 'games')]//div[contains(@class, 'dropdown__container')]/a");
		private static By GamessInputElementBy => By.XPath("//*[contains(@class, 'games')]/div/input");
		private static By ScheduleIdElementBy => By.XPath("//*[contains(@class, 'schedule')]//div[contains(@class, 'dropdown__container')]");
		private static By ScheduleIdInputElementBy => By.XPath("//*[contains(@class, 'schedule')]/div/input");
		private static By LaddereliminationssElementBy => By.XPath("//*[contains(@class, 'laddereliminations')]//div[contains(@class, 'dropdown__container')]/a");
		private static By LaddereliminationssInputElementBy => By.XPath("//*[contains(@class, 'laddereliminations')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly RoundEntity _roundEntity;

		//Attribute Header Titles
		private IWebElement OrderHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Order']"));
		private IWebElement FullnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='FullName']"));
		private IWebElement ShortnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='ShortName']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public RoundEntityDetailSection(ContextConfiguration contextConfiguration, RoundEntity roundEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_roundEntity = roundEntity;

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
			selectorDict.Add("ShortnameElement", (selector: "//div[contains(@class, 'shortname')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("GamesElement", (selector: ".input-group__dropdown.gamess > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("ScheduleElement", (selector: ".input-group__dropdown.scheduleId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("LaddereliminationsElement", (selector: ".input-group__dropdown.laddereliminationss > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement ScheduleElement => FindElementExt("ScheduleElement");

		//Attribute web Elements
		private IWebElement OrderElement => FindElementExt("OrderElement");
		private IWebElement FullnameElement => FindElementExt("FullnameElement");
		private IWebElement ShortnameElement => FindElementExt("ShortnameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Order" => OrderHeaderTitle,
				"FullName" => FullnameHeaderTitle,
				"ShortName" => ShortnameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Order":
					return OrderElement;
				case "FullName":
					return FullnameElement;
				case "ShortName":
					return ShortnameElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
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
				case "ShortName":
					SetShortname(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Order" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.order > div > p"),
				"FullName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.fullname > div > p"),
				"ShortName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.shortname > div > p"),
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
			SetOrder(_roundEntity.Order);
			SetFullname(_roundEntity.Fullname);
			SetShortname(_roundEntity.Shortname);

			if (_roundEntity.GamesIds != null)
			{
				SetGamess(_roundEntity.GamesIds.Select(x => x.ToString()));
			}
			SetScheduleId(_roundEntity.ScheduleId?.ToString());
			if (_roundEntity.LaddereliminationsIds != null)
			{
				SetLaddereliminationss(_roundEntity.LaddereliminationsIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "games":
					return GetGamess();
				case "schedule":
					return new List<Guid>() {GetScheduleId()};
				case "laddereliminations":
					return GetLaddereliminationss();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetGamess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, GamessInputElementBy, ElementState.VISIBLE);
			var gamessInputElement = _driver.FindElementExt(GamessInputElementBy);

			foreach(var id in ids)
			{
				gamessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				gamessInputElement.SendKeys(Keys.Return);
			}
		}

		private void SetScheduleId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, ScheduleIdInputElementBy, ElementState.VISIBLE);
			var scheduleIdInputElement = _driver.FindElementExt(ScheduleIdInputElementBy);

			if (id != null)
			{
				scheduleIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				scheduleIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetLaddereliminationss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, LaddereliminationssInputElementBy, ElementState.VISIBLE);
			var laddereliminationssInputElement = _driver.FindElementExt(LaddereliminationssInputElementBy);

			foreach(var id in ids)
			{
				laddereliminationssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				laddereliminationssInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetGamess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, GamessElementBy, ElementState.VISIBLE);
			var gamessElement = _driver.FindElements(GamessElementBy);

			foreach(var element in gamessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private Guid GetScheduleId()
		{
			WaitUtils.elementState(_driverWait, ScheduleIdElementBy, ElementState.VISIBLE);
			var scheduleIdElement = _driver.FindElementExt(ScheduleIdElementBy);
			return new Guid(scheduleIdElement.GetAttribute("data-id"));
		}
		private List<Guid> GetLaddereliminationss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, LaddereliminationssElementBy, ElementState.VISIBLE);
			var laddereliminationssElement = _driver.FindElements(LaddereliminationssElementBy);

			foreach(var element in laddereliminationssElement)
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

		private void SetShortname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "shortname", value, _isFastText);
			ShortnameElement.SendKeys(Keys.Tab);
			ShortnameElement.SendKeys(Keys.Escape);
		}

		private String GetShortname =>
			ShortnameElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}