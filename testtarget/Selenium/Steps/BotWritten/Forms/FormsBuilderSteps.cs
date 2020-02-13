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
using System.Linq;
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
	public sealed class FormsSteps
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly FormsLandingPage _formsLandingPage;
		private readonly FormsBuildPage _formsBuildPage;
		private readonly EntityDetailFactory _entityDetailFactory;

		public FormsSteps(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_formsLandingPage = new FormsLandingPage(contextConfiguration);
			_formsBuildPage = new FormsBuildPage(contextConfiguration);
			_entityDetailFactory =  new EntityDetailFactory(contextConfiguration);
		}

		[StepDefinition(@"I navigate to the forms landing page")]
		public void INavigateToTheFormsBackendPage()
		{
			_formsLandingPage.Navigate();
		}

		[StepDefinition(@"I expect to see an accordion for (.*)")]
		public void IExpectToSeeAccordionsFor(string formEntityName)
		{
			var accordionHeaders = _formsLandingPage.GetAccordionFormHeaderNames().ToList();
			Assert.Contains(formEntityName, accordionHeaders);
		}

		[StepDefinition(@"I expect to be able to create and save a new (.*) form")]
		public void IExpectToBeAbleToCreateAndSaveANewForm(string formEntityName)
		{
			// Create a new instance of the form entity
			_formsLandingPage.ToggleAccordionWithWait(formEntityName);
			_formsLandingPage.ClickNewFormItemWithWait(formEntityName);
			var createFormEntityPage = new GenericEntityEditPage(formEntityName.RemoveWordsSpacing(), _contextConfiguration);
			var formEntity = _entityDetailFactory.ApplyDetails(formEntityName.RemoveWordsSpacing(), true);
			createFormEntityPage.SubmitButton.Click();

			// Create and save a new slide and question
			var slideName = Guid.NewGuid().ToString();
			var questionName = Guid.NewGuid().ToString();
			_formsBuildPage.CreateNewSlide(slideName);
			_formsBuildPage.CreateNewQuestion(questionName);
			_formsBuildPage.SaveAndPublishButton.Click();

			// Assert that the slide and question have been saved
			_formsLandingPage.ToggleAccordionWithWait(formEntityName);
			_formsLandingPage.ClickFormItemWithWait(formEntity.toDictionary()["name"]);
			var slideNames = _formsBuildPage.GetSlideNamesWithWait();
			var questionNames = _formsBuildPage.GetQuestionNamesWithWait();
			Assert.Contains(slideName, slideNames);
			Assert.Contains(questionName, questionNames);
		}
	}
}
