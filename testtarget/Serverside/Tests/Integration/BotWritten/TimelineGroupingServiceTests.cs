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
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Sportstats.Helpers;
using Sportstats.Models;
using Sportstats.Services.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace ServersideTests.Tests.Integration.BotWritten
{

	public class DateRangeTheoryData : TheoryData<DateRange>
	{
		public DateRangeTheoryData()
		{
			// starts at 1 hour date range and compounds 25% additional time
			// up until 10 years date range
			for (double i = 1; i < (24 * 365 * 10); i *= 1.25)
			{
				Add(new DateRange
				{
					EndDate = DateTime.Now,
					StartDate = DateTime.Now.AddHours(-i)
				});
			}
		}
	}

	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class GroupingServiceTests
	{
		private readonly ITestOutputHelper _testOutputHelper;
		private readonly IWebHost _host;
		private readonly SportstatsDBContext _database;
		private readonly IServiceScope _scope;
		private readonly IServiceProvider _serviceProvider;
		private readonly ITimelineGroupingService _groupingService;

		public GroupingServiceTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
			_host = ServerBuilder.CreateServer();
			_scope = _host.Services.CreateScope();
			_serviceProvider = _scope.ServiceProvider;
			_database = _serviceProvider.GetRequiredService<SportstatsDBContext>();
		}
		
		public async Task<IOrderedEnumerable<TimelineGroupDateQueryResult<RosterTimelineEventsEntity>>> FormClusters(DateRange testRange)
		{
			// Arrange
			var groupingService = _serviceProvider.GetRequiredService<ITimelineGroupingService>();

			var totalHours = (int) (testRange.EndDate - testRange.StartDate).TotalHours;

			var eventEntities = new EntityFactory<RosterTimelineEventsEntity>(100)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();

			for (var i = 0; i < eventEntities.Count; i++)
			{
				eventEntities[i].Created = eventEntities[i].Created.AddHours(-(totalHours * i / eventEntities.Count));
			}
			
			_database.AddRange(eventEntities);
			await _database.SaveChangesAsync();
			
			// Act
			var timelineFilter = new TimelineFilter
			{
				DateRange = testRange
			};

			return await groupingService.GraphQueryGroupingResult<RosterTimelineEventsEntity>(timelineFilter);
			
		}

		[Theory]
		[ClassData(typeof(DateRangeTheoryData))]
		public async Task TestNoOverLappingDateRanges(DateRange testRange)
		{
			// arrange
			// act
			var data = await FormClusters(testRange);

			//assert
			foreach (var graphQueryResult in data)
			{
				var startDate = graphQueryResult.DateTimeGroup.StartDate;
				var endDate = graphQueryResult.DateTimeGroup.EndDate;

				var clustersInRange = data.Where(x =>
					(x.DateTimeGroup.StartDate <= startDate && startDate < x.DateTimeGroup.EndDate) ||
					(x.DateTimeGroup.StartDate < endDate && endDate <= x.DateTimeGroup.EndDate)).ToList();

				clustersInRange.Count().Should().Be(1);
			}
		}
		
		[Theory]
		[ClassData(typeof(DateRangeTheoryData))]
		public async Task TestNumberOfResultsInCluster(DateRange testRange)
		{
			// arrange
			// act
			var data = await FormClusters(testRange);
			
			// assert
			foreach (var cluster in data)
			{
				var startDate = cluster.DateTimeGroup.StartDate;
				var endDate = cluster.DateTimeGroup.EndDate;

				var numResultsInDatabase = _database.RosterTimelineEventsEntity.Count(x => x.Created >= startDate && x.Created < endDate);
				cluster.NumberOfResults.Should().Be(numResultsInDatabase);
			}
		}
	}
}