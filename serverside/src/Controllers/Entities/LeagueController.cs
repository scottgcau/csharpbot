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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sportstats.Helpers;
using Sportstats.Models;
using Sportstats.Services;
using Sportstats.Services.Interfaces;
using Sportstats.Utility;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Sportstats.Controllers
{
	/// <summary>
	/// The controller that provides rest endpoints for the League model
	/// </summary>
	[Route("/api/league")]
	[Authorize]
	[ApiController]
	public class LeagueController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public LeagueController(ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the League for the given id
		/// </summary>
		/// <param name="id">The id of the League to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The League object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<LeagueDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<League>(id);
			return await result
				.Select(model => new LeagueDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all Leagues with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of Leagues</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<LeagueDto>> Get([FromQuery]LeagueOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<League>(pagination: new Pagination(options));
			return await result
				.Select(model => new LeagueDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Update an League
		/// </summary>
		/// <param name="model">The new League that shall be updated</param>
		/// <returns>The League object after it has been updated</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<LeagueDto> Post([BindRequired, FromBody] LeagueDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new LeagueDto((await _crudService.Create(new List<League>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an League
		/// </summary>
		/// <param name="model">The new League that shall be updated</param>
		/// <returns>The League object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<LeagueDto> Put([BindRequired, FromBody] LeagueDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new LeagueDto((await _crudService.Update(new List<League>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a League
		/// </summary>
		/// <param name="id">The id of the League to delete</param>
		/// <returns>The ids of the deleted Leagues</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<League>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of Leagues with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of Leagues</returns>
		[HttpGet]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<League>().AddWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		/// <summary>
		/// Export a list of Leagues with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of Leagues</returns>
		[HttpPost]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<League>().AddConditionalWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		private async Task<IActionResult> ExportData(
			IQueryable<League> queryable,
			CancellationToken cancellationToken)
		{
			try
			{
				var result = await _crudService.ExportAsCsv<League, LeagueDto>(queryable, cancellationToken);
				return CreateCsvResponse(Encoding.ASCII.GetBytes(result), "export_league");
			}
			catch
			{
				return BadRequest(new ApiErrorResponse("Invalid query"));
			}
		}

		public class LeagueOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}