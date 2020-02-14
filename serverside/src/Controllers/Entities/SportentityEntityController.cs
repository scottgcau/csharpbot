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

namespace Sportstats.Controllers.Entities
{
	/// <summary>
	/// The controller that provides rest endpoints for the SportentityEntity model
	/// </summary>
	[Route("/api/entity/SportentityEntity")]
	[Authorize]
	[ApiController]
	public class SportentityEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public SportentityEntityController(
			ICrudService crudService
			// % protected region % [Add any extra constructor arguments here] off begin
			// % protected region % [Add any extra constructor arguments here] end
			)
		{
			_crudService = crudService;
			// % protected region % [Add any extra constructor logic here] off begin
			// % protected region % [Add any extra constructor logic here] end
		}

		/// <summary>
		/// Get the SportentityEntity for the given id
		/// </summary>
		/// <param name="id">The id of the SportentityEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The SportentityEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<SportentityEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<SportentityEntity>(id);
			return await result
				.Select(model => new SportentityEntityDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all SportentityEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of SportentityEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<SportentityEntityDto>> Get([FromQuery]SportentityEntityOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<SportentityEntity>(pagination: new Pagination(options));
			return await result
				.Select(model => new SportentityEntityDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Create SportentityEntity
		/// </summary>
		/// <param name="model">The new SportentityEntity to be created</param>
		/// <returns>The SportentityEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<SportentityEntityDto> Post([BindRequired, FromBody] SportentityEntityDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentityEntityDto((await _crudService.Create(new List<SportentityEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an SportentityEntity
		/// </summary>
		/// <param name="model">The SportentityEntity to be updated</param>
		/// <returns>The SportentityEntity object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<SportentityEntityDto> Put([BindRequired, FromBody] SportentityEntityDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentityEntityDto((await _crudService.Update(new List<SportentityEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a SportentityEntity
		/// </summary>
		/// <param name="id">The id of the SportentityEntity to delete</param>
		/// <returns>The ids of the deleted SportentityEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<SportentityEntity>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of SportentityEntitys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentityEntitys</returns>
		[HttpGet]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentityEntity>().AddWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		/// <summary>
		/// Export a list of SportentityEntitys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentityEntitys</returns>
		[HttpPost]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentityEntity>().AddConditionalWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		private async Task<IActionResult> ExportData(
			IQueryable<SportentityEntity> queryable,
			CancellationToken cancellationToken)
		{
			try
			{
				var result = await _crudService.ExportAsCsv<SportentityEntity, SportentityEntityDto>(queryable, cancellationToken);
				return CreateCsvResponse(Encoding.ASCII.GetBytes(result), "export_sportentity");
			}
			catch
			{
				return BadRequest(new ApiErrorResponse("Invalid query"));
			}
		}

		public class SportentityEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}
