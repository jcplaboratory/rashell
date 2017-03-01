using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

namespace Rashell
{
    class Configuration : Rashell
    {
        private string config_dir_path = AppDomain.CurrentDomain.BaseDirectory;
        private string config_path = AppDomain.CurrentDomain.BaseDirectory + "\\config.conf";
        private string DEF_WORKING_DIR;
        private List<string> EnvironmentPaths = new List<string>();
        private List<string> Known_Extensions = new List<string>();

        public Configuration()
        {
            this.config_dir_path = AppDomain.CurrentDomain.BaseDirectory;
            this.config_path = this.config_dir_path + "\\config.conf";
            this.DEF_WORKING_DIR = null;
        }
       

        public bool Read()
        {
            Formatters format = new Formatters();
            bool FoundEnvi = false;
            bool FoundKnown_Ex = false;
            string Working_Dir = null;
            
   Start:
            if (File.Exists(config_path))
            {

                StreamReader ConfigReader = new StreamReader(this.config_path);
                while (!ConfigReader.EndOfStream)
                {
                    string line = ConfigReader.ReadLine();
                    line = format.RemoveTab(line);
                    line = format.RemoveSpace(line);

                    if (!string.IsNullOrEmpty(line))
                    {
                        
                        if (line.StartsWith("ENVIRONMENT_PATHS {"))
                        {
                            FoundEnvi = true;
                        } else if (line.StartsWith("KNOWN_EXECUTABLES {"))
                        {
                            FoundKnown_Ex = true;
                        } else if (line.StartsWith("DEF_WORKING_DIR ="))
                        {
                           if (line.EndsWith(";"))
                            {
                                Working_Dir = line.Substring(line.IndexOf("=") + 1, (line.Length - (line.IndexOf("=") + 1)) - 1);

                                if (!string.IsNullOrEmpty(Working_Dir) && !string.IsNullOrWhiteSpace(Working_Dir))
                                {

                                    Working_Dir = format.Break(Working_Dir);
                                    if (Directory.Exists(Working_Dir))
                                    {
                                        this.DEF_WORKING_DIR = Working_Dir;
                                    }
                                }
                            } 
                           
                        }
                        else if (FoundEnvi)
                        {
                            string envipath = null;
                            if (line.EndsWith(";") && line.Length > 1)
                            {
                                envipath = line.Substring(0, line.Length - 1);
                                envipath = format.Break(envipath);
                                
                                if (Directory.Exists(envipath))
                                {
                                    this.EnvironmentPaths.Add(envipath);
                                }
                            }
                            else if (line == "}")
                            {
                                FoundEnvi = false;
                            }
                        } else if (FoundKnown_Ex)
                        {
                            string extension = null;
                            if (line.EndsWith(";") && line.Length > 1)
                            {
                                extension = line.Substring(0, line.Length - 1);
                                if (extension.StartsWith("."))
                                {
                                    this.Known_Extensions.Add(extension);
                                }
                                  
                            }
                            else if (line == "}")
                            {
                                FoundKnown_Ex = false;
                            }
                        } 
                    }
                }

                ConfigReader.Close();
            }
            else
            {
                Generate();
                goto Start;
            }

            return true;
        }

        public bool Generate()
        {
            string Default = "----------------------------------------------------------------------- \n"
                             + "This is Rashell Auto-Generated Configuration File. \n"
                             + "Use it to add further relations, references and to tweak settings. \n \n"
                             + "Restart is required after this file is modified. \n \n"
                             + "WARNING... This configuration file is version specific (for v0.2) \n"
                             + "You may need to migrate your chamges after you update Rashell \n"
                             + "----------------------------------------------------------------------- \n \n"
                             + "---Definition of Environment Paths \n \n"
                             + "ENVIRONMENT_PATHS {\n\t"
                             + "%SYSTEMROOT%;\n\t"
                             + "%SYSTEMROOT%\\SYSTEM32;\n" 
                             + "}";

            if (!File.Exists(this.config_path))
            {
                try
                {
                    StreamWriter write = new StreamWriter(config_path);
                    write.Write(Default);
                    write.Close();
                }
                catch (IOException e)
                {

                }
            }
            else
            {
                try
                {
                    Console.Write(" Configuration file aleady exists.\n If you continue, the file will be overwritten with default values.\n \n Do you want to continue anyway? ");
                    string readUser = Console.ReadLine().ToLower();
                    if (readUser == "yes" || readUser == "y")
                    {
                        File.Delete(config_path);
                        StreamWriter write = new StreamWriter(config_path);
                        write.Write(Default);
                        write.Close();
                    }
                    
                }
                catch (IOException e)
                {
                    
                }
            }

            return true;
        }

        public List<string> GetEnvironmentPaths()
        {
            return this.EnvironmentPaths;
        }

        public List<string> GetKnownExtensions()
        {
            return this.Known_Extensions;
        }

        public string GetWorkingDir()
        {
            return this.DEF_WORKING_DIR;
        }
    }
  
}
