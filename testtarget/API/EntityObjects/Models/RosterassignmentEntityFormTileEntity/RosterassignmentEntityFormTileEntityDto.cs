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
using ServersideRosterassignmentEntityFormTileEntity = Sportstats.Models.RosterassignmentEntityFormTileEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Stores the form entity to form tile mappings
	/// </summary>
	public class RosterassignmentEntityFormTileEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Tile { get; set; }


		public RosterassignmentEntityFormTileEntityDto(RosterassignmentEntityFormTileEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Tile = model.Tile;
		}

		public RosterassignmentEntityFormTileEntityDto(ServersideRosterassignmentEntityFormTileEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Tile = model.Tile;
		}

		public RosterassignmentEntityFormTileEntity GetTesttargetRosterassignmentEntityFormTileEntity()
		{
			return new RosterassignmentEntityFormTileEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Tile = Tile,
			};
		}

		public ServersideRosterassignmentEntityFormTileEntity GetServersideRosterassignmentEntityFormTileEntity()
		{
			return new ServersideRosterassignmentEntityFormTileEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Tile = Tile,
			};
		}

		public static ServersideRosterassignmentEntityFormTileEntity Convert(RosterassignmentEntityFormTileEntity model)
		{
			var dto = new RosterassignmentEntityFormTileEntityDto(model);
			return dto.GetServersideRosterassignmentEntityFormTileEntity();
		}

		public static RosterassignmentEntityFormTileEntity Convert(ServersideRosterassignmentEntityFormTileEntity model)
		{
			var dto = new RosterassignmentEntityFormTileEntityDto(model);
			return dto.GetTesttargetRosterassignmentEntityFormTileEntity();
		}
	}
}