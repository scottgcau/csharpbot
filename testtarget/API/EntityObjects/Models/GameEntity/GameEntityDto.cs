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
using ServersideGameEntity = Sportstats.Models.GameEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Scheduled game
	/// </summary>
	public class GameEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public DateTime? Datestart { get; set; }
		public int? Hometeamid { get; set; }
		public int? Awayteamid { get; set; }

		public Guid? VenueId { get; set; }
		public Guid ScheduleId { get; set; }
		public ICollection<PersonEntity> Refereess { get; set; }

		public GameEntityDto(GameEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Datestart = model.Datestart;
			Hometeamid = model.Hometeamid;
			Awayteamid = model.Awayteamid;
			VenueId = model.VenueId;
			ScheduleId = model.ScheduleId;
			Refereess = model.Refereess;
		}

		public GameEntityDto(ServersideGameEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Datestart = model.Datestart;
			Hometeamid = model.Hometeamid;
			Awayteamid = model.Awayteamid;
			VenueId = model.VenueId;
			ScheduleId = model.ScheduleId;
			Refereess = model.Refereess.Select(PersonEntityDto.Convert).ToList();
		}

		public GameEntity GetTesttargetGameEntity()
		{
			return new GameEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Datestart = Datestart,
				Hometeamid = Hometeamid,
				Awayteamid = Awayteamid,
				VenueId = VenueId,
				ScheduleId = ScheduleId,
				Refereess = Refereess,
			};
		}

		public ServersideGameEntity GetServersideGameEntity()
		{
			return new ServersideGameEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Datestart = Datestart,
				Hometeamid = Hometeamid,
				Awayteamid = Awayteamid,
				VenueId = VenueId,
				ScheduleId = ScheduleId,
				Refereess = Refereess?.Select(PersonEntityDto.Convert).ToList(),
			};
		}

		public static ServersideGameEntity Convert(GameEntity model)
		{
			var dto = new GameEntityDto(model);
			return dto.GetServersideGameEntity();
		}

		public static GameEntity Convert(ServersideGameEntity model)
		{
			var dto = new GameEntityDto(model);
			return dto.GetTesttargetGameEntity();
		}
	}
}