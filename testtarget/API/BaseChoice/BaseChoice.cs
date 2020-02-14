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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace APITests.DataFixtures
{
	[Obsolete("basechoice data generation library has been deprecated, please use TestDataLib.DataUtils")]
	public static class BaseChoice
	{
		private readonly static int[] _specialChars =
		{ 0x0001,0x0080,0x0100,0x0180,0x0250,0x02B0,0x0300,0x0370,0x0400,0x0500,0x0530,0x0590,0x0600,0x0700,0x0750,0x0780,0x07C0,0x0800,0x0840,0x0860,0x08A0,0x0900,0x0980,0x0A00,
		0x0A80,0x0B00,0x0B80,0x0C00,0x0C80,0x0D00,0x0D80,0x0E00,0x0E80,0x0F00,0x1000,0x10A0,0x1100,0x1200,0x1380,0x13A0,0x1400,0x1680,0x16A0,0x1700,0x1720,0x1740,0x1760,0x1780,
		0x1800,0x18B0,0x1900,0x1950,0x1980,0x19E0,0x1A00,0x1A20,0x1AB0,0x1B00,0x1B80,0x1BC0,0x1C00,0x1C50,0x1C80,0x1C90,0x1CC0,0x1CD0,0x1D00,0x1D80,0x1DC0,0x1E00,0x1F00,0x2000,
		0x2070,0x20A0,0x20D0,0x2100,0x2150,0x2190,0x2200,0x2300,0x2400,0x2440,0x2460,0x2500,0x2580,0x25A0,0x2600,0x2700,0x27C0,0x27F0,0x2800,0x2900,0x2980,0x2A00,0x2B00,0x2C00,
		0x2C60,0x2C80,0x2D00,0x2D30,0x2D80,0x2DE0,0x2E00,0x2E80,0x2F00,0x2FF0,0x3000,0x3040,0x30A0,0x3100,0x3130,0x3190,0x31A0,0x31C0,0x31F0,0x3200,0x3300,0x3400,0x4DC0,0x4E00,
		0xA000,0xA490,0xA4D0,0xA500,0xA640,0xA6A0,0xA700,0xA720,0xA800,0xA830,0xA840,0xA880,0xA8E0,0xA900,0xA930,0xA960,0xA980,0xA9E0,0xAA00,0xAA60,0xAA80,0xAAE0,0xAB00,0xAB30,
		0xAB70,0xABC0,0xAC00,0xD7B0,0xE000,0xF900,0xFB00,0xFB50,0xFE00,0xFE10,0xFE20,0xFE30,0xFE50,0xFE70,0xFF00,0xFFF0,0x10000,0x10080,0x10100,0x10140,
		0x10190,0x101D0,0x10280,0x102A0,0x102E0,0x10300,0x10330,0x10350,0x10380,0x103A0,0x10400,0x10450,0x10480,0x104B0,0x10500,0x10530,0x10600,0x10800,0x10840,0x10860,0x10880,
		0x108E0,0x10900,0x10920,0x10980,0x109A0,0x10A00,0x10A60,0x10A80,0x10AC0,0x10B00,0x10B40,0x10B60,0x10B80,0x10C00,0x10C80,0x10D00,0x10E60,0x10F00,0x10F30,0x10FE0,0x11000,
		0x11080,0x110D0,0x11100,0x11150,0x11180,0x111E0,0x11200,0x11280,0x112B0,0x11300,0x11400,0x11480,0x11580,0x11600,0x11660,0x11680,0x11700,0x11800,0x118A0,0x119A0,0x11A00,
		0x11A50,0x11AC0,0x11C00,0x11C70,0x11D00,0x11D60,0x11EE0,0x11FC0,0x12000,0x12400,0x12480,0x13000,0x13430,0x14400,0x16800,0x16A40,0x16AD0,0x16B00,0x16E40,0x16F00,0x16FE0,
		0x17000,0x18800,0x1B000,0x1B100,0x1B130,0x1B170,0x1BC00,0x1BCA0,0x1D000,0x1D100,0x1D200,0x1D2E0,0x1D300,0x1D360,0x1D400,0x1D800,0x1E000,0x1E100,0x1E2C0,0x1E800,0x1E900,
		0x1EC70,0x1ED00,0x1EE00,0x1F000,0x1F030,0x1F0A0,0x1F100,0x1F200,0x1F300,0x1F600,0x1F650,0x1F680,0x1F700,0x1F780,0x1F800,0x1F900,0x1FA00,0x1FA70,0x20000,0x2A700,0x2B740,
		0x2B820,0x2CEB0,0x2F800,0xE0000,0xE0100,0xF0000,0x100000};

		private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
		private static readonly string[] _words = {"csharpbot", "writes", "code", "tests", "loves", "making", "bots", "better", "developers", "lorem", "ipsum", "wordy", "strings"};
		public static readonly Random Rand = new Random(88);
		public static string ValidEmail = "test@example.com";
		public static string InvalidEmail = "testexamplecom";

		public static string GetValidString(int minLength = 1, int maxLength = 100)
		{
			//the number of chars for this valid string
			var length = Rand.Next(minLength, maxLength + 1);

			//load in base choice configuration
			//TODO Fix this so it does not need to read in a configuration file, this is a temporary measure
			var baseChoiceConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("SiteConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var charType = baseChoiceConfiguration["basechoice:charType"];

			switch (charType)
			{
				case "alphabetic":
					// alphabetic characters only
					return new string(Enumerable.Repeat(_chars, length).Select(s => s[Rand.Next(s.Length)]).ToArray());
				case "special":
					// special unicode characters only
					return Enumerable.Repeat(_specialChars, length).Select(s => char.ConvertFromUtf32(s[Rand.Next(s.Length)])).Aggregate((a,b) => a+b);
				case "wordy":
					return GetValidWordyString(minLength, maxLength);
				case "mixed":
					//mix od special and ordinary characters
					var result = new StringBuilder();
					for (var i = 0; i < length; i++)
					{
						var choice = Rand.Next(2);
						if (choice == 1)
						{
							result.Append(char.ConvertFromUtf32(_specialChars[Rand.Next(_specialChars.Length)]));
						}
						else
						{
							result.Append(_chars[Rand.Next(_chars.Length)]);
						}
					}
					return result.ToString(0,length);
				default:
					// wordy by default
					return GetValidWordyString(minLength, maxLength);
			}
		}

		/// <summary>
		/// Return a valid email address that has been randomly generated
		/// </summary>
		/// <returns> A valid email address that has been randomly generated </returns>
		public static string GetUniqueValidEmail()
		{
			var emailAddress = new StringBuilder();
			emailAddress.Append(Guid.NewGuid().ToString().Replace("-", ""));
			emailAddress.Append("@example.com");
			return emailAddress.ToString();
		}

		/// <summary>
		/// An integer that is greater or less than the limit
		/// </summary>
		/// <param name="isMax">min/max violation</param>
		/// <param name="limit">the limit as an integer</param>
		/// <returns>integer which violates the limit</returns>
		public static int GetLimitViolatedInt(bool isMax,int limit)
		{
			return isMax ? limit + 1 : limit - 1;
		}

		/// <summary>
		/// A double that is greater or less than the limit
		/// </summary>
		/// <param name="isMax">min/max violation</param>
		/// <param name="limit">the limit</param>
		/// <returns>double which violates the limit</returns>
		public static double GetLimitViolatedDouble(bool isMax, double limit)
		{
			return isMax ? limit + 0.1 : limit - 0.1;
		}

		/// <summary>
		/// A string with length that is greater or less than the limit
		/// </summary>
		/// <param name="isMax">min/max violation</param>
		/// <param name="limit">the string limit</param>
		/// <returns>a string which violates the limit</returns>
		public static string GetLimitViolatedString(bool isMax, int limit)
		{
			return isMax ? GetValidWordyString(limit + 1, limit + 1) : GetValidWordyString(limit - 1, limit - 1);
		}

		/// <summary>
		/// Will return an illegal javascript popup alert in the form of a string
		/// to be used in a text input field to test site security
		/// </summary>
		/// <returns>a confirm window</returns>
		public static string GetIllegalJavascriptString()
		{
			return "js.ExecuteScript(\"window.confirm(\"hello there\");\");";
		}

		/// <summary>
		/// Returns an array of integers with values below, at and above the given edge
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>lower, edge and upper limit cases in a dictionary</returns>
		public static Dictionary<string, int> GetEdgeCaseIntegers(int edge)
		{
			return new Dictionary<string, int>
			{
				{"LowerEdge", edge -1 },
				{"Edge", edge },
				{"UpperEdge", edge +1 }
			};
		}

		/// <summary>
		/// Returns an array of doubles with values below, at and above the given edge
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>lower, edge and upper limit cases in a dictionary</returns>
		public static Dictionary<string, double> GetEdgeCaseDoubles(int edge)
		{
			return new Dictionary<string, double>
			{
				{"LowerEdge", edge - 0.1 },
				{"Edge", edge },
				{"UpperEdge", edge + 0.1 }
			};
		}

		/// <summary>
		/// Returns an array of wordy strings with lengths below, at, and above the given edge
		/// </summary>
		/// <param name="edge"></param>
		/// <returns>lower, edge and upper limit cases in a dictionary</returns>
		public static Dictionary<string, string> GetEdgeCaseWordyString(int edge)
		{
			return new Dictionary<string, string>
			{
				{"LowerEdge", GetValidWordyString(edge-1,edge-1) },
				{"Edge", GetValidWordyString(edge,edge)},
				{"UpperEdge", GetValidWordyString(edge + 1, edge + 1)}
			};
		}

		/// <summary>
		/// Returns a numeric string with a number of digits between input min and max lengths.
		/// </summary>
		/// <param name="minLength"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string GetValidIntString(int minLength = 1, int maxLength = 100)
		{
			var numDigits = Rand.Next(minLength, maxLength + 1);
			var intString = new StringBuilder();
			while(intString.Length < numDigits)
			{
				intString.Append(Rand.Next(10).ToString());
			}
			return intString.ToString();
		}

		public static int GetValidint(int min = 1, int max = 100) => Rand.Next(min, max + 1);

		public static bool GetValidbool() => Rand.Next(0, 2) == 0;

		public static double GetValiddouble(int min = 1, int max = 100)
		{
			return (Rand.NextDouble() * max % (max - min + 1)) + min;
		}

		//TODO: make this random, take start date and end date as params
		public static DateTime GetValidDateTime() => DateTime.Today;

		public static String GetValidWordyString(int minLength = 1, int maxLength = 100)
		{
			//the number of chars for this valid string
			var numChars = Rand.Next(minLength, maxLength + 1);

			//create the string builder we will be using
			var wordyWords = new StringBuilder();
			for (var i = 0; i < numChars; i = wordyWords.Length) {
				//append the words and spacing between words
				wordyWords.Append(_words[Rand.Next(_words.Length)]);
				if (wordyWords.Length < numChars) {wordyWords.Append(" ");}
			}
			return wordyWords.ToString(0, numChars);
		}
	}
}