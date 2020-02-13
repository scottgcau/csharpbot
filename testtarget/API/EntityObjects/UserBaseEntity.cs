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
using System.Net;
using Xunit;
using Sportstats.Models;
using APITests.Setup;
using APITests.DataFixtures;
using APITests.Utils;
using RestSharp;

namespace APITests.EntityObjects.Models
{
	public abstract class UserBaseEntity : BaseEntity
	{
		public string EmailAddress = BaseChoice.GetUniqueValidEmail();
		public string Password = "password";
		public string EndpointName;
		private readonly StartupTestFixture _configure = new StartupTestFixture();

		public Guid CreateUser(bool isRegistered = true)
		{
			var clientxsrf = ClientXSRF.GetValidClientAndxsrfTokenPair(_configure);
			var client = clientxsrf.client;
			client.BaseUrl = new Uri(_configure.BaseUrl + $"/api/register/{EndpointName}");
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };
			request.AddHeader("X-XSRF-TOKEN", clientxsrf.xsrfToken);
			request.AddHeader("Content-Type", "application/json");
			request.AddParameter("query", this.toJson(), ParameterType.RequestBody);
			var response = client.Execute(request);

			//TODO add output helper for this this.

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			if (isRegistered)
			{
				// we will confirm their email, they are registered
				var configure = new StartupTestFixture();
				using (var context = new SportstatsDBContext(configure.DbContextOptions, null, null))
				{
					context.Users.FirstOrDefault(x => x.UserName == this.EmailAddress).EmailConfirmed = true;
					context.SaveChanges();
				}
			}
			return this.Id;
		}
	}
}