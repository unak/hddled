using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace hddled
{
	public partial class FormMain : Form
	{
		static private PerformanceCounter readCounter;
		static private PerformanceCounter writeCounter;

		public FormMain()
		{
			InitializeComponent();

			readCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
			writeCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
			timer.Start();
		}

		private void menuItemExit_Click(object sender, EventArgs e)
		{
			timer.Stop();
			if (readCounter != null)
			{
				readCounter.Dispose();
				readCounter = null;
			}
			if (writeCounter != null)
			{
				writeCounter.Dispose();
				writeCounter = null;
			}
			this.Close();
			Application.Exit();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (readCounter != null && writeCounter != null)
			{
				var read = readCounter.NextValue();
				var write = writeCounter.NextValue();

				//notifyIcon.Text = string.Format("Read: {0}, Write: {1}", read, write);
				if (read == 0 && write == 0)
				{
					notifyIcon.Icon = Properties.Resources.ICON_LED0;
				}
				else if (read > 0 && write == 0)
				{
					notifyIcon.Icon = Properties.Resources.ICON_LED1;
				}
				else if (write > 0)
				{
					notifyIcon.Icon = Properties.Resources.ICON_LED2;
				}
				else
				{
					notifyIcon.Icon = Properties.Resources.ICON_LED3;
				}
			}
		}
	}
}
