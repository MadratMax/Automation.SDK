namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class FileUtils
    {
        /// <summary>
        /// Gets the APP path
        /// </summary>
        /// <returns>The string type object</returns>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Legacy code")]
        public static string getAppPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Replace(@"file:///", string.Empty));
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="path">Location to the file</param>
        /// <param name="filename">Name of the file to delete</param>
        /// <param name="ReportError">Report assertions</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Legacy code")]
        public static void DeleteFile_ext(string path, string filename = "", bool ReportError = false)
        {
            string filePath = Path.Combine(path, filename);

            if (File.Exists(filePath))
            {
                if (!ReportError)
                {
                    //// We do not want any Exceptions or errors if the file cannot be deleted
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {
                    File.Delete(filePath);
                }
            }
            else if (ReportError)
            {
                CompareResult(false, "The file: " + filePath + " not found.");
            }
        }

        /// <summary>
        /// Verify the content of the file
        /// </summary>
        /// <param name="path">Location to the file</param>
        /// <param name="filename">Name of the file</param>
        /// <param name="content">Expected content of the file</param>
        public static void VerifyFileContent(string path, string filename, string content)
        {
            string filePath = Path.Combine(path, filename);

            CompareResult(File.Exists(filePath), "File does not exist: " + filePath);

            string fileData = File.ReadAllText(filePath).Trim();
            AreEqual(content, fileData, "Wrong file content was returned from '" + filePath + "'");
        }

        /// <summary>
        /// Compare the contents of 2 files
        /// </summary>
        /// <param name="path1">Source file path</param>
        /// <param name="path2">Target file path</param>
        /// <param name="filename1">File name of the source</param>
        /// <param name="filename2">File name of the target</param>
        public static void CompareFileContent(string path1, string path2, string filename1, string filename2)
        {
            string filePath1 = Path.Combine(path1, filename1);
            string filePath2 = Path.Combine(path2, filename2);
            CompareResult(File.Exists(filePath1), "File does not exist: " + filePath1);
            CompareResult(File.Exists(filePath2), "File does not exist: " + filePath2);

            string fileData1 = File.ReadAllText(filePath1).Trim().Replace("\r\n", "\n");
            string fileData2 = File.ReadAllText(filePath2).Trim().Replace("\r\n", "\n");

            // compare only files with non zero bytes
            CompareResult(fileData1.Length > 0, "File is empty: " + filePath1);
            CompareResult(fileData2.Length > 0, "File is empty: " + filePath2);

            // compare only files with same length
            CompareResult(fileData1.Length.Equals(fileData2.Length), "File lengths did not match. " + filename1 + " and " + filename2 + " are not equal.");

            for (int i = 0; i < fileData1.Length; i++)
            {
                if (fileData1[i] != fileData2[i])
                {
                    CompareResult(false, "Data mismatch. \r\nFrom file1: " + GetFragment(fileData1, i) + "\r\nFrom file2: " + GetFragment(fileData2, i));
                }
            }
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="dirPath">Location to the file</param>
        /// <param name="filename">File name</param>
        public static void DeleteFile(string dirPath, string filename)
        {
            string deleteFile = Path.Combine(dirPath, filename);

            // Delete a file
            if (File.Exists(deleteFile))
            {
                File.Delete(deleteFile);
            }
        }

        /// <summary>
        /// Delete all files and subdirectories in a directory
        /// </summary>
        /// <param name="dirPath">Location to the files</param>
        public static void DeleteAllFiles(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo deleteDirInfo = new DirectoryInfo(dirPath);

                //// Delete all files in a directory
                foreach (FileInfo file in deleteDirInfo.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (IOException)
                    {
                        ////file is currently locked
                    }
                }

                // Delete all subdirectories
                foreach (DirectoryInfo dir in deleteDirInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
        }

        /// <summary>
        /// Delete a directory and all subdirectories
        /// </summary>
        /// <param name="path">Location of the directory</param>
        /// <param name="dirName">Directory name</param>
        /// <param name="delSubDirs">True if subdirectories are to be delete, otherwise do not delete subdirectories</param>
        public static void DeleteDir(string path, string dirName, bool delSubDirs)
        {
            string deleteDir = Path.Combine(path, dirName);

            if (!delSubDirs)
            {
                // Delete a directory
                if (Directory.Exists(deleteDir))
                {
                    Directory.Delete(deleteDir);
                }
            }
            else
            {
                // Delete a directory and all subdirectories
                if (Directory.Exists(deleteDir))
                {
                    Directory.Delete(deleteDir, true);
                }
            }
        }

        /// <summary>
        /// The delete directory.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public static void DeleteDirectory(string path)
        {
            DeleteDirectory(path, false);
        }

        /// <summary>
        /// The delete directory.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="recursive">
        /// The recursive.
        /// </param>
        public static void DeleteDirectory(string path, bool recursive)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder
            Directory.Delete(path);
        }

        /// <summary>
        /// Copy a file
        /// </summary>
        /// <param name="sourcePath">Location of the file to be copied</param>
        /// <param name="targetPath">Location of the directory to copy the file to</param>
        /// <param name="filename">Name of the file to copy</param>
        public static void CopyFile(string sourcePath, string targetPath, string filename)
        {
            if (Directory.Exists(sourcePath))
            {
                string sourceFile = Path.Combine(sourcePath, filename);
                string destFile = Path.Combine(targetPath, filename);

                // Create the target folder, if it doesn't exist
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                // Copy the file to the target location and overwrite the destination file if it already exists
                File.Copy(sourceFile, destFile, true);
            }
            else
            {
                // ...
                // Console.WriteLine("Source path does not exist!");
            }
        }

        /// <summary>
        /// The GetLastModifiedFileInDir method
        /// </summary>
        /// <param name="path">The path parameter</param>
        /// <returns>The System.IO.FileInfo type object</returns>        
        public static FileInfo GetLastModifiedFileInDir(string path)
        {
            var directory = new DirectoryInfo(path);
            FileInfo lastModifiedFile = null;

            if (directory.Exists)
            {
                var files = directory.GetFiles();

                if (files.Length > 0)
                {
                    lastModifiedFile = files.OrderByDescending(f => f.LastWriteTime).First();
                }
            }

            return lastModifiedFile;
        }

        /// <summary>
        /// Copy files from a directory and all subdirectories
        /// </summary>
        /// <param name="sourcePath">Location to the files to be copied</param>
        /// <param name="targetPath">Location to where the files will be copied to</param>
        /// <param name="copySubDirs">True if subdirectories to be copied</param>
        public static void CopyDir(string sourcePath, string targetPath, bool copySubDirs)
        {
            if (Directory.Exists(sourcePath))
            {
                // Get the subdirectories for the specified directory
                DirectoryInfo dir = new DirectoryInfo(sourcePath);
                DirectoryInfo[] dirs = dir.GetDirectories();

                // Create the target folder, if it doesn't exist
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                // Get the files in the directory and copy them to the new location
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tempPath = Path.Combine(targetPath, file.Name);
                    file.CopyTo(tempPath, true);
                }

                // If copying subdirectories, copy them and their contents to new location
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(targetPath, subdir.Name);
                        CopyDir(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }
            else
            {
                // ...
                // Console.WriteLine("Source path does not exist!");
            }
        }

        /// <summary>
        /// Assert true
        /// </summary>
        /// <param name="result">Expected result</param>
        /// <param name="message">Failure message</param>
        private static void CompareResult(bool result, string message)
        {
            Assert.IsTrue(result, message);
        }

        /// <summary>
        /// Assert equal
        /// </summary>
        /// <param name="s1">Expected result</param>
        /// <param name="s2">Actual result</param>
        /// <param name="message">Failure message</param>
        private static void AreEqual(string s1, string s2, string message)
        {
            Assert.AreEqual(s1, s2, message);
        }

        /// <summary>
        /// Returns fragment of string around the index.
        /// Get -20 and + 10
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="index">Index</param>
        /// <returns>Fragment of string</returns>
        private static string GetFragment(string value, int index)
        {
            if (value.Length < 30)
            {
                return value;
            }

            int start = Math.Max(index - 20, 0);
            int end = Math.Min(index + 10, value.Length);

            return value.Substring(start, end - start - 1);
        }
    }
}
