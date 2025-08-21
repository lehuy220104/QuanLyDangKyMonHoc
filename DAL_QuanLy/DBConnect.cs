using System.Data;
using System.Data.SqlClient;

namespace DAL_QuanLy
{
    public class DBConnect
    {
        protected SqlConnection _conn = new SqlConnection("Data Source=LAPTOP-GC2FQGCT\\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True");
    }


}
