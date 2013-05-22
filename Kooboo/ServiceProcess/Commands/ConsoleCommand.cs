﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.ServiceProcess
{
    public class ConsoleCommand : ICommand
    {
        public ApplicationStarter Starter { get; set; }

        public bool Execute(string[] args)
        {
            try
            {
                if (!Environment.UserInteractive)
                    return false;

                Starter.OnStart();
                while (true)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
            finally
            {
                Starter.OnStop();
            }
            return true;
        }
    }
}
