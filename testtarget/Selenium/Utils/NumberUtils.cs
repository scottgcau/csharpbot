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

using System.Linq;
using APITests.DataFixtures;

namespace SeleniumTests.Utils
{
	internal static class NumberUtils
	{
		/// <summary>
		/// Will return an array of a specific length with random numbers that total the input number
		/// </summary>
		/// <param name="numNumbers"></param>
		/// <param name="numbersSum"></param>
		/// <returns></returns>
		public static int[] GetRandomIntegers(int numNumbers, int numbersSum)
		{
			var rnd = BaseChoice.Rand;
			var outputInts = new int[numNumbers];

			foreach(var index in Enumerable.Range(0,numNumbers))
			{
				var maxRngValue = numbersSum - outputInts.Sum() - numNumbers + index + 1;
				outputInts[index] = rnd.Next(1, maxRngValue);
			}

			outputInts[numNumbers - 1] = numbersSum - outputInts.Sum();
			return outputInts;
		}
	}
}
