namespace SHModMaker
{
    partial class ConfigForm
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
            this.components = new System.ComponentModel.Container();
            this.btn_default = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.txt_shsmod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txt_crafters = new System.Windows.Forms.TextBox();
            this.txt_material_tags = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bnt_getSHcrafters = new System.Windows.Forms.Button();
            this.txt_alias_skip_words = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_default
            // 
            this.btn_default.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_default.Location = new System.Drawing.Point(185, 276);
            this.btn_default.Name = "btn_default";
            this.btn_default.Size = new System.Drawing.Size(80, 23);
            this.btn_default.TabIndex = 1;
            this.btn_default.Text = "Load Default";
            this.btn_default.UseVisualStyleBackColor = true;
            this.btn_default.Click += new System.EventHandler(this.btn_default_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_save.BackColor = System.Drawing.SystemColors.Control;
            this.btn_save.Location = new System.Drawing.Point(12, 276);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(80, 23);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Save Config";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_load.Location = new System.Drawing.Point(98, 276);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(80, 23);
            this.btn_load.TabIndex = 3;
            this.btn_load.Text = "Load Config";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // txt_shsmod
            // 
            this.txt_shsmod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_shsmod.Location = new System.Drawing.Point(12, 25);
            this.txt_shsmod.Name = "txt_shsmod";
            this.txt_shsmod.Size = new System.Drawing.Size(339, 20);
            this.txt_shsmod.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "stoneheath.smod";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_cancel.Location = new System.Drawing.Point(271, 276);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Canel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "SH Crafters";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txt_crafters
            // 
            this.txt_crafters.Location = new System.Drawing.Point(12, 75);
            this.txt_crafters.Multiline = true;
            this.txt_crafters.Name = "txt_crafters";
            this.txt_crafters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_crafters.Size = new System.Drawing.Size(166, 87);
            this.txt_crafters.TabIndex = 11;
            // 
            // txt_material_tags
            // 
            this.txt_material_tags.Location = new System.Drawing.Point(185, 75);
            this.txt_material_tags.Multiline = true;
            this.txt_material_tags.Name = "txt_material_tags";
            this.txt_material_tags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_material_tags.Size = new System.Drawing.Size(166, 87);
            this.txt_material_tags.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Common Material Tags";
            // 
            // bnt_getSHcrafters
            // 
            this.bnt_getSHcrafters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_getSHcrafters.Location = new System.Drawing.Point(76, 48);
            this.bnt_getSHcrafters.Name = "bnt_getSHcrafters";
            this.bnt_getSHcrafters.Size = new System.Drawing.Size(100, 21);
            this.bnt_getSHcrafters.TabIndex = 14;
            this.bnt_getSHcrafters.Text = "Get";
            this.bnt_getSHcrafters.UseVisualStyleBackColor = true;
            this.bnt_getSHcrafters.Click += new System.EventHandler(this.bnt_getSHcrafters_Click);
            // 
            // txt_alias_skip_words
            // 
            this.txt_alias_skip_words.Location = new System.Drawing.Point(12, 181);
            this.txt_alias_skip_words.Multiline = true;
            this.txt_alias_skip_words.Name = "txt_alias_skip_words";
            this.txt_alias_skip_words.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_alias_skip_words.Size = new System.Drawing.Size(166, 87);
            this.txt_alias_skip_words.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "SH alias skip words";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 311);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_alias_skip_words);
            this.Controls.Add(this.bnt_getSHcrafters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_material_tags);
            this.Controls.Add(this.txt_crafters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_shsmod);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_default);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_default;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.TextBox txt_shsmod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txt_crafters;
        private System.Windows.Forms.TextBox txt_material_tags;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bnt_getSHcrafters;
        private System.Windows.Forms.TextBox txt_alias_skip_words;
        private System.Windows.Forms.Label label4;
    }
}