﻿using System.Windows;
using SharpBlade;
using SharpBlade.Native;
using SharpBlade.Razer;

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

            //Get Switchblade instance
            ISwitchblade sbInstance = Switchblade.Instance;

            //Add the event for checking the App Status
            sbInstance.AppEvent += OnAppEvent;

            // This sends the current window to the SBUI
            sbInstance.Touchpad.Set(this);

            /* Here are some dynamic keys I made for Razer Calculator
             * I enable them with parameters of the Dynamic Key I want to 
             * enable, a method I want to 'callback' to when it's pressed
             * and the key I want to show.
             */
            sbInstance.DynamicKeys.Enable(DynamicKeyType.DK1, OnPlusPress, @"Default\Images\PlusDK.png");
            sbInstance.DynamicKeys.Enable(DynamicKeyType.DK2, OnMinusPress, @"Default\Images\MinusDK.png");
            sbInstance.DynamicKeys.Enable(DynamicKeyType.DK3, OnMultiplyPress, @"Default\Images\MultiplyDK.png");
            sbInstance.DynamicKeys.Enable(DynamicKeyType.DK4, OnDividePress, @"Default\Images\DivideDK.png");
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
        void OnAppEvent(object sender, SharpBlade.Events.AppEventEventArgs appEventArgs)
        {
            /* Just an example here.  If the app becomes deactivated, is closed or force quit, then close the app.
             * Potentially you could run a App Lifecycle here where when it's deactivated, it unbinds any data connection
             * And when the app is activated again, it reloads the data source and opens back up
             */
            if (appEventArgs.EventType == AppEventType.Deactivated || appEventArgs.EventType == AppEventType.Close ||
                appEventArgs.EventType == AppEventType.Exit)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
