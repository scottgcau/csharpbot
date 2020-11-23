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
using OpenQA.Selenium;
using SeleniumTests.Enums;
using SeleniumTests.Factories;
using SeleniumTests.Setup;
using SeleniumTests.PageObjects.BotWritten.Forms;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Forms
{
	[Binding]
	public sealed class FormsSubmissionSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly FormsLandingPage _formsLandingPage;
		private readonly FormsBuildPage _formsBuildPage;
		private readonly EntityDetailFactory _entityDetailFactory;
		private string _questionName;
		private string _slideName;

		public FormsSubmissionSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_formsLandingPage = new FormsLandingPage(contextConfiguration);
			_formsBuildPage = new FormsBuildPage(contextConfiguration);
			_entityDetailFactory =  new EntityDetailFactory(contextConfiguration);
		}

		[StepDefinition(@"I create a (.*) called (.*) associated with the (.*) tile")]
		public void ICreateAFormAssociatedWithTheFormPage(string formEntityName, string formDisplayName, string tileName)
		{
			//Navigate to the forms landing page
			_formsLandingPage.Navigate();

			// Create a new instance of the form entity
			_formsLandingPage.ToggleAccordionWithWait(formDisplayName);
			_formsLandingPage.ClickNewFormItemWithWait(formDisplayName);
			var createFormEntityPage = new GenericEntityEditPage(formEntityName, _contextConfiguration);
			var formEntity = _entityDetailFactory.ApplyDetails(formEntityName, true);
			createFormEntityPage.SubmitButton.Click();

			// Create and save a new slide and question
			_slideName = Guid.NewGuid().ToString();
			_questionName = Guid.NewGuid().ToString();
			_formsBuildPage.CreateNewSlide(_slideName);
			_formsBuildPage.CreateNewQuestion(_questionName);
			_formsBuildPage.SaveAndPublishButton.Click();
			_driverWait.Until(x => x.Url.EndsWith("/admin/forms"));

			// Link to submission tile
			var formTileUrl = $"{_baseUrl}/admin/{formEntityName}formtileentity/create";
			_driver.Navigate().GoToUrl(formTileUrl);
			WaitUtils.waitForPage(_driverWait, formTileUrl);
			var createFormTile = new GenericEntityEditPage(formEntityName.RemoveWordsSpacing(), _contextConfiguration);
			var createFormTilePage = _entityDetailFactory.CreateDetailSection($"{formEntityName.RemoveWordsSpacing()}FormTileEntity");
			createFormTilePage.SetInputElement("Tile", tileName);

			var formIdElement = createFormTilePage.GetInputElement("FormId");
			var formIdInput = formIdElement.FindElement(By.TagName("input"));
			formIdInput.SendKeys(formEntity.ToDictionary()["name"]);
			_driverWait.Until(x => !formIdElement.GetAttribute("class").Contains("loading"));
			formIdInput.SendKeys(Keys.Return);

			createFormTile.SubmitButton.Click();
			WaitUtils.waitForPage(_driverWait, $"{_baseUrl}/admin/forms");
		}

		[StepDefinition(@"I expect to be able to submit a (.*) form on the (.*) page")]
		public void IExpectToBeAbleToSubmitAFormEntity(string formEntityName, string formPageName)
		{
			// Navigate to page
			var formSubmissionUrl = $"{_baseUrl}/{formPageName}";
			_driver.Navigate().GoToUrl(formSubmissionUrl);
			WaitUtils.waitForPage(_driverWait, formSubmissionUrl);

			// Check the contents are correct
			var submissionPage = new FormsSubmissionPage(_contextConfiguration);
			submissionPage.OpenFormButton.Click();
			Assert.True(submissionPage.SlideExists(_slideName));
			Assert.True(submissionPage.QuestionExists(_questionName));

			// Complete the submission
			var answer = Guid.NewGuid().ToString();
			submissionPage.AnswerTextQuestion(_questionName, answer);
			submissionPage.SubmitButton.Click();
		}
	}
}