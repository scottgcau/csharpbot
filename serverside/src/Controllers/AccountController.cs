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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sportstats.Exceptions;
using Sportstats.Models;
using Sportstats.Helpers;
using Sportstats.Services;
using Sportstats.Services.Interfaces;
using Sportstats.Utility;
using Sportstats.Graphql.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Controllers
{
	/// <summary>
	/// Controller for managing users
	/// </summary>
	[Route("/api/account")]
	[Authorize]
	[ApiController]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;
		private readonly RoleManager<Group> _roleManager;
		private readonly IIdentityService _identityService;
		private readonly ILogger<AccountController> _logger;

		public class UsernameModel
		{
			/// <summary>
			/// The username to reset the password of
			/// </summary>
			[Required]
			public string Username { get; set; }
		}

		public class ResetPasswordModel
		{
			/// <summary>
			/// The username to reset the password for
			/// </summary>
			[Required]
			public string Username { get; set; }

			/// <summary>
			/// The password to reset to
			/// </summary>
			[Required]
			public string Password { get; set; }

			/// <summary>
			/// The password reset token
			/// </summary>
			[Required]
			public string Token { get; set; }
		}

		public class AllUserRequestModel
		{
			/// <summary>
			/// The conditions for sorting user entities
			/// </summary>
			public List<OrderBy> SortConditions { get; set; }
			/// <summary>
			/// The pagination options
			/// </summary>
			public PaginationOptions PaginationOptions { get; set; }
			/// <summary>
			/// The search conditions
			/// </summary>
			public IEnumerable<IEnumerable<WhereExpression>> SearchConditions { get; set; }
		}

		public class UserListModel
		{
			/// <summary>
			/// The total number of users
			/// </summary>
			[Required]
			public int countUsers { get; set; }
			/// <summary>
			/// A paginated list of users
			/// </summary>
			[Required]
			public IEnumerable<UserDto> Users { get; set; }
		}

		public AccountController(
			UserManager<User> userManager,
			IUserService userService,
			RoleManager<Group> roleManager,
			IIdentityService identityService,
			ILogger<AccountController> logger)
		{
			_userManager = userManager;
			_userService = userService;
			_roleManager = roleManager;
			_identityService = identityService;
			_logger = logger;
		}

		/// <summary>
		/// Gets the logged in user
		/// </summary>
		/// <returns>The current logged in user</returns>
		[HttpGet]
		[HttpPost]
		[Produces("application/json")]
		[Route("me")]
		[Authorize]
		public async Task<UserResult> Get()
		{
			var user = await _userService.GetUser(User);
			return user;
		}

		/// <summary>
		/// Registers a new user
		/// </summary>
		/// <param name="model">JSON object with a key for email and password</param>
		/// <returns>200 OK on success</returns>
		[HttpPost]
		[AllowAnonymous]
		[Authorize]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			// In the case of an invalid model return an error
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.GetNormalisedErrors());
			}

			try
			{
				// Attempt to make the user
				var registerResult = await _userService.RegisterUser(model, null);
				var result = registerResult.Result;

				if (result.Succeeded && model.Password != null)
				{
					// User was returned successfully, return 200 OK
					return Ok(result.Succeeded);
				}

				// We have not succeeded in creating the user
				// Collect any errors that were created and send them back to the client
				var errors = result.Errors.ToList();
				AddErrors(errors);
				return BadRequest(ModelState.GetNormalisedErrors());
			}
			catch (DuplicateUserException e)
			{
				// In the case of a duplicate user return a 409 Conflict response code
				return StatusCode(StatusCodes.Status409Conflict, new ApiErrorResponse(e.Message));
			}
		}

		/// <summary>
		/// Gets all the user groups in the system
		/// </summary>
		/// <returns>A list of user groups</returns>
		/// <response code="200">On a successful response</response>
		/// <response code="401">On failing to authenticate</response>
		[HttpGet]
		[Authorize]
		[Route("groups")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IEnumerable<string>> GetRoles()
		{
			return await _roleManager.Roles.Select(group => group.Name).ToListAsync();
		}

		/// <summary>
		/// Get a paginated list of all the users in the system
		/// </summary>
		/// <returns> A list of user list model </returns>
		/// <response code="200"> On a successful response </response>
		/// <response code="401"> On failing to authenticate </response>
		[HttpPost]
		[Authorize]
		[Route("users")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<UserListModel> GetUsers([FromBody] AllUserRequestModel options)
		{
			_identityService.RetrieveUserAsync().Wait();

			var userQuery = _userManager.Users
				.Where(UsersFilter.AllUsersFilter(_identityService.User, _identityService.Groups, DATABASE_OPERATION.READ))
				.AddConditionalWhereFilter(options.SearchConditions)
				.AddOrderBys(options.SortConditions);
			
			return new UserListModel
			{
				countUsers = await userQuery.CountAsync(),
				Users = userQuery
					.AddPagination(new Pagination(options.PaginationOptions))
					.ToList()
					.Select(u => new UserDto(u))
			};
		}

		/// <summary>
		/// Deactivates a user
		/// </summary>
		/// <returns> 200OK on success </returns>
		/// <response code="200"> On a successful response </response>
		/// <response code="401"> On failing to authenticate </response>
		[HttpPost]
		[Authorize]
		[Route("deactivate")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> DeactivateUser([FromBody] UsernameModel deactivateUser)
		{
			_identityService.RetrieveUserAsync().Wait();
			
			var user = _userManager.Users
				.Where(UsersFilter.AllUsersFilter(_identityService.User, _identityService.Groups, DATABASE_OPERATION.UPDATE))
				.FirstOrDefault(u => u.UserName == deactivateUser.Username);
			
			if (user == null)
			{
				return BadRequest("The user does not exist or you do not have permission to deactivate the user");
			}

			user.EmailConfirmed = false;
			await _userManager.UpdateAsync(user);
			return Ok();
		}

		/// <summary>
		/// Activates a user
		/// </summary>
		/// <returns> 200OK on success </returns>
		/// <response code="200"> On a successful response </response>
		/// <response code="401"> On failing to authenticate </response>
		[HttpPost]
		[Authorize]
		[Route("activate")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> ActivateUser([FromBody] UsernameModel userModel)
		{
			_identityService.RetrieveUserAsync().Wait();
			
			var user = _userManager
				.Users
				.Where(UsersFilter.AllUsersFilter(_identityService.User, _identityService.Groups, DATABASE_OPERATION.UPDATE))
				.FirstOrDefault(u => u.UserName == userModel.Username);
			
			if (user == null)
			{
				return BadRequest("The user does not exist or you do not have permission to activate the user");
			}

			user.EmailConfirmed = true;
			await _userManager.UpdateAsync(user);
			return Ok();
		}

		/// <summary>
		/// Sends a reset password email to a specified user
		/// </summary>
		/// <param name="userModel">The user details</param>
		/// <returns>Returns 200 OK</returns>
		[HttpPost("reset-password-request")]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		public async Task<IActionResult> ResetPasswordRequest([FromBody]UsernameModel userModel)
		{
			try
			{
				var user = await _userManager.Users.FirstAsync(u => u.UserName == userModel.Username);
				_userService.SendPasswordResetEmail(user);
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
			}
			return Ok();
		}

		/// <summary>
		/// Resets a users password for the forgot password workflow
		/// </summary>
		/// <param name="details">The username, password and reset password token for the user</param>
		/// <returns>200 OK on success and 401 on failure</returns>
		[HttpPost("reset-password")]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel details)
		{
			try
			{
				var user = _userManager.Users.FirstOrDefault(u => u.UserName == details.Username);

				if (!await _userManager.VerifyUserTokenAsync(
					user,
					_userManager.Options.Tokens.PasswordResetTokenProvider,
					"ResetPassword",
					details.Token))
				{
					throw new UnauthorizedAccessException($"Invalid password reset token for {details.Username}");
				}

				var result = await _userManager.ResetPasswordAsync(user, details.Token, details.Password);
				if (!result.Succeeded)
				{
					throw new IdentityOperationException(result);
				}

				return Ok();
			}
			catch (IdentityOperationException e)
			{
				_logger.LogError(e.ToString());
				return BadRequest(new ApiErrorResponse(e.IdentityResult.Errors.Select(ie => ie.Description)));
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
				return Unauthorized(new ApiErrorResponse("Could not update user"));
			}
		}

		private void AddErrors(IEnumerable<IdentityError> errors)
		{
			foreach (var error in errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		// % protected region % [Add any account controller methods here] off begin
		// % protected region % [Add any account controller methods here] end
	}
}
