using System;

namespace Rashell
{
    class Rashell
    {
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

            //Console default settings
            Console.Title = "Rashell | ~v " + version + " | " + "~m " + machine + " | "+ platform + " | " + sys_arch;
            
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            WorkingDirectory = user_home;

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
            Executor execute = new Executor();
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

                execute.Starter(stdin);
                Console.Write(ShellWorkingDirectory);
                goto Start;
            }
            else
            {
                Console.Write(ShellWorkingDirectory);
                goto Start;
            }
        }

        #endregion;

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
        static void Main(string[] args)
        {
            //Call init functions
            Rashell shell = new Rashell();

            shell.Init();
            shell.Listen();
        }
    }
}
