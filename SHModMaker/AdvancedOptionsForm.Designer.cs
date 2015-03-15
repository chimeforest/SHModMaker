namespace SHModMaker
{
    partial class AdvancedOptionsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_alias = new System.Windows.Forms.TextBox();
            this.txt_mixinto = new System.Windows.Forms.TextBox();
            this.txt_override = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_extFolder = new System.Windows.Forms.TextBox();
            this.btn_broswe = new System.Windows.Forms.Button();
            this.btn_fin = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Overrides:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Mixintos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Aliases:";
            // 
            // txt_alias
            // 
            this.txt_alias.Location = new System.Drawing.Point(12, 25);
            this.txt_alias.Multiline = true;
            this.txt_alias.Name = "txt_alias";
            this.txt_alias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_alias.Size = new System.Drawing.Size(507, 84);
            this.txt_alias.TabIndex = 22;
            // 
            // txt_mixinto
            // 
            this.txt_mixinto.Location = new System.Drawing.Point(12, 128);
            this.txt_mixinto.Multiline = true;
            this.txt_mixinto.Name = "txt_mixinto";
            this.txt_mixinto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_mixinto.Size = new System.Drawing.Size(507, 84);
            this.txt_mixinto.TabIndex = 23;
            // 
            // txt_override
            // 
            this.txt_override.Location = new System.Drawing.Point(12, 231);
            this.txt_override.Multiline = true;
            this.txt_override.Name = "txt_override";
            this.txt_override.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_override.Size = new System.Drawing.Size(507, 84);
            this.txt_override.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "External Folder:";
            // 
            // txt_extFolder
            // 
            this.txt_extFolder.Location = new System.Drawing.Point(98, 323);
            this.txt_extFolder.Name = "txt_extFolder";
            this.txt_extFolder.Size = new System.Drawing.Size(340, 20);
            this.txt_extFolder.TabIndex = 26;
            // 
            // btn_broswe
            // 
            this.btn_broswe.Location = new System.Drawing.Point(444, 321);
            this.btn_broswe.Name = "btn_broswe";
            this.btn_broswe.Size = new System.Drawing.Size(75, 23);
            this.btn_broswe.TabIndex = 27;
            this.btn_broswe.Text = "Browse";
            this.btn_broswe.UseVisualStyleBackColor = true;
            this.btn_broswe.Click += new System.EventHandler(this.btn_broswe_Click);
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(98, 350);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(340, 23);
            this.btn_fin.TabIndex = 35;
            this.btn_fin.Text = "Finished";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // btn_help
            // 
            this.btn_help.Location = new System.Drawing.Point(444, 350);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(75, 23);
            this.btn_help.TabIndex = 100;
            this.btn_help.Text = "Help!";
            this.btn_help.UseVisualStyleBackColor = true;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(12, 349);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 30;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // AdvancedOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(531, 384);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.btn_broswe);
            this.Controls.Add(this.txt_extFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_override);
            this.Controls.Add(this.txt_mixinto);
            this.Controls.Add(this.txt_alias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedOptionsForm";
            this.Text = "Advanced Options";
            this.Load += new System.EventHandler(this.AdvancedOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_alias;
        private System.Windows.Forms.TextBox txt_mixinto;
        private System.Windows.Forms.TextBox txt_override;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_extFolder;
        private System.Windows.Forms.Button btn_broswe;
        private System.Windows.Forms.Button btn_fin;
        private System.Windows.Forms.Button btn_help;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}