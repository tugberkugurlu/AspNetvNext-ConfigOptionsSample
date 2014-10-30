using System.Threading;
using System.Threading.Tasks;

namespace Farticus
{
    public interface IFarticusRepository
    {
        Task<string> GetFartMessageAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}