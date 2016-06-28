using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_ScoreBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Stopwatch _stopwatch = new Stopwatch();
        Timer _timer;

        string _mask = "00 00 00";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void stopwatch_Tapped(object sender, TappedRoutedEventArgs e)
        {

            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _timer.Dispose();
            }
            else
            {
                _stopwatch.Start();
                _timer = new Timer(updateTime, null, 0, 1);
            }

        }

        private async void updateTime(object state)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                    {
                        _mask = String.Format("{0:00} {1:00} {2:00}", _stopwatch.Elapsed.Minutes, _stopwatch.Elapsed.Seconds, _stopwatch.Elapsed.Milliseconds / 10);// + " " + _stopwatch.Elapsed.ToString()
                        stopwatchLbl.Text = _mask;
                    }
                );
       
        }

    }
}
