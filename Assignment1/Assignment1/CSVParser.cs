using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace assignmentdontnet
{
    class CSVParser
    {
        // Static variables to store valid and skipped rows count
        public static int VALID_LINES = 0;
        public static int INVALID_LINES = 0;
        private static Boolean COLUMNS = false;

        // Checks all the rows of a file and check if data is valid
        // If data is valid then store it in output file
        // If data is invalid then skip the row and put one log
        public void validateAndCopy(String fileName, StreamWriter output, StreamWriter logs)
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    string[] path = fileName.Split("\\");

                    string date = path[path.Length - 4] + "/" + path[path.Length - 3] + "/" + path[path.Length - 2];
                    string fileLoc = path[path.Length - 4] + "\\" + path[path.Length - 3] + "\\" + path[path.Length - 2] + "\\" + path[path.Length-1];
                    
                    // get columns of file
                    string[] columns;
                    if (!parser.EndOfData) columns = parser.ReadLine().Split(',');
                    else return;

                    // If program is processing first file then write columns first
                    if (!CSVParser.COLUMNS)
                    {
                        output.WriteLine(String.Join(",", columns)+",DATE");
                        CSVParser.COLUMNS = true;
                    }

                    // Check data in each row to be valid
                    while (!parser.EndOfData)
                    {
                        string temp = parser.ReadLine();
                        temp = temp.Replace("\"","");
                        string[] fields = temp.Split(',');

                        // if data contains extra commas OR have more columns than expected then skip the row
                        if (fields.Length != 10)
                        {
                            logs.WriteLine("File: " + fileLoc + " - data cannot be interpreted due to extra commas");
                            CSVParser.INVALID_LINES += 1;
                            continue;
                        }

                        // check if any required column is missing data
                        // REQUIRED COLUMNS: FIRST NAME, STREET NUMBER, STREET, CITY, PROVINCE, COUNTRY, EMAIL 
                        int flag = -1;
                        for (int i=0; i<fields.Length; i++)
                        {
                            if (i == 0 && fields[0] == "")
                            {
                                flag = 0;
                                break;
                            }
                            else if (i == 2 && fields[2] == "")
                            {
                                flag = 2;
                                break;
                            }
                            else if (i == 3 && fields[3] == "")
                            {
                                flag = 3;
                                break;
                            }
                            else if (i == 4 && fields[4] == "")
                            {
                                flag = 4;
                                break;
                            }
                            else if (i == 5 && fields[5] == "")
                            {
                                flag = 5;
                                break;
                            }
                            else if (i == 7 && fields[7] == "")
                            {
                                flag = 7;
                                break;
                            }
                            else if (i == 9 && fields[9] == "")
                            {
                                flag = 9;
                                break;
                            }
                        }

                        // if any column's data is missing then write a log 
                        if(flag>-1)
                        {
                            logs.WriteLine("File: " + fileLoc + " - (Skipping Row...) There is no data for required field: " + columns[flag]);
                            CSVParser.INVALID_LINES += 1;
                        }
                        // otherwise write the row in output file
                        else
                        {
                            output.WriteLine(String.Join(",", fields)+","+ date);
                            CSVParser.VALID_LINES += 1;
                        }
                        
                    }
                }

            }
            catch(Exception exception)
            {
                logs.WriteLine("ERROR occured - " + exception);
            }
    
        }

    }
}
