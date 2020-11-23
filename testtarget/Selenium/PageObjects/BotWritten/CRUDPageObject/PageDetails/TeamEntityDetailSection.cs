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
	public class TeamEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By RosterssElementBy => By.XPath("//*[contains(@class, 'rosters')]//div[contains(@class, 'dropdown__container')]/a");
		private static By RosterssInputElementBy => By.XPath("//*[contains(@class, 'rosters')]/div/input");
		private static By LeagueIdElementBy => By.XPath("//*[contains(@class, 'league')]//div[contains(@class, 'dropdown__container')]");
		private static By LeagueIdInputElementBy => By.XPath("//*[contains(@class, 'league')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly TeamEntity _teamEntity;

		//Attribute Header Titles
		private IWebElement RepresentsHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Represents']"));
		private IWebElement FullnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='FullName']"));
		private IWebElement ShortnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='ShortName']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public TeamEntityDetailSection(ContextConfiguration contextConfiguration, TeamEntity teamEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_teamEntity = teamEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("RepresentsElement", (selector: "//div[contains(@class, 'represents')]//input", type: SelectorType.XPath));
			selectorDict.Add("FullnameElement", (selector: "//div[contains(@class, 'fullname')]//input", type: SelectorType.XPath));
			selectorDict.Add("ShortnameElement", (selector: "//div[contains(@class, 'shortname')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("RostersElement", (selector: ".input-group__dropdown.rosterss > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("LeagueElement", (selector: ".input-group__dropdown.leagueId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement LeagueElement => FindElementExt("LeagueElement");

		//Attribute web Elements
		private IWebElement RepresentsElement => FindElementExt("RepresentsElement");
		private IWebElement FullnameElement => FindElementExt("FullnameElement");
		private IWebElement ShortnameElement => FindElementExt("ShortnameElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Represents" => RepresentsHeaderTitle,
				"FullName" => FullnameHeaderTitle,
				"ShortName" => ShortnameHeaderTitle,
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
				case "Represents":
					return RepresentsElement;
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
				case "Name":
					SetName(value);
					break;
				case "Represents":
					SetRepresents(value);
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
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "//div[contains(@class, 'name')]"),
				"Represents" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.represents > div > p"),
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
			SetName(_teamEntity.Name);
			SetRepresents(_teamEntity.Represents);
			SetFullname(_teamEntity.Fullname);
			SetShortname(_teamEntity.Shortname);

			if (_teamEntity.RostersIds != null)
			{
				SetRosterss(_teamEntity.RostersIds.Select(x => x.ToString()));
			}
			SetLeagueId(_teamEntity.LeagueId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "rosters":
					return GetRosterss();
				case "league":
					return new List<Guid>() {GetLeagueId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetRosterss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, RosterssInputElementBy, ElementState.VISIBLE);
			var rosterssInputElement = _driver.FindElementExt(RosterssInputElementBy);

			foreach(var id in ids)
			{
				rosterssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				rosterssInputElement.SendKeys(Keys.Return);
			}
		}

		private void SetLeagueId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, LeagueIdInputElementBy, ElementState.VISIBLE);
			var leagueIdInputElement = _driver.FindElementExt(LeagueIdInputElementBy);

			if (id != null)
			{
				leagueIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				leagueIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private List<Guid> GetRosterss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, RosterssElementBy, ElementState.VISIBLE);
			var rosterssElement = _driver.FindElements(RosterssElementBy);

			foreach(var element in rosterssElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private Guid GetLeagueId()
		{
			WaitUtils.elementState(_driverWait, LeagueIdElementBy, ElementState.VISIBLE);
			var leagueIdElement = _driver.FindElementExt(LeagueIdElementBy);
			return new Guid(leagueIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetRepresents (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "represents", value, _isFastText);
			RepresentsElement.SendKeys(Keys.Tab);
			RepresentsElement.SendKeys(Keys.Escape);
		}

		private String GetRepresents =>
			RepresentsElement.Text;

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