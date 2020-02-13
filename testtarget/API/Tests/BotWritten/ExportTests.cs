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
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using APITests.EntityObjects.Models;
using APITests.Factories;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{

	public class ExportTests : IClassFixture<StartupTestFixture>
	{

		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ExportTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityFactorySingleTheoryData))]
		[ClassData(typeof(EntityFactoryMultipleTheoryData))]
		public void ExportEntity(EntityFactory entityFactory, int numEntities)
		{
			var entityList = entityFactory.ConstructAndSave(_output, numEntities);
			var entityName = entityList[0].EntityName;

			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/{entityName}/export")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			//get the authorization token and adds the token to the request
			var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
			string authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
			request.AddHeader("Authorization", authorizationToken);

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*\\*");

			JsonObject query = QueryBuilder.CreateExportQuery(entityList);
			var queryList = new JsonArray { new JsonArray{query} };
			request.AddParameter("text/json", queryList, ParameterType.RequestBody);

			// execute the request
			var response = client.Execute(request);
			var responseDictionary = CsvToDictionary(response.Content);
			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);

			foreach (var entity in entityList)
			{
				var entityDict = entity.toDictionary();
				var attributeKeys = entityDict.Keys;

				if (entity is UserBaseEntity)
				{
					// export will not contain password
					entityDict.Remove("password");
				}

				foreach (var attributeKey in entityDict.Keys)
				{
					Assert.Contains(entityDict[attributeKey], responseDictionary[attributeKey]);
				}
			}
		}

		private static Dictionary<string, List<string>> CsvToDictionary(string csv)
		{
			var entityDictionary = new Dictionary<string, List<string>>();
			var splitcsv = csv.Split(Environment.NewLine.ToCharArray());
			var entityAttributeKeys = splitcsv[0].Split(',');

			for (var i = 1 ; i < splitcsv.Length; i++)
			{
				var entityAttributeValues = splitcsv[i].Split(',');
				for (var k = 0; k < entityAttributeKeys.Length; k++)
				{
					if (i == 1)
					{
						entityDictionary[entityAttributeKeys[k]] = new List<string>();
					}
					entityDictionary[entityAttributeKeys[k]].Add(entityAttributeValues[k].Split('"')[1]);
				}
			}
			return entityDictionary;
		}
	}
}