﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Kooboo.ServiceProcess
{
    public class InstallCommand : ICommand
    {
        public ApplicationStarter Starter { get; set; }

        public bool Execute(string[] args)
        {
            if (!args.Contains("-i"))
                return false;


            var installArgs = new ServiceInstallEventArgs();
            Starter.OnServiceInstalling(installArgs);

            InstallService(installArgs);
            StartService(installArgs);

            return true;
        }

        public void InstallService(ServiceInstallEventArgs args)
        {
            Console.WriteLine("Installing service......");

            using (var installer = Starter.CreateInstaller(args))
            {
                IDictionary state = new Hashtable();
                try
                {
                    // Install service
                    installer.Install(state);
                    installer.Commit(state);
                    Console.WriteLine("Service installed.");
                }
                catch
                {
                    installer.Rollback(state);
                    throw;
                }
            }
        }

        protected void StartService(ServiceInstallEventArgs args)
        {
            using (var sc = new ServiceController(args.Service.Name))
            {
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                Console.WriteLine("Service started.");
            }
        }
    }


}
