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

using APITests.Factories;
using Xunit;

namespace APITests.TheoryData.BotWritten
{
	public class UserEntityFactorySingleTheoryData : TheoryData<UserEntityFactory>
	{
		public UserEntityFactorySingleTheoryData()
		{
			Add(new UserEntityFactory("User"));
		}
	}

	public class EntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactorySingleTheoryData()
		{
			Add(new EntityFactory("Sport"), 1);
			Add(new EntityFactory("League"), 1);
			Add(new EntityFactory("User"), 1);
		}
	}

	public class NonUserEntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public NonUserEntityFactorySingleTheoryData()
		{
			Add(new EntityFactory("Sport"), 1);
			Add(new EntityFactory("League"), 1);
			Add(new EntityFactory("User"), 1);
		}
	}

	public class EntityFactoryTheoryData : TheoryData<EntityFactory>
	{
		public EntityFactoryTheoryData()
		{
			Add(new EntityFactory("Sport"));
			Add(new EntityFactory("League"));
			Add(new EntityFactory("User"));
		}
	}

	public class EntityFactoryMultipleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactoryMultipleTheoryData()
		{
			var numEntities = 3;
			Add(new EntityFactory("Sport"), numEntities);
			Add(new EntityFactory("League"), numEntities);
			Add(new EntityFactory("User"), numEntities);
		}
	}
}