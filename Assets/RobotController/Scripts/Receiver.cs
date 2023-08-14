using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace RobotController
{
    public class Receiver : MonoBehaviour
    {
        private UdpClient _udpClient;

        public byte[] ReceivedBytes { get; private set; }
        public string ReceivedString { get; private set; }

        private void Start()
        {
            _udpClient = new UdpClient(11000);
            _udpClient.BeginReceive(ReceiveCallback, null);
        }

        private void OnApplicationQuit()
        {
            _udpClient.Close();
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            var bytes = _udpClient.EndReceive(ar, ref ipEndPoint);
            var message = Encoding.ASCII.GetString(bytes);
            ReceivedBytes = bytes;
            ReceivedString = message;
            Debug.Log($"Received: {message}");
            _udpClient.BeginReceive(ReceiveCallback, null);
        }
    }
}