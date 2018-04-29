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
            foreach (string line in lines.Skip(3))
            {
                dataGridView1.Rows.Add(line.Substring(0,1), line.Substring(1));
            }
            refreshRowNumber();
        }

        private void refreshRowNumber()
        {
            int i = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["testNumber"].Value = i;
                i++;
            }
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string text = memLimit.Text + Environment.NewLine + timeLim.Text + Environment.NewLine + compilerPath.Text + Environment.NewLine;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["testGrade"].Value != null)
                {
                    text += row.Cells["testNumber"].Value.ToString() + " ";
                    text += row.Cells["testGrade"].Value.ToString() + Environment.NewLine;
                }
            }
            System.IO.File.WriteAllText(startupPath+"\\config.txt", text);
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            refreshRowNumber();
        }
    }
}
