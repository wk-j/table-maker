using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FloatingCharacter;

namespace TableMaker {
    internal static class ArrayPrinter {
        private const string CellLeftTop = "┌";
        private const string CellRightTop = "┐";
        private const string CellLeftBottom = "└";
        private const string CellRightBottom = "┘";
        private const string CellVerticalJointLeft = "├";
        private const string CellTJoint = "┼";
        private const string CellVerticalJointRight = "┤";
        private const string CellVerticalLine = "│";
        private static char[] FloatChars = Glyph.FloatingCharacters();

        private static int GetMaxCellWidth(string[,] arrValues) {
            var maxWidth = 1;

            for (var i = 0; i < arrValues.GetLength(0); i++) {
                for (var j = 0; j < arrValues.GetLength(1); j++) {
                    var length = arrValues[i, j].Length;
                    if (length > maxWidth) {
                        maxWidth = length;
                    }
                }
            }

            return maxWidth;
        }

        private static int GetFloatingCount(string v) {
            return v.Count(FloatChars.Contains);
        }

        private static string GetDataInTableFormat(string[,] arrValues) {

            var formattedString = string.Empty;

            if (arrValues == null)
                return formattedString;

            var dimension1Length = arrValues.GetLength(0);
            var dimension2Length = arrValues.GetLength(1);

            var maxCellWidth = GetMaxCellWidth(arrValues);
            var indentLength = (dimension2Length * maxCellWidth) + (dimension2Length - 1);
            //printing top line;
            formattedString = $"{CellLeftTop}{Indent(indentLength)}{CellRightTop}{Environment.NewLine}";

            for (var i = 0; i < dimension1Length; i++) {
                var lineWithValues = CellVerticalLine;
                var line = CellVerticalJointLeft;
                for (var j = 0; j < dimension2Length; j++) {
                    var floatCount = GetFloatingCount(arrValues[i, j]);
                    var value = arrValues[i, j].PadLeft(maxCellWidth + floatCount, ' ');
                    lineWithValues += string.Format("{0}{1}", value, CellVerticalLine);
                    line += Indent(maxCellWidth);
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

            //printing bottom line
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