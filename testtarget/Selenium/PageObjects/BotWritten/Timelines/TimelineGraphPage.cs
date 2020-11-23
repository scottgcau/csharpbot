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

using System.Linq;
using SeleniumTests.Enums;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten.Timelines
{
	public class TimelineGraphPage : BasePage
	{
		public override string Url => baseUrl + "/admin/timelines/graph";
		public IWebElement Controls => FindElementExt("Controls");
		public IWebElement Sidebar => FindElementExt("Sidebar");
		public IWebElement Graph => FindElementExt("Graph");
		public IWebElement ListViewButton => FindElementExt("ListViewButton");
		
		public TimelineGraphPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("Controls", (selector: "section.timelines__menu", type: SelectorType.CSS));
			selectorDict.Add("Sidebar", (selector: "div.sidebar__graph-view", type: SelectorType.CSS));
			selectorDict.Add("Graph", (selector: "section.timelines__view", type: SelectorType.CSS));
			selectorDict.Add("ListViewButton", (selector: "button.icon-ol-list", type: SelectorType.CSS));
		}

		public bool CheckValidPageContents()
		{
			var validPageContents = true;
			validPageContents &= WaitUtils.elementState(driverWait, GetWebElementBy("Controls"), ElementState.VISIBLE);
			validPageContents &= WaitUtils.elementState(driverWait, GetWebElementBy("Sidebar"), ElementState.VISIBLE);
			validPageContents &= WaitUtils.elementState(driverWait, GetWebElementBy("Graph"), ElementState.VISIBLE);
			validPageContents &= WaitUtils.elementState(driverWait, GetWebElementBy("ListViewButton"), ElementState.VISIBLE);
			return validPageContents;
		}
	}
}