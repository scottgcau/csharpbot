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
using System.Linq;
using FluentAssertions;
using Sportstats.Controllers.Entities;
using Sportstats.Models;
using Sportstats.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class TimelineBehaviourTests : IDisposable
	{
		private readonly IWebHost _host;
		private readonly SportstatsDBContext _database;
		private readonly IServiceScope _scope;
		private readonly IServiceProvider _serviceProvider;
		// % protected region % [Add any additional members here] off begin
		// % protected region % [Add any additional members here] end

		public TimelineBehaviourTests()
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

		[Fact]
		public async void RosterEntityControllerEventLogTest()
		{
			// % protected region % [Configure controller EventLog test for Roster here] off begin
			// Arrange
			using var entityController = _serviceProvider.GetRequiredService<RosterEntityController>();
			using var eventController = _serviceProvider.GetRequiredService<RosterTimelineEventsEntityController>();
			
			var entityDto = new EntityFactory<RosterEntity>(1)
				.UseAttributes()
				.UseReferences()
				.DisableIdGeneration()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.Select(x => new RosterEntityDto(x))
				.First();

			// Act
			var entityResult = await entityController.Post(entityDto, default);
			var data = await eventController.Get(null, default);

			// Assert
			data.Data.Select(d => d.EntityId).Should().Contain(entityResult.Id);
			// % protected region % [Configure controller EventLog test for Roster here] end
		}


		[Fact]
		public async void RosterTimelineEventsEntityControllerQuickJumpOptionsTest()
		{
			// % protected region % [Configure controller QuickJumpOptions test for Roster Timeline Events here] off begin
			// Arrange
			using var eventController = _serviceProvider.GetRequiredService<RosterTimelineEventsEntityController>();
			
			var random = new Random();

			var eventEntities = new EntityFactory<RosterTimelineEventsEntity>(5)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			eventEntities.ForEach(x => x.Created = x.Created.AddDays(- random.Next(100)));
			_database.AddRange(eventEntities);
			await _database.SaveChangesAsync();
			
			// Act
			var quickJumpOptions = await eventController.GetQuickJumpOptions(new TimelineFilter{TimeFrame = TimeFrameOption.Weeks});
			
			// Assert
			foreach (var eventEntity in eventEntities)
			{
				Assert.Contains(quickJumpOptions, x => x.StartDate <= eventEntity.Created && x.EndDate > eventEntity.Created);
			}
			// % protected region % [Configure controller QuickJumpOptions test for Roster Timeline Events here] end
		}

		[Fact]
		public async void RosterTimelineEventsEntityControllerGraphDataTest()
		{
			// % protected region % [Configure controller GraphData test for Roster Timeline Events here] off begin
			// Arrange
			using var eventController = _serviceProvider.GetRequiredService<RosterTimelineEventsEntityController>();
			
			var random = new Random();

			var eventEntities = new EntityFactory<RosterTimelineEventsEntity>(5)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			eventEntities.ForEach(x => x.Created = x.Created.AddDays(- random.Next(100)));
			_database.AddRange(eventEntities);
			await _database.SaveChangesAsync();
			
			// Act
			var timelineFilter = new TimelineFilter
			{
				DateRange = new DateRange
				{
					StartDate = eventEntities.Select(x => x.Created).Min(),
					EndDate = DateTime.Now
				}
			};
			var graphQueryResults = await eventController.GetTimelineGraphData(timelineFilter);
			
			// Assert
			foreach (var graphQueryResult in graphQueryResults)
			{
				var dateRange = graphQueryResult.DateTimeGroup;
				var numEventsInDateRange = eventEntities
					.Count(x => x.Created >= dateRange.StartDate && x.Created < dateRange.EndDate);
				graphQueryResult.NumberOfResults.Should().Be(numEventsInDateRange);
			}
			// % protected region % [Configure controller GraphData test for Roster Timeline Events here] end
		}
		
		// % protected region % [Add any additional methods here] off begin
		// % protected region % [Add any additional methods here] end
	}
}