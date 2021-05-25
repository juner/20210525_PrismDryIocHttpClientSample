using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using PrismDryIocHttpClientSample.Models;

namespace PrismDryIocHttpClientSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        readonly ExampleRequestService ExampleRequestService;
        readonly YahooRequestService YahooRequestService;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        string requestResultText = string.Empty;
        public string RequestResultText { get => requestResultText; set => SetProperty(ref requestResultText, value); }

        public MainWindowViewModel(ExampleRequestService ExampleRequestService, YahooRequestService YahooRequestService)
        {
            this.ExampleRequestService = ExampleRequestService;
            this.YahooRequestService = YahooRequestService;
            RequestCommand = new DelegateCommand(OnRequest, CanRequest);
            YahooCommand = new DelegateCommand(OnYahooReqeust, CanYahooRequest);
        }
        void RefreshButton()
        {
            RequestCommand.RaiseCanExecuteChanged();
            YahooCommand.RaiseCanExecuteChanged();
        }
        public DelegateCommand RequestCommand { get; }
        public DelegateCommand YahooCommand { get; }
        Task RequestTask = Task.CompletedTask;
        bool CanRequest() => RequestTask.IsCompleted;
        void OnRequest()
        {
            if (!RequestTask.IsCompleted)
                return;
            RequestTask = OnRequest();
            RefreshButton();
            async Task OnRequest()
            {
                RequestResultText = string.Empty;
                try
                {
                    RequestResultText = await ExampleRequestService.GetAsync();
                }
                catch (Exception e)
                {
                    RequestResultText = $"Error: {e.Message}";
                }
                finally
                {
                    RefreshButton();
                }
            }
        }
        bool CanYahooRequest() => RequestTask.IsCompleted;
        void OnYahooReqeust()
        {
            if (!RequestTask.IsCompleted)
                return;
            RequestTask = OnYahooReqeust();
            RefreshButton();
            async Task OnYahooReqeust()
            {
                RequestResultText = string.Empty;
                try
                {
                    RequestResultText = await YahooRequestService.GetAsync();
                }
                catch (Exception e)
                {
                    RequestResultText = $"Error: {e.Message}";
                }
                finally
                {
                    RefreshButton();
                }
            }
        }
    }
}
