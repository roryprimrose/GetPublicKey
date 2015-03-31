using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GetPublicKey
{
    public partial class Form1 : Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        
        private void BrowsePath_Click(object sender, EventArgs e)
        {
            if (BrowseDialog.ShowDialog() == DialogResult.OK)
            {
                ProcessPath(BrowseDialog.FileName);
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets the SN path.
        /// </summary>
        /// <returns></returns>
        private static String GetSNPath()
        {
            const String snName = "sn.exe";
            FileInfo snInfo = new FileInfo(snName);

            if (snInfo.Exists == false)
            {
                throw new Exception("sn.exe not available.");
            }

            return snInfo.FullName;
        }

        /// <summary>
        /// Processes the path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static String ProcessPath(String filePath)
        {
            String extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".snk":

                    return GetTokenFromSnk(filePath);

                case ".pub":

                    return GetTokenFromPub(filePath);

                case ".dll":
                case ".exe":

                    return GetTokenFromAssembly(filePath);

                default:

                    throw new NotSupportedException(String.Format("File extension {0} is not supported.", extension));
            }
        }

        /// <summary>
        /// Gets the token from SNK.
        /// </summary>
        /// <param name="filePath">The filepath.</param>
        /// <returns></returns>
        private static String GetTokenFromSnk(String filePath)
        {
            Debug.Assert(String.IsNullOrEmpty(filePath) == false);
            Debug.Assert(Path.GetExtension(filePath).ToLower() == ".snk");

            String pubPath = Path.GetTempFileName();

            try
            {
                const String snkArgumentFormat = "-p \"{0}\" \"{1}\"";

                ProcessStartInfo pubStartInfo = new ProcessStartInfo(GetSNPath(), String.Format(snkArgumentFormat, filePath, pubPath));
                Process.Start(pubStartInfo);

                return GetTokenFromPub(pubPath);
            }
            finally
            {
                // Delete the temporary file
                if (File.Exists(pubPath))
                {
                    File.Delete(pubPath);
                }
            }
        }

        /// <summary>
        /// Gets the token from pub.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static String GetTokenFromPub(String filePath)
        {
            Debug.Assert(String.IsNullOrEmpty(filePath) == false);
            Debug.Assert(Path.GetExtension(filePath).ToLower() == ".pub");

            String outputPath = Path.GetTempFileName();

            try
            {
                const String pubArgumentFormat = "-t \"{0}\" > \"{1}\"";

                ProcessStartInfo pubStartInfo = new ProcessStartInfo(GetSNPath(), String.Format(pubArgumentFormat, filePath, outputPath));
                Process.Start(pubStartInfo);

                // TODO: Read the output from the process
            }
            finally
            {
                // Delete the temporary file
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
            }
        }

        /// <summary>
        /// Gets the token from assembly.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static String GetTokenFromAssembly(String filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}