namespace LoopiaDDNSApp.Domain.Services.Interfaces
{
    public struct RecordDto
    {
        public string type;
        public int ttl;
        public int priority;
        public string rdata;
        public int record_id;
    }
}