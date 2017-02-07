using MultiWindowRepro.Service;
using MultiWindowRepro.ViewModel;
using System.Threading.Tasks;

namespace MultiWindowRepro.Infrastructure
{
    public class Locator
    {
        private static NavigationService navigationService = null;
        private static LayoutService layoutService = null;

        public static NavigationService NavigationServiceInstance => navigationService ?? (navigationService = new NavigationService());
        public static LayoutService LayoutServiceInstance => layoutService ?? (layoutService = new LayoutService());

        public static Task Initialise()
        {
            return LayoutServiceInstance.Initialise();
        }

        public MainViewModel MainViewModel
        {
            get { return new MainViewModel(NavigationServiceInstance, LayoutServiceInstance); }
        }

        public AdditionalViewModel AdditionalViewModel
        {
            get { return new AdditionalViewModel(NavigationServiceInstance, LayoutServiceInstance); }
        }
    }
}
