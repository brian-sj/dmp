using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using log4net;
using System.Reflection;
using MissionPlanner.Controls;
using System.Threading;
using System.ComponentModel;

namespace DMP.Dialogs
{
    /// <summary>
    /// frmProgressReporter.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressReporterDialogue : Window
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Exception workerException;
        public ProgressWorkerEventArgs doWorkArgs = new ProgressWorkerEventArgs();

        internal object locker = new object();
        internal int _progress = -1;
        internal string _status = "";

        public bool Running = false;

        private System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();

        public delegate void DoWorkEventHandler(object sender, ProgressWorkerEventArgs e, object passdata = null);

        // This is the event that will be raised on the BG thread
        public event DoWorkEventHandler DoWork;



        public ProgressReporterDialogue()
        {
            InitializeComponent();
            doWorkArgs = new ProgressWorkerEventArgs();
            //btnCancel.Visibility = Visibility.Hidden;
            //timer1.Tick += timer1_Tick;
        }

        public void RunBackgroundOperationAsync()
        {
            ThreadPool.QueueUserWorkItem(RunBackgroundOperation);
log.Info("RunBackgroundOperation");
            Console.WriteLine("   RunBackGroundOperationAsync ");
            var t = Type.GetType("Mono.Runtime");
            if ((t != null))
                this.Height += 25;

            // this.ShowDialog();
        }

        private void RunBackgroundOperation(object o)
        {
            Running = true;
            log.Info("RunBackgroundOperation");
            try
            {
                Thread.CurrentThread.Name = "ProgressReporterDialogue Background thread";
            }
            catch { } // ok on windows - fails on mono

            // mono fix - ensure the dialog is running
            try
            {
                var source = PresentationSource.FromVisual(this);
                while (source == null || source.IsDisposed == true)
                {
                    System.Threading.Thread.Sleep(200);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            try
            {
                

                this.Dispatcher.Invoke( new Action(delegate {
                    this.UpdateLayout();
                    }));
/*
                this.Invoke((MethodInvoker)delegate
                {
                    // make sure its drawn
                    this.Refresh();
                });
*/
            }
            catch { Running = false; return; }

            log.Info("Focus ctl ");

            try
            {
                this.Dispatcher.Invoke(new Action(delegate {
                    log.Info("in focus invoke");
                    // if this windows isnt the current active windows, popups inherit the wrong parent.
                    if (!this.IsFocused )
                    {
                        this.Focus();
                        this.UpdateLayout();
                    }
                    
                }));

            }
            catch { Running = false; return; }

            try
            {
                log.Info("DoWork");
                if (this.DoWork != null) this.DoWork(this, doWorkArgs);
                log.Info("DoWork Done");
            }
            catch (Exception e)
            {
                // The background operation thew an exception.
                // Examine the work args, if there is an error, then display that and the exception details
                // Otherwise display 'Unexpected error' and exception details
                timer1.Stop();
                ShowDoneWithError(e, doWorkArgs.ErrorMessage);
                Running = false;
                return;
            }

            // stop the timer
            timer1.Stop();

            // run once more to do final message and progressbar
            try
            {
                var source = PresentationSource.FromVisual(this);
                if (source == null || source.IsDisposed == true)
                    return;
            }
            catch (Exception ee) {
                Console.WriteLine(ee.Message );
            }
            

            try
            {


                this.Dispatcher.Invoke(new Action(delegate
                {
                    timer1_Tick(null, null);
                }));
            }
            catch
            {
                Running = false;
                return;
            }

            if (doWorkArgs.CancelRequested && doWorkArgs.CancelAcknowledged)
            {
                //ShowDoneCancelled();
                Running = false;
                this.Dispatcher.BeginInvoke(new Action(delegate { this.Close(); }));
                return;
            }

            if (!string.IsNullOrEmpty(doWorkArgs.ErrorMessage))
            {
                ShowDoneWithError(null, doWorkArgs.ErrorMessage);
                Running = false;
                return;
            }

            if (doWorkArgs.CancelRequested)
            {
                ShowDoneWithError(null, "Operation could not cancel");
                Running = false;
                return;
            }

            ShowDone();
            Running = false;
        }
        private void ShowDoneCancelled()
        {
            Dispatcher.Invoke( new Action(delegate
            {
                this.progressBar1.Visibility = Visibility.Hidden;
                this.lblProgressMessage.Content  = "Cancelled";
                this.btnClose.Visibility = Visibility.Visible;
            }));
        }

        // Called as a possible last operation of the bg thread
        // - Set progress bar to 100%
        // - Wait a little bit to allow the Aero progress animatiom to catch up
        // - Signal that we can close
        private void ShowDone()
        {
            /*
            var source = PresentationSource.FromVisual(this);
            if (source == null || source.IsDisposed == true)
                return;
                */
            Dispatcher.Invoke(  new Action(delegate
            {
                //this.progressBar1.Style = ProgressBarStyle.Continuous;
                this.progressBar1.Value = 100;
                this.btnCancel.Visibility = Visibility.Hidden;
                this.btnClose.Visibility = Visibility.Hidden;
            }));

            Thread.Sleep(100);

            //   Dispatcher.BeginInvoke(new Action(delegate { this.Close(); }));
        }

        public void UpdateProgressAndStatus(float progress, string status)
        {
            // we don't let the worker update progress when  a cancel has been
            // requested, unless the cancel has been acknowleged, so we know that
            // this progress update pertains to the cancellation cleanup
            if (doWorkArgs.CancelRequested && !doWorkArgs.CancelAcknowledged)
                return;

            lock (locker)
            {
                _progress = (int)( progress) ;
                _status = status;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var source = PresentationSource.FromVisual(this);
            if (source == null || source.IsDisposed == true)
                return;

            //this.

            int pgv = -1;
            lock (locker)
            {
                pgv = _progress;
                lblProgressMessage.Content = _status;
            }
            if (pgv == -1)
            {
                //this.progressBar1.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                //this.progressBar1.Style  = ProgressBarStyle.Continuous;
                try
                {
                    this.progressBar1.Value = pgv;
                } // Exception System.ArgumentOutOfRangeException: Value of '-12959800' is not valid for 'Value'. 'Value' should be between 'minimum' and 'maximum'.
                catch { } // clean fail. and ignore, chances are we will hit this again in the next 100 ms
            }
        }

        private void ShowDoneWithError(Exception exception, string doWorkArgs)
        {

            var errMessage = doWorkArgs ?? "There was an unexpected error";

            try
            {
                var source = PresentationSource.FromVisual(this);
                if (source == null || source.IsDisposed == true)
                    return;
            }
            catch (Exception e) { log.Error( e.Message); }
            
            if (!Dispatcher.CheckAccess())
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        this.Title = "Error";
                        //this.lblProgressMessage.Left = 65;
                        this.lblProgressMessage.Content = errMessage;
                        this.imgWarning.Visibility = Visibility.Visible ;
                        this.progressBar1.Visibility = Visibility.Hidden;
                        this.btnCancel.Visibility = Visibility.Hidden;
                        this.btnClose.Visibility = Visibility.Visible;
                        this.linkLabel1.Visibility = (exception != null) ? Visibility.Visible : Visibility.Hidden;
                        this.workerException = exception;
                    }));
                }
                catch { } // disposing
            }
            else
            {
                this.lblProgressMessage.Content = errMessage;
                this.imgWarning.Visibility = Visibility.Visible;
                this.progressBar1.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                this.btnClose.Visibility = Visibility.Visible;
                this.linkLabel1.Visibility = (exception != null) ? Visibility.Visible : Visibility.Hidden;
                this.workerException = exception;
            }

        }


        public ProgressReporterDialogue( float progress , string text)
        {
            InitializeComponent();
            progressBar1.Value = progress;
            lblProgressMessage.Content = text;
        }


        private void progressbar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    
}
