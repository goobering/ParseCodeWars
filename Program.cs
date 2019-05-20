using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParseCodeWars
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var langNameDict = new Dictionary<string, string>()
            {
                // Current
                { "C", "c" },
                { "C#", "cs" },
                { "C++", "cpp" },
                { "Clojure", "clj" },
                { "CoffeeScript", "coffee" },
                { "Crystal", "cr" },
                { "Dart", "dart" },
                { "Elixir", "ex" },
                { "F#", "fs" },
                { "Go", "go" },
                { "Haskell", "hs" },
                { "Java", "java" },
                { "JavaScript", "js" },
                { "PHP", "php" },
                { "Python", "py" },
                { "Ruby", "rb" },
                { "Rust", "rs" },
                { "Shell", "" },
                { "SQL", "sql" },
                { "Swift", "swift" },
                { "TypeScript", "ts" },

                // Proposed
                { "Agda", "agda" },
                { "BF", "b" },
                { "Coq", "v" },
                { "Elm", "elm" },
                { "Erlang", "erl" },
                { "Fortran", "f90" },
                { "Groovy", "groovy" },
                { "Idris", "idr" },
                { "Julia", "jl" },
                { "Kotlin", "kt" },
                { "Lua", "lua" },
                { "NASM", "asm" },
                { "Nim", "nim" },
                { "Objective-C", "m" },
                { "OCaml", "ml" },
                { "PowerShell", "ps1" },
                { "PureScript", "purs" },
                { "R", "r" },
                { "Racket", "rkt" },
                { "Reason", "re" },
                { "Scala", "scala" },
                { "Solidity", "sol" },

                // c
                // clojure
                // coffeescript
                // cpp
                // crystal
                // csharp
                // dart
                // elixir
                // fsharp
                // go
                // haskell
                // java
                // javascript
                // lua
                // objc
                // ocaml
                // php
                // python
                // ruby
                // rust
                // shell
                // sql
                // swift
                // typescript
            };

            string filePath = "./Katas.txt";
            var inputString = File.ReadAllText(filePath);

            // Honking dirty great magic Regex looking for the end of each section: 
            // 3 weeks ago
            // Refactor
            // Discuss
            var kyuArray = Regex.Split(inputString, @"(?:\d*\s(?:second|seconds|minute|minutes|hour|hours|day|days|week|weeks|month|months|year|years)\sago(?:\r\n|\r|\n)\s*Refactor(?:\r\n|\r|\n)\s*Discuss(?:\r\n|\r|\n)*)");

            var solutionArray = new Solution[kyuArray.Length];
            for(int i = 0; i < kyuArray.Length; i++)
            {
                var splitSolution = Regex.Split(kyuArray[i], "\r\n|\r|\n");

                var kyu = int.Parse(splitSolution[0][0].ToString());
                var name = splitSolution[1];
                var language = splitSolution[2].TrimEnd(new char[]{':'});

                var codeList = new List<String>();
                for(int j = 4; j < splitSolution.Length; j++)
                {
                    codeList.Add(splitSolution[j]);
                }
                    
                var code = string.Join(Environment.NewLine, codeList);

                solutionArray[i] = new Solution(kyu, name, language, code);
            }

            foreach(var solution in solutionArray)
            {
                var outputFileName = solution.Name;
                foreach (var c in Path.GetInvalidFileNameChars())
                {
                    outputFileName = outputFileName.Replace(c, '-');
                }

                var strippedFileName = new string(outputFileName.Where(c => !char.IsPunctuation(c)).ToArray());
                strippedFileName = strippedFileName.Replace(' ', '-');

                var outputFileExt = langNameDict[solution.Language];
                var outputFilePath = $@"./Output/";
                var outputFilePathNameExt = Path.Combine(outputFilePath, $"{strippedFileName}.{outputFileExt}");

                using(StreamWriter sw = new StreamWriter(outputFilePathNameExt, true))
                {
                    sw.Write(solution.Code);
                }
            }

            var path = $@"https://www.codewars.com/api/v1/users/goobering/code-challenges/completed?page=0&access_key=[YOUR ACCESS KEY]";
            try
            {
                var kataList = await WebProgram.GetCompletedKata(path);
                Console.WriteLine("Got list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }

            // await Task.Delay(2000);

            Console.WriteLine("Done");
        }
    }
}
