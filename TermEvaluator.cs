using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	static class TermEvaluator
	{
		//seems to work fine
		public static bool evaluateTerm(Term term, List<Assignment> varVals)
		{
			string lhs = composeString(term.getLhs(), varVals);
			string rhs = composeString(term.getRhs(), varVals);
			StringLogicComparator comparator = term.getComparator();
			int left = lhs.Length;
			int right = rhs.Length;
			switch (comparator)
			{
				case StringLogicComparator.Identical:
					return lhs.Equals(rhs);
				case StringLogicComparator.NotIdentical:
					return !lhs.Equals(rhs);
				case StringLogicComparator.Equal:
					return left == right;
				case StringLogicComparator.Lesser:
					return left < right;
				case StringLogicComparator.LesserOrEqual:
					return left <= right;
				case StringLogicComparator.Greater:
					return left > right;
				case StringLogicComparator.GreaterOrEqual:
					return left >= right;
				case StringLogicComparator.NotEqual:
					return left != right;
				default:
					throw (new Exception("Term parsed with nonexistent comparator."));
			}
		}
		//seems to work fine
		static string composeString(string atom, List<Assignment> varVals)
		{
			if (atom.All(char.IsDigit))
			{
				return atom;
			}
			foreach (Assignment var in varVals)
			{
				if (var.GetVariableName() == atom)
				{
					return var.GetValue();
				}
			}
			if (atom.First().Equals('"') && atom.Last().Equals('"'))
			{
				return atom.Substring(1, atom.Length - 2);
			}
			else
			{
				throw new Exception("Strings are malformed.");
			}
		}
		static string composeString(string[] termStrings, List<Assignment> varVals)
		{
			if (termStrings.Length != 1 && termStrings.Length!=0)
			{
				return composeString(termStrings[0], varVals) + composeString(termStrings.Skip(1).ToArray(), varVals);
			}
			else
			{
				return composeString(termStrings[0], varVals);
			}
		}
	}
}
