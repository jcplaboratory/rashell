using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rashell.Commands;

namespace Rashell
{
    internal class Command
    {
        public bool Clear()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception E)
            {
                return false;
            }
            return true;
        }

        public bool Exit()
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception E)
            {
                return false;
            }
            return true;
        }

        public bool Ls(List<string> arguments)
        {
            ls list = new ls();
            if (list.main(arguments)) {
                return true;
            } else
            {
                return false;
            }
        }

        public bool Time()
        {
            try
            {
                Console.WriteLine("The current time is: " + DateTime.Now.ToString("HH:mm:ss:fff") + "\n");
            }
            catch (Exception E)
            {
                return false;
            }
            return true;
        }

        public bool Date()
        {
            try
            {
                Console.WriteLine("The current date is: " + DateTime.Now.ToLongDateString() + "\n");
            }
            catch (Exception E)
            {
                return false;
            }
            return true;
        }

        public bool Mkdir(List<string> arguments)
        {
            mkdir MKDIR = new mkdir();

            if (MKDIR.main(arguments))
            {
                return true;
            } else
            {
                return false;
            }      
        }

        public bool cd(List<string> directory)
        {
            Formatters format = new Formatters();
            Rashell shell = new Rashell();

            if (directory.Count != 0)
            {
                try
                {
                    string dir = format.RemoveSpace(directory[0].ToLower());

                    if (!string.IsNullOrEmpty(dir) && !string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
                    {
                        Directory.SetCurrentDirectory(dir);

                        string workdir = shell.setShellWorkingDirectory(shell.getSessionUser(), dir);
                        Rashell.ShellSessionDirectory = workdir;
                    } else
                    {
                        Console.WriteLine("The directory you specified is invalid.");
                    }
                }catch (Exception e)
                {

                }
            }
            return true;
        }

        public bool pwd()
        {
            string pwd = Directory.GetCurrentDirectory().ToString();
            Console.WriteLine("Working Directory: \"" + pwd + "\".");
            return true;
        }
    }
}