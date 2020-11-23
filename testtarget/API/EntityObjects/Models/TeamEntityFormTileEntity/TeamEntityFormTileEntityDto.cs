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
using ServersideTeamEntityFormTileEntity = Sportstats.Models.TeamEntityFormTileEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Stores the form entity to form tile mappings
	/// </summary>
	public class TeamEntityFormTileEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Tile { get; set; }


		public TeamEntityFormTileEntityDto(TeamEntityFormTileEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Tile = model.Tile;
		}

		public TeamEntityFormTileEntityDto(ServersideTeamEntityFormTileEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Tile = model.Tile;
		}

		public TeamEntityFormTileEntity GetTesttargetTeamEntityFormTileEntity()
		{
			return new TeamEntityFormTileEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Tile = Tile,
			};
		}

		public ServersideTeamEntityFormTileEntity GetServersideTeamEntityFormTileEntity()
		{
			return new ServersideTeamEntityFormTileEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Tile = Tile,
			};
		}

		public static ServersideTeamEntityFormTileEntity Convert(TeamEntityFormTileEntity model)
		{
			var dto = new TeamEntityFormTileEntityDto(model);
			return dto.GetServersideTeamEntityFormTileEntity();
		}

		public static TeamEntityFormTileEntity Convert(ServersideTeamEntityFormTileEntity model)
		{
			var dto = new TeamEntityFormTileEntityDto(model);
			return dto.GetTesttargetTeamEntityFormTileEntity();
		}
	}
}