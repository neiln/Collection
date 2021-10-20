using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CtrlManager.ViewModels;
using Autofac;
using Caliburn.Micro.Autofac;

namespace CtrlManager
{
    public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            //Autofac registration
            builder.RegisterType<ShellViewModel>();
        }
    }
}
