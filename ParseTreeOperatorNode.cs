using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
    class ParseTreeOperatorNode
    {

		Operator? folOperator = null;
        private ParseTreeOperatorNode leftChild = null;
        private ParseTreeOperatorNode rightChild = null;
		private Term term = null;

		public ParseTreeOperatorNode(Operator folOperator, ParseTreeOperatorNode leftChild, ParseTreeOperatorNode rightChild)
		{
			this.folOperator = folOperator;
			this.leftChild = leftChild;
			this.rightChild = rightChild;
		}

		public ParseTreeOperatorNode(Term term)
		{
			this.term = term;
		}

		public Term getTerm()
		{
			return term;
		}

		public ParseTreeOperatorNode getLeftChild()
		{
			return leftChild;
		}

		public ParseTreeOperatorNode getRightChild()
		{
			return rightChild;
		}

		public Operator? getFolOperator()
		{
			return folOperator;
		}
    }
}
