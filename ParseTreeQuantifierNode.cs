using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
    class ParseTreeQuantifierNode
    {

        Quantifier? folQuantifier;
		string quantifiedVar;
        private ParseTreeQuantifierNode child = null;
		private ParseTreeOperatorNode expression = null;

		public ParseTreeQuantifierNode(Quantifier? folquantifier, string quantifiedVar, ParseTreeQuantifierNode child)
		{
			this.folQuantifier = folquantifier;
			this.quantifiedVar = quantifiedVar;
			this.child = child;
		}

		public ParseTreeQuantifierNode(Quantifier? folquantifier, string quantifiedVar, ParseTreeOperatorNode expression)
		{
			this.folQuantifier = folquantifier;
			this.quantifiedVar = quantifiedVar;
			this.expression = expression;
		}

		public ParseTreeQuantifierNode getChild()
		{
			return child;
		}
		public string getQuantifiedVar()
		{
			return quantifiedVar;
		}
		public Quantifier? getFOLQuantifier()
		{
			return folQuantifier;
		}
		public ParseTreeOperatorNode getExpression()
		{
			return expression;
		}
	}
}
