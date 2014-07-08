using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using YoAPI;

namespace YoAPI.Example
{
    /// <summary>
    /// A simple example using the YoAPIClass.
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer sentTimer = new DispatcherTimer();

        public MainWindow()
        {
            this.InitializeComponent();
            this.sentTimer.Tick += this.SentTimer_Tick;
            this.sentTimer.Interval = new TimeSpan(0, 0, 3);
        }

        private void SentTimer_Tick(object sender, EventArgs e)
        {
            this.YoButton.Content = "Yo";
            this.sentTimer.Stop();
        }

        private async void YoButton_Click(object sender, RoutedEventArgs e)
        {
            await this.PostYo();
        }

        /// <summary>
        /// Sennds a yo to all subscribers of the account associated with the api token.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        private async Task PostYo()
        {
            using (var client = new HttpClient())
            {
                this.YoButton.IsEnabled = false;
                this.YoButton.Content = "Sending..";

                YoAPIClass.APIToken = ConfigurationManager.AppSettings["api_token"];
                await YoAPIClass.YoAll();

                if (YoAPIClass.LastCallSuccessful)
                {
                    this.YoButton.Content = "Sent!";
                    this.sentTimer.Start();
                }
                else
                {
                    MessageBox.Show(YoAPIClass.ExceptionObject.Message);
                }

                this.YoButton.IsEnabled = true;
            }
        }
    }
}
