using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator___UI
{
    class ParseTreeFOL_Q
    {

        FOLQuantifier? folQuantifier;
		string quantifiedVar;
        private ParseTreeFOL_Q child = null;
		private ParseTreeFOL_O expression = null;

		public ParseTreeFOL_Q(FOLQuantifier? folquantifier, string quantifiedVar, ParseTreeFOL_Q child)
		{
			this.folQuantifier = folquantifier;
			this.quantifiedVar = quantifiedVar;
			this.child = child;
		}

		public ParseTreeFOL_Q(FOLQuantifier? folquantifier, string quantifiedVar, ParseTreeFOL_O expression)
		{
			this.folQuantifier = folquantifier;
			this.quantifiedVar = quantifiedVar;
			this.expression = expression;
		}

		public ParseTreeFOL_Q getChild()
		{
			return child;
		}
		public string getQuantifiedVar()
		{
			return quantifiedVar;
		}
		public FOLQuantifier? getFOLQuantifier()
		{
			return folQuantifier;
		}
		public ParseTreeFOL_O getExpression()
		{
			return expression;
		}
	}
}
