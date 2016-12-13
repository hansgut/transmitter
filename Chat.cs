using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.IO;


namespace App1
{
    class Chat
    {
        public string Ip { get; set; }

        public Chat()
        {
			Ip = null;
        }

        public void buttonSend_Click(string mess)
        {
            UdpClient udp = new UdpClient();

            IPAddress ipaddress = IPAddress.Parse(Ip);
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 15000);
            Console.WriteLine(Ip);
            byte[] message = Encoding.Default.GetBytes(mess);
            int sended = udp.Send(message, message.Length, ipendpoint);
            udp.Close();
        }
    }
}
