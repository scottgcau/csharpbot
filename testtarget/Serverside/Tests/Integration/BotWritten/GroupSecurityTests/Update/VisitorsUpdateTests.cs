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
using System.Threading.Tasks;
using Sportstats.Models;
using ServersideTests.Helpers;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// to prevent warnings of using the model type in theory data.
#pragma warning disable xUnit1026
#pragma warning disable S2699

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Update
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class VisitorsUpdateTests : BaseUpdateTest
	{
		public VisitorsUpdateTests()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, string, string> VisitorsUpdateSecurityData =>
			new TheoryData<IAbstractModel, string,string>
			{
				// % protected region % [Configure theory data for Visitors here] off begin
				{new LadderEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new ScheduleEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new LaddereliminationEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new LadderwinlossEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new RoundEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new GameEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new DivisionEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new VenueEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new TeamEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new GamerefereeEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new SeasonEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new PersonEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new SportEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new LeagueEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new RosterEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new RosterassignmentEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				{new RosterTimelineEventsEntity(), SecurityStringHelper.UserPermissionDenied, "Visitors"},
				// % protected region % [Configure theory data for Visitors here] end

				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
			};

		[Theory]
		[MemberData(nameof(VisitorsUpdateSecurityData))]
		public async Task VisitorsUpdateSecurityTests<T>(T model, string message, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite update security test here] off begin
			await UpdateTest(model, message, groupName);
			// % protected region % [Overwrite update security test here] end
		}
	}
}