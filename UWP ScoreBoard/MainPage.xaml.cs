using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void stopwatch_Tapped(object sender, TappedRoutedEventArgs e)
        {

            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
            }
            else
            {
                _stopwatch.Start();

                _timer = new Timer(updateTime, null, 1500, Timeout.Infinite);
                
                //stopwatch.Text = _stopwatch.Elapsed.ToString();

            }

        }

        private async void updateTime(object state)
        {
            // do some work not connected with UI
            stopwatch.Text = _stopwatch.Elapsed.ToString();

            //await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            //    () => {
            //        stopwatch.Text = _stopwatch.Elapsed.ToString();
            //});
        }
    }
}
