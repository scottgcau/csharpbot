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
	public class LadderwinlossEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By TeamIdElementBy => By.XPath("//*[contains(@class, 'team')]//div[contains(@class, 'dropdown__container')]");
		private static By TeamIdInputElementBy => By.XPath("//*[contains(@class, 'team')]/div/input");
		private static By LadderIdElementBy => By.XPath("//*[contains(@class, 'ladder')]//div[contains(@class, 'dropdown__container')]");
		private static By LadderIdInputElementBy => By.XPath("//*[contains(@class, 'ladder')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly LadderwinlossEntity _ladderwinlossEntity;

		//Attribute Header Titles
		private IWebElement PlayedHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Played']"));
		private IWebElement WonHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Won']"));
		private IWebElement LostHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Lost']"));
		private IWebElement PointsforHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='PointsFor']"));
		private IWebElement PointsagainstHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='PointsAgainst']"));
		private IWebElement HomewonHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeWon']"));
		private IWebElement HomelostHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeLost']"));
		private IWebElement HomeforHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeFor']"));
		private IWebElement HomeagainstHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='HomeAgainst']"));
		private IWebElement AwaywonHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayWon']"));
		private IWebElement AwaylostHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayLost']"));
		private IWebElement AwayforHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayFor']"));
		private IWebElement AwayagainstHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='AwayAgainst']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public LadderwinlossEntityDetailSection(ContextConfiguration contextConfiguration, LadderwinlossEntity ladderwinlossEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_ladderwinlossEntity = ladderwinlossEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("PlayedElement", (selector: "//div[contains(@class, 'played')]//input", type: SelectorType.XPath));
			selectorDict.Add("WonElement", (selector: "//div[contains(@class, 'won')]//input", type: SelectorType.XPath));
			selectorDict.Add("LostElement", (selector: "//div[contains(@class, 'lost')]//input", type: SelectorType.XPath));
			selectorDict.Add("PointsforElement", (selector: "//div[contains(@class, 'pointsfor')]//input", type: SelectorType.XPath));
			selectorDict.Add("PointsagainstElement", (selector: "//div[contains(@class, 'pointsagainst')]//input", type: SelectorType.XPath));
			selectorDict.Add("HomewonElement", (selector: "//div[contains(@class, 'homewon')]//input", type: SelectorType.XPath));
			selectorDict.Add("HomelostElement", (selector: "//div[contains(@class, 'homelost')]//input", type: SelectorType.XPath));
			selectorDict.Add("HomeforElement", (selector: "//div[contains(@class, 'homefor')]//input", type: SelectorType.XPath));
			selectorDict.Add("HomeagainstElement", (selector: "//div[contains(@class, 'homeagainst')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwaywonElement", (selector: "//div[contains(@class, 'awaywon')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwaylostElement", (selector: "//div[contains(@class, 'awaylost')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwayforElement", (selector: "//div[contains(@class, 'awayfor')]//input", type: SelectorType.XPath));
			selectorDict.Add("AwayagainstElement", (selector: "//div[contains(@class, 'awayagainst')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("TeamElement", (selector: ".input-group__dropdown.teamId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("LadderElement", (selector: ".input-group__dropdown.ladderId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement TeamElement => FindElementExt("TeamElement");
		//get the input path as set by the selector library
		private IWebElement LadderElement => FindElementExt("LadderElement");

		//Attribute web Elements
		private IWebElement PlayedElement => FindElementExt("PlayedElement");
		private IWebElement WonElement => FindElementExt("WonElement");
		private IWebElement LostElement => FindElementExt("LostElement");
		private IWebElement PointsforElement => FindElementExt("PointsforElement");
		private IWebElement PointsagainstElement => FindElementExt("PointsagainstElement");
		private IWebElement HomewonElement => FindElementExt("HomewonElement");
		private IWebElement HomelostElement => FindElementExt("HomelostElement");
		private IWebElement HomeforElement => FindElementExt("HomeforElement");
		private IWebElement HomeagainstElement => FindElementExt("HomeagainstElement");
		private IWebElement AwaywonElement => FindElementExt("AwaywonElement");
		private IWebElement AwaylostElement => FindElementExt("AwaylostElement");
		private IWebElement AwayforElement => FindElementExt("AwayforElement");
		private IWebElement AwayagainstElement => FindElementExt("AwayagainstElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Played" => PlayedHeaderTitle,
				"Won" => WonHeaderTitle,
				"Lost" => LostHeaderTitle,
				"PointsFor" => PointsforHeaderTitle,
				"PointsAgainst" => PointsagainstHeaderTitle,
				"HomeWon" => HomewonHeaderTitle,
				"HomeLost" => HomelostHeaderTitle,
				"HomeFor" => HomeforHeaderTitle,
				"HomeAgainst" => HomeagainstHeaderTitle,
				"AwayWon" => AwaywonHeaderTitle,
				"AwayLost" => AwaylostHeaderTitle,
				"AwayFor" => AwayforHeaderTitle,
				"AwayAgainst" => AwayagainstHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Played":
					return PlayedElement;
				case "Won":
					return WonElement;
				case "Lost":
					return LostElement;
				case "PointsFor":
					return PointsforElement;
				case "PointsAgainst":
					return PointsagainstElement;
				case "HomeWon":
					return HomewonElement;
				case "HomeLost":
					return HomelostElement;
				case "HomeFor":
					return HomeforElement;
				case "HomeAgainst":
					return HomeagainstElement;
				case "AwayWon":
					return AwaywonElement;
				case "AwayLost":
					return AwaylostElement;
				case "AwayFor":
					return AwayforElement;
				case "AwayAgainst":
					return AwayagainstElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Played":
					int? played = null;
					if (int.TryParse(value, out var intPlayed))
					{
						played = intPlayed;
					}
					SetPlayed(played);
					break;
				case "Won":
					int? won = null;
					if (int.TryParse(value, out var intWon))
					{
						won = intWon;
					}
					SetWon(won);
					break;
				case "Lost":
					int? lost = null;
					if (int.TryParse(value, out var intLost))
					{
						lost = intLost;
					}
					SetLost(lost);
					break;
				case "PointsFor":
					int? pointsfor = null;
					if (int.TryParse(value, out var intPointsfor))
					{
						pointsfor = intPointsfor;
					}
					SetPointsfor(pointsfor);
					break;
				case "PointsAgainst":
					int? pointsagainst = null;
					if (int.TryParse(value, out var intPointsagainst))
					{
						pointsagainst = intPointsagainst;
					}
					SetPointsagainst(pointsagainst);
					break;
				case "HomeWon":
					int? homewon = null;
					if (int.TryParse(value, out var intHomewon))
					{
						homewon = intHomewon;
					}
					SetHomewon(homewon);
					break;
				case "HomeLost":
					int? homelost = null;
					if (int.TryParse(value, out var intHomelost))
					{
						homelost = intHomelost;
					}
					SetHomelost(homelost);
					break;
				case "HomeFor":
					int? homefor = null;
					if (int.TryParse(value, out var intHomefor))
					{
						homefor = intHomefor;
					}
					SetHomefor(homefor);
					break;
				case "HomeAgainst":
					int? homeagainst = null;
					if (int.TryParse(value, out var intHomeagainst))
					{
						homeagainst = intHomeagainst;
					}
					SetHomeagainst(homeagainst);
					break;
				case "AwayWon":
					int? awaywon = null;
					if (int.TryParse(value, out var intAwaywon))
					{
						awaywon = intAwaywon;
					}
					SetAwaywon(awaywon);
					break;
				case "AwayLost":
					int? awaylost = null;
					if (int.TryParse(value, out var intAwaylost))
					{
						awaylost = intAwaylost;
					}
					SetAwaylost(awaylost);
					break;
				case "AwayFor":
					int? awayfor = null;
					if (int.TryParse(value, out var intAwayfor))
					{
						awayfor = intAwayfor;
					}
					SetAwayfor(awayfor);
					break;
				case "AwayAgainst":
					int? awayagainst = null;
					if (int.TryParse(value, out var intAwayagainst))
					{
						awayagainst = intAwayagainst;
					}
					SetAwayagainst(awayagainst);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Played" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.played > div > p"),
				"Won" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.won > div > p"),
				"Lost" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.lost > div > p"),
				"PointsFor" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.pointsfor > div > p"),
				"PointsAgainst" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.pointsagainst > div > p"),
				"HomeWon" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.homewon > div > p"),
				"HomeLost" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.homelost > div > p"),
				"HomeFor" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.homefor > div > p"),
				"HomeAgainst" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.homeagainst > div > p"),
				"AwayWon" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awaywon > div > p"),
				"AwayLost" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awaylost > div > p"),
				"AwayFor" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awayfor > div > p"),
				"AwayAgainst" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.awayagainst > div > p"),
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
			SetPlayed(_ladderwinlossEntity.Played);
			SetWon(_ladderwinlossEntity.Won);
			SetLost(_ladderwinlossEntity.Lost);
			SetPointsfor(_ladderwinlossEntity.Pointsfor);
			SetPointsagainst(_ladderwinlossEntity.Pointsagainst);
			SetHomewon(_ladderwinlossEntity.Homewon);
			SetHomelost(_ladderwinlossEntity.Homelost);
			SetHomefor(_ladderwinlossEntity.Homefor);
			SetHomeagainst(_ladderwinlossEntity.Homeagainst);
			SetAwaywon(_ladderwinlossEntity.Awaywon);
			SetAwaylost(_ladderwinlossEntity.Awaylost);
			SetAwayfor(_ladderwinlossEntity.Awayfor);
			SetAwayagainst(_ladderwinlossEntity.Awayagainst);

			SetTeamId(_ladderwinlossEntity.TeamId?.ToString());
			SetLadderId(_ladderwinlossEntity.LadderId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "team":
					return new List<Guid>() {GetTeamId()};
				case "ladder":
					return new List<Guid>() {GetLadderId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetTeamId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, TeamIdInputElementBy, ElementState.VISIBLE);
			var teamIdInputElement = _driver.FindElementExt(TeamIdInputElementBy);

			if (id != null)
			{
				teamIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				teamIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetLadderId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, LadderIdInputElementBy, ElementState.VISIBLE);
			var ladderIdInputElement = _driver.FindElementExt(LadderIdInputElementBy);

			if (id != null)
			{
				ladderIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				ladderIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetTeamId()
		{
			WaitUtils.elementState(_driverWait, TeamIdElementBy, ElementState.VISIBLE);
			var teamIdElement = _driver.FindElementExt(TeamIdElementBy);
			return new Guid(teamIdElement.GetAttribute("data-id"));
		}
		private Guid GetLadderId()
		{
			WaitUtils.elementState(_driverWait, LadderIdElementBy, ElementState.VISIBLE);
			var ladderIdElement = _driver.FindElementExt(LadderIdElementBy);
			return new Guid(ladderIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetPlayed (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "played", intValue.ToString(), _isFastText);
			}
		}

		private int? GetPlayed =>
			int.Parse(PlayedElement.Text);

		private void SetWon (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "won", intValue.ToString(), _isFastText);
			}
		}

		private int? GetWon =>
			int.Parse(WonElement.Text);

		private void SetLost (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "lost", intValue.ToString(), _isFastText);
			}
		}

		private int? GetLost =>
			int.Parse(LostElement.Text);

		private void SetPointsfor (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "pointsfor", intValue.ToString(), _isFastText);
			}
		}

		private int? GetPointsfor =>
			int.Parse(PointsforElement.Text);

		private void SetPointsagainst (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "pointsagainst", intValue.ToString(), _isFastText);
			}
		}

		private int? GetPointsagainst =>
			int.Parse(PointsagainstElement.Text);

		private void SetHomewon (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "homewon", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHomewon =>
			int.Parse(HomewonElement.Text);

		private void SetHomelost (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "homelost", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHomelost =>
			int.Parse(HomelostElement.Text);

		private void SetHomefor (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "homefor", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHomefor =>
			int.Parse(HomeforElement.Text);

		private void SetHomeagainst (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "homeagainst", intValue.ToString(), _isFastText);
			}
		}

		private int? GetHomeagainst =>
			int.Parse(HomeagainstElement.Text);

		private void SetAwaywon (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awaywon", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwaywon =>
			int.Parse(AwaywonElement.Text);

		private void SetAwaylost (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awaylost", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwaylost =>
			int.Parse(AwaylostElement.Text);

		private void SetAwayfor (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awayfor", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwayfor =>
			int.Parse(AwayforElement.Text);

		private void SetAwayagainst (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "awayagainst", intValue.ToString(), _isFastText);
			}
		}

		private int? GetAwayagainst =>
			int.Parse(AwayagainstElement.Text);


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}