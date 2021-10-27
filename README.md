# Assignment 1
 My name is **Karnjot Singh** and my student Id is **A00457246**.
 
 ## What is the purpose this assignment?
 The goal of this assignment is to understand basics of C# and Visual studio. In this assignment, I have worked on concepts like directory traversing, file I/O, OOPS and exceptions to implement a small application which traverse through a target directory, look for csv file under this directory, validate the rows of csv files and then store all the valid rows in a single file.
 
 ## Working of this application
 - Basically this application reads files under given directory, filter for csv files.
 - In each csv file, it reads every record and check if all required columns have data present. If row is valid then it is stored in output.csv file otherwise it is skipped.
 - Required columns are - First Name, Street Number, Street, City, Province, Country, Email
 - Optional columns are - Last Name, Postal Code, Phone Number
 - For each skipped row, a log is stored in logs.txt file describing the reason behind skipping.
 - Count of valid rows and skipped rows is also stored in logs.txt file along with total time took by program to check all the files.
 
 ## Validations on each row
 - Each row should have data present for required columns.
 - If there is extra comma(s) then row is ignored because it cause issues to understand which comma is column separator and which one is part of data.
 - More check can be implemented on columns such as whether email address or phone number is valid or not. (Not implemented in this assignment)
 
 ## Assumptions
 - It is assumed that there are two directories which are output and logs present alongside application's directory.
 - Path of target directory is specified in MainClass.cs.
 - All csv files share the same column structure.
