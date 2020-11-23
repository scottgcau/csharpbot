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
using ServersideSportSubmissionEntity = Sportstats.Models.SportSubmissionEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// The Sport submission form entity
	/// </summary>
	public class SportSubmissionEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }


		public SportSubmissionEntityDto(SportSubmissionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SportSubmissionEntityDto(ServersideSportSubmissionEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public SportSubmissionEntity GetTesttargetSportSubmissionEntity()
		{
			return new SportSubmissionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public ServersideSportSubmissionEntity GetServersideSportSubmissionEntity()
		{
			return new ServersideSportSubmissionEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public static ServersideSportSubmissionEntity Convert(SportSubmissionEntity model)
		{
			var dto = new SportSubmissionEntityDto(model);
			return dto.GetServersideSportSubmissionEntity();
		}

		public static SportSubmissionEntity Convert(ServersideSportSubmissionEntity model)
		{
			var dto = new SportSubmissionEntityDto(model);
			return dto.GetTesttargetSportSubmissionEntity();
		}
	}
}