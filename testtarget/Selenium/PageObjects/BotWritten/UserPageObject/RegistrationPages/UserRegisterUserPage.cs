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
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegisterUserUserPage : RegisterUserBasePage
	{
		public override string Url => baseUrl + "/login";
		public IWebElement IdInput => FindElementExt("IdInput");
		public IWebElement UsernameInput => FindElementExt("UsernameInput");

		public RegisterUserUserPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("IdInput", (selector: "div.id > input", type: SelectorType.CSS));
			selectorDict.Add("UsernameInput", (selector: "div.username > input", type: SelectorType.CSS));
		}
		public override void Register (UserBaseEntity entity)
		{
			FillRegistrationDetails(entity.EmailAddress, entity.Password);
			IdInput.SendKeys(entity.toDictionary()["id"]);
			UsernameInput.SendKeys(entity.toDictionary()["username"]);
			RegisterButton.Click();
		}
	}
}
