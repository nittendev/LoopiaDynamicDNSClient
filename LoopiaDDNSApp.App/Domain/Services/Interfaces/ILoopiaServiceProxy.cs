using Horizon.XmlRpc.Client;
using Horizon.XmlRpc.Core;
using LoopiaDDNSApp.Models;

namespace LoopiaDDNSApp.Domain.Services.Interfaces
{
    public interface ILoopiaServiceProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("updateZoneRecord")]
        public string UpdateZoneRecord(string username, string password, string domain, string subdomain, RecordDto record_Obj);

        [XmlRpcMethod("getZoneRecords")]
        public RecordDto[] GetZoneRecords(string username, string password, string domain, string subdomain);
    }
}