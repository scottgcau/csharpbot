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
	public class LaddereliminationEntity : BaseEntity
	{
		// 
		public int? Pointsfor { get; set; }
		// 
		public int? Awatwon { get; set; }
		// 
		public int? Awaylost { get; set; }
		// 
		public int? Awayfor { get; set; }
		// 
		public int? Awayagainst { get; set; }
		// 
		public int? Homeagainst { get; set; }
		// 
		public int? Homefor { get; set; }
		// 
		public int? Homelost { get; set; }
		// 
		public int? Homewon { get; set; }
		// 
		public int? Pointsagainst { get; set; }
		// 
		public int? Played { get; set; }
		// 
		public int? Won { get; set; }
		// 
		public int? Lost { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Ladder"/>
		public Guid? LadderId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Round"/>
		public Guid? RoundId { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Sportstats.Models.Team"/>
		public Guid? TeamId { get; set; }


		public LaddereliminationEntity()
		{
			EntityName = "LaddereliminationEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public LaddereliminationEntity(ConfigureOptions option)
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
				Name = "Pointsfor",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Awatwon",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Awaylost",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Awayfor",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Awayagainst",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Homeagainst",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Homefor",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Homelost",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Homewon",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Pointsagainst",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Played",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Won",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Lost",
				IsRequired = true
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "LadderEntity",
				OppositeName = "Ladder",
				Name = "Laddereliminations",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "RoundEntity",
				OppositeName = "Round",
				Name = "Laddereliminations",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
			References.Add(new Reference
			{
				EntityName = "TeamEntity",
				OppositeName = "Team",
				Name = "Laddereliminations",
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
				case "PointsFor":
					return GetInvalidPointsfor(validator);
				case "HomeAgainst":
					return GetInvalidHomeagainst(validator);
				case "HomeFor":
					return GetInvalidHomefor(validator);
				case "HomeLost":
					return GetInvalidHomelost(validator);
				case "HomeWon":
					return GetInvalidHomewon(validator);
				case "PointsAgainst":
					return GetInvalidPointsagainst(validator);
				case "Played":
					return GetInvalidPlayed(validator);
				case "Won":
					return GetInvalidWon(validator);
				case "Lost":
					return GetInvalidLost(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidPointsfor(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Pointsfor");
			}
		}
		private static string GetInvalidHomeagainst(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Homeagainst");
			}
		}
		private static string GetInvalidHomefor(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Homefor");
			}
		}
		private static string GetInvalidHomelost(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Homelost");
			}
		}
		private static string GetInvalidHomewon(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Homewon");
			}
		}
		private static string GetInvalidPointsagainst(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Pointsagainst");
			}
		}
		private static string GetInvalidPlayed(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Played");
			}
		}
		private static string GetInvalidWon(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Won");
			}
		}
		private static string GetInvalidLost(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Lost");
			}
		}

		private static string GetInvalidLadderId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute LadderEliminations");
			}
		}
		private static string GetInvalidRoundId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute LadderEliminations");
			}
		}
		private static string GetInvalidTeamId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute LadderEliminations");
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
					"The Pointsfor field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining pointsfor,
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Homeagainst field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining homeagainst,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Homefor field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining homefor,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Homelost field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining homelost,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Homewon field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining homewon,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Pointsagainst field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining pointsagainst,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Played field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining played,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["won"] = Won.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Won field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining won,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["lost"] = Lost.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),
			(
				new List<string>
				{
					"The Lost field is required.",
				},

				new RestSharp.JsonObject
				{
						["id"] = Id,
						// not defining lost,
						["pointsfor"] = Pointsfor.ToString(),
						["awatwon"] = Awatwon.ToString(),
						["awaylost"] = Awaylost.ToString(),
						["awayfor"] = Awayfor.ToString(),
						["awayagainst"] = Awayagainst.ToString(),
						["homeagainst"] = Homeagainst.ToString(),
						["homefor"] = Homefor.ToString(),
						["homelost"] = Homelost.ToString(),
						["homewon"] = Homewon.ToString(),
						["pointsagainst"] = Pointsagainst.ToString(),
						["played"] = Played.ToString(),
						["won"] = Won.ToString(),
						["ladderId"] = LadderId,
						["roundId"] = RoundId,
						["teamId"] = TeamId,
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"pointsfor" , Pointsfor.ToString()},
				{"awatwon" , Awatwon.ToString()},
				{"awaylost" , Awaylost.ToString()},
				{"awayfor" , Awayfor.ToString()},
				{"awayagainst" , Awayagainst.ToString()},
				{"homeagainst" , Homeagainst.ToString()},
				{"homefor" , Homefor.ToString()},
				{"homelost" , Homelost.ToString()},
				{"homewon" , Homewon.ToString()},
				{"pointsagainst" , Pointsagainst.ToString()},
				{"played" , Played.ToString()},
				{"won" , Won.ToString()},
				{"lost" , Lost.ToString()},
			};

			if (LadderId != default)
			{
				entityVar["ladderId"] = LadderId.ToString();
			}
			if (RoundId != default)
			{
				entityVar["roundId"] = RoundId.ToString();
			}
			if (TeamId != default)
			{
				entityVar["teamId"] = TeamId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["pointsfor"] = Pointsfor,
				["awatwon"] = Awatwon,
				["awaylost"] = Awaylost,
				["awayfor"] = Awayfor,
				["awayagainst"] = Awayagainst,
				["homeagainst"] = Homeagainst,
				["homefor"] = Homefor,
				["homelost"] = Homelost,
				["homewon"] = Homewon,
				["pointsagainst"] = Pointsagainst,
				["played"] = Played,
				["won"] = Won,
				["lost"] = Lost,
			};


			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "LadderId":
						ReferenceIdDictionary.Add("LadderId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "RoundId":
						ReferenceIdDictionary.Add("RoundId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					case "TeamId":
						ReferenceIdDictionary.Add("TeamId", guidCollection.FirstOrDefault());
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
				case "LadderId":
					LadderId = guid;
					break;
				case "RoundId":
					RoundId = guid;
					break;
				case "TeamId":
					TeamId = guid;
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
			// not defining Pointsfor
			// not defining Homeagainst
			// not defining Homefor
			// not defining Homelost
			// not defining Homewon
			// not defining Pointsagainst
			// not defining Played
			// not defining Won
			// not defining Lost
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static LaddereliminationEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static LaddereliminationEntity GetInvalidEntity()
		{
			var laddereliminationEntity = new LaddereliminationEntity
			{
				// not defining Pointsfor
				// not defining Homeagainst
				// not defining Homefor
				// not defining Homelost
				// not defining Homewon
				// not defining Pointsagainst
				// not defining Played
				// not defining Won
				// not defining Lost
			};
			return laddereliminationEntity;
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
			Pointsfor = DataUtils.RandInt();
			Awatwon = DataUtils.RandInt();
			Awaylost = DataUtils.RandInt();
			Awayfor = DataUtils.RandInt();
			Awayagainst = DataUtils.RandInt();
			Homeagainst = DataUtils.RandInt();
			Homefor = DataUtils.RandInt();
			Homelost = DataUtils.RandInt();
			Homewon = DataUtils.RandInt();
			Pointsagainst = DataUtils.RandInt();
			Played = DataUtils.RandInt();
			Won = DataUtils.RandInt();
			Lost = DataUtils.RandInt();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static LaddereliminationEntity GetValidEntity(string fixedStrValue = null)
		{
			var laddereliminationEntity = new LaddereliminationEntity
			{

				Pointsfor = DataUtils.RandInt(),

				Awatwon = DataUtils.RandInt(),

				Awaylost = DataUtils.RandInt(),

				Awayfor = DataUtils.RandInt(),

				Awayagainst = DataUtils.RandInt(),

				Homeagainst = DataUtils.RandInt(),

				Homefor = DataUtils.RandInt(),

				Homelost = DataUtils.RandInt(),

				Homewon = DataUtils.RandInt(),

				Pointsagainst = DataUtils.RandInt(),

				Played = DataUtils.RandInt(),

				Won = DataUtils.RandInt(),

				Lost = DataUtils.RandInt(),
			};

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return laddereliminationEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Sportstats.Models.LaddereliminationEntity>(LaddereliminationEntityDto.Convert(this));
		}
	}
}
