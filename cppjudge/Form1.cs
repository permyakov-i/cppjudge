using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Collections;


namespace tinycpp
{
    public partial class Form1 : Form
    {
        string CurrentFile;
        public Form1()
        {
            InitializeComponent();        
            //Скомпилировать программу
        }

        private void testFile_Click(object sender, EventArgs e)
        {
            string exeName = Path.GetFileNameWithoutExtension(CurrentFile); 
            Process proc = new Process();
            proc.StartInfo.FileName = @"C:/Program Files/mingw-w64/x86_64-7.3.0-posix-seh-rt_v5-rev0/mingw64/bin/g++";
            proc.StartInfo.Arguments = CurrentFile + " -o" + exeName;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.EnableRaisingEvents = true;

            var errors = new StringBuilder();
            var output = new StringBuilder();
            var hadErrors = false;

            // Получить вывод
            proc.OutputDataReceived += (s, d) => {
                output.Append(d.Data);
            };

            // Получить ошибки
            proc.ErrorDataReceived += (s, d) => {
                if (!hadErrors)
                {
                    hadErrors = !String.IsNullOrEmpty(d.Data);
                }
                errors.Append(d.Data);
            };

            proc.Start();
            // Слушать поток
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();

            proc.WaitForExit();
            string stdout = output.ToString();
            string stderr = errors.ToString();

            if (proc.ExitCode != 0 || hadErrors)
            {
                MessageBox.Show("error:" + stderr);
            }

        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                textBox1.Text=sr.ReadToEnd();
                sr.Close();
            }
        }

        private void openTests_Click(object sender, EventArgs e)
        {
            string path = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
            }
            ProcessDirectory(path);
        }

        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);
        }

        public void ProcessFile(string path)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(path);
        }

        /*
        void StartProcess(bool hidden, string command, params string[] args)
        {
            ProcessStartInfo pStartInfo = new ProcessStartInfo();

            pStartInfo.FileName = command;
            pStartInfo.Arguments = string.Join(" ", args);
            pStartInfo.UseShellExecute = false;

            if (hidden)
            {
                pStartInfo.RedirectStandardError = true;
                pStartInfo.RedirectStandardOutput = true;
                pStartInfo.CreateNoWindow = true;
            }

            Process proc = new Process();
            proc.StartInfo = pStartInfo;

            proc.Start();

            textBox2.Clear();

            if (hidden)
            {
                while (!proc.StandardError.EndOfStream)
                {
                    textBox2.AppendText("  Error: " + proc.StandardError.ReadLine() + Environment.NewLine);
                }
            }
        }*/
    }
}
