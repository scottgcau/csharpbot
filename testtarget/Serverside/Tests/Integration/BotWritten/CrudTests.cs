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

		// % protected region % [Customise Schedule Submission Entity crud tests here] off begin
		[Fact]
		public async void ScheduleSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ScheduleSubmissionEntityController>();
			var entities = new EntityFactory<ScheduleSubmissionEntity>(10)
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
		// % protected region % [Customise Schedule Submission Entity crud tests here] end

		// % protected region % [Customise Season Submission Entity crud tests here] off begin
		[Fact]
		public async void SeasonSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SeasonSubmissionEntityController>();
			var entities = new EntityFactory<SeasonSubmissionEntity>(10)
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
		// % protected region % [Customise Season Submission Entity crud tests here] end

		// % protected region % [Customise Venue Submission Entity crud tests here] off begin
		[Fact]
		public async void VenueSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<VenueSubmissionEntityController>();
			var entities = new EntityFactory<VenueSubmissionEntity>(10)
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
		// % protected region % [Customise Venue Submission Entity crud tests here] end

		// % protected region % [Customise Game Submission Entity crud tests here] off begin
		[Fact]
		public async void GameSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<GameSubmissionEntityController>();
			var entities = new EntityFactory<GameSubmissionEntity>(10)
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
		// % protected region % [Customise Game Submission Entity crud tests here] end

		// % protected region % [Customise Sport Submission Entity crud tests here] off begin
		[Fact]
		public async void SportSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SportSubmissionEntityController>();
			var entities = new EntityFactory<SportSubmissionEntity>(10)
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
		// % protected region % [Customise Sport Submission Entity crud tests here] end

		// % protected region % [Customise League Submission Entity crud tests here] off begin
		[Fact]
		public async void LeagueSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LeagueSubmissionEntityController>();
			var entities = new EntityFactory<LeagueSubmissionEntity>(10)
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
		// % protected region % [Customise League Submission Entity crud tests here] end

		// % protected region % [Customise Team Submission Entity crud tests here] off begin
		[Fact]
		public async void TeamSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TeamSubmissionEntityController>();
			var entities = new EntityFactory<TeamSubmissionEntity>(10)
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
		// % protected region % [Customise Team Submission Entity crud tests here] end

		// % protected region % [Customise Person Submission Entity crud tests here] off begin
		[Fact]
		public async void PersonSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<PersonSubmissionEntityController>();
			var entities = new EntityFactory<PersonSubmissionEntity>(10)
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
		// % protected region % [Customise Person Submission Entity crud tests here] end

		// % protected region % [Customise Roster Submission Entity crud tests here] off begin
		[Fact]
		public async void RosterSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterSubmissionEntityController>();
			var entities = new EntityFactory<RosterSubmissionEntity>(10)
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
		// % protected region % [Customise Roster Submission Entity crud tests here] end

		// % protected region % [Customise RosterAssignment Submission Entity crud tests here] off begin
		[Fact]
		public async void RosterassignmentSubmissionEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterassignmentSubmissionEntityController>();
			var entities = new EntityFactory<RosterassignmentSubmissionEntity>(10)
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
		// % protected region % [Customise RosterAssignment Submission Entity crud tests here] end

		// % protected region % [Customise Schedule Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void ScheduleEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ScheduleEntityFormTileEntityController>();
			var entities = new EntityFactory<ScheduleEntityFormTileEntity>(10)
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
		// % protected region % [Customise Schedule Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Season Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void SeasonEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SeasonEntityFormTileEntityController>();
			var entities = new EntityFactory<SeasonEntityFormTileEntity>(10)
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
		// % protected region % [Customise Season Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Venue Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void VenueEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<VenueEntityFormTileEntityController>();
			var entities = new EntityFactory<VenueEntityFormTileEntity>(10)
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
		// % protected region % [Customise Venue Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Game Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void GameEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<GameEntityFormTileEntityController>();
			var entities = new EntityFactory<GameEntityFormTileEntity>(10)
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
		// % protected region % [Customise Game Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Sport Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void SportEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SportEntityFormTileEntityController>();
			var entities = new EntityFactory<SportEntityFormTileEntity>(10)
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
		// % protected region % [Customise Sport Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise League Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void LeagueEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<LeagueEntityFormTileEntityController>();
			var entities = new EntityFactory<LeagueEntityFormTileEntity>(10)
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
		// % protected region % [Customise League Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Team Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void TeamEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TeamEntityFormTileEntityController>();
			var entities = new EntityFactory<TeamEntityFormTileEntity>(10)
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
		// % protected region % [Customise Team Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Person Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void PersonEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<PersonEntityFormTileEntityController>();
			var entities = new EntityFactory<PersonEntityFormTileEntity>(10)
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
		// % protected region % [Customise Person Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise Roster Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void RosterEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterEntityFormTileEntityController>();
			var entities = new EntityFactory<RosterEntityFormTileEntity>(10)
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
		// % protected region % [Customise Roster Entity Form Tile Entity crud tests here] end

		// % protected region % [Customise RosterAssignment Entity Form Tile Entity crud tests here] off begin
		[Fact]
		public async void RosterassignmentEntityFormTileEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<RosterassignmentEntityFormTileEntityController>();
			var entities = new EntityFactory<RosterassignmentEntityFormTileEntity>(10)
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
		// % protected region % [Customise RosterAssignment Entity Form Tile Entity crud tests here] end

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