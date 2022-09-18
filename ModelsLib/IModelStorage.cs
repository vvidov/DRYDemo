namespace ModelsLib.Models
{
    public interface IModelStorage
    {
        T Load<T>();
        void Save<T>(T obj);
    }
}