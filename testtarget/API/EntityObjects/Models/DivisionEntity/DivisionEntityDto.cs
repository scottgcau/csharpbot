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
using ServersideDivisionEntity = Sportstats.Models.DivisionEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Division entity
	/// </summary>
	public class DivisionEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Fullname { get; set; }
		public String Shortname { get; set; }

		public ICollection<TeamEntity> Teamss { get; set; }
		public Guid? SeasonId { get; set; }

		public DivisionEntityDto(DivisionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Teamss = model.Teamss;
			SeasonId = model.SeasonId;
		}

		public DivisionEntityDto(ServersideDivisionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Fullname = model.Fullname;
			Shortname = model.Shortname;
			Teamss = model.Teamss.Select(TeamEntityDto.Convert).ToList();
			SeasonId = model.SeasonId;
		}

		public DivisionEntity GetTesttargetDivisionEntity()
		{
			return new DivisionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Fullname = Fullname,
				Shortname = Shortname,
				Teamss = Teamss,
				SeasonId = SeasonId,
			};
		}

		public ServersideDivisionEntity GetServersideDivisionEntity()
		{
			return new ServersideDivisionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Fullname = Fullname,
				Shortname = Shortname,
				Teamss = Teamss?.Select(TeamEntityDto.Convert).ToList(),
				SeasonId = SeasonId,
			};
		}

		public static ServersideDivisionEntity Convert(DivisionEntity model)
		{
			var dto = new DivisionEntityDto(model);
			return dto.GetServersideDivisionEntity();
		}

		public static DivisionEntity Convert(ServersideDivisionEntity model)
		{
			var dto = new DivisionEntityDto(model);
			return dto.GetTesttargetDivisionEntity();
		}
	}
}