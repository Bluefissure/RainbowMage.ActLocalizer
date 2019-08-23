using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Reflection;

namespace RainbowMage.ActLocalizer
{
    public class PluginMain : IActPluginV1
    {
        TabPage pluginTab;
        Label statusLabel;
        Config config;
        ConfigPanel configPanel;

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            pluginTab = pluginScreenSpace;
            statusLabel = pluginStatusText;
            pluginTab.Text = "Localizer";

            PopulateConfig();

            if (config.ExportOriginalOnNextBoot)
            {
                Localizer.ExportLocalizerXml();
                config.ExportOriginalOnNextBoot = false;
            }

            PopulateConfigPanel();
            ApplyLocalizer();

            statusLabel.Text = "Initialized.";
        }

        public void DeInitPlugin()
        {
            config.Save(GetConfigPath());
            statusLabel.Text = "Finalized.";
        }

        private void PopulateConfig()
        {
            try
            {
                var configPath = GetConfigPath();
                if (File.Exists(configPath))
                {
                    config = Config.Load(configPath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load config file.\n\n" + e.ToString());
            }

            if (config == null)
            {
                config = new Config();
            }
        }

        private void PopulateConfigPanel()
        {
            configPanel = new ConfigPanel(config);
            configPanel.Dock = DockStyle.Fill;

            configPanel.LocaleChanged += (o, e) =>
            {
                //ApplyLocalizer();
            };

            pluginTab.Controls.Add(configPanel);
        }

        private void ApplyLocalizer()
        {
            if (config.IsEnabled)
            {
                try
                {
                    var localizer = new Localizer(config.Locale);
                    localizer.Localize();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to create or apply localizer.\n\n" + e.ToString());
                }
            }
        }

        private static string GetConfigPath()
        {
            var path = Path.Combine(
                ActGlobals.oFormActMain.AppDataFolder.FullName,
                "Config",
                "RainbowMage.ActLocalizer.config.xml");

            return path;
        }
    }


}
