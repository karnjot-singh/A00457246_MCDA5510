using System;
using System.IO;

namespace assignmentdontnet
{
    class DirWalker
    {
        
        // Open given file and return StreamWriter Object 
        public StreamWriter openFile(string filePath)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                return new StreamWriter(fileStream);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file or directory cannot be found - " + filePath);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The file or directory cannot be found - " + filePath);
            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("The drive specified in 'path' is invalid - " + filePath);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("'path' exceeds the maxium supported path length - " + filePath);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to create this file - " + filePath);
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                Console.WriteLine("There is a sharing violation - " + filePath);
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                Console.WriteLine("The file already exists - " + filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred:\nMessage: " + e.Message);
            }
            return null;
        }
        
        // Close given file
        public void closeFile(StreamWriter file)
        {
            try
            {
                file.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occured while closing the file - Message: " + exception.Message);
            }
        }

        // Walk throught directories recursively and process all CSV files
        public void walk(String path, StreamWriter outputFile, StreamWriter logsFile)
        {
            try
            {
                string[] list = Directory.GetDirectories(path);

                if (list == null) return;

                // Search for directories recursively
                foreach (string dirpath in list)
                {
                    if (Directory.Exists(dirpath))
                    {
                        walk(dirpath, outputFile, logsFile);
                    }
                }

                // Get all CSV files in that directory
                string[] fileList = Directory.GetFiles(path, "*.csv");
                foreach (string filepath in fileList)
                {
                    CSVParser parser = new CSVParser();

                    // Process each file's data, validate rows and store if row is valid else ignore
                    parser.validateAndCopy(filepath, outputFile, logsFile);
                }
            }
            catch (Exception exception)
            {
                logsFile.WriteLine("Error occured: " + exception.Message);
            }
        }

    }
}
