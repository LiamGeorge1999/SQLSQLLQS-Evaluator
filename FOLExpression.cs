using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator___UI
{
	class FOLExpression
	{
		ParseTreeFOL_Q FOQuantifiers;
		ParseTreeFOL_O PLExpression;

		public FOLExpression(ParseTreeFOL_Q quantifiers, ParseTreeFOL_O expression)
		{
			this.FOQuantifiers = quantifiers;
			this.PLExpression = expression;
		}
		

	}
}
