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
        string testResult;
        int globalGrade=0;
        public Form1()
        {
            InitializeComponent();        
            //Скомпилировать программу
        }

        private void testFile_Click(object sender, EventArgs e)
        {
            compileCode();
        }

        public string runTest(string fileName)
        {
            string result;
            // Прочитать файл теста
            string[] readText = File.ReadAllLines(fileName);

            string exeName = Path.GetFileNameWithoutExtension(CurrentFile);
            Process proc = new Process();
            proc.StartInfo.FileName = exeName;
            //proc.StartInfo.Arguments = CurrentFile + " -o" + exeName;
            proc.StartInfo.RedirectStandardInput = true;
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
            long memoryUsed = proc.PrivateMemorySize64;
            foreach (string s in readText)
            {
                proc.StandardInput.WriteLine(s);
            }         
            proc.WaitForExit();
            // Проверка памяти процесса
            
            if (memoryUsed>256)
            {
                result = "[FAIL] Out of memory " + memoryUsed.ToString() + "used";
            }
            string stdout = output.ToString();
            string stderr = errors.ToString();

            if (proc.ExitCode != 0 || hadErrors)
            {
                MessageBox.Show("error:" + stderr);
            }

            return stdout;
        }

        /* Скомпилировать тестируемую программу
         */
        public void compileCode ()
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
            textBox1.Text = globalGrade.ToString();
        }

        public void ProcessDirectory(string targetDirectory)
        {
            comboBox1.Items.Clear();
            // Process the list of files found in thedirectory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
                if (fileName.EndsWith(".a"))
                {
                    int grade = compareResult(fileName, testResult);
                    globalGrade += grade;
                } else
                {
                    testResult=runTest(fileName);
                }
            }
        }

        public int compareResult(string fileName, string result)
        {
            int grade = 0;
            // Прочитать файл теста
            string[] readText = File.ReadAllLines(fileName);
            foreach (string s in readText)
            {
                if (result==s)
                {
                    grade++;
                }
            }
            return grade;
        }

        public void ProcessFile(string path)
        {
            comboBox1.Items.Add(path);
        }
    }
}
