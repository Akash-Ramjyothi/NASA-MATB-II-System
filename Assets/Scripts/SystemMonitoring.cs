using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMonitoring : MonoBehaviour
{
    public bool automationState;
    public static List<List<int>> tasks = new List<List<int>>();
    public List<GameObject> top = new List<GameObject>();
    public List<GameObject> bars = new List<GameObject>();
    public List<bool> barsRandom = new List<bool>();
    private List<int> dirs = new List<int>() { 1 , 1 , 1 , 1 };

    public int count = -1;

    public Serializer serializer;
    public Loading loading;

    private float timer = 0f;

    public List<Color32> colors = new List<Color32>();

    public static List<int> score = new List<int> { 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        count = tasks.Count;
        // what // at time (seconds) // timeout //
        /*tasks.Add(new List<int> { 1, 0, 2 });
        tasks.Add(new List<int> { 2, 5, 2 });
        tasks.Add(new List<int> { 1, 5, 2 });
        tasks.Add(new List<int> { 2, 8, 2 });
        tasks.Add(new List<int> { 5, 8, 4 });
        tasks.Add(new List<int> { 1, 15, 2 });
        tasks.Add(new List<int> { 6, 15, 4 });
        tasks.Add(new List<int> { 4, 18, 4 });
        tasks.Add(new List<int> { 3, 20, 4 });*/
        int i = 0;
        foreach (List<int> task in tasks){
            //Debug.Log(task[0] + " " + task[1] + " " + task[2]);
            tasks[i].Add('0');
            if (task[0] <= 2)
            {
                StartCoroutine(runBtnTask(task[0], task[1], task[2], i));
            }
            else
            {
                StartCoroutine(runBarTask(task[0], task[1], task[2], i));
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f1"))
        {
            resetBar(0);
        }
        if (Input.GetKeyDown("f2"))
        {
            resetBar(1);
        }
        if (Input.GetKeyDown("f3"))
        {
            resetBar(2);
        }
        if (Input.GetKeyDown("f4"))
        {
            resetBar(3);
        }
        if (Input.GetKeyDown("f5"))
        {
            Color32 newCol = top[0].GetComponent<Image>().color;
            if (newCol.Equals(colors[4]))
            {
                top[0].GetComponent<Image>().color = colors[0];
            }
            else
            {
                top[0].GetComponent<Image>().color = colors[4];
            }
        }
        if (Input.GetKeyDown("f6"))
        {
            Color32 newCol = top[1].GetComponent<Image>().color;
            if (newCol.Equals(colors[1]))
            {
                top[1].GetComponent<Image>().color = colors[4];
            }
            else
            {
                top[1].GetComponent<Image>().color = colors[1];
            }
        }

        timer += Time.deltaTime;

        if(timer >= 1f/10f)
        {
            randomMotion();
            timer = 0;
        }
    }

    IEnumerator runBtnTask(int taskNum, int startTime, int timeout, int id)
    {
        yield return new WaitForSeconds(startTime);
        //Do the changes 
        if(taskNum == 1)
        {
            top[0].GetComponent<Image>().color = colors[4];
        }

        if (taskNum == 2)
        {
            top[1].GetComponent<Image>().color = colors[1];
        }

        yield return new WaitForSeconds(timeout);
        //Check if human interacted
        //Chenge back to normal if not

        if (taskNum == 1)
        {
            Color32 newCol = top[0].GetComponent<Image>().color;
            if (newCol.Equals(colors[4])) // Didn't fix automate
            {
                top[0].GetComponent<Image>().color = colors[0];
                tasks[id][3] = 0;
                serializer.SystemMonitoringAddRecord(tasks[id]);
            }
            else
            {
                tasks[id][3] = 1;
                serializer.SystemMonitoringAddRecord(tasks[id]);
                Debug.Log("User Fixed it 1");
            }
        }

        if (taskNum == 2)
        {
            Color32 newCol = top[1].GetComponent<Image>().color;
            if (newCol.Equals(colors[1])) // Didn't fix automate
            {
                top[1].GetComponent<Image>().color = colors[4];
                tasks[id][3] = 0;
                serializer.SystemMonitoringAddRecord(tasks[id]);
                score[1]++;
            }
            else
            {
                tasks[id][3] = 1;
                serializer.SystemMonitoringAddRecord(tasks[id]);
                Debug.Log("User Fixed it 2");
                score[0]++;
                score[1]++;
            }
        }

        count--;
        if (count == 0)
        {
            loading.toTlx();
        }
    }

    IEnumerator runBarTask(int taskNum, int startTime, int timeout, int id)
    {

        yield return new WaitForSeconds(startTime);
        //Do the changes 
        barsRandom[taskNum - 3] = false;

        yield return new WaitForSeconds(timeout);
        //Check if human interacted
        //Chenge back to normal if not

        if (barsRandom[taskNum - 3] == false)
        {
            barsRandom[taskNum - 3] = true;
            tasks[id][3] = 0;
            serializer.SystemMonitoringAddRecord(tasks[id]);
            score[1]++;
        }
        else
        {
            Debug.Log("User Fixed it (bars) " + (taskNum - 3));
            tasks[id][3] = 1;
            serializer.SystemMonitoringAddRecord(tasks[id]);
            score[0]++;
            score[1]++;
        }
        count--;
        if (count == 0)
        {
            loading.toTlx();
        }
    }

    void resetBar(int id)
    {
        barsRandom[id] = true;
    }

    void randomMotion()
    {
        for(int i=0; i<4; i++)
        {
            var val = bars[i].GetComponent<Slider>().value;
            if (barsRandom[i])
            {
                if (val >= 0.620) { dirs[i] = -1; }
                if (val <= 0.380) { dirs[i] = 1; }
            }
            bars[i].GetComponent<Slider>().value = val + (0.025f * dirs[i]);
        }
    }
}
