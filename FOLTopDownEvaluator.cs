﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	class FOLTopDownEvaluator
	{
		public static List<List<Assignment>> evaluateParsedExpression(Term term, string[] subwords, string[] freeVars, string body)
		{
			return evaluateParsedExpression(new ParseTreeOperatorNode(term), subwords, freeVars, body);
		}

		public static List<List<Assignment>> evaluateParsedExpression(ParseTreeOperatorNode expression, string[] subwords, string[] freeVars, string body)
		{
			return evaluateParsedExpression(new ParseTreeQuantifierNode(null, null, expression), subwords, freeVars, body);
		}

		public static List<List<Assignment>> evaluateParsedExpression(ParseTreeQuantifierNode parsedExpression, string[] subwords, string[] freeVars,  string body)
		{
			int[] freeVarValIndices = new int[freeVars.Length];
			for (int i = 0; i < freeVarValIndices.Length; i++)
			{
				freeVarValIndices[i] = 0;
			}
			List<List<Assignment>> successfulValueCombinations = new List<List<Assignment>>();
			do
			{
				List<Assignment> varVals = new List<Assignment> { new Assignment("S", body) };
				for (int i = 0; i < freeVarValIndices.Length; i++)
				{
					Assignment varVal = new Assignment(freeVars[i], subwords[freeVarValIndices[i]]);
					varVals.Add(varVal);
				}
				if (evaluateQuantifiedExpression(parsedExpression, subwords.ToArray(), varVals))
				{
					successfulValueCombinations.Add(varVals);
				}
				freeVarValIndices[freeVarValIndices.Length - 1]++;
				for (int i = freeVarValIndices.Length - 1; i > 0; i--)
				{
					if (freeVarValIndices[i] >= subwords.Count())
					{
						freeVarValIndices[i] = 0;
						freeVarValIndices[i - 1]++;
					}
				}
			} while (freeVarValIndices[0] != subwords.Count());
			successfulValueCombinations = FOLSearcher.removeVariable(successfulValueCombinations, "S");
			return successfulValueCombinations;
		}

		public static bool evaluateQuantifiedExpression(ParseTreeQuantifierNode quantifiedExpression, string[] subwords, List<Assignment> varVals)
		{
			if (quantifiedExpression.getExpression() != null)
			{
				return evaluateLogicalExpression(quantifiedExpression.getExpression(), varVals);
			}
			bool res = false;
			bool endState = false;

			//handles the difference between "There Exists" and "For All" quantifiers
			if (quantifiedExpression.getFOLQuantifier() == Quantifier.E)
			{
				endState = true;
			}
			else
			{
				res = true;
			}

			//until we've tried all subwords, or the result proves the quantifier true or false
			for (int j = 0; j < subwords.Length && res != endState; j++)
			{
				List<Assignment> newVarVals = new List<Assignment>();
				newVarVals.AddRange(varVals);
				Assignment varVal = new Assignment(quantifiedExpression.getQuantifiedVar(), subwords[j]);
				newVarVals.Add(varVal);

				res = evaluateQuantifiedExpression(quantifiedExpression.getChild(), subwords, newVarVals);
			}
			return res;
		}

			private static bool evaluateLogicalExpression(ParseTreeOperatorNode expressionTree, List<Assignment> varVals)
			{
				Operator? op = expressionTree.getFolOperator();
				if (expressionTree.getTerm() != null)
				{
					return TermEvaluator.evaluateTerm(expressionTree.getTerm(), varVals);
				}
				else
				{
					if (op == Operator.Conjunction)
					{
						return evaluateLogicalExpression(expressionTree.getLeftChild(), varVals)
							&& evaluateLogicalExpression(expressionTree.getRightChild(), varVals);
					}
					else if (op == Operator.Disjunction)
					{
						return evaluateLogicalExpression(expressionTree.getLeftChild(), varVals)
							|| evaluateLogicalExpression(expressionTree.getRightChild(), varVals);
					}
				}
				throw (new Exception("Bad expression parsing"));
			}
		}
}
