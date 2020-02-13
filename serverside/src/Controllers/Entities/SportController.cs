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
	/// The controller that provides rest endpoints for the Sport model
	/// </summary>
	[Route("/api/sport")]
	[Authorize]
	[ApiController]
	public class SportController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public SportController(ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the Sport for the given id
		/// </summary>
		/// <param name="id">The id of the Sport to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The Sport object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<SportDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<Sport>(id);
			return await result
				.Select(model => new SportDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all Sports with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of Sports</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<SportDto>> Get([FromQuery]SportOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<Sport>(pagination: new Pagination(options));
			return await result
				.Select(model => new SportDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Update an Sport
		/// </summary>
		/// <param name="model">The new Sport that shall be updated</param>
		/// <returns>The Sport object after it has been updated</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<SportDto> Post([BindRequired, FromBody] SportDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportDto((await _crudService.Create(new List<Sport>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an Sport
		/// </summary>
		/// <param name="model">The new Sport that shall be updated</param>
		/// <returns>The Sport object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<SportDto> Put([BindRequired, FromBody] SportDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportDto((await _crudService.Update(new List<Sport>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a Sport
		/// </summary>
		/// <param name="id">The id of the Sport to delete</param>
		/// <returns>The ids of the deleted Sports</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<Sport>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of Sports with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of Sports</returns>
		[HttpGet]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<Sport>().AddWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		/// <summary>
		/// Export a list of Sports with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of Sports</returns>
		[HttpPost]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<Sport>().AddConditionalWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		private async Task<IActionResult> ExportData(
			IQueryable<Sport> queryable,
			CancellationToken cancellationToken)
		{
			try
			{
				var result = await _crudService.ExportAsCsv<Sport, SportDto>(queryable, cancellationToken);
				return CreateCsvResponse(Encoding.ASCII.GetBytes(result), "export_sport");
			}
			catch
			{
				return BadRequest(new ApiErrorResponse("Invalid query"));
			}
		}

		public class SportOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}