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
using ServersideLaddereliminationEntity = Sportstats.Models.LaddereliminationEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Elimination ladder entity
	/// </summary>
	public class LaddereliminationEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int? Pointsfor { get; set; }
		public int? Awatwon { get; set; }
		public int? Awaylost { get; set; }
		public int? Awayfor { get; set; }
		public int? Awayagainst { get; set; }
		public int? Homeagainst { get; set; }
		public int? Homefor { get; set; }
		public int? Homelost { get; set; }
		public int? Homewon { get; set; }
		public int? Pointsagainst { get; set; }
		public int? Played { get; set; }
		public int? Won { get; set; }
		public int? Lost { get; set; }

		public Guid? LadderId { get; set; }
		public Guid? RoundId { get; set; }
		public Guid? TeamId { get; set; }

		public LaddereliminationEntityDto(LaddereliminationEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Pointsfor = model.Pointsfor;
			Awatwon = model.Awatwon;
			Awaylost = model.Awaylost;
			Awayfor = model.Awayfor;
			Awayagainst = model.Awayagainst;
			Homeagainst = model.Homeagainst;
			Homefor = model.Homefor;
			Homelost = model.Homelost;
			Homewon = model.Homewon;
			Pointsagainst = model.Pointsagainst;
			Played = model.Played;
			Won = model.Won;
			Lost = model.Lost;
			LadderId = model.LadderId;
			RoundId = model.RoundId;
			TeamId = model.TeamId;
		}

		public LaddereliminationEntityDto(ServersideLaddereliminationEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Pointsfor = model.Pointsfor;
			Awatwon = model.Awatwon;
			Awaylost = model.Awaylost;
			Awayfor = model.Awayfor;
			Awayagainst = model.Awayagainst;
			Homeagainst = model.Homeagainst;
			Homefor = model.Homefor;
			Homelost = model.Homelost;
			Homewon = model.Homewon;
			Pointsagainst = model.Pointsagainst;
			Played = model.Played;
			Won = model.Won;
			Lost = model.Lost;
			LadderId = model.LadderId;
			RoundId = model.RoundId;
			TeamId = model.TeamId;
		}

		public LaddereliminationEntity GetTesttargetLaddereliminationEntity()
		{
			return new LaddereliminationEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Pointsfor = Pointsfor,
				Awatwon = Awatwon,
				Awaylost = Awaylost,
				Awayfor = Awayfor,
				Awayagainst = Awayagainst,
				Homeagainst = Homeagainst,
				Homefor = Homefor,
				Homelost = Homelost,
				Homewon = Homewon,
				Pointsagainst = Pointsagainst,
				Played = Played,
				Won = Won,
				Lost = Lost,
				LadderId = LadderId,
				RoundId = RoundId,
				TeamId = TeamId,
			};
		}

		public ServersideLaddereliminationEntity GetServersideLaddereliminationEntity()
		{
			return new ServersideLaddereliminationEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Pointsfor = Pointsfor,
				Awatwon = Awatwon,
				Awaylost = Awaylost,
				Awayfor = Awayfor,
				Awayagainst = Awayagainst,
				Homeagainst = Homeagainst,
				Homefor = Homefor,
				Homelost = Homelost,
				Homewon = Homewon,
				Pointsagainst = Pointsagainst,
				Played = Played,
				Won = Won,
				Lost = Lost,
				LadderId = LadderId,
				RoundId = RoundId,
				TeamId = TeamId,
			};
		}

		public static ServersideLaddereliminationEntity Convert(LaddereliminationEntity model)
		{
			var dto = new LaddereliminationEntityDto(model);
			return dto.GetServersideLaddereliminationEntity();
		}

		public static LaddereliminationEntity Convert(ServersideLaddereliminationEntity model)
		{
			var dto = new LaddereliminationEntityDto(model);
			return dto.GetTesttargetLaddereliminationEntity();
		}
	}
}