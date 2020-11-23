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
using APITests.EntityObjects.Models;
using APITests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Factories
{
	internal class EntityDetailFactory
	{
		private readonly ContextConfiguration _contextConfiguration;

		public EntityDetailFactory(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		public BaseEntity ApplyDetails(string entityName, bool isValid)
		{
			var entityFactory = new EntityFactory(entityName);
			var entity = entityFactory.Construct(isValid);
			entity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			CreateDetailSection(entityName, entity).Apply();
			return entity;
		}

		public IEntityDetailSection CreateDetailSection(string entityName, BaseEntity entity = null)
		{
			return entityName switch
			{
				"ScheduleEntity" => new ScheduleEntityDetailSection(_contextConfiguration, (ScheduleEntity) entity),
				"SeasonEntity" => new SeasonEntityDetailSection(_contextConfiguration, (SeasonEntity) entity),
				"VenueEntity" => new VenueEntityDetailSection(_contextConfiguration, (VenueEntity) entity),
				"GameEntity" => new GameEntityDetailSection(_contextConfiguration, (GameEntity) entity),
				"SportEntity" => new SportEntityDetailSection(_contextConfiguration, (SportEntity) entity),
				"LeagueEntity" => new LeagueEntityDetailSection(_contextConfiguration, (LeagueEntity) entity),
				"TeamEntity" => new TeamEntityDetailSection(_contextConfiguration, (TeamEntity) entity),
				"PersonEntity" => new PersonEntityDetailSection(_contextConfiguration, (PersonEntity) entity),
				"RosterEntity" => new RosterEntityDetailSection(_contextConfiguration, (RosterEntity) entity),
				"RosterassignmentEntity" => new RosterassignmentEntityDetailSection(_contextConfiguration, (RosterassignmentEntity) entity),
				"ScheduleSubmissionEntity" => new ScheduleSubmissionEntityDetailSection(_contextConfiguration, (ScheduleSubmissionEntity) entity),
				"SeasonSubmissionEntity" => new SeasonSubmissionEntityDetailSection(_contextConfiguration, (SeasonSubmissionEntity) entity),
				"VenueSubmissionEntity" => new VenueSubmissionEntityDetailSection(_contextConfiguration, (VenueSubmissionEntity) entity),
				"GameSubmissionEntity" => new GameSubmissionEntityDetailSection(_contextConfiguration, (GameSubmissionEntity) entity),
				"SportSubmissionEntity" => new SportSubmissionEntityDetailSection(_contextConfiguration, (SportSubmissionEntity) entity),
				"LeagueSubmissionEntity" => new LeagueSubmissionEntityDetailSection(_contextConfiguration, (LeagueSubmissionEntity) entity),
				"TeamSubmissionEntity" => new TeamSubmissionEntityDetailSection(_contextConfiguration, (TeamSubmissionEntity) entity),
				"PersonSubmissionEntity" => new PersonSubmissionEntityDetailSection(_contextConfiguration, (PersonSubmissionEntity) entity),
				"RosterSubmissionEntity" => new RosterSubmissionEntityDetailSection(_contextConfiguration, (RosterSubmissionEntity) entity),
				"RosterassignmentSubmissionEntity" => new RosterassignmentSubmissionEntityDetailSection(_contextConfiguration, (RosterassignmentSubmissionEntity) entity),
				"SportEntityFormTileEntity" => new SportEntityFormTileEntityDetailSection(_contextConfiguration, (SportEntityFormTileEntity) entity),
				"RosterTimelineEventsEntity" => new RosterTimelineEventsEntityDetailSection(_contextConfiguration, (RosterTimelineEventsEntity) entity),
				_ => throw new Exception($"Cannot find entity type {entityName}"),
			};
		}
	}
}
