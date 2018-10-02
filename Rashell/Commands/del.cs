using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rashell.Commands
{
    /// <summary>
    /// Del command : deleting a file
    /// </summary>
    class del : Command
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
                    return DeleteFiles(arguments);
                }
                else
                {
                    Console.WriteLine("The syntax of the command is incorrect. No argument or file provided \n");
                    return false;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine("Error : " + E.Message + "\n");
                return false;
            }
        }

        /// <summary>
        /// writes help about the del command
        /// </summary>
        private static void Help()
        {
            Console.WriteLine("Deletes one or more files");
            Console.WriteLine("");
            //Console.WriteLine("DEL [/P] [/F] [/S] [/Q] [/A[[:]attributes]] names");
            Console.WriteLine("DEL [/P] [/Q] names");
            Console.WriteLine("");
            Console.WriteLine("  names         Specifies a list of one or more files or directories.");
            Console.WriteLine("                Wildcards may be used to delete multiple files. If a");
            Console.WriteLine("                directory is specified, all files within the directory");
            Console.WriteLine("                will be deleted.");
            Console.WriteLine("");
            Console.WriteLine("  /P            Prompts for confirmation before deleting each file.");

            // TODO : implement /F if needed
            //Console.WriteLine("  /F            Force deleting of read - only files.");

            // TODO : implement /S
            //Console.WriteLine("  /S            Delete specified files from all subdirectories.");

            Console.WriteLine("  /Q            Quiet mode, do not ask if ok to delete on global wildcard");

            // TODO : implement /A
            /*Console.WriteLine("  /A            Selects files to delete based on attributes");
            Console.WriteLine("  attributes    R Read-only files S  System files");
            Console.WriteLine("                H Hidden files A  Files ready for archiving");
            Console.WriteLine("                I  Not content indexed Files  L  Reparse Points");
            Console.WriteLine("- Prefix meaning not");*/

        }

        /// <summary>
        /// main routine for del command
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private bool DeleteFiles(List<string> arguments)
        {
            if (arguments.Any(p => string.Equals(p, "/?", StringComparison.InvariantCultureIgnoreCase)))
            {
                Help();
            }
            else
            {
                // define args
                bool needPrompt = arguments.Any(p => string.Equals(p, "/P", StringComparison.InvariantCultureIgnoreCase));

                bool isQuiet = arguments.Any(p => string.Equals(p, "/Q", StringComparison.InvariantCultureIgnoreCase));

                // TODO : implement /F if needed
                //bool forceDelete = arguments.Any(p => string.Equals(p, "/F", StringComparison.InvariantCultureIgnoreCase));

                // TODO : implement /S
                //bool deleteSub = arguments.Any(p => string.Equals(p, "/S", StringComparison.InvariantCultureIgnoreCase));

                // TODO : implement /A
                //bool selectAttribute = arguments.Any(p => string.Equals(p, "/A", StringComparison.InvariantCultureIgnoreCase));


                // excluding the parameters
                IEnumerable<string> paths = arguments.Where(path =>
                        !string.Equals(path, "/Q", StringComparison.InvariantCultureIgnoreCase) &&
                        !string.Equals(path, "/P", StringComparison.InvariantCultureIgnoreCase) &&
                        !string.Equals(path, "/?", StringComparison.InvariantCultureIgnoreCase)
                        );

                if (paths.Count() > 0)
                {
                    foreach (string p in paths)
                    {
                        if (!string.IsNullOrEmpty(p))
                        {
                            bool canDelete = true;
                            if (needPrompt)
                            {
                                // prompting before deletion
                                Console.WriteLine("Deleting " + p + ". Are you sure ? (y/n)");
                                string answer = Console.ReadLine();
                                if (!string.Equals(answer, "y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    canDelete = false;
                                }
                            }
                            if (canDelete)
                            {
                                if (p.Contains("*") || p.Contains("?"))
                                {
                                    // wildcards
                                    foreach (var file in Directory.EnumerateFiles(Path.GetDirectoryName(p), Path.GetFileName(p)))
                                    {
                                        File.Delete(file);
                                        if (!isQuiet)
                                        {
                                            // no need to write it again if there was already a prompt
                                            Console.WriteLine("Deleting " + file);
                                        }
                                    }
                                }
                                else
                                {
                                    // standard file
                                    File.Delete(p);
                                    if (!isQuiet && !needPrompt)
                                    {
                                        // no need to write it again if there was already a prompt
                                        Console.WriteLine("Deleting " + p);
                                    }
                                }

                            }
                        }

                    }
                }
                else
                {
                    Console.WriteLine("The syntax of the command is incorrect. No folder provided \n");
                    return false;
                }
            }
            return true;
        }
    }
}
