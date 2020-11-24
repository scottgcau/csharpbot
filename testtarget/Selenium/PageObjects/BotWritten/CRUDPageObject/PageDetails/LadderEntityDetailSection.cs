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
	public class LadderEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By LadderwinlossessElementBy => By.XPath("//*[contains(@class, 'ladderwinlosses')]//div[contains(@class, 'dropdown__container')]/a");
		private static By LadderwinlossessInputElementBy => By.XPath("//*[contains(@class, 'ladderwinlosses')]/div/input");
		private static By LaddereliminationssElementBy => By.XPath("//*[contains(@class, 'laddereliminations')]//div[contains(@class, 'dropdown__container')]/a");
		private static By LaddereliminationssInputElementBy => By.XPath("//*[contains(@class, 'laddereliminations')]/div/input");
		private static By ScheduleIdElementBy => By.XPath("//*[contains(@class, 'schedule')]//div[contains(@class, 'dropdown__container')]");
		private static By ScheduleIdInputElementBy => By.XPath("//*[contains(@class, 'schedule')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly LadderEntity _ladderEntity;

		//Attribute Header Titles
		private IWebElement LaddertypeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='LadderType']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public LadderEntityDetailSection(ContextConfiguration contextConfiguration, LadderEntity ladderEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_ladderEntity = ladderEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("LaddertypeElement", (selector: "//div[contains(@class, 'laddertype')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("LadderwinlossesElement", (selector: ".input-group__dropdown.ladderwinlossess > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("LaddereliminationsElement", (selector: ".input-group__dropdown.laddereliminationss > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("ScheduleElement", (selector: ".input-group__dropdown.schedule > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement LaddertypeElement => FindElementExt("LaddertypeElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"LadderType" => LaddertypeHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "LadderType":
					return LaddertypeElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "LadderType":
					SetLaddertype((Laddertype)Enum.Parse(typeof(Laddertype), value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"LadderType" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.laddertype > div > p"),
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
			SetLaddertype(_ladderEntity.Laddertype);

			if (_ladderEntity.LadderwinlossesIds != null)
			{
				SetLadderwinlossess(_ladderEntity.LadderwinlossesIds.Select(x => x.ToString()));
			}
			if (_ladderEntity.LaddereliminationsIds != null)
			{
				SetLaddereliminationss(_ladderEntity.LaddereliminationsIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "ladderwinlosses":
					return GetLadderwinlossess();
				case "laddereliminations":
					return GetLaddereliminationss();
				case "schedule":
					return new List<Guid>() {GetScheduleId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetLadderwinlossess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, LadderwinlossessInputElementBy, ElementState.VISIBLE);
			var ladderwinlossessInputElement = _driver.FindElementExt(LadderwinlossessInputElementBy);

			foreach(var id in ids)
			{
				ladderwinlossessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				ladderwinlossessInputElement.SendKeys(Keys.Return);
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
		private List<Guid> GetLadderwinlossess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, LadderwinlossessElementBy, ElementState.VISIBLE);
			var ladderwinlossessElement = _driver.FindElements(LadderwinlossessElementBy);

			foreach(var element in ladderwinlossessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
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
		private Guid GetScheduleId()
		{
			WaitUtils.elementState(_driverWait, ScheduleIdElementBy, ElementState.VISIBLE);
			var scheduleIdElement = _driver.FindElementExt(ScheduleIdElementBy);
			return new Guid(scheduleIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetLaddertype (Laddertype value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "laddertype", value.ToString(), _isFastText);
		}

		private Laddertype GetLaddertype =>
			(Laddertype)Enum.Parse(typeof(Laddertype), LaddertypeElement.Text);
			

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}