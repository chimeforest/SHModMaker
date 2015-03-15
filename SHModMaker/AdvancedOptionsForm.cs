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
    public partial class AdvancedOptionsForm : Form
    {
        public AdvancedOptionsForm()
        {
            InitializeComponent();
        }

        private void AdvancedOptionsForm_Load(object sender, EventArgs e)
        {
            //Load stuff into form
            foreach (String[] str in MainForm.mod.extraAliases)
            {
                txt_alias.AppendText(str[0] + "|" + str[1]);
            }
            foreach (String[] str in MainForm.mod.extraMixintos)
            {
                txt_mixinto.AppendText(str[0] + "|" + str[1]);
            }
            foreach (String[] str in MainForm.mod.extraOverrides)
            {
                txt_override.AppendText(str[0] + "|" + str[1]);
            }
            txt_extFolder.Text = MainForm.mod.OutsideFolderPath;
        }

        private void btn_fin_Click(object sender, EventArgs e)
        {
            //save stuff to mod
            MainForm.mod.extraAliases.Clear();
            MainForm.mod.extraMixintos.Clear();
            MainForm.mod.extraOverrides.Clear();

            foreach (String str in txt_alias.Lines)
            {
                if (str.Contains('|'))
                    MainForm.mod.extraAliases.Add(new String[] { str.Split('|')[0], str.Split('|')[1] });
            }
            foreach (String str in txt_mixinto.Lines)
            {
                if (str.Contains('|'))
                    MainForm.mod.extraMixintos.Add(new String[] { str.Split('|')[0], str.Split('|')[1] });
            }
            foreach (String str in txt_override.Lines)
            {
                if (str.Contains('|'))
                    MainForm.mod.extraOverrides.Add(new String[] { str.Split('|')[0], str.Split('|')[1] });
            }

            MainForm.mod.OutsideFolderPath = txt_extFolder.Text;

            this.Close();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            HowToForm how = new HowToForm("adv");
            how.Show();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_broswe_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = MainForm.SHMMPath;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_extFolder.Text = (folderBrowserDialog1.SelectedPath);
            }
        }
    }
}
