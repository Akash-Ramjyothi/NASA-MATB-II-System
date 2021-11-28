using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class LoadJSON 
{
    private static string configFile = "con";

    //System monitoring
    public List<IntListWrapper> System_Monitoring_Tasks = new List<IntListWrapper>();

    //Tracking
    public List<IntListWrapper> Tracking_Tasks = new List<IntListWrapper>();

    //Communications 
    public List<IntListWrapper> Communicaion_Tasks = new List<IntListWrapper>();

    //Resource Management
    public List<IntListWrapper> Resource_Management_Tasks = new List<IntListWrapper>();
    public List<IntListWrapper> Resource_Management_Tank_Capacity = new List<IntListWrapper>();
    public List<int> Resource_Management_Flow_Rate = new List<int>();
    public List<int> Resource_Management_Tank_Consumption = new List<int>();

    public void LoadData(LoadJSON loadJSON, string json)
    {
        loadJSON = new LoadJSON();
        JsonUtility.FromJsonOverwrite(json, loadJSON);

        /*
        //FOR TESTS
        placeHolderTasks();
        */

        //Done now forward the data
        loadJSON.passData();
    }

    public void passData()
    {
        //System Monitoring 
        foreach (IntListWrapper sysMon in System_Monitoring_Tasks)
        {
            SystemMonitoring.tasks.Add(sysMon.list);
        }

        //Tracking
        foreach(IntListWrapper TR in Tracking_Tasks)
        {
            Tracking.tasks.Add(TR.list);
        }

        //Communications 
        foreach(IntListWrapper COM in Communicaion_Tasks)
        {
            CommunicationsTask.tasks.Add(COM.list);
        }

        //Resource Management
        foreach(IntListWrapper ResMan in Resource_Management_Tasks)
        {
            ResourceManagement.tasks.Add(ResMan.list);
        }

        foreach (IntListWrapper ResMan in Resource_Management_Tank_Capacity)
        {
            ResourceManagement.tankCapacity.Add(ResMan.list);
        }

        foreach(int ResMan in Resource_Management_Flow_Rate)
        {
            ResourceManagement.flowRates.Add(ResMan);
        }
        ResourceManagement.flowRates.Add(Resource_Management_Tank_Consumption[0]);
        ResourceManagement.flowRates.Add(Resource_Management_Tank_Consumption[1]);

    }

    public void WriteToFile(string json)
    {
        string path = GetFilePath(configFile);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        Debug.Log(path);
        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private string ReadFromFile()
    {
        string path = GetFilePath(configFile);
        Debug.Log(path);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var fileContents = reader.ReadToEnd();
                return fileContents;
            }
        }
        else
        {
            Debug.Log("404 not found");
            return "";
        }
    }

    private void placeHolderTasks()
    {
        //System monitoring
        // what // at time (seconds) // timeout //
        System_Monitoring_Tasks.Add(new IntListWrapper { 1, 0, 2 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 2, 5, 2 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 1, 5, 2 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 2, 8, 2 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 5, 8, 4 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 1, 15, 2 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 6, 15, 4 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 4, 18, 4 });
        System_Monitoring_Tasks.Add(new IntListWrapper { 3, 20, 4 });

        //Tracking
        // at time (seconds) // timeout //
        Tracking_Tasks.Add(new IntListWrapper { 0, 4 });
        Tracking_Tasks.Add(new IntListWrapper { 7, 4 });
        Tracking_Tasks.Add(new IntListWrapper { 14, 4 });
        Tracking_Tasks.Add(new IntListWrapper { 21, 4 });

        //Communications
        // what channel ( 0, 1, 2, 3, 4{distractor})// what frequency*100 // at time (seconds) // timeout //
        Communicaion_Tasks.Add(new IntListWrapper { 0, 126500, 0, 6 });
        Communicaion_Tasks.Add(new IntListWrapper { 1, 121250, 7, 6 });
        Communicaion_Tasks.Add(new IntListWrapper { 2, 121100, 14, 6 });
        Communicaion_Tasks.Add(new IntListWrapper { 3, 120500, 21, 6 });
        Communicaion_Tasks.Add(new IntListWrapper { 4, 127500, 28, 6 });

        //Resource Management 
        Resource_Management_Flow_Rate = new List<int> { 800, 600, 800, 600, 600, 600, 400, 400 };
        Resource_Management_Tank_Consumption = new List<int> { 800, 800 };

        Resource_Management_Tank_Capacity.Add(new IntListWrapper { 4000, 2500 });
        Resource_Management_Tank_Capacity.Add(new IntListWrapper { 4000, 2500 });
        Resource_Management_Tank_Capacity.Add(new IntListWrapper { 2000, 1000 });
        Resource_Management_Tank_Capacity.Add(new IntListWrapper { 2000, 1000 });

        // what channel pump // at time (seconds) // timeout //
        Resource_Management_Tasks.Add(new IntListWrapper { 0, 0, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 1, 7, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 2, 14, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 3, 21, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 4, 28, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 5, 28, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 6, 28, 4 });
        Resource_Management_Tasks.Add(new IntListWrapper { 7, 28, 4 });
    } 
}

[Serializable]
public class ListWrapper<T>: IEnumerable<T>
{
    public List<T> list;

    public ListWrapper()
    {
        list = new List<T>();
    }
    public ListWrapper(IEnumerable<T> collection)
    {
        list = new List<T>(collection);
    }
    public ListWrapper(int capacity)
    {
        list = new List<T>(capacity);
    }

    public void Add(T item)
    {
        list.Add(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}


[Serializable]
public class IntListWrapper : ListWrapper<int> { }