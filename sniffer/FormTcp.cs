using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sniffer
{
    public partial class FormTcp : Form
    {
        private Dictionary<int, RawCapture> packets;
        private Dictionary<int, string> timestamps;
        private string srcIp;
        private string dstIp;
        private ushort srcPort;
        private ushort dstPort;


        public FormTcp(Dictionary<int, RawCapture> packets, Dictionary<int, string> timestamps, string srcIp, string dstIp, ushort srcPort, ushort dstPort)
        {
            this.packets = packets;
            this.timestamps = timestamps;
            this.srcIp = srcIp;
            this.dstIp = dstIp;
            this.srcPort = srcPort;
            this.dstPort = dstPort;
            InitializeComponent();
        }

        private void FormTcp_Load(object sender, EventArgs e)
        {
            this.label_left.Text = this.srcIp + ":" + this.srcPort;
            this.label_right.Text = this.dstIp + ":" + this.dstPort;
            foreach (var item in this.packets)
            {
                Packet packet = item.Value.GetPacket();
                IPPacket ipPacket = packet.Extract<IPPacket>();
                
                if (ipPacket != null && ipPacket.Protocol==ProtocolType.Tcp)
                {
                    string srcIp = ipPacket.SourceAddress.ToString();
                    string dstIp = ipPacket.DestinationAddress.ToString();
                    TcpPacket tcpPacket = packet.Extract<TcpPacket>();
                    ushort srcPort = tcpPacket.SourcePort;
                    ushort dstPort = tcpPacket.DestinationPort;
                    if (srcIp==this.srcIp && dstIp==this.dstIp && srcPort==this.srcPort && dstPort==this.dstPort)
                    {
                        ListViewItem showitem = new ListViewItem(item.Key.ToString());
                        showitem.SubItems.Add(timestamps[item.Key]);
                        showitem.SubItems.Add(srcIp);
                        showitem.SubItems.Add(dstIp);
                        showitem.SubItems.Add(packet.TotalPacketLength.ToString());

                        listView.Items.Add(showitem);
                    }
                    else if (srcIp == this.dstIp && dstIp == this.srcIp && srcPort == this.dstPort && dstPort == this.srcPort)
                    {
                        ListViewItem showitem = new ListViewItem(item.Key.ToString());
                        showitem.SubItems.Add(timestamps[item.Key]);
                        showitem.SubItems.Add(srcIp);
                        showitem.SubItems.Add(dstIp);
                        showitem.SubItems.Add(packet.TotalPacketLength.ToString());

                        listView.Items.Add(showitem);
                    }
                }
            }
        }


        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int key = Int32.Parse(e.Item.SubItems[0].Text);
            RawCapture capture;
            bool getPacket = packets.TryGetValue(key, out capture);
            if (getPacket)
            {
                Packet packet = capture.GetPacket();
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
        }
    }
}
