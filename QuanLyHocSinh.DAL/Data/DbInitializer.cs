using QuanLyHocSinh.DAL.Context;

namespace QuanLyHocSinh.DAL.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HocSinhSqlContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
