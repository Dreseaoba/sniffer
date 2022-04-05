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
        private Dictionary<int, RawCapture> capturedPackets_list = new Dictionary<int, RawCapture>();

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
            this.label_warn_filter.Visible = false;
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
            try
            {
                device.Filter = this.textBox_cf.Text;
                device.Capture();
            }
            catch (SharpPcap.PcapException)
            {
                device.Close();
                
                Action action = () =>
                {
                    this.label_warn_filter.Visible = true;
                    this.listView.Enabled = true;
                };
                listView.Invoke(action);
            }
        }

        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            DateTime time = e.Header.Timeval.Date;
            string time_str = time.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string length = e.Data.Length.ToString();

            RawCapture capture = e.GetPacket();
            capturedPackets_list.Add(packetId, capture);
            Packet packet = capture.GetPacket();
            IPPacket ipPacket = packet.Extract<IPPacket>();

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
            if (sniffing!=null)
            {
                if (sniffing.IsAlive)
                {
                    sniffing.Abort();
                }
            }
            
            device.StopCapture();
            device.Close();
            this.listView.Enabled = true;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string protocol = e.Item.SubItems[4].Text;
            int key = Int32.Parse(e.Item.SubItems[0].Text);
            RawCapture capture;
            bool getPacket = capturedPackets_list.TryGetValue(key, out capture);
            if (getPacket)
            {
                Packet packet = capture.GetPacket();
                switch (protocol)
                {
                    case "Tcp":
                        this.button_tcp.Visible = true;
                        TcpPacket tcpPacket = packet.Extract<TcpPacket>();
                        if (tcpPacket != null)
                        {
                            textBox_info.Text = "Packet number: " + key +
                                            " Type: TCP" +
                                            "\r\nSource port:" + tcpPacket.SourcePort +
                                            "\r\nDestination port: " + tcpPacket.DestinationPort +
                                            "\r\nTCP header size: " + tcpPacket.DataOffset +
                                            "\r\nWindow size: " + tcpPacket.WindowSize +
                                            "\r\nChecksum:" + tcpPacket.Checksum.ToString() + (tcpPacket.ValidChecksum ? ",valid" : ",invalid") +
                                            "\r\nTCP checksum: " + (tcpPacket.ValidTcpChecksum ? "valid" : "invalid") +
                                            "\r\nSequence number: " + tcpPacket.SequenceNumber.ToString() +
                                            "\r\nAcknowledgment number: " + tcpPacket.AcknowledgmentNumber + (tcpPacket.Acknowledgment ? ",valid" : ",invalid") +
                                            // flags
                                            "\r\nUrgent pointer: " + (tcpPacket.Urgent ? "valid" : "invalid") +
                                            "\r\nACK flag: " + (tcpPacket.Acknowledgment ? "1" : "0") +
                                            "\r\nPSH flag: " + (tcpPacket.Push ? "1" : "0") +
                                            "\r\nRST flag: " + (tcpPacket.Reset ? "1" : "0") +
                                            "\r\nSYN flag: " + (tcpPacket.Synchronize ? "1" : "0") +
                                            "\r\nFIN flag: " + (tcpPacket.Finished ? "1" : "0") +
                                            "\r\nECN flag: " + (tcpPacket.ExplicitCongestionNotificationEcho ? "1" : "0") +
                                            "\r\nCWR flag: " + (tcpPacket.CongestionWindowReduced ? "1" : "0") +
                                            "\r\nNS flag: " + (tcpPacket.NonceSum ? "1" : "0");
                        }
                        break;
                    case "Udp":
                        this.button_tcp.Visible = false;
                        UdpPacket udpPacket = packet.Extract<UdpPacket>();
                        if (udpPacket != null)
                        {
                            textBox_info.Text = "Packet number: " + key +
                                            " Type: UDP" +
                                            "\r\nSource port:" + udpPacket.SourcePort +
                                            "\r\nDestination port: " + udpPacket.DestinationPort +
                                            "\r\nChecksum:" + udpPacket.Checksum.ToString() + " valid: " + udpPacket.ValidChecksum +
                                            "\r\nValid UDP checksum: " + udpPacket.ValidUdpChecksum;
                        }
                        break;
                    case "Arp":
                        this.button_tcp.Visible = false;
                        ArpPacket arpPacket = packet.Extract<ArpPacket>();
                        if (arpPacket != null)
                        {
                            System.Net.IPAddress senderAddress = arpPacket.SenderProtocolAddress;
                            System.Net.IPAddress targerAddress = arpPacket.TargetProtocolAddress;
                            System.Net.NetworkInformation.PhysicalAddress senderHardwareAddress = arpPacket.SenderHardwareAddress;
                            System.Net.NetworkInformation.PhysicalAddress targerHardwareAddress = arpPacket.TargetHardwareAddress;

                            textBox_info.Text = "Packet number: " + key +
                                            " Type: ARP" +
                                            "\r\nHardware address length:" + arpPacket.HardwareAddressLength +
                                            "\r\nProtocol address length: " + arpPacket.ProtocolAddressLength +
                                            "\r\nOperation: " + arpPacket.Operation.ToString() +
                                            "\r\nSender protocol address: " + senderAddress +
                                            "\r\nTarget protocol address: " + targerAddress +
                                            "\r\nSender hardware address: " + senderHardwareAddress +
                                            "\r\nTarget hardware address: " + targerHardwareAddress;
                        }
                        break;
                    case "Icmp":
                        this.button_tcp.Visible = false;
                        IcmpV4Packet icmpPacket = packet.Extract<IcmpV4Packet>();
                        if (icmpPacket != null)
                        {
                            textBox_info.Text = "Packet number: " + key +
                                            " Type: ICMP v4" +
                                            "\r\nType Code: 0x" + icmpPacket.TypeCode.ToString("x") +
                                            "\r\nChecksum: " + icmpPacket.Checksum.ToString("x") +
                                            "\r\nID: 0x" + icmpPacket.Id.ToString("x") +
                                            "\r\nSequence number: " + icmpPacket.Sequence.ToString("x");
                        }
                        break;
                    case "Igmp":
                        this.button_tcp.Visible = false;
                        IgmpV2Packet igmpPacket = packet.Extract<IgmpV2Packet>();
                        if (igmpPacket != null)
                        {
                            textBox_info.Text = "Packet number: " + key +
                                            " Type: IGMP v2" +
                                            "\r\nType: " + igmpPacket.Type +
                                            "\r\nGroup address: " + igmpPacket.GroupAddress +
                                            "\r\nMax response time" + igmpPacket.MaxResponseTime;
                        }
                        break;
                    default:
                        this.button_tcp.Visible = false;
                        textBox_info.Text = "";
                        break;
                }
            }

            #region Data Format
            string[] Str = new string[capture.Data.Length / 16 + 1];
            textBox_info.Text += "\r\n                          Byte                                   ASCII\r\n";
            for (int i = 0; i <= (capture.Data.Length - 1) / 16; i++)
            {
                Str[i] = i.ToString("X4");
                int j;
                for (j = 0; (j < 16) && (i * 16 + j < capture.Data.Length); j++)
                {
                    if (j % 8 == 0)
                        Str[i] += "  ";
                    Str[i] += capture.Data[i * 16 + j].ToString("X2") + " ";
                }
                for (; j < 16; j++)
                {
                    if (j % 8 == 0)
                        Str[i] += "  ";
                    Str[i] += "   ";
                }
                Str[i] += "      ";
                for (j = 0; (j < 16) && (i * 16 + j < capture.Data.Length); j++)
                {
                    if (capture.Data[i * 16 + j] < 32 || capture.Data[i * 16 + j] > 126)
                        Str[i] += ".";
                    else
                    {
                        Str[i] += Convert.ToChar(capture.Data[i * 16 + j]);
                    }
                }
            }
            for (int i = 0; i <= capture.Data.Length / 16; i++)
            {
                textBox_info.Text += Str[i] + "\r\n";
            }
            #endregion

        }

        private void button_tcp_Click(object sender, EventArgs e)
        {

        }
    }
}
