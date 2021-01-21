using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator___UI
{
	static class FOLParser
	{
		public static ParseTreeFOL_Q Parse(string input, string material)
		{
			string splitterString = "::";
			int splitIndex = input.IndexOf(splitterString);
			int leftSideLength = splitIndex;
			int rightSideStart = splitIndex + splitterString.Length;
			if (splitIndex == -1)
			{
				leftSideLength = 0;
				rightSideStart = 0;
			}

			string lhs = input.Substring(0,leftSideLength);
			string rhs = input.Substring(rightSideStart);

			ValidateBrackets(rhs);
			
			List<String> lexemes = LexExpression(rhs);

			ParseTreeFOL_O expression;
			if (lexemes.Count == 0)
			{
				expression = null;
			}
			else
			{
				expression = ParseExpression(lexemes, material);
			}

			return ParseQuantifiers(lhs,expression);
		}

		private static ParseTreeFOL_Q ParseQuantifiers(string quantifierString, ParseTreeFOL_O expression)
		{
			
			quantifierString = FillInQuantifiers(quantifierString);
			string[] quantifierStrings = quantifierString.Split(':');
			quantifierStrings=quantifierStrings.Reverse().ToArray();
			ParseTreeFOL_Q output = new ParseTreeFOL_Q(null, null, expression);
			if (quantifierStrings[0] != "")
			{
				foreach (string str in quantifierStrings)
				{
					if (str.First().Equals('E'))
					{
						output = new ParseTreeFOL_Q(FOLQuantifier.E, str.Substring(1), output);
					}
					else if (str.First().Equals('A'))
					{
						output = new ParseTreeFOL_Q(FOLQuantifier.A, str.Substring(1), output);
					}
					else
					{
						throw (new Exception("Failed to parse quantifiers."));
					}
				}
			}
			return output;
		}

		private static string FillInQuantifiers(string str) {
			while (str.IndexOf(',') != -1)
			{
				int firstCommaIndex = str.IndexOf(',');
				string firstSection = str.Substring(0, firstCommaIndex);
				string lastSection = str.Substring(firstCommaIndex + 1);
				int lastExists = firstSection.LastIndexOf("E");
				int lastAll = firstSection.LastIndexOf("A");
				if (lastExists > lastAll)
				{
					str = firstSection + ":E" + lastSection;
				}
				else if (lastAll > lastExists)
				{
					str = firstSection + ":A" + lastSection;
				}
				else
				{
					throw (new Exception("Quantifier doesn't exist."));
				}
			}
			return str;
		}
		public static string RemoveUnquotedInstancesOf(string body, string pattern)
		{
			int firstUnquotedIndexOfPattern = FirstUnquotedIndexOf(body, pattern);
			while (firstUnquotedIndexOfPattern != -1)
			{
				List<char> bodyChars = body.ToList();
				for (int i=firstUnquotedIndexOfPattern; i<firstUnquotedIndexOfPattern+pattern.Length; i++)
				{
					bodyChars.RemoveAt(i);
				}
				body = new string(bodyChars.ToArray());
				firstUnquotedIndexOfPattern = FirstUnquotedIndexOf(body, pattern);
			}
			return body;
		}


		private static int FirstUnquotedIndexOf(string body, string pattern)
		{
			//find escaped characters
			//find all unescaped quotes
			//find instances of pattern outside of pairings of quotes
			char[] chars = body.ToCharArray();
			char escapeChar = '\\';

			int indexOfSlash = body.IndexOf(escapeChar);
			List<int> escapedSpeechMarkIndices = new List<int>();
			//gets escaped speech mark indices, nulls all escape characters
			while (indexOfSlash != -1)
			{
				int nextStartPoint;
				char nextChar = body[indexOfSlash + 1];
				switch(nextChar){
					case '\\':
						nextStartPoint=indexOfSlash + 2;
						break;
					case '"':
						chars[indexOfSlash] = '\0';
						escapedSpeechMarkIndices.Add(indexOfSlash + 1);
						nextStartPoint = indexOfSlash + 2;
						break;
					default:
						nextStartPoint = indexOfSlash + 1;
						break;
				}
				if (nextStartPoint < body.Length)
				{
					indexOfSlash = body.Substring(nextStartPoint).IndexOf('\\');
				} else
				{
					indexOfSlash = -1;
				}
			}
			List<int> unescapedSpeechMarkIndices = new List<int>();
			int indexOfSpeechMark = body.IndexOf('"');
			string unsearchedBody = body;
			while (indexOfSpeechMark != -1)
			{
				if (!escapedSpeechMarkIndices.Contains(indexOfSpeechMark))
				{
					unescapedSpeechMarkIndices.Add(indexOfSpeechMark);
				}
				unsearchedBody = body.Substring(indexOfSpeechMark + 1);
				indexOfSpeechMark = unsearchedBody.IndexOf('"');
				if (indexOfSpeechMark != -1)
				{
					indexOfSpeechMark+=body.Length - unsearchedBody.Length;
				}
			}
			if (unescapedSpeechMarkIndices.Count % 2 != 0)
			{
				throw new Exception("odd number of unescaped speechmarks.");
			}
			List<int[]> unescapedSpeechMarkIndexPairs=new List<int[]>();
			for (int i = 0; i < unescapedSpeechMarkIndices.Count; i += 2)
			{
				unescapedSpeechMarkIndexPairs.Add(new int[] {unescapedSpeechMarkIndices[i], unescapedSpeechMarkIndices[i+1] });
			}

			int patternIndex = body.IndexOf(pattern);
			while (patternIndex != -1)
			{
				if (!unescapedSpeechMarkIndexPairs.Any(pair => pair[0]< patternIndex && patternIndex < pair[1]))
				{
					return patternIndex;
				}
				patternIndex = body.IndexOf(pattern, patternIndex+1);
			}
			return -1;
		}

		private static Term ParseTerm(string term,string material)
		{

			term = term.Substring(1, term.Length - 2);
			string lhs;
			string rhs;
			StringLogicComparator? comparator=null;
			//deconstruct term
			string[] comparators =
				{"!==","=!=","==!",
				"!=","=!",
				"==",
				">=","=>",
				"<=","=<",
				"<",
				">",
				"="
				};
			int comparatorIndex=-1;
			int comparatorLength = -1;
			for (int i=0; i<comparators.Length; i++)
			{
				if (FirstUnquotedIndexOf(term, comparators[i]) != -1)
				{
					
					comparatorIndex = FirstUnquotedIndexOf(term, comparators[i]);
					comparatorLength = comparators[i].Length;
					switch (comparators[i])
					{
						case "!==":
							comparator = StringLogicComparator.NotIdentical;
							break;
						case "=!=":
							comparator = StringLogicComparator.NotIdentical;
							break;
						case "==!":
							comparator = StringLogicComparator.NotIdentical;
							break;
						case "!=":
							comparator = StringLogicComparator.NotEqual;
							break;
						case "=!":
							comparator = StringLogicComparator.NotEqual;
							break;
						case "==":
							comparator = StringLogicComparator.Identical;
							break;
						case ">=":
							comparator = StringLogicComparator.GreaterOrEqual;
							break;
						case "=>":
							comparator = StringLogicComparator.GreaterOrEqual;
							break;
						case "<=":
							comparator = StringLogicComparator.LesserOrEqual;
							break;
						case "=<":
							comparator = StringLogicComparator.LesserOrEqual;
							break;
						case ">":
							comparator = StringLogicComparator.Greater;
							break;
						case "<":
							comparator = StringLogicComparator.Lesser;
							break;
						case "=":
							comparator = StringLogicComparator.Equal;
							break;
						default:
							throw (new Exception($"Cannot identify comparator of term \"{term}\" - check comparator."));
					}
				}
			}
			StringLogicComparator nonNullComparator;
			if (comparator == null)
			{
				throw (new Exception($"Cannot identify comparator of term \"{term}\" - check comparator."));
			}
			else
			{
				nonNullComparator =(StringLogicComparator) comparator;
			}
			lhs = term.Substring(0, comparatorIndex);
			rhs = term.Substring(comparatorIndex + comparatorLength);

			return new Term(EscapeAndAtomizeString(lhs, material), EscapeAndAtomizeString(rhs, material), nonNullComparator);
		}
		//TODO: probably do more thorough testing for this
		static string[] EscapeAndAtomizeString(string body, string material)
		{


			List<string> bodyAtoms = new List<string>();
			int splitPointIndex = FirstUnquotedIndexOf(body, ".");
			while (splitPointIndex != -1)
			{
				bodyAtoms.Add(body.Substring(0, splitPointIndex));
				body = body.Substring(splitPointIndex + 1);
				splitPointIndex = FirstUnquotedIndexOf(body, ".");
			}
			bodyAtoms.Add(body);
			for (int i = 0; i < bodyAtoms.Count; i++)
			{
				char[] chars = bodyAtoms[i].ToCharArray();
				char escapeChar = '\\';
				int indexOfSlash = bodyAtoms[i].IndexOf(escapeChar);
				while (indexOfSlash != -1)
				{
					int nextStartPoint;
					char nextChar = bodyAtoms[i][indexOfSlash + 1];
					switch (nextChar)
					{
						case '\\':
							chars[indexOfSlash] = '\0';
							nextStartPoint = indexOfSlash + 2;
							break;
						case '"':
							chars[indexOfSlash] = '\0';
							nextStartPoint = indexOfSlash + 2;
							break;
						default:
							nextStartPoint = indexOfSlash + 1;
							break;
					}
					if (nextStartPoint < bodyAtoms[i].Length)
					{
						indexOfSlash = bodyAtoms[i].Substring(nextStartPoint).IndexOf('\\');
					}
					else
					{
						indexOfSlash = -1;
					}

					bodyAtoms[i] = "";

					foreach (char c in chars)
					{
						if (c != '\0')
						{
							bodyAtoms[i] += c;
						}
					}
				}
			}

			return bodyAtoms.ToArray();
		}

		private static ParseTreeFOL_O ParseExpression(List<String> lexemes, string material)
		{
			if (lexemes.Count==1)
			{
				return new ParseTreeFOL_O(ParseTerm(lexemes[0], material));
			}
			int i = 0;
			int bracketDepth = 0;
			do
			{
				switch (lexemes[i])
				{
					case "(":
						bracketDepth++;
						break;
					case ")":
						bracketDepth--;
						break;
					case "+":
						if (bracketDepth == 0)
						{
							ParseTreeFOL_O left = ParseExpression(lexemes.Take(i).ToList(), material);
							ParseTreeFOL_O right = ParseExpression(lexemes.Skip(i + 1).ToList(), material);
							return new ParseTreeFOL_O(FOLOperator.Disjunction, left, right);
						}
						break;
				}
				i++;
			} while (i < lexemes.Count);
			i = 0;
			bracketDepth = 0;
			do
			{
				switch (lexemes[i])
				{
					case "(":
						bracketDepth++;
						break;
					case ")":
						bracketDepth--;
						break;
					case "*":
						if (bracketDepth == 0)
						{
							ParseTreeFOL_O left = ParseExpression(lexemes.Take(i).ToList(), material);
							ParseTreeFOL_O right = ParseExpression(lexemes.Skip(i + 1).ToList(), material);
							return new ParseTreeFOL_O(FOLOperator.Conjunction, left, right);
						}
						break;
				}
				i++;
			} while (i < lexemes.Count);

			if (lexemes.First()=="(" && lexemes.Last()==")")
			{
				return ParseExpression(lexemes.Skip(1).Take(lexemes.Count-2).ToList(), material);
			}
			return null;
		}

		private static List<string> LexExpression(string input)
		{
			try
			{
				if (input == "")
				{
					return new List<string>();
				}
				List<string> lexemes = new List<string>();
				if (input.LastIndexOf('(') != -1)
				{
					int indexOfLastOpen = input.LastIndexOf('(');
					int indexOfRelatedClose = input.IndexOf(')', indexOfLastOpen);

					string first = input.Substring(0, indexOfLastOpen);
					string middle = input.Substring(indexOfLastOpen + 1, indexOfRelatedClose - indexOfLastOpen - 1);
					string last = input.Substring(indexOfRelatedClose + 1);

					lexemes.AddRange(LexExpression(first));

					lexemes.Add("(");
					lexemes.AddRange(LexExpression(middle));
					lexemes.Add(")");

					lexemes.AddRange(LexExpression(last));
				}
				else if (input.IndexOf('*') != -1)
				{
					int indexOfConjunction = input.IndexOf('*');
					lexemes.AddRange(LexExpression(input.Substring(0, indexOfConjunction)));
					lexemes.Add("*");
					lexemes.AddRange(LexExpression(input.Substring(indexOfConjunction + 1)));
				}
				else if (input.IndexOf('+') != -1)
				{
					int indexOfConjunction = input.IndexOf('+');
					lexemes.AddRange(LexExpression(input.Substring(0, indexOfConjunction)));
					lexemes.Add("+");
					lexemes.AddRange(LexExpression(input.Substring(indexOfConjunction + 1)));
				}
				else
				{
					lexemes.Add(input);
				}
				return lexemes;
			}
			catch (Exception e)
			{
				throw (new Exception("Badly formed query matrix. Check brackets, operators and parentheses."));
			}
		}

		private static void ValidateBrackets(string input)
		{
			int bracketDepth = 0;
			foreach (char character in input)
			{
				if (character == '(')
				{
					bracketDepth++;
				}
				else if (character == ')')
				{
					bracketDepth--;
				}
				if (bracketDepth < 0)
				{
					throw (new Exception("Unequal number of opening and closing brackets."));
				}
			}
		}
	}
}
