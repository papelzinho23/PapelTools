using System;
namespace _4RTools
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            // PapelTools fork: skip the self-updater (AutoPatcher). It pulled
            // releases from the upstream 4RTools repo and would overwrite this
            // build. Start straight at the supported-servers loader instead.
            //Forms.AutoPatcher app = new Forms.AutoPatcher();
            Forms.ClientUpdaterForm app = new Forms.ClientUpdaterForm();
            System.Windows.Forms.Application.Run(app);
        }
    }
}
