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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Sportstats.Controllers;
using Sportstats.Models;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class DeactivatedUserTests
	{
		// % protected region % [Customize CreateAndValidateUser Test for SystemuserEntity] off begin
		[Fact]
		public async void SystemuserEntityDeactivatedLoginTest()
		{
			await CreateAndValidateUser<SystemuserEntity>();
		}
		// % protected region % [Customize CreateAndValidateUser Test for SystemuserEntity] end


		// % protected region % [Customize CreateAndValidateUser method here] off begin
		private static async Task CreateAndValidateUser<T>()
			where T : User, new()
		{
			using var host = ServerBuilder.CreateServer();

			var controller = host.Services.GetRequiredService<AuthorizationController>();
			var userManager = host.Services.GetRequiredService<UserManager<User>>();

			// Create a user with the user manager
			var entity = new EntityFactory<T>()
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.First();

			var id = Guid.NewGuid().ToString();
			entity.UserName = id;
			entity.Email = $"{id}@example.com";
			entity.NormalizedUserName = entity.UserName.ToUpper();
			entity.NormalizedEmail = entity.Email.ToUpper();
			entity.EmailConfirmed = false;
			await userManager.CreateAsync(entity, "password");

			var result = await controller.Login(new LoginDetails
			{
				Username = entity.UserName,
				Password = "password"
			});

			Assert.Equal(typeof(UnauthorizedObjectResult), result.GetType());
		}
		// % protected region % [Customize CreateAndValidateUser method here] end
	}
}