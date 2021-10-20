using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace CtrlManager.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly DashboardViewModel _dashboardView;
        private readonly ConfigViewModel _configView;

        public ShellViewModel(DashboardViewModel dashboardView,
            ConfigViewModel configView)
        {
            this._dashboardView = dashboardView;
            this._configView = configView;

            DetailView = this._dashboardView;
        }

        private Screen _detailView;
        public Screen DetailView
        {
            get { return _detailView; }
            private set
            {
                _detailView = value;
                NotifyOfPropertyChange(nameof(DetailView));
            }
        }

        public void Dashboard()
        {
            this.DetailView = _dashboardView;
        }


        public void Config()
        {
            this.DetailView = _configView;
        }

    }
}
