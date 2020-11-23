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
	public class VenueEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By GamessElementBy => By.XPath("//*[contains(@class, 'games')]//div[contains(@class, 'dropdown__container')]/a");
		private static By GamessInputElementBy => By.XPath("//*[contains(@class, 'games')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly VenueEntity _venueEntity;

		//Attribute Header Titles
		private IWebElement FullnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='FullName']"));
		private IWebElement ShortnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='ShortName']"));
		private IWebElement AddressHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Address']"));
		private IWebElement LatHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Lat']"));
		private IWebElement LonHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Lon']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public VenueEntityDetailSection(ContextConfiguration contextConfiguration, VenueEntity venueEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_venueEntity = venueEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FullnameElement", (selector: "//div[contains(@class, 'fullname')]//input", type: SelectorType.XPath));
			selectorDict.Add("ShortnameElement", (selector: "//div[contains(@class, 'shortname')]//input", type: SelectorType.XPath));
			selectorDict.Add("AddressElement", (selector: "//div[contains(@class, 'address')]//input", type: SelectorType.XPath));
			selectorDict.Add("LatElement", (selector: "//div[contains(@class, 'lat')]//input", type: SelectorType.XPath));
			selectorDict.Add("LonElement", (selector: "//div[contains(@class, 'lon')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("GamesElement", (selector: ".input-group__dropdown.gamess > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Form Entity specific web Element
			selectorDict.Add("NameElement", (selector: "div.name > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement FullnameElement => FindElementExt("FullnameElement");
		private IWebElement ShortnameElement => FindElementExt("ShortnameElement");
		private IWebElement AddressElement => FindElementExt("AddressElement");
		private IWebElement LatElement => FindElementExt("LatElement");
		private IWebElement LonElement => FindElementExt("LonElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"FullName" => FullnameHeaderTitle,
				"ShortName" => ShortnameHeaderTitle,
				"Address" => AddressHeaderTitle,
				"Lat" => LatHeaderTitle,
				"Lon" => LonHeaderTitle,
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
				case "FullName":
					return FullnameElement;
				case "ShortName":
					return ShortnameElement;
				case "Address":
					return AddressElement;
				case "Lat":
					return LatElement;
				case "Lon":
					return LonElement;
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
				case "FullName":
					SetFullname(value);
					break;
				case "ShortName":
					SetShortname(value);
					break;
				case "Address":
					SetAddress(value);
					break;
				case "Lat":
					SetLat(Convert.ToDouble(value));
					break;
				case "Lon":
					SetLon(Convert.ToDouble(value));
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
				"FullName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.fullname > div > p"),
				"ShortName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.shortname > div > p"),
				"Address" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.address > div > p"),
				"Lat" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.lat > div > p"),
				"Lon" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.lon > div > p"),
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
			SetName(_venueEntity.Name);
			SetFullname(_venueEntity.Fullname);
			SetShortname(_venueEntity.Shortname);
			SetAddress(_venueEntity.Address);
			SetLat(_venueEntity.Lat);
			SetLon(_venueEntity.Lon);

			if (_venueEntity.GamesIds != null)
			{
				SetGamess(_venueEntity.GamesIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "games":
					return GetGamess();
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

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

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

		private void SetAddress (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "address", value, _isFastText);
			AddressElement.SendKeys(Keys.Tab);
			AddressElement.SendKeys(Keys.Escape);
		}

		private String GetAddress =>
			AddressElement.Text;

		private void SetLat (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "lat", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetLat =>
			Convert.ToDouble(LatElement.Text);
		private void SetLon (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "lon", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetLon =>
			Convert.ToDouble(LonElement.Text);

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