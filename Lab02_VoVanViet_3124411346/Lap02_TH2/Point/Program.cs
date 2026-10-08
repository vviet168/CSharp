using System;
using System.Globalization;
using System.Text;

namespace Bai12;

class Point
{

    private double x;
    private double y;

    public double X { get => x; set => x = value; }
    public double Y { get => y; set => y = value; }


    // Mặc định: x = y = 0
    public Point()
    {
        x = 0;
        y = 0;
    }

    // Có tham số (tiện dùng khi tạo điểm mới)
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // ----- Nhập / Xuất -----
    static double DocSo(string nhan)
    {
        while (true)
        {
            Console.Write(nhan);
            string? s = Console.ReadLine();
            // Chấp nhận cả dấu ',' lẫn '.'
            if (s != null && double.TryParse(s.Trim().Replace(',', '.'),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
                return v;
            Console.WriteLine("Giá trị không hợp lệ, nhập lại.");
        }
    }

    public void Input()
    {
        x = DocSo("  Nhập x: ");
        y = DocSo("  Nhập y: ");
    }

    public void Output() => Console.WriteLine(ToString());

    // ----- Override ToString -----
    public override string ToString() => $"({x}, {y})";

    // ----- (a) Khoảng cách giữa 2 điểm -----
    // Phương thức thành viên: this là điểm thứ nhất
    public double KhoangCach(Point p)
    {
        double dx = x - p.x;
        double dy = y - p.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Phương thức tĩnh
    public static double KhoangCach(Point a, Point b)
    {
        double dx = a.x - b.x;
        double dy = a.y - b.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // ----- (b) Trung điểm của 2 điểm -----
    // Phương thức thành viên
    public Point TrungDiem(Point p) => new Point((x + p.x) / 2, (y + p.y) / 2);

    // Phương thức tĩnh
    public static Point TrungDiem(Point a, Point b) => new Point((a.x + b.x) / 2, (a.y + b.y) / 2);

    // ----- Toán tử -----
    public static Point operator +(Point a, Point b) => new Point(a.x + b.x, a.y + b.y);
    public static Point operator -(Point a, Point b) => new Point(a.x - b.x, a.y - b.y);
    public static Point operator -(Point a) => new Point(-a.x, -a.y);   // lấy âm
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Point a = new Point();
        Point b = new Point();

        Console.WriteLine("Nhập điểm A:");
        a.Input();
        Console.WriteLine("Nhập điểm B:");
        b.Input();

        Console.Write("A = "); a.Output();
        Console.Write("B = "); b.Output();

        // (a) Khoảng cách
        Console.WriteLine("\n--- (a) Khoảng cách AB ---");
        Console.WriteLine($"Phương thức thành viên: {a.KhoangCach(b):0.####}");
        Console.WriteLine($"Phương thức tĩnh      : {Point.KhoangCach(a, b):0.####}");

        // (b) Trung điểm
        Console.WriteLine("\n--- (b) Trung điểm I của AB ---");
        Console.WriteLine($"Phương thức thành viên: I = {a.TrungDiem(b)}");
        Console.WriteLine($"Phương thức tĩnh      : I = {Point.TrungDiem(a, b)}");

        // Toán tử
        Console.WriteLine("\n--- Toán tử ---");
        Console.WriteLine($"A + B = {a + b}");
        Console.WriteLine($"A - B = {a - b}");
        Console.WriteLine($"-A    = {-a}");
    }
}