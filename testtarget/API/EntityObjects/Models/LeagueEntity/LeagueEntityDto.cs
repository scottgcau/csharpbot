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
using ServersideLeagueEntity = Sportstats.Models.LeagueEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// League entity
	/// </summary>
	public class LeagueEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public String Fullname { get; set; }
		public String Shortname { get; set; }

		public ICollection<SeasonEntity> Seasonss { get; set; }
		public ICollection<TeamEntity> Teamss { get; set; }
		public Guid SportId { get; set; }

		public LeagueEntityDto(LeagueEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Seasonss = model.Seasonss;
			Teamss = model.Teamss;
			SportId = model.SportId;
		}

		public LeagueEntityDto(ServersideLeagueEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Seasonss = model.Seasonss.Select(SeasonEntityDto.Convert).ToList();
			Teamss = model.Teamss.Select(TeamEntityDto.Convert).ToList();
			SportId = model.SportId;
		}

		public LeagueEntity GetTesttargetLeagueEntity()
		{
			return new LeagueEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Shortname = Shortname,
				Seasonss = Seasonss,
				Teamss = Teamss,
				SportId = SportId,
			};
		}

		public ServersideLeagueEntity GetServersideLeagueEntity()
		{
			return new ServersideLeagueEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Shortname = Shortname,
				Seasonss = Seasonss?.Select(SeasonEntityDto.Convert).ToList(),
				Teamss = Teamss?.Select(TeamEntityDto.Convert).ToList(),
				SportId = SportId,
			};
		}

		public static ServersideLeagueEntity Convert(LeagueEntity model)
		{
			var dto = new LeagueEntityDto(model);
			return dto.GetServersideLeagueEntity();
		}

		public static LeagueEntity Convert(ServersideLeagueEntity model)
		{
			var dto = new LeagueEntityDto(model);
			return dto.GetTesttargetLeagueEntity();
		}
	}
}