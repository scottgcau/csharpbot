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
using Sportstats.Controllers;
using Sportstats.Models;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	public class CrudTests
	{
		[Fact]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Unit")]
		public async void SportControllerGetTest()
		{
			using var host = ServerBuilder.CreateServer();

			var database = host.Services.GetRequiredService<SportstatsDBContext>();
			var controller = host.Services.GetRequiredService<SportController>();

			var entities = new EntityFactory<Sport>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();

			database.AddRange(entities);
			await database.SaveChangesAsync();

			var data = await controller.Get(null, default);
			Assert.Contains(data, d => entities.Select(r => r.Id).Contains(d.Id));
		}

		[Fact]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Unit")]
		public async void LeagueControllerGetTest()
		{
			using var host = ServerBuilder.CreateServer();

			var database = host.Services.GetRequiredService<SportstatsDBContext>();
			var controller = host.Services.GetRequiredService<LeagueController>();

			var entities = new EntityFactory<League>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();

			database.AddRange(entities);
			await database.SaveChangesAsync();

			var data = await controller.Get(null, default);
			Assert.Contains(data, d => entities.Select(r => r.Id).Contains(d.Id));
		}

		[Fact]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Unit")]
		public async void UserControllerGetTest()
		{
			using var host = ServerBuilder.CreateServer();

			var database = host.Services.GetRequiredService<SportstatsDBContext>();
			var controller = host.Services.GetRequiredService<UserController>();

			var entities = new EntityFactory<User>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();

			database.AddRange(entities);
			await database.SaveChangesAsync();

			var data = await controller.Get(null, default);
			Assert.Contains(data, d => entities.Select(r => r.Id).Contains(d.Id));
		}

	}
}