namespace SHModMaker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_armor = new System.Windows.Forms.TabPage();
            this.btn_armor = new System.Windows.Forms.Button();
            this.tab_crop = new System.Windows.Forms.TabPage();
            this.btn_crop = new System.Windows.Forms.Button();
            this.tab_weapon = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.nud_weap_ilevel = new System.Windows.Forms.NumericUpDown();
            this.nud_weap_reach = new System.Windows.Forms.NumericUpDown();
            this.nud_weap_damage = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_weap_desc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_weap_PNGBrowse = new System.Windows.Forms.Button();
            this.txt_weap_png = new System.Windows.Forms.TextBox();
            this.btn_weap_QBIBrowse = new System.Windows.Forms.Button();
            this.btn_weap_QBBrowse = new System.Windows.Forms.Button();
            this.txt_weap_qb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_weap_qbi = new System.Windows.Forms.TextBox();
            this.txt_weap_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_weapon = new System.Windows.Forms.Button();
            this.tab_recipe = new System.Windows.Forms.TabPage();
            this.btn_recipe = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_mod_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_api_version = new System.Windows.Forms.TextBox();
            this.newModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportsmodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tab_armor.SuspendLayout();
            this.tab_crop.SuspendLayout();
            this.tab_weapon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_ilevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_reach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_damage)).BeginInit();
            this.tab_recipe.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tab_armor);
            this.tabControl1.Controls.Add(this.tab_crop);
            this.tabControl1.Controls.Add(this.tab_weapon);
            this.tabControl1.Controls.Add(this.tab_recipe);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(20, 50);
            this.tabControl1.Location = new System.Drawing.Point(12, 54);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(469, 318);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tab_armor
            // 
            this.tab_armor.Controls.Add(this.btn_armor);
            this.tab_armor.Location = new System.Drawing.Point(54, 4);
            this.tab_armor.Name = "tab_armor";
            this.tab_armor.Padding = new System.Windows.Forms.Padding(3);
            this.tab_armor.Size = new System.Drawing.Size(411, 310);
            this.tab_armor.TabIndex = 0;
            this.tab_armor.Text = "Armor";
            this.tab_armor.UseVisualStyleBackColor = true;
            // 
            // btn_armor
            // 
            this.btn_armor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_armor.Location = new System.Drawing.Point(6, 281);
            this.btn_armor.Name = "btn_armor";
            this.btn_armor.Size = new System.Drawing.Size(402, 23);
            this.btn_armor.TabIndex = 1;
            this.btn_armor.Text = "Add Armor";
            this.btn_armor.UseVisualStyleBackColor = true;
            // 
            // tab_crop
            // 
            this.tab_crop.Controls.Add(this.btn_crop);
            this.tab_crop.Location = new System.Drawing.Point(54, 4);
            this.tab_crop.Name = "tab_crop";
            this.tab_crop.Padding = new System.Windows.Forms.Padding(3);
            this.tab_crop.Size = new System.Drawing.Size(411, 310);
            this.tab_crop.TabIndex = 2;
            this.tab_crop.Text = "Crop";
            this.tab_crop.UseVisualStyleBackColor = true;
            // 
            // btn_crop
            // 
            this.btn_crop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_crop.Location = new System.Drawing.Point(6, 281);
            this.btn_crop.Name = "btn_crop";
            this.btn_crop.Size = new System.Drawing.Size(402, 23);
            this.btn_crop.TabIndex = 1;
            this.btn_crop.Text = "Add Crop";
            this.btn_crop.UseVisualStyleBackColor = true;
            // 
            // tab_weapon
            // 
            this.tab_weapon.Controls.Add(this.label11);
            this.tab_weapon.Controls.Add(this.nud_weap_ilevel);
            this.tab_weapon.Controls.Add(this.nud_weap_reach);
            this.tab_weapon.Controls.Add(this.nud_weap_damage);
            this.tab_weapon.Controls.Add(this.label10);
            this.tab_weapon.Controls.Add(this.label9);
            this.tab_weapon.Controls.Add(this.label8);
            this.tab_weapon.Controls.Add(this.txt_weap_desc);
            this.tab_weapon.Controls.Add(this.label7);
            this.tab_weapon.Controls.Add(this.btn_weap_PNGBrowse);
            this.tab_weapon.Controls.Add(this.txt_weap_png);
            this.tab_weapon.Controls.Add(this.btn_weap_QBIBrowse);
            this.tab_weapon.Controls.Add(this.btn_weap_QBBrowse);
            this.tab_weapon.Controls.Add(this.txt_weap_qb);
            this.tab_weapon.Controls.Add(this.label6);
            this.tab_weapon.Controls.Add(this.label5);
            this.tab_weapon.Controls.Add(this.txt_weap_qbi);
            this.tab_weapon.Controls.Add(this.txt_weap_name);
            this.tab_weapon.Controls.Add(this.label3);
            this.tab_weapon.Controls.Add(this.btn_weapon);
            this.tab_weapon.Location = new System.Drawing.Point(54, 4);
            this.tab_weapon.Name = "tab_weapon";
            this.tab_weapon.Padding = new System.Windows.Forms.Padding(3);
            this.tab_weapon.Size = new System.Drawing.Size(411, 310);
            this.tab_weapon.TabIndex = 1;
            this.tab_weapon.Text = "Weapon";
            this.tab_weapon.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "iLevel:";
            // 
            // nud_weap_ilevel
            // 
            this.nud_weap_ilevel.Location = new System.Drawing.Point(50, 113);
            this.nud_weap_ilevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_weap_ilevel.Name = "nud_weap_ilevel";
            this.nud_weap_ilevel.Size = new System.Drawing.Size(57, 20);
            this.nud_weap_ilevel.TabIndex = 22;
            this.nud_weap_ilevel.Value = new decimal(new int[] {
            9001,
            0,
            0,
            0});
            // 
            // nud_weap_reach
            // 
            this.nud_weap_reach.DecimalPlaces = 1;
            this.nud_weap_reach.Location = new System.Drawing.Point(50, 165);
            this.nud_weap_reach.Name = "nud_weap_reach";
            this.nud_weap_reach.Size = new System.Drawing.Size(57, 20);
            this.nud_weap_reach.TabIndex = 21;
            this.nud_weap_reach.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            // 
            // nud_weap_damage
            // 
            this.nud_weap_damage.Location = new System.Drawing.Point(50, 139);
            this.nud_weap_damage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_weap_damage.Name = "nud_weap_damage";
            this.nud_weap_damage.Size = new System.Drawing.Size(57, 20);
            this.nud_weap_damage.TabIndex = 20;
            this.nud_weap_damage.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Reach:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Dmg:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Desc:";
            // 
            // txt_weap_desc
            // 
            this.txt_weap_desc.Location = new System.Drawing.Point(50, 87);
            this.txt_weap_desc.Name = "txt_weap_desc";
            this.txt_weap_desc.Size = new System.Drawing.Size(350, 20);
            this.txt_weap_desc.TabIndex = 14;
            this.txt_weap_desc.Text = "THIS SWORD IS THE MOST EPIC SWORD EVAR!!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "PNG:";
            // 
            // btn_weap_PNGBrowse
            // 
            this.btn_weap_PNGBrowse.Location = new System.Drawing.Point(325, 61);
            this.btn_weap_PNGBrowse.Name = "btn_weap_PNGBrowse";
            this.btn_weap_PNGBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_weap_PNGBrowse.TabIndex = 12;
            this.btn_weap_PNGBrowse.Text = "Browse";
            this.btn_weap_PNGBrowse.UseVisualStyleBackColor = true;
            this.btn_weap_PNGBrowse.Click += new System.EventHandler(this.btn_weap_PNGBrowse_Click);
            // 
            // txt_weap_png
            // 
            this.txt_weap_png.Location = new System.Drawing.Point(219, 60);
            this.txt_weap_png.Name = "txt_weap_png";
            this.txt_weap_png.Size = new System.Drawing.Size(100, 20);
            this.txt_weap_png.TabIndex = 11;
            this.txt_weap_png.Text = "PNG HERE";
            // 
            // btn_weap_QBIBrowse
            // 
            this.btn_weap_QBIBrowse.Location = new System.Drawing.Point(325, 32);
            this.btn_weap_QBIBrowse.Name = "btn_weap_QBIBrowse";
            this.btn_weap_QBIBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_weap_QBIBrowse.TabIndex = 10;
            this.btn_weap_QBIBrowse.Text = "Browse";
            this.btn_weap_QBIBrowse.UseVisualStyleBackColor = true;
            this.btn_weap_QBIBrowse.Click += new System.EventHandler(this.btn_weap_QBIBrowse_Click);
            // 
            // btn_weap_QBBrowse
            // 
            this.btn_weap_QBBrowse.Location = new System.Drawing.Point(325, 5);
            this.btn_weap_QBBrowse.Name = "btn_weap_QBBrowse";
            this.btn_weap_QBBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_weap_QBBrowse.TabIndex = 9;
            this.btn_weap_QBBrowse.Text = "Browse";
            this.btn_weap_QBBrowse.UseVisualStyleBackColor = true;
            this.btn_weap_QBBrowse.Click += new System.EventHandler(this.btn_weap_QBBrowse_Click);
            // 
            // txt_weap_qb
            // 
            this.txt_weap_qb.Location = new System.Drawing.Point(219, 7);
            this.txt_weap_qb.Name = "txt_weap_qb";
            this.txt_weap_qb.Size = new System.Drawing.Size(100, 20);
            this.txt_weap_qb.TabIndex = 8;
            this.txt_weap_qb.Text = "QBHERE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "QB Iconic:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "QB Equip:";
            // 
            // txt_weap_qbi
            // 
            this.txt_weap_qbi.Location = new System.Drawing.Point(219, 34);
            this.txt_weap_qbi.Name = "txt_weap_qbi";
            this.txt_weap_qbi.Size = new System.Drawing.Size(100, 20);
            this.txt_weap_qbi.TabIndex = 4;
            this.txt_weap_qbi.Text = "OBICONICHERE";
            // 
            // txt_weap_name
            // 
            this.txt_weap_name.Location = new System.Drawing.Point(50, 7);
            this.txt_weap_name.Name = "txt_weap_name";
            this.txt_weap_name.Size = new System.Drawing.Size(100, 20);
            this.txt_weap_name.TabIndex = 2;
            this.txt_weap_name.Text = "SWORD OF AWESOME";
            this.txt_weap_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress_NoSpecChar);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name:";
            // 
            // btn_weapon
            // 
            this.btn_weapon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_weapon.Location = new System.Drawing.Point(6, 284);
            this.btn_weapon.Name = "btn_weapon";
            this.btn_weapon.Size = new System.Drawing.Size(402, 23);
            this.btn_weapon.TabIndex = 0;
            this.btn_weapon.Text = "Add Weapon";
            this.btn_weapon.UseVisualStyleBackColor = true;
            this.btn_weapon.Click += new System.EventHandler(this.btn_weapon_Click);
            // 
            // tab_recipe
            // 
            this.tab_recipe.Controls.Add(this.btn_recipe);
            this.tab_recipe.Location = new System.Drawing.Point(54, 4);
            this.tab_recipe.Name = "tab_recipe";
            this.tab_recipe.Padding = new System.Windows.Forms.Padding(3);
            this.tab_recipe.Size = new System.Drawing.Size(411, 310);
            this.tab_recipe.TabIndex = 3;
            this.tab_recipe.Text = "Recipe";
            this.tab_recipe.UseVisualStyleBackColor = true;
            // 
            // btn_recipe
            // 
            this.btn_recipe.Location = new System.Drawing.Point(6, 281);
            this.btn_recipe.Name = "btn_recipe";
            this.btn_recipe.Size = new System.Drawing.Size(399, 23);
            this.btn_recipe.TabIndex = 0;
            this.btn_recipe.Text = "Add Recipe";
            this.btn_recipe.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(493, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModToolStripMenuItem,
            this.loadModToolStripMenuItem,
            this.saveModToolStripMenuItem,
            this.exportsmodToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modManagerToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "mod name: ";
            // 
            // txt_mod_name
            // 
            this.txt_mod_name.Location = new System.Drawing.Point(84, 28);
            this.txt_mod_name.Name = "txt_mod_name";
            this.txt_mod_name.Size = new System.Drawing.Size(221, 20);
            this.txt_mod_name.TabIndex = 3;
            this.txt_mod_name.Text = "MOD_OF_AWESOME";
            this.txt_mod_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress_NoSpecChar);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "API Version:";
            // 
            // txt_api_version
            // 
            this.txt_api_version.Location = new System.Drawing.Point(382, 28);
            this.txt_api_version.Name = "txt_api_version";
            this.txt_api_version.Size = new System.Drawing.Size(100, 20);
            this.txt_api_version.TabIndex = 5;
            this.txt_api_version.Text = "1";
            // 
            // newModToolStripMenuItem
            // 
            this.newModToolStripMenuItem.Name = "newModToolStripMenuItem";
            this.newModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newModToolStripMenuItem.Text = "New Mod";
            // 
            // loadModToolStripMenuItem
            // 
            this.loadModToolStripMenuItem.Name = "loadModToolStripMenuItem";
            this.loadModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadModToolStripMenuItem.Text = "Load Mod";
            // 
            // saveModToolStripMenuItem
            // 
            this.saveModToolStripMenuItem.Name = "saveModToolStripMenuItem";
            this.saveModToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveModToolStripMenuItem.Text = "Save Mod";
            // 
            // exportsmodToolStripMenuItem
            // 
            this.exportsmodToolStripMenuItem.Name = "exportsmodToolStripMenuItem";
            this.exportsmodToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportsmodToolStripMenuItem.Text = "Export .smod";
            // 
            // modManagerToolStripMenuItem
            // 
            this.modManagerToolStripMenuItem.Name = "modManagerToolStripMenuItem";
            this.modManagerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modManagerToolStripMenuItem.Text = "Mod Manager";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 384);
            this.Controls.Add(this.txt_api_version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_mod_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHModMaker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_armor.ResumeLayout(false);
            this.tab_crop.ResumeLayout(false);
            this.tab_weapon.ResumeLayout(false);
            this.tab_weapon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_ilevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_reach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weap_damage)).EndInit();
            this.tab_recipe.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_armor;
        private System.Windows.Forms.TabPage tab_weapon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.TabPage tab_crop;
        private System.Windows.Forms.Button btn_weapon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_weap_PNGBrowse;
        private System.Windows.Forms.TextBox txt_weap_png;
        private System.Windows.Forms.Button btn_weap_QBIBrowse;
        private System.Windows.Forms.Button btn_weap_QBBrowse;
        private System.Windows.Forms.TextBox txt_weap_qb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_weap_qbi;
        private System.Windows.Forms.TextBox txt_weap_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_mod_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_api_version;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nud_weap_ilevel;
        private System.Windows.Forms.NumericUpDown nud_weap_reach;
        private System.Windows.Forms.NumericUpDown nud_weap_damage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_weap_desc;
        private System.Windows.Forms.Button btn_armor;
        private System.Windows.Forms.Button btn_crop;
        private System.Windows.Forms.TabPage tab_recipe;
        private System.Windows.Forms.Button btn_recipe;
        private System.Windows.Forms.ToolStripMenuItem newModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveModToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportsmodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

