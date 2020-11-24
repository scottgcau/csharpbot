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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sportstats.Helpers;
using Sportstats.Models;
using Sportstats.Services.Interfaces;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Services
{
	public class TimelineGroupingService : ITimelineGroupingService
	{
		private readonly ICrudService _crudService;

		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		// % protected region % [Override TimelineGroupingService here] off begin
		public TimelineGroupingService(ICrudService crudService)
		{
			_crudService = crudService;
		}
		// % protected region % [Override TimelineGroupingService here] end

		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end

		/// <inheritdoc />
		// % protected region % [Override GraphQueryGroupingResult here] off begin
		public async Task<IOrderedEnumerable<TimelineGroupDateQueryResult<T>>> GraphQueryGroupingResult<T>(
			TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new()
		{
			var graphQueryResolutionSwitchResult = await GraphQueryResolutionSwitchResult<T>(timelineFilter);
			var timelineGroupDateQueryResults = graphQueryResolutionSwitchResult.QueryResult.OrderBy(x => x.DateTimeGroup.StartDate).ToList();
			
			// add missing date ranges
			var startDate = graphQueryResolutionSwitchResult.DateRange.StartDate;
			while (startDate < graphQueryResolutionSwitchResult.DateRange.EndDate)
			{
				var endDate = graphQueryResolutionSwitchResult.DateIncrementingFunc(startDate);

				var resultInRange = timelineGroupDateQueryResults
					.FirstOrDefault(x => 
						startDate < x.DateTimeGroup.EndDate && x.DateTimeGroup.EndDate <= endDate);

				if (resultInRange == null)
				{
					timelineGroupDateQueryResults.Add(new TimelineGroupDateQueryResult<T>
					{
						NumberOfResults = 0,
						DateTimeGroup = new DateRange{ StartDate = startDate, EndDate = endDate }
					});
					startDate = endDate;
				}
				else
				{
					startDate = resultInRange.DateTimeGroup.EndDate;
				}
				
			}
			
			// line up all of the start and end dates
			foreach (var timelineGroupDateQueryResult in timelineGroupDateQueryResults)
			{
				var nextResult = timelineGroupDateQueryResults
					.Where(x => x.DateTimeGroup.StartDate > timelineGroupDateQueryResult.DateTimeGroup.StartDate)
					.MinByOrDefault(x => x.DateTimeGroup.StartDate);

				if (nextResult != null)
				{
					timelineGroupDateQueryResult.DateTimeGroup.EndDate = nextResult.DateTimeGroup.StartDate;
				}
			}
			
			// line up the last end date
			if (timelineFilter.DateRange.EndDate != default)
			{
				var lastItem = timelineGroupDateQueryResults.MaxByOrDefault(x => x.DateTimeGroup.StartDate);
				lastItem.DateTimeGroup.EndDate = timelineFilter.DateRange.EndDate;
			}

			// join last two entries into single entry if they span less then the expected date increment after consolidating
			// the end date of the entry to align with the user specified date filter.
			if (timelineGroupDateQueryResults.Count >= 2)
			{
				var lastTwoEntries= timelineGroupDateQueryResults
					.OrderByDescending(x => x.DateTimeGroup.StartDate)
					.Take(2)
					.ToList();
				var expectedEndDate =
					graphQueryResolutionSwitchResult.DateIncrementingFunc(lastTwoEntries.Last().DateTimeGroup
						.StartDate);
				if (lastTwoEntries.First().DateTimeGroup.EndDate < expectedEndDate)
				{
					lastTwoEntries.Last().DateTimeGroup.EndDate = lastTwoEntries.First().DateTimeGroup.EndDate;
					lastTwoEntries.Last().NumberOfResults += lastTwoEntries.First().NumberOfResults;
					if (lastTwoEntries.First().FirstResult != null)
					{
						lastTwoEntries.Last().FirstResult = lastTwoEntries.First().FirstResult;
					}
					timelineGroupDateQueryResults.Remove(lastTwoEntries.First());
				}
			}
			
			return timelineGroupDateQueryResults.OrderBy(x => x.DateTimeGroup.StartDate);
		}
		// % protected region % [Override GraphQueryGroupingResult here] end

		/// <inheritdoc />
		// % protected region % [Override GraphQueryResolutionSwitchResult here] off begin
		public async Task<GraphQueryResolutionSwitchResult<T>> GraphQueryResolutionSwitchResult<T>(
			TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new()
		{
			List<TimelineGroupDateQueryResult<T>> queryResult;
			Func<DateTime, DateTime> dateIncrementingFunc;

			var dateRange = timelineFilter.DateRange;
			dateRange.StartDate = dateRange.StartDate.AddMinutes(-dateRange.StartDate.Minute)
				.AddSeconds(-dateRange.StartDate.Second);
			var daysDifference = (dateRange.EndDate - dateRange.StartDate).TotalDays;
			var startDate = dateRange.StartDate;

			if (daysDifference > 16)
			{
				startDate = startDate.AddHours(-startDate.Hour);
			}

			if (daysDifference > 200)
			{
				startDate = startDate.AddDays(-startDate.Day + 1);
			}

			switch (daysDifference)
			{
				case var _ when daysDifference <= 0.5:
					dateIncrementingFunc = x => x.AddHours(1);
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 1);
					break;
				case var _ when daysDifference > 0.5 && daysDifference <= 1:
					dateIncrementingFunc = x => x.AddHours(2);
					startDate = startDate.AddHours(-(startDate.Hour % 2));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 2);
					break;
				case var _ when daysDifference > 1 && daysDifference <= 2:
					dateIncrementingFunc = x => x.AddHours(4);
					startDate = startDate.AddHours(-(startDate.Hour % 4));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 4);
					break;
				case var _ when daysDifference > 2 && daysDifference <= 4:
					dateIncrementingFunc = x => x.AddHours(8);
					startDate = startDate.AddHours(-(startDate.Hour % 8));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 8);
					break;
				case var _ when daysDifference > 4 && daysDifference <= 6:
					dateIncrementingFunc = x => x.AddHours(12);
					startDate = startDate.AddHours(-(startDate.Hour % 12));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 12);
					break;
				case var _ when daysDifference > 6 && daysDifference <= 12:
					dateIncrementingFunc = x => x.AddHours(24);
					startDate = startDate.AddHours(-startDate.Hour);
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await DailyGroupingQuery<T>(timelineFilter, 1);
					break;
				case var _ when daysDifference > 12 && daysDifference <= 24:
					dateIncrementingFunc = x => x.AddHours(48);
					startDate = startDate.AddDays(-((startDate.Day + 1) % 2));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await DailyGroupingQuery<T>(timelineFilter, 2);
					break;
				case var _ when daysDifference > 24 && daysDifference <= 40:
					dateIncrementingFunc = x => x.AddHours(96);
					startDate = startDate.AddDays(-((startDate.Day + 1) % 4));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await DailyGroupingQuery<T>(timelineFilter, 4);
					break;
				case var _ when daysDifference > 40 && daysDifference <= 200:
					dateIncrementingFunc = x => x.AddDays(17);
					startDate = startDate.AddDays(-((startDate.Day + 1) % 17));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await DailyGroupingQuery<T>(timelineFilter, 17);
					break;
				case var _ when daysDifference > 200 && daysDifference <= 400:
					dateIncrementingFunc = x => x.AddMonths(1);
					queryResult = await MonthlyGroupingQuery<T>(timelineFilter, 1);
					break;
				case var _ when daysDifference > 400 && daysDifference <= 900:
					dateIncrementingFunc = x => x.AddMonths(3);
					startDate = startDate.AddMonths(-((startDate.Month + 1) % 3));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await MonthlyGroupingQuery<T>(timelineFilter, 3);
					break;
				case var _ when daysDifference > 900:
					dateIncrementingFunc = x => x.AddMonths(6);
					startDate = startDate.AddMonths(-((startDate.Month + 1) % 6));
					timelineFilter.DateRange.StartDate = startDate;
					queryResult = await MonthlyGroupingQuery<T>(timelineFilter, 6);
					break;
				default:
					dateIncrementingFunc = x => x.AddHours(1);
					queryResult = await HourlyGroupingQuery<T>(timelineFilter, 1);
					break;
			}

			dateRange.StartDate = startDate;

			return new GraphQueryResolutionSwitchResult<T>
			{
				DateRange = dateRange,
				QueryResult = queryResult,
				DateIncrementingFunc = dateIncrementingFunc
			};
		}
		// % protected region % [Override GraphQueryResolutionSwitchResult here] end

		/// <inheritdoc />
		// % protected region % [Override MonthlyGroupingQuery here] off begin
		public Task<List<TimelineGroupDateQueryResult<T>>> MonthlyGroupingQuery<T>(TimelineFilter timelineFilter, int months) 
			where T : class, IOwnerAbstractModel, new()
		{
			var baseQuery = GetBaseQuery<T>(timelineFilter);
			return baseQuery
				.GroupBy(x => x.Created.Year.ToString() + "-" + ((x.Created.Month - 1)/months*months + 1 ))
				.Select(x => new
				{
					Range = x.Key,
					Count = x.Count()
				})
				.Select(group => baseQuery
					.Where(x => x.Created.Year.ToString() + "-" + ((x.Created.Month - 1)/months*months + 1 ) == group.Range)
					.OrderByDescending(x => x.Created)
					.Select(x => new TimelineGroupDateQueryResult<T>()
					{
						FirstResult = x,
						NumberOfResults = group.Count,
						DateTimeGroup = new DateRange
						{
							StartDate = DateTime.Parse(group.Range),
							EndDate = DateTime.Parse(group.Range).AddMonths(months)
						}
					})
					.FirstOrDefault())
				.ToListAsync();
		}
		// % protected region % [Override MonthlyGroupingQuery here] end

		/// <inheritdoc />
		// % protected region % [Override DailyGroupingQuery here] off begin
		public Task<List<TimelineGroupDateQueryResult<T>>> DailyGroupingQuery<T>(TimelineFilter timelineFilter, int days) 
			where T : class, IOwnerAbstractModel, new()
		{
			var baseQuery = GetBaseQuery<T>(timelineFilter);
			return baseQuery
				.GroupBy(x => x.Created.Year.ToString() + "-" + x.Created.Month + "-" + ((x.Created.Day - 1)/days*days + 1))
				.Select(x => new
				{
					Range = x.Key,
					Count = x.Count()
				})
				.Select(group => baseQuery
					.Where(x => x.Created.Year.ToString() + "-" + x.Created.Month + "-" + ((x.Created.Day - 1)/days*days + 1) == group.Range)
					.OrderByDescending(x => x.Created)
					.Select(x => new TimelineGroupDateQueryResult<T>()
					{
						FirstResult = x,
						NumberOfResults = group.Count,
						DateTimeGroup = new DateRange
						{
							StartDate = DateTime.Parse(group.Range),
							EndDate =  DateTime.Parse(group.Range).AddDays(days)
						}
					})
					.FirstOrDefault())
				.ToListAsync();
		}
		// % protected region % [Override DailyGroupingQuery here] end

		/// <inheritdoc />
		// % protected region % [Override HourlyGroupingQuery here] off begin
		public Task<List<TimelineGroupDateQueryResult<T>>> HourlyGroupingQuery<T>(TimelineFilter timelineFilter, int hours) 
			where T : class, IOwnerAbstractModel, new()
		{
			var baseQuery = GetBaseQuery<T>(timelineFilter);
			return baseQuery
				.GroupBy(x => x.Created.Year.ToString() + "-" + x.Created.Month + "-" + x.Created.Day + "T" + ((x.Created.Hour/hours)*hours).ToString().PadLeft(2, '0') + ":00:00")
				.Select(x => new
				{
					Range = x.Key,
					Count = x.Count()
				})
				.Select(group => baseQuery
					.Where(x => x.Created.Year.ToString() + "-" + x.Created.Month + "-" + x.Created.Day + "T" + ((x.Created.Hour/hours)*hours).ToString().PadLeft(2, '0') + ":00:00" == group.Range)
					.OrderByDescending(x => x.Created)
					.Select(x => new TimelineGroupDateQueryResult<T>()
					{
						FirstResult = x,
						NumberOfResults = group.Count,
						DateTimeGroup = new DateRange
						{
							StartDate = DateTime.Parse(group.Range),
							EndDate = DateTime.Parse(group.Range).AddHours(hours)
						}
					})
					.FirstOrDefault())
				.ToListAsync();
		}
		// % protected region % [Override HourlyGroupingQuery here] end

		/// <inheritdoc />
		// % protected region % [Override GetBaseQuery here] off begin
		public IQueryable<T> GetBaseQuery<T>(TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new()
		{
			return _crudService.Get<T>()
				.AddConditionalWhereFilter(timelineFilter.Conditions)
				.Where(x => x.Created >= timelineFilter.DateRange.StartDate)
				.Where(x => x.Created < timelineFilter.DateRange.EndDate);
		}
		// % protected region % [Override GetBaseQuery here] end
	}
}