using System.Collections;
using System.Collections.Generic;
using System.Linq; //For Toggle Group https://www.youtube.com/watch?v=0b6KmdPcDQU
using UnityEngine;
using UnityEngine.UI;

public class CommunicationsTask : MonoBehaviour
{
    ToggleGroup toggleGroupInstance;
    public Button submitButton;
    public GameObject changedText; 
    public List<GameObject> comLabels;
    public List<GameObject> CallSign;
    public List<Color32> Colors;
    public static List<List<int>> tasks = new List<List<int>>();

    public int count = -1;

    public Serializer serializer;
    public Loading loading;

    public static List<int> score = new List<int> { 0, 0 };

    public Toggle currentSelection
    {
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); } //Returns the Active toggle 
    }

    public void submitButtonClick()
    {
        string numberStr = currentSelection.name;
        int number;
        bool isParsable = int.TryParse(numberStr, out number);
        string callSignChannel = CallSign[1].GetComponent<Text>().text;
        string callSignFrequency = CallSign[2].GetComponent<Text>().text;
        string value = changedText.GetComponent<Text>().text;

        if (isParsable) //false if error
        {
            comLabels[number].GetComponent<Text>().text = value;
        }
        else
        {
            Debug.Log("ERROR: make sure the name of toggles are an integer value.");
        }
        switch (callSignChannel)
        {
            case "NAV 1":
                if(number == 0)
                { checkSubmission(callSignChannel, callSignFrequency, value); }
                break;
            case "NAV 2":
                if (number == 1)
                { checkSubmission(callSignChannel, callSignFrequency, value); }
                break;
            case "COM 1":
                if (number == 2)
                { checkSubmission(callSignChannel, callSignFrequency, value); }
                break;
            case "COM 2":
                if (number == 3)
                { checkSubmission(callSignChannel, callSignFrequency, value); }
                break;
            default: 
                Debug.Log("Distractor");
                break ;
        }
    }

    private void checkSubmission(string channel, string frequency, string answer) {
        if (frequency == answer)
        {
            normalizeAlert();
        }
        else
        {
            //wrong answer
            CallSign[0].GetComponent<Image>().color = Colors[0];
        }
    }

    private void normalizeAlert()
    {
        CallSign[0].GetComponent<Image>().color = Colors[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        count = tasks.Count;
        toggleGroupInstance = GetComponent<ToggleGroup>();
        submitButton.onClick.AddListener(submitButtonClick);
        
        // what channel ( 0, 1, 2, 3, 4{distractor})// what frequency*100 // at time (seconds) // timeout //
        /*tasks.Add(new List<int> { 0, 126500, 0, 6 });
        tasks.Add(new List<int> { 1, 121250, 7, 6 });
        tasks.Add(new List<int> { 2, 121100, 14, 6 });
        tasks.Add(new List<int> { 3, 120500, 21, 6 });
        tasks.Add(new List<int> { 4, 127500, 28, 6 });*/

        //Start Coroutines 
        int i = 0;
        foreach (List<int> task in tasks)
        {
            tasks[i].Add('0');
            StartCoroutine(runCommnTask(task[0], task[1], task[2], task[3], i));
            i++;
        }
    }

    IEnumerator runCommnTask(int num,int freq, int startTime, int timeout, int id)
    {
        yield return new WaitForSeconds(startTime);
        string channelName;
        string frequency = freq.ToString();
        frequency = frequency.Insert(3, ".");
        switch (num) //Switch > IF ELSE (for this case), tabs > Spaces (always)
        {
            case 0:
                channelName = "NAV 1";
                break;
            case 1:
                channelName = "NAV 2";
                break;
            case 2:
                channelName = "COM 1";
                break;
            case 3:
                channelName = "COM 2";
                break;
            case 4:
                channelName = "RES 1";
                break;
            default:
                channelName = "RES 4";
                Debug.Log("ERROR: Wrong instruction.");
                break;
        }

        CallSign[1].GetComponent<Text>().text = channelName;
        CallSign[2].GetComponent<Text>().text = frequency;
        CallSign[0].GetComponent<Image>().color = Colors[0];

        yield return new WaitForSeconds(timeout);
        if(CallSign[0].GetComponent<Image>().color == Colors[1])
        {
            //User Fixed it
            Debug.Log("User Fixed it communication: " + channelName + "   " + frequency);
            tasks[id][4] = 1;
            serializer.CommunicationsTaskAddRecord(tasks[id]);
            score[0]++;
            score[1]++;
        }
        else
        {
            //computer fixes it
            CallSign[0].GetComponent<Image>().color = Colors[1];
            if(tasks[id][0] == 4)
            {
                tasks[id][4] = 2;
                serializer.CommunicationsTaskAddRecord(tasks[id]);
            }
            else
            {
                tasks[id][4] = 0;
                serializer.CommunicationsTaskAddRecord(tasks[id]);
                score[1]++;
            }
        }
        count--;
        if(count == 0)
        {
            loading.toTlx();
        }
    }
}
