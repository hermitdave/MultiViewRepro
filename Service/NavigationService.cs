using MultiWindowRepro.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MultiWindowRepro.Service
{
    public class NavigationService
    {
        public static Type Main { get; private set; }
        public static Type Additional { get; private set; }

        static NavigationService()
        {
            Main = typeof(MainView);
            Additional = typeof(AdditionalView);
        }

        public async Task OpenAdditionalViewInNewWindow()
        {
            var newCoreAppView = CoreApplication.CreateNewView();
            var appView = ApplicationView.GetForCurrentView();

            await newCoreAppView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, async () =>
            {
                var window = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

                var frame = new Frame();
                window.Content = frame;
                frame.Navigate(Additional);
                window.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMore, appView.Id, ViewSizePreference.Default);
            });
        }
    }
}
