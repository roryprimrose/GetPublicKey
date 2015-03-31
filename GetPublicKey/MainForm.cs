using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace GetPublicKey
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Defines the name of the EventTraceListener used by the form to capture Trace messages.
        /// </summary>
        private const String EventTraceListenerName = "EventTraceListenerInstance";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds the trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void AddTraceMessage(String message)
        {
            if (TraceOutput.InvokeRequired)
            {
                TraceOutput.Invoke(new StringDelegate(AddTraceMessage), new Object[] {message});

                return;
            }

            TraceOutput.Text += message;
        }

        /// <summary>
        /// Handles the Click event of the BrowsePath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BrowsePath_Click(Object sender, EventArgs e)
        {
            SelectNewFile();
        }

        /// <summary>
        /// Calculates the value.
        /// </summary>
        private void CalculateValue()
        {
            const String attributeNamespace = "System.Runtime.CompilerServices";
            const String attributeOutputFormat = "[assembly:{0}InternalsVisibleTo(\"{1}, PublicKey={2}\")]";

            String attributeNamespaceValue = String.Empty;

            if (IncludeNamespace.Checked)
            {
                attributeNamespaceValue = attributeNamespace;
            }

            // Populate the textbox
            PublicKeyOutput.Text = String.Format(
                CultureInfo.CurrentUICulture,
                attributeOutputFormat,
                attributeNamespaceValue,
                LastAssemblyName,
                LastPublicKeyValue);
        }

        /// <summary>
        /// Handles the Click event of the CloseForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseForm_Click(Object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the CopyClipboard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyClipboard_Click(Object sender, EventArgs e)
        {
            Clipboard.SetText(PublicKeyOutput.Text);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the IncludeNamespace control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void IncludeNamespace_CheckedChanged(Object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(LastPublicKeyValue) == false)
            {
                CalculateValue();
            }
            else
            {
                SelectNewFile();
            }
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(Object sender, EventArgs e)
        {
            ShowLog.Checked = false;

            EventTraceListener listener = new EventTraceListener(EventTraceListenerName);
            listener.MessageWritten += TraceListenter_MessageWritten;
            Trace.Listeners.Add(listener);

            try
            {
                foreach (String argument in Environment.GetCommandLineArgs())
                {
                    if (File.Exists(argument)
                        && (argument.ToUpperInvariant() != Assembly.GetExecutingAssembly().Location.ToUpperInvariant())
                        && PublicKeyResolver.IsFileTypeSupported(argument))
                    {
                        ResolvePublicKey(argument);

                        break;
                    }
                }
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// Resolves the public key.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private void ResolvePublicKey(String filePath)
        {
            Debug.Assert(File.Exists(filePath));

            try
            {
                TraceOutput.Clear();

                LastPublicKeyValue = PublicKeyResolver.GetPublicKey(filePath);
                LastAssemblyName = Path.GetFileNameWithoutExtension(filePath);

                CalculateValue();

                FailureMessage.Clear();
            }
            catch (Exception ex)
            {
                ShowLog.Checked = true;
                FailureMessage.SetIconAlignment(BrowsePath, ErrorIconAlignment.MiddleLeft);
                FailureMessage.SetError(BrowsePath, ex.Message);
            }
        }

        /// <summary>
        /// Selects the new file.
        /// </summary>
        private void SelectNewFile()
        {
            if (BrowseDialog.ShowDialog()
                == DialogResult.OK)
            {
                ResolvePublicKey(BrowseDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ShowLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowLog_CheckedChanged(Object sender, EventArgs e)
        {
            ToggleLogView();
        }

        /// <summary>
        /// Toggles the log view.
        /// </summary>
        private void ToggleLogView()
        {
            if (ShowLog.Checked
                == TraceOutput.Visible)
            {
                return;
            }

            // Toggle the visibility of the trace output textbox
            TraceOutput.Visible = (TraceOutput.Visible == false);

            Int32 keyHeight = PublicKeyOutput.Height;
            Int32 outputHeight = TraceOutput.Height;

            // Adjust the height of the form
            if (ShowLog.Checked)
            {
                Height += outputHeight;

                ShowLog.Top -= outputHeight;
                CopyClipboard.Top -= outputHeight;
                IncludeNamespace.Top -= outputHeight;
            }
            else
            {
                Height -= outputHeight;

                ShowLog.Top += outputHeight;
                CopyClipboard.Top += outputHeight;
                IncludeNamespace.Top += outputHeight;
            }

            PublicKeyOutput.Height = keyHeight;
        }

        /// <summary>
        /// Handles the MessageWritten event of the TraceListenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetPublicKey.EventTraceListenerEventArgs"/> instance containing the event data.</param>
        private void TraceListenter_MessageWritten(Object sender, EventTraceListenerEventArgs e)
        {
            AddTraceMessage(e.Message);
        }

        /// <summary>
        /// Handles the LinkClicked event of the WebLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void WebLink_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(WebLink.Text);
        }

        /// <summary>
        /// Gets or sets the last name of the assembly.
        /// </summary>
        /// <value>The last name of the assembly.</value>
        private String LastAssemblyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last token value.
        /// </summary>
        /// <value>The last token value.</value>
        private String LastPublicKeyValue
        {
            get;
            set;
        }

        /// <summary>
        /// The delegate for sending a string to a method.
        /// </summary>
        private delegate void StringDelegate(String message);
    }
}