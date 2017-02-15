using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;


namespace Rashell
{
    class Executor
    {
        protected Commands command = new Commands();
        protected List<string> arguments = new List<string>();

        #region "Executors"
        public void Starter(string stdin)
        {
  
            string cmd = Break(stdin);
            string cmdLoc = Find(cmd);

            if (!string.IsNullOrEmpty(cmdLoc))
            {
                Execute(cmd);
            }
            else { exec_in(Break(stdin)); }

        }

        private void exec_in(string command)
        {
            switch (command)
            {
                case "clear":
                    this.command.Clear();
                    break;
                case "bye":
                    this.command.Exit();
                    break;
                case "quit":
                    this.command.Exit();
                    break;
                case "exit":
                    this.command.Exit();
                    break;
                default:
                    Console.WriteLine("Command or Operator " + "\"" + command + "\"" + " not found." + "\n Check syntax.");
                    break;

            }
        }

        private bool Execute(string cmd)
        {

            int argsCt = this.arguments.Count;
            string Arguments = null;

            foreach (string arg in this.arguments)
            {
                Arguments += " " + arg;
            }

            Process process = new Process();

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = cmd;
            process.StartInfo.Arguments = Arguments;

            

            process.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    Console.WriteLine(e.Data);
                }
            );
            process.ErrorDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    Console.WriteLine(e.Data);
                }
            );

            process.Start();
            process.BeginOutputReadLine();

            //while (!process.HasExited)
            //{

            //    string stdin = null;
            //    stdin = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(stdin))
            //    {
            //        process.StandardInput.WriteLine(stdin);
            //    }
            //}

            //foreach (ProcessThread thread in process.Threads)
            //{
            //    if (thread.ThreadState == ThreadState.Wait )
            //    {
            //        string stdin = null;
            //        stdin = Console.ReadLine();
            //        if (!string.IsNullOrEmpty(stdin))
            //        {
            //            process.StandardInput.WriteLine(stdin);
            //        }
            //    }

            //}


            process.WaitForExit();

            return true;
        }
        #endregion

        #region "Formatters"
        private string Break(string stdin)
        {
            
           //Count spaces
            int spaces = 0;
            foreach (char c in stdin)
            {
                if (c.ToString() == " ")
                {
                    spaces++;
                }
            }
            //create array to store args
            string[] args = new string[spaces + 1];
            args.Initialize();

            //separate cmd and args
            int i = 0;
            bool foundDCom = false; //double inverted commas presence checker
            bool foundEnvi = false;
            string enviVar = null;

            foreach (char c in stdin)
            {
                if (c.ToString() == " ") //looks out for space
                {
                    if (!foundDCom) //move to the next in array if double comma wasn't previously found
                    {
                        i++; //increment array index
                    }
                    else //if comma was previous found continue adding characters to the current index
                    {
                        args[i] += " "; 
                    }
                } else if (c.ToString() == "%" && foundEnvi == false) //looks for '%'
                {
                    int count = 0;
                    foreach (char ch in stdin) //counts the number of '%' in stdin
                    {
                        if (ch.ToString().Equals("%"))
                        {
                            count++;
                        }   
                    }

                    double rm = count % 2; //number of '%' must be even

                    if (rm.Equals(0)) //if even number of '%' was found add chars to enviVar
                    {
                        foundEnvi = true;
                        enviVar = null;
                        enviVar += c;
                    }
                    else
                    {
                        args[i] += c; //if not even, continue adding to current index
                    }
                }
                else
                {
                    if (c.ToString().Equals("\"") && foundDCom == false) //if double inverted comma found set foundDcom = true
                    {
                        foundDCom = true;
                    } else if (c.ToString().Equals("\"") && foundDCom)
                    {
                        foundDCom = false; //if comma was previously found, means closing comma was found. Set foundDcom = false
                    } else if (!c.ToString().Equals("%") && foundEnvi)
                    {
                        enviVar += c; //increment enviVar while closing '%' is not found
                    } else if (c.ToString().Equals("%") && foundEnvi)
                    {
                        foundEnvi = false; //closing '%' found. set foundEnvi = false and expand Variable in enviVar
                        enviVar += c;
                        args[i] = Environment.ExpandEnvironmentVariables(enviVar);
                    }
                    else { args[i] += c; }
                    
                }
            }


            this.arguments.Clear();
            i = 0;
            foreach (string item in args)
            {
               if (!i.Equals(0))
                {
                    this.arguments.Add(item);
                }
                i++;
            }
            return args[0];
        }

        public string Find(string cmd)
        {
            //**********************************
            string[] envi_paths = new string[3];
            string[] known_ex = new string[3];
            string cmdLoc = null;
            envi_paths.Initialize();
            known_ex.Initialize();
            //**********************************

            envi_paths[0] = Environment.ExpandEnvironmentVariables("%WINDIR%");
            envi_paths[1] = Environment.ExpandEnvironmentVariables("%WINDIR%\\SYSTEM32");

            known_ex[0] = ".exe";
            known_ex[1] = ".bat";
            known_ex[2] = ".cmd";

            if (!File.Exists(cmd))
            {
                foreach (string ex in known_ex)
                {
                    cmdLoc = cmd + ex;
                    if (File.Exists(cmdLoc))
                    {
                        return cmdLoc;
                    }
                    else
                    {
                        foreach (string path in envi_paths)
                        {
                            if (!Path.HasExtension(cmd)) //Check is extension is already present in path
                            {
                                foreach (string x in known_ex)
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
        #endregion
        
    }
}

