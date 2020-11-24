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
using ServersideSystemuserEntity = Sportstats.Models.SystemuserEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// System users
	/// </summary>
	public class SystemuserEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }


		public SystemuserEntityDto(SystemuserEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SystemuserEntityDto(ServersideSystemuserEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SystemuserEntity GetTesttargetSystemuserEntity()
		{
			return new SystemuserEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public ServersideSystemuserEntity GetServersideSystemuserEntity()
		{
			return new ServersideSystemuserEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public static ServersideSystemuserEntity Convert(SystemuserEntity model)
		{
			var dto = new SystemuserEntityDto(model);
			return dto.GetServersideSystemuserEntity();
		}

		public static SystemuserEntity Convert(ServersideSystemuserEntity model)
		{
			var dto = new SystemuserEntityDto(model);
			return dto.GetTesttargetSystemuserEntity();
		}
	}
}