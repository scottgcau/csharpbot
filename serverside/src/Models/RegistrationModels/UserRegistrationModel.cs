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
using System.ComponentModel.DataAnnotations;
using Sportstats.Validators;

namespace Sportstats.Models.RegistrationModels
{
	public class UserRegistrationModel : UserDto, IRegistrationModel<User>
	{
		[Email]
		[Required]
		public new string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public IList<string> Groups => new List<string> {
			"User",
			// % protected region % [Add extra groups for User here] off begin
			// % protected region % [Add extra groups for User here] end
		};

		public override User ToModel()
		{
			var model = base.ToModel();
			model.Email = Email;
			return model;
		}
	}

	public class UserGraphQlRegistrationModel : UserRegistrationModel
	{
		public override User ToModel()
		{
			var model = base.ToModel();
			return model;
		}
	}
}