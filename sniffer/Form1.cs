using SharpPcap.LibPcap;
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
    public partial class Form1 : Form
    {

        List<LibPcapLiveDevice> interfaceList = new List<LibPcapLiveDevice>();
        LibPcapLiveDevice device;

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
        }

        private void button_device_Click(object sender, EventArgs e)
        {
            this.label_warn_device.Visible = false;
            if (this.comboBox_device.SelectedIndex>=0)
            {
                this.device = this.interfaceList[this.comboBox_device.SelectedIndex];
            }
            else
            {
                this.label_warn_device.Visible = true;
            }
        }
    }
}
