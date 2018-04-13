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
        bool isOk = true; // флаг ошибки
        string CurrentFile;   // Текущий открытый файл
        string testResult;   // Выход теста
        int globalGrade=0;  // Оценка
        int testsCount=0;   // Количество тестов
        int currTest = 1; // Номер текущего теста
        int timeLimit = 150;
        int memoryLimit = 268435456; // Ограничение памяти
        public Form1()
        {
            InitializeComponent();        
            //Скомпилировать программу
        }

        private void testFile_Click(object sender, EventArgs e)
        {
            compileCode();
            textBox1.Clear();
        }

        public string runTest(string fileName)
        {
            isOk = true;
            string result="";
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
            Stopwatch sw = Stopwatch.StartNew();
            proc.Start();
            // Слушать поток
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            long memoryUsed = proc.PrivateMemorySize64; //считать память
            
            TimeSpan timeElapsed = sw.Elapsed; //считать время выполнения
            foreach (string s in readText)
            {
                proc.StandardInput.WriteLine(s);
            }         
            proc.WaitForExit();
            // Проверка памяти процесса
            if (memLimit.TextLength!=0)
            {
                Int32.TryParse(memLimit.Text, out memoryLimit);
            }
            if (memoryUsed> memoryLimit)
            {
                result = "[FAIL] Out of memory " + (memoryUsed/ 1024).ToString() + "KB used";
                textBox1.Text += result + " in test №" + currTest.ToString()+"\n";
                isOk = false;
            }
            // Проверка времени выполнения
            
            if (timeLim.TextLength!=0)
            {
                Int32.TryParse(timeLim.Text, out timeLimit);
            }
            if (timeElapsed.TotalSeconds > timeLimit)
            {
                result = "[FAIL] Timeout " + timeElapsed.TotalSeconds.ToString() + " seconds";
                textBox1.Text += result + " in test №" + currTest.ToString() + "\n";
                isOk = false;
            }
            // Вывод
            string stdout = output.ToString();
            string stderr = errors.ToString();
            currTest++;
            if (proc.ExitCode != 0 || hadErrors)
            {
                MessageBox.Show("error:" + stderr);
            }
            if (isOk)
                return stdout;
            else
                return result;
        }

        /* Скомпилировать тестируемую программу
         */
        public void compileCode ()
        {
            string exeName = Path.GetFileNameWithoutExtension(CurrentFile);
            Process proc = new Process();
            proc.StartInfo.FileName = @compilerPath.Text;
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
            }else
            {
                MessageBox.Show("Compilation successful");
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
            textBox1.Clear();
            string path = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
            }
            ProcessDirectory(path);
            int grade= (globalGrade/(testsCount-2))*100;
            textBox1.Text += "Overall grade: " + grade.ToString();
        }

        public void ProcessDirectory(string targetDirectory)
        {
            comboBox1.Items.Clear();
            // Process the list of files found in thedirectory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            testsCount = fileEntries.Count();
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
