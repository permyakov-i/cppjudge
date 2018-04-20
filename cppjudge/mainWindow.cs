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


namespace cppjudge
{
    public partial class mainWindowForm : Form
    {
        bool isOk = true; // флаг ошибки
        string CurrentFile;   // Текущий открытый файл
        string testResult;   // Выход теста      
        int globalGrade=0;  // Оценка
        int testsCount=0;   // Количество тестов
        int currTest = 1; // Номер текущего теста
        int timeLimit = 150;
        int memoryLimit = 268435456; // Ограничение памяти
        string timeLim = ""; // Поле времени
        string memLimit = ""; // Поле памяти
        string compilerPath = ""; // Поле пути к компилятору
        string directoryPath = ""; // Путь к папке с тестами       
        public mainWindowForm()
        {
            InitializeComponent();
            //Скомпилировать программу
        }

        // Обработки кнопки компиляции
        private void compileFile_Click(object sender, EventArgs e)
        {
            compileCode();
            statWindow.Clear();
        }

        // Запуск одного теста
        public void runTest(string fileName, string expectedResult)
        {
            isOk = true;
            bool timeout = true;
            string result="";
            // Загрузить конфигурацию
            loadConfig();
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
            // Считать ограничение по времени
            if (timeLim.Length != 0)
            {
                Int32.TryParse(timeLim, out timeLimit);
            }
            // Считать ограничение по памяти
            if (memLimit.Length != 0)
            {
                Int32.TryParse(memLimit, out memoryLimit);
            }

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
            // Убить процесс если память превышена
            if (memoryUsed > memoryLimit)
            {
                proc.Kill();
            }
            foreach (string s in readText)
            {
                proc.StandardInput.WriteLine(s);          
            }
            timeout=proc.WaitForExit(timeLimit*1000);
            //Убить процесс если время превышено
            if (!timeout)
            {
                 proc.Kill();
            }
            // Вывести ошибку памяти
            if (memoryUsed> memoryLimit)
            {
                result = "[FAIL] Out of memory " + (memoryUsed/ 1024).ToString() + "KB used";
                statWindow.Text += result + " in test №" + currTest.ToString()+"\n";
                isOk = false;
            }
            TimeSpan timeElapsed = sw.Elapsed; //считать время выполнения
            // Вывести ошибку времени выполнения         
            if (timeElapsed.TotalSeconds > timeLimit)
            {
                result = "[FAIL] Timeout " + timeElapsed.TotalSeconds.ToString() + " seconds";
                statWindow.Text += result + " in test №" + currTest.ToString() + "\n";
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
                testResult= stdout;
            else
                testResult= result;

            int grade = compareResult(expectedResult, testResult);
            globalGrade += grade;
        }

        /* Скомпилировать тестируемую программу
         */
        public void compileCode ()
        {
            loadConfig();
            string exeName = Path.GetFileNameWithoutExtension(CurrentFile);
            Process proc = new Process();
            proc.StartInfo.FileName = @compilerPath;
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

        // Обработчик кнопки открытия файла
        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
            }
        }

        // Обработчик кнопки открытия тестов
        private void openTests_Click(object sender, EventArgs e)
        {
            statWindow.Clear();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directoryPath = folderBrowserDialog1.SelectedPath;
            }
            ProcessDirectory(directoryPath);
        }

        // Обработка тестов в папке
        public void ProcessDirectory(string targetDirectory)
        {
            folderItems.Items.Clear();
            // Process the list of files found in thedirectory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
            }
        }

        // Проверка результатов тестирования
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

        // Добавление файлов в виджет
        public void ProcessFile(string path)
        {
            folderItems.Items.Add(path);
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            testAll(directoryPath);
            int grade = (globalGrade / (testsCount - 2)) * 100;
            statWindow.Text += "Overall grade: " + grade.ToString() + "\n";
        }

        private void testAll(string targetDirectory)
        {
            // Тестировать
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            testsCount = fileEntries.Count();
            
            string prevFile = null;
            foreach (string fileName in fileEntries)
            {
                if (prevFile != null && !prevFile.EndsWith(".a"))
                {
                    //System.Threading.ThreadStart starter = () => runTest(prevFile, fileName);
                    //System.Threading.Thread thread = new System.Threading.Thread(starter);
                    //thread.Start();
                    runTest(prevFile,fileName);
                }
                prevFile = fileName;
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Config configForm = new Config(this);
            configForm.Show();
        }

        private void loadConfig()
        {
            string startupPath = Environment.CurrentDirectory;
            string[] lines = System.IO.File.ReadAllLines(@startupPath + "//Config.txt");
            memLimit = lines[0];
            timeLim = lines[1];
            compilerPath = lines[2];
        }
    }
}
