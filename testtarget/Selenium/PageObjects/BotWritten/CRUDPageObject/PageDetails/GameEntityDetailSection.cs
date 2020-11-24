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
		private static By RoundIdElementBy => By.XPath("//*[contains(@class, 'round')]//div[contains(@class, 'dropdown__container')]");
		private static By RoundIdInputElementBy => By.XPath("//*[contains(@class, 'round')]/div/input");
		private static By GamerefereessElementBy => By.XPath("//*[contains(@class, 'gamereferees')]//div[contains(@class, 'dropdown__container')]/a");
		private static By GamerefereessInputElementBy => By.XPath("//*[contains(@class, 'gamereferees')]/div/input");
		private static By VenueIdElementBy => By.XPath("//*[contains(@class, 'venue')]//div[contains(@class, 'dropdown__container')]");
		private static By VenueIdInputElementBy => By.XPath("//*[contains(@class, 'venue')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent DatestartElement => new DateTimePickerComponent(_contextConfiguration, "datestart");

		//Attribute Headers
		private readonly GameEntity _gameEntity;

		//Attribute Header Titles
		private IWebElement DatestartHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='DateStart']"));
		private IWebElement HomepointsHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomePoints']"));
		private IWebElement AwaypointsHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayPoints']"));
		private IWebElement HometeamidHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeTeamId']"));
		private IWebElement AwayteamidHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayTeamId']"));

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
			selectorDict.Add("HomepointsElement", (selector: "//div[contains(@class, 'homepoints')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwaypointsElement", (selector: "//div[contains(@class, 'awaypoints')]//input", type: SelectorType.XPath));
			selectorDict.Add("HometeamidElement", (selector: "//div[contains(@class, 'hometeamid')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwayteamidElement", (selector: "//div[contains(@class, 'awayteamid')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("RoundElement", (selector: ".input-group__dropdown.roundId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("GamerefereesElement", (selector: ".input-group__dropdown.gamerefereess > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("VenueElement", (selector: ".input-group__dropdown.venueId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement RoundElement => FindElementExt("RoundElement");
		//get the input path as set by the selector library
		private IWebElement VenueElement => FindElementExt("VenueElement");

		//Attribute web Elements
		private IWebElement HomepointsElement => FindElementExt("HomepointsElement");
		private IWebElement AwaypointsElement => FindElementExt("AwaypointsElement");
		private IWebElement HometeamidElement => FindElementExt("HometeamidElement");
		private IWebElement AwayteamidElement => FindElementExt("AwayteamidElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"DateStart" => DatestartHeaderTitle,
				"HomePoints" => HomepointsHeaderTitle,
				"AwayPoints" => AwaypointsHeaderTitle,
				"HomeTeamId" => HometeamidHeaderTitle,
				"AwayTeamId" => AwayteamidHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "DateStart":
					return DatestartElement.DateTimePickerElement;
				case "HomePoints":
					return HomepointsElement;
				case "AwayPoints":
					return AwaypointsElement;
				case "HomeTeamId":
					return HometeamidElement;
				case "AwayTeamId":
					return AwayteamidElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "DateStart":
					SetDatestart(Convert.ToDateTime(value));
					break;
				case "HomePoints":
					int? homepoints = null;
					if (int.TryParse(value, out var intHomepoints))
					{
						homepoints = intHomepoints;
					}
					SetHomepoints(homepoints);
					break;
				case "AwayPoints":
					int? awaypoints = null;
					if (int.TryParse(value, out var intAwaypoints))
					{
						awaypoints = intAwaypoints;
					}
					SetAwaypoints(awaypoints);
					break;
				case "HomeTeamId":
					SetHometeamid(value);
					break;
				case "AwayTeamId":
					SetAwayteamid(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"DateStart" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.datestart > div > p"),
				"HomePoints" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.homepoints > div > p"),
				"AwayPoints" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awaypoints > div > p"),
				"HomeTeamId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.hometeamid > div > p"),
				"AwayTeamId" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awayteamid > div > p"),
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
			SetDatestart(_gameEntity.Datestart);
			SetHomepoints(_gameEntity.Homepoints);
			SetAwaypoints(_gameEntity.Awaypoints);
			SetHometeamid(_gameEntity.Hometeamid);
			SetAwayteamid(_gameEntity.Awayteamid);

			SetRoundId(_gameEntity.RoundId?.ToString());
			if (_gameEntity.GamerefereesIds != null)
			{
				SetGamerefereess(_gameEntity.GamerefereesIds.Select(x => x.ToString()));
			}
			SetVenueId(_gameEntity.VenueId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "round":
					return new List<Guid>() {GetRoundId()};
				case "gamereferees":
					return GetGamerefereess();
				case "venue":
					return new List<Guid>() {GetVenueId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetRoundId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, RoundIdInputElementBy, ElementState.VISIBLE);
			var roundIdInputElement = _driver.FindElementExt(RoundIdInputElementBy);

			if (id != null)
			{
				roundIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				roundIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetGamerefereess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, GamerefereessInputElementBy, ElementState.VISIBLE);
			var gamerefereessInputElement = _driver.FindElementExt(GamerefereessInputElementBy);

			foreach(var id in ids)
			{
				gamerefereessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				gamerefereessInputElement.SendKeys(Keys.Return);
			}
		}

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

		// get associations
		private Guid GetRoundId()
		{
			WaitUtils.elementState(_driverWait, RoundIdElementBy, ElementState.VISIBLE);
			var roundIdElement = _driver.FindElementExt(RoundIdElementBy);
			return new Guid(roundIdElement.GetAttribute("data-id"));
		}
		private List<Guid> GetGamerefereess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, GamerefereessElementBy, ElementState.VISIBLE);
			var gamerefereessElement = _driver.FindElements(GamerefereessElementBy);

			foreach(var element in gamerefereessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private Guid GetVenueId()
		{
			WaitUtils.elementState(_driverWait, VenueIdElementBy, ElementState.VISIBLE);
			var venueIdElement = _driver.FindElementExt(VenueIdElementBy);
			return new Guid(venueIdElement.GetAttribute("data-id"));
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
		private void SetHomepoints (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "homepoints", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHomepoints =>
			int.Parse(HomepointsElement.Text);

		private void SetAwaypoints (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awaypoints", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwaypoints =>
			int.Parse(AwaypointsElement.Text);

		private void SetHometeamid (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "hometeamid", value, _isFastText);
			HometeamidElement.SendKeys(Keys.Tab);
			HometeamidElement.SendKeys(Keys.Escape);
		}

		private String GetHometeamid =>
			HometeamidElement.Text;

		private void SetAwayteamid (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "awayteamid", value, _isFastText);
			AwayteamidElement.SendKeys(Keys.Tab);
			AwayteamidElement.SendKeys(Keys.Escape);
		}

		private String GetAwayteamid =>
			AwayteamidElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}