
namespace UserMgmtDAL.Repository;
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByName(string name);
    T GetById(int id);
    int Add(T item);
    bool Update(T item);
    bool Delete(int id);
}
