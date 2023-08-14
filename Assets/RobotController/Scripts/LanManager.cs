using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace RobotController
{
    public class LanManager : MonoBehaviour
    {
        public const int LocalPort = 11000;
        public const int RemotePort = 10000;
        private const string PingMessage = "ping-robot";
        private const string PongMessage = "pong-robot";
        public string IpAddress { get; private set; }
        public string TargetIpAddress { get; private set; }

        private void Awake()
        {
            GetCurrentIp();
            GetTargetIP();
        }

        private void GetCurrentIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IpAddress = ip.ToString();
                    Debug.Log($"IP Address: {IpAddress}");
                    return;
                }

            Debug.Log("No network adapters with an IPv4 address in the system!");
        }

        private void GetTargetIP()
        {
            // get all devices connected to the same wireless local network by broadcasting to the subnet
            var udpClient = new UdpClient();
            var ipEndPoint = new IPEndPoint(IPAddress.Broadcast, RemotePort);
            udpClient.EnableBroadcast = true;
            var request = Encoding.ASCII.GetBytes(PingMessage);
            udpClient.Send(request, request.Length, ipEndPoint);
            var response = udpClient.Receive(ref ipEndPoint);
            if (Encoding.ASCII.GetString(response) == PongMessage)
            {
                Debug.Log($"IP Address: {ipEndPoint.Address}");
                TargetIpAddress = ipEndPoint.Address.ToString();
            }
            else
            {
                Debug.Log("Target device not found!");
            }
        }
    }
}