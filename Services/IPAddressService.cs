using System.Net;


namespace HomeController.Services
{

    public class IPAssigner
    {
        private readonly IPAddress startIP;
        private readonly int maxHosts;
        List<string> assignedIPs = new List<string>();
        private int currentOffset;

        private readonly HttpClient httpClient;

        public IPAssigner(HttpClient httpClient, int maxHosts = 254)
        {
            this.httpClient = httpClient;
            this.startIP = IPAddress.Parse("192.168.1.0");
            this.maxHosts = maxHosts;
            this.currentOffset = 30; // Start from .1 (e.g., 192.168.0.1)
        }

        public async Task InitializeAsync()
        {
            await GetAssignedIPsAsync();
            ShowAssignedIPs();
        }

        public async Task GetAssignedIPsAsync()
        {
            string url = "http://192.168.1.16:3000/ip/get?subnet=192.168.1.0/24";

            httpClient.Timeout = TimeSpan.FromSeconds(15);

            var response = await httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            var doc = System.Text.Json.JsonDocument.Parse(json);
            var ips = doc.RootElement.GetProperty("activeIPs").EnumerateArray();
            if (doc != null)
            {

                foreach (var ip in ips)
                {
                    var ipStr = ip.GetString();

                    assignedIPs.Add(ipStr);
                }
            }

            DbHelper dbHelper = new DbHelper();

            var ipsList = dbHelper.getIps();
            foreach (var ip in ipsList)
            {
                assignedIPs.Add(ip);
            }
        }

        public string RequestNewIP()
        {
            while (currentOffset <= maxHosts)
            {
                ShowAssignedIPs();
                var ipBytes = startIP.GetAddressBytes();
                ipBytes[3] = (byte)(ipBytes[3] + currentOffset);
                string newIP = new IPAddress(ipBytes).ToString();
                Console.WriteLine($"Trying IP: {newIP}");
                if (!assignedIPs.Contains(newIP))
                {
                    Console.WriteLine($"Assigned IP: {newIP}");
                    assignedIPs.Add(newIP);
                    currentOffset++;
                    ShowAssignedIPs();
                    return newIP;
                }

                currentOffset++;
            }
            throw new InvalidOperationException("No available IP addresses.");
        }

        public bool ReleaseIP(string ip)
        {
            return assignedIPs.Remove(ip);
        }

        public void ShowAssignedIPs()
        {
            Console.WriteLine("Assigned IPs:");
            foreach (var ip in assignedIPs)
            {
                Console.WriteLine(ip);
            }
        }
    }

};
