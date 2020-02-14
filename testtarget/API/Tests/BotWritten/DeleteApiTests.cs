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
using System.Collections.Generic;
using System.Net;
using APITests.Factories;
using APITests.EntityObjects.Models;
using APITests.Setup;
using APITests.Utils;
using APITests.TheoryData.BotWritten;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class DeleteApiTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly CreateApiTests _createApiTests;
		private readonly ITestOutputHelper _output;

		public DeleteApiTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
			_createApiTests = new CreateApiTests(new StartupTestFixture(), _output);
		}

		#region GraphQl delete
		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityFactorySingleTheoryData))]
		[ClassData(typeof(EntityFactoryMultipleTheoryData))]
		public void GraphqlDeleteEntities(EntityFactory entityFactory, int numEntities)
		{
			var entityList = entityFactory.ConstructAndSave(_output, numEntities);
			GraphQlDelete(entityList);
		}


		/// <summary>
		/// This function is called recursively to delete
		/// entities, it is abstracted away from the creation tests
		/// so it can run independently
		/// </summary>
		/// <param name="entityList">
		/// Takes a list of entities as input, these entities are delete and their
		/// parent entities are also deleted.
		/// </param>
		internal void GraphQlDelete(List<BaseEntity> entityList)
		{
			// instantiate a new rest client, and configure the Uri
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/graphql")
			};

			// instantiate a new request
			var request = new RestRequest();
			// get a valid authorization token to process the request
			var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
			var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";

			// setup request format and headers including authorization token
			request.RequestFormat = DataFormat.Json;
			request.AddHeader("Authorization", authorizationToken);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text/html, */*");

			// form the query to delete the entity
			var deleteQuery = QueryBuilder.DeleteEntityQueryBuilder(entityList);
			request.AddParameter("text/json", deleteQuery, ParameterType.RequestBody);

			// mass delete all entities in the list in a single request and check status code is ok
			ValidateResponse(client, Method.POST, request, HttpStatusCode.OK);

			/*
			 * Run recursively for parent entities,
			 * The first item is passed through because each entity in
			 * the list share the same parent references
			 */
			foreach (var parentEntity in entityList[0].ParentEntities)
			{
				GraphQlDelete(new List<BaseEntity>() {parentEntity});
			}
		}
		#endregion

		#region Rest Endpoint Delete

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityFactoryTheoryData))]
		public void ApiDeleteEntities(EntityFactory entityFactory)
		{
			// create some test entities over the api to run the delete tests against
			var entityList = entityFactory.ConstructAndSave(_output, 1);

			foreach (var entityObject in entityList)
			{
				// instantiate a list of entity names and guids to be deleted
				var entityKeysGuids = new List<KeyValuePair<string, Guid>>();
				entityKeysGuids.Add(new KeyValuePair<string, Guid>(entityObject.EntityName, entityObject.Id));

				// populate the list using information returned from create entities.
				GetParentKeysGuids(entityObject).ForEach(x => entityKeysGuids.Add(x));

				foreach(var entityKeyGuid in entityKeysGuids)
				{
					// instantiate a new rest client, and configure the Uri
					var client = new RestClient
					{
						BaseUrl = new Uri($"{_configure.BaseUrl}/api/entity/{entityKeyGuid.Key}/{entityKeyGuid.Value}")
					};

					// instantiate a new request
					var request = new RestRequest();

					// get a valid authorization token to process the request
					var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
					var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";

					// setup request format and headers including authorization token
					request.RequestFormat = DataFormat.Json;
					request.AddHeader("Authorization", authorizationToken);
					request.AddHeader("Content-Type", "application/json");
					request.AddHeader("Accept", "*\\*");

					// use GET to verify that the entity currently exists
					ValidateResponse(client, Method.GET, request, HttpStatusCode.OK);
					// use DELETE to delete entity and check good status code is returned
					ValidateResponse(client, Method.DELETE, request, HttpStatusCode.OK);
					// verify API will no longer return a valid status when trying to GET deleted entity.
					ValidateResponse(client, Method.GET, request, HttpStatusCode.NoContent);
					// verify API will no longer return a valid status when trying to DELETE deleted entity.
					ValidateResponse(client, Method.DELETE, request, HttpStatusCode.OK);
				}
			}
		}
		#endregion

		internal List<KeyValuePair<string, Guid>> GetParentKeysGuids(BaseEntity entityObject)
		{
			var entityKeysGuids = new List<KeyValuePair<string, Guid>>();

			foreach (var parentEntity in entityObject.ParentEntities)
			{
				if ((parentEntity.EntityName != entityObject.EntityName))
				{
					entityKeysGuids.Add(new KeyValuePair<string, Guid>(parentEntity.EntityName, parentEntity.Id));
					GetParentKeysGuids(parentEntity).ForEach(x => entityKeysGuids.Add(x));
				}
			}
			return entityKeysGuids;
		}

		private void ValidateResponse(RestClient client, Method method, RestRequest request, HttpStatusCode expectedResponse)
		{
			request.Method = method;
			var response = client.Execute(request);
			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);
			Assert.Equal(expectedResponse, response.StatusCode);
		}
	}
}