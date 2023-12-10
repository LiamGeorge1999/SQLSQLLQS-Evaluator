using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_FC_Evaluator
{
	class Assignment : IEquatable<Assignment>
	{
		private readonly string variableName;
		private readonly string value;

		public Assignment(string variableName, string value)
		{
			this.variableName = variableName;
			this.value = value;
		}
		public string GetVariableName()
		{
			return variableName;
		}
		public string GetValue()
		{
			return value;
		}

		public override bool Equals(object obj)
		{
			return obj is Assignment assignment &&
				   variableName == assignment.variableName &&
				   value == assignment.value;
		}

		public override int GetHashCode()
		{
			int hashVariableName = variableName == null ? 0 : variableName.GetHashCode();
			int hashValueName = value == null ? 0 : value.GetHashCode();
			return hashVariableName ^ hashValueName;
		}
		public bool Equals(Assignment other)
		{ 
			if (other is null) return false;
			if (Object.ReferenceEquals(this, other)) return true;
			return variableName.Equals(other.variableName) && value.Equals(other.value);
		}
	}
}
