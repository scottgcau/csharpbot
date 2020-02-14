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
using ServersideSportentitySubmissionEntity = Sportstats.Models.SportentitySubmissionEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// The SportEntity submission form entity
	/// </summary>
	public class SportentitySubmissionEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }


		public SportentitySubmissionEntityDto(SportentitySubmissionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SportentitySubmissionEntityDto(ServersideSportentitySubmissionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SportentitySubmissionEntity GetTesttargetSportentitySubmissionEntity()
		{
			return new SportentitySubmissionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public ServersideSportentitySubmissionEntity GetServersideSportentitySubmissionEntity()
		{
			return new ServersideSportentitySubmissionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public static ServersideSportentitySubmissionEntity Convert(SportentitySubmissionEntity model)
		{
			var dto = new SportentitySubmissionEntityDto(model);
			return dto.GetServersideSportentitySubmissionEntity();
		}

		public static SportentitySubmissionEntity Convert(ServersideSportentitySubmissionEntity model)
		{
			var dto = new SportentitySubmissionEntityDto(model);
			return dto.GetTesttargetSportentitySubmissionEntity();
		}
	}
}