using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab
{
    public class ChepinIOData
    {
        public List<string> FuncVars;
        public List<string> AllVars;
        public List<string> PVars;
        public List<string> MVars;
        public List<string> CVars;
        public List<string> TVars;

        public ChepinIOData()
        {
            FuncVars = new List<string>() { };
            AllVars = new List<string>() { };
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

            if (int.TryParse(variable, out _) || float.TryParse(variable, out _))
                return;

            switch (type)
            {
                case ChepinVariableType.P:
                    if (!PVars.Contains(variable) && AllVars.Contains(variable))
                        PVars.Add(variable);
                    break;
                case ChepinVariableType.M:
                    if (!MVars.Contains(variable) && AllVars.Contains(variable))
                        MVars.Add(variable);
                    break;
                case ChepinVariableType.C:
                    if (!CVars.Contains(variable) && AllVars.Contains(variable))
                        CVars.Add(variable);
                    break;
                case ChepinVariableType.T:
                    if (!TVars.Contains(variable) && AllVars.Contains(variable))
                        TVars.Add(variable);
                    break;
                case ChepinVariableType.All:
                    if (!AllVars.Contains(variable))
                        AllVars.Add(variable);
                    break;
                case ChepinVariableType.Func:
                    if (!FuncVars.Contains(variable) && AllVars.Contains(variable))
                        FuncVars.Add(variable);
                    break;
            }
        }
    }

    public class ChepinIOMetrics
    {
        public ChepinIOMetrics()
        {
            outputData = new ChepinIOData();
        }
        ChepinIOData outputData;

        public ChepinIOData FindChepin(string filepath)
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
            filecodeText += "\r\n";

            DeleteVarDeclarations(ref filecodeText);
            while (DeleteFunDeclarations(ref filecodeText)) ;
            RemoveSigns(ref filecodeText);

            FindAllVars(filecodeText);
            FindFuncVars(filecodeText);
            FindPType(filecodeText);
            FindCType(filecodeText);
            FindMType(filecodeText);
            FindTType(filecodeText);

            return outputData;
        }

        void FindFuncVars(string line)
        {
            string[] patterns =
            {
                @"\b([\w|_]+(\s)*[\(]{1})"
            };
            for (int j = 0; j < patterns.Length; j++)
            {
                Regex pattern = new Regex(patterns[j]);
                if (pattern.IsMatch(line))
                {
                    MatchCollection matches = pattern.Matches(line);
                    foreach (Match cur in matches)
                    {
                        int startIndex = cur.Index + cur.Value.Length;
                        int i = startIndex;
                        while (line[i] != '\r') i++;
                        string targetString = line.Substring(startIndex, i - startIndex);
                        CountAllVars(targetString, ChepinVariableType.Func);
                    }
                }
            }
        }

        void FindAllVars(string line)
        {
            string[] patterns =
            {
                @"\b(print(\s)*[\(]{1})",
                @"\b(println(\s)*[\(]{1})",
            };
            for (int j = 0; j < patterns.Length; j++)
            {
                Regex pattern = new Regex(patterns[j]);
                if (pattern.IsMatch(line))
                {
                    MatchCollection matches = pattern.Matches(line);
                    foreach (Match cur in matches)
                    {
                        int startIndex = cur.Index + cur.Value.Length;
                        int i = startIndex;
                        while (line[i] != '\r') i++;
                        string targetString = line.Substring(startIndex, i - startIndex);
                        CountAllVars(targetString, ChepinVariableType.All);
                    }
                }
            }
            Regex readLinePattern = new Regex(@"(\s)*[=]{1}(\s)*(readLine)(\s)*[\(]{1}");
            MatchCollection readlineMatches = readLinePattern.Matches(line);
            foreach (Match cur in readlineMatches)
            {
                int startIndex = cur.Index - 1;
                int i = startIndex;
                while (line[i] != ' ' && line[i] != '\t' && line[i] != '\r' && line[i] != '\n')
                    i--;
                outputData.AddVariable(line.Substring(i + 1, startIndex - i), ChepinVariableType.All);
            }
        }

        void FindTType(string line)
        {
            IEnumerable<string> TType = outputData.AllVars.Except(outputData.PVars);
            TType = TType.Except(outputData.CVars);
            TType = TType.Except(outputData.MVars);

            foreach (var cur in TType)
                outputData.AddVariable(cur, ChepinVariableType.T);
        }

        void FindMType(string line)
        {
            string[] patterns =
            {
                @"([\+]{1}[=]{1})",
                @"([-]{1}[=]{1})",
                @"([\*]{1}[=]{1})",
                @"([/]{1}[=]{1})",
                @"([%]{1}[=]{1})",
                @"([=]{1})"
            };
            for (int j = 0; j < patterns.Length; j++)
            {
                Regex pattern = new Regex(patterns[j]);
                if (pattern.IsMatch(line))
                {
                    MatchCollection matches = pattern.Matches(line);
                    foreach (Match cur in matches)
                    {
                        int startIndex = cur.Index + cur.Value.Length;
                        int i = startIndex;
                        while (line[i] != '\r') i++;
                        string targetString = line.Substring(startIndex, i - startIndex);
                        CountAllVars(targetString, ChepinVariableType.M);
                    }
                }
            }
            IEnumerable<string> funcVariables = outputData.FuncVars.Except(outputData.CVars);
            foreach (var cur in funcVariables)
                outputData.AddVariable(cur, ChepinVariableType.M);
        }

        void FindCType(string line)
        {
            string[] patterns =
            {
                @"\b(if(\s)*[\(]{1})",
                @"\b(for(\s)*[\(]{1})",
                @"\b(while(\s)*[\(]{1})",
                @"\b(when(\s)*[\(]{1})"
            };
            for (int j = 0; j < patterns.Length; j++)
            {
                Regex pattern = new Regex(patterns[j]);
                if (pattern.IsMatch(line))
                {
                    MatchCollection matches = pattern.Matches(line);
                    foreach (Match cur in matches)
                    {
                        int startIndex = cur.Index + cur.Value.Length;
                        int i = startIndex;
                        int left = 1;
                        int right = 0;
                        while (left != right)
                        {
                            if (line[i] == '(') left++;
                            if (line[i] == ')') right++;
                            i++;
                        }
                        i--;
                        string targetString = line.Substring(startIndex, i - startIndex);
                        CountAllVars(targetString, ChepinVariableType.C);
                    }
                }
            }
        }

        void CountAllVars(string line, ChepinVariableType type)
        {
            (bool answer, string before, string _in, string after) funcData = HasFunctions(line);
            if (funcData.answer)
            {
                CountAllVars(funcData.before, type);
                CountAllVars(funcData._in, type);
                CountAllVars(funcData.after, type);
            }
            else
            {
                (bool answer, int index) twoSideOperatorsData = HasTwoSideOperators(line);
                if (twoSideOperatorsData.answer)
                {
                    CountAllVars(line.Substring(0, twoSideOperatorsData.index), type);
                    CountAllVars(line.Substring(twoSideOperatorsData.index + 1), type);
                }
                else
                {
                    outputData.AddVariable(line, type);
                }
            }
        }

        (bool, string, string, string) HasFunctions(string line)
        {
            Regex pattern = new Regex(@"\b([\w|_]+(\s)*[\(]{1})");
            if (pattern.IsMatch(line))
            {
                string match = pattern.Match(line).Value;
                string beforeFunc = line.Substring(0, pattern.Match(line).Index);
                int i = pattern.Match(line).Index + match.Length;
                int left = 1;
                int right = 0;
                while (left != right)
                {
                    if (line[i] == '(') left++;
                    if (line[i] == ')') right++;
                    i++;
                }
                string afterFunc = line.Substring(i);
                string inFunc = line.Substring(pattern.Match(line).Index + match.Length, i - 1 - pattern.Match(line).Index - match.Length);

                return (true, beforeFunc, inFunc, afterFunc);
            }

            return (false, "", "", "");
        }

        static string[] twoSideOperators =
        {
            @"([\+]{1}[=]{1})|([-]{1}[=]{1})|([\*]{1}[=]{1})|([/]{1}[=]{1})|([%]{1}[=]{1})",
            @"(===)|(!==)",
            @"(\bis\b)|(\bas\b)",
            @"(&&)|([\|]{2})",
            @"(->)",
            @"([\(]{1})|([\)]{1})",
            @"(==)|(!=)|(>=)|(<=)|(>)|(<)",
            @"(\band\b)|(\bxor\b)|(\bor\b)|(\bin\b)|(\bshr\b)|(\bshl\b)|(\bushr\b)",
            @"(\bdo\b)",
            @"(\bcontinue\b)|(\bbreak\b)|(\bimport\b)|(\breturn\b)",
            @"([.]{2})",
            @"([\:]{1})",
            @"([\+]{2})|([-]{2})",
            @"([\+]{1})|([-]{1})|([\*]{1})|([/]{1})|([%]{1)}|([=]{1})|([!]{1})",
            @"[;]{1}",
            @"(\bvar\b)|(\bval\b)",
            @"([(]{1})|([)]{1})|([{]{1})|([}]{1})"
        };

        (bool, int) HasTwoSideOperators(string input)
        {
            for (int i = 0; i < twoSideOperators.Length; i++)
            {
                Regex reg = new Regex(twoSideOperators[i]);
                if (reg.IsMatch(input))
                {
                    string match = reg.Match(input).Value;
                    int index = input.IndexOf(match);
                    return (true, index);
                }
            }
            return (false, 0);
        }

        void FindPType(string line)
        {
            Regex pattern = new Regex(@"(\s)*[=]{1}(\s)*(readLine)(\s)*[\(]{1}");
            MatchCollection matches = pattern.Matches(line);
            foreach (Match cur in matches)
            {
                int startIndex = cur.Index - 1;
                int i = startIndex;
                while (line[i] != ' ' && line[i] != '\t' && line[i] != '\r' && line[i] != '\n')
                    i--;
                outputData.AddVariable(line.Substring(i + 1, startIndex - i), ChepinVariableType.P);
            }
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
