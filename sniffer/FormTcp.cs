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



        public FormTcp(Dictionary<int, RawCapture> packets, string srcIp, string dstIp, ushort srcPort, ushort dstPort)
        {
            this.packets = packets;
            InitializeComponent();
        }

        private void FormTcp_Load(object sender, EventArgs e)
        {

        }
    }
}
