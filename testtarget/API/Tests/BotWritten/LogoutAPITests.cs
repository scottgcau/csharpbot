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
using System.Net;
using APITests.Setup;
using APITests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class LogoutApiTest : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public LogoutApiTest(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		#region GraphQl Logout
		[Fact]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		public void APIUserLogoutTest()
		{
			// login to the backend server
			var clientxsrf = ClientXsrf.GetValidClientAndxsrfTokenPair(_configure);

			// extract the client
			var client = clientxsrf.client;

			// should be 3 cookies after login
			Assert.Equal(3, client.CookieContainer.Count);

			// extract the xsrf token
			var xsrfToken = clientxsrf.xsrfToken;

			// set the logout url
			client.BaseUrl = new Uri($"{_configure.BaseUrl}/api/authorization/logout");

			//setup the request headers
			var request = RequestHelpers.BasicPostRequest();

			// get the authorization token and adds the token to the request
			request.AddHeader("X-XSRF-TOKEN", xsrfToken);

			// execute the logout request
			var response = client.Execute(request);

			// valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			// should be no cookies in the response after login
			Assert.Equal(0, response.Cookies.Count);

			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);
		}

		#endregion
	}
}