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

namespace Sportstats.Models
{
	/// <summary>
	/// League
	/// </summary>
	public class LeagueDto : ModelDto<League>
	{
		/// <summary>
		/// Id
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		public String Name { get; set; }
		/// <summary>
		/// Sport Id
		/// </summary>
		public int? Sportid { get; set; }
		/// <summary>
		/// Short name
		/// </summary>
		public String Shortname { get; set; }

		public Guid SportId { get; set; }

		public LeagueDto(League model)
		{
			LoadModelData(model);
		}

		public LeagueDto() { }

		public override League ToModel()
		{
			return new League
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Name = Name,
				Sportid = Sportid,
				Shortname = Shortname,
				SportId  = SportId,

			};
		}

		public override ModelDto<League> LoadModelData(League model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Name = model.Name;
			Sportid = model.Sportid;
			Shortname = model.Shortname;
			SportId  = model.SportId;

			return this;
		}
	}
}