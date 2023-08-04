using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public class MathFormula
    {

       



        #region Static Members

        /// <summary>
        /// Constant for left association symbols
        /// </summary>
        private static readonly int LEFT_ASSOC = 0;

        /// <summary>
        /// Constant for right association symbols
        /// </summary>
        private static readonly int RIGHT_ASSOC = 1;

        /// <summary>
        /// Static list of operators in the _formula
        /// </summary>
        private static readonly Dictionary<String, int[]> Operators = new Dictionary<string, int[]>();

        /// <summary>
        /// Static constructor.
        /// </summary>
        static MathFormula()
        {
            Operators.Add("+", new[] { 0, LEFT_ASSOC });
            Operators.Add("-", new[] { 0, LEFT_ASSOC });
            Operators.Add("*", new[] { 5, LEFT_ASSOC });
            Operators.Add("/", new[] { 5, LEFT_ASSOC });
            Operators.Add("%", new[] { 5, LEFT_ASSOC });
            Operators.Add("^", new[] { 10, RIGHT_ASSOC });
        }

        /// <summary>
        /// Static method to check if a token is an operator.
        /// </summary>
        /// <param name="token">The token we want to check.</param>
        /// <returns>True if it is an operator, else false.</returns>
        private static bool IsOperator(String token)
        {
            return Operators.ContainsKey(token);
        }

        /// <summary>
        /// Static method to check if the type of operation is associative (left or right).
        /// </summary>
        /// <param name="token">The token operator.</param>
        /// <param name="type">The type of association (left or right).</param>
        /// <returns>True if it's associative, else false.</returns>
        private static bool IsAssociative(String token, int type)
        {
            if (!IsOperator(token))
                throw new ArgumentException("Invalid token: " + token);

            if (Operators[token][1] == type)
                return true;

            return false;
        }

        /// <summary>
        /// Static method to compare operator precendece.
        /// </summary>
        /// <param name="token1">First operator.</param>
        /// <param name="token2">Second operator.</param>
        /// <returns>The value of precedence between the two operators.</returns>
        private static int ComparePrecedence(String token1, String token2)
        {
            if (!IsOperator(token1) || !IsOperator(token2))
                throw new ArgumentException("Invalid token: " + token1 + " " + token2);

            return Operators[token1][0] - Operators[token2][0];
        }

        /// <summary>
        /// Static method to transfor a normal _formula into an RPN _formula.
        /// </summary>
        /// <param name="input">The normal infix _formula.</param>
        /// <returns>The RPN _formula.</returns>
        public static String InfixToRpn(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (IsOperator(input[i].ToString()) || input[i] == '(' || input[i] == ')')
                {
                    if (i != 0 && input[i - 1] != ' ')
                        input = input.Insert(i, " ");
                    if (i != input.Length - 1 && input[i + 1] != ' ')
                        input = input.Insert(i + 1, " ");
                }
            }

            input = input.Trim();

            String[] inputTokens = input.Split(' ');
            List<string> outList = new List<string>();
            Stack<string> stack = new Stack<string>();

            foreach (string token in inputTokens)
            {
                if (token == " " || token == String.Empty)
                    continue;

                if (IsOperator(token))
                {
                    while (stack.Count != 0 && IsOperator(stack.Peek()))
                    {
                        if ((IsAssociative(token, LEFT_ASSOC) && ComparePrecedence(token, stack.Peek()) <= 0) ||
                            (IsAssociative(token, RIGHT_ASSOC) && ComparePrecedence(token, stack.Peek()) < 0))
                        {
                            outList.Add(stack.Pop());
                            continue;
                        }
                        break;
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count != 0 && stack.Peek() != "(")
                        outList.Add(stack.Pop());
                    stack.Pop();
                }
                else
                {
                    outList.Add(token);
                }
            }

            while (stack.Count != 0)
                outList.Add(stack.Pop());

            return String.Join(" ", outList.ToArray());
        }

        /// <summary>
        /// Static method to handle a single operation between two operands.
        /// </summary>
        /// <param name="val1">First operand.</param>
        /// <param name="val2">Second operand.</param>
        /// <param name="op">Operator.</param>
        /// <returns>The result of the operation.</returns>
        private static decimal DoOperation(decimal val1, decimal val2, string op)
        {
            switch (op)
            {
                case "*":
                    return val1 * val2;
                case "/":
                    return val1 / val2;
                case "%":
                    return (int)val1 % (int)val2;
                case "+":
                    return val1 + val2;
                case "-":
                    return val1 - val2;
                case "^":
                    return (decimal)Math.Pow((double)val1, (double)val2);
                default:
                    return 0;
            }
        }




        #endregion

        /// <summary>
        /// Environment for all the variables in the _formula.
        /// </summary>
        private readonly Dictionary<string, decimal> _environment = new Dictionary<string, decimal>();

        /// <summary>
        /// Written _formula in normal form.
        /// </summary>
        private string _formula;

        /// <summary>
        /// Written _formula in Reverse Polish Notation form.
        /// </summary>
        private string _rpnFormula;

        /// <summary>
        /// Value of a parameter in the _environment.
        /// </summary>
        /// <param name="param">Parameter name.</param>
        /// <returns>Value of the parameter.</returns>
        public decimal this[string param]
        {
            get
            {
                return _environment[param];
            }
            set
            {
                _environment[param] = value;
            }
        }

        /// <summary>
        /// Value of the _formula.
        /// </summary>
        public decimal Value
        {
            get
            {
          
                return Calculate();
            }
        }

        

        /// <summary>
        /// Method use to calculate the value of the _formula.
        /// </summary>
        /// <returns>The floating point value of the _formula.</returns>
        private decimal Calculate()
        {
            String[] tokens = _rpnFormula.Split(' ');
            Stack<decimal> values = new Stack<decimal>();

            foreach (string token in tokens)
            {
                if (!Operators.ContainsKey(token))
                {
                    decimal value;
                    if (decimal.TryParse(token, out value))
                        values.Push(decimal.Parse(token, System.Globalization.CultureInfo.InvariantCulture));
                    else
                        values.Push(_environment[token]);
                }
                else
                {
                    decimal val1 = values.Pop();
                    decimal val2 = values.Pop();
                    values.Push(DoOperation(val2, val1, token));
                }
            }

            if (values.Count != 1)
                throw new InvalidOperationException("Cannot calculate _formula.");

            return values.Pop();
        }



        /// <summary>
        /// String representation of the _formula.
        /// </summary>
        public string Formula
        {
            get
            {
                return _formula;
            }
            set
            {
                _rpnFormula = InfixToRpn(value);
                _formula = value;
            }
        }

        /// <summary>
        /// String representation of the _formula.
        /// </summary>
        /// <returns>The string _formula.</returns>
        public override string ToString()
        {
            return _formula;
        }

        /// <summary>
        /// CTor.
        /// </summary>
        /// <param name="formula">Infix Notation of the _formula.</param>
        public MathFormula(string formula)
        {
            this._formula = formula;
            _rpnFormula = InfixToRpn(formula);
        }

        /// <summary>
        /// Adds a parameter (variable) to the _environment.
        /// </summary>
        /// <param name="param">Parameter name.</param>
        /// <param name="value">Paramatere value.</param>
        public void AddParameter(string param, decimal value)
        {
            _environment.Add(param, value);
        }

        /// <summary>
        /// Checks if the _environment contains the passed parameter.
        /// </summary>
        /// <param name="param">Name of the parameter.</param>
        /// <returns>True if it contains the parameter, else false.</returns>
        public bool ContainsParameter(string param)
        {
            return _environment.ContainsKey(param);
        }
    }

}
