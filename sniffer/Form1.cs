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
            capturedPackets_list.Add(packetId, packet);
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
            sniffing.Abort();
            device.StopCapture();
            device.Close();
            this.listView.Enabled = true;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string protocol = e.Item.SubItems[4].Text;
            int key = Int32.Parse(e.Item.SubItems[0].Text);
            Packet packet;
            bool getPacket = capturedPackets_list.TryGetValue(key, out packet);

            switch (protocol)
            {
                case "Tcp":
                    if (getPacket)
                    {
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
                    }
                    break;
                case "Udp":
                    if (getPacket)
                    {
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
                    }
                    break;
                case "Arp":
                    if (getPacket)
                    {
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
                    }
                    break;
                case "Icmp":
                    if (getPacket)
                    {
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
                    }
                    break;
                case "Igmp":
                    if (getPacket)
                    {
                        IgmpV2Packet igmpPacket = packet.Extract<IgmpV2Packet>();
                        if (igmpPacket != null)
                        {
                            textBox_info.Text = "Packet number: " + key +
                                            " Type: IGMP v2" +
                                            "\r\nType: " + igmpPacket.Type +
                                            "\r\nGroup address: " + igmpPacket.GroupAddress +
                                            "\r\nMax response time" + igmpPacket.MaxResponseTime;
                        }
                    }
                    break;
                default:
                    textBox_info.Text = "";
                    break;
            }
        }
    }
}
