using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
namespace FYP_FC_Evaluator___UI
{

	static class FOLSearcher
	{
		public static List<List<Assignment>> Search(string body, string query, bool topDownSearch)
		{ 
			List<string> subwords = getAllSubwords(body);
			string folExpression = query.Substring(query.IndexOf(":=") + 2);
			string freeVarsString = query.Substring(query.IndexOf('(') + 1).Substring(0, query.IndexOf(')') - 2);
			string[] freeVars = freeVarsString.Split(',');

			ParseTreeFOL_Q parsedExpression = FOLParser.Parse(folExpression, body);

			List<List<Assignment>> res;
			if (topDownSearch)
			{
				res= TopDownFOLEvaluator.evaluateParsedExpression(parsedExpression, subwords.ToArray(), freeVars, body);
			}
			else {
				res= BottomUpFOLEvaluator.EvaluateParsedExpression(parsedExpression, subwords.ToArray(), freeVars, body);
			}
			return res;
		}

		//Works fine.
		static List<String> getAllSubwords(string word)
		{
			if (word == "")
			{
				return new List<String> { "" };
			}
			else
			{
			List<String> res = getAllSubwords(word.Remove(word.Length - 1));
				foreach (string rightWord in getRightSubwords(word.Remove(0,1)))
				{
					if (!res.Contains(rightWord))
					{
						res.Add(rightWord);
					}
				}
			if (!res.Contains(word)) { 
				res.Add(word);
			}
			return res;
			}
		}
		static List<String> getRightSubwords(string word)
		{
			if (word == "")
			{
				return new List<String> { };
			}
			else if (word.Length == 1)
			{
				return new List<String> { word };
			}
			else
			{
				List<String> res = getRightSubwords(word.Remove(0, 1));
				if (!res.Contains(word))
				{
					res.Add(word);
				}
				return res;
			}
		}
		public static List<List<Assignment>> removeVariable(List<List<Assignment>> solutions, string var)
		{
			for (int i = 0; i < solutions.Count; i++)
			{
				solutions[i].RemoveAll(assignment => assignment.GetVariableName() == var);
			}
			return Distinct(solutions);
		}
		public static List<List<Assignment>> Distinct(List<List<Assignment>> solutions)
		{
			List<List<Assignment>> distinctSols = new List<List<Assignment>>();
			foreach (List<Assignment> sol in solutions)
			{
				if (!distinctSols.Any(s => BottomUpFOLEvaluator.CompareSolutions(s, sol)))
				{
					distinctSols.Add(sol);
				}
			}
			return distinctSols;
		}
		 public static string[] GetFreeVars(List<List<Assignment>> sol)
		{
			List<string> freeVars = new List<string>();
			if (sol.Count == 0)
			{
				return freeVars.ToArray();
			}
			foreach (Assignment assignment in sol[0])
			{
				if (!freeVars.Contains(assignment.GetVariableName()))
				{
					freeVars.Add(assignment.GetVariableName());
				}
			}
			return freeVars.ToArray();
		}

		public static string[] GetFreeVars(ParseTreeFOL_Q query)
		{
			if(query.getExpression()!=null)
			{
				return GetFreeVars(query.getExpression());
			}
			List<string> vars = GetFreeVars(query.getChild()).ToList();
			vars.Remove(query.getQuantifiedVar());
			return vars.ToArray();
		}

		public static string[] GetFreeVars(ParseTreeFOL_O expression)
		{
			if (expression.getTerm() != null)
			{
				return GetFreeVars(expression.getTerm());
			}
			return GetFreeVars(expression.getLeftChild()).ToList().Concat(GetFreeVars(expression.getRightChild()).ToList()).Distinct().ToArray();
		}

		public static string[] GetFreeVars(Term term)
		{
			string[] lhs = term.getLhs();
			string[] rhs = term.getRhs();
			List<string> freeVars = new List<string>();
			foreach (string atom in lhs)
			{
				if (atom[0] != '"' && atom != "S")
				{
					freeVars.Add(atom);
				}
			}
			foreach (string atom in rhs)
			{
				if (atom[0] != '"' && atom != "S")
				{
					freeVars.Add(atom);
				}
			}
			freeVars = freeVars.Distinct().ToList();
			return freeVars.ToArray();
		}
	}
}
