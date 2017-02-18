using ShakeGestures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShakeGesturesSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region CurrentShakeType dependency property

        public ShakeType CurrentShakeType
        {
            get { return (ShakeType)GetValue(CurrentShakeTypeProperty); }
            set { SetValue(CurrentShakeTypeProperty, value); }
        }

        public static readonly DependencyProperty CurrentShakeTypeProperty = DependencyProperty.Register("CurrentShakeType", typeof(ShakeType), typeof(MainPage), new PropertyMetadata(ShakeType.None));


        #endregion

        DispatcherTimer _resetTimer;

        public MainPage()
        {
            this.InitializeComponent();
            // register shake event
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);

            // optional, set parameters
            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 2;

            // start shake detection
            ShakeGesturesHelper.Instance.Active = true;

            // run shake Z simulation
            //ShakeGesturesHelper.Instance.Simulate(ShakeType.Z);

            _resetTimer = new DispatcherTimer();
            _resetTimer.Interval = TimeSpan.FromSeconds(3);
            _resetTimer.Tick += _resetTimer_Tick;

        }

        private void _resetTimer_Tick(object sender, object e)
        {
            if (_resetTimer.IsEnabled)
                _resetTimer.Stop();

            CurrentShakeType = ShakeType.None;
        }

        private void Instance_ShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    _lastUpdateTime.Text = DateTime.Now.ToString();
                    CurrentShakeType = e.ShakeType;

                    if (_resetTimer.IsEnabled)
                        _resetTimer.Stop();
                    _resetTimer.Start();
                });
            System.Diagnostics.Debug.WriteLine(DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "  " + e.ShakeType);
        }
    }
}
