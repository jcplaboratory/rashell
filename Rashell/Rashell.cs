using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace Rashell
{
    internal class Rashell
    {
        private List<string> EnvironmentPaths = new List<string>();
        private List<string> KnownExtensions = new List<string>();
        public static String[] RashellArguments;
        private string DEF_WORKING_DIR = null;
        static string ShellWorkingDirectory = null;
        private bool PRIORITIZE_BUILTIN_SHELL = false;
        private static Formatters format = new Formatters();

        #region "Initialization"

        private void Init()
        {
            string version = "0.2a Build 352 (Pansy Violet)";
            string platform = Environment.OSVersion.ToString();
            string sessionUser = getSessionUser();
            string user_home = Environment.ExpandEnvironmentVariables("%userprofile%");
            string machine = Environment.MachineName.ToString();

            string sys_arch = null;

            if (Environment.Is64BitOperatingSystem)
            {
                sys_arch = "x86-x64";
            }
            else
            {
                sys_arch = "x86";
            }

            //Load Configuration
            Configuration config = new Configuration();
            this.EnvironmentPaths = config.GetEnvironmentPaths();
            this.KnownExtensions = config.GetKnownExtensions();
            this.DEF_WORKING_DIR = config.GetWorkingDir();
            this.PRIORITIZE_BUILTIN_SHELL = config.PrioritizeBuiltInShell();

            //Console default settings
            Console.Title = "Rashell | ~v " + version + " | " + "~m " + machine + " | " + platform + " | " + sys_arch;

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();

            if (config.DisplayMSG()) //Display Rashell Welcome Message
            {
                DisplayWelcome(version);
            }

            if (string.IsNullOrEmpty(this.DEF_WORKING_DIR) || string.IsNullOrWhiteSpace(this.DEF_WORKING_DIR) || !Directory.Exists(this.DEF_WORKING_DIR))
            {
               ShellSessionDirectory = user_home;
            }
            else
            { ShellSessionDirectory = this.DEF_WORKING_DIR; }

            //apply default working directory
            Directory.SetCurrentDirectory(ShellSessionDirectory);
            UpdateShellWorkingDirectory(sessionUser, ShellSessionDirectory, true);

        }

        private void Listen()
        {
            string stdin = null;

        Start:
            stdin = null;
            stdin = Console.ReadLine();


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
                    format.ConsoleColorWrite(":>",ConsoleColor.Cyan, true);
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
                Console.WriteLine();
                format.ConsoleColorWrite(ShellWorkingDirectory, ConsoleColor.Cyan, true);
                goto Start;
            }
            else
            {
                format.ConsoleColorWrite(ShellWorkingDirectory, ConsoleColor.Cyan, true);
                goto Start;
            }
        }

        #endregion "Initialization"

        #region "Session Vars"

        public static string ShellSessionDirectory
        {
            get
            {
                return ShellWorkingDirectory;
            }
            set
            {
                ShellWorkingDirectory = value;
            }
        }

        private bool UpdateShellWorkingDirectory(string username, string directory, bool reset)
        {
            directory = directory.ToLower();

            if (directory == Environment.ExpandEnvironmentVariables("%userprofile%").ToLower())
            {
                ShellWorkingDirectory = username + "@" + "rashell:>";
            } else
            {
                ShellWorkingDirectory = username + "@" + "rashell:" + directory + ">";
            }

            if (reset)
            {
                format.ConsoleColorWrite(ShellSessionDirectory, ConsoleColor.Cyan, true);
            }

            return true;
        }

        public string setShellWorkingDirectory(string username, string directory)
        {
            directory = directory.ToLower();

            if (directory == Environment.ExpandEnvironmentVariables("%userprofile%").ToLower())
            {
                ShellWorkingDirectory = username + "@" + "rashell:>";
            }
            else
            {
                ShellWorkingDirectory = username + "@" + "rashell:" + directory + ">";
            }

            return ShellWorkingDirectory;
        }

        public string getSessionUser()
        {
            string sys_usr = Environment.UserName.ToString().ToLower();
            string sys_usr_short = null;
       

            if (sys_usr.IndexOf(" ") > 0)
            {
                sys_usr_short = sys_usr.Substring(0, sys_usr.IndexOf(" "));
                return sys_usr_short;
            }
            else {
                sys_usr_short = null;
            }

            return sys_usr;
        }

        private void DisplayWelcome(string version)
        {
            string msg = null;
            msg = "Rashell Dynamic Command Processor\n"
                  + "Version " + version + "\n"
                  + "(c) 2008-2017 J.C.P Laboratory, Under the GNU General Public License v3.0.\n";
            Console.WriteLine(msg);
        }

        #endregion "Session Vars"

        #region "Loaders"

        public bool Starter(string stdin)
        {
            Formatters format = new Formatters();
            Executor execute = new Executor();
            string cmd, cmdLoc;

            //Converting command to lowercase
            if (format.Break(stdin) != null)
            {
                cmd = format.Break(stdin).ToLower();
            } else
            {
                return false;
            }

            if (FindInternal(cmd) && this.PRIORITIZE_BUILTIN_SHELL)
            {
                execute.exec_in(cmd, format.getArguments());
                return true;
            }

            cmdLoc = Find(cmd);

            if (!string.IsNullOrEmpty(cmdLoc))
            {
                execute.Execute(cmdLoc, format.getArguments());
            }
            else
            {
                execute.exec_in(cmd, format.getArguments());
            }

            return true;
        }

        public string Find(string cmd)
        {
            string cmdLoc = null;
            List<string> AllPaths = new List<string>();
           
            AllPaths = this.EnvironmentPaths;
            AllPaths.Add(ShellWorkingDirectory);
            
            //check invalid chars in cmd
            string invalidChr = format.GetInvalidChars(cmd);

            if (invalidChr != null)
            {
                Console.WriteLine("Rashell: Wrong Syntax detected.");

                format.ConsoleColorWrite("Invalid Character(s): \"" + invalidChr + "\"", ConsoleColor.Red, false);
                return null;
            }
            //check ends

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
                        foreach (string path in AllPaths)
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

        public bool FindInternal(string cmd)
        {
            Dictionary<int, string> BuitInCommandDict = new Dictionary<int, string>();
            Command command = new Command();

            BuitInCommandDict = command.GetDictionary();

            if (BuitInCommandDict.ContainsValue(cmd))
            {
                return true;
            }
            return false;
        }

        public bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void restartAsAdmin()
        {
            if (IsAdministrator() == false)
            {
                try
                {
                    // Restart program and run as admin
                    var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    startInfo.Verb = "runas";
                    System.Diagnostics.Process.Start(startInfo);
                    Environment.Exit(0);
                    return;
                }
                catch (Exception e)
                {
                    if (e.ToString().Contains("The operation was canceled by the user"))
                    {
                        Console.WriteLine("Rashell: Operation unsuccessful.");
                        format.ConsoleColorWrite("User Denied Operation", ConsoleColor.Red, false);
                    } else
                    {
                        Console.WriteLine("Rashell: Operation unsuccessful.");
                        format.ConsoleColorWrite("Unknown Error", ConsoleColor.Yellow, false);
                    }
                }
            }
        }

        #endregion "Loaders"

        #region "Argument Handlers"

        private List<char> getSwitches(String[] args)
        {
            List<char> switches = new List<char>();

            foreach (string arg in args)
            {
                try
                {
                    string buffer = format.RemoveSpace(arg.ToLower());

                    if (buffer.StartsWith("-") || buffer.StartsWith("/"))
                    {
                        buffer = buffer.Substring(1);

                        if (!buffer.StartsWith("-")) //checks it is not an option which has two: (--).
                        {
                            foreach (char sw in buffer)
                            {
                                switches.Add(sw);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            return switches;
        }

        private List<string> getArguments(String[] args)
        {
            List<string> arguments = new List<string>();

            if (args.Length != 0)
            {
                foreach (string arg in args)
                {
                    try
                    {
                        string buffer = format.RemoveSpace(arg.ToLower());

                        if (!buffer.StartsWith("-") && !buffer.StartsWith("/"))
                        {
                            if (buffer.Contains(" "))
                            {
                                arguments.Add("\"" + buffer.ToString() + "\"");
                            } else
                            {
                                arguments.Add(buffer.ToString());
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return arguments;
        }

        public List<String> getOptions(String[] args)
        {
            List<string> options = new List<string>();

            foreach (string arg in args)
            {
                try
                {
                    string option = format.RemoveSpace(arg.ToLower());

                    if (option.StartsWith("--"))
                    {
                        option = option.Substring(2);
                        options.Add(option);
                    }
                }
                catch (Exception e)
                {

                }
            }

            return options;
        }

        private void ExecArgument()
        {

            if(RashellArguments.Length == 0) //exit if no argument is parsed to Rashell on startup
            {
                return;
            }

            List<char> switches = new List<char>();
            List<string> arguments = new List<string>();

            switches = this.getSwitches(RashellArguments);
            arguments = this.getArguments(RashellArguments);

            if(arguments.Count == 0) //exit if no argument is found
            {
                return;
            }

            //swithes options
            bool StayAwake = false;
            bool exec = false;
            bool Verbose = false;

            foreach (char sw in switches)
            {
                string swt = sw.ToString().ToLower();

                if (swt == "e")
                {
                    StayAwake = true;
                }
                else if (swt == "c")
                {
                    exec = true;
                } else if (swt == "v")
                {
                    Verbose = true;
                }
            }

            //argument execution
            if (exec)
            {
                string args = null;
                foreach (string arg in arguments)
                {
                    if (args == null)
                    {
                        args = arg;
                    }
                    else
                    {
                        args += " " + arg;
                    }
                }

                if (Verbose)
                {
                    Console.WriteLine(args);
                } else
                {
                    Console.WriteLine("");
                }

                
                Starter(args);

                if (StayAwake == false) { Environment.Exit(0); }

                UpdateShellWorkingDirectory(getSessionUser(), Directory.GetCurrentDirectory(), true);

            }

        }

        #endregion "Argument Handlers"

        private static void Main(string[] args)
        {
            //Call init functions
            Rashell shell = new Rashell();
            //assign shell arguments
            Rashell.RashellArguments = args;

            shell.Init();
            shell.ExecArgument();
            shell.Listen();

        }
    }
}