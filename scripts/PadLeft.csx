
var data = new List<(string, int)> {
    ("วันนี้มีเวลาไม่มาก", 20),
    ("สวัสดี", 17),
    ("ภาษาไทย", 15),
    ("Hello",15),
    ("English", 15)
};

foreach (var (item, x) in data) {
    Console.WriteLine($"|{item.PadLeft(x)} |");
}