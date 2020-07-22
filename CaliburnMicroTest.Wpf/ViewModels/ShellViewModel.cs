using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CaliburnMicroTest.Wpf.Models;

namespace CaliburnMicroTest.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<IBaseViewModel>.Collection.OneActive
    {
        private string _inputField;
        private readonly FirstViewModel _firstViewModel;
        private readonly SecondViewModel _secondViewModel;

        public ShellViewModel(FirstViewModel firstViewModel, 
            SecondViewModel secondViewModel)
        {
            Title = "Caliburn Test App";
            
            this._firstViewModel = firstViewModel;
            _firstViewModel.OnStatusMessage = SetStatus;
            this._secondViewModel = secondViewModel;

            //default item
            ActivateItem(_firstViewModel);
        }


        public string Title { get; }
        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();
        private void SetStatus(string status)
        {
            Messages.Add(status);
            NotifyOfPropertyChange(nameof(Messages));
        }
     
        public string InputField
        {
            get => _inputField;
            set { _inputField = value; NotifyOfPropertyChange(nameof(InputField)); }
        }

        public bool CanValidateButton(string inputField)
        {
            return !string.IsNullOrWhiteSpace(inputField);
        }

        public void ValidateButton(string inputField)
        {
            Console.WriteLine("Validation Call");
        }


        public void FirstViewButton()
        {
            Console.WriteLine("1st");
            _firstViewModel.Title = "First view is active";
            ActivateItem(_firstViewModel);

        }
        public void SecondViewButton()
        {
            Console.WriteLine("2nd");
            _firstViewModel.Title = "Second view is active";
            ActivateItem(_secondViewModel);
        }


    }
}
