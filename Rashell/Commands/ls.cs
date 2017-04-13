using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Rashell.Commands
{
    class ls
    {
        private Formatters format = new Formatters();

        public bool main(List<string> arguments)
        {
            bool ShowHidden = false;
            string DirectoryLocation = null;

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
                        int NHiddenDirs = GetDirectory(ShowHidden, DirectoryLocation);

                        //Get files form specified dir
                        int NHiddenFiles = GetFiles(ShowHidden, DirectoryLocation);

                        //Display Count
                        if (ShowHidden)
                        {
                            Console.WriteLine("\nNumber of Directories: " + CountDirs(DirectoryLocation) + "\nNumber of Files: " + CountFiles(DirectoryLocation));
                        } else
                        {
                            Console.WriteLine("\nNumber of Directories: " + (CountDirs(DirectoryLocation) - NHiddenDirs) + "\nNumber of Files: " + (CountFiles(DirectoryLocation) - NHiddenFiles));
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Either \"" + DirectoryLocation + "\" is not a valid directory or it doesn't exists");
                    }
                }
                else
                {
                    //List Directies and gather information about each
                    int nHiddenDirs = GetDirectory(ShowHidden, Directory.GetCurrentDirectory().ToString());

                    //Get files form specified dir
                    int nHiddenFiles = GetFiles(ShowHidden, Directory.GetCurrentDirectory().ToString());

                    //Display Count

                    if (ShowHidden)
                    {
                        Console.WriteLine("\nNumber of Directories: " + CountDirs(Directory.GetCurrentDirectory()) + "\nNumber of Files: " + CountFiles(Directory.GetCurrentDirectory()));
                    }
                    else
                    {
                        Console.WriteLine("\nNumber of Directories: " + (CountDirs(Directory.GetCurrentDirectory()) - nHiddenDirs) + "\nNumber of Files: " + (CountFiles(Directory.GetCurrentDirectory()) - nHiddenFiles));
                    }
                }
            }
            else
            {
                //List Directies and gather information about each
                int nHiddenDirs = GetDirectory(ShowHidden, Directory.GetCurrentDirectory().ToString());

                //Get files form specified dir
                int nHiddenFiles = GetFiles(ShowHidden, Directory.GetCurrentDirectory().ToString());

                //Display Count
                if (ShowHidden)
                {
                    Console.WriteLine("\nNumber of Directories: " + CountDirs(Directory.GetCurrentDirectory()) + "\nNumber of Files: " + CountFiles(Directory.GetCurrentDirectory()));
                } else
                {
                    Console.WriteLine("\nNumber of Directories: " + (CountDirs(Directory.GetCurrentDirectory()) - nHiddenDirs) + "\nNumber of Files: " + (CountFiles(Directory.GetCurrentDirectory()) - nHiddenFiles));
                }
  
            }
            return true;
        }

        private int GetFiles(bool ShowHidden, string DirectoryLocation)
        {
            //List Directies and gather information about each
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryLocation);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            int countHidden = 0;

            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                string attr = file.Attributes.ToString();

                if (attr.Contains("Hidden"))
                {
                    if (ShowHidden)
                    {
                        format.ConsoleColorWrite(file.Name, ConsoleColor.Yellow);
                    }
                    countHidden++;

                }
                else if (!attr.Contains("Hidden"))
                {
                    Console.WriteLine(file.Name);
                }
            }
            return countHidden;
        }

        private int GetDirectory(bool ShowHidden, string DirectoryLocation)
        {
            //List Directies and gather information about each
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryLocation);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            int countHiddenDirs = 0;

            foreach (DirectoryInfo dir in dirs)
            {
                string attr = dir.Attributes.ToString();

                if (attr.Contains("Hidden"))
                {
                    if (ShowHidden)
                    {
                        format.ConsoleColorWrite(dir.Name, ConsoleColor.Cyan);
                    }
                    countHiddenDirs++;

                }
                else if (!attr.Contains("Hidden"))
                {
                    format.ConsoleColorWrite(dir.Name, ConsoleColor.Green);
                }
            }
            return countHiddenDirs;
        }

        private int CountDirs(string DirectoryLocation)
        {
            //List Directies and gather information about each
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryLocation);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            return dirs.Count<DirectoryInfo>();
        }

        private int CountFiles(string DirectoryLocation)
        {
            //List Directies and gather information about each
            DirectoryInfo dirInfo = new DirectoryInfo(DirectoryLocation);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            FileInfo[] files = dirInfo.GetFiles();

            return files.Count<FileInfo>();
        }
    }

 
}

