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
using APITests.EntityObjects.Models;
using Xunit.Abstractions;

namespace APITests.Factories
{
	public class EntityFactory : IXunitSerializable
	{
		private string _type;
		private readonly string _fixedStrValues;

		public EntityFactory(string type, string fixedStrValues = null)
		{
			_type = type;
			_fixedStrValues = fixedStrValues;
		}

		public EntityFactory()
		{

		}

		public BaseEntity Construct(bool isValid = true)
		{
			return _type switch
			{
				"LadderEntity" => LadderEntity.GetEntity(isValid, _fixedStrValues),
				"ScheduleEntity" => ScheduleEntity.GetEntity(isValid, _fixedStrValues),
				"LaddereliminationEntity" => LaddereliminationEntity.GetEntity(isValid, _fixedStrValues),
				"LadderwinlossEntity" => LadderwinlossEntity.GetEntity(isValid, _fixedStrValues),
				"RoundEntity" => RoundEntity.GetEntity(isValid, _fixedStrValues),
				"GameEntity" => GameEntity.GetEntity(isValid, _fixedStrValues),
				"DivisionEntity" => DivisionEntity.GetEntity(isValid, _fixedStrValues),
				"VenueEntity" => VenueEntity.GetEntity(isValid, _fixedStrValues),
				"TeamEntity" => TeamEntity.GetEntity(isValid, _fixedStrValues),
				"GamerefereeEntity" => GamerefereeEntity.GetEntity(isValid, _fixedStrValues),
				"SeasonEntity" => SeasonEntity.GetEntity(isValid, _fixedStrValues),
				"PersonEntity" => PersonEntity.GetEntity(isValid, _fixedStrValues),
				"SystemuserEntity" => SystemuserEntity.GetEntity(isValid, _fixedStrValues),
				"SportEntity" => SportEntity.GetEntity(isValid, _fixedStrValues),
				"LeagueEntity" => LeagueEntity.GetEntity(isValid, _fixedStrValues),
				"RosterEntity" => RosterEntity.GetEntity(isValid, _fixedStrValues),
				"RosterassignmentEntity" => RosterassignmentEntity.GetEntity(isValid, _fixedStrValues),
				"RosterTimelineEventsEntity" => RosterTimelineEventsEntity.GetEntity(isValid, _fixedStrValues),
				_ => throw new Exception($"Cannot find entity type {_type}"),
			};
		}

		public List<BaseEntity> Construct(int numEntities)
		{
			var entityList = new List<BaseEntity>(numEntities);
			for (var i = 0; i < numEntities; i++)
			{
				entityList.Add(Construct());
			}
			return entityList;
		}

		public List<BaseEntity> ConstructAndSave(ITestOutputHelper output, int numEntities)
		{
			var entityList = new List<BaseEntity>();
			var options = _fixedStrValues == null ? BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES : BaseEntity.ConfigureOptions.CREATE_REFERENCES_ONLY;

			for(var i = 0; i < numEntities; i++)
			{
				var entity = Construct();
				entity.Configure(options);
				entity.Save();
				output.WriteLine($"Database Saved Entity:\n{entity.EntityName}:\n{entity.ToJson()}\n");
				entityList.Add(entity);
			}
			return entityList;
		}

		public BaseEntity ConstructAndSave(ITestOutputHelper output) => ConstructAndSave(output,1)[0];

		public void Deserialize(IXunitSerializationInfo info) => _type = info.GetValue<string>("type");

		public void Serialize(IXunitSerializationInfo info) => info.AddValue("type", _type, typeof(string));

		public override string ToString() => $"Type = {_type}";

		public string GetFixedString() => _fixedStrValues;

		public string GetEnumValue(BaseEntity entity, string enumColumnName)
		{
			switch (_type)
			{
				case "LadderEntity":
					switch (enumColumnName)
					{
						case "LadderType":
							return ((LadderEntity)entity).Laddertype.ToString();
						default:
							return null;
					}
				case "ScheduleEntity":
					switch (enumColumnName)
					{
						case "ScheduleType":
							return ((ScheduleEntity)entity).Scheduletype.ToString();
						default:
							return null;
					}
				case "RosterassignmentEntity":
					switch (enumColumnName)
					{
						case "RoleType":
							return ((RosterassignmentEntity)entity).Roletype.ToString();
						default:
							return null;
					}
				default:
					return null;
			}
		}
	}
}