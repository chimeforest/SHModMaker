﻿using Ionic.Zip;
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
        //Directory variables
        public static String localPath = System.IO.Directory.GetCurrentDirectory();
        public static String SHMMPath = "";

        //Config Variable
        public static CONFIG config = new CONFIG();//FIX change to LOADCONFIG after loadconfig method is written

        //Current Item variables
        public static MOD mod = new MOD();
        public static Recipe currentRecipe = new Recipe();
        public static Weapon currentWeapon = new Weapon();

        private static String currentListBox = "";

        //__________Form and FormLoad stuff__________
        public Form1()
        {
            InitializeComponent();
            tabcontrol.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
        }
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabcontrol.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabcontrol.GetTabRect(e.Index);

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
            config = config.LOADCONFIG();
            mod.manifest = new ManifestJSON();
            lbl_status.Text = "Welcome to the Stonehearth Mod Maker by Chimeforest";

        }

        //restricts characters for name fields.
        private void txt_KeyPress_NoSpecChar(object sender, KeyPressEventArgs e)
        {
            e.Handled = utils.NoSpecChar(e);
        }


        //__________MAIN MENU ITEMS__________
        private void newModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mod = new MOD();
            lbl_status.Text = "Blank mod established.";
            updateMODtab();
        }
        private void saveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mod.name != "")
            {
                if (SHMMPath != "")
                {
                    mod.SaveMod(SHMMPath);
                    lbl_status.Text = mod.name + " saved to: " + SHMMPath;
                }
                else
                {
                    if (saveFileDialogSHMM.ShowDialog() == DialogResult.OK)
                    {
                        mod.SaveMod(saveFileDialogSHMM.FileName);
                        SHMMPath = openFileDialogSHMM.FileName;
                        lbl_status.Text = mod.name + " saved to: " + SHMMPath;
                    }
                }
            }
            else
            {
                lbl_status.Text = "CAN NOT SAVE! MOD HAS NO NAME!!";
            }
        }
        private void saveAsModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mod.name != "")
            {
                if (saveFileDialogSHMM.ShowDialog() == DialogResult.OK)
                {
                    mod.SaveMod(saveFileDialogSHMM.FileName);
                    SHMMPath = openFileDialogSHMM.FileName;
                    lbl_status.Text = mod.name + " saved to: " + SHMMPath;
                    //Console.WriteLine(mod.name + " saved to: " + openFileDialog1.FileName);
                }
            }
            else
            {
                lbl_status.Text = "CAN NOT SAVE! MOD HAS NO NAME!!";
            }
            
        }
        private void loadModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogSHMM.ShowDialog() == DialogResult.OK)
            {
                mod = mod.LoadMod(openFileDialogSHMM.FileName);
                SHMMPath = openFileDialogSHMM.FileName;
                lbl_status.Text = mod.name + " loaded from: " + SHMMPath;
                //Console.WriteLine(mod.name + " loaded from: " + openFileDialog1.FileName);
                updateMODtab();

                //if weaponlist != empty then set selected index to 0.. same for recipe
                if (lst_weap.Items.Count > 0)
                {
                    lst_weap.SelectedIndex = 0;
                }
                if (lst_recipe.Items.Count > 0)
                {
                    lst_recipe.SelectedIndex = 0;
                }
            }
        }
        private void exportsmodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mod.name != "")
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    mod.SaveSMOD(folderBrowserDialog1.SelectedPath + "\\" + mod.name + ".smod");
                }
            }
            else
            {
                lbl_status.Text = "CAN NOT EXPORT! MOD HAS NO NAME!!";
            }
        }
        private void exportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mod.name != "")
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    mod.BuildMod(folderBrowserDialog1.SelectedPath + "\\");
                }
            }
            else
            {
                lbl_status.Text = "CAN NOT EXPORT! MOD HAS NO NAME!!";
            }
        }

        //__________MOD TAB__________
        private void tab_MOD_Enter(object sender, EventArgs e)
        {
            updateMODtab();
        }
        private void updateMODtab()
        {
            //update mod/manifest info
            txt_mod_name.Text = mod.name;
            txt_api_version.Text = mod.apiVersion;

            //update weapons list
            lst_weap.Items.Clear();
            foreach (Weapon weap in mod.weapons)
            {
                lst_weap.Items.Add(weap.name);
            }
            //update recipe list
            lst_recipe.Items.Clear();
            foreach (Recipe recp in mod.recipes)
            {
                lst_recipe.Items.Add(recp.name);
            }
        }
        private void txt_mod_name_TextChanged(object sender, EventArgs e)
        {
            mod.name = txt_mod_name.Text;
            mod.manifest.name = txt_mod_name.Text;
        }
        private void txt_api_version_TextChanged(object sender, EventArgs e)
        {
            mod.apiVersion = txt_api_version.Text;
        }

        //lst_weapon stuff
        private void lst_weap_MouseDown(object sender, MouseEventArgs e)
        {
            lst_weap.SelectedIndex = lst_weap.IndexFromPoint(e.X, e.Y);
            currentListBox = "weapons";
        }
        private void lst_weap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_weap.SelectedItem != null)
            {
                foreach (Weapon weap in mod.weapons)
                {
                    if (weap.name == lst_weap.SelectedItem.ToString())
                    {
                        pic_mod_weapon.Image = Image.FromStream(new MemoryStream(weap.png));
                        pic_mod_weapon.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_weapon.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
            }
        }

        //lst_recipe stuff
        private void lst_recipe_MouseDown(object sender, MouseEventArgs e)
        {
            lst_recipe.SelectedIndex = lst_recipe.IndexFromPoint(e.X, e.Y);
            currentListBox = "recipes";
        }
        private void lst_recipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_recipe.SelectedItem != null)
            {
                foreach (Recipe recp in mod.recipes)
                {
                    if (recp.name == lst_recipe.SelectedItem.ToString())
                    {
                        //FIX : figure out how to do recipe icon...
                        //pic_recipe.Image = Image.FromStream(new MemoryStream(recp.png));
                        //pic_recipe.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_recipe.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
            }
        }

        //context menu for listboxs
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentListBox == "weapons")
            {
                currentWeapon = new Weapon();
                tabcontrol.SelectedIndex = tabcontrol.TabPages.IndexOfKey("tab_weapon");
            }
            if (currentListBox == "recipes")
            {
                currentRecipe = new Recipe();
                tabcontrol.SelectedIndex = tabcontrol.TabPages.IndexOfKey("tab_recipe");
            }
            
        }
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentListBox == "weapons")
            {
                foreach (Weapon weap in mod.weapons)
                {
                    if (weap.name == lst_weap.SelectedItem.ToString())
                    {
                        currentWeapon = weap;
                        break;
                    }
                }
                tabcontrol.SelectedIndex = tabcontrol.TabPages.IndexOfKey("tab_weapon");
            }
            if (currentListBox == "recipes")
            {
                foreach (Recipe recp in mod.recipes)
                {
                    if (recp.name == lst_recipe.SelectedItem.ToString())
                    {
                        currentRecipe = recp;
                        break;
                    }
                }
                tabcontrol.SelectedIndex = tabcontrol.TabPages.IndexOfKey("tab_recipe");
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentListBox == "weapons")
            {
                foreach (Weapon weap in mod.weapons)
                {
                    if (weap.name == lst_weap.SelectedItem.ToString())
                    {
                        mod.weapons.Remove(weap);
                        break;
                    }
                }
                updateMODtab();
            }
            if (currentListBox == "recipes")
            {
                foreach (Recipe recp in mod.recipes)
                {
                    if (recp.name == lst_recipe.SelectedItem.ToString())
                    {
                        mod.recipes.Remove(recp);
                        break;
                    }
                }
                updateMODtab();
            }
        }

        //__________Weapon Tab Stuff__________
        private void tab_weapon_Enter(object sender, EventArgs e)
        {
            txt_weap_name.Text = currentWeapon.name;
            txt_weap_desc.Text = currentWeapon.desc;
            nud_weap_ilevel.Value = currentWeapon.ilevel;
            nud_weap_damage.Value = currentWeapon.damage;
            nud_weap_reach.Value = (decimal)currentWeapon.reach;

            pic_weap_qb.Image = Image.FromStream(new MemoryStream(currentWeapon.qbICON));
            pic_weap_qb.Refresh();

            pic_weap_qbi.Image = Image.FromStream(new MemoryStream(currentWeapon.qbiICON));
            pic_weap_qbi.Refresh();

            pic_weap_png.Image = Image.FromStream(new MemoryStream(currentWeapon.png));
            pic_weap_png.Refresh();
        }

        private void pic_weap_qb_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentWeapon.qb = File.ReadAllBytes(openFileDialogQB.FileName);
                //currentWeapon.qbICON = HAL.thumbnailer(openFileDialogQB.FileName);
                //pic_weap_qb.Image = Image.FromStream(new MemoryStream(currentWeapon.qbICON));
                //pic_weap_qb.Refresh();
            }
        }
        private void pic_weap_qbi_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentWeapon.qbi = File.ReadAllBytes(openFileDialogQB.FileName);
                //currentWeapon.qbiICON = HAL.thumbnailer(openFileDialogQB.FileName);
                //pic_weap_qbi.Image = Image.FromStream(new MemoryStream(currentWeapon.qbiICON));
                //pic_weap_qbi.Refresh();
            }
        }
        private void pic_weap_png_Click(object sender, EventArgs e)
        {
            if (openFileDialogPNG.ShowDialog() == DialogResult.OK)
            {
                currentWeapon.png = File.ReadAllBytes(openFileDialogPNG.FileName);
                pic_weap_png.Image = Image.FromStream(new MemoryStream(currentWeapon.png));
                pic_weap_png.Refresh();
            }
        }
        private void txt_weap_changed(object sender, EventArgs e)
        {
            currentWeapon.name = txt_weap_name.Text;
            currentWeapon.iname = utils.LowerNoSpaces(currentWeapon.name);
            currentWeapon.desc = txt_weap_desc.Text;
            currentWeapon.ilevel = (int)nud_weap_ilevel.Value;
            currentWeapon.damage = (int)nud_weap_damage.Value;
            currentWeapon.reach = (float)nud_weap_reach.Value;
        }
        private void btn_weapon_Click(object sender, EventArgs e)
        {
            //String iname = utils.LowerNoSpaces(txt_weap_name.Text);
            //String localPath = System.IO.Directory.GetCurrentDirectory();

            //currentWeapon = new Weapon(txt_weap_name.Text, txt_weap_desc.Text, (int)nud_weap_ilevel.Value, (int)nud_weap_damage.Value, (float)nud_weap_reach.Value);

            //Add weapon to MOD
            if (currentWeapon.name != "")
            {
                mod.AddWeapon(currentWeapon);
                tabcontrol.SelectedIndex = 0;
                lbl_status.Text = "Weapon " + currentWeapon.name + " has been added/updated.";
            }
            else
            {
                lbl_status.Text = "Weapon has no name! Could not add/update weapon.";
            }


        }

        //__________Recipe Tab Stuff__________
        private void tab_recipe_Enter(object sender, EventArgs e)
        {
            txt_recp_name.Text = currentRecipe.name;
            txt_recp_desc.Text = currentRecipe.Desc;
            txt_recp_flavor.Text = currentRecipe.Flavor;
            nud_recipe_level.Value = currentRecipe.level;
            nud_recipe_work.Value = currentRecipe.workTime;
            //crafter
            //produces
            //ingredients
            //image
        }

        private void chk_recipe_lockimg_CheckedChanged(object sender, EventArgs e)
        {
            pic_recipe.Enabled = !chk_recipe_lockimg.Checked;
        }

        private void pic_recipe_Click(object sender, EventArgs e)
        {

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
    }

    public class ManifestJSON
    {
        public String name;
        public String apiVersion;
        public List<String[]> aliases = new List<string[]>();

        public ManifestJSON()
        {
            name = "";
            apiVersion = "1";
        }
        public ManifestJSON(String n, String v)
        {
            name = n;
            apiVersion = v;
        }

        public String Get()
        {
            String maniFile = "";

            //Add info section
            maniFile = "{\n\t\"info\" : {\n\t\t\"name\" : \"" + name + "\",\n\t\t\"version\" : " + apiVersion + "\n\t}";

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
        public int workTime;
        public int level;
        public String crafter;
        public String Desc;
        public String Flavor;
        public byte[] png;
        public List<AmountOfItem> Ingredients;
        public List<AmountOfItem> Produces;

        public Recipe()
        {
            name = "";
            workTime = 2;
            level = 0;
            crafter = "";
            Desc = "";
            Flavor = "";
            png = File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png");
            Ingredients = new List<AmountOfItem>();
            Produces = new List<AmountOfItem>();
        }
    }

    public class Crafter
    {
        public String mod;
        public String name;
        public List<Recipe> recipes;//????
    }

    public class AmountOfItem
    {
        public String name;
        public int amount;

        AmountOfItem()
        {
            name = "";
            amount = 1;
        }
        AmountOfItem(String nam, int amt)
        {
            name = nam;
            amount = amt;
        }

        public String GetString()
        {
            return (amount.ToString() + "," + name);
        }
        public AmountOfItem FromString(String str)
        {
            return new AmountOfItem(str.Split(',')[1],int.Parse(str.Split(',')[1]));
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
        public byte[] qbICON;
        public byte[] qbiICON;

        public String custom1;
        public String custom2;
        public String custom3;

        public Weapon()
            : this("", "", 0, 0, 0.0f, File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.qb"), 
                  File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.qb"), 
                  File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"), 
                  File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"), 
                  File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"))
        { }

        public Weapon(String nam, String des, int ilvl, int dam, float rea)
            : this(nam, des, ilvl, dam, rea, File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.qb"), File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.qb"), File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"), File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"), File.ReadAllBytes(Form1.localPath + "\\Configs\\blank.png"))
        { }

        public Weapon(String nam, String des, int ilvl, int dam, float rea, Byte[] q, Byte[] qi, Byte[] p, Byte[] qbIC, Byte[] qbiIC)
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
            qbICON = qbIC;
            qbiICON = qbiIC;
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
        public String apiVersion;
        public ManifestJSON manifest;
        public List<String> crafters;
        public List<Recipe> recipes;
        public List<Weapon> weapons;

        public MOD()
        {
            name = "";
            apiVersion = "1";
            manifest = new ManifestJSON();
            crafters = new List<string>();
            recipes = new List<Recipe>();
            weapons = new List<Weapon>();
        }
        public MOD(string nam)
        {
            name = nam;
            apiVersion = "1";
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
            weapons.Remove(weap);
        }

        public void BuildManifest()
        {
            manifest.name = name;
            manifest.apiVersion = apiVersion;
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

        }
        public void SaveSMOD(String smodPath)
        {
            BuildMod(Form1.localPath + "\\");

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Form1.localPath + "\\" + name, name);
                zip.Save(smodPath);
            }

            System.IO.Directory.Delete(Form1.localPath + "\\" + name, true);
        }
        public void SaveMod(String filePath)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json, Encoding.ASCII);
        }
        public MOD LoadMod(String filePath)
        {
            return JsonConvert.DeserializeObject<MOD>(System.IO.File.ReadAllText(filePath));
        }

    }

    public class CONFIG
    {
        public string SHsmodPath;

        public CONFIG()
        {
            if (System.IO.Directory.Exists("C:\\Program Files (x86)"))
            {
                SHsmodPath = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Stonehearth\\mods\\stonehearth.smod";
            }
            else
            {
                SHsmodPath = "C:\\Program Files\\Steam\\SteamApps\\common\\Stonehearth\\mods\\stonehearth.smod";
            }
        }
        public void SAVECONFIG()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(Form1.localPath + "\\Configs\\config.json", json, Encoding.ASCII);
        }
        public CONFIG LOADCONFIG()
        {
            return JsonConvert.DeserializeObject<CONFIG>(System.IO.File.ReadAllText(Form1.localPath + "\\Configs\\config.json"));
        }
    }
}
