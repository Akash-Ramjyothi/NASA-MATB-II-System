using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.Globalization;

public class Graph : MonoBehaviour
{
    public DD_DataDiagram DD;

    private float timer;
    private GameObject line;
    private Vector2 point = new Vector2();

    public static List<float> workloadValues = new List<float>();
    public static bool active = true;
    //HttpWebRequest request;
    //HttpWebResponse response;
    //StreamReader reader;
    string resp;

    public const string url = "http://localhost:8080/taskbattery/workload";

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(active);
        line = DD.AddLine("EEG", Color.white);
        point.x = 0;
        point.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.2f)
        {
            addPoint();
            timer = 0;
        }
    }

    void addPoint()
    {
        //request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/taskbattery/workload");
        //response = (HttpWebResponse)request.GetResponse();
        //reader = new StreamReader(response.GetResponseStream());
        //resp = reader.ReadToEnd();

        WWW request = new WWW(url);
        StartCoroutine(OnResponse(request));
    }

    IEnumerator OnResponse(WWW req)
    {
        yield return req;

        Debug.Log(req.text);
        point.x += 0.01f;
        point.y = float.Parse(req.text, CultureInfo.InvariantCulture.NumberFormat);
        DD.InputPoint(line, point);
        workloadValues.Add(point.y);
    }
  
}
