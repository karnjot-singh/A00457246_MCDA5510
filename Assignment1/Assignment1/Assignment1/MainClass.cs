using System;
using System.Diagnostics;
using System.IO;

namespace assignmentdontnet
{
    class MainClass
    {
        // This function uses DirWalker to traverse directories and CSVParser to parse, fetch and process data
        public void startApplication(string targetDirectory)
        {
            Stopwatch watch = Stopwatch.StartNew();

            DirWalker walker = new DirWalker();

            // Directories paths for output and logs data
            string path = Directory.GetCurrentDirectory();
            string outputPath = path + @"\..\..\..\..\output\output.csv";
            string logsPath = path + @"\..\..\..\..\logs\logs.txt";
            
            StreamWriter outputFile = walker.openFile(outputPath);
            StreamWriter logsFile = walker.openFile(logsPath);

            try
            {

                if (logsFile is null)
                {
                    return;
                }

                if (outputFile is null)
                {
                    return;
                }

                // Walk through directories and process data
                walker.walk(targetDirectory, outputFile, logsFile);

                // Store data about skipped and valid rows
                logsFile.WriteLine("VALID ROWS COUNT: " + CSVParser.VALID_LINES);
                logsFile.WriteLine("SKIPPED ROWS COUNT: " + CSVParser.INVALID_LINES);

                watch.Stop();

                var executionTime = watch.ElapsedMilliseconds;
                // Total execution time took by application
                logsFile.WriteLine("EXECUTION TIME IN MILLISECONDS: " + executionTime);
            }
            catch (Exception exception)
            {
                Console.WriteLine("An unexpected error occured with message: " + exception.Message);
            }
            finally
            {
                // Close opened files
                walker.closeFile(outputFile);
                walker.closeFile(logsFile);
            }
        }

        // Main function to start application
        static void Main(String[] args)
        {
            // Directory path where data files are stored
            string targetDirectory = @"C:\Users\karnj\Downloads\Sample Data\Sample Data";

            // start application
            new MainClass().startApplication(targetDirectory);
        }
    }
}
