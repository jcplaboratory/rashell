using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rashell.Commands;

namespace Rashell
{
  /// <summary>
  /// Contains the implementations of the built-in commands of Rashell.
  /// </summary>
    internal class Command
    {
        //The dictionary of the built-in commands.
        private Dictionary<int, string> dictionary = new Dictionary<int, string>();

        public Command()
        {
            this.InitCommandDict();
        }

        /// <summary>
        /// Initializes the class and the Dictionary of commands.
        /// </summary>
        private void InitCommandDict()
        {
            this.dictionary.Add(1, "clear");
            this.dictionary.Add(2, "bye");
            this.dictionary.Add(3, "quit");
            this.dictionary.Add(4, "exit");
            this.dictionary.Add(5, "exit");
            this.dictionary.Add(6, "time");
            this.dictionary.Add(7, "date");
            this.dictionary.Add(8, "mkdir");
            this.dictionary.Add(9, "ls");
            this.dictionary.Add(10, "pwd");
            this.dictionary.Add(11, "echo");
            this.dictionary.Add(12, "whoiam");
        }

        /// <summary>
        /// Returns the Dicionary of commands.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetDictionary()
        {
            return this.dictionary;
        }

        ///<summary>Contains the built-in commands.</summary>
        #region "Built-in Commands"
        /// <summary>
        /// Commands clears the console's screen
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Close the shell.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of the ls command. See the ls class under Commands>ls.
        /// </summary>
        /// <param name="arguments">The arguments to be parsed to ls.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the time in the console.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the current date in the console.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of the mkdir command. See the mkdir class under Commands>mkdir.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Changes the current directory
        /// </summary>
        /// <param name="directory">The directory to be working into.</param>
        /// <param name="dir">holds a formatted version of the directory path.</param>
        /// <returns></returns>
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
                        int CountSlash = 0;

                        //Formats the shell working directory.
                        foreach(char slash in Directory.GetCurrentDirectory())
                        {
                            if (slash.ToString() == "\\")
                            {
                                CountSlash++;
                            }
                        }
                       
                        string dirname = Directory.GetCurrentDirectory().Split('\\').Last();
                        string workdir;

                        if (CountSlash != 1)
                        {
                            if (!string.IsNullOrEmpty(dirname) || !string.IsNullOrWhiteSpace(dirname))
                            {
                                workdir = shell.setShellWorkingDirectory(shell.getSessionUser(), dirname);
                            }
                            else
                            {
                                workdir = shell.setShellWorkingDirectory(shell.getSessionUser(), dir);
                            }
                        } else
                        {
                            workdir = shell.setShellWorkingDirectory(shell.getSessionUser(), Directory.GetCurrentDirectory());
                        }

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

        /// <summary>
        /// Prints the current working directory on console.
        /// </summary>
        /// <returns></returns>
        public bool pwd()
        {
            string pwd = Directory.GetCurrentDirectory().ToString();
            Console.WriteLine("Working Directory: \"" + pwd + "\".");
            return true;
        }

        /// <summary>
        /// prints a series of string characters on screen.
        /// </summary>
        /// <param name="text">The text to print on screen.</param>
        /// <returns></returns>
        public bool echo(List<string> text)
        {
            string output = null;
            foreach(string txt in text)
            {
                if(txt != null)
                {
                    if(output == null)
                    {
                        output = txt;
                    }else
                    {
                        output += " " + txt;
                    }
                }
            }

            Console.WriteLine(output);

            return true;
        }

        /// <summary>
        /// An extended version of the whoami command.
        /// </summary>
        /// <returns></returns>
        public bool whoami()
        {
            Rashell shell = new Rashell();
            string user = shell.getSessionUser();
            string domain = Environment.MachineName.ToString();
            string profile = "\"" + Environment.ExpandEnvironmentVariables("%userprofile%") + "\"";
            string sys_user = Environment.UserName.ToString();

            if(shell.IsAdministrator() == false)
            {
                Console.WriteLine("Session User: " + user + "\n" + "System User: " + sys_user + "\n" + "Elevation: Standard" + 
                    "\n" + "Domain: " + domain + "\n" + "Profile: " + profile);
            } else
            {
                Console.WriteLine("Session User: " + user + "\n" + "System User: " + sys_user + "\n" + "Elevation: Administrator" + 
                    "\n" + "Domain: " + domain + "\n" + "Profile: " + profile);
            }

            return true;
        }

        #endregion
    }
}