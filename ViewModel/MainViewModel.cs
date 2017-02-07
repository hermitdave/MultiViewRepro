using GalaSoft.MvvmLight.Command;
using MultiWindowRepro.Service;

namespace MultiWindowRepro.ViewModel
{
    public class MainViewModel
    {
        private NavigationService navigationService = null;
        private LayoutService layoutService = null;
        private RelayCommand openAdditionalViewCommand = null;

        public MainViewModel(NavigationService navService, LayoutService layService)
        {
            navigationService = navService;
            layoutService = layService;
        }

        public RelayCommand OpenAddtionalViewCommand => openAdditionalViewCommand ?? (openAdditionalViewCommand = new RelayCommand(async () =>
        {
            await navigationService.OpenAdditionalViewInNewWindow();
        }));

        public LayoutService LayoutService { get { return this.layoutService; } }
    }
}
