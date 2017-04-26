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
                case "pwd":
                    this.command.pwd();
                    break;

                default:
                    Console.WriteLine("Command or Operator " + "\"" + command + "\"" + " not found." + "\nCheck syntax.");
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
            ProcessStartInfo property = new ProcessStartInfo(cmd)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = Arguments,
            };

            process.StartInfo = property;

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


            try
            {
                process.Start();
                process.BeginOutputReadLine();

                process.WaitForExit();
            } catch (Exception x)
            {
                if (x.ToString().Contains("requires elevation"))
                {
                    Console.WriteLine("Rashell: Unable to start application \"" + cmd + "\"" + ".");
                    format.ConsoleColorWrite("Requires Elevation", ConsoleColor.Red);

                    Console.Write("You want to restart Rashell with Elevated Priviledges? (Y/N):");
                    string reply = Console.ReadLine().ToLower();

                    reply = format.RemoveTab(reply);
                    reply = format.RemoveSpace(reply);

                    if (reply == "y" || reply == "yes")
                    {
                        shell.restartAsAdmin();
                    }


                } else if (x.ToString().Contains("specified executable is not a valid")) {
                    Console.WriteLine("Rashell: Unable to start \"" + cmd + "\"" + ".");
                    format.ConsoleColorWrite("Invalid File Type", ConsoleColor.Red);
                } else
                {
                    Console.WriteLine("Rashell: Unable to start \"" + cmd + "\"" + ".");
                    format.ConsoleColorWrite("Unknown Error", ConsoleColor.Yellow);
                }
            }
            

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

            return true;
        }

        #endregion "Executors"
    }
}