using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
	public enum OperatorType
	{
		Multiply,
		Divide,
		Add,
		Subtract,
		Invalid
	}
	public enum GrouperType
	{
		Open, Close
	}

	public interface ICalculator
	{
		double Add(double value1, double value2);
		double Subtract(double value1, double value2);
		double Multiply(double value1, double value2);
		double Divide(double value1, double value2);

	}

	//Expression represents a collection of operators, numbers, and grouping symbols (parentheses)
	public class Expression : ICalculator
	{
		List<ExpressionMember> Equation;
		public Expression(string input) 
		{
			Equation = new List<ExpressionMember>();
			while(input != string.Empty)
			{
				Equation.Add(popMemberFromString(input, out input));
			}
		}

		//removes and returns the first expressionmember from the string 
		private ExpressionMember popMemberFromString(string input, out string output)
		{
			string test = string.Empty;
			OperatorType opType;
			int index = 0; 
			foreach(char c in input) //multiple iterations only needed for Number type
			{
				if (c == '(' || c == ')' && test == string.Empty) //test will not be empty if it is storing part of a number
				{
					index++;
					output = pop(input, index);
					return new Grouper(c);
				}
				if ((opType = Operator.toOperator(c.ToString())) != OperatorType.Invalid  && test == string.Empty)
				{
					index++;
					output = pop(input, index);
					return new Operator(opType);
				}
				if (Number.IsNumberMember(c))
				{
					test += c;
					index++;
					if (index == input.Length || !Number.IsNumberMember(input[index])) //check if the next character is the end of the string or not another digit of the number
					{
						output = pop(input, index);
						return new Number(test);
					}
				}
			}
			output = string.Empty;
			return null;
		}

		//used by popMemberFromString, basically just substring
		public static string pop(string input, int length)
		{
			if (input.Length > length)
			{
				return input.Substring(length);
			}
			return string.Empty;
		}
		public override string ToString()
		{
			string output = string.Empty;
			foreach(var m in Equation)
			{
				output += m.ToString();
			}
			return output;
		}

		//begin spaghetti code
		public double Solve()
		{
			double result = 0;
			bool containsGroupers = Equation.OfType<Grouper>().Any();
			while (containsGroupers)
			{
				int innermostGroupStart;
				int innermostGroupEnd;
				innermostGroup(out innermostGroupStart, out innermostGroupEnd);
				result = evaluateSection(innermostGroupStart+1, innermostGroupEnd);
				replaceSection(Equation, innermostGroupStart, innermostGroupEnd, new Number(result));
				containsGroupers = Equation.OfType<Grouper>().Any();
			}
			bool containsOperators = Equation.OfType<Operator>().Any();
			while (containsOperators)
			{	
				result = evaluateSection(0, Equation.Count);
				replaceSection(Equation, 0, Equation.Count-1, new Number(result));
				containsOperators = Equation.OfType<Operator>().Any();
			}
			return result;
		}
		//gets the start and end indices of the innermost parentheses group
		private void innermostGroup(out int start,  out int end)
		{
			start = end = 0;
			for (int i = 0; i < Equation.Count; i++)
			{
				ExpressionMember test = Equation[i];
				if (test is Grouper)
				{
					if (test.ExpressionType() == 0) //if grouper is opener
					{
						start = i;
					} 
					if (test.ExpressionType() == 1) //if grouper is closer
					{
						end = i;
						return;
					}
				}
			}
		}

		//performs all operations in the specified section
		//SECTION CANNOT CONTAIN PARENTHESES
		private double evaluateSection(int start, int end)
		{
			List<ExpressionMember> section = Equation.GetRange(start, end - start);
			while (section.OfType<Operator>().Any())
			{
				int currentOperatorIndex = locationOfHighestPrecedentOperator(section);
				Operator op = (Operator)section[currentOperatorIndex];
				double result = op.Evaluate(section[currentOperatorIndex - 1].GetValue(), section[currentOperatorIndex + 1].GetValue());
				replaceSection(section, currentOperatorIndex - 1, currentOperatorIndex + 1, new Number(result));
			}
			return section[0].GetValue();
		}
		//replaces the specified contents of a list with a new item
		private void replaceSection(List<ExpressionMember> section, int start, int end, ExpressionMember replacement)
		{
			int length = end - start;
			for (int i = 0; i <= length; i++)
			{
				section.RemoveAt(start);
			}
			section.Insert(start, replacement);
		}

		//title is pretty self explanatory
		private static int locationOfHighestPrecedentOperator(List<ExpressionMember> section)
		{
			int index = 0;
			int highestPrecedent = int.MaxValue;
			for (int i = 0; i < section.Count; i++)
			{
				ExpressionMember test = section[i];
				if (test is Operator)
				{
					if (test.ExpressionType() < highestPrecedent)
					{
						highestPrecedent = test.ExpressionType();
						index = i;
					}
				}
			}
			return index;
		}
		//I literally didn't even use these because it didn't make sense with the way I structured the solution
		public double Add(double value1, double value2)
		{
			return value1 + value2;
		}

		public double Subtract(double value1, double value2)
		{
			return value1 - value2;
		}

		public double Multiply(double value1, double value2)
		{
			return value1 * value2;
		}

		public double Divide(double value1, double value2)
		{
			return value1 / value2;
		}
	}

	
	//ExpressionMember is a general class representing entities within an expression
	public abstract class ExpressionMember
	{
		public abstract int ExpressionType();
		public abstract double GetValue();
	}
	public class Operator : ExpressionMember
	{
		OperatorType Type { get; set; }

		public Operator(OperatorType type)
		{
			this.Type = type;
		}
		
		public override string ToString()
		{
			switch (Type)
			{
				case OperatorType.Add:return "+";
				case OperatorType.Subtract: return "-";
				case OperatorType.Multiply: return "*";
				case OperatorType.Divide: return "/";
			}
			return "";
		}
		public static OperatorType toOperator(string test)
		{
			switch (test)
			{
				case "+":
					return OperatorType.Add;
				case "-":
					return OperatorType.Subtract;
				case "*":
					return OperatorType.Multiply;
				case "/":
					return OperatorType.Divide;
			}
			return OperatorType.Invalid;
		}

		public override int ExpressionType()
		{
			return (int)this.Type;
		}
		public double Evaluate(double value1, double value2)
		{
			switch (Type)
			{
				case OperatorType.Add: return value1 + value2;
				case OperatorType.Subtract: return value1 - value2;
				case OperatorType.Multiply: return value1 * value2;
				case OperatorType.Divide: return value1 / value2;
			}
			return 0;
		}
		//hopefully this never gets called
		public override double GetValue()
		{
			throw new NotImplementedException();
		}
	}
	public class Number : ExpressionMember
	{
		double Value { get; set; }
		string ValueString { get; set; }
		bool ContainsDecimal { get; }
		public Number(string input)
		{
			Value = double.Parse(input);
		}

		public Number(double result)
		{
			this.Value = result;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
		public static bool IsNumberMember(char c)
		{
			return (c >= '0' && c <= '9') || (c == '.');
		}
		public override int ExpressionType()
		{
			return -1;
		}

		public override double GetValue()
		{
			return this.Value;
		}
	}
	public class Grouper : ExpressionMember
	{
		public GrouperType Type { get; set; }
		public Grouper(char c)
		{
			switch (c)
			{
				case '(':
					Type = GrouperType.Open; break;
				case ')':
					Type = GrouperType.Close; break;
			}
		}
		public override string ToString()
		{
			switch (Type)
			{
				case GrouperType.Open: return "(";
				case GrouperType.Close: return ")";
			}
			return "";
		}
		public override int ExpressionType()
		{
			return (int)this.Type;
		}

		public override double GetValue()
		{
			throw new NotImplementedException();
		}
	}
}
