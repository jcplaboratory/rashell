using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rashell
{
    internal class Executor
    {
        protected Command command = new Command();
        private Formatters format = new Formatters();
        private Rashell shell = new Rashell();

        #region "Executors"

        public void exec_in(string command, List<string> Arguments)
        {
            switch (command)
            {
                case "clear":
                    this.command.Clear();
                    break;

                case "bye":
                    this.command.Exit();
                    break;

                case "quit":
                    this.command.Exit();
                    break;

                case "exit":
                    this.command.Exit();
                    break;

                case "time":
                    this.command.Time();
                    break;

                case "date":
                    this.command.Date();
                    break;

                case "mkdir":
                    this.command.Mkdir(Arguments);
                    break;
                case "ls":
                    this.command.Ls(Arguments);
                    break;
                case "cd":
                    this.command.cd(Arguments);
                    break;

                default:
                    Console.WriteLine("Command or Operator " + "\"" + command + "\"" + " not found." + "\n Check syntax.");
                    break;
            }
        }

        public bool Execute(string cmd, List<string> arguments)
        {
            int argsCt = arguments.Count;
            string Arguments = null;

            foreach (string arg in arguments)
            {
                Arguments += " " + arg;
            }

            Process process = new Process();

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = cmd;
            process.StartInfo.Arguments = Arguments;

            process.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    Console.WriteLine(e.Data);
                }
            );
            process.ErrorDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    Console.WriteLine(e.Data);
                }
            );

            process.Start();
            process.BeginOutputReadLine();

            //while (!process.HasExited)
            //{
            //    string stdin = null;
            //    stdin = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(stdin))
            //    {
            //        process.StandardInput.WriteLine(stdin);
            //    }
            //}

            //foreach (ProcessThread thread in process.Threads)
            //{
            //    if (thread.ThreadState == ThreadState.Wait)
            //    {
            //        string stdin = null;
            //        stdin = Console.ReadLine();
            //        if (!string.IsNullOrEmpty(stdin))
            //        {
            //            process.StandardInput.WriteLine(stdin);
            //        }
            //    }

            //}

            process.WaitForExit();

            return true;
        }

        #endregion "Executors"
    }
}