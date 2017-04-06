using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rashell
{
    internal class Commands
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

        public bool ls(List<string> arguments)
        {
            Formatters format = new Formatters();
            bool ShowHidden = false;
            string DirectoryLocation = null;

            int countHiddenDirs = 0;
     

            //argument handlers
            if (arguments.Count != 0)
            {
                foreach (string arg in arguments)
                {
                    try
                    {
                        string buffer = format.RemoveSpace(arg.ToLower());

                        if (buffer.StartsWith("-") || buffer.StartsWith("/"))
                        {
                            if (buffer.Substring(1) == "about")
                            {
                                Console.WriteLine("\"" + "ls" + "\"" + " is developed by Cédric Poottaren\n" + "(c) 2017");
                                return true;
                            } else if (buffer.Substring(1) == "help" || buffer.Substring(1) == "?")
                            {
                                Console.WriteLine("Hello.\n" +
                                    "Thanks for using ls." +
                                    "\n" +
                                    "Use the (-h) switch to reveal hidden files." +
                                    "\nls use different colors to identify the different types of files.\n" +
                                    "\n" +
                                    "Hidden Directories: Cyan Blue\n" +
                                    "Other Directories: Green\n" +
                                    "Hidden Files: Yellow\n" +
                                    "Other Files: Grey\n");
                                return true;
                            }
                            else
                            {
                                foreach (char c in buffer)
                                {
                                    if (c.ToString() == "h")
                                    {
                                        ShowHidden = true;
                                    } else if (c.ToString() != "-" && c.ToString() != "/") { 
                                        Console.WriteLine("Invalid Switch \"" + c.ToString() + "\"");
                                        return false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            DirectoryLocation = buffer;
                            break;
                        }
                    }
                        catch (Exception e)
                    {

                    }
                }
                //end of arguments handlers

                //ls operations
                if (!string.IsNullOrEmpty(DirectoryLocation))
                {
                    if (Directory.Exists(DirectoryLocation))
                    {
                        //List Directies and gather information about each
                        DirectoryInfo dirInfo = new DirectoryInfo(DirectoryLocation);
                        DirectoryInfo[] dirs = dirInfo.GetDirectories();

                        foreach (DirectoryInfo dir in dirs)
                        {
                            string attr = dir.Attributes.ToString();

                            if (attr.Contains("Hidden"))
                            {
                                if (ShowHidden)
                                {
                                    format.ConsoleColorWrite(dir.Name, ConsoleColor.Cyan);
                                    countHiddenDirs++;
                                }

                            } else if (!attr.Contains("Hidden"))
                            {
                                format.ConsoleColorWrite(dir.Name, ConsoleColor.Green);
                            }
                        }
                        
                        //Get files form specified dir
                        FileInfo[] files = dirInfo.GetFiles();

                        foreach (FileInfo file in files)
                        {
                            string attr = file.Attributes.ToString();

                            if (attr.Contains("Hidden"))
                            {
                                if (ShowHidden)
                                {
                                   format.ConsoleColorWrite(file.Name, ConsoleColor.Yellow);
                                   countHiddenDirs++;
                                }

                            }
                            else if (!attr.Contains("Hidden"))
                            {
                                Console.WriteLine(file.Name);
                            }
                        }

                        //Display Count
                     
                            Console.WriteLine("\nNumber of Directories: " + dirs.Count<DirectoryInfo>() + "\nNumber of Files: " + files.Count<FileInfo>());                 
                    }
                    else
                    {
                        Console.WriteLine("Either \"" + DirectoryLocation + "\" is not a valid directory or it doesn't exists");
                    }
                } else {
                    DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                    DirectoryInfo[] dirs = dirInfo.GetDirectories();

                    foreach (DirectoryInfo dir in dirs)
                    {
                        string attr = dir.Attributes.ToString();

                        if (attr.Contains("Hidden"))
                        {
                            if (ShowHidden)
                            {
                                format.ConsoleColorWrite(dir.Name, ConsoleColor.Cyan);
                                countHiddenDirs++;
                            }

                        }
                        else if (!attr.Contains("Hidden"))
                        {
                            format.ConsoleColorWrite(dir.Name, ConsoleColor.Green);
                        }
                    }

                    FileInfo[] files = dirInfo.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        string attr = file.Attributes.ToString();

                        if (attr.Contains("Hidden"))
                        {
                            if (ShowHidden)
                            {
                                format.ConsoleColorWrite(file.Name, ConsoleColor.Yellow);
                                countHiddenDirs++;
                            }

                        }
                        else if (!attr.Contains("Hidden"))
                        {
                            Console.WriteLine(file.Name);
                        }
                    }

                    //Display Count

                        Console.WriteLine("\nNumber of Directories: " + dirs.Count<DirectoryInfo>() + "\nNumber of Files: " + files.Count<FileInfo>());
                }
            } else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                DirectoryInfo[] dirs = dirInfo.GetDirectories();

                foreach (DirectoryInfo dir in dirs)
                {
                    string attr = dir.Attributes.ToString();

                    if (attr.Contains("Hidden"))
                    {
                        if (ShowHidden)
                        {
                            format.ConsoleColorWrite(dir.Name, ConsoleColor.Cyan);
                            countHiddenDirs++;
                        }

                    }
                    else if (!attr.Contains("Hidden"))
                    {
                        format.ConsoleColorWrite(dir.Name, ConsoleColor.Green);
                    }
                }

                FileInfo[] files = dirInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    string attr = file.Attributes.ToString();

                    if (attr.Contains("Hidden"))
                    {
                        if (ShowHidden)
                        {
                            format.ConsoleColorWrite(file.Name, ConsoleColor.Yellow);
                            countHiddenDirs++;
                        }

                    }
                    else if (!attr.Contains("Hidden"))
                    {
                        Console.WriteLine(file.Name);
                    }
                }

                //Display Count

                    Console.WriteLine("\nNumber of Directories: " + dirs.Count<DirectoryInfo>() + "\nNumber of Files: " + files.Count<FileInfo>());

            }
            return true;
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

        public bool Mkdir(List<string> dir_paths)
        {
            try
            {
                if (dir_paths.Count != 0)
                {
                    foreach (string p in dir_paths)
                    {
                        if (Directory.Exists(p))
                        {
                            Console.WriteLine("A subdirectory or file " + p + " already exists. \n");
                        }
                        else if (p == "/about")
                        {
                            Console.WriteLine("\"" + "mkdir" + "\"" + " is developed by Arwin Neil Baichoo \n" + "(c) 2017 \n");
                        }
                        else
                        {
                            Directory.CreateDirectory(p);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The syntax of the command is incorrect. \n");
                    return false;
                }
            }
            catch (Exception E)
            {
                return false;
            }
            return true;
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
    }
}