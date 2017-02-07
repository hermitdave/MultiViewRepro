using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace MultiWindowRepro.Service
{
    public class LayoutService : ReactiveObject
    {
        private double width;
        private double height;
        private IDisposable sizeChangedDisposable = null;

        public double Width
        {
            get { return this.width; }
            set { this.RaiseAndSetIfChanged(ref this.width, value); }
        }

        public double Height
        {
            get { return this.height; }
            set { this.RaiseAndSetIfChanged(ref this.height, value); }
        }

        public async Task Initialise()
        {
            var mainWindow = Window.Current;
            var dispatcher = mainWindow.Dispatcher;

            sizeChangedDisposable = Observable.FromEventPattern(mainWindow, "SizeChanged")
                .Throttle(TimeSpan.FromSeconds(0.35))
                .ObserveOn(RxApp.MainThreadScheduler)
                .SubscribeOn(TaskPoolScheduler.Default)
                .Subscribe(x =>
                {
                    try
                    {
                        GetApplicationBounds();
                    }
                    catch (TargetInvocationException)
                    {
                        sizeChangedDisposable?.Dispose();
                    }
                    catch (InvalidCastException)
                    {
                    }
                });

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => GetApplicationBounds());
        }

        private void GetApplicationBounds()
        {
            var appView = ApplicationView.GetForCurrentView();

            this.Height = appView.VisibleBounds.Height;
            this.Width = appView.VisibleBounds.Width;
        }
    }
}
