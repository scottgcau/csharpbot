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
	public class LeagueDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// many associations web elements

		// one association web elements
		private static By sportElementBy => By.XPath("//*[contains(@class, 'sportId')]//div[contains(@class, 'dropdown__single-value')]/span");
		private static By sportInputElementBy => By.XPath("//*[contains(@class, 'sportId')]/div/div//*/input");

		// self reference web element

		//FlatPickr Elements

		//Attribute Headers
		//private IWebElement AllHeaders => _driver.FindElementExt(By.XPath("//tr[@class='list__header']"));
		private readonly League _league;

		//Attribute Header Titles
		private IWebElement IdHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Id']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));
		private IWebElement SportidHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='SportId']"));
		private IWebElement ShortnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='ShortName']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public LeagueDetailSection(ContextConfiguration ContextConfiguration, League League = null) : base(ContextConfiguration)
		{
			_driver = ContextConfiguration.WebDriver;
			initializeSelectors();
			_driverWait = ContextConfiguration.WebDriverWait;
			_isFastText = ContextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_league = League;
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void initializeSelectors()
		{
			//outgoing Reference web elements
			//SOURCE SportId
			//get the input path as set by the selector library
			selectorDict.Add("SportElement", (selector: "//*[contains(@class, 'sportId')]/div/div//*/input", type: SelectorType.XPath));

			//Attribute web Elements
			selectorDict.Add("IdElement", (selector: "//div[contains(@class, 'id')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));
			selectorDict.Add("SportidElement", (selector: "//div[contains(@class, 'sportid')]//input", type: SelectorType.XPath));
			selectorDict.Add("ShortnameElement", (selector: "//div[contains(@class, 'shortname')]//input", type: SelectorType.XPath));



			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//SOURCE sportId

		//get the input path as set by the selector library
		private IWebElement SportElement => FindElementExt("SportElement");

		//Attribute web Elements
		private IWebElement IdElement => FindElementExt("IdElement");
		private IWebElement NameElement => FindElementExt("NameElement");
		private IWebElement SportidElement => FindElementExt("SportidElement");
		private IWebElement ShortnameElement => FindElementExt("ShortnameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Id" => IdHeaderTitle,
				"Name" => NameHeaderTitle,
				"SportId" => SportidHeaderTitle,
				"ShortName" => ShortnameHeaderTitle,
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
				case "SportId":
					return SportidElement;
				case "ShortName":
					return ShortnameElement;
				case "SportId":
					return SportElement;
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
				case "SportId":
					setSportid(int.Parse(value));
					break;
				case "ShortName":
					setShortname(value);
					break;
				case "SportId":
					SetSportId();
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
				"SportId" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'sportid')]"),
				"ShortName" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'shortname')]"),
				"SportId" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'sportId')]"),
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

		public League extractEntity()
		{
			var League = new League
			{
				Id = getId,
				Name = getName,
				Sportid = getSportid,
				Shortname = getShortname,
				SportId = GetSport(),
			};

			// % protected region % [Add any extra steps to extract an entity] off begin

			// % protected region % [Add any extra steps to extract an entity] end

			return League;
		}

		public void Apply()
		{
			setId(_league.Id);
			setName(_league.Name);
			setSportid(_league.Sportid);
			setShortname(_league.Shortname);
			SetSport(_league.SportId);
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "sport":
					return new List<Guid>() {GetSport()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// get/set self associations




		// get/set many associations

		// get/set one associations

		private Guid GetSport ()
		{
			WaitUtils.elementState(_driverWait, sportElementBy, ElementState.VISIBLE);
			var sportElement = _driver.FindElementExt(sportElementBy);
			return new Guid(sportElement.GetAttribute("data-id"));
		}

		private void SetSport(Guid guid)
		{
			WaitUtils.elementState(_driverWait, sportInputElementBy, ElementState.VISIBLE);
			var sportInputElement = _driver.FindElementExt(sportInputElementBy);

			sportInputElement.SendKeys(guid.ToString());
			WaitForDropdownOptions();
			sportInputElement.SendKeys(Keys.Return);
		}

		private void SetSportId()
		{
			SportElement.SendKeys(Keys.Return);
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

		private void setSportid (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "sportid", intValue.ToString(), _isFastText);
			}
		}

		private int? getSportid =>
			int.Parse(SportidElement.Text);

		private void setShortname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "shortname", value, _isFastText);
			ShortnameElement.SendKeys(Keys.Tab);
		}

		private String getShortname =>
			ShortnameElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}