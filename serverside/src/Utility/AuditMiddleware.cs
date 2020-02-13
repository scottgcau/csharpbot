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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sportstats.Models;
using Microsoft.AspNetCore.Http;

namespace Sportstats.Utility
{
	public class AuditMiddleware
	{
		private readonly RequestDelegate _next;

		public AuditMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, SportstatsDBContext dbContext)
		{
			await _next.Invoke(context);

			var logs = GetLogs(context);
			if (logs.Count > 0)
			{
				logs.ForEach(l => dbContext.AuditLog.Add(l));
				await dbContext.SaveChangesAsync();
			}
		}

		private static List<AuditLog> GetLogs(HttpContext context)
		{
			if (!context.Items.ContainsKey("AuditLogs"))
			{
				return new List<AuditLog>();
			}

			if (context.Items["AuditLogs"] is IList item)
			{
				return item.Cast<AuditLog>().ToList();
			}

			return new List<AuditLog>();
		}
	}
}