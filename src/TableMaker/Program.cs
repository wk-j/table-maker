using System;
using FloatingCharacter;
using System.Linq;

namespace TableMaker {
    class Program {
        static void Main(string[] args) {
            var excludes = Glyph.FloatingCharacters();

            var data = new[,] {
                { "ปรับตัวรับมือกับเทคโนโลยีดิจิทัลกันอย่างคึกคัก", "เชื้อชาติ" },
                { "20", "ต้องทำอะไรบ้างรับมือแนวโน้มการเปลี่ยนแปลง" },
                { "ซอฟต์แวร์ออกแบบเชิงวิศวกรรมชื่อดัง", "ภาษาไทยมีปัญหาสระลอย" },
                { "ซอฟต์แวร์ออกแบบเชิงวิศวกรรมชื่อดัง", "ภาษาไทยมีปัญหาสระลอย" },
                { "ขี่ช้าง", "ภาษาไทยมีปัญหาสระลอย" },
                { "ซอฟต์แวร์ออกแบบเชิงวิศวกรรมชื่อดัง", "ชี้ชี้ชี้" },
                { "อู่อู้", "ก็อ๋อชี้ชี้ชี้" },
                { "อู่อู้", "อี้อี้อี้อี่อี่" }
            };

            ArrayPrinter.PrintToConsole(data);
        }
    }
}
