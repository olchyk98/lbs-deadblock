using System.IO;
using System.Linq;

namespace Tools
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
        public static string[] ReadAsLines (string aPath)
        {
            string tempFile = File.ReadAllText(aPath);
            string[] tempLines = tempFile.Split("\n");

            return tempLines;
        }

        /// <summary>
        /// Reads contents of the specified files,
        /// and interpretes it as a 2D array of chars.
        /// </summary>
        /// <param name="myPath">
        /// Path to the targeted file.
        /// </param>
        /// <param name="mySeparator">
        /// Blocks in the file will be
        /// detected using the passed separator.
        /// The value is set to comma by default.
        /// </param>
        /// <returns>
        /// 2D array of chars parsed
        /// from the targeted file.
        /// </returns>
        public static char[][] ReadAs2DSequence (string myPath, char mySeparator = ',')
        {
            // NOTE: It's possible to implement that with LINQ,
            // but present approach is much faster and readable.

            string[] tempLines = ReadAsLines(myPath);
            var tempSequence = new char[tempLines.Length][];

            for(var ma = 0; ma < tempLines.Length; ++ma)
            {
                string[] parts = tempLines[0].Split(mySeparator);
                tempSequence[ma] = new char[parts.Length];

                for(var mk = 0; mk < parts.Length; ++mk)
                {
                    tempSequence[ma][mk] = parts[mk].Trim()[0];
                }
            }

            return tempSequence.ToArray();
        }

        public static char[][][] ReadAs3DSequence (string[] myPaths, char mySeparator)
        {
            return null;
        }
    }
}
