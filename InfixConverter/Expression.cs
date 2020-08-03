/*!	\file		Expression.cs
	\author		Group 13 (Haohan Liu, Dmytro Liaska, David Rivard)
	\date		2019-03-19

    Expression Object Properties and Methods
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace InfixConverter
{
    public class Expression
    {
        // Properties:
        public int ID { get; set; }         // Manditory for XML output
        public string InFix { get; set; }   // Expression we are converting to PreFix & PostFix
        public string PreFix { get; set; }  // Manditory for XML output
        public string PostFix { get; set; } // Manditory for XML output
        public double Evaluation { get; set; } // Manditory for XML output
        public bool Comparison { get; set; }// Manditory for XML output

        // Constructors:
        public Expression() { }
        
        public Expression(int id, string infix)
        {
            ID = id;
            InFix = infix;
        }

        // CONVERSION FROM INFIX TO PREFIX ALGORITHM:
        #region INFIX_TO_PREFIX_ALGORITHM
        public string ConverToPreFix(string infix)
        {

            // Convert to PostFix
            ConverToPostFix(infix);
            string postfix = PostFix;

            // Make an output variable:
            string output = "";

            Stack newExpression = new Stack();
            
            for (int i = 0; i < postfix.Length; i++)
            {

                // Check if character is operator:
                if (isOperator(postfix[i]))
                {
                    // Pop() two operands from the newExpression stack:
                    string operand1 = (string)newExpression.Peek();
                    newExpression.Pop();
                    string operand2 = (string)newExpression.Peek();
                    newExpression.Pop();

                    // Hold the sub-expression in a temporary string: 
                    string temp = postfix[i] + operand2 + operand1;

                    // Push String temp back to stack 
                    newExpression.Push(temp);
                }
                else // The character is an operand...
                {
                    // Push the operand to the newExpression stack 
                    newExpression.Push(postfix[i] + "");
                }
            }

            // newExpression[0] is holding the converted Prefix expression:
            output = (string)newExpression.Peek();

            return output;
        }

        // Returns true if the character is an Operator:
        public bool isOperator(char c)
        {
            switch(c)
            { 
                case '+': 
                case '-': 
                case '/': 
                case '*': 
            return true;
            }
            return false;
        }
        #endregion // INFIX_TO_PREFIX_ALGORITHM
        
        // CONVERSION FROM INFIX TO POSTFIX ALGORITHM:
        #region INFIX_TO_POSTFIX_ALGORITHM
        public void ConverToPostFix(string infix)
        {
            //Empty Variables:
            string output = "";
            Stack<char> newExpression = new Stack<char>();

            //Loop through each character in the string:
            for (int i = 0; i < infix.Length; ++i)
            {
                //Get the character value at the specified index number:
                char c = infix[i];

                // Check to see if the character is an operand. If so, add it to the output string:
                if (Char.IsLetterOrDigit(c))
                {
                    output += c;
                }
                else if (c == '(') // If the character is '(', push it to the newExpression.
                {
                    newExpression.Push(c);
                }
                else if (c == ')') //  If the character is ')'...
                {
                    // pop() from newExpression stack and add it to the output string until '(':
                    while (newExpression.Count != 0 && newExpression.Peek() != '(')
                    {
                        output += newExpression.Pop();
                    }

                    // If there was no other '(' found by the time the stack is empty, the expression was invalid:
                    if (newExpression.Count > 0 && newExpression.Peek() != '(')
                        Console.WriteLine("Invalid Expression");
                    else
                        newExpression.Pop();
                }
                else
                {
                    // Operator was found. Determine its priority:
                    // If the Priorty value of the character is less than or equal to the the Priority value
                    // of the last operator on the top of the stack...
                    while (newExpression.Count > 0 && Priority(c) <= Priority(newExpression.Peek()))
                    {
                        //Add the operator to the output string
                        output += newExpression.Pop();
                    }

                    //Otherwise, push it on to the newExpression stack:
                    newExpression.Push(c);
                }
            }

            // pop() all remaining characters/operators to the output string
            while (newExpression.Count > 0)
            {
                output += newExpression.Pop();
            }

            //Set the object'newExpression PostFix value to the output string:
            PostFix = output;
        }


        // Operator Prioritization: Returns an integer value representing the priority of a given operator;
        // the higher the value, the greater the priority.
        static int Priority(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;
            }
            return -1;
        }
        #endregion // INFIX_TO_POSTFIX_ALGORITHM

        // PREFIX EVALUATION:
        #region PREFIX_EVALUATOR
        public double PrefixEvaluate()
        {
            string prefix = PreFix;
            Stack<Double> Stack = new Stack<Double>();

            for (int j = prefix.Length - 1; j >= 0; j--)
            {

                // Push operand to Stack 
                // To convert exprsn[j] to digit subtract 
                // '0' from exprsn[j]. 
                if (isOperand(prefix[j]))
                    Stack.Push((double)(prefix[j] - 48));

                else
                {

                    // Operator encountered 
                    // Pop two elements from Stack 
                    double o1 = Stack.Peek();
                    Stack.Pop();
                    double o2 = Stack.Peek();
                    Stack.Pop();

                    // Use switch case to operate on o1  
                    // and o2 and perform o1 O o2. 
                    switch (prefix[j])
                    {
                        case '+':
                            Stack.Push(o1 + o2);
                            break;
                        case '-':
                            Stack.Push(o1 - o2);
                            break;
                        case '*':
                            Stack.Push(o1 * o2);
                            break;
                        case '/':
                            Stack.Push(o1 / o2);
                            break;
                    }
                }
            }

            return Stack.Peek();
        }

        private bool isOperand(char v)
        {
            if (v >= 48 && v <= 57)
                return true;
            else
                return false;
        }
        #endregion //PREFIX_EVALUATOR

        // PREFIX EVALUATION:
        #region POSTFIX_EVALUATOR
        public double PostfixEvaluate()
        {
            string postfix = PostFix;
            //create a stack 
            Stack<Double> stack = new Stack<Double>();

            // Scan all characters one by one 
            for (int i = 0; i < postfix.Length; i++)
            {
                char c = postfix[i];

                // If the scanned character is an operand (number here), 
                // push it to the stack. 
                if (Char.IsDigit(c))
                    stack.Push(c - '0');

                //  If the scanned character is an operator, pop two 
                // elements from stack apply the operator 
                else
                {
                    double val1 = stack.Pop();
                    double val2 = stack.Pop();

                    switch (c)
                    {
                        case '+':
                            stack.Push(val2 + val1);
                            break;

                        case '-':
                            stack.Push(val2 - val1);
                            break;

                        case '/':
                            stack.Push(val2 / val1);
                            break;

                        case '*':
                            stack.Push(val2 * val1);
                            break;
                    }
                }
            }
            return stack.Pop();
        }
        #endregion //PREFIX_EVALUATOR

    }// class Expression
}// namespace InfixConverter
