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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APITests.EntityObjects.Models;
using APITests.Factories;
using APITests.Setup;
using APITests.Utils;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class ValidatorApiTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ValidatorApiTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		// create multiple entities by passing in the factory and number you wish to create
		public static TheoryData<EntityFactory> EntitiesFactoryData()
		{
			return new TheoryData<EntityFactory>
			{
				{ new EntityFactory("Sport")},
				{ new EntityFactory("League")},
				{ new EntityFactory("User")},
			};
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[MemberData(nameof(EntitiesFactoryData))]
		public void CreateInvalidEntity(EntityFactory entityFactory)
		{
			var entityObject = entityFactory.Construct(1)[0];

			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/graphql")
			};

			// get references to other entities
			foreach (var reference in entityObject.References)
			{
				var referenceIdName = reference.OppositeName + "Id";

				//if we have a self reference, set it to null and continue
				if (reference.EntityName == entityObject.EntityName)
				{
					// set all entitites in the entity list to null
					entityObject.ReferenceIdDictionary[referenceIdName] = null;
					continue;
				}

				var createdEntity = new CreateApiTests(_configure, _output).CreateEntities(new EntityFactory(reference.EntityName),1)[0];

				//add the created entity to our dictionary for the child entity for all entities in the list
				entityObject.ReferenceIdDictionary[referenceIdName] = createdEntity.Id;
			}

			var invalidEntities = entityObject.GetInvalidMutatedJsons().ToList();

			// seperate an invalid json that has mutiple errors / invalid attributes associated with it from the ones with
			// a single error, and test seperately.

			// TODO: tidy up this seperating out section

			var multipleErrorJson = invalidEntities.Where(x => x.expectedErrors.Count > 1).ToList();
			invalidEntities = invalidEntities.Where(x => x.expectedErrors.Count == 1).ToList();

			// bulk test all the invalid jsons that have single errors
			CheckInvalidJsonsForInvalidResponse(entityObject, invalidEntities, client);

			// test the json with multiple error if there was one.
			if (multipleErrorJson.Count > 0)
			{
				CheckInvalidJsonsForInvalidResponse(entityObject, multipleErrorJson, client);
			}

			var invalidEnumEntities = entityObject.GetInvalidMutatedJsonsForEnums().ToList();

			invalidEnumEntities = invalidEnumEntities.Where(x => x.expectedErrors.Count == 1).ToList();

			// Looping through to test one by one because invalid enum error is thrown by
			// GraphQl Deserializing and it only returns the first error it comes with in one request
			var invalidEnumEntitiesList = invalidEnumEntities.Select(x => new List<(List<string> expectedErrors, JsonObject jsonObject)> { x }).ToList();

			foreach (var invalidEnumEntity in invalidEnumEntitiesList)
			{
				CheckInvalidJsonsForInvalidResponse(entityObject, invalidEnumEntity, client);
			}
		}

		private void CheckInvalidJsonsForInvalidResponse(BaseEntity entityObject, List<(List<string> expectedErrors,
			JsonObject jsonObject)> invalidEntities, RestClient client)
		{
			// Looping through to test one by one because invalid enum error is thrown by
			// GraphQl Deserializing and it only returns the first error it comes with in one request
			invalidEntities.ForEach(invalidEntity =>
			{
				var invalidJsons = new List<JsonObject>() { invalidEntity.jsonObject };

				//setup the request
				var request = RequestHelpers.BasicPostRequest();
				var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
				var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
				request.AddHeader("Authorization", authorizationToken);
				var query = QueryBuilder.InvalidEntityQueryBuilder(entityObject, invalidJsons);
				request.AddParameter("text/json", query, ParameterType.RequestBody);

				// execute the request
				var response = client.Execute(request);

				// check that a bad request status code was returned in the response
				Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

				// for each expected error
				foreach (var expectedError in invalidEntity.expectedErrors)
				{
					// check the response contains the expected error for the failed validator
					Assert.Contains(expectedError, response.Content);
				}
			});
		}
	}
}