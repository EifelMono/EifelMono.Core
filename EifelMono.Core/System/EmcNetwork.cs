using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EifelMono.Core.System
{
    public static class EmcNetwork
    {
        public static bool Ping(string ipAddress, int timeout = 2000, int retries = 5)
        {
            var task= EmcTask<bool>.Run(async () =>
            {
                using (var pingSender = new Ping())
                {
                    var options = new PingOptions
                    {
                        DontFragment = true
                    };

                    string data = "12345678901234567890123456789012";

                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    try
                    {
                        for (int retry = 0; retry < retries; retry++)
                            if ((await pingSender.SendPingAsync(ipAddress, timeout, buffer, options).ConfigureAwait(false)).Status == IPStatus.Success)
                                return true;
                    }
                    catch { }
                    return false;
                }
            });
            task.Wait();
            return task.Result;
        }

        public static async Task<bool> PingAsync(string ipAddress, int timeout = 2000, int retries = 5)
        {
            return await EmcTask<bool>.Run(() =>
            {
                return Ping(ipAddress, timeout, retries);
            }).ConfigureAwait(false);
        }

        public static bool CheckPortUsage(int port)
        {
            try
            {
                using (var tcpClient = new TcpClient(new IPEndPoint(IPAddress.Any, port))) { }
                return true;
            }
            catch { }
            return false;
        }

        public static async Task<bool> CheckPortUsageAsync(int port)
        {
            return await EmcTask<bool>.Run(() => CheckPortUsage(port)).ConfigureAwait(false);
        }
    }
}
