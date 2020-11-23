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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sportstats.Helpers;
using Sportstats.Models;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Services.Interfaces
{
	public interface ITimelineGroupingService
	{
		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end

		// % protected region % [Override GraphQueryGroupingResult here] off begin
		/// <summary>
		/// Uses a TimelineFilter to find clustered groups of data, grouped by DateTime.
		/// </summary>
		/// <param name="timelineFilter">Filter to apply of the data</param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>A task that resolves to an IOrderedEnumerable of TimelineGroupDateQueryResult</returns>
		Task<IOrderedEnumerable<TimelineGroupDateQueryResult<T>>> GraphQueryGroupingResult<T>(TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override GraphQueryGroupingResult here] end

		// % protected region % [Override GraphQueryResolutionSwitchResult here] off begin
		/// <summary>
		/// Switches on the time difference in a TimelineFilter and collects grouped data of time T
		/// </summary>
		/// <param name="timelineFilter">Filter to apply of the data</param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>A task that resolves to a GraphQueryResolutionSwitchResult of type T</returns>
		Task<GraphQueryResolutionSwitchResult<T>> GraphQueryResolutionSwitchResult<T>(TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override GraphQueryResolutionSwitchResult here] end

		// % protected region % [Override MonthlyGroupingQuery here] off begin
		/// <summary>
		/// Queries the database for groups of data.
		/// </summary>
		/// <param name="timelineFilter">Filter to apply of the data</param>
		/// <param name="months">Amount of months in each group </param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>A task that resolves to a list of TimelineGroupDateQueryResult</returns>
		Task<List<TimelineGroupDateQueryResult<T>>> MonthlyGroupingQuery<T>(TimelineFilter timelineFilter, int months)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override MonthlyGroupingQuery here] end

		// % protected region % [Override DailyGroupingQuery here] off begin
		/// <summary>
		/// Queries the database for groups of data.
		/// </summary>
		/// <param name="timelineFilter">Filter to apply of the data</param>
		/// <param name="days">Amount of days in each group</param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>A task that resolves to a list of TimelineGroupDateQueryResult</returns>
		Task<List<TimelineGroupDateQueryResult<T>>> DailyGroupingQuery<T>(TimelineFilter timelineFilter, int days)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override DailyGroupingQuery here] end

		// % protected region % [Override HourlyGroupingQuery here] off begin
		/// <summary>
		/// Queries the database for groups of data.
		/// </summary>
		/// <param name="timelineFilter">Filter to apply of the data</param>
		/// <param name="hours">Amount of hours in each group</param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>A task that resolves to a list of TimelineGroupDateQueryResult</returns>
		Task<List<TimelineGroupDateQueryResult<T>>> HourlyGroupingQuery<T>(TimelineFilter timelineFilter, int hours)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override HourlyGroupingQuery here] end
		
		// % protected region % [Override GetBaseQuery here] off begin
		/// <summary>
		/// Base query used for clustering timeline data
		/// </summary>
		/// <param name="timelineFilter">Filter to apply to over data</param>
		/// <typeparam name="T">The generic type of the query</typeparam>
		/// <returns>An IQueryable of type T</returns>
		IQueryable<T> GetBaseQuery<T>(TimelineFilter timelineFilter)
			where T : class, IOwnerAbstractModel, new();
		// % protected region % [Override GetBaseQuery here] end
	}
}