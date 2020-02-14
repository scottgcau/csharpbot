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
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sportstats.Models;
using Sportstats.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ServersideTests.Tests.Unit.BotWritten
{
	public class UserServiceTests
	{
		[Fact(Skip = "Needs update to mocking")]
		public async Task testUserAsync()
		{
			var mockIdentityOptions = new Mock<IOptions<IdentityOptions>>();

			var mockUserManager = new Mock<UserManager<User>>(
					new Mock<IUserStore<User>>().Object,
					new Mock<IOptions<IdentityOptions>>().Object,
					new Mock<IPasswordHasher<User>>().Object,
					new IUserValidator<User>[0],
					new IPasswordValidator<User>[0],
					new Mock<ILookupNormalizer>().Object,
					new Mock<IdentityErrorDescriber>().Object,
					new Mock<IServiceProvider>().Object,
					new Mock<ILogger<UserManager<User>>>().Object);

			var user = new User();
			user.Email = "test@example.com";
			user.UserName = "test@example.com";
			IList<string> testList = new List<string>();
			testList.Add("BBB");
			testList.Add("ZZZ");
			testList.Add("AAA");

			//IQueryable<Group> groupList = (IQueryable<Group>)new List<Group>();
			var group = new Group();
			group.Name = "Fake Group";

			//var data = new List<Group>() { group }.AsQueryable();

			var data = new List<Group>
			{
				new Group { Name = "BBB" },
				new Group { Name = "ZZZ" },
				new Group { Name = "AAA" },
			}.AsQueryable();


			var mockSet = new Mock<DbSet<Group>>();
			mockSet.As<IAsyncEnumerable<Group>>()
				.Setup(m => m.GetAsyncEnumerator(default))
				.Returns(new TestAsyncEnumerator<Group>(data.GetEnumerator()));



			mockSet.As<IQueryable<Group>>()
			   .Setup(m => m.Provider)
			   .Returns(new TestAsyncQueryProvider<Group>(data.Provider));

			mockSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockUserManager.Setup(x => x.GetRolesAsync(user)).Returns(Task.FromResult(testList));


			//mockRoleManager
			#region Mocked Items
			//var roleName = mockUserManager.Setup(x => x.GetRolesAsync(user)).Returns(Task.FromResult(testList));
			//mockRoleManager.Setup(x => x.Roles.Where(role => testList.Contains(role.Name))).Returns(groupList);


			var mockIHttpContextAccessor = new Mock<IHttpContextAccessor>();


			var mockEmailAccount = new Mock<IOptions<EmailAccount>>();
			var mockEmailService = new Mock<EmailService>(mockEmailAccount.Object);

			var mockIConfiguration = new Mock<IConfiguration>();

			var mockPrincipalCLaims = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
			var mockOptionsAccessor = new Mock<IOptions<Microsoft.AspNetCore.Identity.IdentityOptions>>().Object;
			var mockILogger = new Mock<ILogger<Microsoft.AspNetCore.Identity.SignInManager<User>>>().Object;
			var MockAuthScheme = new Mock<IAuthenticationSchemeProvider>().Object;

			var mockSignInManager = new Mock<SignInManager<User>>(mockUserManager.Object, mockIHttpContextAccessor.Object, mockPrincipalCLaims, mockOptionsAccessor, mockILogger, MockAuthScheme);

			var mockIRoleStore = new Mock<IRoleStore<IdentityRole>>().Object;
			var mockIEnumerable = new Mock<IEnumerable<IRoleValidator<IdentityRole>>>().Object;
			var mockILookupNormalizer = new Mock<ILookupNormalizer>().Object;
			var mockIdentityErrorDescriber = new Mock<IdentityErrorDescriber>().Object;
			var mockIRoleLogger = new Mock<ILogger<RoleManager<IdentityRole>>>().Object;

			#endregion
			var mockRoleManager = new Mock<RoleManager<Group>>(
					new Mock<IRoleStore<Group>>().Object,
					new IRoleValidator<Group>[0],
					new Mock<ILookupNormalizer>().Object,
					new Mock<IdentityErrorDescriber>().Object,
					new Mock<ILogger<RoleManager<Group>>>().Object);

			mockRoleManager.Setup(x => x.Roles).Returns(mockSet.Object);


			var userService = new UserService(
				mockIdentityOptions.Object,
				mockSignInManager.Object,
				mockUserManager.Object,
				mockIHttpContextAccessor.Object,
				mockRoleManager.Object,
				mockEmailService.Object,
				mockIConfiguration.Object);

			var result = await userService.GetUser(user);
			var h = result.Groups;
		}
	}

	internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
	{
		private readonly IQueryProvider _inner;

		internal TestAsyncQueryProvider(IQueryProvider inner)
		{
			_inner = inner;
		}

		public IQueryable CreateQuery(Expression expression)
		{
			return new TestAsyncEnumerable<TEntity>(expression);
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new TestAsyncEnumerable<TElement>(expression);
		}

		public object Execute(Expression expression)
		{
			return _inner.Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return _inner.Execute<TResult>(expression);
		}

		public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
		{
			return new TestAsyncEnumerable<TResult>(expression);
		}

		public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Execute<TResult>(expression));
		}

		TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => throw new NotImplementedException();
	}

	internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
	{
		public TestAsyncEnumerable(IEnumerable<T> enumerable)
			: base(enumerable)
		{ }

		public TestAsyncEnumerable(Expression expression)
			: base(expression)
		{ }

		public IAsyncEnumerator<T> GetEnumerator()
		{
			return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
		}

		public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		IQueryProvider IQueryable.Provider
		{
			get { return new TestAsyncQueryProvider<T>(this); }
		}
	}

	internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
	{
		private readonly IEnumerator<T> _inner;

		public TestAsyncEnumerator(IEnumerator<T> inner)
		{
			_inner = inner;
		}

		public void Dispose()
		{
			_inner.Dispose();
		}

		public T Current
		{
			get
			{
				return _inner.Current;
			}
		}

		public Task<bool> MoveNext(CancellationToken cancellationToken)
		{
			return Task.FromResult(_inner.MoveNext());
		}

		public ValueTask<bool> MoveNextAsync() => throw new NotImplementedException();
		public ValueTask DisposeAsync() => throw new NotImplementedException();
	}
}
