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
using System.Data.OleDb;
using System.Threading;
using System.Threading.Tasks;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models.Interfaces
{
	public interface ITimelineEntity
	{
		// % protected region % [Add any extra methods here] off begin
		// % protected region % [Add any extra methods here] end

		// % protected region % [Override CreateTimelineEventsAsync here] off begin
		Task CreateTimelineEventsAsync<TEntity>(
			TEntity original,
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default) where TEntity : IOwnerAbstractModel;
		// % protected region % [Override CreateTimelineEventsAsync here] end

		// % protected region % [Override CreateTimelineCreateEventsAsync here] off begin
		Task CreateTimelineCreateEventsAsync(
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default);
		// % protected region % [Override CreateTimelineCreateEventsAsync here] end
		
		// % protected region % [Override CreateTimelineDeleteEventsAsync here] off begin
		Task CreateTimelineDeleteEventsAsync(
			SportstatsDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default);
		// % protected region % [Override CreateTimelineDeleteEventsAsync here] end
	}
}