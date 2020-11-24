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
using ServersideSeasonEntity = Sportstats.Models.SeasonEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Season entity
	/// </summary>
	public class SeasonEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public DateTime? Startdate { get; set; }
		public DateTime? Enddate { get; set; }
		public String Fullname { get; set; }
		public String Shortname { get; set; }

		public ICollection<DivisionEntity> Divisionss { get; set; }
		public Guid? LeagueId { get; set; }
		public ICollection<RosterEntity> Rosterss { get; set; }
		public ICollection<ScheduleEntity> Scheduless { get; set; }

		public SeasonEntityDto(SeasonEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Startdate = model.Startdate;
			Enddate = model.Enddate;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Divisionss = model.Divisionss;
			LeagueId = model.LeagueId;
			Rosterss = model.Rosterss;
			Scheduless = model.Scheduless;
		}

		public SeasonEntityDto(ServersideSeasonEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Startdate = model.Startdate;
			Enddate = model.Enddate;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Divisionss = model.Divisionss.Select(DivisionEntityDto.Convert).ToList();
			LeagueId = model.LeagueId;
			Rosterss = model.Rosterss.Select(RosterEntityDto.Convert).ToList();
			Scheduless = model.Scheduless.Select(ScheduleEntityDto.Convert).ToList();
		}

		public SeasonEntity GetTesttargetSeasonEntity()
		{
			return new SeasonEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Startdate = Startdate,
				Enddate = Enddate,
				Fullname = Fullname,
				Shortname = Shortname,
				Divisionss = Divisionss,
				LeagueId = LeagueId,
				Rosterss = Rosterss,
				Scheduless = Scheduless,
			};
		}

		public ServersideSeasonEntity GetServersideSeasonEntity()
		{
			return new ServersideSeasonEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Startdate = Startdate,
				Enddate = Enddate,
				Fullname = Fullname,
				Shortname = Shortname,
				Divisionss = Divisionss?.Select(DivisionEntityDto.Convert).ToList(),
				LeagueId = LeagueId,
				Rosterss = Rosterss?.Select(RosterEntityDto.Convert).ToList(),
				Scheduless = Scheduless?.Select(ScheduleEntityDto.Convert).ToList(),
			};
		}

		public static ServersideSeasonEntity Convert(SeasonEntity model)
		{
			var dto = new SeasonEntityDto(model);
			return dto.GetServersideSeasonEntity();
		}

		public static SeasonEntity Convert(ServersideSeasonEntity model)
		{
			var dto = new SeasonEntityDto(model);
			return dto.GetTesttargetSeasonEntity();
		}
	}
}