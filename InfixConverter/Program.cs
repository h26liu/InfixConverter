/*!	\file		Program.cs
	\author		Group 13 (Haohan Liu, Dmytro Liaska, David Rivard)
	\date		2019-03-19

    Main() implementation
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InfixConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            //List declarations:
            #region LIST_CONTAINERS
            //IList to hold all Expression objects:
            IList<Expression> expressions = new List<Expression>();

            //IList to hold all InFix strings:
            IList<string> InFix = new List<string>();

            //IList to hold all converted PreFix strings:
            IList<string> PreFix = new List<string>();

            //IList to hold all converted PostFix strings:
            IList<string> PostFix = new List<string>();

            //Create a Generic List to store evaluated PreFix evaluations:
            IList<double> PreFixEvaluations = new List<double>();

            //Create a Generic List to store evaluated PostFix evaluations:
            IList<double> PostFixEvaluations = new List<double>();

            //Create a Generic List to store boolean comparison values:
            IList<bool> Comparisons = new List<bool>();
            #endregion //LIST_CONTAINERS

            //PROGRAM:
            /***************************************************************************************************/
            string inputYN = "";

            //User Prompt: If 'N', the program will shut down after 2 seconds.
            Console.Write("Would you like the program to read in 'Project 2_INFO_5101'? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION
            while (!inputYN.ToLower().Equals("y"))
            {
                if(inputYN.ToLower().Equals("n"))
                {
                    Console.WriteLine("Closing program...");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION

            //Get the file name: (Located in the "\InfixConverter\InfixConverter\bin\Debug" folder by default)
            string filename = "Project 2_INFO_5101.csv";

            //Create a new object of the Converter class to access its methods:
            Converter converter = new Converter();
            
            //Fill expressions List with the objects read from the .csv file:
            expressions = converter.ReadFile(filename);
            if(expressions.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("'{0}' has been read successfully.", filename);
                Console.WriteLine();
            }

            //INFIX OUTPUT:
            /***************************************************************************************************/
            //Get the List of InFix string values:
            InFix = converter.GetInFix();

            //Prompt to output converted PreFix strings from PreFix List: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the InFix expressions? (Y/N): ");
            inputYN = Console.ReadLine();
            
            #region INPUT_VALIDATION_INFIX
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (InFix.Count != 0)
                    {
                        Console.WriteLine("'InFix' List successfully filled with {0} expressions.", InFix.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'InFix' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_PREFIX

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var expression in InFix)
                {
                    Console.WriteLine(expression);
                }
                Console.WriteLine();
            }

            //PREFIX OUTPUT:
            /***************************************************************************************************/
            //Get the List of converted PreFix string values:
            PreFix = converter.GetPreFix();

            //Prompt to output converted PreFix strings from PreFix List: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the converted PreFix expressions? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_PREFIX
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (PreFix.Count != 0)
                    {
                        Console.WriteLine("'PreFix' List successfully filled with {0} expressions.", PreFix.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'PreFix' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_PREFIX

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var expression in PreFix)
                {
                    Console.WriteLine(expression);
                }
                Console.WriteLine();
            }

            //POSTFIX OUTPUT:
            /***************************************************************************************************/

            //Get the List of converted PreFix string values:
            PostFix = converter.GetPostFix();

            //Prompt to output converted PostFix strings from PreFix List: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the converted PostFix expressions? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_POSTFIX
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (PostFix.Count != 0)
                    {
                        Console.WriteLine("'PostFix' List successfully filled with {0} expressions.", PostFix.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'PostFix' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_PREFIX

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var expression in PostFix)
                {
                    Console.WriteLine(expression);
                }
                Console.WriteLine();
            }

            //EVALUATE PREFIX AND POSTFIX EXPRESSIONS:
            /***************************************************************************************************/
            //Perform the Evaluation of both Prefix and Postfix expressions.
            converter.EvaluateAndCompare();

            //Get the PreFix Evaluation List:
            PreFixEvaluations = converter.GetPreFixEvaluations();

            //Prompt to output Evaluated PreFix solutions from PreFixEvaluations List: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the evaluated PreFix expressions? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_PREFIX_EVALUATION
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (PreFixEvaluations.Count != 0)
                    {
                        Console.WriteLine("'PreFixEvaluations' List successfully filled with {0} expressions.", PreFixEvaluations.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'PreFixEvaluations' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_PREFIX_EVALUATION

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var solution in PreFixEvaluations)
                {
                    Console.WriteLine(solution);
                }
                Console.WriteLine();
            }

            //Get the PostFix Evaluation List:
            PostFixEvaluations = converter.GetPostFixEvaluations();

            //Prompt to output Evaluated PostFix solutions from PostFixEvaluations List: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the evaluated PostFix expressions? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_POSTFIX_EVALUATION
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (PostFixEvaluations.Count != 0)
                    {
                        Console.WriteLine("'PostFixEvaluations' List successfully filled with {0} expressions.", PostFixEvaluations.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'PostFixEvaluations' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_PREFIX_EVALUATION

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var solution in PostFixEvaluations)
                {
                    Console.WriteLine(solution);
                }
                Console.WriteLine();
            }

            //Get the list of Comparison booleans:
            Comparisons = converter.GetComparisons();

            //Prompt to output Compared PreFix and PostFix solutions: If 'N', the program will continue without the output.
            inputYN = "";
            Console.Write("Would you like to output the boolean Comparison results of the PreFix and PostFix evaluations? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_COMPARE
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    if (Comparisons.Count != 0)
                    {
                        Console.WriteLine("'Comparisons' List successfully filled with {0} expressions.", PostFixEvaluations.Count);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("'Comparisons' List is empty. Continuing program...");
                        break;
                    }
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_COMPARE

            //Output List:
            if (inputYN.ToLower().Equals("y"))
            {
                foreach (var boolean in Comparisons)
                {
                    Console.WriteLine(boolean);
                }
                Console.WriteLine();
            }

            //GENERATE XML FILE:
            /***************************************************************************************************/
            #region GENERATE_XML

            inputYN = "";
            Console.Write("Would you like to generate an XML file? (Y/N): ");
            inputYN = Console.ReadLine();

            #region INPUT_VALIDATION_XML_GENERATE
            while (!inputYN.ToLower().Equals("y"))
            {
                if (inputYN.ToLower().Equals("n"))
                {
                    Console.WriteLine("Closing program...");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Please choose 'Y' or 'N': ");
                    inputYN = Console.ReadLine();
                }
            }
            #endregion //INPUT_VALIDATION_XML_GENERATE

            // prompt user to input a name for xml file
            Console.Write("Please enter a name for your XML file: ");
            string inputName = Console.ReadLine();

            #region INPUT_VALIDATION_XML_NAME
            while (inputName.Length == 0 || inputName == null || !inputName.All(char.IsLetterOrDigit))
            {
                Console.Write("Invalid name input, please choose another name: ");
                inputName = Console.ReadLine();
            }
            #endregion //INPUT_VALIDATION_XML_NAME

            string xmlfilename = inputName + ".xml";
            string filepath = "../../../../InfixConverter/" + xmlfilename;

            try
            {
                // clear the xml file with empty string
                File.WriteAllText(xmlfilename, String.Empty);

                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    // write version number and encoding
                    writer.WriteStartDocument();

                    writer.WriteStartRootElement();

                    foreach (var expression in expressions)
                    {
                        // write start element
                        writer.WriteStartElement();

                        // write attributes
                        writer.WriteAttribute("sno", expression.ID.ToString());
                        writer.WriteAttribute("infix", expression.InFix);
                        writer.WriteAttribute("prefix", expression.PreFix);
                        writer.WriteAttribute("postfix", expression.PostFix);
                        writer.WriteAttribute("evaluation", expression.Evaluation.ToString());
                        writer.WriteAttribute("comparison", expression.Comparison.ToString());

                        // write end element 
                        writer.WriteEndElement();
                    }
                    writer.WriteEndRootElement();

                    // close stream writter
                    writer.Close();

                    Console.WriteLine("Successfully generated XML file!\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to generate XML file, " + ex.Message);
            }
            #endregion

            Console.WriteLine("Closing program...");
            System.Threading.Thread.Sleep(2000);
            Environment.Exit(0);

        }// End Main()
    }// class Program
}// namespace InfixConverter
