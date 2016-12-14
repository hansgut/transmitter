using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace App1
{
    [Activity(Label = "Transmitter", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        bool flag = true;
        protected override void OnCreate(Bundle bundle)
        {   
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Chat send = new Chat();
            //GatewayIPAddressInformation info;
			EditText input_ip = FindViewById<EditText>(Resource.Id.input_ip);
            ImageButton button_start = FindViewById<ImageButton>(Resource.Id.start_button);
            ImageButton button_next = FindViewById<ImageButton>(Resource.Id.next_button);
            ImageButton button_prev = FindViewById<ImageButton>(Resource.Id.prev_button);
			Button button_connect = FindViewById<Button>(Resource.Id.connect_button);
            TextView current_song = FindViewById<TextView>(Resource.Id.current_song);
            string Ip = GetIP4Address();
            input_ip.Text = Ip.Substring(0, Ip.Length - 1);
			button_prev.Enabled = false;
			button_next.Enabled = false;
			button_start.Enabled = false;
            send.receive_songs(current_song);
			button_connect.Click += delegate {
				try
				{
					string ip = input_ip.Text;
					Regex reg = new Regex(@"[0-9]{3}\.[0-9]{3}\.[0-9]{1,3}\.[0-9]{1,3}");
					if (reg.IsMatch(ip))
					{
						send.Ip = ip;
						button_prev.Enabled = true;
						button_next.Enabled = true;
						button_start.Enabled = true;
						input_ip.Enabled = false;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}	
			};
            button_next.Click += delegate { send.buttonSend_Click("1"); };
            button_prev.Click += delegate { send.buttonSend_Click("-1"); };
			button_start.Click += delegate { 
                send.buttonSend_Click("0");
                if (flag)
                    button_start.SetImageResource(Resource.Drawable.ic_play_arrow_white_48dp);
                else
                    button_start.SetImageResource(Resource.Drawable.ic_pause_white_48dp);
                flag = !flag;
            };
        }

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
    }
}

