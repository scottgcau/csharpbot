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
using Sportstats.Models.RegistrationModels;
using ServersideTests.Helpers;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// to prevent warnings of using the model type in theory data.
#pragma warning disable xUnit1026
#pragma warning disable S2699

namespace ServersideTests.Tests.Integration.BotWritten.GroupSecurityTests.Create
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	[Trait("Category", "Security")]
	public class UnauthenticatedCreateTest : BaseCreateTest
	{

		public UnauthenticatedCreateTest()
		{
			// % protected region % [Add constructor logic here] off begin
			// % protected region % [Add constructor logic here] end
		}

		public static TheoryData<IAbstractModel, string, string> UnauthenticatedCreateSecurityData =>
			new TheoryData<IAbstractModel, string,string>
			{
				// % protected region % [Configure entity theory data for Unauthenticated here] off begin
				{new ScheduleEntity(), null, null},
				{new SeasonEntity(), null, null},
				{new VenueEntity(), null, null},
				{new GameEntity(), null, null},
				{new SportEntity(), null, null},
				{new LeagueEntity(), null, null},
				{new TeamEntity(), null, null},
				{new PersonEntity(), null, null},
				{new RosterEntity(), null, null},
				{new RosterassignmentEntity(), null, null},
				{new ScheduleSubmissionEntity(), null, null},
				{new SeasonSubmissionEntity(), null, null},
				{new VenueSubmissionEntity(), null, null},
				{new GameSubmissionEntity(), null, null},
				{new SportSubmissionEntity(), null, null},
				{new LeagueSubmissionEntity(), null, null},
				{new TeamSubmissionEntity(), null, null},
				{new PersonSubmissionEntity(), null, null},
				{new RosterSubmissionEntity(), null, null},
				{new RosterassignmentSubmissionEntity(), null, null},
				{new ScheduleEntityFormTileEntity(), null, null},
				{new SeasonEntityFormTileEntity(), null, null},
				{new VenueEntityFormTileEntity(), null, null},
				{new GameEntityFormTileEntity(), null, null},
				{new SportEntityFormTileEntity(), null, null},
				{new LeagueEntityFormTileEntity(), null, null},
				{new TeamEntityFormTileEntity(), null, null},
				{new PersonEntityFormTileEntity(), null, null},
				{new RosterEntityFormTileEntity(), null, null},
				{new RosterassignmentEntityFormTileEntity(), null, null},
				{new RosterTimelineEventsEntity(), null, null},
				// % protected region % [Configure entity theory data for Unauthenticated here] end

				// % protected region % [Add any extra theory data here] off begin
				// % protected region % [Add any extra theory data here] end
			};

		[Theory]
		[MemberData(nameof(UnauthenticatedCreateSecurityData))]
		public async Task UnauthenticatedCreateTests<T>(T model, string message, string groupName)
			where T : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Overwrite create security test here] off begin
			await CreateSecurityTest(model, message, groupName);
			// % protected region % [Overwrite create security test here] end
		}

		public static TheoryData<IAbstractModel, object, string, string> UnauthenticatedCreateUserSecurityData =>
			new TheoryData<IAbstractModel, object, string,string>
			{
				// % protected region % [Configure user theory data for Unauthenticated here] off begin
				// % protected region % [Configure user theory data for Unauthenticated here] end

				// % protected region % [Add any extra user theory data here] off begin
				// % protected region % [Add any extra user theory data here] end
			};

		[Theory]
		[MemberData(nameof(UnauthenticatedCreateUserSecurityData))]
		public async Task UnauthenticatedCreateUserTests<T, TDto>(T model, TDto dto, string message, string groupName)
			where T : User, IOwnerAbstractModel, new()
			where TDto : ModelDto<T>, IRegistrationModel<T>, new()
		{
			// % protected region % [Overwrite create user security test here] off begin
			await CreateUserTest(model, dto, message, groupName);
			// % protected region % [Overwrite create user security test here] end
		}
	}
}