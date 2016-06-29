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

        Windows.UI.Input.GestureRecognizer gr = new Windows.UI.Input.GestureRecognizer();

        public MainPage()
        {
            this.InitializeComponent();

            this.PointerPressed += MainPage_PointerPressed;
            this.PointerMoved += MainPage_PointerMoved;
            this.PointerReleased += MainPage_PointerReleased;
            gr.CrossSliding += gr_CrossSliding;
            gr.Dragging += gr_Dragging;
            gr.Holding += gr_Holding;
            gr.ManipulationCompleted += gr_ManipulationCompleted;
            gr.ManipulationInertiaStarting += gr_ManipulationInertiaStarting;
            gr.ManipulationStarted += gr_ManipulationStarted;
            gr.ManipulationUpdated += gr_ManipulationUpdated;
            gr.RightTapped += gr_RightTapped;
            gr.Tapped += gr_Tapped;
            gr.GestureSettings = Windows.UI.Input.GestureSettings.ManipulationRotate | Windows.UI.Input.GestureSettings.ManipulationTranslateX | Windows.UI.Input.GestureSettings.ManipulationTranslateY |
            Windows.UI.Input.GestureSettings.ManipulationScale | Windows.UI.Input.GestureSettings.ManipulationRotateInertia | Windows.UI.Input.GestureSettings.ManipulationScaleInertia |
            Windows.UI.Input.GestureSettings.ManipulationTranslateInertia | Windows.UI.Input.GestureSettings.Tap;
        }

        void MainPage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessUpEvent(ps[0]);
                e.Handled = true;
                gr.CompleteGesture();
            }
        }

        void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            gr.ProcessMoveEvents(e.GetIntermediatePoints(null));
            e.Handled = true;
        }

        void MainPage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessDownEvent(ps[0]);
                e.Handled = true;
            }
        }

        void gr_Tapped(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.TappedEventArgs args)
        {
            Debug.WriteLine("gr_Tapped");
        }
        void gr_RightTapped(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.RightTappedEventArgs args)
        {
            Debug.WriteLine("gr_RightTapped");
        }
        void gr_Holding(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.HoldingEventArgs args)
        {
            Debug.WriteLine("gr_Holding");
        }
        void gr_Dragging(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.DraggingEventArgs args)
        {
            Debug.WriteLine("gr_Dragging");
        }
        void gr_CrossSliding(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.CrossSlidingEventArgs args)
        {
            Debug.WriteLine("gr_CrossSliding");
        }
        void gr_ManipulationUpdated(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.ManipulationUpdatedEventArgs args)
        {
            Debug.WriteLine("gr_ManipulationUpdated");
        }
        void gr_ManipulationStarted(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.ManipulationStartedEventArgs args)
        {
            Debug.WriteLine("gr_ManipulationStarted");
        }
        void gr_ManipulationCompleted(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.ManipulationCompletedEventArgs args)
        {
            Debug.WriteLine("gr_ManipulationCompleted");
        }
        void gr_ManipulationInertiaStarting(Windows.UI.Input.GestureRecognizer sender, Windows.UI.Input.ManipulationInertiaStartingEventArgs args)
        {
            Debug.WriteLine("gr_ManipulationInertiaStarting");
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
                        stopwatchLbl.Text = String.Format("{0:00} {1:00} {2:00}", _stopwatch.Elapsed.Minutes, _stopwatch.Elapsed.Seconds, _stopwatch.Elapsed.Milliseconds / 10);
                    }
                );
       
        }

        private void textBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            increaseScore((TextBlock) sender);
        }


        void increaseScore(TextBlock scoreLbl)
        {
            int newValue = Convert.ToInt32(scoreLbl.Text) + 1;

            scoreLbl.Text = newValue.ToString();
            
        }
    }
}
