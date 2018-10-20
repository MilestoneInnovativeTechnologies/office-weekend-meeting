using System;
using System.Diagnostics;
using System.Windows.Forms;
using ePlusReplication.Properties;
using System.Drawing;

namespace ePlusReplication
{
	/// <summary>
	/// 
	/// </summary>
	class ContextMenus
	{
		/// <summary>
		/// Is the About box displayed?
		/// </summary>
		bool isAboutLoaded = false;
        bool isStatusMinimised = true;

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>ContextMenuStrip</returns>
		public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
			ToolStripSeparator sep;

			// Status Window.
			item = new ToolStripMenuItem();
			item.Text = "Status";
			item.Click += new EventHandler(Status_Click);
			item.Image = Resources.Explorer;
			menu.Items.Add(item);

			// About.
			item = new ToolStripMenuItem();
			item.Text = "About";
			item.Click += new EventHandler(About_Click);
			item.Image = Resources.About;
			menu.Items.Add(item);

			// Separator.
			sep = new ToolStripSeparator();
			menu.Items.Add(sep);

			// Exit.
			item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			item.Image = Resources.Exit;
			menu.Items.Add(item);

			return menu;
		}

		/// <summary>
		/// Handles the Click event of the Explorer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Status_Click(object sender, EventArgs e)
		{
            if (isStatusMinimised)
            {
                isStatusMinimised = true;
                
            }
			//Process.Start("explorer", null);

		}

		/// <summary>
		/// Handles the Click event of the About control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void About_Click(object sender, EventArgs e)
		{
			if (!isAboutLoaded)
			{
				isAboutLoaded = true;
                new frmAbout().ShowDialog();
				isAboutLoaded = false;
			}
		}

		/// <summary>
		/// Processes a menu item.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
            Application.ExitThread();
			Application.Exit();
		}
	}
}