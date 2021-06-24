using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Deadblock.Tools
{
    public class FileUtils
    {
        /// <summary>
        /// Reads contents of the specified files,
        /// and splits it into lines.
        /// </summary>
        /// <param name="aPath">
        /// Path to the targeted file.
        /// </param>
        /// <returns>
        /// Lines of content from the file, represented
        /// as an array of strings.
        /// </returns>
        public static string[] ReadAsLines(string aPath)
        {
            string tempFile = File.ReadAllText(aPath);
            string[] tempLines = tempFile.Split("\n");

            // When splitting by line,
            // last line is always empty
            return tempLines[..^1];
        }

        /// <summary>
        /// Reads contents of the specified files,
        /// and interpretes it as a 2D array of chars.
        /// </summary>
        /// <param name="aPath">
        /// Path to the targeted file.
        /// </param>
        /// <param name="aSeparator">
        /// Blocks in the file will be
        /// detected using the passed separator.
        /// The value is set to comma by default.
        /// </param>
        /// <returns>
        /// 2D array of chars parsed
        /// from the targeted file.
        /// </returns>
        public static char[][] ReadAs2DSequence(string aPath, char aSeparator = ',')
        {
            // NOTE: It's possible to implement that with LINQ,
            // but present approach is much faster and readable.

            string[] tempLines = ReadAsLines(aPath);
            var tempSequence = new char[tempLines.Length][];

            for (var ma = 0; ma < tempLines.Length; ++ma)
            {
                string[] parts = tempLines[ma].Split(aSeparator);
                tempSequence[ma] = new char[parts.Length];

                for (var mk = 0; mk < parts.Length; ++mk)
                {
                    tempSequence[ma][mk] = parts[mk].Trim().First();
                }
            }

            return tempSequence;
        }

        /// <summary>
        /// Constructs a 3D sequence of chars
        /// from contents of passed files.
        /// </summary>
        /// <param name="myPath">
        /// Path to the targeted file.
        /// </param>
        /// <param name="aSeparator">
        /// Blocks in the file will be
        /// detected using the passed separator.
        /// The value is set to comma by default.
        /// </param>
        /// <returns>
        /// 3D array of chars parsed
        /// from the targeted file.
        /// </returns>
        public static char[][][] ReadAs3DSequence(string[] somePaths, char aSeparator = ',')
        {
            return somePaths
                .ToList()
                .Select(f => ReadAs2DSequence(f, aSeparator))
                .ToArray();
        }

        /// <summary>
        /// Parses string lines
        /// into key value chain, represented
        /// as string dictionary.
        /// </summary>
        /// <param name="someConfigLines">
        /// Array of string lines that
        /// need to parsed into a dictionary.
        /// </param>
        /// <returns>
        /// String dictionary constructed by processing
        /// each line in the passed stack.
        /// </returns>
        private static Dictionary<string, string> ParseConfig(string[] someConfigLines)
        {
            var tempConfig = new Dictionary<string, string>();
            var tempLines = someConfigLines.ToList();

            tempLines.ForEach((line) =>
            {
                var pair = line.Split(":").Select(f => f.Trim()).ToList();
                if (pair.Count != 2)
                {
                    throw new AggregateException($"Couldn't parse a config line. Invalid line: >>>> {line} <<<<. Contact DEV.");
                }

                var key = pair[0];
                var value = pair[1];

                if (tempConfig.ContainsKey(key))
                {
                    throw new AggregateException($"Detected key duplication in config. Key: {key}. Contact DEV");
                }

                tempConfig[key] = value;
            });

            return tempConfig;
        }

        /// <summary>
        /// Reads contents of the passed file,
        /// and treats it as a single-block config.
        /// </summary>
        /// <param name="myPath">
        /// Relative or absolute path to the targeted file.
        /// </param>
        /// <returns>
        /// String dictionary which represents
        /// config content of the targeted file.
        /// </returns>
        public static Dictionary<string, string> ReadAsConfig(string myPath)
        {
            string[] tempLines = ReadAsLines(myPath);
            return ParseConfig(tempLines);
        }

        /// <summary>
        /// Reads contents fo the passed file,
        /// and treats it as a multi-block config.
        /// </summary>
        /// <param name="myPath">
        /// Relative or absolute path to the targeted file.
        /// </param>
        /// <returns>
        /// Array of string dictionaries that represent
        /// configs content from the targeted file.
        /// </returns>
        public static Dictionary<string, string>[] ReadAsConfigBlocks(string myPath)
        {
            string[] tempLines = ReadAsLines(myPath);
            var tempBuffer = new List<string>();
            var tempBlocks = new List<List<string>>();

            for (var ma = 0; ma < tempLines.Length; ++ma)
            {
                string line = tempLines[ma];

                // Flush existing block
                if (line.StartsWith("---"))
                {
                    // No saved data -> ignore
                    if (tempBuffer.Count <= 0) continue;

                    tempBlocks.Add(tempBuffer);
                    tempBuffer = new List<string>();
                    continue;
                }

                // Write to the current buffer
                tempBuffer.Add(line);
            }

            var tempConfigs = tempBlocks.Select(f => ParseConfig(f.ToArray()));
            return tempConfigs.ToArray();
        }
    }
}
