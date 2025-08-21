using System;
using System.Data;
using BUS_QuanLy;
using DAL_QuanLy;
using DTO_QuanLy;

public class BUS_DangKyHoc
{
    private DAL_DangKyHoc dalDK = new DAL_DangKyHoc();

    public bool ThemDangKy(DTO_DangKyHoc dk)
    {
        return dalDK.DangKyMonHoc(dk);
    }

    public bool KiemTraDaDangKy(string maSV, string maLHP)
    {
        return dalDK.DaDangKy(maSV, maLHP);
    }

    public bool KiemTraTrungLich(string maSV, string maLHP)
    {
        // Dùng lại hàm LayDanhSachLHPDaDangKy để kiểm tra giao thời khóa biểu
        DataTable dtDaDK = dalDK.LayDanhSachLHPDaDangKy(maSV);
        DataTable dtMoi = new BUS_LopHocPhan().LayThongTinLopHocPhan(maLHP); // viết riêng DAL cho việc này

        if (dtDaDK.Rows.Count == 0 || dtMoi.Rows.Count == 0)
            return false;

        int thuMoi = Convert.ToInt32(dtMoi.Rows[0]["Thu"]);
        int tietBDMoi = Convert.ToInt32(dtMoi.Rows[0]["TietBatDau"]);
        int tietKTMoi = Convert.ToInt32(dtMoi.Rows[0]["TietKetThuc"]);

        foreach (DataRow row in dtDaDK.Rows)
        {
            int thu = Convert.ToInt32(row["Thu"]);
            int tietBD = Convert.ToInt32(row["TietBatDau"]);
            int tietKT = Convert.ToInt32(row["TietKetThuc"]);

            if (thu == thuMoi &&
                !(tietKT < tietBDMoi || tietBD > tietKTMoi)) // giao nhau
            {
                return true;
            }
        }
        return false;
    }

    public bool KiemTraMonTienQuyet(string maSV, string maLHP)
    {
        // Dùng DAL để truy ra MaMH → MonTienQuyet → kiểm tra điểm >= 5
        return new DAL_LopHocPhan().KiemTraMonTienQuyet(maSV, maLHP);
    }
    public DataTable LayMonDaDangKy(string maSV, string maHK)
    {
        return dalDK.LayMonDaDangKy(maSV, maHK);
    }
    public bool HuyDangKy(string maSV, string maLHP)
    {
        return dalDK.HuyDangKy(maSV, maLHP);
    }
    public DataTable LayDiemSinhVien(string maSV, string maHK)
    {
        return dalDK.LayDiemSinhVien(maSV, maHK);
    }

}
