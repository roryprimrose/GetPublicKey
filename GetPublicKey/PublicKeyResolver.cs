using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using GetPublicKey.Properties;
using Microsoft.Win32;

namespace GetPublicKey
{
    /// <summary>
    /// The <see cref="PublicKeyResolver"/>
    /// class is used to resolve public keys from assemblys, snk or pub files.
    /// </summary>
    internal static class PublicKeyResolver
    {
        /// <summary>
        /// Defines the name of the registry key value for storing SDK current install folders.
        /// </summary>
        private const String CurrentInstallFolderValue = "CurrentInstallFolder";

        /// <summary>
        /// Defines the file extension for assembly files.
        /// </summary>
        private const String DllFileExtension = ".DLL";

        /// <summary>
        /// Defines the file extension for application files.
        /// </summary>
        private const String ExeFileExtension = ".EXE";

        /// <summary>
        /// Defines the name of the registry key value for storing SDK installation folders.
        /// </summary>
        private const String InstallationFolderValue = "InstallationFolder";

        /// <summary>
        /// Defines the file extension for pub files.
        /// </summary>
        private const String PubFileExtension = ".PUB";

        /// <summary>
        /// Defines the file extension for snk files.
        /// </summary>
        private const String SnkFileExtension = ".SNK";

        /// <summary>
        /// Defines the name of the sn assembly.
        /// </summary>
        private const String SNName = "sn.exe";

        /// <summary>
        /// Gets the public key.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A <see cref="String"/> value.</returns>
        internal static String GetPublicKey(String filePath)
        {
            const String FilePathParameterName = "filePath";

            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(FilePathParameterName, ExceptionMessageResources.NoFilePathSpecified);
            }
            else if (File.Exists(filePath) == false)
            {
                throw new ArgumentNullException(
                    FilePathParameterName, ExceptionMessageResources.FilePathSpecifiedDoesNotExist);
            }

            String extension = Path.GetExtension(filePath).ToUpperInvariant();
            String publicKeyResponse;

            switch (extension)
            {
                case SnkFileExtension:

                    publicKeyResponse = GetPublicKeyFromSnk(filePath);

                    break;

                case PubFileExtension:

                    publicKeyResponse = GetPublicKeyFromPub(filePath);

                    break;

                case DllFileExtension:
                case ExeFileExtension:

                    publicKeyResponse = GetPublicKeyFromAssembly(filePath);

                    break;

                default:

                    throw new NotSupportedException(
                        String.Format(
                            CultureInfo.CurrentUICulture,
                            ExceptionMessageResources.FileTypeNotSupportedFormat,
                            extension));
            }

            const String publicKeyRegEx = "(?<=Public key is\r\n)[0-9A-Za-z\r\n]+(?=Public key token is )";
            Match publicKeyMatch = Regex.Match(publicKeyResponse, publicKeyRegEx);

            // Check if the value was found
            if ((publicKeyMatch != null)
                && (publicKeyMatch.Length > 0))
            {
                // Remove the new line character sets
                String publicKeyValue = publicKeyMatch.Value.Replace(Environment.NewLine, String.Empty);

                Trace.TraceInformation("Public key identified as {0}", publicKeyValue);

                // Return the upper case value
                return publicKeyValue.ToUpperInvariant();
            }
            else
            {
                // Failed to find the public key value
                throw new ApplicationException(ExceptionMessageResources.NoPublicKeyFound);
            }
        }

        /// <summary>
        /// Determines whether [is file type supported] [the specified file path].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// 	<c>true</c> if [is file type supported] [the specified file path]; otherwise, <c>false</c>.
        /// </returns>
        internal static Boolean IsFileTypeSupported(String filePath)
        {
            String extension = Path.GetExtension(filePath).ToUpperInvariant();

            switch (extension)
            {
                case SnkFileExtension:
                case PubFileExtension:
                case DllFileExtension:
                case ExeFileExtension:

                    return true;

                default:

                    return false;
            }
        }

        /// <summary>
        /// Gets the public key from assembly.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A <see cref="String"/> value.</returns>
        private static String GetPublicKeyFromAssembly(String filePath)
        {
            Debug.Assert(String.IsNullOrEmpty(filePath) == false);
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(
                ((Path.GetExtension(filePath).ToUpperInvariant() == DllFileExtension)
                 || (Path.GetExtension(filePath).ToUpperInvariant() == ExeFileExtension)));

            const String assemblyArgumentFormat = "-Tp \"{0}\"";
            String arguments = String.Format(CultureInfo.InvariantCulture, assemblyArgumentFormat, filePath);

            return RunProcess(arguments);
        }

        /// <summary>
        /// Gets the public key from pub.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A <see cref="String"/> value.</returns>
        private static String GetPublicKeyFromPub(String filePath)
        {
            Debug.Assert(String.IsNullOrEmpty(filePath) == false);
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(Path.GetExtension(filePath).ToUpperInvariant() == PubFileExtension);

            const String pubArgumentFormat = "-tp \"{0}\"";
            String arguments = String.Format(CultureInfo.InvariantCulture, pubArgumentFormat, filePath);

            return RunProcess(arguments);
        }

        /// <summary>
        /// Gets the public key from SNK.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A <see cref="String"/> value.</returns>
        private static String GetPublicKeyFromSnk(String filePath)
        {
            Debug.Assert(String.IsNullOrEmpty(filePath) == false);
            Debug.Assert(File.Exists(filePath));
            Debug.Assert(Path.GetExtension(filePath).ToUpperInvariant() == SnkFileExtension);

            const String pubTemporaryFileNameFormat = "{0}" + PubFileExtension;

            // Build a temporary path to store the pub file output
            String pubPath = Path.Combine(
                Path.GetTempPath(),
                String.Format(CultureInfo.InvariantCulture, pubTemporaryFileNameFormat, Guid.NewGuid()));

            try
            {
                const String snkArgumentFormat = "-p \"{0}\" \"{1}\"";
                String arguments = String.Format(CultureInfo.InvariantCulture, snkArgumentFormat, filePath, pubPath);

                RunProcess(arguments);

                return GetPublicKeyFromPub(pubPath);
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
        /// Gets the SN path.
        /// </summary>
        /// <returns>A <see cref="String"/> value.</returns>
        private static String GetSNPath()
        {
            // Check if the path has already been determined
            if (String.IsNullOrEmpty(SNPath) == false)
            {
                return SNPath;
            }

            SNPath = SearchRegistry();

            if (String.IsNullOrEmpty(SNPath))
            {
                // The sn.exe application is not found
                throw new ApplicationException(ExceptionMessageResources.SnApplicationNotFound);
            }

            Trace.TraceInformation("Found {0}", SNPath);

            return SNPath;
        }

        /// <summary>
        /// Runs the process.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        private static String RunProcess(String arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(GetSNPath(), arguments);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;

            Trace.TraceInformation(
                String.Format(
                    CultureInfo.InvariantCulture, "Executing \"{0}\" {1}", startInfo.FileName, startInfo.Arguments));
            Process snkProcess = Process.Start(startInfo);

            String processOutput = snkProcess.StandardOutput.ReadToEnd();

            Trace.TraceInformation(processOutput);

            return processOutput;
        }

        /// <summary>
        /// Searches the registry.
        /// </summary>
        /// <returns></returns>
        private static String SearchRegistry()
        {
            const String BaseKeyPath = "Software\\Microsoft\\Microsoft SDKs";
            RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(
                BaseKeyPath,
                RegistryKeyPermissionCheck.ReadSubTree,
                RegistryRights.EnumerateSubKeys | RegistryRights.QueryValues | RegistryRights.ReadKey);

            return SearchRegistryKey(baseKey);
        }

        /// <summary>
        /// Searches the registry key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static String SearchRegistryKey(RegistryKey key)
        {
            try
            {
                // Get the value names in this key
                String[] valueNames = key.GetValueNames();

                // Loop through each value in the key
                foreach (String valueName in valueNames)
                {
                    // Check if the value name indicates an SDK installation folder
                    if ((valueName == InstallationFolderValue)
                        || (valueName == CurrentInstallFolderValue))
                    {
                        // Get the installation path
                        String installationPath = key.GetValue(valueName) as String;

                        // Check that the directory exists
                        if (Directory.Exists(installationPath))
                        {
                            // Calculate the path to the sn.exe application
                            String snPath = Path.Combine(installationPath, SNName);

                            // Check if the path exists
                            if (File.Exists(snPath))
                            {
                                // We have found the application
                                // Return the path
                                return snPath;
                            }

                            // The application wasn't found in the installation directory
                            const String BinRelativePath = "bin";

                            String binPath = Path.Combine(installationPath, BinRelativePath);

                            // Check if the bin directory exists
                            if (Directory.Exists(binPath))
                            {
                                // Calculate the path of the application in the bin path
                                snPath = Path.Combine(binPath, SNName);

                                // Check if the path exists
                                if (File.Exists(snPath))
                                {
                                    // We have found the application
                                    // Return the path
                                    return snPath;
                                }
                            }
                        }
                    }
                }

                // Get the sub keys
                String[] subKeys = key.GetSubKeyNames();

                // Loop through each sub key
                foreach (String subKey in subKeys)
                {
                    // Recursively search the sub keys
                    String recursiveValue = SearchRegistryKey(key.OpenSubKey(subKey));

                    // Check if the recursion found a value to return from a sub key
                    if (String.IsNullOrEmpty(recursiveValue) == false)
                    {
                        // Return the value found
                        return recursiveValue;
                    }
                }
            }
            finally
            {
                key.Close();
            }

            // No value has been found
            return String.Empty;
        }

        /// <summary>
        /// Gets or sets the SN path.
        /// </summary>
        /// <value>The SN path.</value>
        private static String SNPath
        {
            get;
            set;
        }
    }
}