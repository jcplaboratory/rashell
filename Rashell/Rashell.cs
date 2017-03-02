using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Rashell
{
    class Rashell
    {
        private List<string> EnvironmentPaths = new List<string>();
        private List<string> KnownExtensions = new List<string>();
        private string DEF_WORKING_DIR;
    
        #region "Initialization"
        private void Init()
        {
            string version = "0.2a";
            string platform = Environment.OSVersion.ToString();
            string sys_usr = Environment.UserName.ToString().ToLower();
            string user_home = Environment.ExpandEnvironmentVariables("%userprofile%");
            string machine = Environment.MachineName.ToString();
            string sys_usr_short = null;
            bool use_short = false;

           

            if (sys_usr.IndexOf(" ") > 0)
            {
                sys_usr_short = sys_usr.Substring(0, sys_usr.IndexOf(" "));
                use_short = true;
            } else { sys_usr_short = null; }

            string sys_arch = null;

            if (Environment.Is64BitOperatingSystem)
            {
                sys_arch = "x86-x64";
            } else {
                sys_arch = "x86";
            }

            //Load Configuration
            Configuration config = new Configuration();
            config.Read();
            this.EnvironmentPaths = config.GetEnvironmentPaths();
            this.KnownExtensions = config.GetKnownExtensions();
            this.DEF_WORKING_DIR = config.GetWorkingDir();

            //Console default settings
            Console.Title = "Rashell | ~v " + version + " | " + "~m " + machine + " | "+ platform + " | " + sys_arch;
            
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();

            if (config.DisplayMSG()) //Display Rashell Welcome Message
            {
                DisplayWelcome(version);
            }
            

            if (string.IsNullOrEmpty(this.DEF_WORKING_DIR) || string.IsNullOrWhiteSpace(this.DEF_WORKING_DIR))
            {
                WorkingDirectory = user_home;
            }
            else
            { WorkingDirectory = this.DEF_WORKING_DIR; }

            if (use_short)
            {
                Console.Write(sys_usr_short + "@" + "rashell:>");
                ShellWorkingDirectory = sys_usr_short + "@" + "rashell:>";
            } else {
                Console.Write(sys_usr + "@" + "rashell:>");
                ShellWorkingDirectory = sys_usr + "@" + "rashell:>";
            }
           
        }
        private void Listen()
        {
            string stdin = null;

        Start:
            stdin = null;
            stdin = Console.ReadLine().ToLower();

            if (!string.IsNullOrEmpty(stdin) && !string.IsNullOrWhiteSpace(stdin))
            {
                //Count Double Inverted Commas
                int commaCount = 0;
                foreach (char c in stdin)
                {
                    if (c.ToString() == "\"")
                    {
                        commaCount++;
                    }
                }

                double rm = 0; //Check if num of d inverted commas is uneven
                rm = commaCount % 2;
                string read = null;
                if (!rm.Equals(0))
                {
                    append: //append stdin until comma is even
                    Console.Write(":>");
                    read = Console.ReadLine();
                    bool found = false;
                    foreach (char c in read)
                    {
                        if (!c.ToString().Equals("\"") && !found)
                        {
                            stdin = stdin + c;
                        }
                        else
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        goto append;
                    }
                }

                Starter(stdin);
                Console.Write(ShellWorkingDirectory);
                goto Start;
            }
            else
            {
                Console.Write(ShellWorkingDirectory);
                goto Start;
            }
        }
        private void DisplayWelcome(string version)
        {
            string msg = null;
            msg = "Rashell Dynamic Command Processor\n"
                  + "Version " + version + "\n"
                  + "2008-2017 J.C.P Laboratory, Under CPGSL V4.\n";
            Console.WriteLine(msg);
        }
        #endregion

        #region "Session Vars"
        protected string WorkingDir;
        protected string ShellWorkingDir;

        public string WorkingDirectory
        {
            get { return WorkingDir; }
            set { WorkingDir = value; }
        }

        public string ShellWorkingDirectory
        {
            get { return ShellWorkingDir; }
            set { ShellWorkingDir = value; }
        }
        #endregion

        #region "Loaders"
        public void Starter(string stdin)
        {
            Formatters format = new Formatters();
            Executor execute = new Executor();
            string cmd = format.Break(stdin);
            string cmdLoc = Find(cmd);

            if (!string.IsNullOrEmpty(cmdLoc))
            {
                execute.Execute(cmdLoc, format.getArguments());
            }
            else { execute.exec_in(cmd); }

        }

        public string Find(string cmd)
        {
            string cmdLoc = null;

            if (!File.Exists(cmd))
            {
                foreach (string ex in this.KnownExtensions)
                {
                    cmdLoc = cmd + ex;
                    if (File.Exists(cmdLoc))
                    {
                        return cmdLoc;
                    }
                    else
                    {
                        foreach (string path in this.EnvironmentPaths)
                        {
                            if (!Path.HasExtension(cmd)) //Check is extension is already present in path
                            {
                                foreach (string x in this.KnownExtensions)
                                {
                                    cmdLoc = path + "\\" + cmd + x;

                                    if (File.Exists(cmdLoc))
                                    {
                                        return cmdLoc;
                                    }
                                }
                            }
                            else
                            {
                                cmdLoc = path + "\\" + cmd;
                                if (File.Exists(cmdLoc))
                                {
                                    return cmdLoc;
                                }
                            }


                        }
                    }
                }



            }
            else
            {
                return cmd;
            }

            return null;
        }
        #endregion;
        static void Main(string[] args)
        {
            //Call init functions
            Rashell shell = new Rashell();

            shell.Init();
            shell.Listen();
        }
    }
}
