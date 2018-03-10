using System.Threading.Tasks;

namespace TraceIdentifierPoc.Service
{
    public interface ISomeService
    {
        Task LongRunningAsync(int myref);
    }
}
