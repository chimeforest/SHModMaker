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
    public partial class HowToForm : Form
    {
        public HowToForm()
        {
            InitializeComponent();
            webBrowser1.Navigate("file:///" + System.IO.Directory.GetCurrentDirectory() + "/Help/Help.html");
        }

        public HowToForm(String page)
        {
            InitializeComponent();
            //Add code to put it on the right page
            webBrowser1.Navigate("file:///" + System.IO.Directory.GetCurrentDirectory() + "/Help/" + page +".html");
        }

        private void HowToForm_Load(object sender, EventArgs e)
        {

        }
    }
}
