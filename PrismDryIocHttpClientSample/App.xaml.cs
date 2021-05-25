using System;
using System.Windows;
using DryIoc.Microsoft.DependencyInjection.Extension;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using PrismDryIocHttpClientSample.Views;

namespace PrismDryIocHttpClientSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().RegisterServices(services =>
            {
                services.AddHttpClient("example", client =>
                 {
                     client.BaseAddress = new Uri("https://example.com");
                 });
                services.AddHttpClient("yahoo", client =>
                {
                    client.BaseAddress = new Uri("https://yahoo.co.jp");
                });
                services.AddSingleton<Models.ExampleRequestService>();
            });
        }
    }
}
