using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace CaliburnMicroTest.Wpf.ViewModels
{
    public class SecondViewModel : Screen, IBaseViewModel
    {
        public Action<string> OnStatusMessage { get; set; }
        private string _title;
        public string Title
        {
            get => _title; set
            {

                _title = value;
                if (OnStatusMessage != null)
                {
                    OnStatusMessage(_title);
                }
            }
        }

    }
}
