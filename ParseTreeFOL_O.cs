using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator___UI
{
    class ParseTreeFOL_O
    {

		FOLOperator? folOperator = null;
        private ParseTreeFOL_O leftChild = null;
        private ParseTreeFOL_O rightChild = null;
		private Term term = null;

		public ParseTreeFOL_O(FOLOperator folOperator, ParseTreeFOL_O leftChild, ParseTreeFOL_O rightChild)
		{
			this.folOperator = folOperator;
			this.leftChild = leftChild;
			this.rightChild = rightChild;
		}

		public ParseTreeFOL_O(Term term)
		{
			this.term = term;
		}

		public Term getTerm()
		{
			return term;
		}

		public ParseTreeFOL_O getLeftChild()
		{
			return leftChild;
		}

		public ParseTreeFOL_O getRightChild()
		{
			return rightChild;
		}

		public FOLOperator? getFolOperator()
		{
			return folOperator;
		}
    }
}
