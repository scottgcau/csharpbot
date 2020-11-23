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
	public class GameEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By VenueIdElementBy => By.XPath("//*[contains(@class, 'venue')]//div[contains(@class, 'dropdown__container')]");
		private static By VenueIdInputElementBy => By.XPath("//*[contains(@class, 'venue')]/div/input");
		private static By ScheduleIdElementBy => By.XPath("//*[contains(@class, 'schedule')]//div[contains(@class, 'dropdown__container')]");
		private static By ScheduleIdInputElementBy => By.XPath("//*[contains(@class, 'schedule')]/div/input");
		private static By RefereessElementBy => By.XPath("//*[contains(@class, 'referees')]//div[contains(@class, 'dropdown__container')]/a");
		private static By RefereessInputElementBy => By.XPath("//*[contains(@class, 'referees')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent DatestartElement => new DateTimePickerComponent(_contextConfiguration, "datestart");

		//Attribute Headers
		private readonly GameEntity _gameEntity;

		//Attribute Header Titles
		private IWebElement DatestartHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='DateStart']"));
		private IWebElement HometeamidHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeTeamId']"));
		private IWebElement AwayteamidHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayTeamId']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public GameEntityDetailSection(ContextConfiguration contextConfiguration, GameEntity gameEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_gameEntity = gameEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("HometeamidElement", (selector: "//div[contains(@class, 'hometeamid')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwayteamidElement", (selector: "//div[contains(@class, 'awayteamid')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("VenueElement", (selector: ".input-group__dropdown.venueId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("ScheduleElement", (selector: ".input-group__dropdown.scheduleId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("RefereesElement", (selector: ".input-group__dropdown.refereess > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement VenueElement => FindElementExt("VenueElement");
		//get the input path as set by the selector library
		private IWebElement ScheduleElement => FindElementExt("ScheduleElement");

		//Attribute web Elements
		private IWebElement HometeamidElement => FindElementExt("HometeamidElement");
		private IWebElement AwayteamidElement => FindElementExt("AwayteamidElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"DateStart" => DatestartHeaderTitle,
				"HomeTeamId" => HometeamidHeaderTitle,
				"AwayTeamId" => AwayteamidHeaderTitle,
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
				case "DateStart":
					return DatestartElement.DateTimePickerElement;
				case "HomeTeamId":
					return HometeamidElement;
				case "AwayTeamId":
					return AwayteamidElement;
				case "ScheduleId":
					return ScheduleElement;
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
				case "DateStart":
					SetDatestart(Convert.ToDateTime(value));
					break;
				case "HomeTeamId":
					int? hometeamid = null;
					if (int.TryParse(value, out var intHometeamid))
					{
						hometeamid = intHometeamid;
					}
					SetHometeamid(hometeamid);
					break;
				case "AwayTeamId":
					int? awayteamid = null;
					if (int.TryParse(value, out var intAwayteamid))
					{
						awayteamid = intAwayteamid;
					}
					SetAwayteamid(awayteamid);
					break;
				case "ScheduleId":
					SetScheduleId(value);
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
				"DateStart" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.datestart > div > p"),
				"HomeTeamId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.hometeamid > div > p"),
				"AwayTeamId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awayteamid > div > p"),
				"ScheduleId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.scheduleId > div > p"),
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
			SetName(_gameEntity.Name);
			SetDatestart(_gameEntity.Datestart);
			SetHometeamid(_gameEntity.Hometeamid);
			SetAwayteamid(_gameEntity.Awayteamid);

			SetVenueId(_gameEntity.VenueId?.ToString());
			SetScheduleId(_gameEntity.ScheduleId.ToString());
			if (_gameEntity.RefereesIds != null)
			{
				SetRefereess(_gameEntity.RefereesIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "venue":
					return new List<Guid>() {GetVenueId()};
				case "schedule":
					return new List<Guid>() {GetScheduleId()};
				case "referees":
					return GetRefereess();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetVenueId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, VenueIdInputElementBy, ElementState.VISIBLE);
			var venueIdInputElement = _driver.FindElementExt(VenueIdInputElementBy);

			if (id != null)
			{
				venueIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				venueIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetScheduleId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, ScheduleIdInputElementBy, ElementState.VISIBLE);
			var scheduleIdInputElement = _driver.FindElementExt(ScheduleIdInputElementBy);

			scheduleIdInputElement.SendKeys(id);
			WaitForDropdownOptions();
			WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option'][@data-id='{id}']"), ElementState.EXISTS);
			scheduleIdInputElement.SendKeys(Keys.Return);
		}
		private void SetRefereess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, RefereessInputElementBy, ElementState.VISIBLE);
			var refereessInputElement = _driver.FindElementExt(RefereessInputElementBy);

			foreach(var id in ids)
			{
				refereessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				refereessInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private Guid GetVenueId()
		{
			WaitUtils.elementState(_driverWait, VenueIdElementBy, ElementState.VISIBLE);
			var venueIdElement = _driver.FindElementExt(VenueIdElementBy);
			return new Guid(venueIdElement.GetAttribute("data-id"));
		}
		private Guid GetScheduleId()
		{
			WaitUtils.elementState(_driverWait, ScheduleIdElementBy, ElementState.VISIBLE);
			var scheduleIdElement = _driver.FindElementExt(ScheduleIdElementBy);
			return new Guid(scheduleIdElement.GetAttribute("data-id"));
		}
		private List<Guid> GetRefereess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, RefereessElementBy, ElementState.VISIBLE);
			var refereessElement = _driver.FindElements(RefereessElementBy);

			foreach(var element in refereessElement)
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

		private void SetDatestart (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DatestartElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDatestart =>
			Convert.ToDateTime(DatestartElement.DateTimePickerElement.Text);
		private void SetHometeamid (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "hometeamid", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHometeamid =>
			int.Parse(HometeamidElement.Text);

		private void SetAwayteamid (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awayteamid", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwayteamid =>
			int.Parse(AwayteamidElement.Text);


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