/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
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
using ServersideUser = Sportstats.Models.User;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// User account
	/// </summary>
	public class UserDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int? Id { get; set; }
		public String Username { get; set; }

		public UserDto(User model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Username = model.Username;
		}

		public UserDto(ServersideUser model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Username = model.Username;
		}

		public User GetTesttargetUser()
		{
			return new User
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Username = Username,
			};
		}

		public ServersideUser GetServersideUser()
		{
			return new ServersideUser
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Username = Username,
			};
		}

		public static ServersideUser Convert(User model)
		{
			var dto = new UserDto(model);
			return dto.GetServersideUser();
		}

		public static User Convert(ServersideUser model)
		{
			var dto = new UserDto(model);
			return dto.GetTesttargetUser();
		}
	}
}