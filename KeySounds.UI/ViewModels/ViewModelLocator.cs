using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace KeySounds.UI.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<TrayIconViewModel>();
        }

        public TrayIconViewModel TrayIcon { get { return ServiceLocator.Current.GetInstance<TrayIconViewModel>(); } }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}