using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	class Term
	{
		string[] lhs;
		string[] rhs;
		StringLogicComparator comparator;

		public Term(string[] lhs, string[] rhs, StringLogicComparator comparator)
		{
			this.lhs = lhs;
			this.rhs = rhs;
			this.comparator = comparator;
		}
		public string[] getLhs()
		{
			return lhs;
		}
		public string[] getRhs()
		{
			return rhs;
		}
		public StringLogicComparator getComparator()
		{
			return comparator;
		}
	}
}
