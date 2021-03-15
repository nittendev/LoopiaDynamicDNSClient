namespace LoopiaDDNSApp.Models
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