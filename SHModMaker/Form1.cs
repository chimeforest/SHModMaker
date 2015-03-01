using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHModMaker
{
    public partial class Form1 : Form
    {
        public static String fileDialogPath = "c:\\";
        public static MOD mod = new MOD();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            mod.manifest = new ManifestJSON();
        }

        private void btn_weapon_Click(object sender, EventArgs e)
        {
            String iname = utils.LowerNoSpaces(txt_weap_name.Text);
            string localPath = System.IO.Directory.GetCurrentDirectory();

            String[] filesAll;
            List<String[]> filesJsonLua = new List<string[]>();

            Weapon weapon = new Weapon(txt_weap_name.Text, txt_weap_desc.Text, (int)nud_weap_ilevel.Value, (int)nud_weap_damage.Value, (float)nud_weap_reach.Value);

            //Get ByteArrays from QBs and PNG
            if (System.IO.File.Exists(txt_weap_qb.Text))
            {
                weapon.qb = File.ReadAllBytes(txt_weap_qb.Text);
            }
            else { Console.WriteLine(txt_weap_qb.Text + " Does not exsist"); }
            if (System.IO.File.Exists(txt_weap_qbi.Text))
            {
                weapon.qbi = File.ReadAllBytes(txt_weap_qbi.Text);
            }
            else { Console.WriteLine(txt_weap_qbi.Text + " Does not exsist"); }
            if (System.IO.File.Exists(txt_weap_png.Text))
            {
                weapon.png = File.ReadAllBytes(txt_weap_png.Text);
            }
            else { Console.WriteLine(txt_weap_png.Text + " Does not exsist"); }


            //Build parse references
            //including locations of files
            //List<String[]> refList = new List<string[]>();
            //refList.Add(new String[] { "name", txt_weap_name.Text });
            //refList.Add(new String[] { "iname", iname });
            //refList.Add(new String[] { "desc", txt_weap_desc.Text });
            //refList.Add(new String[] { "ilevel", nud_weap_ilevel.Value.ToString() });
            //refList.Add(new String[] { "damage", nud_weap_damage.Value.ToString() });
            //refList.Add(new String[] { "reach", nud_weap_reach.Value.ToString() });

            //Open config

            //Get JSONs and LUA files
            //filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\Weapon\\");
            //foreach (String str in filesAll)
            //{
            //    if (str.EndsWith(".json") || str.EndsWith(".lua") || str.EndsWith(".luac"))
            //    {
            //        //Parse files
            //        System.IO.StreamReader filepath = new System.IO.StreamReader(str);
            //        filesJsonLua.Add(new String[] { System.IO.Path.GetFileName(str), utils.Parse(filepath.ReadToEnd(), refList) });
            //        filepath.Close();// But now I don't know which files are which =/ FIX
            //    }
            //}

            //Make folder
            //if (!System.IO.Directory.Exists(localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname))
            //{
            //    System.IO.Directory.CreateDirectory(localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname);
            //    Console.WriteLine(localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname);
            //}

            ////Copy QBs & PNG
            //if (System.IO.File.Exists(txt_weap_qb.Text))
            //{
            //    System.IO.File.Copy(txt_weap_qb.Text, localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname + "\\" + iname + ".qb", true);
            //}
            //else { Console.WriteLine(txt_weap_qb.Text + " Does not exsist"); }
            //if (System.IO.File.Exists(txt_weap_qbi.Text))
            //{
            //    System.IO.File.Copy(txt_weap_qbi.Text, localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname + "\\" + iname + "_iconic.qb", true);
            //}
            //else { Console.WriteLine(txt_weap_qbi.Text + " Does not exsist"); }
            //if (System.IO.File.Exists(txt_weap_png.Text))
            //{
            //    System.IO.File.Copy(txt_weap_png.Text, localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname + "\\" + iname + ".png", true);
            //}
            //else { Console.WriteLine(txt_weap_png.Text + " Does not exsist"); }

            ////Save JSON and LUA Files
            //foreach (String[] str in filesJsonLua)
            //{
            //    String name = str[0];
            //    if (name.ToLower().Contains("weapon"))
            //    {
            //        name = name.ToLower().Replace("weapon", iname);
            //    }
            //    System.IO.File.WriteAllText(localPath + "\\" + txt_mod_name.Text + "\\entities\\weapons\\" + iname + "\\" + name, str[1], Encoding.ASCII);
            //}


            //Update Manifest
            mod.name = txt_mod_name.Text;
            mod.manifest.version = txt_api_version.Text;

            //Add weapon to MOD
            mod.AddWeapon(weapon);
            //mod.manifest.AddAlias("weapon:" + iname, "file(entities/weapons/" + iname + ")");
            //Save Manifest
            //System.IO.File.WriteAllText(localPath + "\\" + txt_mod_name.Text + "\\" + "manifest.json", mod.manifest.Get(), Encoding.ASCII);

            //Build MOD .. remove this later
            mod.BuildMod(localPath + "\\");
            mod.SaveMod(localPath + "\\");
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

        private void txt_KeyPress_NoSpecChar(object sender, KeyPressEventArgs e)
        {
            e.Handled = utils.NoSpecChar(e);
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

        public static bool NoSpecChar(KeyPressEventArgs e)
        {
            bool b = false;
            var regex = new Regex(@"[^a-zA-Z0-9\s_\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                b = true;
            }
            return b;
        }

        public static String LowerNoSpaces(String str)
        {
            string newStr = "";
            newStr = str.Replace(' ', '_');
            newStr = newStr.ToLower();
            return newStr;
        }

        public static void Debug()
        {

        }
    }

    public class ManifestJSON
    {
        public String name;
        public String version;
        public List<String[]> aliases = new List<string[]>();

        public ManifestJSON()
        {
            name = "";
            version = "";
        }
        public ManifestJSON(String n, String v)
        {
            name = n;
            version = v;
        }

        public String Get()
        {
            String maniFile = "";

            //Add info section
            maniFile = "{\n\t\"info\" : {\n\t\t\"name\" : \"" + name + "\",\n\t\t\"version\" : " + version + "\n\t}";

            //Add aliases
            if (aliases.Count != 0)
            {
                maniFile = maniFile + ",\n\n\t\"aliases\" : {";
                for (int i = 0; i < aliases.Count; i++)
                {
                    maniFile = maniFile + "\n\t\t\"" + aliases[i][0] + "\" : \"" + aliases[i][1] + "\"";
                    if (i != aliases.Count - 1)
                    {
                        maniFile = maniFile + ",";
                    }
                }
                maniFile = maniFile + "\n\t}";
            }

            //Add closing bracket
            maniFile = maniFile + "\n}";

            return maniFile;
        }

        public void Write(String modPath)
        {

            System.IO.File.WriteAllText(modPath + "\\" + "manifest.json", Get(), Encoding.ASCII);
        }

        public void AddAlias(String alias, string data)
        //Adds Alias to aliases List. If Alias already exsists, it updates the data
        {
            bool replaced = false;
            for (int i = 0; i < aliases.Count; i++)
            {
                if (aliases[i][0] == alias)
                {
                    aliases[i][1] = data;
                    replaced = true;
                    break;
                }
            }
            if (replaced == false)
            {
                aliases.Add(new String[] { alias, data });
            }
        }
        public void RemoveAlias(String alias)
        //searches for and removes an alias from the List
        {
            for (int i = 0; i < aliases.Count; i++)
            {
                if (aliases[i][0] == alias)
                {
                    aliases.RemoveAt(i);
                    break;
                }
            }
        }
        public string GetAliasData(String alias)
        //searches for and returns the data of an alias, if alias does not exsist "" is returned instead.
        {
            string data = "";
            for (int i = 0; i < aliases.Count; i++)
            {
                if (aliases[i][0] == alias)
                {
                    data = aliases[i][1];
                }
            }
            return data;
        }
    }

    public class Recipe
    {
        public String name;

        public Recipe()
        {
            name = "";
        }
    }

    public class Weapon
    {
        public String name;
        public String iname;
        public String desc;
        public int ilevel;
        public int damage;
        public float reach;
        public byte[] qb;
        public byte[] qbi;
        public byte[] png;
        public String custom1;
        public String custom2;
        public String custom3;

        public Weapon()
            : this("", "", 0, 0, 0.0f, new Byte[1], new Byte[1], new Byte[1]){ }

        public Weapon(String nam, String des, int ilvl, int dam, float rea)
            : this(nam, des, ilvl, dam, rea, new Byte[1], new Byte[1], new Byte[1]){ }

        public Weapon(String nam, String des, int ilvl, int dam, float rea, Byte[] q, Byte[] qi, Byte[] p)
        {
            name = nam;
            iname = utils.LowerNoSpaces(nam);
            desc = des;
            ilevel = ilvl;
            damage = dam;
            reach = rea;
            qb = q;
            qbi = qi;
            png = p;
            custom1 = "";
            custom2 = "";
            custom3 = "";
        }

        public void WriteWeaponFile(String modPath)
        {
            string localPath = System.IO.Directory.GetCurrentDirectory();
            String[] filesAll;
            List<String[]> filesJsonLua = new List<string[]>();
            //Build Parse List
            List<String[]> refList = new List<string[]>();
            refList.Add(new String[] { "name", name });
            refList.Add(new String[] { "iname", iname });
            refList.Add(new String[] { "desc", desc });
            refList.Add(new String[] { "ilevel", ilevel.ToString() });
            refList.Add(new String[] { "damage", damage.ToString() });
            refList.Add(new String[] { "reach", reach.ToString() });

            //Get JSONs and LUA files
            //FIX can probably make a utils for this utils.GetJsonLua(String path)??
            filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\Weapon\\");
            foreach (String str in filesAll)
            {
                if (str.EndsWith(".json") || str.EndsWith(".lua") || str.EndsWith(".luac"))
                {
                    //Parse files
                    System.IO.StreamReader filepath = new System.IO.StreamReader(str);
                    filesJsonLua.Add(new String[] { System.IO.Path.GetFileName(str), utils.Parse(filepath.ReadToEnd(), refList) });
                    filepath.Close();
                }
            }

            //make folder if needed
            if (!System.IO.Directory.Exists(modPath + "\\entities\\weapons\\" + iname))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\entities\\weapons\\" + iname);
                Console.WriteLine(modPath + "\\entities\\weapons\\" + iname);
            }

            //write them to new path
            foreach (String[] str in filesJsonLua)
            {
                String fileName = str[0];
                if (fileName.ToLower().Contains("weapon"))
                {
                    fileName = fileName.ToLower().Replace("weapon", iname);
                }
                System.IO.File.WriteAllText(modPath + "\\entities\\weapons\\" + iname + "\\" + fileName, str[1], Encoding.ASCII);
            }

            //write qbs and png to new path
            System.IO.File.WriteAllBytes(modPath + "\\entities\\weapons\\" + iname + "\\" + iname + ".qb", qb);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\weapons\\" + iname + "\\" + iname + "_iconic.qb", qbi);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\weapons\\" + iname + "\\" + iname + ".png", png);
        }
    }
    public class MOD
    {
        public String name;
        public ManifestJSON manifest;
        public List<String> crafters;
        public List<Recipe> recipes;
        public List<Weapon> weapons;

        public MOD()
        {
            name = "";
            manifest = new ManifestJSON();
            crafters = new List<string>();
            recipes = new List<Recipe>();
            weapons = new List<Weapon>();
        }

        public void AddWeapon(Weapon weap)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for(int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].iname == weap.iname)
                {
                    changed = true;
                    weapons[i] = weap;
                }
            }
            if(changed == false)
            {
                weapons.Add(weap);
            }
        }
        public void RemoveWeapon(Weapon weap)
        {
            //Searches for a weapon, if it finds it, then it rmoves it from the list
            RemoveWeapon(weap.name);
        }
        public void RemoveWeapon(String weapName)
        {
            //Searches for a weapon, if it finds it, then it rmoves it from the list
            //FIX
        }

        public void BuildManifest()
        {
            //Add mod name to manifest
            manifest.name = name;

            //Add weapons to manifest
            foreach (Weapon weap in weapons)
            {
                manifest.AddAlias("weapon:" + weap.iname, "file(entities/weapons/" + weap.iname + ")");
            }
            //Add blah to maifest
        }

        public void BuildMod(String modPath)
        {
            BuildManifest();

            //make folder if needed
            if (!System.IO.Directory.Exists(modPath + "\\" + name))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\" + name);
                Console.WriteLine(modPath + "\\" + name);
            }

            //write weapon files
            foreach (Weapon weap in weapons)
            {
                weap.WriteWeaponFile(modPath + name);
            }

            //write manifest
            manifest.Write(modPath + name + "\\");

            //???

            //write mod to .smod
        }

        public void SaveMod(String path)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(path + name + ".shmm",json,Encoding.ASCII);
        }
        public void LoadMod(String path) { }
    }
}

