using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	class FOLExpression
	{
		ParseTreeQuantifierNode FOQuantifiers;
		ParseTreeOperatorNode PLExpression;

		public FOLExpression(ParseTreeQuantifierNode quantifiers, ParseTreeOperatorNode expression)
		{
			this.FOQuantifiers = quantifiers;
			this.PLExpression = expression;
		}
		

	}
}
