using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHModMaker
{
    public partial class ConfigForm : Form
    {
        CONFIG currentConfig = new CONFIG(false);
        public ConfigForm()
        {
            InitializeComponent();
        }
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            currentConfig = MainForm.config;
            updateForm();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //add sh smod path
            currentConfig.SHsmodPath = txt_shsmod.Text;

            //add materials
            currentConfig.CommonMaterialTags.Clear();
            foreach (String str in txt_material_tags.Lines)
            {
                if (str != "") { currentConfig.CommonMaterialTags.Add(str); }
            }

            //add skip words
            currentConfig.AliasSkipWords.Clear();
            foreach (String str in txt_alias_skip_words.Lines)
            {
                if (str != "") { currentConfig.AliasSkipWords.Add(str); }
            }

            //tell form1 that the config was just updated
            MainForm.configJustUpdated = true;

            //change folderdialog path
            //Form1.folderBrowserDialog1.SelectedPath = currentConfig.SHsmodPath;

            //save config
            MainForm.config = currentConfig;
            MainForm.config.SAVECONFIG();
            this.Close();
        }
        private void btn_load_Click(object sender, EventArgs e)
        {
            currentConfig = MainForm.config;
            updateForm();
        }
        private void btn_default_Click(object sender, EventArgs e)
        {
            currentConfig = new CONFIG(true);
            updateForm();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateForm()
        {
            txt_shsmod.Text = currentConfig.SHsmodPath;

            lst_SHcrafters.Items.Clear();
            foreach (Crafter crft in currentConfig.SHCrafters)
            {
                lst_SHcrafters.Items.Add(crft.name);
            }

            txt_material_tags.Clear();
            foreach (string str in currentConfig.CommonMaterialTags)
            {
                if (txt_material_tags.Text != "")
                {
                    txt_material_tags.AppendText("\n");
                }
                txt_material_tags.AppendText(str);
            }

            txt_alias_skip_words.Clear();
            foreach (string str in currentConfig.AliasSkipWords)
            {
                if (txt_alias_skip_words.Text != "")
                {
                    txt_alias_skip_words.AppendText("\n");
                }
                txt_alias_skip_words.AppendText(str);
            }
        }

        private void bnt_getSHcrafters_Click(object sender, EventArgs e)
        {
            currentConfig.GetSHCrafters();
            updateForm();
        }

        private void lst_SHcrafters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_crafter_cat.Clear();
            foreach (Crafter crft in currentConfig.SHCrafters)
            {
                //for each crafter in sh crafters, check to see if it matches
                if (crft.IsCrafter("stonehearth:" + lst_SHcrafters.Text))
                {
                    //if it does, then add all the categories from that crafter
                    foreach (String cat in crft.categories)
                    {
                        if (txt_crafter_cat.Text != "")
                        {
                            txt_crafter_cat.AppendText("\n");
                        }
                        txt_crafter_cat.AppendText(cat);
                    }
                }
            }
        }

        private void btn_shsmod_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Title = "Please locate your stonhearth.smod file.";
            filedialog.Filter = "stonehearth.smod|stonehearth.smod";
            filedialog.FilterIndex = 1;
            filedialog.Multiselect = false;

            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                txt_shsmod.Text = filedialog.FileName;
            }
        }
    }
}
