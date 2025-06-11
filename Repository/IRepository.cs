namespace HuginnLogAPI.Repository;

public interface IRepository
{
    void Add(object model);
    void Save(object model);
    void Delete(object model);
    T GetById<T>(object id);
    IQueryable<T> GetAll<T>();
    
    IDisposable InitTransaction();
    void Commit();
    void Rollback();
}