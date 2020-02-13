/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
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
using System.Threading.Tasks;
using Sportstats.Models;
using Sportstats.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Sportstats.Services
{
	public class IdentityService : IIdentityService
	{
		private bool _fetched = false;

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;

		/// <inheritdoc />
		public User User { get; private set; }

		/// <inheritdoc />
		public IList<string> Groups { get; private set; }

		public IdentityService(
			IHttpContextAccessor httpContextAccessor,
			IUserService userService,
			UserManager<User> userManager)
		{
			_httpContextAccessor = httpContextAccessor;
			_userService = userService;
			_userManager = userManager;
		}

		/// <inheritdoc />
		public async Task RetrieveUserAsync()
		{
			if (_fetched != true)
			{
				User = await _userService.GetUserFromClaim(_httpContextAccessor.HttpContext.User);
				Groups = User == null? new List<string>() : await _userManager.GetRolesAsync(User);
				_fetched = true;
			}
		}
	}
}