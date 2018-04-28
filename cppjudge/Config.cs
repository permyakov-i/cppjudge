using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cppjudge
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        public Config(mainWindowForm f)
        {
            InitializeComponent();
        }

   
        private void Config_Load(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string[] lines = System.IO.File.ReadAllLines(@startupPath + "//Config.txt");
            memLimit.Text = lines[0];
            timeLim.Text = lines[1];
            compilerPath.Text = lines[2];
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string text = memLimit.Text + Environment.NewLine + timeLim.Text + Environment.NewLine + compilerPath.Text;
           
            System.IO.File.WriteAllText(startupPath+"\\config.txt", text);
        }
    }
}
