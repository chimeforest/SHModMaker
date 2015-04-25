using Ionic.Zip;
using Newtonsoft.Json;
using QbBreeze;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHModMaker
{
    public partial class MainForm : Form
    {
        //Version of this Program
        public String version = "0.15.0";

        //Directory variables
        public static String localPath = System.IO.Directory.GetCurrentDirectory();
        public static String SHMMPath = "";

        //Config Variable
        public static CONFIG config = new CONFIG(false);
        public static bool canRenderQB = false;
        public static bool configJustUpdated = false;

        //Current Item variables
        public static MOD mod = new MOD();
        public static Recipe currentRecipe = new Recipe();
        public static Armor currentArmor = new Armor();
        public static Construction currentConsturction = new Construction();
        public static Flower currentFlower = new Flower();
        public static Weapon currentWeapon = new Weapon();

        private static bool TabLoading = false;
        private static String currentListBox = "";

        //__________Form and FormLoad stuff__________
        public MainForm()
        {
            InitializeComponent();
            tabControl.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
        }
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl.GetTabRect(e.Index);

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
            lbl_status.Text = "Welcome to the Stonehearth Mod Maker by Chimeforest";

            if (System.IO.File.Exists(MainForm.localPath + "\\Configs\\config.json"))
            {
                config = config.LOADCONFIG();
                if (!System.IO.File.Exists(config.SHsmodPath))
                {
                    config.GetSHsmodPath();
                }
            }
            else
            {
                config = new CONFIG(true);
            }
            
            if (config.SHCrafters.Count < 0)
            {
                config.GetSHCrafters();
            }

            update_recipe_crafters();
            update_cmb();

            folderBrowserDialog1.SelectedPath = config.SHsmodPath.Remove(config.SHsmodPath.Length - 16);

            rdo_armor_armor.Checked = true;

            //check if computer can run thumbnailer
            Console.WriteLine("Attempting to Initialize QbBreeze...");
            if (Breeze.Initialize())
            {
                canRenderQB = true;
            }
            Console.WriteLine("Breeze Initialized? " + canRenderQB.ToString());

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Breeze.Dispose();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            //used after the config window is closed
            //Console.WriteLine("Activated");
            if (configJustUpdated)
            {
                update_recipe_crafters();
                update_cmb();

                folderBrowserDialog1.SelectedPath = config.SHsmodPath.Remove(config.SHsmodPath.Length - 16);
                configJustUpdated = false;
            }
        }

        //updates most of the comboboxes in the form, It does not update the crafters.
        private void update_cmb()
        {
            update_recipe_igredients();
            update_recipe_products();
            update_constr_mat();
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

                if (lst_recipe.Items.Count > 0)
                {
                    lst_recipe.SelectedIndex = 0;
                }
                if (lst_armor.Items.Count > 0)
                {
                    lst_armor.SelectedIndex = 0;
                }
                if (lst_constr.Items.Count > 0)
                {
                    lst_constr.SelectedIndex = 0;
                }
                if (lst_flow.Items.Count > 0)
                {
                    lst_flow.SelectedIndex = 0;
                }
                if (lst_weap.Items.Count > 0)
                {
                    lst_weap.SelectedIndex = 0;
                }

                update_recipe_crafters();
                update_cmb();

                tabControl.SelectedIndex = 0;
            }
        }
        private void exportsmodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mod.name != "")
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    mod.SaveSMOD(folderBrowserDialog1.SelectedPath + "\\" + mod.name + ".smod");
                    lbl_status.Text = mod.name + " exported to: " + folderBrowserDialog1.SelectedPath + "\\" + mod.name + ".smod";
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
                    mod.BuildModFolder(folderBrowserDialog1.SelectedPath + "\\");
                    lbl_status.Text = mod.name + " exported to: " + folderBrowserDialog1.SelectedPath + "\\" + mod.name;
                }
            }
            else
            {
                lbl_status.Text = "CAN NOT EXPORT! MOD HAS NO NAME!!";
            }
        }


        //__________EDIT MENU ITEMS__________
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm con = new ConfigForm();
            con.ShowDialog();
        }
        private void advancedOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdvancedOptionsForm adv = new AdvancedOptionsForm();
            adv.Show();
        }

        //__________HELP MENU ITEMS__________
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm abt = new AboutForm();
            abt.Show();
        }
        private void howToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToForm how = new HowToForm();
            how.Show();
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

            //update lists
            lst_recipe.Items.Clear();
            foreach (Recipe recp in mod.recipes)
            {
                lst_recipe.Items.Add(recp.name);
            }

            lst_armor.Items.Clear();
            foreach (Armor armr in mod.armors)
            {
                lst_armor.Items.Add(armr.name);
            }
            lst_constr.Items.Clear();
            foreach (Construction constr in mod.constructions)
            {
                lst_constr.Items.Add(constr.name);
            }
            lst_flow.Items.Clear();
            foreach (Flower flow in mod.flowers)
            {
                lst_flow.Items.Add(flow.name);
            }
            lst_weap.Items.Clear();
            foreach (Weapon weap in mod.weapons)
            {
                lst_weap.Items.Add(weap.name);
            }
        }
        private void txt_mod_name_TextChanged(object sender, EventArgs e)
        {
            txt_mod_name.Text = utils.LowerNoSpaces(txt_mod_name.Text);
            txt_mod_name.Select(txt_mod_name.Text.Length, 0);
            mod.name = txt_mod_name.Text;
            mod.manifest.name = txt_mod_name.Text;
            
            update_recipe_crafters();
            update_cmb();
        }
        private void txt_api_version_TextChanged(object sender, EventArgs e)
        {
            mod.apiVersion = txt_api_version.Text;
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
                        pic_mod_recipe.Image = Image.FromStream(new MemoryStream(recp.png));
                        pic_mod_recipe.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_recipe.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
                pic_mod_recipe.Refresh();
            }
        }

        //lst_armor stuff
        private void lst_armor_MouseDown(object sender, MouseEventArgs e)
        {
            lst_armor.SelectedIndex = lst_armor.IndexFromPoint(e.X, e.Y);
            currentListBox = "armors";
        }
        private void lst_armor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_armor.SelectedItem != null)
            {
                foreach (Armor armr in mod.armors)
                {
                    if (armr.name == lst_armor.SelectedItem.ToString())
                    {
                        pic_mod_armor.Image = Image.FromStream(new MemoryStream(armr.png));
                        pic_mod_armor.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_armor.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
                pic_mod_armor.Refresh();
            }
        }

        //lst_constr stuff
        private void lst_constr_MouseDown(object sender, MouseEventArgs e)
        {
            lst_constr.SelectedIndex = lst_constr.IndexFromPoint(e.X, e.Y);
            currentListBox = "constructions";
        }
        private void lst_constr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_constr.SelectedItem != null)
            {
                foreach (Construction constr in mod.constructions)
                {
                    if (constr.name == lst_constr.SelectedItem.ToString())
                    {
                        pic_mod_constr.Image = Image.FromStream(new MemoryStream(constr.png));
                        pic_mod_constr.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_constr.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
                pic_mod_constr.Refresh();
            }
        }

        //lst_flower stuff
        private void lst_flower_MouseDown(object sender, MouseEventArgs e)
        {
            lst_flow.SelectedIndex = lst_flow.IndexFromPoint(e.X, e.Y);
            currentListBox = "flowers";
        }
        private void lst_flower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_flow.SelectedItem != null)
            {
                foreach (Flower flow in mod.flowers)
                {
                    if (flow.name == lst_flow.SelectedItem.ToString())
                    {
                        pic_mod_flow.Image = Image.FromStream(new MemoryStream(flow.png));
                        pic_mod_flow.Refresh();
                        break;
                    }
                }
            }
            else
            {
                pic_mod_flow.Image = Image.FromFile(localPath + "\\Configs\\blank.png");
                pic_mod_flow.Refresh();
            }
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
                pic_mod_weapon.Refresh();
            }
        }

        //context menu for listboxs
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentListBox == "recipes")
            {
                currentRecipe = new Recipe();
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_recipe");
            }
            if (currentListBox == "armors")
            {
                currentArmor = new Armor();
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_armor");
            }
            if (currentListBox == "constructions")
            {
                currentConsturction = new Construction();
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_constr");
            }
            if (currentListBox == "flowers")
            {
                currentFlower = new Flower();
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_flower");
            }
            if (currentListBox == "weapons")
            {
                currentWeapon = new Weapon();
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_weapon");
            }
        }
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
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
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_recipe");
                }
                if (currentListBox == "armors")
                {
                    foreach (Armor armr in mod.armors)
                    {
                        if (armr.name == lst_armor.SelectedItem.ToString())
                        {
                            currentArmor = armr;
                            break;
                        }
                    }
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_armor");
                }
                if (currentListBox == "constructions")
                {
                    foreach (Construction constr in mod.constructions)
                    {
                        if (constr.name == lst_constr.SelectedItem.ToString())
                        {
                            currentConsturction = constr;
                            break;
                        }
                    }
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_constr");
                }
                if (currentListBox == "flowers")
                {
                    foreach (Flower flow in mod.flowers)
                    {
                        if (flow.name == lst_flow.SelectedItem.ToString())
                        {
                            currentFlower = flow;
                            break;
                        }
                    }
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_flower");
                }
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
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey("tab_weapon");
                }
            }
            catch(NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            if (currentListBox == "armors")
            {
                foreach (Armor armr in mod.armors)
                {
                    if (armr.name == lst_armor.SelectedItem.ToString())
                    {
                        mod.armors.Remove(armr);
                        break;
                    }
                }
                updateMODtab();
            }
            if (currentListBox == "constructions")
            {
                foreach (Construction constr in mod.constructions)
                {
                    if (constr.name == lst_constr.SelectedItem.ToString())
                    {
                        mod.constructions.Remove(constr);
                        break;
                    }
                }
                updateMODtab();
            }
            if (currentListBox == "flowers")
            {
                foreach (Flower flow in mod.flowers)
                {
                    if (flow.name == lst_flow.SelectedItem.ToString())
                    {
                        mod.flowers.Remove(flow);
                        break;
                    }
                }
                updateMODtab();
            }
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
        }

        //__________Recipe Tab Stuff__________
        private void tab_recipe_Enter(object sender, EventArgs e)
        {
            TabLoading = true;
            txt_recp_name.Text = currentRecipe.name;
            txt_recp_desc.Text = currentRecipe.desc;
            txt_recp_flavor.Text = currentRecipe.flavor;
            nud_recipe_level.Value = currentRecipe.level;
            nud_recipe_work.Value = currentRecipe.workTime;

            cmb_recipe_Crafters.Text = currentRecipe.crafter;
            cmb_recp_cat.Text = currentRecipe.category;
            cmb_recipe_prod.Text = currentRecipe.product;
            pic_recipe.Image = Image.FromStream(new MemoryStream(currentRecipe.png));
            pic_recipe.Refresh();

            txt_recp_ingr.Clear();
            foreach (AmountOfItem item in currentRecipe.ingredients)
            {
                if (txt_recp_ingr.Text != "")
                {
                    txt_recp_ingr.AppendText("\n");
                }
                txt_recp_ingr.AppendText(item.amount.ToString() + "," + item.name);
            }


            TabLoading = false;
        }

        private void chk_recipe_lockimg_CheckedChanged(object sender, EventArgs e)
        {
            pic_recipe.Enabled = !chk_recipe_lockimg.Checked;
        }
        private void pic_recipe_Click(object sender, EventArgs e)
        {
            if (openFileDialogPNG.ShowDialog() == DialogResult.OK)
            {
                currentRecipe.png = File.ReadAllBytes(openFileDialogPNG.FileName);
                pic_recipe.Image = Image.FromStream(new MemoryStream(currentRecipe.png));
                pic_recipe.Refresh();
            }
        }
        private void txt_recp_TextChanged(object sender, EventArgs e)
        {
            if (!TabLoading)
            {
                currentRecipe.name = txt_recp_name.Text;
                currentRecipe.desc = txt_recp_desc.Text;
                currentRecipe.flavor = txt_recp_flavor.Text;
                currentRecipe.level = (int)nud_recipe_level.Value;
                currentRecipe.workTime = (int)nud_recipe_work.Value;
            }
        }

        private void btn_recipe_add_ingredient_Click(object sender, EventArgs e)
        {
            if (txt_recp_ingr.Text != "")
            {
                txt_recp_ingr.AppendText("\n");
            }
            txt_recp_ingr.AppendText(nud_recipe_ingredients.Value.ToString() + "," + cmb_recipe_ingredients.SelectedItem.ToString());
        }

        private void update_recipe_crafters()
        {
            //update crafterlist
            cmb_recipe_Crafters.Items.Clear();
            foreach (String str in utils.GetCrafters())
            {

                cmb_recipe_Crafters.Items.Add(str);
            }
        }
        private void update_recipe_igredients()
        {
            cmb_recipe_ingredients.Items.Clear();
            foreach (string str in utils.GetIngredients())
            {
                cmb_recipe_ingredients.Items.Add(str);
            }
        }
        private void update_recipe_products()
        {
            cmb_recipe_prod.Items.Clear();
            foreach (string str in mod.GetItemAliasesForRecipe())
            {
                cmb_recipe_prod.Items.Add(str);
            }
        }

        private void cmb_recipe_prod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if the cmb is not empty
            if (cmb_recipe_prod.SelectedItem.ToString() != "")
            {
                currentRecipe.product = cmb_recipe_prod.SelectedItem.ToString();
                Console.WriteLine(currentRecipe.product.ToString());
                //if pic checkbox is not checked, change pic
                if (!chk_recipe_lockimg.Checked)
                {
                    //if it has ':weapon:' in the text then it must be a weapon
                    if (cmb_recipe_prod.SelectedItem.ToString().Contains(":weapon:"))
                    {
                        foreach (Weapon weap in mod.weapons)
                        {
                            //Console.WriteLine(weap.iname + " " + cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":weapons:".Length-1));
                            if (weap.iname == cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":weapon:".Length))
                            {
                                currentRecipe.png = weap.png;
                                pic_recipe.Image = Image.FromStream(new MemoryStream(currentRecipe.png));
                                pic_recipe.Refresh();
                                break;
                            }
                        }
                    }

                    //if it has ':armor:' in the text then it must be an armor
                    if (cmb_recipe_prod.SelectedItem.ToString().Contains(":armor:"))
                    {
                        foreach (Armor armr in mod.armors)
                        {
                            //Console.WriteLine("armor: " + armr.iname);
                            //MessageBox.Show(armr.iname + " - " + cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":armor:".Length));
                            if (armr.iname == cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":armor:".Length))
                            {
                                currentRecipe.png = armr.png;
                                pic_recipe.Image = Image.FromStream(new MemoryStream(currentRecipe.png));
                                pic_recipe.Refresh();
                                break;
                            }
                        }
                    }
                    //if it has ':flower:' in the text then it must be an armor
                    if (cmb_recipe_prod.SelectedItem.ToString().Contains(":flower:"))
                    {
                        foreach (Flower flow in mod.flowers)
                        {
                            //Console.WriteLine("flower: " + armr.iname);
                            //MessageBox.Show(armr.iname + " - " + cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":armor:".Length));
                            //Why is this code +0 while the other one was -1????
                            if (flow.iname == cmb_recipe_prod.SelectedItem.ToString().Remove(0, mod.name.Length + ":flower:".Length))
                            {
                                currentRecipe.png = flow.png;
                                pic_recipe.Image = Image.FromStream(new MemoryStream(currentRecipe.png));
                                pic_recipe.Refresh();
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void cmb_recipe_Crafters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TabLoading)
            {
                //change categories based on crafter
                cmb_recp_cat.Items.Clear();
                if (cmb_recipe_Crafters.Text != "")
                {
                    //if the text contains 'stonehearth' then it is a stonehearth crafter, and NOT a crafter from this mod
                    if (cmb_recipe_Crafters.Text.Contains("stonehearth"))
                    {
                        foreach (Crafter crft in config.SHCrafters)
                        {
                            //for each crafter in sh crafters, check to see if it matches
                            if (crft.IsCrafter(cmb_recipe_Crafters.Text))
                            {
                                //if it does, then add all the categories from that crafter to cmb_recp_cat
                                foreach (String cat in crft.categories)
                                {
                                    cmb_recp_cat.Items.Add(cat);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_recipe_Click(object sender, EventArgs e)
        {
            //Add recipe to MOD
            if (currentRecipe.name == "")
            {
                lbl_status.Text = "Recipe has no name! Could not add/update weapon.";
            }
            else if (txt_recp_ingr.Text == "")
            {
                lbl_status.Text = "Recipe has no ingredients! Could not add/update weapon.";
            }
            else if (currentRecipe.product == "")
            {
                lbl_status.Text = "Recipe has no product! Could not add/update weapon.";
            }
            else if (cmb_recipe_Crafters.Text == "")
            {
                lbl_status.Text = "Recipe has no crafter! Could not add/update weapon.";
            }
            else if (cmb_recp_cat.Text == "")
            {
                lbl_status.Text = "Recipe has no category! Could not add/update weapon.";
            }
            else
            {
                currentRecipe.ingredients.Clear();
                foreach (String str in txt_recp_ingr.Lines)
                {
                    if (str.Contains(","))
                    {
                        AmountOfItem temp = new AmountOfItem();
                        currentRecipe.ingredients.Add(temp.FromString(str));
                    }
                }
                currentRecipe.crafter = cmb_recipe_Crafters.Text;
                currentRecipe.category = cmb_recp_cat.Text;
                mod.AddRecipe(currentRecipe);
                tabControl.SelectedIndex = 0;
                lbl_status.Text = "Recipe " + currentRecipe.name + " has been added/updated.";
            }
        }

        //__________Armor Tab Stuff__________
        private void tab_armor_Enter(object sender, EventArgs e)
        {
            TabLoading = true;
            txt_armor_name.Text = currentArmor.name;
            txt_armor_desc.Text = currentArmor.desc;
            txt_armor_tags.Text = currentArmor.tags;
            nud_armor_ilevel.Value = currentArmor.ilevel;
            nud_armor_dmgRed.Value = currentArmor.damageReduc;

            pic_armor_qb.Image = Image.FromStream(new MemoryStream(currentArmor.qbICON));
            pic_armor_qb.Refresh();

            pic_armor_qbf.Image = Image.FromStream(new MemoryStream(currentArmor.qbfICON));
            pic_armor_qbf.Refresh();

            pic_armor_qbi.Image = Image.FromStream(new MemoryStream(currentArmor.qbiICON));
            pic_armor_qbi.Refresh();

            pic_armor_png.Image = Image.FromStream(new MemoryStream(currentArmor.png));
            pic_armor_png.Refresh();

            if (currentArmor.type == "Armor") { rdo_armor_armor.Checked = true; }
            if (currentArmor.type == "Shield") { rdo_armor_shield.Checked = true; }

            TabLoading = false;
        }

        private void pic_armor_qb_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentArmor.qb = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentArmor.qb))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentArmor.qbICON = stream.ToArray();
                        }
                    }
                }
                pic_armor_qb.Image = Image.FromStream(new MemoryStream(currentArmor.qbICON));
                pic_armor_qb.Refresh();
            }
        }
        private void pic_armor_qbf_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentArmor.qbf = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentArmor.qbf))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentArmor.qbfICON = stream.ToArray();
                        }
                    }
                }
                pic_armor_qbf.Image = Image.FromStream(new MemoryStream(currentArmor.qbfICON));
                pic_armor_qbf.Refresh();
            }
        }
        private void pic_armor_qbi_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentArmor.qbi = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentArmor.qbi))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentArmor.qbiICON = stream.ToArray();
                        }
                    }
                }
                pic_armor_qbi.Image = Image.FromStream(new MemoryStream(currentArmor.qbiICON));
                pic_armor_qbi.Refresh();
            }
        }
        private void pic_armor_png_Click(object sender, EventArgs e)
        {
            if (openFileDialogPNG.ShowDialog() == DialogResult.OK)
            {
                currentArmor.png = File.ReadAllBytes(openFileDialogPNG.FileName);
                pic_armor_png.Image = Image.FromStream(new MemoryStream(currentArmor.png));
                pic_armor_png.Refresh();
            }
        }
        private void txt_armor_changed(object sender, EventArgs e)
        {
            if (!TabLoading)
            {
                currentArmor.name = txt_armor_name.Text;
                currentArmor.iname = utils.LowerNoSpaces(currentArmor.name);
                currentArmor.desc = txt_armor_desc.Text;
                currentArmor.tags = txt_armor_tags.Text;
                currentArmor.ilevel = (int)nud_armor_ilevel.Value;
                currentArmor.damageReduc = (int)nud_armor_dmgRed.Value;

                //change the text of the button
                //bool button_update = false;
                //foreach (Armor item in mod.armors)
                //{
                //    if (currentArmor.name == item.name) { button_update = true;}
                //}
                //if (button_update)
                //{
                //    btn_armor.Text = "Update Armor";
                //}
                //else
                //{
                //    btn_armor.Text = "Add Armor";
                //}
            }
        }

        private void rdo_armor_armor_CheckedChanged(object sender, EventArgs e)
        {
            //if armor is checked then enable the female qb pic.. otherwise disable it.
            if (rdo_armor_armor.Checked)
            {
                pic_armor_qbf.BackgroundImage = SHModMaker.Properties.Resources.QB_female;
                pic_armor_qbf.Enabled = true;
                currentArmor.type = "Armor";
            }
            else
            {
                pic_armor_qbf.BackgroundImage = SHModMaker.Properties.Resources.QB_female_bw;
                pic_armor_qbf.Enabled = false;
                currentArmor.qbf = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb");
                currentArmor.qbfICON = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
                pic_armor_qbf.Image = Image.FromStream(new MemoryStream(currentArmor.qbfICON));
                pic_armor_qbf.Refresh();

            }
        }
        private void rdo_armor_shield_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_armor_shield.Checked)
            {
                currentArmor.type = "Shield";
            }
        }

        private void btn_armor_Click(object sender, EventArgs e)
        {
            //Add weapon to MOD
            if (currentArmor.name != "")
            {
                mod.AddArmor(currentArmor);
                tabControl.SelectedIndex = 0;
                lbl_status.Text = "Armor " + currentArmor.name + " has been added/updated.";
            }
            else
            {
                lbl_status.Text = "Armor has no name! Could not add/update weapon.";
            }
            update_cmb();

        }

        //__________Construction Tab Stuff__________
        private void tab_constr_Enter(object sender, EventArgs e)
        {
            TabLoading = true;

            txt_constr_name.Text = currentConsturction.name;
            txt_constr_desc.Text = currentConsturction.desc;

            pic_constr_qb.Image = Image.FromStream(new MemoryStream(currentConsturction.qbICON));
            pic_constr_qb.Refresh();

            pic_constr_png.Image = Image.FromStream(new MemoryStream(currentConsturction.png));
            pic_constr_png.Refresh();

            cmb_constr_cat.Text = currentConsturction.category;
            cmb_constr_mat.Text = currentConsturction.material;
            if (currentConsturction.type == "Curb" || currentConsturction.type == "Road")
            {
                nud_constr_speed.Enabled = true;
            }
            else
            {
                nud_constr_speed.Enabled = false;
            }
            nud_constr_speed.Value = (decimal)currentConsturction.road_speed;

            if (currentConsturction.type == "") { currentConsturction.type = "Curb"; }
            if (currentConsturction.type == "Curb") { rdo_constr_curb.Checked = true; }
            if (currentConsturction.type == "Road") { rdo_constr_road.Checked = true; }
            if (currentConsturction.type == "Column") { rdo_constr_column.Checked = true; }
            if (currentConsturction.type == "Wall") { rdo_constr_wall.Checked = true; }
            if (currentConsturction.type == "Floor") { rdo_constr_floor.Checked = true; }
            if (currentConsturction.type == "Slab") { rdo_constr_slab.Checked = true; }
            if (currentConsturction.type == "Roof") { rdo_constr_roof.Checked = true; }

            if (rdo_constr_curb.Checked == true || rdo_constr_road.Checked == true)
            {
                nud_constr_speed.Enabled = true;
            }
            else
            {
                nud_constr_speed.Enabled = false;
            }

            TabLoading = false;
        }

        private void update_constr_mat()
        {
            cmb_constr_mat.Items.Clear();
            foreach (string str in utils.GetCommonMaterials())
            {
                cmb_constr_mat.Items.Add(str);
            }
        }

        private void pic_constr_qb_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentConsturction.qb = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentConsturction.qb))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentConsturction.qbICON = stream.ToArray();
                        }
                    }
                }
                pic_constr_qb.Image = Image.FromStream(new MemoryStream(currentConsturction.qbICON));
                pic_constr_qb.Refresh();
            }
        }
        private void pic_constr_png_Click(object sender, EventArgs e)
        {
            if (openFileDialogPNG.ShowDialog() == DialogResult.OK)
            {
                currentConsturction.png = File.ReadAllBytes(openFileDialogPNG.FileName);
                pic_constr_png.Image = Image.FromStream(new MemoryStream(currentConsturction.png));
                pic_constr_png.Refresh();
            }
        }
        private void txt_constr_changed(object sender, EventArgs e)
        {
            if (!TabLoading)
            {
                currentConsturction.name = txt_constr_name.Text;
                currentConsturction.iname = utils.LowerNoSpaces(currentConsturction.name);
                currentConsturction.desc = txt_constr_desc.Text;

                currentConsturction.category = cmb_constr_cat.Text;
                currentConsturction.material = cmb_constr_mat.Text;
                if (rdo_constr_curb.Checked == true || rdo_constr_road.Checked == true)
                {
                    nud_constr_speed.Enabled = true;
                }
                else
                {
                    nud_constr_speed.Enabled = false;
                }
                currentConsturction.road_speed = (float)nud_constr_speed.Value;

                foreach (RadioButton rdo in grp_constr_type.Controls.OfType<RadioButton>())
                {
                    if (rdo.Checked == true)
                    {
                        currentConsturction.type = rdo.Text;
                        break;
                    }
                }
            }
        }
        private void btn_constr_Click(object sender, EventArgs e)
        {
            //Add constr to MOD

            if (currentConsturction.name == "")
            {
                lbl_status.Text = "Construction has no Name! Could not add/update weapon.";
            }
            else if (currentConsturction.material == "")
            {
                lbl_status.Text = "Construction has no Category! Could not add/update weapon.";
            }
            else if (currentConsturction.material == "")
            {
                lbl_status.Text = "Construction has no Material! Could not add/update weapon.";
            }
            else
            {
                mod.AddConstruction(currentConsturction);
                tabControl.SelectedIndex = 0;
                lbl_status.Text = currentConsturction.type + " " + currentConsturction.name + " has been added/updated.";
            }
        }

        //__________Flower Tab Stuff__________
        private void tab_flower_Enter(object sender, EventArgs e)
        {
            TabLoading = true;
            txt_flower_name.Text = currentFlower.name;
            txt_flower_desc.Text = currentFlower.desc;
            txt_flower_tag.Text = currentFlower.tags;
            //build habitat string
            txt_flower_habit.Clear();
            foreach (String hab in currentFlower.habitats)
            {
                txt_flower_habit.AppendText(hab + " ");
            }

            pic_flower_qb.Image = Image.FromStream(new MemoryStream(currentFlower.qbICON));
            pic_flower_qb.Refresh();

            pic_flower_qbi.Image = Image.FromStream(new MemoryStream(currentFlower.qbiICON));
            pic_flower_qbi.Refresh();

            pic_flower_png.Image = Image.FromStream(new MemoryStream(currentFlower.png));
            pic_flower_png.Refresh();

            nud_flower_weight.Value = currentFlower.weight;
            nud_flower_width.Value = currentFlower.width;
            nud_flower_length.Value = currentFlower.length;
            nud_flower_min.Value = currentFlower.min;
            nud_flower_max.Value = currentFlower.max;

            TabLoading = false;
        }

        private void pic_flower_qb_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentFlower.qb = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentFlower.qb))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentFlower.qbICON = stream.ToArray();
                        }
                    }
                }
                pic_flower_qb.Image = Image.FromStream(new MemoryStream(currentFlower.qbICON));
                pic_flower_qb.Refresh();
            }
        }
        private void pic_flower_qbi_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentFlower.qbi = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentFlower.qbi))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentFlower.qbiICON = stream.ToArray();
                        }
                    }
                }
                pic_flower_qbi.Image = Image.FromStream(new MemoryStream(currentFlower.qbiICON));
                pic_flower_qbi.Refresh();
            }
        }
        private void pic_flower_png_Click(object sender, EventArgs e)
        {
            if (openFileDialogPNG.ShowDialog() == DialogResult.OK)
            {
                currentFlower.png = File.ReadAllBytes(openFileDialogPNG.FileName);
                pic_flower_png.Image = Image.FromStream(new MemoryStream(currentFlower.png));
                pic_flower_png.Refresh();
            }
        }
        private void txt_flower_changed(object sender, EventArgs e)
        {
            if (!TabLoading)
            {
                currentFlower.name = txt_flower_name.Text;
                currentFlower.iname = utils.LowerNoSpaces(currentFlower.name);
                currentFlower.desc = txt_flower_desc.Text;
                currentFlower.tags = txt_flower_tag.Text;
                currentFlower.habitats.Clear();
                if (txt_flower_habit.Text.Contains(' '))
                {
                    foreach (String str in txt_flower_habit.Text.Split(' '))
                    {
                        currentFlower.habitats.Add(str);
                    }
                } else if (txt_flower_habit.Text != "")
                {
                    currentFlower.habitats.Add(txt_flower_habit.Text);
                }
                currentFlower.weight = (int)nud_flower_weight.Value;
                currentFlower.width = (int)nud_flower_width.Value;
                currentFlower.length = (int)nud_flower_length.Value;
                currentFlower.min = (int)nud_flower_min.Value;
                currentFlower.max = (int)nud_flower_max.Value;
            }
        }
        private void btn_flower_Click(object sender, EventArgs e)
        {
            //Add weapon to MOD
            if (currentFlower.name != "")
            {
                mod.AddFlower(currentFlower);
                tabControl.SelectedIndex = 0;
                lbl_status.Text = "Flower " + currentFlower.name + " has been added/updated.";
            }
            else
            {
                lbl_status.Text = "Flower has no name! Could not add/update.";
            }
            update_cmb();


        }

        //__________Weapon Tab Stuff__________
        private void tab_weapon_Enter(object sender, EventArgs e)
        {
            TabLoading = true;
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
            TabLoading = false;
        }

        private void pic_weap_qb_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentWeapon.qb = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentWeapon.qb))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentWeapon.qbICON = stream.ToArray();
                        }
                    }
                }
                pic_weap_qb.Image = Image.FromStream(new MemoryStream(currentWeapon.qbICON));
                pic_weap_qb.Refresh();
            }
        }
        private void pic_weap_qbi_Click(object sender, EventArgs e)
        {
            if (openFileDialogQB.ShowDialog() == DialogResult.OK)
            {
                currentWeapon.qbi = File.ReadAllBytes(openFileDialogQB.FileName);
                if (canRenderQB)
                {
                    using (Bitmap thumbnail = Breeze.GetThumbnail(currentWeapon.qbi))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            thumbnail.Save(stream, ImageFormat.Png);
                            currentWeapon.qbiICON = stream.ToArray();
                        }
                    }
                }
                pic_weap_qbi.Image = Image.FromStream(new MemoryStream(currentWeapon.qbiICON));
                pic_weap_qbi.Refresh();
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
            if (!TabLoading)
            {
                currentWeapon.name = txt_weap_name.Text;
                currentWeapon.iname = utils.LowerNoSpaces(currentWeapon.name);
                currentWeapon.desc = txt_weap_desc.Text;
                currentWeapon.ilevel = (int)nud_weap_ilevel.Value;
                currentWeapon.damage = (int)nud_weap_damage.Value;
                currentWeapon.reach = (float)nud_weap_reach.Value;
            }
        }
        private void btn_weapon_Click(object sender, EventArgs e)
        {
            //Add weapon to MOD
            if (currentWeapon.name != "")
            {
                mod.AddWeapon(currentWeapon);
                tabControl.SelectedIndex = 0;
                lbl_status.Text = "Weapon " + currentWeapon.name + " has been added/updated.";
            }
            else
            {
                lbl_status.Text = "Weapon has no name! Could not add/update weapon.";
            }
            update_cmb();

        }

        
    }

    // __________Other Classes__________
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
        public static List<String> GetCrafters()
        {
            List<String> crafters = new List<String>();

            //Get crafters from stonehearth
            foreach (Crafter crft in MainForm.config.SHCrafters)
            {
                crafters.Add(crft.GetString());
            }

            //Get crafters from current mod
            foreach (Crafter crft in MainForm.mod.crafters)
            {
                crafters.Add(MainForm.mod.name + ":" + crft.name);
            }

            ////Get crafters from other mods?

            return crafters;
        }
        public static List<String> GetIngredients()
        {
            List<String> ingredients = new List<string>();

            //add common resources
            foreach (String str in MainForm.config.CommonMaterialTags)
            {
                ingredients.Add(str);
            }

            //Read stonehearth.smod and get ingredients from the manifest return alias as string
            using (ZipFile zip = ZipFile.Read(MainForm.config.SHsmodPath))
            {
                foreach (ZipEntry e in zip)
                {
                    if (e.FileName.Contains("stonehearth/manifest.json"))
                    {
                        //read manifest until the "Aliases" section
                        //then grab each alias, line by line.. getting rid of the parts which are unneeded
                        //when the next secion show up.. stop

                        Console.WriteLine("Found Manifest: " + e.FileName);
                        String[] strArray;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            e.Extract(stream);
                            stream.Position = 0;
                            Console.WriteLine("Extracted to stream:" + stream.ToString());
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                //Console.WriteLine("The stream:");
                                //Console.WriteLine(sr.ReadToEnd());
                                strArray = sr.ReadToEnd().Split('\n');
                            }
                        }
                        bool inAliasSection = false;
                        foreach (String str in strArray)
                        {
                            //Console.WriteLine("CURRENT STR :" + str);
                            //check for the end of alias section
                            if (inAliasSection && str.Contains("},")) { inAliasSection = false; break; }

                            // process aliases
                            if (inAliasSection)
                            {
                                bool containsSkipWord = false;
                                //if string contains a " then it is 1. not empty and 2/ contains an alias.
                                if (str.Contains('\"'))
                                {
                                    //Console.WriteLine(str + " contains a \"");
                                    foreach (String word in MainForm.config.AliasSkipWords)
                                    {
                                        if (str.Contains(word))
                                        {
                                            containsSkipWord = true;
                                            break;
                                        }
                                    }

                                    if (!containsSkipWord)
                                    {
                                        String alias = str.Remove(str.LastIndexOf(':'));
                                        alias = alias.Remove(alias.LastIndexOf('"'));
                                        alias = alias.Remove(0, alias.IndexOf('"') + 1);
                                        ingredients.Add("stonehearth:" + alias);
                                        //Console.WriteLine("stonehearth:" + alias);
                                    }
                                }
                            }

                            //check for beginning of aliases... if string contains 'aliases' then the next string will be an alias
                            if (str.Contains("aliases")) { inAliasSection = true; Console.WriteLine("Entering Aliases section"); }
                        }
                        //Console.WriteLine(e.FileName);
                        //String str = e.FileName.Remove((e.FileName.Length - 21));
                        //str = str.Remove(0, 17);
                        //Console.WriteLine(str);
                        //ingredients.Add("stonehearth:" + str);
                    }
                }
            }

            //Get items from this mod
            foreach (String str in MainForm.mod.GetItemAliasesForRecipe())
            {
                ingredients.Add(str);
            }


            ////Get items from other mods??

            return ingredients;
        }

        public static List<String> GetCommonMaterials()
        {
            List<String> ingredients = new List<string>();

            //add common resources
            foreach (String str in MainForm.config.CommonMaterialTags)
            {
                ingredients.Add(str);
            }
            return ingredients;
        }
    }

    public class ManifestJSON
    {
        public String name;
        public String apiVersion;
        public List<String[]> aliases = new List<string[]>();
        public List<String[]> mixintos = new List<string[]>();
        public List<String[]> overrides = new List<string[]>();

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

            //Add mixintos
            if (mixintos.Count != 0)
            {
                maniFile = maniFile + ",\n\n\t\"mixintos\" : {";
                for (int i = 0; i < mixintos.Count; i++)
                {
                    maniFile = maniFile + "\n\t\t\"" + mixintos[i][0] + "\" : \"" + mixintos[i][1] + "\"";
                    if (i != mixintos.Count - 1)
                    {
                        maniFile = maniFile + ",";
                    }
                }
                maniFile = maniFile + "\n\t}";
            }

            //Add overrides
            if (overrides.Count != 0)
            {
                maniFile = maniFile + ",\n\n\t\"overrides\" : {";
                for (int i = 0; i < overrides.Count; i++)
                {
                    maniFile = maniFile + "\n\t\t\"" + overrides[i][0] + "\" : \"" + overrides[i][1] + "\"";
                    if (i != overrides.Count - 1)
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

        public void AddMixinto(String mixinto, string data)
        //Adds Mixin to mixins List. 
        {
            mixintos.Add(new String[] { mixinto, data });
        }

        public void AddOverride(String ovrRid, string data)
        //Adds Mixin to mixins List. 
        {
            overrides.Add(new String[] { ovrRid, data });
        }
    }

    public class Recipe
    {
        public String name;
        public int workTime;
        public int level;
        public String crafter;
        public String desc;
        public String flavor;
        public String category;
        public byte[] png;
        public List<AmountOfItem> ingredients;
        public String product;

        public Recipe()
        {
            name = "";
            workTime = 2;
            level = 0;
            crafter = "";
            desc = "";
            flavor = "";
            category = "";
            png = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
            ingredients = new List<AmountOfItem>();
            product = "";
        }

        public void WriteRecipeFile(String modPath)
        {
            String iname = utils.LowerNoSpaces(name);
            string localPath = System.IO.Directory.GetCurrentDirectory();
            String[] filesAll;
            List<String[]> filesJsonLua = new List<string[]>();
            String craftr = crafter.Remove(0, crafter.IndexOf(":") + 1);

            //Build Parse List
            List<String[]> refList = new List<string[]>();
            refList.Add(new String[] { "name", name });
            refList.Add(new String[] { "iname", iname });
            refList.Add(new String[] { "desc", desc });
            refList.Add(new String[] { "cat", category });
            refList.Add(new String[] { "wrk", workTime.ToString() });
            refList.Add(new String[] { "flav", flavor });
            refList.Add(new String[] { "lvl", level.ToString() });
            refList.Add(new String[] { "prod", product });
            String ingr = "";
            foreach (AmountOfItem item in ingredients)
            {
                String MorU = "material";
                if (item.name.Contains(':')) { MorU = "uri"; }
                if (ingr != "")
                {
                    ingr = ingr + ",\n";
                }
                ingr = ingr + "\t\t{\n" +
                               "\t\t\t\"" + MorU + "\" : \"" + item.name + "\",\n" +
                               "\t\t\t\"count\" : " + item.amount.ToString() + "\n" +
                               "\t\t}";
            }
            refList.Add(new String[] { "ingr", ingr });

            //Get JSONs and LUA files
            //FIX can probably make a utils for this utils.GetJsonLua(String path)??
            filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\Recipe\\");
            foreach (String str in filesAll)
            {
                if (str.EndsWith(".json") || str.EndsWith(".lua") || str.EndsWith(".luac"))
                {
                    System.IO.StreamReader filepath = new System.IO.StreamReader(str);
                    filesJsonLua.Add(new String[] { System.IO.Path.GetFileName(str), utils.Parse(filepath.ReadToEnd(), refList) });
                    filepath.Close();
                }
            }

            //make folder if needed
            if (!System.IO.Directory.Exists(modPath + "\\recipes\\" + craftr))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\recipes\\" + craftr);
                Console.WriteLine(modPath + "\\recipes\\" + craftr);
            }

            //write them to new path
            foreach (String[] str in filesJsonLua)
            {
                String fileName = str[0];
                if (fileName.ToLower().Contains("item"))
                {
                    fileName = fileName.ToLower().Replace("item", iname);
                }
                System.IO.File.WriteAllText(modPath + "\\recipes\\" + craftr + "\\" + fileName, str[1], Encoding.ASCII);
            }

            //write qbs and png to new path
            System.IO.File.WriteAllBytes(modPath + "\\recipes\\" + craftr + "\\" + iname + ".png", png);
        }
    }

    public class Crafter
    {
        public String mod;
        public String name;
        public List<String> categories;

        public Crafter()
        {
            mod = "";
            name = "";
            categories = new List<String>();
        }
        public Crafter(String m, String n, List<String> c)
        {
            mod = m;
            name = n;
            categories = c;
        }
        public String GetString()
        {
            return mod + ":" + name;
        }
        public bool IsCrafter(String crafter)
        {
            if (crafter == mod + ":" + name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AmountOfItem
    {
        public String name;
        public int amount;

        public AmountOfItem()
        {
            name = "";
            amount = 1;
        }
        public AmountOfItem(String nam, int amt)
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
            return new AmountOfItem(str.Split(',')[1], int.Parse(str.Split(',')[0]));
        }
    }

    public class Armor
    {
        public String name;
        public String iname;
        public String desc;
        public String tags;
        public int ilevel;
        public int damageReduc;
        public String type;

        public byte[] qb;
        public byte[] qbf;
        public byte[] qbi;
        public byte[] png;
        public byte[] qbICON;
        public byte[] qbfICON;
        public byte[] qbiICON;

        public Armor()
            : this("", "", 0, 0, "", File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"))
        { }

        public Armor(String nam, String des, int ilvl, int dam, String typ)
            : this(nam, des, ilvl, dam, typ, File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"),
                                        File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"))
        { }

        public Armor(String nam, String des, int ilvl, int dam, String t, Byte[] q, Byte[] qf, Byte[] qi, Byte[] p, Byte[] qbIC, Byte[] qbfIC, Byte[] qbiIC)
        {
            name = nam;
            iname = utils.LowerNoSpaces(nam);
            desc = des;
            ilevel = ilvl;
            damageReduc = dam;
            type = t;
            qb = q;
            qbf = qf;
            qbi = qi;
            png = p;
            qbICON = qbIC;
            qbfICON = qbfIC;
            qbiICON = qbiIC;
        }

        public void WriteArmorFile(String modPath)
        {
            string localPath = System.IO.Directory.GetCurrentDirectory();
            String[] filesAll = new String[] { "" };
            List<String[]> filesJsonLua = new List<string[]>();
            //Build Parse List
            List<String[]> refList = new List<string[]>();
            refList.Add(new String[] { "name", name });
            refList.Add(new String[] { "iname", iname });
            refList.Add(new String[] { "desc", desc });
            refList.Add(new String[] { "tag", tags });
            refList.Add(new String[] { "ilevel", ilevel.ToString() });
            refList.Add(new String[] { "damage", damageReduc.ToString() });

            //Get JSONs and LUA files
            if (type == "Shield")
            {
                filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\Shield\\");
            }
            else //if it's not a shield, it must be armor.
            {
                filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\Armor\\");
            }

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
            if (!System.IO.Directory.Exists(modPath + "\\entities\\armor\\" + iname))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\entities\\armor\\" + iname);
                Console.WriteLine(modPath + "\\entities\\armor\\" + iname);
            }

            //write them to new path
            foreach (String[] str in filesJsonLua)
            {
                String fileName = str[0];
                if (fileName.ToLower().Contains("armor") || fileName.ToLower().Contains("shield"))
                {
                    fileName = fileName.ToLower().Replace("armor", iname);
                    fileName = fileName.ToLower().Replace("shield", iname);
                }
                System.IO.File.WriteAllText(modPath + "\\entities\\armor\\" + iname + "\\" + fileName, str[1], Encoding.ASCII);
            }

            //write qbs and png to new path
            System.IO.File.WriteAllBytes(modPath + "\\entities\\armor\\" + iname + "\\" + iname + ".qb", qb);
            if (type == "Armor") { System.IO.File.WriteAllBytes(modPath + "\\entities\\armor\\" + iname + "\\" + iname + "_female.qb", qbf); }
            System.IO.File.WriteAllBytes(modPath + "\\entities\\armor\\" + iname + "\\" + iname + "_iconic.qb", qbi);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\armor\\" + iname + "\\" + iname + ".png", png);
        }
    }

    public class Construction
    {
        public String name;
        public String iname;
        public String desc;

        public String material;
        public String category;
        public String type;
        public float road_speed;

        public byte[] qb;
        public byte[] png;
        public byte[] qbICON;

        public Construction()
        {
            name = "";
            iname = utils.LowerNoSpaces(name);
            desc = "";

            material = "";
            category = "";
            type = "";
            road_speed = 0.25f;

            qb = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb");
            png = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
            qbICON = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
        }

        public void WriteConstructionFile(String modPath)
        {
            string localPath = System.IO.Directory.GetCurrentDirectory();
            String[] filesAll;
            List<String[]> filesJsonLua = new List<string[]>();
            //Build Parse List
            List<String[]> refList = new List<string[]>();
            refList.Add(new String[] { "name", name });
            refList.Add(new String[] { "iname", iname });
            refList.Add(new String[] { "desc", desc });
            refList.Add(new String[] { "mat", material });
            refList.Add(new String[] { "cat", category });
            refList.Add(new String[] { "type", type.ToLower() });
            refList.Add(new String[] { "speed", road_speed.ToString() });
            refList.Add(new String[] { "mod", utils.LowerNoSpaces(MainForm.mod.name) });

            //Get JSONs and LUA files
            //FIX can probably make a utils for this utils.GetJsonLua(String path)??
            filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\" + type + "\\");
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
            if (!System.IO.Directory.Exists(modPath + "\\entities\\build\\" + type.ToLower() + "\\" + iname))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\entities\\build\\" + type.ToLower() + "\\" + iname);
                //Console.WriteLine(modPath + "\\entities\\build\\" + iname);
            }

            //write them to new path
            foreach (String[] str in filesJsonLua)
            {
                String fileName = str[0];
                if (fileName.ToLower().Contains(type.ToLower()))
                {
                    fileName = fileName.ToLower().Replace(type.ToLower(), iname);
                }
                System.IO.File.WriteAllText(modPath + "\\entities\\build\\" + type.ToLower() + "\\" + iname + "\\" + fileName, str[1], Encoding.ASCII);
            }

            //write qbs and png to new path
            System.IO.File.WriteAllBytes(modPath + "\\entities\\build\\" + type.ToLower() + "\\" + iname + "\\" + iname + ".qb", qb);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\build\\" + type.ToLower() + "\\" + iname + "\\" + iname + ".png", png);
        }
    }

    public class Flower
    {
        public String name;
        public String iname;
        public String desc;
        public String tags;
        public List<String> habitats;

        public byte[] qb;
        public byte[] qbi;
        public byte[] png;
        public byte[] qbICON;
        public byte[] qbiICON;

        public int weight;
        public int width;
        public int length;
        public int min;
        public int max;

        public Flower()
        {
            name = "";
            iname = utils.LowerNoSpaces(name);
            desc = "";
            tags = "";
            habitats = new List<string>();

            qb = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb");
            qbi = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb");
            png = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
            qbICON = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");
            qbiICON = File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png");

            weight = 10;
            width = 4;
            length = 4;
            min = 2;
            max = 5;
        }

        public void AddHabitat(String str)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for (int i = 0; i < habitats.Count; i++)
            {
                if (habitats[i] == str)
                {
                    changed = true;
                    habitats[i] = str;
                    break;
                }
            }
            if (changed == false)
            {
                habitats.Add(str);
            }
        }
        public void RemoveHabitat(String str)
        {
            habitats.Remove(str);
        }

        public void WriteFlowerFile(String modPath)
        {
            string localPath = System.IO.Directory.GetCurrentDirectory();
            String[] filesAll;
            List<String[]> filesJsonLua = new List<string[]>();

            String habitatSTR = "[";

            //Build Parse List
            List <String[]> refList = new List<string[]>();
            refList.Add(new String[] { "mname", MainForm.mod.name });
            refList.Add(new String[] { "name", name });
            refList.Add(new String[] { "iname", iname });
            refList.Add(new String[] { "desc", desc });
            refList.Add(new String[] { "tag", tags });
            //make habitat str
            for(int i = 0; i < habitats.Count; i++)
            {
                habitatSTR = habitatSTR + "\"" + habitats[i] + "\"";
                if (i != habitats.Count-1) { habitatSTR = habitatSTR + ","; } else { habitatSTR = habitatSTR + "]"; }
            }
            refList.Add(new String[] { "habitat", habitatSTR });
            refList.Add(new String[] { "weight", weight.ToString() });
            refList.Add(new String[] { "width", width.ToString() });
            refList.Add(new String[] { "length", length.ToString() });
            refList.Add(new String[] { "min", min.ToString() });
            refList.Add(new String[] { "max", max.ToString() });

            //Get JSONs and LUA files
            //FIX can probably make a utils for this utils.GetJsonLua(String path)??
            filesAll = System.IO.Directory.GetFiles(localPath + "\\JSONs\\WildFlower\\");
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
            if (!System.IO.Directory.Exists(modPath + "\\entities\\flowers\\" + iname))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\entities\\flowers\\" + iname);
                Console.WriteLine(modPath + "\\entities\\flowers\\" + iname);
            }

            //write them to new path
            foreach (String[] str in filesJsonLua)
            {
                String fileName = str[0];
                if (fileName.ToLower().Contains("flower"))
                {
                    fileName = fileName.ToLower().Replace("flower", iname);
                }
                System.IO.File.WriteAllText(modPath + "\\entities\\flowers\\" + iname + "\\" + fileName, str[1], Encoding.ASCII);
            }

            //write qbs and png to new path
            System.IO.File.WriteAllBytes(modPath + "\\entities\\flowers\\" + iname + "\\" + iname + ".qb", qb);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\flowers\\" + iname + "\\" + iname + "_iconic.qb", qbi);
            System.IO.File.WriteAllBytes(modPath + "\\entities\\flowers\\" + iname + "\\" + iname + ".png", png);
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
            : this("", "", 0, 0, 0.0f, File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"), 
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"), 
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"), 
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"), 
                  File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"))
        { }

        public Weapon(String nam, String des, int ilvl, int dam, float rea)
            : this(nam, des, ilvl, dam, rea, File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"), File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.qb"), File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"), File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"), File.ReadAllBytes(MainForm.localPath + "\\Configs\\blank.png"))
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

        public List<Crafter> crafters;

        public List<Recipe> recipes;
        public List<Armor> armors;
        public List<Construction> constructions;
        public List<Flower> flowers;
        public List<Weapon> weapons;

        public List<String[]> extraAliases = new List<string[]>();
        public List<String[]> extraMixintos = new List<string[]>();
        public List<String[]> extraOverrides = new List<string[]>();
        public String OutsideFolderPath = "";

        public MOD()
        {
            name = "";
            apiVersion = "1";
            manifest = new ManifestJSON();
            crafters = new List<Crafter>();
            recipes = new List<Recipe>();
            armors = new List<Armor>();
            constructions = new List<Construction>();
            flowers = new List<Flower>();
            weapons = new List<Weapon>();
        }
        public MOD(string nam)
        {
            name = nam;
            apiVersion = "1";
            manifest = new ManifestJSON();
            crafters = new List<Crafter>();
            recipes = new List<Recipe>();
            armors = new List<Armor>();
            constructions = new List<Construction>();
            flowers = new List<Flower>();
            weapons = new List<Weapon>();
        }

        public void AddArmor(Armor armr)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for (int i = 0; i < armors.Count; i++)
            {
                if (armors[i].iname == armr.iname)
                {
                    changed = true;
                    armors[i] = armr;
                }
            }
            if (changed == false)
            {
                armors.Add(armr);
            }
        }
        public void RemoveArmor(Armor armr)
        {
            armors.Remove(armr);
        }

        public void AddConstruction(Construction constr)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for (int i = 0; i < constructions.Count; i++)
            {
                if (constructions[i].iname == constr.iname)
                {
                    changed = true;
                    constructions[i] = constr;
                }
            }
            if (changed == false)
            {
                constructions.Add(constr);
            }
        }
        public void RemoveConstruction(Construction constr)
        {
            constructions.Remove(constr);
        }

        public void AddFlower(Flower flow)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for (int i = 0; i < flowers.Count; i++)
            {
                if (flowers[i].iname == flow.iname)
                {
                    changed = true;
                    flowers[i] = flow;
                }
            }
            if (changed == false)
            {
                flowers.Add(flow);
            }
        }
        public void RemoveFlower(Flower flow)
        {
            flowers.Remove(flow);
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

        public void AddRecipe(Recipe recp)
        {
            bool changed = false;
            //Search for an item of the same name, if one exsists then it updates the information, else it adds it to the list.
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].name == recp.name)
                {
                    changed = true;
                    recipes[i] = recp;
                }
            }
            if (changed == false)
            {
                recipes.Add(recp);
            }
        }
        public void RemoveRecipe(Recipe recp)
        {
            recipes.Remove(recp);
        }

        public List<String> GetItemAliasesForRecipe()
        {
            List<String> items = new List<String>();

            //Get aliases from armor
            foreach (Armor armr in armors)
            {
                items.Add(name + ":" + "armor:" + armr.iname);
            }

            //Get aliases from weapons
            foreach (Flower flow in flowers)
            {
                items.Add(name + ":" + "flower:" + flow.iname);
            }

            //Get aliases from weapons
            foreach (Weapon weap in weapons)
            {
                items.Add(name + ":" + "weapon:" + weap.iname);
            }

            //Get aliases from ...

            return items;
        }

        public void BuildManifest()
        {
            manifest.name = name;
            manifest.apiVersion = apiVersion;

            manifest.aliases.Clear();
            manifest.mixintos.Clear();
            manifest.overrides.Clear();

            //Add Armors to manifest
            foreach (Armor armr in armors)
            {
                manifest.AddAlias("armor:" + armr.iname, "file(entities/armor/" + armr.iname + ")");
            }
            //Add Constructables to manifest
            foreach (Construction constr in constructions)
            {
                manifest.AddAlias(constr.type.ToLower() + ":" + constr.iname, "file(entities/build/" + constr.type.ToLower() + "/" + constr.iname + ")");
                manifest.AddMixinto("stonehearth/data/build/building_parts.json", "file(entities/build/" + constr.type.ToLower() + "/" + constr.iname + "/part.json)");
            }
            //Add wildflowers to manifest
            foreach (Flower flow in flowers)
            {
                manifest.AddAlias("flower:" + flow.iname, "file(entities/flowers/" + flow.iname + ")");
                manifest.AddAlias("flower:" + flow.iname + ":wild", "file(entities/flowers/" + flow.iname + "/" + flow.iname + "_wild.json)");
                manifest.AddMixinto("stonehearth/scenarios/scenario_index.json", "file(entities/flowers/" + flow.iname + "/" + flow.iname + "_index.json)");
            }
            //Add weapons to manifest
            foreach (Weapon weap in weapons)
            {
                manifest.AddAlias("weapon:" + weap.iname, "file(entities/weapons/" + weap.iname + ")");
            }

            //add recipes to manifest
            foreach (Recipe recp in recipes)
            {
                manifest.AddMixinto("stonehearth/jobs/" + recp.crafter.Remove(0,recp.crafter.IndexOf(":")+1) + "/recipes/recipes.json", 
                                    "file(recipes/" + recp.crafter.Remove(0, recp.crafter.IndexOf(":") + 1) + "/" + utils.LowerNoSpaces(recp.name) +"_mixinto.json)");
            }

            //add Extra/Additional Aliases/Mixintos/Overrides
            foreach (String[] str in extraAliases)
            {
                manifest.AddAlias(str[0],str[1]);
            }
            foreach (String[] str in extraMixintos)
            {
                manifest.AddMixinto(str[0], str[1]);
            }
            foreach (String[] str in extraOverrides)
            {
                manifest.AddOverride(str[0], str[1]);
            }
        }
        public void BuildModFolder(String modPath)
        {
            BuildManifest();

            //make folder if needed
            if (!System.IO.Directory.Exists(modPath + "\\" + name))
            {
                System.IO.Directory.CreateDirectory(modPath + "\\" + name);
                Console.WriteLine(modPath + "\\" + name);
            }

            //write item files
            foreach (Recipe recp in recipes)
            {
                recp.WriteRecipeFile(modPath + name);
            }

            foreach (Armor armr in armors)
            {
                armr.WriteArmorFile(modPath + name);
            }
            foreach (Construction constr in constructions)
            {
                constr.WriteConstructionFile(modPath + name);
            }
            foreach (Flower flow in flowers)
            {
                flow.WriteFlowerFile(modPath + name);
            }
            foreach (Weapon weap in weapons)
            {
                weap.WriteWeaponFile(modPath + name);
            }

            //write manifest
            manifest.Write(modPath + name + "\\");

            //Copy External Folder
            if (System.IO.Directory.Exists(OutsideFolderPath) && OutsideFolderPath != "")
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(OutsideFolderPath, "*",
                    SearchOption.AllDirectories))
                { 
                    Directory.CreateDirectory(dirPath.Replace(OutsideFolderPath, modPath + name));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(OutsideFolderPath, "*.*",
                    SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(OutsideFolderPath, modPath + name), true);
                }
            }

        }
        public void SaveSMOD(String smodPath)
        {
            BuildModFolder(MainForm.localPath + "\\");

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(MainForm.localPath + "\\" + name, name);
                    zip.Save(smodPath);
                }
            }
            catch(IOException e)
            {
                //FIX need to tell the user that It didn't save the smod and why
                Debug.WriteLine("IOExceotion: " + e.Message);
            }
            

            System.IO.Directory.Delete(MainForm.localPath + "\\" + name, true);
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
        public List<Crafter> SHCrafters;
        public List<String> CommonMaterialTags;
        public List<String> AliasSkipWords;

        public CONFIG( bool getSHsmodPath)
        {
            if(getSHsmodPath)
                GetSHsmodPath();
            SHCrafters = new List<Crafter>();
            CommonMaterialTags = new List<string>();
            AliasSkipWords = new List<string>();

            //SAVECONFIG();
        }
        public void SAVECONFIG()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(Form1.localPath + "\\Configs\\config.json"))
            //{
            //    file.Write(json);
            //}
            System.IO.File.WriteAllText(MainForm.localPath + "\\Configs\\config.json", json, Encoding.ASCII);
        }
        public CONFIG LOADCONFIG()
        {
            return JsonConvert.DeserializeObject<CONFIG>(System.IO.File.ReadAllText(MainForm.localPath + "\\Configs\\config.json"));
        }

        public void GetSHCrafters()
        {
            SHCrafters.Clear();
            //Read stonehearth.smod and get everybody who has a recipe.json
            using (ZipFile zip = ZipFile.Read(MainForm.config.SHsmodPath))
            {
                foreach (ZipEntry e in zip)
                {
                    if (e.FileName.Contains("recipes.json"))
                    {
                        //get crafter's name
                        String str = e.FileName.Remove((e.FileName.Length - 21));
                        str = str.Remove(0, 17);

                        //get categories
                        List<String> cate = new List<string>();
                        String[] strArray;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            e.Extract(stream);
                            stream.Position = 0;
                            Console.WriteLine("Extracted to stream:" + stream.ToString());
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                strArray = sr.ReadToEnd().Split('\n');
                            }
                        }
                        for (int i = 0; i < strArray.Length - 1; i++)
                        {
                            //if the next line contains 'ordinal' then THIS line must contain the category name.
                            if (strArray[i + 1].Contains("ordinal"))
                            {
                                cate.Add(strArray[i].Remove(strArray[i].LastIndexOf('"')).Remove(0, strArray[i].IndexOf('"') + 1));
                                Console.Write(str + " - " +strArray[i].Remove(strArray[i].LastIndexOf('"')).Remove(0, strArray[i].IndexOf('"') + 1));
                            }
                        }

                        SHCrafters.Add(new Crafter("stonehearth", str, cate));
                    }
                }
            }
        }
        public void GetSHsmodPath()
        {
            String path = "";
            if (System.IO.Directory.Exists("C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Stonehearth"))
            {
                path = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Stonehearth\\mods\\stonehearth.smod";
            }
            else if (System.IO.Directory.Exists("C:\\Program Files\\Steam\\SteamApps\\common\\Stonehearth"))
            {
                path = "C:\\Program Files\\Steam\\SteamApps\\common\\Stonehearth\\mods\\stonehearth.smod";
            }
            else
            {
                bool foundSMOD = false;
                OpenFileDialog filedialog = new OpenFileDialog();
                filedialog.Filter = "stonehearth.smod|stonehearth.smod";
                filedialog.FilterIndex = 1;
                filedialog.Multiselect = false;

                while (foundSMOD == false)
                {
                    if (filedialog.ShowDialog() == DialogResult.OK)
                    {
                        if (filedialog.FileName != "")
                        {
                            foundSMOD = true;
                        }
                    }
                }
                path = filedialog.FileName;
            }
            SHsmodPath = path;
        }
    }
}
