using System;

namespace TableMaker {
    class Program {
        static void Main(string[] args) {
            var data = new[,] {
                { "C1", "C2" },
                { "1333", "2333" },
                { "1333", "23333" },
                { "ภาษาไทย", "ภาษาไทยมีปัญหาสระลอย" }
            };

            ArrayPrinter.PrintToConsole(data);
        }
    }
}
