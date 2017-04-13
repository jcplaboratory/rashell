using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rashell.Commands
{
    class ls
    {
        public bool accept(List<string> arguments)
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
                            }
                            else if (buffer.Substring(1) == "help" || buffer.Substring(1) == "?")
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
                                    }
                                    else if (c.ToString() != "-" && c.ToString() != "/")
                                    {
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

                            }
                            else if (!attr.Contains("Hidden"))
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
                }
                else
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
            }
            else
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
    }
}
