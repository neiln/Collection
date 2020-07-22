using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CaliburnMicroTest.Wpf.Models;

namespace CaliburnMicroTest.Wpf.ViewModels
{
    public interface IBaseViewModel
    {
        string Title { get; set; }
        Action<string> OnStatusMessage { get; set; }
    }

    public class Log : ILog
    {
        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }

    public class FirstViewModel : Screen, IBaseViewModel
    {
        public Action<string> OnStatusMessage { get; set; }
        private readonly IDataAccessProvider dataAccessProvider;
       
        public FirstViewModel(IDataAccessProvider dataAccessProvider, ILog log)
        {
            this.dataAccessProvider = dataAccessProvider;
            this.Title = "First View Title";
        }

        private string _title;
        public string Title { get=> _title; set {

                _title = value;
                if (OnStatusMessage != null)
                {
                    OnStatusMessage(_title);
                }
            } }

       
    }
}
