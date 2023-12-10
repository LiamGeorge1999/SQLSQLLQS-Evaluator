using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	static class FOLBottomUpEvaluator
	{
		public static List<List<Assignment>> EvaluateParsedExpression(ParseTreeQuantifierNode quantifiedExpression, string[] subwords, string[] freeVars, string body)
		{
			List<List<Assignment>> res = EvaluateQuantifiedExpression(quantifiedExpression, subwords, body);
			List<string> varList = freeVars.ToList();
			varList.RemoveAll(var => res.Any(sol => sol.Any(a => a.GetVariableName() == var)));
			res = FillInSolutions(res, varList.ToArray(), subwords);
			return res;
		}
		public static List<List<Assignment>> EvaluateQuantifiedExpression(ParseTreeQuantifierNode quantifiedExpression, string[] subwords, string body)
		{
			List<List<Assignment>> solutions;
			if (quantifiedExpression.getExpression() == null)
			{
				solutions = EvaluateQuantifiedExpression(quantifiedExpression.getChild(), subwords, body);
				Quantifier quantifier = (Quantifier)quantifiedExpression.getFOLQuantifier();
				string variableName = quantifiedExpression.getQuantifiedVar();
				if (quantifier == Quantifier.E)
				{
					if (!solutions.Any(sol => sol.Any(assignment => assignment.GetVariableName() == variableName)))
					{
						solutions.RemoveAll(a => true);
					}
					else
					{
						solutions = FOLSearcher.removeVariable(solutions, variableName);
					}
				}
				else
				{
					List<List<Assignment>> newSols = new List<List<Assignment>>();
					List<List<Assignment>> solsWithoutVar = FOLSearcher.removeVariable(
						solutions.ConvertAll(sol => new List<Assignment>(sol)),
						variableName);
					
					foreach (List<Assignment> solWithoutVar in solsWithoutVar)
					{
						bool include = true;
						foreach (string word in subwords)
						{
							List<Assignment> sol = new List<Assignment>(solWithoutVar);
							sol.Add(new Assignment(variableName, word));
							if (!solutions.Any(solution => CompareSolutions(sol, solution)))
							{
								include = false;
							}
						}
						if (include)
						{
							newSols.Add(solWithoutVar);
						}
					}
					solutions = newSols;
				}
			}
			else
			{
				solutions = EvaluateLogicalExpression(quantifiedExpression.getExpression(), subwords, body);
			}
			return solutions;
		}
		static List<Assignment> addAndReturnAssignment(List<Assignment> sol, Assignment assignment)
		{
			sol.Add(assignment);
			return sol;
		}

		private static List<List<Assignment>> EvaluateLogicalExpression(ParseTreeOperatorNode expression, string[] subwords, string body)
		{
			if (expression.getTerm() != null)
			{
				Term term = expression.getTerm();
				string[] freeVars = FOLSearcher.GetFreeVars(term);
				return FOLTopDownEvaluator.evaluateParsedExpression(term, subwords, freeVars, body);
			}
			else
			{
				List<List<Assignment>> leftTable = EvaluateLogicalExpression(expression.getLeftChild(), subwords, body);
				List<List<Assignment>> rightTable = EvaluateLogicalExpression(expression.getRightChild(), subwords, body);
				rightTable = FillInSolutions(rightTable, leftTable, subwords);
				leftTable = FillInSolutions(leftTable, rightTable, subwords);
				if (expression.getFolOperator() == Operator.Disjunction)
				{
					leftTable.AddRange(rightTable.Where(r => !leftTable.Any(l => CompareSolutions(l, r))));
				}
				else
				{
					leftTable.RemoveAll(l => !rightTable.Any(r => CompareSolutions(l, r)));
				}
				return leftTable;
			}
		}

		static List<List<Assignment>> FillInSolutions(List<List<Assignment>> left, List<List<Assignment>> right, string[] subwords)
		{
			string[] leftVars = FOLSearcher.GetFreeVars(left);
			string[] rightVars = FOLSearcher.GetFreeVars(right);

			string[] newLeftVars = rightVars.Where(r => !leftVars.Any(l => r == l)).ToArray();

			return FillInSolutions(left, newLeftVars, subwords);
		}

		static List<List<Assignment>> newFillInSolutions(List<List<Assignment>>left, string[] newFreeVars, string[] subwords)
		{
			return null;
		}

		static List<List<Assignment>> FillInSolutions(List<List<Assignment>> left, string[] newFreeVars, string[] subwords)
		{
			if (newFreeVars.Count() == 0)
			{
				return left;
			}
			foreach (string var in newFreeVars)
			{
				List<List<Assignment>> currentSolutions = new List<List<Assignment>>();
				//tick through each set of values for the freevars and add them with the current solution
				foreach (List<Assignment> solution in left)
				{
					foreach (string word in subwords)
					{
						List<Assignment> newSol = new List<Assignment>(solution);
						newSol.Add(new Assignment(var, word));
						currentSolutions.Add(newSol);
					}
				}
				left = currentSolutions;
			}
			return left;
		}

		public static List<List<Assignment>> GetSolutionSetDifference(List<List<Assignment>> left, List<List<Assignment>> right)
		{
			return left.Where(l => !right.Any(r => CompareSolutions(l, r))).ToList();
		}

		public static bool CompareSolutions(List<Assignment> left, List<Assignment> right)
		{
			bool res = left.Count == right.Count && left.All(l => right.Any(r => CompareAssignments(l, r)));
			return res;
		}
		public static bool CompareAssignments(Assignment left, Assignment right)
		{
			return left.GetValue() == right.GetValue() && left.GetVariableName() == right.GetVariableName();
		}
	}
}
