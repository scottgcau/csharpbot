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
	/// The controller that provides rest endpoints for the SportentityEntityFormTileEntity model
	/// </summary>
	[Route("/api/entity/SportentityEntityFormTileEntity")]
	[Authorize]
	[ApiController]
	public class SportentityEntityFormTileEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public SportentityEntityFormTileEntityController(
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
		/// Get the SportentityEntityFormTileEntity for the given id
		/// </summary>
		/// <param name="id">The id of the SportentityEntityFormTileEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The SportentityEntityFormTileEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<SportentityEntityFormTileEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<SportentityEntityFormTileEntity>(id);
			return await result
				.Select(model => new SportentityEntityFormTileEntityDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all SportentityEntityFormTileEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of SportentityEntityFormTileEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<SportentityEntityFormTileEntityDto>> Get([FromQuery]SportentityEntityFormTileEntityOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<SportentityEntityFormTileEntity>(pagination: new Pagination(options));
			return await result
				.Select(model => new SportentityEntityFormTileEntityDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Create SportentityEntityFormTileEntity
		/// </summary>
		/// <param name="model">The new SportentityEntityFormTileEntity to be created</param>
		/// <returns>The SportentityEntityFormTileEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<SportentityEntityFormTileEntityDto> Post([BindRequired, FromBody] SportentityEntityFormTileEntityDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentityEntityFormTileEntityDto((await _crudService.Create(new List<SportentityEntityFormTileEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an SportentityEntityFormTileEntity
		/// </summary>
		/// <param name="model">The SportentityEntityFormTileEntity to be updated</param>
		/// <returns>The SportentityEntityFormTileEntity object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<SportentityEntityFormTileEntityDto> Put([BindRequired, FromBody] SportentityEntityFormTileEntityDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentityEntityFormTileEntityDto((await _crudService.Update(new List<SportentityEntityFormTileEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a SportentityEntityFormTileEntity
		/// </summary>
		/// <param name="id">The id of the SportentityEntityFormTileEntity to delete</param>
		/// <returns>The ids of the deleted SportentityEntityFormTileEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<SportentityEntityFormTileEntity>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of SportentityEntityFormTileEntitys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentityEntityFormTileEntitys</returns>
		[HttpGet]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentityEntityFormTileEntity>().AddWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		/// <summary>
		/// Export a list of SportentityEntityFormTileEntitys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentityEntityFormTileEntitys</returns>
		[HttpPost]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentityEntityFormTileEntity>().AddConditionalWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		private async Task<IActionResult> ExportData(
			IQueryable<SportentityEntityFormTileEntity> queryable,
			CancellationToken cancellationToken)
		{
			try
			{
				var result = await _crudService.ExportAsCsv<SportentityEntityFormTileEntity, SportentityEntityFormTileEntityDto>(queryable, cancellationToken);
				return CreateCsvResponse(Encoding.ASCII.GetBytes(result), "export_sportentity_entity_form_tile");
			}
			catch
			{
				return BadRequest(new ApiErrorResponse("Invalid query"));
			}
		}

		public class SportentityEntityFormTileEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}
