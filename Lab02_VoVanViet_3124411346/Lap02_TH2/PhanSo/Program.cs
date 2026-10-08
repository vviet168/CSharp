using System;
using System.Text;

namespace Bai14;

class PhanSo
{

    private long tu;
    private long mau;

    public long Tu => tu;
    public long Mau => mau;

    public PhanSo()
    {
        tu = 0;
        mau = 1;
    }

    // Sao chép
    public PhanSo(PhanSo p)
    {
        tu = p.tu;
        mau = p.mau;
    }

    // Một tham số: số nguyên n = n/1
    public PhanSo(long tu) : this(tu, 1) { }

    // Hai tham số: tử và mẫu (tự rút gọn, chuẩn hóa dấu)
    public PhanSo(long tu, long mau)
    {
        if (mau == 0)
            throw new ArgumentException("Mẫu số phải khác 0.");
        if (mau < 0)
        {
            tu = -tu;
            mau = -mau;
        }
        long g = UCLN(Math.Abs(tu), mau);
        this.tu = tu / g;
        this.mau = mau / g;
    }

    private static long UCLN(long a, long b)
    {
        while (b != 0)
        {
            long r = a % b;
            a = b;
            b = r;
        }
        return a == 0 ? 1 : a;
    }

    // ----- ToString -----
    public override string ToString() => mau == 1 ? tu.ToString() : $"{tu}/{mau}";

    // ----- Toán tử một ngôi -----
    public static PhanSo operator +(PhanSo a) => new PhanSo(a.tu, a.mau);
    public static PhanSo operator -(PhanSo a) => new PhanSo(-a.tu, a.mau);

    // ----- Toán tử hai ngôi -----
    public static PhanSo operator +(PhanSo a, PhanSo b) => new PhanSo(a.tu * b.mau + b.tu * a.mau, a.mau * b.mau);
    public static PhanSo operator -(PhanSo a, PhanSo b) => new PhanSo(a.tu * b.mau - b.tu * a.mau, a.mau * b.mau);
    public static PhanSo operator *(PhanSo a, PhanSo b) => new PhanSo(a.tu * b.tu, a.mau * b.mau);
    public static PhanSo operator /(PhanSo a, PhanSo b)
    {
        if (b.tu == 0)
            throw new DivideByZeroException("Không thể chia cho phân số bằng 0.");
        return new PhanSo(a.tu * b.mau, a.mau * b.tu);
    }

    // ----- So sánh (mẫu luôn > 0 nên nhân chéo giữ nguyên chiều bất đẳng thức) -----
    public static bool operator >(PhanSo a, PhanSo b) => a.tu * b.mau > b.tu * a.mau;
    public static bool operator <(PhanSo a, PhanSo b) => a.tu * b.mau < b.tu * a.mau;
    public static bool operator >=(PhanSo a, PhanSo b) => a.tu * b.mau >= b.tu * a.mau;
    public static bool operator <=(PhanSo a, PhanSo b) => a.tu * b.mau <= b.tu * a.mau;
    public static bool operator ==(PhanSo a, PhanSo b) => a.tu == b.tu && a.mau == b.mau;
    public static bool operator !=(PhanSo a, PhanSo b) => !(a == b);

    // Đi kèm với == và !=
    public override bool Equals(object? obj) => obj is PhanSo p && this == p;
    public override int GetHashCode() => HashCode.Combine(tu, mau);
}

class Program
{
    static PhanSo NhapPhanSo(string ten)
    {
        while (true)
        {
            Console.WriteLine($"Nhập phân số {ten}:");
            Console.Write("  Tử số : ");
            bool okTu = long.TryParse(Console.ReadLine(), out long tu);
            Console.Write("  Mẫu số: ");
            bool okMau = long.TryParse(Console.ReadLine(), out long mau);

            if (!okTu || !okMau) { Console.WriteLine("Giá trị không hợp lệ, nhập lại."); continue; }
            if (mau == 0) { Console.WriteLine("Mẫu số phải khác 0, nhập lại."); continue; }
            return new PhanSo(tu, mau);
        }
    }

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        PhanSo macDinh = new PhanSo();
        Console.WriteLine($"Constructor mặc định: {macDinh}");

        PhanSo a = NhapPhanSo("a");
        PhanSo b = NhapPhanSo("b");
        PhanSo banSao = new PhanSo(a);

        Console.WriteLine($"\na = {a}, b = {b}, bản sao của a = {banSao}");

        Console.WriteLine("\n--- Toán tử một ngôi ---");
        Console.WriteLine($"+a = {+a}");
        Console.WriteLine($"-a = {-a}");

        Console.WriteLine("\n--- Toán tử hai ngôi ---");
        Console.WriteLine($"a + b = {a + b}");
        Console.WriteLine($"a - b = {a - b}");
        Console.WriteLine($"a * b = {a * b}");
        if (b.Tu == 0)
            Console.WriteLine("a / b = không thực hiện được (b bằng 0)");
        else
            Console.WriteLine($"a / b = {a / b}");

        Console.WriteLine("\n--- So sánh ---");
        Console.WriteLine($"a >  b : {a > b}");
        Console.WriteLine($"a <  b : {a < b}");
        Console.WriteLine($"a >= b : {a >= b}");
        Console.WriteLine($"a <= b : {a <= b}");
        Console.WriteLine($"a == b : {a == b}");
        Console.WriteLine($"a != b : {a != b}");
    }
}