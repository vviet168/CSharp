using System;
using System.Text;

namespace Bai11;

class SinhVien
{

    private string hoTen;
    private int namSinh;

    public SinhVien()
    {
        hoTen = "";
        namSinh = DateTime.Now.Year;
    }

    public SinhVien(string hoTen, int namSinh)
    {
        HoTen = hoTen;
        NamSinh = namSinh;
    }


    public SinhVien(SinhVien sv)
    {
        hoTen = sv.hoTen;
        namSinh = sv.namSinh;
    }


    public string HoTen
    {
        get => hoTen;
        set => hoTen = value?.Trim() ?? "";
    }

    public int NamSinh
    {
        get => namSinh;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Năm sinh phải từ 1900 đến {DateTime.Now.Year}.");
            namSinh = value;
        }
    }


    public int TinhTuoi() => DateTime.Now.Year - namSinh;

    public void Xuat()
    {
        Console.WriteLine($"Họ tên   : {HoTen}");
        Console.WriteLine($"Năm sinh : {NamSinh}");
        Console.WriteLine($"Tuổi     : {TinhTuoi()}");
    }
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== TÍNH TUỔI SINH VIÊN ===");

        Console.Write("Nhập họ tên: ");
        string ten = Console.ReadLine() ?? "";

        SinhVien sv = new SinhVien();
        sv.HoTen = ten;

        while (true)
        {
            Console.Write("Nhập năm sinh: ");
            if (int.TryParse(Console.ReadLine(), out int nam))
            {
                try
                {
                    sv.NamSinh = nam;
                    break;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            Console.WriteLine("Năm sinh không hợp lệ, nhập lại.");
        }

        Console.WriteLine("\n--- Thông tin sinh viên ---");
        sv.Xuat();

        // Minh họa constructor sao chép
        SinhVien banSao = new SinhVien(sv);
        Console.WriteLine("\n--- Bản sao (copy constructor) ---");
        banSao.Xuat();
    }
}