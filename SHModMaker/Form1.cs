using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHModMaker
{
    public partial class Form1 : Form
    {
        public static String fileDialogPath = "c:\\";
        public Form1()
        {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
        }
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void btn_weapon_Click(object sender, EventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine(startupPath);

            System.IO.StreamReader filepath = new System.IO.StreamReader(startupPath + "\\JSONs\\Weapon\\weapon.json");
            string file = filepath.ReadToEnd();
            filepath.Close();

            Console.WriteLine(file);

            //Build parse references
            //including locations of files
            List<String[]> refList = new List<string[]>();
            refList.Add(new String[] { "name", txt_weap_name.Text });
            refList.Add(new String[] { "iname", txt_weap_iname.Text });
            refList.Add(new String[] { "desc", txt_weap_desc.Text });
            refList.Add(new String[] { "QB", txt_weap_qb.Text });
            refList.Add(new String[] { "QBI", txt_weap_qbi.Text });
            refList.Add(new String[] { "PNG", txt_weap_png.Text });
            refList.Add(new String[] { "ilevel", nud_weap_ilevel.Value.ToString() });
            refList.Add(new String[] { "damage", nud_weap_damage.Value.ToString() });
            refList.Add(new String[] { "reach", nud_weap_reach.Value.ToString() });

            //Open config

            //Get JSONs and LUA files

            //Parse files

            String newFile = utils.Parse(file, refList);

            Console.WriteLine(newFile);

            //Make folder

            //Copy QBs & PNG

            //Save Files

            //Update Manifest

            //Save Manifest
        }

        private void btn_weap_QBBrowse_Click(object sender, EventArgs e)
        {
            txt_weap_qb.Text = utils.getDialogPath("qb", fileDialogPath);
        }

        private void btn_weap_QBIBrowse_Click(object sender, EventArgs e)
        {
            txt_weap_qbi.Text = utils.getDialogPath("qb", fileDialogPath);
        }

        private void btn_weap_PNGBrowse_Click(object sender, EventArgs e)
        {
            txt_weap_png.Text = utils.getDialogPath("png", fileDialogPath);
        }
    }
    public class utils
    {
        public static String Parse(String line, List<String[]> refList)
        {
            String newLine = "";
            String[] stringArray = line.Split('~');

            for (int i = 0; i < stringArray.Length; i++)
            {
                bool replaced = false;
                for (int ii = 0; ii < refList.Count; ii++)
                {
                    if (stringArray[i] == refList[ii][0])
                    {
                        newLine = newLine + refList[ii][1];
                        replaced = true;
                        break;
                    }
                }
                if (replaced == false)
                {
                    newLine = newLine + stringArray[i];
                }
            }

            return newLine;
        }

        public static String getDialogPath(String fileType)
        {
            return getDialogPath(fileType, "c:\\");
        }
        public static String getDialogPath(String fileType, String dir)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = dir;
            openFileDialog1.Filter = fileType + " files(*." + fileType + ")|*." + fileType + "|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form1.fileDialogPath = openFileDialog1.FileName;
                return openFileDialog1.FileName;
            }
            else
            {
                return "";
            }
        }

        public static void Debug()
        {

        }
    }
}

