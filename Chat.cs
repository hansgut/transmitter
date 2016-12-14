using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Android.Widget;
using Android.App;


namespace App1
{
    class Chat : Activity
    {
        public string Ip { get; set; }

        public Chat()
        {
			Ip = null;
        }
        private UdpClient socket = new UdpClient(15000);
        TextView current_song = null;
        public void buttonSend_Click(string mess)
        {

            UdpClient udp = new UdpClient(15000);

            IPAddress ipaddress = IPAddress.Parse(Ip);
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 15000);
            Console.WriteLine(Ip);
            byte[] message = Encoding.Default.GetBytes(mess);
            int sended = udp.Send(message, message.Length, ipendpoint);
            udp.Close();
        }

        void OnUdpData(IAsyncResult result)
        {
            string select = null;
            UdpClient socket = result.AsyncState as UdpClient;
            IPEndPoint source = new IPEndPoint(0, 0);
            byte[] message = socket.EndReceive(result, ref source);
            select = Encoding.Default.GetString(message);
            Application.SynchronizationContext.Post(_ => { current_song.Text = select; }, null);
            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        }

        public void receive_songs(TextView t)
        {
            current_song = t;
            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        }
    }
}
