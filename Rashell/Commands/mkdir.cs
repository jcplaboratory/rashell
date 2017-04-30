using System;
using System.Collections.Generic;
using System.IO;

namespace Rashell.Commands
{
    class mkdir : Command
    {
        public bool main(List<string> dir_paths)
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
