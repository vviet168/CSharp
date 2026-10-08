using System;
using System.Text;

namespace Bai13;

class Person
{
    private int id;
    private string name;
    private int yob;   // năm sinh
    private int yod;   // năm mất (0 = còn sống)

    public Person()
    {
        id = 0;
        name = "";
        yob = 0;
        yod = 0;
    }

    // ----- Copy constructor -----
    public Person(Person p)
    {
        id = p.id;
        name = p.name;
        yob = p.yob;
        yod = p.yod;
    }

    // ----- Hàm hỗ trợ nhập số nguyên -----
    static int DocSoNguyen(string nhan, Func<int, bool> hopLe, string thongBaoLoi)
    {
        while (true)
        {
            Console.Write(nhan);
            if (int.TryParse(Console.ReadLine(), out int v) && hopLe(v))
                return v;
            Console.WriteLine(thongBaoLoi);
        }
    }

    // ----- Input -----
    public void Input()
    {
        id = DocSoNguyen("  Nhập id: ", v => true, "Id không hợp lệ, nhập lại.");

        Console.Write("  Nhập họ tên: ");
        name = (Console.ReadLine() ?? "").Trim();

        yob = DocSoNguyen("  Nhập năm sinh: ", v => v > 0, "Năm sinh phải là số nguyên dương.");

        int namSinh = yob;
        yod = DocSoNguyen("  Nhập năm mất (0 nếu còn sống): ",
            v => v == 0 || v >= namSinh,
            "Năm mất phải bằng 0 hoặc không nhỏ hơn năm sinh.");
    }

    // ----- Output -----
    public void Output()
    {
        Console.WriteLine($"  Id       : {id}");
        Console.WriteLine($"  Họ tên   : {name}");
        Console.WriteLine($"  Năm sinh : {yob}");
        Console.WriteLine($"  Năm mất  : {(yod == 0 ? "(chưa có)" : yod.ToString())}");
        Console.WriteLine($"  Tình trạng: {(IsLiving() ? "Còn sống" : "Đã mất")}");
    }

    // ----- IsLiving -----
    // yod == 0: chưa có năm mất -> còn sống -> true
    // yod != 0: đã có năm mất   -> false
    public bool IsLiving() => yod == 0;
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Person mặc định (Default constructor) ===");
        Person macDinh = new Person();
        macDinh.Output();

        Console.WriteLine("\n=== Nhập thông tin một người ===");
        Person p1 = new Person();
        p1.Input();
        Console.WriteLine("\n--- p1 ---");
        p1.Output();

        Console.WriteLine("\n=== Bản sao (Copy constructor) ===");
        Person p2 = new Person(p1);
        Console.WriteLine("--- p2 ---");
        p2.Output();
    }
}