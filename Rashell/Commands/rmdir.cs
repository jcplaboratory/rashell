using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rashell.Commands
{
    /// <summary>
    /// rmdir command : removing a folder and everything inside it if the user wants it
    /// </summary>
    class rmdir : Command
    {
        /// <summary>
        /// main rmdir algorithm
        /// </summary>
        /// <param name="arguments">arguments provided</param>
        /// <returns></returns>
        public bool main(List<string> arguments)
        {
            try
            {
                if (arguments.Count != 0)
                {
                    return RemoveDirs(arguments);
                }
                else
                {
                    Console.WriteLine("The syntax of the command is incorrect. Missing operand.");
                    return false;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine("Error : " + E.Message);
                return false;
            }
        }

        /// <summary>
        /// writes help about the rmdir command
        /// </summary>
        private static void Help()
        {
            Console.WriteLine("Removes a directory \n");
            Console.WriteLine("rmdir [/S] [/Q] path(s)");
            Console.WriteLine("");
            Console.WriteLine("/S : Deletes the directory and its content recursively");
            Console.WriteLine("/Q : Silent mode");
        }

        /// <summary>
        /// main routine for directory removal
        /// </summary>
        /// <param name="arguments">arguments provided</param>
        /// <returns>did the rmdir routine go well ?</returns>
        private static bool RemoveDirs(List<string> arguments)
        {
            if (arguments.Any(p => string.Equals(p, "/?", StringComparison.InvariantCultureIgnoreCase)) || arguments.Any(p => string.Equals(p, "-?", StringComparison.InvariantCultureIgnoreCase)))
            {
                Help();
            }
            else
            {
                bool isSilent = arguments.Any(p => string.Equals(p, "/Q", StringComparison.InvariantCultureIgnoreCase));
                bool deleteEverything = arguments.Any(p => string.Equals(p, "/S", StringComparison.InvariantCultureIgnoreCase));


                // the arguments are excluded from the main routine
                IEnumerable<string> paths = arguments.Where(path =>
                    !string.Equals(path, "/Q", StringComparison.InvariantCultureIgnoreCase) &&
                    !string.Equals(path, "/S", StringComparison.InvariantCultureIgnoreCase) &&
                    !string.Equals(path, "/?", StringComparison.InvariantCultureIgnoreCase)
                    );

                if (paths.Count() > 0)
                {
                    foreach (string p in paths)
                    {
                        Directory.Delete(p, deleteEverything);
                        if (!isSilent)
                        {
                            Console.WriteLine("Deleting directory " + "\"" + p + "\"" + (deleteEverything ? " recursively" : ""));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No directory specified.");
                    return false;
                }
            }
            return true;
        }
    }
}
