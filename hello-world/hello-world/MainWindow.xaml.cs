using System.Windows;
using Sharparam.SharpBlade;
using Sharparam.SharpBlade.Native;
using Sharparam.SharpBlade.Razer;

namespace SharpBlade.HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Init XAML
            InitializeComponent();
            //Create SharpBlade RazerManager object
            RazerManager manager = new RazerManager();

            //Add the event for checking the App Status
            manager.AppEvent += OnAppEvent;

            /* This sends the current window to the SBUI
             * We give it the Polling RenderMethod which updates
             * SBUI every 42ms (about 24FPS)
             */
            manager.Touchpad.SetWindow(this, Touchpad.RenderMethod.Polling);

            /* Here are some dynamic keys I made for Razer Calculator
             * I enable them with parameters of the Dynamic Key I want to 
             * enable, a method I want to 'callback' to when it's pressed
             * and the key I want to show.
             */
            manager.EnableDynamicKey(RazerAPI.DynamicKeyType.DK1, OnPlusPress,
              @"Default\Images\PlusDK.png");
            manager.EnableDynamicKey(RazerAPI.DynamicKeyType.DK2, OnMinusPress,
               @"Default\Images\MinusDK.png");
            manager.EnableDynamicKey(RazerAPI.DynamicKeyType.DK3, OnMultiplyPress,
               @"Default\Images\MultiplyDK.png");
            manager.EnableDynamicKey(RazerAPI.DynamicKeyType.DK4, OnDividePress,
               @"Default\Images\DivideDK.png");

        }

        private void OnDividePress(object sender, System.EventArgs e)
        {
            SharpBladeTextBlock.Text = "Divide Pressed";
        }

        private void OnMultiplyPress(object sender, System.EventArgs e)
        {
            SharpBladeTextBlock.Text = "Multiply Pressed";
        }
       
        private void OnMinusPress(object sender, System.EventArgs e)
        {
            SharpBladeTextBlock.Text = "Minus Pressed";
        }

        private void OnPlusPress(object sender, System.EventArgs e)
        {
            SharpBladeTextBlock.Text = "Plus Pressed";
        }

        /// <summary>
        /// This event handler is fired when an app event happens
        /// </summary>
        /// <param name="sender">The object that called this event</param>
        /// <param name="appEventArgs">The arguments that give details as to the app event</param>
        void OnAppEvent(object sender, Sharparam.SharpBlade.Razer.Events.AppEventEventArgs appEventArgs)
        {
            /* Just an example here.  If the app becomes deactivated, is closed or force quit, then close the app.
             * Potentially you could run a App Lifecycle here where when it's deactivated, it unbinds any data connection
             * And when the app is activated again, it reloads the data source and opens back up
             */
            if (appEventArgs.Type == RazerAPI.AppEventType.Deactivated || appEventArgs.Type == RazerAPI.AppEventType.Close ||
                appEventArgs.Type == RazerAPI.AppEventType.Exit)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
