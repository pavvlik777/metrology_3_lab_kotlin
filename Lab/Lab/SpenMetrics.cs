﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab
{
    public class SpenData
    {
        public Dictionary<string, int> spens;

        public SpenData()
        {
            spens = new Dictionary<string, int>() { };
        }

        public void AddSpen(string spen)
        {
            while (true)
            {
                if (spen.Length > 0)
                {
                    if (spen[0] == '\r' || spen[0] == '\n' || spen[0] == ' ' || spen[0] == '\t')
                        spen = spen.Remove(0, 1);
                    else
                        break;
                }
                else
                    return;
            }
            while (true)
            {
                if (spen.Length > 0)
                {
                    if (spen[spen.Length - 1] == '\r' || spen[spen.Length - 1] == '\n' || spen[spen.Length - 1] == ' ' || spen[spen.Length - 1] == '\t')
                        spen = spen.Remove(spen.Length - 1, 1);
                    else
                        break;
                }
                else
                    return;
            }

            if (int.TryParse(spen, out _) || float.TryParse(spen, out _))
                return;

            if (spens.ContainsKey(spen))
                spens[spen]++;
            else
                spens.Add(spen, 0);
        }
    }

    public class SpenMetrics
    {
        public SpenMetrics()
        {
            outputSpen = new SpenData();
        }

        SpenData outputSpen;

        public Dictionary<string, int> FindSpen(string filepath)
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

            //DeleteVarDeclarations(ref filecodeText);
            while (DeleteFunDeclarations(ref filecodeText)) ;
            //RemoveSigns(ref filecodeText);
            while (ReplaceStrings(ref filecodeText)) ;
            while (ReplaceChars(ref filecodeText));

            ParseStringForSpen(filecodeText);
            return outputSpen.spens;
        }

        void RemoveSigns(ref string input)
        {
            input = input.Replace("{", " ");
            input = input.Replace("}", " ");
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

        void ParseStringForSpen(string line)
        {
            (bool answer, string before, string _in, string after) funcData = HasFunctions(line);
            if(funcData.answer)
            {
                ParseStringForSpen(funcData.before);
                ParseStringForSpen(funcData._in);
                ParseStringForSpen(funcData.after);
            }
            else
            {
                (bool answer, int index, int lenght) twoSideOperatorsData = HasTwoSideOperators(line);
                if(twoSideOperatorsData.answer)
                {
                    ParseStringForSpen(line.Substring(0, twoSideOperatorsData.index));
                    ParseStringForSpen(line.Substring(twoSideOperatorsData.index + twoSideOperatorsData.lenght));
                }
                else
                {
                    outputSpen.AddSpen(line);
                }
            }
        }

        (bool, string, string, string) HasFunctions(string line)
        {
            Regex pattern = new Regex(@"\b([\w|_]+(\s)*[\(]{1})");
            if(pattern.IsMatch(line))
            {
                string match = pattern.Match(line).Value;
                string beforeFunc = line.Substring(0, pattern.Match(line).Index);
                int i = pattern.Match(line).Index + match.Length;
                int left = 1;
                int right = 0;
                while(left != right)
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
            @"(\r\n)",
            @"([\(]{1})|([\)]{1})",
            @"([\[]{1})|([\]]{1})",
            @"(==)|(!=)|(>=)|(<=)|(>)|(<)",
            @"(\band\b)|(\bxor\b)|(\bor\b)|(\bin\b)|(\bshr\b)|(\bshl\b)|(\bushr\b)",
            @"(\bdo\b)",
            @"(\bcontinue\b)|(\bbreak\b)|(\bimport\b)|(\breturn\b)",
            @"([.]{2})|([,]{1})",
            @"([\:]{1})",
            @"([\+]{2})|([-]{2})",
            @"([\+]{1})|([-]{1})|([\*]{1})|([/]{1})|([%]{1)}|([=]{1})|([!]{1})",
            @"[;]{1}",
            @"(\bvar\b)|(\bval\b)",
            @"([(]{1})|([)]{1})|([{]{1})|([}]{1})",
            @"(Byte)|(Short)|(Int)|(Long)|(Float)|(Double)|(String)",
            @"(\btrue\b)|(\bfalse\b)|(\belse\b)"
        };

        bool ReplaceChars(ref string input)
        {
            Regex doubleQuotes = new Regex(@"[']{1}");
            bool output = doubleQuotes.IsMatch(input);
            if (output)
            {
                Match cur = doubleQuotes.Match(input);
                int startIndex = cur.Index + 1;
                int i = startIndex;
                while (input[i] != '\'' && input[i] != '\r')
                    i++;
                if (input[i] == '\r')
                {
                    input = input.Remove(startIndex - 1, 1);
                    input = input.Insert(startIndex - 1, "1");
                }
                else
                {
                    input = input.Remove(startIndex - 1, i - startIndex + 2);
                    input = input.Insert(startIndex - 1, "1");
                }
            }
            return output;
        }

        bool ReplaceStrings(ref string input)
        {
            Regex doubleQuotes = new Regex(@"[""]{1}");
            bool output = doubleQuotes.IsMatch(input);
            if(output)
            {
                Match cur = doubleQuotes.Match(input);
                int startIndex = cur.Index + 1;
                int i = startIndex;
                while (input[i] != '"' && input[i] != '\r')
                    i++;
                if(input[i] == '\r')
                {
                    input = input.Remove(startIndex - 1, 1);
                    input = input.Insert(startIndex - 1, "1");
                }
                else
                {
                    input = input.Remove(startIndex - 1, i - startIndex + 2);
                    input = input.Insert(startIndex - 1, "1");
                }
            }
            return output;
        }

        (bool, int, int) HasTwoSideOperators(string input)
        {
            for (int i = 0; i < twoSideOperators.Length; i++)
            {
                Regex reg = new Regex(twoSideOperators[i]);
                if (reg.IsMatch(input))
                {
                    string match = reg.Match(input).Value;
                    int index = input.IndexOf(match);
                    return (true, index, match.Length);
                }
            }
            return (false, 0, 0);
        }

        void DeleteVarDeclarations(ref string filecode)
        {
            Regex pattern = new Regex(@"\b(var)\b");
            filecode = pattern.Replace(filecode, " ");
            pattern = new Regex(@"\b(val)\b");
            filecode = pattern.Replace(filecode, " ");
        }

        bool DeleteFunDeclarations(ref string filecode) 
        {
            Regex pattern = new Regex(@"\b(fun)(\s)+[\w|_]+(\s)*[\(]{1}");
            bool output = pattern.IsMatch(filecode);
            if(output)
            {
                int startIndex = pattern.Match(filecode).Index;
                int i = startIndex;
                while (filecode[i] != '(') i++;
                i++;
                int left = 1;
                int right = 0;
                while(left != right)
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
