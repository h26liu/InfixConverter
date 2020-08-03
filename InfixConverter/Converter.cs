/*!	\file		Converter.cs
	\author		Group 13 (Haohan Liu, Dmytro Liaska, David Rivard)
	\date		2019-03-19

    Implementation for Read 
*/

using System;
using System.Collections.Generic;
using ChoETL;

namespace InfixConverter
{
    public class Converter
    {
        #region LIST_CONTAINERS
        //IList to hold all Expression objects from the .csv file:
        IList<Expression> expressions = new List<Expression>();

        //IList to hold all Infix expressions from each Expression object (manditory):
        IList<string> InFix = new List<string>();

        //Create a List to store converted PreFix expressions:
        IList<string> PreFix = new List<string>();

        //Create a Generic List to store converted PostFix expressions:
        IList<string> PostFix = new List<string>();

        //Create a Generic List to store evaluated PreFix expressions:
        IList<double> PreFixEvaluations = new List<double>();

        //Create a Generic List to store evaluated PostFix expressions:
        IList<double> PostFixEvaluations = new List<double>();

        //Create a Generic List to store boolean comparison values:
        IList<bool> Comparisons = new List<bool>();
        #endregion // LIST_CONTAINERS

        //Returns an IList of all InFix expressions:
        public IList<string> GetInFix()
        {
            foreach (var obj in expressions)
            {
                InFix.Add(obj.InFix);
            }
            return InFix;
        }


        //Returns an IList of all PreFix expressions:
        public IList<string> GetPreFix()
        {
            foreach (var obj in expressions)
            {
                //Set the object's PreFix to the converted PreFix string:
                obj.PreFix = obj.ConverToPreFix(obj.InFix);
                PreFix.Add(obj.PreFix);
            }
            return PreFix;
        }


        //Returns an IList of all PostFix expressions:
        public IList<string> GetPostFix()
        {
            foreach (var obj in expressions)
            {
                //Add the converted object's PostFix string to the PostFix List:
                PostFix.Add(obj.PostFix);
            }
            return PostFix;
        }

        //Returns an IList of Expression objects from the .csv file:
        public IList<Expression> ReadFile(string filename)
        {
            ReadCSV(filename);
            return expressions;
        }

        // Returns the List of PrefixEvaluations:
        public IList<double> GetPreFixEvaluations()
        {
            return PreFixEvaluations;
        }

        //Returns the List of PostFixEvaluations:
        public IList<double> GetPostFixEvaluations()
        {
            return PostFixEvaluations;
        }

        public IList<bool> GetComparisons()
        {
            return Comparisons;
        }

        /***************************************************************************************/

        //Reader for CSV file:
        #region READER
        // Read CSV
        private void ReadCSV(string filename)
        {
            try
            {
                Expression exp;
                using (var parser = new ChoCSVReader(filename).WithFirstLineHeader().WithDelimiter(","))
                {
                    foreach (var e in parser)
                    {
                        int id = (e.Count == 2) ? int.Parse(e[0]) : 0;
                        string expression = (e.Count == 2) ? e[1] : "";
                        exp = new Expression(id, expression);

                        expressions.Add(exp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        #endregion //READER

        /***************************************************************************************/

        // Evaluates each prefix and postfix expression, and compares them.
        #region EVALUATE_AND_COMPARE
        public void EvaluateAndCompare()
        {
            double prefixEval = 0;
            double postfixEval = 0;
            foreach (var obj in expressions)
            {
                // Evaluate the PreFix and PostFix expressions:
                prefixEval = obj.PrefixEvaluate();
                postfixEval = obj.PostfixEvaluate();

                // Add each to their respective List:
                PreFixEvaluations.Add(prefixEval);
                PostFixEvaluations.Add(postfixEval);

                //Compare them. If they are the same, set the object's properties:
                if (prefixEval == postfixEval)
                {
                    obj.Evaluation = prefixEval;
                    obj.Comparison = true;
                    Comparisons.Add(obj.Comparison);
                }
                else
                {
                    obj.Evaluation = -1;
                    obj.Comparison = false;
                    Comparisons.Add(obj.Comparison);
                }
            }
        }// End EvaluateAndCompare()
        #endregion EVALUATE_AND_COMPARE

    }// class Converter
}// namespace InfixConverter
