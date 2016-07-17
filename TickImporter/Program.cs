using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickImporter
{
    static class Program
    {
        private static List<Thread> Threads = new List<Thread>();
        private static Queue<InputFile> Queue = new Queue<InputFile>();
        private static string StartingLocation;
        private static Form1 gui;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new Form1();
            Application.Run(gui);
        }

        public static void BuildQueue()
        {
            try
            {
                var txtFiles = Directory.EnumerateFiles(StartingLocation, "*.csv", SearchOption.AllDirectories);
                foreach (string currentFile in txtFiles)
                {
                    if (currentFile != null)
                    {
                        Queue.Enqueue(new InputFile(currentFile));
                        gui.Log(currentFile);
                    }
                }
            }
            catch (Exception e)
            {
                gui.Log(e.Message);
            }
            gui.Log(String.Format("{0} files queued.", Queue.Count()));
        }

        public static InputFile Next()
        {
            return Queue.Dequeue();
        }

        public static void UpdateStatus( InputFile file, Status status )
        {
            file.Status = status;
            gui.Log(file.FileName + " is " + status.ToString("g"));
        }

        public static void SetFolder( string path )
        {
            StartingLocation = path;
            BuildQueue();
        }

        public static void SpawnThread()
        {
            ReadWriteCycle();
        }

        public static void ReadWriteCycle()
        {
            try
            {
                InputFile n = Next();
                while (n != null)
                {
                    try
                    {
                        n.Read();
                        n.Write();
                        UpdateStatus(n, Status.COMPLETE);
                    }
                    catch (Exception e)
                    {
                        UpdateStatus(n, Status.ERROR);
                        gui.Log(e.Message);
                    }
                    n = Next();
                }
            }
            catch (Exception ex)
            {
                gui.Log(ex.Message);
            }
        }
    }
}
