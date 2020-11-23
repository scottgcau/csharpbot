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
	public class GameEntity : BaseEntity
	{
		// Form Name
		public string Name { get; set; }
		// 
		public DateTime? Datestart { get; set; }
		// 
		public int? Hometeamid { get; set; }
		// 
		public int? Awayteamid { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Venue"/>
		public Guid? VenueId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Schedule"/>
		public Guid ScheduleId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Referees"/>
		public List<Guid> RefereesIds { get; set; }
		public ICollection<PersonEntity> Refereess { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.FormPage"/>
		public List<Guid> FormPageIds { get; set; }
		public ICollection<GameEntityFormTileEntity> FormPages { get; set; }


		public GameEntity()
		{
			EntityName = "GameEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public GameEntity(ConfigureOptions option)
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
				Name = "Name",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Datestart",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Hometeamid",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Awayteamid",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "VenueEntity",
				OppositeName = "Venue",
				Name = "Games",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "ScheduleEntity",
				OppositeName = "Schedule",
				Name = "Games",
				Optional = false,
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
				case "HomeTeamId":
					return GetInvalidHometeamid(validator);
				case "ScheduleId":
					return GetInvalidScheduleId(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidHometeamid(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Hometeamid");
			}
		}

		private static string GetInvalidVenueId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Games");
			}
		}
		private static string GetInvalidScheduleId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Games");
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
					"The Hometeamid field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining hometeamid,
						["datestart"] = Datestart?.ToString("s"),
						["awayteamid"] = Awayteamid.ToString(),
						["venueId"] = VenueId,
						["scheduleId"] = ScheduleId,
				}
			),
			(
				new List<string>
				{
					"violates foreign key constraint",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						["name"] = Name,
						// not defining ScheduleId,
						["datestart"] = Datestart?.ToString("s"),
						["hometeamid"] = Hometeamid.ToString(),
						["awayteamid"] = Awayteamid.ToString(),
						["venueId"] = VenueId,
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"name" , Name},
				{"datestart" ,((DateTime)Datestart).ToIsoString()},
				{"hometeamid" , Hometeamid.ToString()},
				{"awayteamid" , Awayteamid.ToString()},
			};

			if (VenueId != default)
			{
				entityVar["venueId"] = VenueId.ToString();
			}
			if (ScheduleId != default)
			{
				entityVar["scheduleId"] = ScheduleId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["name"] = Name,
				["datestart"] = Datestart?.ToString("s"),
				["hometeamid"] = Hometeamid,
				["awayteamid"] = Awayteamid,
			};

			if (ScheduleId != default)
			{
				entityVar["scheduleId"] = ScheduleId.ToString();
			}

			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "VenueId":
						ReferenceIdDictionary.Add("VenueId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "ScheduleId":
						ReferenceIdDictionary.Add("ScheduleId", guidCollection.FirstOrDefault());
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
				case "VenueId":
					VenueId = guid;
					break;
				case "ScheduleId":
					ScheduleId = guid;
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
			Name = Guid.NewGuid().ToString();
			// not defining Hometeamid
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static GameEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static GameEntity GetInvalidEntity()
		{
			var gameEntity = new GameEntity
			{
				// not defining Hometeamid
			};
			return gameEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{

			ScheduleId = new ScheduleEntity(ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES).Save();

		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Name = Guid.NewGuid().ToString();
			Datestart = DataUtils.RandDatetime();
			Hometeamid = DataUtils.RandInt();
			Awayteamid = DataUtils.RandInt();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static GameEntity GetValidEntity(string fixedStrValue = null)
		{
			var gameEntity = new GameEntity
			{
				Name = Guid.NewGuid().ToString(),

				Datestart = DataUtils.RandDatetime(),

				Hometeamid = DataUtils.RandInt(),

				Awayteamid = DataUtils.RandInt(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return gameEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.GameEntity>(GameEntityDto.Convert(this));
		}
	}
}
