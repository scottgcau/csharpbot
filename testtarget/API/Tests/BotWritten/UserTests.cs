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
using Sportstats.Controllers;
using APITests.EntityObjects.Models;
using APITests.Factories;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class UserTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public UserTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(UserEntityFactorySingleTheoryData))]
		public void CreateValidRegisteredUserTests(UserEntityFactory entityFactory)
		{
			var userEntity = entityFactory.Construct();
			userEntity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			userEntity.CreateUser(true);
			var loginResponse = AttemptLogin(userEntity.EmailAddress, userEntity.Password);
			Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
		}

		private IRestResponse AttemptLogin(string username, string password)
		{
			var client = new RestClient { BaseUrl = new Uri(_configure.BaseUrl + "/api/authorization/login") };
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };
			request.AddHeader("Content-Type", "application/json");
			request.AddJsonBody(new { username, password});
			var response = client.Execute(request);
			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);
			return response;
		}


		//define registration errors
		private const string UnregisteredAccountError = "This account is not yet activated";

		private const string SuperUsername = "super@example.com";
		private const string SuperPassword = "password";

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(PasswordInvalidTheoryData))]
		public void RegistrationInvalidPasswordTests(UserEntityFactory userEntityFactory, string password, string expectedException)
		{
			var userEntity = userEntityFactory.Construct();
			userEntity.Password = password;

			try
			{
				new Registration(userEntity, _output);
			}
			catch (AggregateException e)
			{
				var exceptionInList = e.InnerExceptions.Select(x => x.Message);
				Assert.Contains(expectedException, exceptionInList);
				return;
			}

			throw new Exception("User creation succeeded when it was expected to fail");
		}


		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(UsernameInvalidTheoryData))]
		public void RegistrationUserInvalidTests(UserEntityFactory userEntityFactory, string username, string expectedException)
		{
			var userEntity = userEntityFactory.Construct();
			userEntity.EmailAddress = username;

			try
			{
				new Registration(userEntity, _output);
			}
			catch (AggregateException e)
			{
				var exceptionInList = e.InnerExceptions.Select(x => x.Message);
				Assert.Contains(expectedException, exceptionInList);
				return;
			}

			throw new Exception("User creation succeeded when it was expected to fail");
		}

		public static TheoryData<string, string, string> ValidTokenResponse()
		{
			return new TheoryData<string, string, string>
			{
				{SuperUsername, SuperPassword, "Bearer"}
			};
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[MemberData(nameof(ValidTokenResponse))]
		public void ValidLoginUserTests(string username, string password, string expectedResponse)
		{
			//try to get an access token, if we get one then we're all sweet
			var loginTokenObject = new LoginToken(_configure.BaseUrl, username, password);
				var accessToken = loginTokenObject.AccessToken;
				var tokenType = loginTokenObject.TokenType;
				Assert.True(accessToken != null && tokenType == expectedResponse);
		}

		private const string InvalidPasswordError = "The username/password couple is invalid.";
		private const string MissingUsernamePasswordError = "The mandatory 'username' and/or 'password' parameters are missing.";

		public static TheoryData<string, string, string> GetExpectedInvalidLoginResponses()
		{
			return new TheoryData<string, string, string>
			{
				{ "user@example.com", "password",InvalidPasswordError},
				{ "", "password", MissingUsernamePasswordError},
				{ "user@example.com", "", MissingUsernamePasswordError},
				{ "", "",MissingUsernamePasswordError},
			};
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[MemberData(nameof(GetExpectedInvalidLoginResponses))]
		public void InvalidLoginUserTests(string username, string password, string expectedResponse)
		{
			var exception = Assert.Throws<Exception>(() => new LoginToken(_configure.BaseUrl, username, password));
			Assert.Equal(expectedResponse, exception.Message);
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(UserEntityFactorySingleTheoryData))]
		public void GetAllUsersEndpointTests(UserEntityFactory entityFactory)
		{
			// Arrange
			var userEntity = entityFactory.Construct();
			userEntity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			userEntity.CreateUser(true);

			// Endpoint requires sorting and pagination options to be supplied.
			var sortOptions = new[] {new {Path = "id", Descending = false}};
			var paginationOptions = new {PageNo = 1, PageSize = 10};

			// Search query for the user entity that we have created.
			var searchConditions = new[] { new[] { new {comparison = "Like", path = "Email", value = new string[] {$"%{userEntity.EmailAddress}%"}}}};

			// Add required sorting and pagination options, and the search for our created entity to the body of the query.
			var query = JsonConvert.SerializeObject(new {PaginationOptions = paginationOptions, SearchConditions = searchConditions, SortConditions = sortOptions });

			var clientxsrf = ClientXSRF.GetValidClientAndxsrfTokenPair(_configure);
			var client = clientxsrf.client;
			client.BaseUrl = new Uri(_configure.BaseUrl + $"/api/account/users");

			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };
			request.AddHeader("X-XSRF-TOKEN", clientxsrf.xsrfToken);
			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("query", query, ParameterType.RequestBody);

			// Act
			var response = client.Execute(request);
			var returnedObject = JsonConvert.DeserializeObject<AccountController.UserListModel>(response.Content);

			// Assert
			Assert.Equal(userEntity.Id, returnedObject.Users.First().Id);

		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(UserEntityFactorySingleTheoryData))]
		public void DeactivateUserEndpointTests(UserEntityFactory entityFactory)
		{
			// Arrange
			var userEntity = entityFactory.Construct();
			userEntity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			userEntity.CreateUser(true);
			var deactivateQuery = JsonConvert.SerializeObject(new
			{
				Username = userEntity.EmailAddress
			});

			var clientxsrf = ClientXSRF.GetValidClientAndxsrfTokenPair(_configure);
			var client = clientxsrf.client;
			client.BaseUrl = new Uri(_configure.BaseUrl + $"/api/account/deactivate");
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };
			request.AddHeader("X-XSRF-TOKEN", clientxsrf.xsrfToken);
			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("query", deactivateQuery, ParameterType.RequestBody);

			// Act
			var response = client.Execute(request);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var activatedInDatabase = UserHelper.GetUserFromDB(userEntity.Id).EmailConfirmed;
			Assert.False(activatedInDatabase);
		}
	}
}