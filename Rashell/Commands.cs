using System;
using System.Collections.Generic;
using System.IO;

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
    }
}