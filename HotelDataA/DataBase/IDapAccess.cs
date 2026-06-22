
namespace HotelDataA.DataBase
{
    public interface IDapAccess
    {
        List<T> LoadData<T, P>(string sql, P param, string csName, bool IsProcedure = false);
        void Save<T>(string sql, T param, string csName, bool IsProcedure = false);
    }
}