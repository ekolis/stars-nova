// ============================================================================
// (c) Mathew Adams, 2001
// The Code Project
// http://www.codeproject.com/KB/miscctrl/progressdialog.aspx
// ===========================================================================

#region Module Description
// ===========================================================================
// A progress dialog box.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nova.Common
{
    public partial class ProgressDialog : Form, IProgressCallback
    {
        public delegate void SetTextInvoker(string text);
        public delegate void IncrementInvoker(int val);
        public delegate void StepToInvoker(int val);
        public delegate void RangeInvoker(int minimum, int maximum);

        private string titleRoot = "";
        private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
        private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
        private bool requiresClose = true;
        private bool operationSuccess; // shadow for property Success


        public ProgressDialog()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }

        #region Implementation of IProgressCallback
        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress meter.
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        public void Begin(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoBegin), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        public void Begin()
        {
            initEvent.WaitOne();
            Invoke(new MethodInvoker(DoBegin));
        }

        /// <summary>
        /// Call this method from the worker thread to reset the range in the progress callback
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void SetRange(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoSetRange), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        public void SetText(string text)
        {
            Invoke(new SetTextInvoker(DoSetText), new object[] { text });
        }

        /// <summary>
        /// Call this method from the worker thread to increase the progress counter by a specified value.
        /// </summary>
        /// <param name="val">The amount by which to increment the progress indicator</param>
        public void Increment(int val)
        {
            Invoke(new IncrementInvoker(DoIncrement), new object[] { val });
        }

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a particular value.
        /// </summary>
        /// <param name="val"></param>
        public void StepTo(int val)
        {
            Invoke(new StepToInvoker(DoStepTo), new object[] { val });
        }


        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        public bool IsAborting
        {
            get
            {
                return abortEvent.WaitOne(0, false);
            }
        }

        public bool Success
        {
            get
            {
                return this.operationSuccess;
            }
            set
            {
                this.operationSuccess = value;
            }
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        public void End()
        {
            if (requiresClose)
            {
                Invoke(new MethodInvoker(DoEnd));
            }
        }
        #endregion

        #region Implementation members invoked on the owner thread
        private void DoSetText(string text)
        {
            label.Text = text;
        }

        private void DoIncrement(int val)
        {
            ProgressBar.Increment(val);
            UpdateStatusText();
        }

        private void DoStepTo(int val)
        {
            ProgressBar.Value = val;
            UpdateStatusText();
        }

        private void DoBegin(int minimum, int maximum)
        {
            DoBegin();
            DoSetRange(minimum, maximum);
        }

        private void DoBegin()
        {
            cancelButton.Enabled = true;
            ControlBox = true;
        }

        private void DoSetRange(int minimum, int maximum)
        {
            ProgressBar.Minimum = minimum;
            ProgressBar.Maximum = maximum;
            ProgressBar.Value = minimum;
            titleRoot = Text;
        }

        private void DoEnd()
        {
            Close();
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Handles the form load, and sets an event to ensure that
        /// intialization is synchronized with the appearance of the form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            ControlBox = false;
            initEvent.Set();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        */

        /// <summary>
        /// Handler for 'Close' clicking
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            requiresClose = false;
            AbortWork();
            base.OnClosing(e);
        }

        #endregion

        #region Implementation Utilities
        /// <summary>
        /// Utility function that formats and updates the title bar text
        /// </summary>
        private void UpdateStatusText()
        {
            Text = titleRoot + String.Format(" - {0}% complete", (ProgressBar.Value * 100) / (ProgressBar.Maximum - ProgressBar.Minimum));
        }

        /// <summary>
        /// Utility function to terminate the thread
        /// </summary>
        private void AbortWork()
        {
            abortEvent.Set();
        }
        #endregion

    }
}
