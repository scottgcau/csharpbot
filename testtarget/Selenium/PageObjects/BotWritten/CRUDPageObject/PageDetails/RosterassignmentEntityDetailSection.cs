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
using EntityObject.Enums;
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
	public class RosterassignmentEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By PersonIdElementBy => By.XPath("//*[contains(@class, 'person')]//div[contains(@class, 'dropdown__container')]");
		private static By PersonIdInputElementBy => By.XPath("//*[contains(@class, 'person')]/div/input");
		private static By RosterIdElementBy => By.XPath("//*[contains(@class, 'roster')]//div[contains(@class, 'dropdown__container')]");
		private static By RosterIdInputElementBy => By.XPath("//*[contains(@class, 'roster')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent DatefromElement => new DateTimePickerComponent(_contextConfiguration, "datefrom");
		private DateTimePickerComponent DatetoElement => new DateTimePickerComponent(_contextConfiguration, "dateto");

		//Attribute Headers
		private readonly RosterassignmentEntity _rosterassignmentEntity;

		//Attribute Header Titles
		private IWebElement DatefromHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='DateFrom']"));
		private IWebElement DatetoHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='DateTo']"));
		private IWebElement RoletypeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='RoleType']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public RosterassignmentEntityDetailSection(ContextConfiguration contextConfiguration, RosterassignmentEntity rosterassignmentEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_rosterassignmentEntity = rosterassignmentEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("RoletypeElement", (selector: "//div[contains(@class, 'roletype')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("PersonElement", (selector: ".input-group__dropdown.personId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("RosterElement", (selector: ".input-group__dropdown.rosterId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement PersonElement => FindElementExt("PersonElement");
		//get the input path as set by the selector library
		private IWebElement RosterElement => FindElementExt("RosterElement");

		//Attribute web Elements
		private IWebElement RoletypeElement => FindElementExt("RoletypeElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"DateFrom" => DatefromHeaderTitle,
				"DateTo" => DatetoHeaderTitle,
				"RoleType" => RoletypeHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "DateFrom":
					return DatefromElement.DateTimePickerElement;
				case "DateTo":
					return DatetoElement.DateTimePickerElement;
				case "RoleType":
					return RoletypeElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "DateFrom":
					SetDatefrom(Convert.ToDateTime(value));
					break;
				case "DateTo":
					SetDateto(Convert.ToDateTime(value));
					break;
				case "RoleType":
					SetRoletype((Roletype)Enum.Parse(typeof(Roletype), value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"DateFrom" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.datefrom > div > p"),
				"DateTo" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.dateto > div > p"),
				"RoleType" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.roletype > div > p"),
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
			SetDatefrom(_rosterassignmentEntity.Datefrom);
			SetDateto(_rosterassignmentEntity.Dateto);
			SetRoletype(_rosterassignmentEntity.Roletype);

			SetPersonId(_rosterassignmentEntity.PersonId?.ToString());
			SetRosterId(_rosterassignmentEntity.RosterId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "person":
					return new List<Guid>() {GetPersonId()};
				case "roster":
					return new List<Guid>() {GetRosterId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetPersonId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, PersonIdInputElementBy, ElementState.VISIBLE);
			var personIdInputElement = _driver.FindElementExt(PersonIdInputElementBy);

			if (id != null)
			{
				personIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				personIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetRosterId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, RosterIdInputElementBy, ElementState.VISIBLE);
			var rosterIdInputElement = _driver.FindElementExt(RosterIdInputElementBy);

			if (id != null)
			{
				rosterIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				rosterIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetPersonId()
		{
			WaitUtils.elementState(_driverWait, PersonIdElementBy, ElementState.VISIBLE);
			var personIdElement = _driver.FindElementExt(PersonIdElementBy);
			return new Guid(personIdElement.GetAttribute("data-id"));
		}
		private Guid GetRosterId()
		{
			WaitUtils.elementState(_driverWait, RosterIdElementBy, ElementState.VISIBLE);
			var rosterIdElement = _driver.FindElementExt(RosterIdElementBy);
			return new Guid(rosterIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetDatefrom (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DatefromElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDatefrom =>
			Convert.ToDateTime(DatefromElement.DateTimePickerElement.Text);
		private void SetDateto (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DatetoElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDateto =>
			Convert.ToDateTime(DatetoElement.DateTimePickerElement.Text);
		private void SetRoletype (Roletype value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "roletype", value.ToString(), _isFastText);
		}

		private Roletype GetRoletype =>
			(Roletype)Enum.Parse(typeof(Roletype), RoletypeElement.Text);
			

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}