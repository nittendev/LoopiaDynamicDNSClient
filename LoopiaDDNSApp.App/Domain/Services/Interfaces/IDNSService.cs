using System.Threading.Tasks;

namespace LoopiaDDNSApp.Domain.Services.Interfaces
{
    public interface IDNSService
    {
        Task<bool> UpdateDNS();
    }
}