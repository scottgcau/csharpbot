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
	/// The controller that provides rest endpoints for the SportentitySubmissionEntity model
	/// </summary>
	[Route("/api/entity/SportentitySubmissionEntity")]
	[Authorize]
	[ApiController]
	public class SportentitySubmissionEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public SportentitySubmissionEntityController(
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
		/// Get the SportentitySubmissionEntity for the given id
		/// </summary>
		/// <param name="id">The id of the SportentitySubmissionEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The SportentitySubmissionEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<SportentitySubmissionEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<SportentitySubmissionEntity>(id);
			return await result
				.Select(model => new SportentitySubmissionEntityDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all SportentitySubmissionEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of SportentitySubmissionEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<SportentitySubmissionEntityDto>> Get([FromQuery]SportentitySubmissionEntityOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<SportentitySubmissionEntity>(pagination: new Pagination(options));
			return await result
				.Select(model => new SportentitySubmissionEntityDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Create SportentitySubmissionEntity
		/// </summary>
		/// <param name="model">The new SportentitySubmissionEntity to be created</param>
		/// <returns>The SportentitySubmissionEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<SportentitySubmissionEntityDto> Post([BindRequired, FromBody] SportentitySubmissionEntityDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentitySubmissionEntityDto((await _crudService.Create(new List<SportentitySubmissionEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an SportentitySubmissionEntity
		/// </summary>
		/// <param name="model">The SportentitySubmissionEntity to be updated</param>
		/// <returns>The SportentitySubmissionEntity object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<SportentitySubmissionEntityDto> Put([BindRequired, FromBody] SportentitySubmissionEntityDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SportentitySubmissionEntityDto((await _crudService.Update(new List<SportentitySubmissionEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a SportentitySubmissionEntity
		/// </summary>
		/// <param name="id">The id of the SportentitySubmissionEntity to delete</param>
		/// <returns>The ids of the deleted SportentitySubmissionEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<SportentitySubmissionEntity>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of SportentitySubmissionEntitys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentitySubmissionEntitys</returns>
		[HttpGet]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentitySubmissionEntity>().AddWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		/// <summary>
		/// Export a list of SportentitySubmissionEntitys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A csv file containing the export of SportentitySubmissionEntitys</returns>
		[HttpPost]
		[Route("export")]
		[Authorize]
		public async Task<IActionResult> ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellation)
		{
			var query = _crudService.Get<SportentitySubmissionEntity>().AddConditionalWhereFilter(conditions);
			return await ExportData(query, cancellation);
		}

		private async Task<IActionResult> ExportData(
			IQueryable<SportentitySubmissionEntity> queryable,
			CancellationToken cancellationToken)
		{
			try
			{
				var result = await _crudService.ExportAsCsv<SportentitySubmissionEntity, SportentitySubmissionEntityDto>(queryable, cancellationToken);
				return CreateCsvResponse(Encoding.ASCII.GetBytes(result), "export_sportentity_submission");
			}
			catch
			{
				return BadRequest(new ApiErrorResponse("Invalid query"));
			}
		}

		public class SportentitySubmissionEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}
