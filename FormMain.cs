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
		static Icon[] icons = {
			Properties.Resources.ICON_LED2_0,
			Properties.Resources.ICON_LED2_1,
			Properties.Resources.ICON_LED2_2,
			Properties.Resources.ICON_LED2_3,
		};

		public FormMain()
		{
			InitializeComponent();

			notifyIcon.Icon = icons[0];
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
				notifyIcon.Icon = icons[(read > 0 ? 1 : 0) + (write > 0 ? 2 : 0)];
			}
		}
	}
}
