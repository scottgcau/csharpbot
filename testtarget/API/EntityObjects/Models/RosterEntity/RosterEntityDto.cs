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
using ServersideRosterEntity = Sportstats.Models.RosterEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Roster entity
	/// </summary>
	public class RosterEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }

		public ICollection<RosterassignmentEntity> Rosterassignmentss { get; set; }
		public Guid SeasonId { get; set; }
		public Guid? TeamId { get; set; }
		public ICollection<RosterTimelineEventsEntity> LoggedEvents { get; set; }

		public RosterEntityDto(RosterEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Rosterassignmentss = model.Rosterassignmentss;
			SeasonId = model.SeasonId;
			TeamId = model.TeamId;
			LoggedEvents = model.LoggedEvents;
		}

		public RosterEntityDto(ServersideRosterEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Rosterassignmentss = model.Rosterassignmentss.Select(RosterassignmentEntityDto.Convert).ToList();
			SeasonId = model.SeasonId;
			TeamId = model.TeamId;
			LoggedEvents = model.LoggedEvents.Select(RosterTimelineEventsEntityDto.Convert).ToList();
		}

		public RosterEntity GetTesttargetRosterEntity()
		{
			return new RosterEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Rosterassignmentss = Rosterassignmentss,
				SeasonId = SeasonId,
				TeamId = TeamId,
				LoggedEvents = LoggedEvents,
			};
		}

		public ServersideRosterEntity GetServersideRosterEntity()
		{
			return new ServersideRosterEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Rosterassignmentss = Rosterassignmentss?.Select(RosterassignmentEntityDto.Convert).ToList(),
				SeasonId = SeasonId,
				TeamId = TeamId,
				LoggedEvents = LoggedEvents?.Select(RosterTimelineEventsEntityDto.Convert).ToList(),
			};
		}

		public static ServersideRosterEntity Convert(RosterEntity model)
		{
			var dto = new RosterEntityDto(model);
			return dto.GetServersideRosterEntity();
		}

		public static RosterEntity Convert(ServersideRosterEntity model)
		{
			var dto = new RosterEntityDto(model);
			return dto.GetTesttargetRosterEntity();
		}
	}
}