namespace Janus.ORM
{
    public interface ISaveIntercepter
    {
        void BeforeSave(bool insert);
    }
}