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
using ServersideLadderEntity = Sportstats.Models.LadderEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Ladder entity
	/// </summary>
	public class LadderEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Laddertype Laddertype { get; set; }

		public ICollection<LadderwinlossEntity> Ladderwinlossess { get; set; }
		public ICollection<LaddereliminationEntity> Laddereliminationss { get; set; }

		public LadderEntityDto(LadderEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Laddertype = (Laddertype)model.Laddertype;
			Ladderwinlossess = model.Ladderwinlossess;
			Laddereliminationss = model.Laddereliminationss;
		}

		public LadderEntityDto(ServersideLadderEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Laddertype = model.Laddertype;
			Ladderwinlossess = model.Ladderwinlossess.Select(LadderwinlossEntityDto.Convert).ToList();
			Laddereliminationss = model.Laddereliminationss.Select(LaddereliminationEntityDto.Convert).ToList();
		}

		public LadderEntity GetTesttargetLadderEntity()
		{
			return new LadderEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Laddertype = (TestEnums.Laddertype)Laddertype,
				Ladderwinlossess = Ladderwinlossess,
				Laddereliminationss = Laddereliminationss,
			};
		}

		public ServersideLadderEntity GetServersideLadderEntity()
		{
			return new ServersideLadderEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Laddertype = Laddertype,
				Ladderwinlossess = Ladderwinlossess?.Select(LadderwinlossEntityDto.Convert).ToList(),
				Laddereliminationss = Laddereliminationss?.Select(LaddereliminationEntityDto.Convert).ToList(),
			};
		}

		public static ServersideLadderEntity Convert(LadderEntity model)
		{
			var dto = new LadderEntityDto(model);
			return dto.GetServersideLadderEntity();
		}

		public static LadderEntity Convert(ServersideLadderEntity model)
		{
			var dto = new LadderEntityDto(model);
			return dto.GetTesttargetLadderEntity();
		}
	}
}