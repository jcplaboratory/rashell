using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rashell
{
    class Rashell
    {
        #region "Initialization"
        private void Init()
        {
            string version = "1.0a";
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
            setPwd(user_home);

            if (use_short)
            {
                Console.Write(sys_usr_short + "@" + "rashell:>");
                setShellpwd(sys_usr_short + "@" + "rashell:>");
            } else {
                Console.Write(sys_usr + "@" + "rashell:>");
                setShellpwd(sys_usr + "@" + "rashell:>");
            }

        }
        private void Listen()
        {
            Executor execute = new Executor();
            string stdin = null;
        Start:
            stdin = null;
            stdin = Console.ReadLine().ToLower();

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
            //writes console welcome
            if (!string.IsNullOrEmpty(stdin))
            {

                execute.Starter(stdin);
                Console.Write(getShellpwd());
                goto Start;
            }
            else {
                Console.Write(getShellpwd());
                goto Start;
            }
        }
        #endregion;

        #region "Session Vars"
        protected string pwd;
        protected string shellpwd;
        public void setPwd(string pwd)
        {
            this.pwd = pwd;
        }
        public string getPwd()
        {
            return this.pwd;
        }

        public void setShellpwd(string shellpwd)
        {
            this.shellpwd = shellpwd;
        }
        public string getShellpwd()
        {
            return this.shellpwd;
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
