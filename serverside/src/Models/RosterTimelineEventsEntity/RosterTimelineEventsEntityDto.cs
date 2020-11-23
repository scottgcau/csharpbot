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
using System.Collections.Generic;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Models
{
	/// <summary>
	/// Timeline Events Of Roster
	/// </summary>
	public class RosterTimelineEventsEntityDto : ModelDto<RosterTimelineEventsEntity>
	{
		// % protected region % [Customise Action here] off begin
		/// <summary>
		/// The action taken
		/// </summary>
		public String Action { get; set; }
		// % protected region % [Customise Action here] end

		// % protected region % [Customise ActionTitle here] off begin
		/// <summary>
		/// The title of the action taken
		/// </summary>
		public String ActionTitle { get; set; }
		// % protected region % [Customise ActionTitle here] end

		// % protected region % [Customise Description here] off begin
		/// <summary>
		/// Decription of the event
		/// </summary>
		public String Description { get; set; }
		// % protected region % [Customise Description here] end

		// % protected region % [Customise GroupId here] off begin
		/// <summary>
		/// Id of the group the events belong to
		/// </summary>
		public Guid? GroupId { get; set; }
		// % protected region % [Customise GroupId here] end


		// % protected region % [Customise EntityId here] off begin
		public Guid? EntityId { get; set; }
		// % protected region % [Customise EntityId here] end

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public RosterTimelineEventsEntityDto(RosterTimelineEventsEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public RosterTimelineEventsEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override RosterTimelineEventsEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new RosterTimelineEventsEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Action = Action,
				ActionTitle = ActionTitle,
				Description = Description,
				GroupId = GroupId,
				EntityId  = EntityId,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<RosterTimelineEventsEntity> LoadModelData(RosterTimelineEventsEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Action = model.Action;
			ActionTitle = model.ActionTitle;
			Description = model.Description;
			GroupId = model.GroupId;
			EntityId  = model.EntityId;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}