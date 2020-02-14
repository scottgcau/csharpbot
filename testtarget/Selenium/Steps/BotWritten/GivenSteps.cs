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
using APITests.Tests.BotWritten;
using APITests.Setup;
using APITests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Setup;
using APITests.EntityObjects.Models;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten
{
	[Binding]
	public sealed class GivenSteps
	{
		private readonly ContextConfiguration _contextConfiguration;
		//private static StartupTestFixture startupTestFixture => new StartupTestFixture();
		private readonly LogoutPage _logoutPage;
		private readonly LoginPage _loginPage;
		private readonly GenericEntityPage _genericEntityPage;

		public GivenSteps(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_logoutPage = new LogoutPage(_contextConfiguration);
			_loginPage = new LoginPage(_contextConfiguration);
			_genericEntityPage = new GenericEntityPage(_contextConfiguration);
			// % protected region % [Add any additional setup options here] off begin
			// % protected region % [Add any additional setup options here] end
		}

		[StepDefinition("I am logged out of the site")]
		public void Logout()
		{
			_logoutPage.Navigate();
		}

		[Given("I login to the site as a user")]
		public void LoginAsUser()
		{
			String userName = _contextConfiguration.SuperUserConfiguration.Username;
			String password = _contextConfiguration.SuperUserConfiguration.Password;
			GivenIAttemptToLogin(userName, password, "success");
		}

		[Given(@"I login to the site with username (.*) and password (.*) then I expect login (.*)")]
		public void GivenIAttemptToLogin(string user, string pass, string success)
		{
			_loginPage.Navigate();
			_loginPage.Login(user, pass);
			try
			{
				// % protected region % [The default page to route to after login, change to suit needs] off begin
				_contextConfiguration.WebDriverWait.Until(wd => wd.Url == _contextConfiguration.BaseUrl + "/home");
				// % protected region % [The default page to route to after login, change to suit needs] end
				Assert.Equal("success", success);
			}
			catch (OpenQA.Selenium.UnhandledAlertException)
			{
				Assert.Equal("failure", success);
			}
			catch (OpenQA.Selenium.WebDriverTimeoutException)
			{
				Assert.Equal(_contextConfiguration.WebDriver.Url, _contextConfiguration.BaseUrl + "/login");
			}
		}

		[Then(@"I select all items in the collection")]
		public void IselectAllItemsInTheCollection()
		{
			var totalNumOfItems = _genericEntityPage.TotalEntities();
			_genericEntityPage.ClickSelectAllItemsButton();
			Assert.Equal(totalNumOfItems, _genericEntityPage.NumberOfItemsSelected());
		}

		[Given("I navigate to the (.*) backend page")]
		public void GivenINavigateToTheBackendPage(string pageName)
		{
			new GenericEntityPage(pageName, _contextConfiguration).Navigate();
		}

		[Given(@"I click to create a (.*)")]
		public void IClickToCreateAnEntity(string entityName)
		{
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			page.ClickCreateButton();
		}

		[Given("I have (.*) valid (.*) entities")]
		public void IHaveValidEntities(int numEntities, string entityName)
		{
			var entityFactory = new EntityFactory(entityName);
			entityFactory.ConstructAndSave(_contextConfiguration.TestOutputHelper, numEntities);
		}
	}
}