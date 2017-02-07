using MultiWindowRepro.Service;

namespace MultiWindowRepro.ViewModel
{
    public class AdditionalViewModel
    {
        private NavigationService navigationService = null;
        private LayoutService layoutService = null;
        
        public AdditionalViewModel(NavigationService navService, LayoutService layService)
        {
            navigationService = navService;
            layoutService = layService;
        }

        public LayoutService LayoutService { get { return this.layoutService; } }
    }
}
