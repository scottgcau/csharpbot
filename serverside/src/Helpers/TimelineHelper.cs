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
using GraphQL.EntityFramework;
using Sportstats.Models;
using Sportstats.Models.Interfaces;
using Sportstats.Services.Interfaces;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Helpers
{

	// % protected region % [Add any extra methods here] off begin
	// % protected region % [Add any extra methods here] end
	
	// % protected region % [Override TimelineFilter here] off begin
	public class TimelineFilter 
	{
		public TimeFrameOption TimeFrame { get; set; }
		public IEnumerable<IEnumerable<WhereExpression>> Conditions { get; set; }
		public DateRange DateRange { get; set; }
	}
	// % protected region % [Override TimelineFilter here] end

	// % protected region % [Override TimeFrameOption here] off begin
	public enum TimeFrameOption
	{
		Weeks,
		Months
	}
	// % protected region % [Override TimeFrameOption here] end
	
	// % protected region % [Override TimelineGroupDateQueryResult here] off begin
	public class TimelineGroupDateQueryResult<T>
	{
		public T FirstResult { get; set; }
		public int NumberOfResults { get; set; }
		public DateRange DateTimeGroup { get; set; }
	}
	// % protected region % [Override TimelineGroupDateQueryResult here] end
	
	// % protected region % [Override DateRange here] off begin
	public class DateRange
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		
		public DateRange() { }

		public DateRange(DateTime startDate, TimeFrameOption timeFrame)
		{
			StartDate = startDate;
			EndDate = timeFrame switch
			{
				TimeFrameOption.Months => startDate.AddMonths(1),
				TimeFrameOption.Weeks => startDate.AddDays(7),
				_ => throw new Exception ("Unknown TimeFrameOption")
			};
		}
	}
	// % protected region % [Override DateRange here] end

	// % protected region % [Override GraphQueryResolutionSwitchResult here] off begin
	public class GraphQueryResolutionSwitchResult<T> 
		where T : class, IOwnerAbstractModel, new()
	{
		public Func<DateTime, DateTime> DateIncrementingFunc { get; set; }
		public List<TimelineGroupDateQueryResult<T>> QueryResult { get; set; }
		public DateRange DateRange { get; set; }
	}
	// % protected region % [Override GraphQueryResolutionSwitchResult here] end
	
	public static class TimelineHelper
	{
		// % protected region % [Override CreateUpdateDescription here] off begin
		public static string CreateUpdateDescription(string attribute, string old, string current) =>
			$"Updated {attribute} from {old} to {current}";
		// % protected region % [Override CreateUpdateDescription here] end


		// % protected region % [Override CreateCreateDescription here] off begin
		public static string CreateCreateDescription(string modelType) =>
			$"Created {modelType}";
		// % protected region % [Override CreateCreateDescription here] end
		
		// % protected region % [Override CreateDeleteDescription here] off begin
		public static string CreateDeleteDescription(string modelType, Guid id) =>
			$"Deleted {modelType} with Id {id}";
		// % protected region % [Override CreateDeleteDescription here] end

		// % protected region % [Override AddEvent here] off begin
		public static void AddEvent<T>(
			this ICollection<ITimelineEventEntity> descriptionList, 
			string action, 
			string actionTitle,
			string description,
			Guid id)
			where T : ITimelineEventEntity, new() 
		{
			descriptionList.Add( new T
			{
				Created = DateTime.UtcNow,
				Modified = DateTime.UtcNow,
				Action = action, 
				ActionTitle = actionTitle,
				Description = description,
				GroupId = id,
				EntityId = id
			});
		}
		// % protected region % [Override AddEvent here] end

		// % protected region % [Override ConditionalAddUpdateEvent here] off begin
		public static void ConditionalAddUpdateEvent<T>(
			this List<ITimelineEventEntity> descriptionList,
			string entityName,
			string attributeName, 
			object originalValue,
			object newValue,
			Guid id)
			where T : ITimelineEventEntity, new()
		{
			if (!Equals(newValue, originalValue))
			{
				var description = CreateUpdateDescription(
					attributeName, 
					originalValue?.ToString() ?? "undefined", 
					newValue?.ToString() ?? "undefined");
				descriptionList.AddEvent<T>("Updated", $"Updated {entityName}", description, id);
			}
		}
		// % protected region % [Override ConditionalAddUpdateEvent here] end

		
		// % protected region % [Override AddCreateEvent here] off begin
		public static void AddCreateEvent<T>(
			this List<ITimelineEventEntity> descriptionList,
			string entityName,
			Guid id)
			where T : ITimelineEventEntity, new()
		{
			var description = CreateCreateDescription(entityName);
			descriptionList.AddEvent<T>("Created", $"Created {entityName}", description, id);
		}
		// % protected region % [Override AddCreateEvent here] end
		
		// % protected region % [Override AddDeleteEvent here] off begin
		public static void AddDeleteEvent<T>(
			this List<ITimelineEventEntity> descriptionList,
			string entityName,
			Guid id)
			where T : ITimelineEventEntity, new()
		{
			var description = CreateDeleteDescription(entityName, id);
			descriptionList.AddEvent<T>("Deleted", $"Deleted {entityName}", description, id);
		}
		// % protected region % [Override AddDeleteEvent here] end
		
		// % protected region % [Override GetDistinctDatesForTimeFrame here] off begin
		public static IEnumerable<DateTime> GetDistinctDatesForTimeFrame<T>(
			this ICrudService crudService, 
			TimeFrameOption timeFrame, 
			IEnumerable<IEnumerable<WhereExpression>> conditions)
			where T : class, IOwnerAbstractModel, new()
		{
			return timeFrame switch
			{
				TimeFrameOption.Months => crudService.GetDistinctCreatedMonths<T>(conditions),
				TimeFrameOption.Weeks => crudService.GetDistinctCreatedWeeks<T>(conditions),
				_ => throw new Exception("Unknown TimeFrameOption")
			};
		}
		// % protected region % [Override GetDistinctDatesForTimeFrame here] end

		// % protected region % [Override GetDistinctCreatedMonths here] off begin
		private static IEnumerable<DateTime> GetDistinctCreatedMonths<T>(
			this ICrudService crudService, 
			IEnumerable<IEnumerable<WhereExpression>> conditions)
			where T : class, IOwnerAbstractModel, new()
		{
			return crudService
				.Get<T>()
				.AddConditionalWhereFilter(conditions)
				.OrderByDescending(x => x.Created)
				.Select(x => new
				{
					Month = x.Created.Month,
					Year = x.Created.Year
				})
				.Distinct()
				.Select(x => new DateTime(x.Year, x.Month, 1))
				.ToList();
		}
		// % protected region % [Override GetDistinctCreatedMonths here] end
		
		// % protected region % [Override GetDistinctCreatedWeeks here] off begin
		private static IEnumerable<DateTime> GetDistinctCreatedWeeks<T>(
			this ICrudService crudService, 
			IEnumerable<IEnumerable<WhereExpression>> conditions)
			where T : class, IOwnerAbstractModel, new()
		{
			return crudService
				.Get<T>()
				.AddConditionalWhereFilter(conditions)
				.OrderByDescending(x => x.Created)
				.Select(x => new
				{
					StartOfWeek = x.Created.Day - (int)x.Created.DayOfWeek,
					Year = x.Created.Year,
					Month = x.Created.Month,
				})
				.Distinct()
				.ToList()
				.Select(x => new DateTime(x.Year, x.Month, 1).AddDays(x.StartOfWeek - 1));
		}
		// % protected region % [Override GetDistinctCreatedWeeks here] end

		// % protected region % [Add any extra TimelineHelper static methods here] off begin
		// % protected region % [Add any extra TimelineHelper static methods here] end
	}
}