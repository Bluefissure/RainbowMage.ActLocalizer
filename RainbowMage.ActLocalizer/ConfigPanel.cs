using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RainbowMage.ActLocalizer
{
    public partial class ConfigPanel : UserControl
    {
        private bool initializing = true;

        public Config Config { get; set; }
        public event EventHandler LocaleChanged;

        public ConfigPanel(Config config)
        {
            this.Config = config;
            InitializeComponent();
            UpdateComboLocale();

            initializing = false;
        }

        private void UpdateComboLocale()
        {
            var index = comboLocale.Items.Add("en-US");


            if (Directory.Exists("locale"))
            {
                foreach (var dirPath in Directory.GetDirectories("locale"))
                {
                    var dirInfo = new DirectoryInfo(dirPath);
                    if (dirInfo.Name == "en-US") { continue; }
                    var i = comboLocale.Items.Add(dirInfo.Name);
                    if (Config.Locale == dirInfo.Name)
                    {
                        index = i;
                    }
                }
            }

            comboLocale.SelectedIndex = index;
        }

        private void comboLocale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                Config.Locale = (string)comboLocale.SelectedItem;
                if (LocaleChanged != null) LocaleChanged(this, EventArgs.Empty);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Config.ExportOriginalOnNextBoot = true;
            buttonExport.Enabled = false;
        }

        private void buttonExportRN_Click(object sender, EventArgs e)
        {
            Localizer.ExportPluginsLocalizerXml();
        }
    }
}
