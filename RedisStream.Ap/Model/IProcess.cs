
namespace RedisStream.Ap.Model
{
    using System.Threading.Tasks;

    public interface IProcess
    {
        Task Execute();
    }
}
