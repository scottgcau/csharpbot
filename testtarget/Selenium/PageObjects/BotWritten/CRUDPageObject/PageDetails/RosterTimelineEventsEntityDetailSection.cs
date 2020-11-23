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
	public class RosterTimelineEventsEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements

		//FlatPickr Elements

		//Attribute Headers
		private readonly RosterTimelineEventsEntity _rosterTimelineEventsEntity;

		//Attribute Header Titles
		private IWebElement ActionHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Action']"));
		private IWebElement ActionTitleHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Action Title']"));
		private IWebElement DescriptionHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Description']"));
		private IWebElement GroupIdHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Group Id']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public RosterTimelineEventsEntityDetailSection(ContextConfiguration contextConfiguration, RosterTimelineEventsEntity rosterTimelineEventsEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_rosterTimelineEventsEntity = rosterTimelineEventsEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("ActionElement", (selector: "//div[contains(@class, 'action')]//input", type: SelectorType.XPath));
			selectorDict.Add("ActionTitleElement", (selector: "//div[contains(@class, 'actionTitle')]//input", type: SelectorType.XPath));
			selectorDict.Add("DescriptionElement", (selector: "//div[contains(@class, 'description')]//input", type: SelectorType.XPath));
			selectorDict.Add("GroupIdElement", (selector: "//div[contains(@class, 'groupId')]//input", type: SelectorType.XPath));

			// Reference web elements

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement ActionElement => FindElementExt("ActionElement");
		private IWebElement ActionTitleElement => FindElementExt("ActionTitleElement");
		private IWebElement DescriptionElement => FindElementExt("DescriptionElement");
		private IWebElement GroupIdElement => FindElementExt("GroupIdElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Action" => ActionHeaderTitle,
				"Action Title" => ActionTitleHeaderTitle,
				"Description" => DescriptionHeaderTitle,
				"Group Id" => GroupIdHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Action":
					return ActionElement;
				case "Action Title":
					return ActionTitleElement;
				case "Description":
					return DescriptionElement;
				case "Group Id":
					return GroupIdElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Action":
					SetAction(value);
					break;
				case "Action Title":
					SetActionTitle(value);
					break;
				case "Description":
					SetDescription(value);
					break;
				case "Group Id":
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Action" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.action > div > p"),
				"Action Title" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.actionTitle > div > p"),
				"Description" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.description > div > p"),
				"Group Id" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.groupId > div > p"),
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
			SetAction(_rosterTimelineEventsEntity.Action);
			SetActionTitle(_rosterTimelineEventsEntity.ActionTitle);
			SetDescription(_rosterTimelineEventsEntity.Description);
			SetGroupId(_rosterTimelineEventsEntity.GroupId);

			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations

		// get associations

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetAction (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "action", value, _isFastText);
			ActionElement.SendKeys(Keys.Tab);
			ActionElement.SendKeys(Keys.Escape);
		}

		private String GetAction =>
			ActionElement.Text;

		private void SetActionTitle (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "actionTitle", value, _isFastText);
			ActionTitleElement.SendKeys(Keys.Tab);
			ActionTitleElement.SendKeys(Keys.Escape);
		}

		private String GetActionTitle =>
			ActionTitleElement.Text;

		private void SetDescription (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "description", value, _isFastText);
			DescriptionElement.SendKeys(Keys.Tab);
			DescriptionElement.SendKeys(Keys.Escape);
		}

		private String GetDescription =>
			DescriptionElement.Text;

		private void SetGroupId (Guid? value)
		{
			if (value is Guid guidValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "groupId", value.ToString(), _isFastText);
				GroupIdElement.SendKeys(Keys.Tab);
				GroupIdElement.SendKeys(Keys.Escape);
			}
		}

		private Guid? GetGroupId =>
			Guid.Parse(GroupIdElement.Text);

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}