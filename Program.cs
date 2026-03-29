using System;
using System.Windows.Forms;

namespace PAX_Chatter;

static class Program
{
    [STAThread]
    static void Main()
    {
        try {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        } catch (Exception ex) {
            MessageBox.Show("CRASH ERROR: " + ex.Message + "\n\nInner: " + ex.InnerException?.Message);
        }
    }
}