using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlusReplication
{
    public static class clsStatus
    {

       static Form1 frmSts = null;

        
        /// <summary>
        /// Displays the splashscreen
        /// </summary>
        /// 
        public static void ShowScreen()
        {
            if (frmSts == null)
            {
                frmSts = new Form1();
                frmSts.ShowScreen();
            }
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        public static void CloseScreen()
        {
            if (frmSts != null)
            {
                frmSts.CloseScreen();
                frmSts = null;
            }
        }

        /// <summary>
        /// Update text in default green color of success message
        /// </summary>
        /// <param name="Text">Message</param>
        public static void UdpateStatusText(string Text)
        {
            if (frmSts != null)
                frmSts.UdpateStatusText(Text);
        }

        public static void ClearStatusText()
        {
            if (frmSts != null)
                frmSts.ClearStatusText();

        }
    }

}
