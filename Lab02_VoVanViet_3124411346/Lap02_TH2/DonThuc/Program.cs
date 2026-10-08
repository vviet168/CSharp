using System;
using System.Globalization;
using System.Text;

namespace Bai15;

class DonThuc
{
    // P(x) = a * x^n
    private double a;   // hệ số (số thực)
    private int n;      // số mũ (số nguyên không âm)


    public double A
    {
        get => a;
        set => a = value;
    }

    public int N
    {
        get => n;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Số mũ phải là số nguyên không âm.");
            n = value;
        }
    }

    // ----- Constructor -----
    public DonThuc()
    {
        a = 0;
        n = 0;
    }

    public DonThuc(double a, int n)
    {
        A = a;
        N = n;
    }

    public DonThuc(DonThuc p)
    {
        a = p.a;
        n = p.n;
    }

    // ----- (a) Tính giá trị đơn thức tại x -----
    public double TinhGiaTri(double x) => a * Math.Pow(x, n);

    // ----- (b) Đạo hàm: Q(x) = P'(x) = a*n*x^(n-1) -----
    public DonThuc DaoHam()
    {
        if (n == 0)
            return new DonThuc(0, 0);        // đạo hàm của hằng số bằng 0
        return new DonThuc(a * n, n - 1);
    }

    public override string ToString()
    {
        if (a == 0) return "0";
        string heSo = a.ToString(CultureInfo.InvariantCulture);

        if (n == 0) return heSo;

        string phanX = n == 1 ? "x" : $"x^{n}";
        if (a == 1) return phanX;
        if (a == -1) return "-" + phanX;
        return heSo + phanX;
    }
}

class Program
{
    static double DocDouble(string nhan)
    {
        while (true)
        {
            Console.Write(nhan);
            string? s = Console.ReadLine();
            if (s != null && double.TryParse(s.Trim().Replace(',', '.'),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
                return v;
            Console.WriteLine("Giá trị không hợp lệ, nhập lại.");
        }
    }

    static int DocSoMu(string nhan)
    {
        while (true)
        {
            Console.Write(nhan);
            if (int.TryParse(Console.ReadLine(), out int v) && v >= 0)
                return v;
            Console.WriteLine("Số mũ phải là số nguyên không âm, nhập lại.");
        }
    }

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Nhập đơn thức P(x) = a * x^n");
        double a = DocDouble("  Hệ số a: ");
        int n = DocSoMu("  Số mũ n (nguyên không âm): ");
        DonThuc p = new DonThuc(a, n);
        Console.WriteLine($"P(x) = {p}");

        // (a) Giá trị tại x
        double x = DocDouble("\nNhập x: ");
        Console.WriteLine($"(a) P({x.ToString(CultureInfo.InvariantCulture)}) = {p.TinhGiaTri(x).ToString(CultureInfo.InvariantCulture)}");

        // (b) Đạo hàm
        DonThuc q = p.DaoHam();
        Console.WriteLine($"(b) Q(x) = P'(x) = {q}");
        Console.WriteLine($"    Q({x.ToString(CultureInfo.InvariantCulture)}) = {q.TinhGiaTri(x).ToString(CultureInfo.InvariantCulture)}");
    }
}