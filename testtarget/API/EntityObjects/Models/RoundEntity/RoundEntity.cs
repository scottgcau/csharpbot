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
	public class RoundEntity : BaseEntity
	{
		// Numeric round order
		public int? Order { get; set; }
		// Full name
		public String Fullname { get; set; }
		// Short name / abbreviation
		public String Shortname { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Games"/>
		public List<Guid> GamesIds { get; set; }
		public ICollection<GameEntity> Gamess { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Schedule"/>
		public Guid? ScheduleId { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Laddereliminations"/>
		public List<Guid> LaddereliminationsIds { get; set; }
		public ICollection<LaddereliminationEntity> Laddereliminationss { get; set; }


		public RoundEntity()
		{
			EntityName = "RoundEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public RoundEntity(ConfigureOptions option)
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
				Name = "Order",
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
				EntityName = "ScheduleEntity",
				OppositeName = "Schedule",
				Name = "Rounds",
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
				case "Order":
					return GetInvalidOrder(validator);
				case "FullName":
					return GetInvalidFullname(validator);
				case "ShortName":
					return GetInvalidShortname(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidOrder(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Order");
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

		private static string GetInvalidScheduleId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Rounds");
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
					"The Order field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining order,
						["fullname"] = Fullname,
						["shortname"] = Shortname,
						["scheduleId"] = ScheduleId,
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
						["order"] = Order.ToString(),
						["shortname"] = Shortname,
						["scheduleId"] = ScheduleId,
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
						["order"] = Order.ToString(),
						["fullname"] = Fullname,
						["scheduleId"] = ScheduleId,
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"order" , Order.ToString()},
				{"fullname" , Fullname},
				{"shortname" , Shortname},
			};

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
				["order"] = Order,
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
			// not defining Order
			// not defining Fullname
			// not defining Shortname
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static RoundEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static RoundEntity GetInvalidEntity()
		{
			var roundEntity = new RoundEntity
			{
				// not defining Order
				// not defining Fullname
				// not defining Shortname
			};
			return roundEntity;
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
			Order = DataUtils.RandInt();
			Fullname = DataUtils.RandString();
			Shortname = DataUtils.RandString();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static RoundEntity GetValidEntity(string fixedStrValue = null)
		{
			var roundEntity = new RoundEntity
			{

				Order = DataUtils.RandInt(),

				Fullname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Shortname = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return roundEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.RoundEntity>(RoundEntityDto.Convert(this));
		}
	}
}
