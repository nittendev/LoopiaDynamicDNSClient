using System.Threading.Tasks;
using LoopiaDDNSApp.Repository.Ipify;

namespace LoopiaDDNSApp.Repository.Interfaces
{
    public interface IIpifyRepository
    {
        public Task<string> GetExternalIp();
    }
}