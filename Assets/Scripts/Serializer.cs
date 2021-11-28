using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Serializer : MonoBehaviour
{
    public string DirectoryName;

    void Start()
    {
        string folderName = PlayerPrefs.GetString("SNAME");
        string subFolderName = PlayerPrefs.GetString("SESSION");

        DirectoryName = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(DirectoryName))
        {
            Directory.CreateDirectory(DirectoryName);
        }
        DirectoryName = Path.Combine(DirectoryName, subFolderName);
        if (!Directory.Exists(DirectoryName))
        {
            Directory.CreateDirectory(DirectoryName);
        }  
    }

    public void SystemMonitoringAddRecord(List<int> task)
    {
        string FilePath = Path.Combine(DirectoryName, "SystemMonitoring.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date Time,Event,Start Time,Timeout,Status"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z") + ",";

        switch (task[0])
        {
            case 1: ProcessedLine += "F5";  break;
            case 2: ProcessedLine += "F6"; break;
            case 3: ProcessedLine += "F1"; break;
            case 4: ProcessedLine += "F2"; break;
            case 5: ProcessedLine += "F3"; break;
            case 6: ProcessedLine += "F4"; break;
            default: ProcessedLine += "Wrong input"; break;
        }
        ProcessedLine += "," + task[1].ToString() + "," + task[2].ToString() + "," + task[3].ToString();

        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch(System.Exception ex)
        {
            throw new System.ApplicationException("Exception: "  + ex);
        }
    }

    public void CommunicationsTaskAddRecord(List<int> task)
    {
        string FilePath = Path.Combine(DirectoryName, "CommunicationsTask.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date Time,Event,Frequency,Start Time,Timeout,Status"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z") + ",";

        switch (task[0])
        {
            case 0: ProcessedLine += "NAV 1"; break;
            case 1: ProcessedLine += "NAV 2"; break;
            case 2: ProcessedLine += "COM 1"; break;
            case 3: ProcessedLine += "COM 2"; break;
            case 4: ProcessedLine += "Distractor"; break;
            default: ProcessedLine += "Wrong input"; break;
        }
        ProcessedLine += "," + task[1].ToString() + "," + task[2].ToString() + "," + task[3].ToString() + "," + task[4].ToString();

        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.ApplicationException("Exception: " + ex);
        }
    }
    
    public void TrackingTaskAddReord(List<int> task)
    {
        string FilePath = Path.Combine(DirectoryName, "TrackingTask.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date Time,Start Time,Timeout,Status"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z");
        ProcessedLine += "," + task[0].ToString() + "," + task[1].ToString() + "," + task[2].ToString();

        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.ApplicationException("Exception: " + ex);
        }
    }

    public void ResourceManagementAddRecord()
    {
        string FilePath = Path.Combine(DirectoryName, "ResourceManagementTask.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date Time,Tank A,Tank B,Tank C,Tank D,Pump 1,Pump 2,Pump 3,Pump 4,Pump 5,Pump 6,Pump 7,Pump 8"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z");
        foreach (List<int> tank in ResourceManagement.tankCapacity)
        {
            ProcessedLine += ",";
            ProcessedLine += tank[1].ToString();
        }
        for (int i = 0; i<8; i++)
        {
            ProcessedLine += ",";
            ProcessedLine += ResourceManagement.pumps[i][2].ToString();
        }
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.ApplicationException("Exception: " + ex);
        }
    }

    public void submitForm(List<float> values)
    {
        string FilePath = Path.Combine(DirectoryName, "Questionnaire.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date & Time,Mental Demand,Physical Demand,Temporal Demand,Performance,Effort,Frustration"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z");
        foreach (float value in values)
        {
            ProcessedLine += ",";
            ProcessedLine += value.ToString();
        }
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.ApplicationException("Exception: " + ex);
        }
    }

    public void saveReport(List<int> values)
    {
        string FilePath = Path.Combine(DirectoryName, "Report.csv");
        if (!File.Exists(FilePath)) // Runs for creating the CSV file
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
                {
                    file.WriteLine("Date & Time,System Monitoring,Tracking,Communications,Resource Management, mean workload"); //Adds the heading on file creation
                }
            }
            catch (System.Exception ex)
            {
                throw new System.ApplicationException("Exception: " + ex);
            }
        }

        string ProcessedLine = DateTime.Now.ToString("yyyy-MM-dd\\THH-mm-ss\\Z");
        foreach (int value in values)
        {
            ProcessedLine += ",";
            ProcessedLine += value.ToString();
        }
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FilePath, true))
            {
                file.WriteLine(ProcessedLine);
            }
        }
        catch (System.Exception ex)
        {
            throw new System.ApplicationException("Exception: " + ex);
        }
    }
}
