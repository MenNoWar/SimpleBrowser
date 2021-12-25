using CefSharp.Handler;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CareIt_Desktop
{
    public partial class Form1 : Form
    {
        // private ChromiumWebBrowser browser;
        string[] args;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            browser.RequestHandler = new rc();
        }

        private void Browser_JavascriptMessageReceived(object sender, CefSharp.JavascriptMessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void Browser_AddressChanged(object sender, CefSharp.AddressChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                var url = e.Address.ToString();

                var b = browser.GetBrowser();
                if (b != null && !b.IsPopup && !comboBox1.Items.Contains(url))
                {
                    comboBox1.Items.Insert(0, url);
                }

                comboBox1.Text = url;
            }));
        }

        private void Browser_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            this.Invoke(new Action(() => this.Text = e.Title.Trim()));
        }

        private void Browser_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
            this.Invoke(new Action(() => this.statusLabel.Text = e.Value));
        }

        private void Browser_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            if (browser.IsBrowserInitialized)
            {
                if (this.args.Length > 0)
                    if (args[0].ToUpper().StartsWith("HTTP"))
                        gotoUrl(args[0]);
            }
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    this.statusLabel.Text = e.IsLoading ? "Loading..." : "Done";
                    btnBack.Enabled = e.CanGoBack;
                    btnForward.Enabled = e.CanGoForward;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = "Very Simple Browser";
        }

        void StopLoading()
        {
            this.Invoke(new Action(() =>
                {
                    var b = browser.GetBrowser();
                    if (b != null)
                        b.StopLoad();
                }));
        }

        void gotoUrl(string url = "")
        {
            this.Invoke
                (new Action(() =>
                {
                    if (string.IsNullOrEmpty(url)) url = comboBox1.Text;
                    if (browser.Address != null && browser.Address.ToUpper() == url.ToUpper())
                        return;

                    browser.Load(url);
                }));
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            gotoUrl();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (browser.CanGoBack)
                browser.GetBrowser().GoBack();

            //browser.JavascriptObjectRepository.Register("cefCustomObject", new CefCustomObject(chromeBrowser, this));
            // browser.DownloadHandler.OnDownloadUpdated
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (browser.CanGoForward)
                browser.GetBrowser().GoForward();
        }

        private void btnRefreh_Click(object sender, EventArgs e)
        {
            var b = browser.GetBrowser();
            if (b != null) b.Reload();
        }

        private void browser_ConsoleMessage_1(object sender, CefSharp.ConsoleMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                gotoUrl(comboBox1.Text);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            gotoUrl((string)comboBox1.SelectedItem);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopLoading();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBox1.SelectionStart = 0;
            comboBox1.SelectionLength = comboBox1.Text.Length;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CefSharp.Cef.Shutdown();
        }

    }

    public class rc : RequestHandler
    {
        protected override bool OnOpenUrlFromTab(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, string targetUrl, CefSharp.WindowOpenDisposition targetDisposition, bool userGesture)
        {
            if (!string.IsNullOrEmpty(targetUrl) && !targetUrl.ToUpper().StartsWith("JAVASCRIPT:"))
            {
                var exe = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.FriendlyName);
                return !Process.Start(exe, targetUrl).HasExited;
            }
            else
            {
                return base.OnOpenUrlFromTab(chromiumWebBrowser, browser, frame, targetUrl, targetDisposition, userGesture);
            }
        }
    }
}