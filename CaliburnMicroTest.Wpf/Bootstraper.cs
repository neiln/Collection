using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using CaliburnMicroTest.Wpf.Models;
using CaliburnMicroTest.Wpf.ViewModels;

namespace CaliburnMicroTest.Wpf
{
    public class Bootstrapper:AutofacBootstrapper<ShellViewModel>
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
            builder.RegisterType<Log>().As<ILog>();
            builder.RegisterType<FirstViewModel>().As<IBaseViewModel>();
            builder.RegisterType<SecondViewModel>().As<IBaseViewModel>();
            builder.RegisterType<DataAccessProvider>().As<IDataAccessProvider>().SingleInstance();
            builder.RegisterType<ShellViewModel>();
        }
    }
}
