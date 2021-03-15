namespace LoopiaDDNSApp
{
    public class Config
    {
        public int ttl { get; set; }
        public string LoopiaAPIUri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string Subdomain { get; set; }

        // FOR MX
        public int Priority { get; set; }
        // Typically type=A IPV4
        public string RData { get; set; }
        // Record Index.
        public int RecordId { get; set; }
        // A / CNAME
        public string Type { get; set; }
    }
}