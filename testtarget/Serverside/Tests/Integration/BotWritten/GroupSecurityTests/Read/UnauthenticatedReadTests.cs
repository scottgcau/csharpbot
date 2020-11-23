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

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Read
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class UnauthenticatedReadTests : BaseReadTest
	{

		public UnauthenticatedReadTests()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, bool, string> UnauthenticatedReadSecurityData =>
			new TheoryData<IAbstractModel, bool, string>
			{
				// % protected region % [Configure theory data for Unauthenticated here] off begin
				{new ScheduleEntity(), true, null},
				{new SeasonEntity(), true, null},
				{new VenueEntity(), true, null},
				{new GameEntity(), true, null},
				{new SportEntity(), true, null},
				{new LeagueEntity(), true, null},
				{new TeamEntity(), true, null},
				{new PersonEntity(), true, null},
				{new RosterEntity(), true, null},
				{new RosterassignmentEntity(), true, null},
				{new ScheduleSubmissionEntity(), true, null},
				{new SeasonSubmissionEntity(), true, null},
				{new VenueSubmissionEntity(), true, null},
				{new GameSubmissionEntity(), true, null},
				{new SportSubmissionEntity(), true, null},
				{new LeagueSubmissionEntity(), true, null},
				{new TeamSubmissionEntity(), true, null},
				{new PersonSubmissionEntity(), true, null},
				{new RosterSubmissionEntity(), true, null},
				{new RosterassignmentSubmissionEntity(), true, null},
				{new ScheduleEntityFormTileEntity(), true, null},
				{new SeasonEntityFormTileEntity(), true, null},
				{new VenueEntityFormTileEntity(), true, null},
				{new GameEntityFormTileEntity(), true, null},
				{new SportEntityFormTileEntity(), true, null},
				{new LeagueEntityFormTileEntity(), true, null},
				{new TeamEntityFormTileEntity(), true, null},
				{new PersonEntityFormTileEntity(), true, null},
				{new RosterEntityFormTileEntity(), true, null},
				{new RosterassignmentEntityFormTileEntity(), true, null},
				{new RosterTimelineEventsEntity(), true, null},
				// % protected region % [Configure theory data for Unauthenticated here] end

				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
			};

		[Theory]
		[MemberData(nameof(UnauthenticatedReadSecurityData))]
		public async Task UnauthenticatedReadSecurityTests<T>(T entity, bool canRead, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite delete security test here] off begin
			await ReadTest(entity, canRead, groupName);
			// % protected region % [Overwrite delete security test here] end
		}
	}
}