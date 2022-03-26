using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sniffer
{
    public partial class Form1 : Form
    {

        List<LibPcapLiveDevice> interfaceList = new List<LibPcapLiveDevice>();
        LibPcapLiveDevice device;
        Thread sniffing;
        private int packetId = 0;
        private Dictionary<int, Packet> capturedPackets_list = new Dictionary<int, Packet>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LibPcapLiveDeviceList devices = LibPcapLiveDeviceList.Instance;

            foreach (LibPcapLiveDevice d in devices)
            {
                if (!d.Interface.Addresses.Exists(a => a != null && a.Addr != null && a.Addr.ipAddress != null))
                    continue;
                var devInterface = d.Interface;
                string friendlyName = devInterface.FriendlyName;
                string description = devInterface.Description;

                interfaceList.Add(d);
                this.comboBox_device.Items.Add(friendlyName);
            }

            if (interfaceList.Count>0)
            {
                this.comboBox_device.SelectedIndex = 0;
                this.device = this.interfaceList[0];
                this.Text = "Sniffer Device: " + device.Interface.FriendlyName;
            }
        }

        private void button_device_Click(object sender, EventArgs e)
        {
            this.label_warn_device.Visible = false;
            if (this.comboBox_device.SelectedIndex>=0)
            {
                this.device = this.interfaceList[this.comboBox_device.SelectedIndex];
                this.Text = "Sniffer Device: " + device.Interface.FriendlyName;
            }
            else
            {
                this.label_warn_device.Visible = true;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (device!=null)
            {
                device.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
                this.sniffing = new Thread(new ThreadStart(sniffing_Proccess));
                this.sniffing.Start();
                this.listView.Enabled = false;
            }
        }

        private void sniffing_Proccess()
        {
            int readTimeoutMilliseconds = 1000;

            device.Open(DeviceModes.Promiscuous, readTimeoutMilliseconds);
            device.Filter = this.textBox_cf.Text;
            device.Capture();
        }

        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            DateTime time = e.Header.Timeval.Date;
            string time_str = time.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string length = e.Data.Length.ToString();


            Packet packet = e.GetPacket().GetPacket();



            // add to the list
            capturedPackets_list.Add(packetId, packet);
            


            var ipPacket = packet.Extract<IPPacket>();


            if (ipPacket != null)
            {
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                string protocol_type = ipPacket.Protocol.ToString();
                string srcIP = srcIp.ToString();
                string dstIP = dstIp.ToString();



                var protocolPacket = ipPacket.PayloadPacket;

                ListViewItem item = new ListViewItem(packetId.ToString());
                item.SubItems.Add(time_str);
                item.SubItems.Add(srcIP);
                item.SubItems.Add(dstIP);
                item.SubItems.Add(protocol_type);
                item.SubItems.Add(length);


                Action action = () => listView.Items.Add(item);
                listView.Invoke(action);

                
            }
            packetId++;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            sniffing.Abort();
            device.StopCapture();
            device.Close();
            this.listView.Enabled = true;
        }
    }
}
