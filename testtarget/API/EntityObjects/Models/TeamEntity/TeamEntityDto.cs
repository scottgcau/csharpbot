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
using ServersideTeamEntity = Sportstats.Models.TeamEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Team entity
	/// </summary>
	public class TeamEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public String Represents { get; set; }
		public String Fullname { get; set; }
		public String Shortname { get; set; }

		public ICollection<RosterEntity> Rosterss { get; set; }
		public Guid? LeagueId { get; set; }

		public TeamEntityDto(TeamEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Represents = model.Represents;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Rosterss = model.Rosterss;
			LeagueId = model.LeagueId;
		}

		public TeamEntityDto(ServersideTeamEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Represents = model.Represents;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Rosterss = model.Rosterss.Select(RosterEntityDto.Convert).ToList();
			LeagueId = model.LeagueId;
		}

		public TeamEntity GetTesttargetTeamEntity()
		{
			return new TeamEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Represents = Represents,
				Fullname = Fullname,
				Shortname = Shortname,
				Rosterss = Rosterss,
				LeagueId = LeagueId,
			};
		}

		public ServersideTeamEntity GetServersideTeamEntity()
		{
			return new ServersideTeamEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Represents = Represents,
				Fullname = Fullname,
				Shortname = Shortname,
				Rosterss = Rosterss?.Select(RosterEntityDto.Convert).ToList(),
				LeagueId = LeagueId,
			};
		}

		public static ServersideTeamEntity Convert(TeamEntity model)
		{
			var dto = new TeamEntityDto(model);
			return dto.GetServersideTeamEntity();
		}

		public static TeamEntity Convert(ServersideTeamEntity model)
		{
			var dto = new TeamEntityDto(model);
			return dto.GetTesttargetTeamEntity();
		}
	}
}