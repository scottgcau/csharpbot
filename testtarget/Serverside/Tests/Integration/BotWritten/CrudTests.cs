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
using FluentAssertions;
using Sportstats.Controllers.Entities;
using Sportstats.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Add any additional imports here] off begin
// % protected region % [Add any additional imports here] end

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class CrudTests : IDisposable
	{
		private readonly IWebHost _host;
		private readonly SportstatsDBContext _database;
		private readonly IServiceScope _scope;
		private readonly IServiceProvider _serviceProvider;
		// % protected region % [Add any additional members here] off begin
		// % protected region % [Add any additional members here] end

		public CrudTests()
		{
			// % protected region % [Configure constructor here] off begin
			_host = ServerBuilder.CreateServer();
			_scope = _host.Services.CreateScope();
			_serviceProvider = _scope.ServiceProvider;
			_database = _serviceProvider.GetRequiredService<SportstatsDBContext>();
			// % protected region % [Configure constructor here] end
		}
		
		public void Dispose()
		{
			// % protected region % [Configure dispose here] off begin
			_host?.Dispose();
			_database?.Dispose();
			_scope?.Dispose();
			// % protected region % [Configure dispose here] end
		}


		// % protected region % [Customise Ladder Entity crud tests here] off begin
		[Fact]
		public async void LadderEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LadderEntityController>();
			var entities = new EntityFactory<LadderEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Ladder Entity crud tests here] end

		// % protected region % [Customise Schedule Entity crud tests here] off begin
		[Fact]
		public async void ScheduleEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ScheduleEntityController>();
			var entities = new EntityFactory<ScheduleEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Schedule Entity crud tests here] end

		// % protected region % [Customise LadderElimination Entity crud tests here] off begin
		[Fact]
		public async void LaddereliminationEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LaddereliminationEntityController>();
			var entities = new EntityFactory<LaddereliminationEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise LadderElimination Entity crud tests here] end

		// % protected region % [Customise LadderWinLoss Entity crud tests here] off begin
		[Fact]
		public async void LadderwinlossEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LadderwinlossEntityController>();
			var entities = new EntityFactory<LadderwinlossEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise LadderWinLoss Entity crud tests here] end

		// % protected region % [Customise Round Entity crud tests here] off begin
		[Fact]
		public async void RoundEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RoundEntityController>();
			var entities = new EntityFactory<RoundEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Round Entity crud tests here] end

		// % protected region % [Customise Game Entity crud tests here] off begin
		[Fact]
		public async void GameEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<GameEntityController>();
			var entities = new EntityFactory<GameEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Game Entity crud tests here] end

		// % protected region % [Customise Division Entity crud tests here] off begin
		[Fact]
		public async void DivisionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<DivisionEntityController>();
			var entities = new EntityFactory<DivisionEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Division Entity crud tests here] end

		// % protected region % [Customise Venue Entity crud tests here] off begin
		[Fact]
		public async void VenueEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<VenueEntityController>();
			var entities = new EntityFactory<VenueEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Venue Entity crud tests here] end

		// % protected region % [Customise Team Entity crud tests here] off begin
		[Fact]
		public async void TeamEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TeamEntityController>();
			var entities = new EntityFactory<TeamEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Team Entity crud tests here] end

		// % protected region % [Customise GameReferee Entity crud tests here] off begin
		[Fact]
		public async void GamerefereeEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<GamerefereeEntityController>();
			var entities = new EntityFactory<GamerefereeEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise GameReferee Entity crud tests here] end

		// % protected region % [Customise Season Entity crud tests here] off begin
		[Fact]
		public async void SeasonEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SeasonEntityController>();
			var entities = new EntityFactory<SeasonEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Season Entity crud tests here] end

		// % protected region % [Customise Person Entity crud tests here] off begin
		[Fact]
		public async void PersonEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<PersonEntityController>();
			var entities = new EntityFactory<PersonEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Person Entity crud tests here] end

		// % protected region % [Customise SystemUser Entity crud tests here] off begin
		[Fact]
		public async void SystemuserEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SystemuserEntityController>();
			var entities = new EntityFactory<SystemuserEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise SystemUser Entity crud tests here] end

		// % protected region % [Customise Sport Entity crud tests here] off begin
		[Fact]
		public async void SportEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SportEntityController>();
			var entities = new EntityFactory<SportEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Sport Entity crud tests here] end

		// % protected region % [Customise League Entity crud tests here] off begin
		[Fact]
		public async void LeagueEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LeagueEntityController>();
			var entities = new EntityFactory<LeagueEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise League Entity crud tests here] end

		// % protected region % [Customise Roster Entity crud tests here] off begin
		[Fact]
		public async void RosterEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterEntityController>();
			var entities = new EntityFactory<RosterEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Roster Entity crud tests here] end

		// % protected region % [Customise RosterAssignment Entity crud tests here] off begin
		[Fact]
		public async void RosterassignmentEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterassignmentEntityController>();
			var entities = new EntityFactory<RosterassignmentEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise RosterAssignment Entity crud tests here] end

		// % protected region % [Customise Roster Timeline Events Entity crud tests here] off begin
		[Fact]
		public async void RosterTimelineEventsEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterTimelineEventsEntityController>();
			var entities = new EntityFactory<RosterTimelineEventsEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}
		// % protected region % [Customise Roster Timeline Events Entity crud tests here] end

	// % protected region % [Add any additional tests here] off begin
	// % protected region % [Add any additional tests here] end
	}
}