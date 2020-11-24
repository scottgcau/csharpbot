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
	public class PersonEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By RosterassignmentssElementBy => By.XPath("//*[contains(@class, 'rosterassignments')]//div[contains(@class, 'dropdown__container')]/a");
		private static By RosterassignmentssInputElementBy => By.XPath("//*[contains(@class, 'rosterassignments')]/div/input");
		private static By SystemuserIdElementBy => By.XPath("//*[contains(@class, 'systemuser')]//div[contains(@class, 'dropdown__container')]");
		private static By SystemuserIdInputElementBy => By.XPath("//*[contains(@class, 'systemuser')]/div/input");
		private static By GamerefereeIdElementBy => By.XPath("//*[contains(@class, 'gamereferee')]//div[contains(@class, 'dropdown__container')]");
		private static By GamerefereeIdInputElementBy => By.XPath("//*[contains(@class, 'gamereferee')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent DateofbirthElement => new DateTimePickerComponent(_contextConfiguration, "dateofbirth");

		//Attribute Headers
		private readonly PersonEntity _personEntity;

		//Attribute Header Titles
		private IWebElement FirstnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='FirstName']"));
		private IWebElement LastnameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='LastName']"));
		private IWebElement DateofbirthHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='DateOfBirth']"));
		private IWebElement HeightHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Height']"));
		private IWebElement WeightHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Weight']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public PersonEntityDetailSection(ContextConfiguration contextConfiguration, PersonEntity personEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_personEntity = personEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FirstnameElement", (selector: "//div[contains(@class, 'firstname')]//input", type: SelectorType.XPath));
			selectorDict.Add("LastnameElement", (selector: "//div[contains(@class, 'lastname')]//input", type: SelectorType.XPath));
			selectorDict.Add("HeightElement", (selector: "//div[contains(@class, 'height')]//input", type: SelectorType.XPath));
			selectorDict.Add("WeightElement", (selector: "//div[contains(@class, 'weight')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("RosterassignmentsElement", (selector: ".input-group__dropdown.rosterassignmentss > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("SystemuserElement", (selector: ".input-group__dropdown.systemuserId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("GamerefereeElement", (selector: ".input-group__dropdown.gamerefereeId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement FirstnameElement => FindElementExt("FirstnameElement");
		private IWebElement LastnameElement => FindElementExt("LastnameElement");
		private IWebElement HeightElement => FindElementExt("HeightElement");
		private IWebElement WeightElement => FindElementExt("WeightElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"FirstName" => FirstnameHeaderTitle,
				"LastName" => LastnameHeaderTitle,
				"DateOfBirth" => DateofbirthHeaderTitle,
				"Height" => HeightHeaderTitle,
				"Weight" => WeightHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "FirstName":
					return FirstnameElement;
				case "LastName":
					return LastnameElement;
				case "DateOfBirth":
					return DateofbirthElement.DateTimePickerElement;
				case "Height":
					return HeightElement;
				case "Weight":
					return WeightElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "FirstName":
					SetFirstname(value);
					break;
				case "LastName":
					SetLastname(value);
					break;
				case "DateOfBirth":
					SetDateofbirth(Convert.ToDateTime(value));
					break;
				case "Height":
					int? height = null;
					if (int.TryParse(value, out var intHeight))
					{
						height = intHeight;
					}
					SetHeight(height);
					break;
				case "Weight":
					int? weight = null;
					if (int.TryParse(value, out var intWeight))
					{
						weight = intWeight;
					}
					SetWeight(weight);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"FirstName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.firstname > div > p"),
				"LastName" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.lastname > div > p"),
				"DateOfBirth" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.dateofbirth > div > p"),
				"Height" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.height > div > p"),
				"Weight" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.weight > div > p"),
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
			SetFirstname(_personEntity.Firstname);
			SetLastname(_personEntity.Lastname);
			SetDateofbirth(_personEntity.Dateofbirth);
			SetHeight(_personEntity.Height);
			SetWeight(_personEntity.Weight);

			if (_personEntity.RosterassignmentsIds != null)
			{
				SetRosterassignmentss(_personEntity.RosterassignmentsIds.Select(x => x.ToString()));
			}
			SetSystemuserId(_personEntity.SystemuserId?.ToString());
			SetGamerefereeId(_personEntity.GamerefereeId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "rosterassignments":
					return GetRosterassignmentss();
				case "systemuser":
					return new List<Guid>() {GetSystemuserId()};
				case "gamereferee":
					return new List<Guid>() {GetGamerefereeId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetRosterassignmentss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, RosterassignmentssInputElementBy, ElementState.VISIBLE);
			var rosterassignmentssInputElement = _driver.FindElementExt(RosterassignmentssInputElementBy);

			foreach(var id in ids)
			{
				rosterassignmentssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				rosterassignmentssInputElement.SendKeys(Keys.Return);
			}
		}

		private void SetSystemuserId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, SystemuserIdInputElementBy, ElementState.VISIBLE);
			var systemuserIdInputElement = _driver.FindElementExt(SystemuserIdInputElementBy);

			if (id != null)
			{
				systemuserIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				systemuserIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetGamerefereeId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, GamerefereeIdInputElementBy, ElementState.VISIBLE);
			var gamerefereeIdInputElement = _driver.FindElementExt(GamerefereeIdInputElementBy);

			if (id != null)
			{
				gamerefereeIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				gamerefereeIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private List<Guid> GetRosterassignmentss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, RosterassignmentssElementBy, ElementState.VISIBLE);
			var rosterassignmentssElement = _driver.FindElements(RosterassignmentssElementBy);

			foreach(var element in rosterassignmentssElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private Guid GetSystemuserId()
		{
			WaitUtils.elementState(_driverWait, SystemuserIdElementBy, ElementState.VISIBLE);
			var systemuserIdElement = _driver.FindElementExt(SystemuserIdElementBy);
			return new Guid(systemuserIdElement.GetAttribute("data-id"));
		}
		private Guid GetGamerefereeId()
		{
			WaitUtils.elementState(_driverWait, GamerefereeIdElementBy, ElementState.VISIBLE);
			var gamerefereeIdElement = _driver.FindElementExt(GamerefereeIdElementBy);
			return new Guid(gamerefereeIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetFirstname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "firstname", value, _isFastText);
			FirstnameElement.SendKeys(Keys.Tab);
			FirstnameElement.SendKeys(Keys.Escape);
		}

		private String GetFirstname =>
			FirstnameElement.Text;

		private void SetLastname (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "lastname", value, _isFastText);
			LastnameElement.SendKeys(Keys.Tab);
			LastnameElement.SendKeys(Keys.Escape);
		}

		private String GetLastname =>
			LastnameElement.Text;

		private void SetDateofbirth (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				DateofbirthElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetDateofbirth =>
			Convert.ToDateTime(DateofbirthElement.DateTimePickerElement.Text);
		private void SetHeight (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "height", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHeight =>
			int.Parse(HeightElement.Text);

		private void SetWeight (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "weight", intValue.ToString(), _isFastText);
			}
		}

		private int? GetWeight =>
			int.Parse(WeightElement.Text);


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}