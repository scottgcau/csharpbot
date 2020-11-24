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
using System.Text;
using EntityObject.Enums;
using APITests.Classes;
using RestSharp;
using TestDataLib;
using Sportstats.Utility;

namespace APITests.EntityObjects.Models
{
	public class SeasonEntity : BaseEntity
	{
		// 
		public DateTime? Startdate { get; set; }
		// 
		public DateTime? Enddate { get; set; }
		// Name for the season
		public String Fullname { get; set; }
		// Short name / abbreviation
		public String Shortname { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Divisions"/>
		public List<Guid> DivisionsIds { get; set; }
		public ICollection<DivisionEntity> Divisionss { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.League"/>
		public Guid? LeagueId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Rosters"/>
		public List<Guid> RostersIds { get; set; }
		public ICollection<RosterEntity> Rosterss { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Schedules"/>
		public List<Guid> SchedulesIds { get; set; }
		public ICollection<ScheduleEntity> Scheduless { get; set; }


		public SeasonEntity()
		{
			EntityName = "SeasonEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public SeasonEntity(ConfigureOptions option)
		{
			Configure(option);
			InitialiseAttributes();
			InitialiseReferences();
		}

		public override void Configure(ConfigureOptions option)
		{
			switch (option)
			{
				case ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES:
					SetValidEntityAttributes();
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_ATTRIBUTES_ONLY:
					SetValidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_REFERENCES_ONLY:
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES:
					SetInvalidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES:
					SetInvalidEntityAttributes();
					SetValidEntityAssociations();
					break;
			}
		}

		private void InitialiseAttributes()
		{
			Attributes.Add(new Attribute
			{
				Name = "Startdate",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Enddate",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Fullname",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Shortname",
				IsRequired = true
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "LeagueEntity",
				OppositeName = "League",
				Name = "Seasons",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
		}

		public override (int min, int max) GetLengthValidatorMinMax(string attribute)
		{
			switch(attribute)
			{
				default:
					throw new Exception($"{attribute} does not exist or does not have a length validator");
			}
		}

		public override string GetInvalidAttribute(string attribute, string validator)
		{
			switch (attribute)
			{
				case "StartDate":
					return GetInvalidStartdate(validator);
				case "EndDate":
					return GetInvalidEnddate(validator);
				case "FullName":
					return GetInvalidFullname(validator);
				case "ShortName":
					return GetInvalidShortname(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidStartdate(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Startdate");
			}
		}
		private static string GetInvalidEnddate(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Enddate");
			}
		}
		private static string GetInvalidFullname(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Fullname");
			}
		}
		private static string GetInvalidShortname(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Shortname");
			}
		}

		private static string GetInvalidLeagueId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Seasons");
			}
		}

		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, RestSharp.JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return new List<(List<string> expectedError, RestSharp.JsonObject jsonObject)>
			{

			(
				new List<string>
				{
					"The Startdate field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining startdate,
						["enddate"] = Enddate?.ToString("s"),
						["fullname"] = Fullname,
						["shortname"] = Shortname,
						["leagueId"] = LeagueId,
				}
			),
			(
				new List<string>
				{
					"The Enddate field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining enddate,
						["startdate"] = Startdate?.ToString("s"),
						["fullname"] = Fullname,
						["shortname"] = Shortname,
						["leagueId"] = LeagueId,
				}
			),
			(
				new List<string>
				{
					"The Fullname field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining fullname,
						["startdate"] = Startdate?.ToString("s"),
						["enddate"] = Enddate?.ToString("s"),
						["shortname"] = Shortname,
						["leagueId"] = LeagueId,
				}
			),
			(
				new List<string>
				{
					"The Shortname field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining shortname,
						["startdate"] = Startdate?.ToString("s"),
						["enddate"] = Enddate?.ToString("s"),
						["fullname"] = Fullname,
						["leagueId"] = LeagueId,
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"startdate" ,((DateTime)Startdate).ToIsoString()},
				{"enddate" ,((DateTime)Enddate).ToIsoString()},
				{"fullname" , Fullname},
				{"shortname" , Shortname},
			};

			if (LeagueId != default)
			{
				entityVar["leagueId"] = LeagueId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["startdate"] = Startdate?.ToString("s"),
				["enddate"] = Enddate?.ToString("s"),
				["fullname"] = Fullname.ToString(),
				["shortname"] = Shortname.ToString(),
			};


			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "LeagueId":
						ReferenceIdDictionary.Add("LeagueId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
				case "LeagueId":
					LeagueId = guid;
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
				default:
					throw new Exception($"{reference} not valid many to many reference key");
			}
		}

		private List<RestSharp.JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<RestSharp.JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new RestSharp.JsonObject {[key] = x }));
			return manyToManyList;
		}

		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
			// not defining Startdate
			// not defining Enddate
			// not defining Fullname
			// not defining Shortname
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static SeasonEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static SeasonEntity GetInvalidEntity()
		{
			var seasonEntity = new SeasonEntity
			{
				// not defining Startdate
				// not defining Enddate
				// not defining Fullname
				// not defining Shortname
			};
			return seasonEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Startdate = DataUtils.RandDatetime();
			Enddate = DataUtils.RandDatetime();
			Fullname = DataUtils.RandString();
			Shortname = DataUtils.RandString();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static SeasonEntity GetValidEntity(string fixedStrValue = null)
		{
			var seasonEntity = new SeasonEntity
			{

				Startdate = DataUtils.RandDatetime(),

				Enddate = DataUtils.RandDatetime(),

				Fullname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Shortname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return seasonEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.SeasonEntity>(SeasonEntityDto.Convert(this));
		}
	}
}
