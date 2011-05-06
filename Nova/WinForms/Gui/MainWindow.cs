using Nova.Client;
using Nova.Common;

using System;
using System.Windows.Forms;

namespace Nova.WinForms.Gui
{
    /// ----------------------------------------------------------------------------
    /// <summary>
    /// The main window.
    /// </summary>
    /// ----------------------------------------------------------------------------
    public static class MainWindow
    {
        [STAThread]
        public static void Main(string[] args)
        {
            NovaGUI gui = new NovaGUI();
            
            Application.EnableVisualStyles();
            ClientState.Initialize(args);
            gui.Text = "Nova - " + ClientState.Data.PlayerRace.PluralName;
            gui.InitialiseControls();
            Application.Run(gui);
        }        
    }
}