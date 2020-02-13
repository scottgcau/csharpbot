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
	public class SportentityFormTileDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// many associations web elements

		// one association web elements
		private static By formElementBy => By.XPath("//*[contains(@class, 'formId')]//div[contains(@class, 'dropdown__single-value')]/span");
		private static By formInputElementBy => By.XPath("//*[contains(@class, 'formId')]/div/div//*/input");

		// self reference web element

		//FlatPickr Elements

		//Attribute Headers
		//private IWebElement AllHeaders => _driver.FindElementExt(By.XPath("//tr[@class='list__header']"));
		private readonly SportentityFormTile _sportentityFormTile;

		//Attribute Header Titles
		private IWebElement TileHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Tile']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public SportentityFormTileDetailSection(ContextConfiguration ContextConfiguration, SportentityFormTile SportentityFormTile = null) : base(ContextConfiguration)
		{
			_driver = ContextConfiguration.WebDriver;
			initializeSelectors();
			_driverWait = ContextConfiguration.WebDriverWait;
			_isFastText = ContextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_sportentityFormTile = SportentityFormTile;
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void initializeSelectors()
		{
			//outgoing Reference web elements
			//SOURCE FormId
			//get the input path as set by the selector library
			selectorDict.Add("FormElement", (selector: "//*[contains(@class, 'formId')]/div/div//*/input", type: SelectorType.XPath));

			//Attribute web Elements
			selectorDict.Add("TileElement", (selector: "//div[contains(@class, 'tile')]//input", type: SelectorType.XPath));



			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//SOURCE formId

		//get the input path as set by the selector library
		private IWebElement FormElement => FindElementExt("FormElement");

		//Attribute web Elements
		private IWebElement TileElement => FindElementExt("TileElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Tile" => TileHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Tile":
					return TileElement;
				case "FormId":
					return FormElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Tile":
					setTile(value);
					break;
				case "FormId":
					SetFormId();
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By getAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Tile" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'tile')]"),
				"FormId" => WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, "//div[contains(@class, 'formId')]"),
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

		public SportentityFormTile extractEntity()
		{
			var SportentityFormTile = new SportentityFormTile
			{
				Tile = getTile,
				FormId = GetForm(),
			};

			// % protected region % [Add any extra steps to extract an entity] off begin
			// % protected region % [Add any extra steps to extract an entity] end

			return SportentityFormTile;
		}

		public void Apply()
		{
			setTile(_sportentityFormTile.Tile);
			SetForm(_sportentityFormTile.FormId);
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "form":
					return new List<Guid>() {GetForm()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// get/set self associations




		// get/set many associations

		// get/set one associations

		private Guid GetForm ()
		{
			WaitUtils.elementState(_driverWait, formElementBy, ElementState.VISIBLE);
			var formElement = _driver.FindElementExt(formElementBy);
			return new Guid(formElement.GetAttribute("data-id"));
		}

		private void SetForm(Guid guid)
		{
			WaitUtils.elementState(_driverWait, formInputElementBy, ElementState.VISIBLE);
			var formInputElement = _driver.FindElementExt(formInputElementBy);

			formInputElement.SendKeys(guid.ToString());
			WaitForDropdownOptions();
			formInputElement.SendKeys(Keys.Return);
		}

		private void SetFormId()
		{
			FormElement.SendKeys(Keys.Return);
		}
		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = $"//div[contains(@class, 'dropdown__menu-list')]/div[contains(@class, 'dropdown__option')]";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void setTile (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "tile", value, _isFastText);
			TileElement.SendKeys(Keys.Tab);
		}

		private String getTile =>
			TileElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}