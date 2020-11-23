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
using Sportstats.Enums;
using TestEnums = EntityObject.Enums;
using ServersideScheduleEntity = Sportstats.Models.ScheduleEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Schedule entity
	/// </summary>
	public class ScheduleEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string Name { get; set; }
		public String Fullname { get; set; }
		public Scheduletype Scheduletype { get; set; }

		public ICollection<GameEntity> Gamess { get; set; }
		public Guid SeasonId { get; set; }

		public ScheduleEntityDto(ScheduleEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Scheduletype = (Scheduletype)model.Scheduletype;
			Gamess = model.Gamess;
			SeasonId = model.SeasonId;
		}

		public ScheduleEntityDto(ServersideScheduleEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			Fullname = model.Fullname;
			Scheduletype = model.Scheduletype;
			Gamess = model.Gamess.Select(GameEntityDto.Convert).ToList();
			SeasonId = model.SeasonId;
		}

		public ScheduleEntity GetTesttargetScheduleEntity()
		{
			return new ScheduleEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Scheduletype = (TestEnums.Scheduletype)Scheduletype,
				Gamess = Gamess,
				SeasonId = SeasonId,
			};
		}

		public ServersideScheduleEntity GetServersideScheduleEntity()
		{
			return new ServersideScheduleEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				Fullname = Fullname,
				Scheduletype = Scheduletype,
				Gamess = Gamess?.Select(GameEntityDto.Convert).ToList(),
				SeasonId = SeasonId,
			};
		}

		public static ServersideScheduleEntity Convert(ScheduleEntity model)
		{
			var dto = new ScheduleEntityDto(model);
			return dto.GetServersideScheduleEntity();
		}

		public static ScheduleEntity Convert(ServersideScheduleEntity model)
		{
			var dto = new ScheduleEntityDto(model);
			return dto.GetTesttargetScheduleEntity();
		}
	}
}