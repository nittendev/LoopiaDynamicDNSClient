using System.Threading.Tasks;

namespace LoopiaDDNSApp.Repository.Ipify.Interfaces
{
    public interface IIpifyRepository
    {
        public Task<string> GetExternalIp();
    }
}