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
using SeleniumTests.PageObjects.BotWritten;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Utils
{
	internal static class EntityDetailUtils
	{
		public static IDetailSection GetEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				case "LadderEntity":
					return new LadderEntityDetailSection(contextConfiguration);
				case "ScheduleEntity":
					return new ScheduleEntityDetailSection(contextConfiguration);
				case "LaddereliminationEntity":
					return new LaddereliminationEntityDetailSection(contextConfiguration);
				case "LadderwinlossEntity":
					return new LadderwinlossEntityDetailSection(contextConfiguration);
				case "RoundEntity":
					return new RoundEntityDetailSection(contextConfiguration);
				case "GameEntity":
					return new GameEntityDetailSection(contextConfiguration);
				case "DivisionEntity":
					return new DivisionEntityDetailSection(contextConfiguration);
				case "VenueEntity":
					return new VenueEntityDetailSection(contextConfiguration);
				case "TeamEntity":
					return new TeamEntityDetailSection(contextConfiguration);
				case "GamerefereeEntity":
					return new GamerefereeEntityDetailSection(contextConfiguration);
				case "SeasonEntity":
					return new SeasonEntityDetailSection(contextConfiguration);
				case "PersonEntity":
					return new PersonEntityDetailSection(contextConfiguration);
				case "SystemuserEntity":
					return new SystemuserEntityDetailSection(contextConfiguration);
				case "SportEntity":
					return new SportEntityDetailSection(contextConfiguration);
				case "LeagueEntity":
					return new LeagueEntityDetailSection(contextConfiguration);
				case "RosterEntity":
					return new RosterEntityDetailSection(contextConfiguration);
				case "RosterassignmentEntity":
					return new RosterassignmentEntityDetailSection(contextConfiguration);
				case "RosterTimelineEventsEntity":
					return new RosterTimelineEventsEntityDetailSection(contextConfiguration);
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}

		public static WorkflowPage GetWorkflowEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}
	}
}