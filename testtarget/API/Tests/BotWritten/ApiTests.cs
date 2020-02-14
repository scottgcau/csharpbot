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
using System.Net;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class ApiTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ApiTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityNamePluralizedTheoryData))]
		public void TestGraphqlEndPoints(string entityName)
		{
			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/graphql")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			var query = new JsonObject();

			//get the authorization token and adds the token to the request
			var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
			var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
			request.AddHeader("Authorization", authorizationToken);

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text/html, */*");

			query.Add("query", "{ " + entityName + "{id}}");
			request.AddParameter("text/json", query, ParameterType.RequestBody);

			// execute the request
			var response = client.Execute(request);

			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);

			//check the ids are valid
			var validIds = JObject.Parse(response.Content)["data"][entityName]
				.Select(o => o["id"].Value<string>())
				.All(o => !string.IsNullOrWhiteSpace(o));

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityNameTheoryData))]
		public void TestApiEndPoints(string entityName)
		{
			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/entity/{entityName}")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			//get the authorization token and adds the token to the request
			var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
			var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
			request.AddHeader("Authorization", authorizationToken);

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text/html, */*");

			// execute the request
			var response = client.Execute(request);

			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}