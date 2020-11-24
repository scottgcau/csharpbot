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
using System.Collections.Generic;
using Sportstats.Security.Acl;

namespace Sportstats.Security
{
	public static class SecurityUtilities
	{
		public static IEnumerable<IAcl> GetAllAcls()
		{
			return new List<IAcl>
			{
				new SuperAdministratorsScheme(),
				new VisitorsHome(),
				new VisitorsSportEntity(),
				new VisitorsVenueEntity(),
				new VisitorsTeamEntity(),
				new VisitorsSeasonEntity(),
				new VisitorsScheduleEntity(),
				new VisitorsRosterassignmentEntity(),
				new VisitorsRosterEntity(),
				new VisitorsPersonEntity(),
				new VisitorsLeagueEntity(),
				new VisitorsGameEntity(),
				new VisitorsSystemuserEntity(),
				new VisitorsSports(),
				new VisitorsRoundEntity(),
				new VisitorsLeagues(),
				new VisitorsLadderwinlossEntity(),
				new VisitorsLaddereliminationEntity(),
				new VisitorsLadderEntity(),
				new VisitorsGamerefereeEntity(),
				new VisitorsDivisionEntity(),
				new SystemuserVenueEntity(),
				new SystemuserTeamEntity(),
				new SystemuserSystemuserEntity(),
				new SystemuserSportEntity(),
				new SystemuserSeasonEntity(),
				new SystemuserScheduleEntity(),
				new SystemuserRoundEntity(),
				new SystemuserRosterassignmentEntity(),
				new SystemuserRosterEntity(),
				new SystemuserPersonEntity(),
				new SystemuserLeagueEntity(),
				new SystemuserLadderwinlossEntity(),
				new SystemuserLaddereliminationEntity(),
				new SystemuserLadderEntity(),
				new SystemuserGamerefereeEntity(),
				new SystemuserGameEntity(),
				new SystemuserDivisionEntity(),
				new SystemuserSports(),
				new SystemuserLeagues(),
				new SystemuserHome(),
			};
		}
	}
}