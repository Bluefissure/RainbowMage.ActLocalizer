using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RainbowMage.ActLocalizer
{
    public class Localizer
    {
        public string Locale { get; set; }

        public Localizer(string locale)
        {
            this.Locale = locale;
        }

        private static Dictionary<string, Type> formsToLocalize = new Dictionary<string, Type>()
        {
            { "oFormActMain", typeof(FormActMain) },
            { "oFormAlliesEdit", typeof(FormAlliesEdit) },
            { "oFormAvoidanceReport", typeof(FormAvoidanceReport) },
            { "oFormByCombatantLookup", typeof(FormByCombatantLookup) },
            { "oFormCombatantSearch", typeof(FormCombatantSearch) },
            { "oFormEncounterLogs", typeof(FormEncounterLogs) },
            { "oFormEncounterVcr", typeof(FormEncounterVcr) },
            { "oFormExportFormat", typeof(FormExportFormat) },
            { "oFormGetPlugins", typeof(FormGetPlugins) },
            { "oFormImportProgress", typeof(FormImportProgress) },
            { "oFormMiniParse", typeof(FormMiniParse) },
            { "oFormPerformanceWizard", typeof(FormPerformanceWizard) },
            { "oFormResistsDeathReport", typeof(FormResistsDeathReport) },
            { "oFormSpellRecastCalc", typeof(FormSpellRecastCalc) },
            { "oFormSpellTimers", typeof(FormSpellTimers) },
            { "oFormSpellTimersPanel", typeof(FormSpellTimersPanel) },
            { "oFormSpellTimersPanel2", typeof(FormSpellTimersPanel) },
            { "oFormSqlQuery", typeof(FormSqlQuery) },
            { "oFormStartupWizard", typeof(FormStartupWizard) },
            { "oFormTimeLine", typeof(FormTimeLine) },
            { "oFormUpdater", typeof(FormUpdater) },
            { "oFormXmlSettingsIO", typeof(FormXmlSettingsIO) },
        };

        public void Localize()
        {
            try
            {
                LocalizeForms();
                LocalizeConfigTreeView();
                LocalizeIOTreeView();
                LocalizeMisc();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to apply localization file.\n\n" + e.ToString());
            }
        }

        private void LocalizeForms()
        {
            foreach (var pair in formsToLocalize)
            {
                try
                {
                    var xmlPath = GetLocalizerXmlPath(this.Locale, pair.Value.Name + ".xml");

                    var formInfo = typeof(ActGlobals).GetField(pair.Key, BindingFlags.Public | BindingFlags.Static);
                    var importInfo = pair.Value.GetMethod("ImportControlTextXML", new Type[] { typeof(string) });
                    importInfo.Invoke(formInfo.GetValue(null), new object[] { xmlPath });
                }
                catch
                {
                    continue;
                }
            }
        }

        private void LocalizeConfigTreeView()
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance;
            var tvOptionsInfo = typeof(FormActMain).GetField("tvOptions", bindingFlags);
            var optionsControlSetsInfo = typeof(FormActMain).GetField("optionsControlSets", bindingFlags);

            var tvOptions = tvOptionsInfo.GetValue(ActGlobals.oFormActMain) as TreeView;
            var optionsControlSets = optionsControlSetsInfo.GetValue(ActGlobals.oFormActMain) as Dictionary<string, List<Control>>;

            var serializer = new XmlSerializer(typeof(List<TreeViewTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath(this.Locale, "ConfigTreeView.xml"), FileMode.Open, FileAccess.Read))
            {
                var list = serializer.Deserialize(stream) as List<TreeViewTranslationEntry>;

                foreach (TreeNode node in tvOptions.Nodes)
                {
                    ApplyTranslationsForNodes(node, list);
                }

                var newOptionControlSets = new Dictionary<string, List<Control>>();
                foreach (var pair in optionsControlSets)
                {
                    var translated = "";
                    foreach (var eng in pair.Key.Split('\\'))
                    {
                        var query = list.Where(x => x.Original == eng);
                        if (query.Any())
                        {
                            translated += query.First().Translated;
                        }
                        else
                        {
                            translated += eng;
                        }
                        translated += "\\";
                    }
                    translated = translated.TrimEnd('\\');

                    newOptionControlSets.Add(translated, pair.Value);
                }

                optionsControlSetsInfo.SetValue(ActGlobals.oFormActMain, newOptionControlSets);
            }
        }

        private void LocalizeIOTreeView()
        {
            //return;
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance;
            var tvIoInfo = typeof(FormActMain).GetField("tvIo", bindingFlags);
            var ioControlSets = typeof(FormActMain).GetField("ioControlSets", bindingFlags);

            var tvIo = tvIoInfo.GetValue(ActGlobals.oFormActMain) as TreeView;
            var optionsControlSets = ioControlSets.GetValue(ActGlobals.oFormActMain) as Dictionary<string, List<Control>>;

            var serializer = new XmlSerializer(typeof(List<TreeViewTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath(this.Locale, "IoTreeView.xml"), FileMode.Open, FileAccess.Read))
            {
                var list = serializer.Deserialize(stream) as List<TreeViewTranslationEntry>;

                foreach (TreeNode node in tvIo.Nodes)
                {
                    ApplyTranslationsForNodes(node, list);
                }

                var newOptionControlSets = new Dictionary<string, List<Control>>();
                foreach (var pair in optionsControlSets)
                {
                    var translated = "";
                    foreach (var eng in pair.Key.Split('\\'))
                    {
                        var query = list.Where(x => x.Original == eng);
                        if (query.Any())
                        {
                            translated += query.First().Translated;
                        }
                        else
                        {
                            translated += eng;
                        }
                        translated += "\\";
                    }
                    translated = translated.TrimEnd('\\');

                    newOptionControlSets.Add(translated, pair.Value);
                }

                ioControlSets.SetValue(ActGlobals.oFormActMain, newOptionControlSets);
            }
        }

        private void LocalizeMisc()
        {
            var serializer = new XmlSerializer(typeof(List<MiscTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath(this.Locale, "Misc.xml"), FileMode.Open, FileAccess.Read))
            {
                var list = serializer.Deserialize(stream) as List<MiscTranslationEntry>;

                foreach (var entry in list)
                {
                    if (ActGlobals.ActLocalization.LocalizationStrings.ContainsKey(entry.Key))
                    {
                        ActGlobals.ActLocalization.LocalizationStrings[entry.Key].DisplayedText = entry.DisplayText;
                    }
                }
            }
        }

        private static void ApplyTranslationsForNodes(TreeNode node, List<TreeViewTranslationEntry> list)
        {
            var query = list.Where(x => x.Original == node.Text);
            if (query.Any())
            {
                node.Text = query.First().Translated;
            }

            foreach (TreeNode child in node.Nodes)
            {
                ApplyTranslationsForNodes(child, list);
            }
        }

        public static void ExportLocalizerXml()
        {
            // Forms
            foreach (var pair in formsToLocalize)
            {
                Directory.CreateDirectory(GetLocalizerXmlPath("en-US", ""));
                var xmlPath = GetLocalizerXmlPath("en-US", pair.Value.Name + ".xml");

                var formInfo = typeof(ActGlobals).GetField(pair.Key, BindingFlags.Public | BindingFlags.Static);
                var importInfo = pair.Value.GetMethod("ExportControlTextXML", new Type[] { typeof(string) });
                importInfo.Invoke(formInfo.GetValue(null), new object[] { xmlPath });
            }
            

            // Configuration tree view
            var tvOptionsInfo = typeof(FormActMain).GetField("tvOptions", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance);
            var tvOptions = tvOptionsInfo.GetValue(ActGlobals.oFormActMain) as TreeView;

            var treeViewList = new List<TreeViewTranslationEntry>();

            foreach (TreeNode node in tvOptions.Nodes)
            {
                BuildTreeNodeTranslationEntryList(node, treeViewList);
            }

            var treeViewSerializer = new XmlSerializer(typeof(List<TreeViewTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath("en-US", "ConfigTreeView.xml"), FileMode.Create, FileAccess.Write))
            {
                treeViewSerializer.Serialize(stream, treeViewList);
            }

            // ImExport Configuration tree view
            var tvIoInfo = typeof(FormActMain).GetField("tvIo", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance);
            var tvIo = tvIoInfo.GetValue(ActGlobals.oFormActMain) as TreeView;

            var ioTreeViewList = new List<TreeViewTranslationEntry>();

            foreach (TreeNode node in tvIo.Nodes)
            {
                BuildTreeNodeTranslationEntryList(node, ioTreeViewList);
            }

            var ImExtreeViewSerializer = new XmlSerializer(typeof(List<TreeViewTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath("en-US", "IoTreeView.xml"), FileMode.Create, FileAccess.Write))
            {
                treeViewSerializer.Serialize(stream, ioTreeViewList);
            }

            // Misc
            var miscList = new List<MiscTranslationEntry>();

            foreach (var pair in ActGlobals.ActLocalization.LocalizationStrings)
            {
                var entry = new MiscTranslationEntry
                {
                    Key = pair.Key,
                    Description = pair.Value.LocalizationDescription,
                    DisplayText = pair.Value.DisplayedText,
                };

                miscList.Add(entry);
            }

            var miscSerializer = new XmlSerializer(typeof(List<MiscTranslationEntry>));
            using (var stream = new FileStream(GetLocalizerXmlPath("en-US", "Misc.xml"), FileMode.Create, FileAccess.Write))
            {
                miscSerializer.Serialize(stream, miscList);
            }
        }

        public static void BuildTreeNodeTranslationEntryList(TreeNode node, List<TreeViewTranslationEntry> list)
        {
            var query = list.Where(x => x.Original == node.Text);
            if (!query.Any())
            {
                list.Add(new TreeViewTranslationEntry { Original = node.Text, Translated = node.Text });
            }

            foreach (TreeNode child in node.Nodes)
            {
                BuildTreeNodeTranslationEntryList(child, list);
            }
        }

        private static string GetLocalizerXmlPath(string locale, string fileName)
        {
            return Path.Combine("locale", locale, fileName);
        }
    }

    [Serializable]
    public class TreeViewTranslationEntry
    {
        [XmlAttribute]
        public string Original { get; set; }
        [XmlText]
        public string Translated { get; set; }
    }

    [Serializable]
    public class MiscTranslationEntry
    {
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public string Key { get; set; }
        [XmlText]
        public string DisplayText { get; set; }
    }
}
