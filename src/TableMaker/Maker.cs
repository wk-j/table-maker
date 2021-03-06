using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FloatingCharacter;

namespace TableMaker {
    public static class ArrayPrinter {

        private const string CellLeftTop = "┌";
        private const string CellRightTop = "┐";
        private const string CellLeftBottom = "└";
        private const string CellRightBottom = "┘";
        private const string CellVerticalJointLeft = "├";
        private const string CellTJoint = "┼";
        private const string CellVerticalJointRight = "┤";
        private const string CellVerticalLine = "│";
        private static char[] _floatChars = Glyph.FloatingCharacters();

        private static int GetFloatingCount(string v) {
            return v.Count(_floatChars.Contains);
        }

        private static IDictionary<int, int> MaxCellWidth(string[,] values) {
            var rows = values.GetLength(0);
            var columns = values.GetLength(1);
            var dict = new Dictionary<int, int>();
            foreach (var item in Enumerable.Range(0, columns)) {
                var max = Enumerable
                    .Range(0, rows)
                    .Select(x => values[x, item])
                    .Select(x => (x.Length) - GetFloatingCount(x)).Max();
                dict[item] = max + 3;
            }
            return dict;
        }

        private static string GetDataInTableFormat(string[,] arrValues) {

            var formattedString = string.Empty;

            if (arrValues == null)
                return formattedString;

            var dimension1Length = arrValues.GetLength(0);
            var dimension2Length = arrValues.GetLength(1);

            var maxs = MaxCellWidth(arrValues);
            var indentLength = maxs.Values.Sum() + maxs.Count - 1;

            // printing top line;
            formattedString = $"{CellLeftTop}{Indent(indentLength)}{CellRightTop}{Environment.NewLine}";

            for (var i = 0; i < dimension1Length; i++) {
                var lineWithValues = CellVerticalLine;
                var line = CellVerticalJointLeft;
                for (var j = 0; j < dimension2Length; j++) {
                    var max = maxs[j];
                    var floatCount = GetFloatingCount(arrValues[i, j]);
                    var value = arrValues[i, j].PadLeft(max + floatCount, ' ');
                    lineWithValues += string.Format("{0}{1}", value, CellVerticalLine);
                    line += Indent(max);
                    if (j < (dimension2Length - 1)) {
                        line += CellTJoint;
                    }
                }
                line += CellVerticalJointRight;
                formattedString += string.Format("{0}{1}", lineWithValues, Environment.NewLine);
                if (i < (dimension1Length - 1)) {
                    formattedString += string.Format("{0}{1}", line, Environment.NewLine);
                }
            }

            // printing bottom line
            formattedString += $"{CellLeftBottom}{Indent(indentLength)}{CellRightBottom}{Environment.NewLine}";
            return formattedString;
        }

        private static string Indent(int count) {
            return string.Empty.PadLeft(count, '─');
        }

        public static void PrintToStream(string[,] arrValues, StreamWriter writer) {
            if (arrValues == null)
                return;

            writer?.Write(GetDataInTableFormat(arrValues));
        }

        public static void PrintToConsole(string[,] arrValues) {
            if (arrValues == null)
                return;

            Console.WriteLine(GetDataInTableFormat(arrValues));
        }
    }
}