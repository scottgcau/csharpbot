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

using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class LoginPage : BasePage
	{
		public override string Url => baseUrl + "/login";
		public IWebElement EmailInput => FindElementExt("EmailInput");
		public IWebElement PasswordInput => FindElementExt("PasswordInput");
		public IWebElement LoginButton => FindElementExt("LoginButton");
		public IWebElement RegisterButton => FindElementExt("RegisterButton");
		public IWebElement PasswordResetLink => FindElementExt("PasswordResetLink");

		public LoginPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("EmailInput", (selector: "login_username-field", type: SelectorType.ID));
			selectorDict.Add("PasswordInput", (selector: "login_password-field", type: SelectorType.ID));
			selectorDict.Add("LoginButton", (selector: "login_submit", type: SelectorType.ID));
			selectorDict.Add("RegisterButton", (selector: "login_register", type: SelectorType.ID));
			selectorDict.Add("PasswordResetLink", (selector: ".link-forgotten-password", type: SelectorType.CSS));
		}

		///<summary>
		/// Attempts to login with the given credentials
		///</summary>
		///<param name="email">The email to use as a string</param>
		///<param name="password">The password to use as a string </param>
		public void Login(string email, string password)
		{
			EmailInput.Clear();
			PasswordInput.Clear();
			EmailInput.SendKeys(email);
			PasswordInput.SendKeys(password);
			LoginButton.Click();
		}
	}
}
