using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab
{
    public enum ChepinVariableType
    { P, M, C, T }

    public class ChepinFullData
    {
        public List<string> PVars;
        public List<string> MVars;
        public List<string> CVars;
        public List<string> TVars;

        public ChepinFullData()
        {
            PVars = new List<string>() { };
            MVars = new List<string>() { };
            CVars = new List<string>() { };
            TVars = new List<string>() { };
        }

        public void AddVariable(string variable, ChepinVariableType type)
        {
            while (true)
            {
                if (variable.Length > 0)
                {
                    if (variable[0] == '\r' || variable[0] == '\n' || variable[0] == ' ' || variable[0] == '\t')
                        variable = variable.Remove(0, 1);
                    else
                        break;
                }
                else
                    return;
            }
            while (true)
            {
                if (variable.Length > 0)
                {
                    if (variable[variable.Length - 1] == '\r' || variable[variable.Length - 1] == '\n' || variable[variable.Length - 1] == ' ' || variable[variable.Length - 1] == '\t')
                        variable = variable.Remove(variable.Length - 1, 1);
                    else
                        break;
                }
                else
                    return;
            }

            switch (type)
            {
                case ChepinVariableType.P:
                    if (!PVars.Contains(variable))
                        PVars.Add(variable);
                    break;
                case ChepinVariableType.M:
                    if (!MVars.Contains(variable))
                        MVars.Add(variable);
                    break;
                case ChepinVariableType.C:
                    if (!CVars.Contains(variable))
                        CVars.Add(variable);
                    break;
                case ChepinVariableType.T:
                    if (!TVars.Contains(variable))
                        TVars.Add(variable);
                    break;
            }
        }
    }

    public class ChepinFullMetrics
    {
        public ChepinFullMetrics()
        {
            outputData = new ChepinFullData();
        }

        ChepinFullData outputData;

        public void FindChepin(string filepath)
        {
            string filecodeText = "";
            using (StreamReader sr = File.OpenText(filepath))
            {
                string cur;
                while ((cur = sr.ReadLine()) != null)
                {
                    InputTestLine(ref filecodeText, cur);
                }
            }

            DeleteVarDeclarations(ref filecodeText);
            while (DeleteFunDeclarations(ref filecodeText)) ;
            RemoveSigns(ref filecodeText);



        }

        void RemoveSigns(ref string input)
        {
            input = input.Replace("{", "");
            input = input.Replace("}", "");
            Regex reg = new Regex(@"\b(else(\s*)->)\b");
            while (reg.IsMatch(input))
            {
                string match = reg.Match(input).Value;
                input = input.Remove(input.IndexOf(match), match.Length);
            }
            reg = new Regex(@"\b(else)\b");
            while (reg.IsMatch(input))
            {
                string match = reg.Match(input).Value;
                input = input.Remove(input.IndexOf(match), match.Length);
            }
        }

        void DeleteVarDeclarations(ref string filecode)
        {
            Regex pattern = new Regex(@"\b(var)\b");
            filecode = pattern.Replace(filecode, "");
            pattern = new Regex(@"\b(val)\b");
            filecode = pattern.Replace(filecode, "");
        }

        bool DeleteFunDeclarations(ref string filecode)
        {
            Regex pattern = new Regex(@"\b(fun)(\s)+[\w|_]+(\s)*[\(]{1}");
            bool output = pattern.IsMatch(filecode);
            if (output)
            {
                int startIndex = pattern.Match(filecode).Index;
                int i = startIndex;
                while (filecode[i] != '(') i++;
                i++;
                int left = 1;
                int right = 0;
                while (left != right)
                {
                    if (filecode[i] == '(') left++;
                    if (filecode[i] == ')') right++;
                    i++;
                }
                filecode = filecode.Remove(startIndex, i - startIndex);
            }
            return output;
        }

        bool multilineComment = false;
        void InputTestLine(ref string filecodeText, string cur)
        {
            string line = cur;
            while (line.Length != 0)
            {
                if (line[0] == ' ' || line[0] == '\t')
                    line = line.Remove(0, 1);
                else
                    break;
            }

            int separatorPos = line.IndexOf(';');
            if (separatorPos != -1 && separatorPos != line.Length - 1)
            {
                InputTestLine(ref filecodeText, line.Substring(0, separatorPos + 1));
                InputTestLine(ref filecodeText, line.Substring(separatorPos + 1));
                return;
            }
            int singlelineComment = line.IndexOf("//");
            if (singlelineComment != -1)
            {
                line = line.Substring(0, singlelineComment);
            }
            line += "\r\n";
            if (!multilineComment)
            {
                int multilineCommentStart = line.IndexOf("/*");
                if (multilineCommentStart != -1)
                {
                    int multilineCommentEnd = line.LastIndexOf("*/");
                    if (multilineCommentEnd != -1 && multilineCommentEnd != multilineCommentStart + 1)
                    {
                        line = line.Remove(multilineCommentStart, multilineCommentEnd - multilineCommentStart + 2);
                    }
                    else if (multilineCommentEnd != multilineCommentStart + 1)
                    {
                        multilineComment = true;
                        line = line.Remove(multilineCommentStart);
                    }
                }
            }
            else
            {
                int endMultilineComment = line.LastIndexOf("*/");
                if (endMultilineComment != -1)
                {
                    line = line.Substring(endMultilineComment + 2);
                    multilineComment = false;
                }
                else return;
            }

            if (!string.IsNullOrWhiteSpace(line))
            {
                filecodeText += line;
            }
        }
    }
}
