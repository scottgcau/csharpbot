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
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Sportstats.Services;
using Sportstats.Services.Interfaces;
using GraphQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Npgsql;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Controllers
{
	/// <summary>
	/// The controller that manages all GraphQL operations
	/// </summary>
	[Route("/api/graphql")]
	[ApiController]
	[Authorize(Policy = "AllowVisitorPolicy")]
	public class GraphQlController : Controller
	{
		private readonly IGraphQlService _graphQlService;
		private readonly IUserService _userService;

		public GraphQlController(IGraphQlService graphQlService, IUserService userService)
		{
			_graphQlService = graphQlService;
			_userService = userService;
		}

		/// <summary>
		/// Executor for GraphQL queries
		/// </summary>
		/// <param name="body"></param>
		/// <param name="cancellation"></param>
		/// <returns>The results for the GraphQL query</returns>
		[HttpPost]
		[Authorize(Policy = "AllowVisitorPolicy")]
		public async Task<ExecutionResult> Post(
			[BindRequired, FromBody] PostBody body,
			CancellationToken cancellation)
		{
			var user = await _userService.GetUserFromClaim(User);
			ExecutionResult result = await _graphQlService.Execute(body.Query, body.OperationName, body.Variables, user, cancellation);
			if (result.Errors?.Count > 0)
			{
				var newEx = new ExecutionErrors();
				foreach (var error in result.Errors)
				{
					var ex = error.InnerException;
					if (ex is PostgresException pgException)
					{
						if (string.IsNullOrWhiteSpace(pgException.MessageText))
						{
							newEx.Add(error);
						}
						else
						{
							newEx.Add(new ExecutionError(pgException.MessageText));
						}
					}
					else
					{
						newEx.Add(error);
					}
				}
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				result.Errors = newEx;
			}
			return result;
		}

		public class PostBody
		{
			public string OperationName;
			public string Query;
			public JObject Variables;
		}

		/// <summary>
		/// Executor for GraphQL queries
		/// </summary>
		/// <param name="query"></param>
		/// <param name="variables"></param>
		/// <param name="operationName"></param>
		/// <param name="cancellation"></param>
		/// <returns>The results for the GraphQL query</returns>
		[HttpGet]
		[Authorize(Policy = "AllowVisitorPolicy")]
		public async Task<ExecutionResult> Get(
			[FromQuery] string query,
			[FromQuery] string variables,
			[FromQuery] string operationName,
			CancellationToken cancellation)
		{
			var jObject = ParseVariables(variables);
			var user = await _userService.GetUserFromClaim(User);
			ExecutionResult result = await _graphQlService.Execute(query, operationName, jObject, user, cancellation);
			if (result.Errors?.Count > 0)
			{
				var newEx = new ExecutionErrors();
				foreach (var error in result.Errors)
				{
					var ex = error.InnerException;
					if (ex is PostgresException pgException)
					{
						if (string.IsNullOrWhiteSpace(pgException.MessageText))
						{
							newEx.Add(error);
						}
						else
						{
							newEx.Add(new ExecutionError(pgException.MessageText));
						}
					}
					else
					{
						newEx.Add(error);
					}
				}
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				result.Errors = newEx;
			}
			return result;
		}

		static JObject ParseVariables(string variables)
		{
			if (variables == null)
			{
				return null;
			}

			try
			{
				return JObject.Parse(variables);
			}
			catch (Exception exception)
			{
				throw new Exception("Could not parse variables.", exception);
			}
		}
	}
}