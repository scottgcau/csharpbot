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

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Delete
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class VisitorsDeleteTest : BaseDeleteTest
	{
		public VisitorsDeleteTest()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, string, string> DeleteVisitorsSecurityData =>
			new TheoryData<IAbstractModel, string,string>
			{
				// % protected region % [Configure theory data for Unauthenticated here] off begin
				{new ScheduleEntity(), null, "Visitors"},
				{new SeasonEntity(), null, "Visitors"},
				{new VenueEntity(), null, "Visitors"},
				{new GameEntity(), null, "Visitors"},
				{new SportEntity(), null, "Visitors"},
				{new LeagueEntity(), null, "Visitors"},
				{new TeamEntity(), null, "Visitors"},
				{new PersonEntity(), null, "Visitors"},
				{new RosterEntity(), null, "Visitors"},
				{new RosterassignmentEntity(), null, "Visitors"},
				{new ScheduleSubmissionEntity(), null, "Visitors"},
				{new SeasonSubmissionEntity(), null, "Visitors"},
				{new VenueSubmissionEntity(), null, "Visitors"},
				{new GameSubmissionEntity(), null, "Visitors"},
				{new SportSubmissionEntity(), null, "Visitors"},
				{new LeagueSubmissionEntity(), null, "Visitors"},
				{new TeamSubmissionEntity(), null, "Visitors"},
				{new PersonSubmissionEntity(), null, "Visitors"},
				{new RosterSubmissionEntity(), null, "Visitors"},
				{new RosterassignmentSubmissionEntity(), null, "Visitors"},
				{new ScheduleEntityFormTileEntity(), null, "Visitors"},
				{new SeasonEntityFormTileEntity(), null, "Visitors"},
				{new VenueEntityFormTileEntity(), null, "Visitors"},
				{new GameEntityFormTileEntity(), null, "Visitors"},
				{new SportEntityFormTileEntity(), null, "Visitors"},
				{new LeagueEntityFormTileEntity(), null, "Visitors"},
				{new TeamEntityFormTileEntity(), null, "Visitors"},
				{new PersonEntityFormTileEntity(), null, "Visitors"},
				{new RosterEntityFormTileEntity(), null, "Visitors"},
				{new RosterassignmentEntityFormTileEntity(), null, "Visitors"},
				{new RosterTimelineEventsEntity(), null, "Visitors"},
				// % protected region % [Configure theory data for Unauthenticated here] end

				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
			};

		[Theory]
		[MemberData(nameof(DeleteVisitorsSecurityData))]
		public async Task VisitorsDeleteSecurity<T>(T model, string message, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite delete security test here] off begin
			await DeleteSecurityTest(model, message, groupName);
			// % protected region % [Overwrite delete security test here] end
		}
	}
}