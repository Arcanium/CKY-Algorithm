using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYK_Algorithm
{
    static class CYKAlgorithm
    {
        public static void ParseGrammar(string sentence, Dictionary<string, List<string>> grammars)
        {
            string[] sentencePieces = sentence.ToLower().Split(' ');

            List<string>[,] CYKResults = new List<string>[sentencePieces.Length, sentencePieces.Length];
            for (int i = 0; i < sentencePieces.Length; i++)
                for (int j = 0; j < sentencePieces.Length; j++)
                    CYKResults[i, j] = new List<string>();

            //Terminals.
            for (int i = 0; i < sentencePieces.Length; i++)
            {
                foreach (KeyValuePair<string, List<string>> grammar in grammars)
                {
                    List<string> grammarProduction = grammar.Value;
                    bool terminalMatchFound = false;

                    grammarProduction.ForEach(production =>
                    {
                        if (production == sentencePieces[i])
                            terminalMatchFound = true;
                    });

                    if (terminalMatchFound && !CYKResults[i, 0].Contains(grammar.Key))
                        CYKResults[0, i].Add(grammar.Key);
                }
            }

            //Non-terminals.
            for (int i = 1; i < sentencePieces.Length; i++) //i and j traverse the matrix, following the CYK path.
            {
                for (int j = 0; j < sentencePieces.Length - i; j++)
                {
                    int firstIndex = i - 1;
                    int secondIndex = j + 1;

                    for (int k = 0; k < i; k++) //k is used to traverse which two items to check for the grammar.
                    {
                        List<string> result1 = new List<string>(CYKResults[k, j]); //To remove the existing reference from the result to the CYK (it was messing up the lambda foreach).
                        List<string> result2 = new List<string>(CYKResults[firstIndex--, secondIndex++]);

                        foreach (KeyValuePair<string, List<string>> grammar in grammars)
                        {
                            if (grammar.Value.Count == 2) //Only looking for non-terminals.
                            {
                                result1.ForEach(production =>
                                {
                                    result2.ForEach(production2 =>
                                    {
                                        if (grammar.Value[0] == production && grammar.Value[1] == production2 && !CYKResults[i, j].Contains(grammar.Key))
                                            CYKResults[i, j].Add(grammar.Key);
                                    });
                                });
                            }
                        }
                    }
                }
            }
        }


    }
}
