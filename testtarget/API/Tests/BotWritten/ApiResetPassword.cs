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
using System.Net;
using RestSharp;
using Xunit;
using Xunit.Abstractions;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using APITests.Factories;

namespace APITests.Tests.BotWritten
{
	public class ApiResetPassword : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ApiResetPassword(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[SkippableTheory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(UserEntityFactorySingleTheoryData))]
		public void ResetPassword(UserEntityFactory entityFactory)
		{
			throw new SkipException("Test has been deprecated and will be replaced soon");

			var userEntity = entityFactory.ConstructAndSave(_output);

			// send change password request and read token from locally saved email
			RequestResetPassword(userEntity.EmailAddress);
			var token = GetResetTokenFromEmail(userEntity.EmailAddress);

			// set the new password and check that it works
			var testPassword = "newPassword1!";
			SetNewPassword(token, userEntity.EmailAddress, testPassword);
			CheckPasswordChanged(userEntity.EmailAddress, testPassword, userEntity.Password);

			// set password back to original
			// TODO replace this with database delete user
			RequestResetPassword(userEntity.EmailAddress);
			token = GetResetTokenFromEmail(userEntity.EmailAddress);
			SetNewPassword(token, userEntity.EmailAddress, userEntity.Password);
		}

		private void RequestResetPassword(string username)
		{
			var uri = $"{_configure.BaseUrl}/api/account/reset-password-request";
			var query = new JsonObject { ["username"] = username };
			RequestHelpers.SendPostRequest(uri, query, _output);
		}

		private string GetResetTokenFromEmail(string username)
		{
			var email = FileReadingUtilities.ReadPasswordResetEmail(username);
			Assert.Single(email.Recipients);
			Assert.Equal("Reset Password", email.Subject);
			return System.Web.HttpUtility.UrlDecode(email.Token);
		}

		private void SetNewPassword(string token, string username, string password)
		{
			var uri = $"{_configure.BaseUrl}/api/account/reset-password";
			var query = new JsonObject { ["username"] = username, ["token"] = token, ["password"] = password, };
			RequestHelpers.SendPostRequest(uri, query, _output);
		}

		private void CheckPasswordChanged(string username, string newPassword, string oldPassword)
		{
			AttemptLogin(username, newPassword, HttpStatusCode.OK);
			AttemptLogin(username, oldPassword, HttpStatusCode.Unauthorized);
		}

		private void AttemptLogin(string username, string password, HttpStatusCode expectedStatusCode)
		{
			// setup client and request
			var client = new RestClient { BaseUrl = new Uri(_configure.BaseUrl + "/api/authorization/login") };
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };
			request.AddHeader("Content-Type", "application/json");
			request.AddJsonBody(new { username = username, password = password });

			// execute the request and assert response correct
			var response = client.Execute(request);
			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);
			Assert.Equal(expectedStatusCode, response.StatusCode);
		}
	}
}