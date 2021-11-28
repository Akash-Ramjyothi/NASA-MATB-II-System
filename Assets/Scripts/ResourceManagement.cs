using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManagement : MonoBehaviour
{
    public List<GameObject> pumpsFlows;
    public List<GameObject> tanks;
    public List<Button> buttons;
    public List<GameObject> tankLabel;
    public List<Color32> colors = new List<Color32>();

    public int count = -1;

    public Serializer serializer;
    public Loading loading;

    private float timer = 0f;

    public static List<List<int>> pumps = new List<List<int>>
    {   // From // To // active // flowrate
        new List<int> { 2, 0 , 0 , 800}, 
        new List<int> { 42, 0 , 0 , 600},
        new List<int> { 3, 1 , 0 , 800},
        new List<int> { 42, 1 , 0 , 600},
        new List<int> { 42, 2 , 0 , 600},
        new List<int> { 42, 3 , 0 , 600},
        new List<int> { 0, 1 , 0 , 400},
        new List<int> { 1, 0 , 0 , 400},
        new List<int> { 0, 42 , 1 , 800},
        new List<int> { 1, 42 , 1 , 800},
    };

    public static List<int> flowRates = new List<int>();
    public static List<List<int>> tasks = new List<List<int>>();
    public static List<List<int>> tankCapacity = new List<List<int>>(); // max capacity // current capacity // Consumption // 

    public static List<float> initialCapacity = new List<float> { 0f, 0f };

    // Start is called before the first frame update
    void Start()
    {
        count = tasks.Count;
        /*flowRates = new List<int> { 800, 600, 800, 600, 600, 600, 400, 400 };
        tankConsumption = new List<int> { 800, 800 };

        tankCapacity.Add(new List<int> { 4000, 2500});
        tankCapacity.Add(new List<int> { 4000, 2500});
        tankCapacity.Add(new List<int> { 2000, 1000});
        tankCapacity.Add(new List<int> { 2000, 1000});

        // what channel pump // at time (seconds) // timeout //
        tasks.Add(new List<int> { 0, 0, 4 });
        tasks.Add(new List<int> { 1, 7, 4 });
        tasks.Add(new List<int> { 2, 14, 4 });
        tasks.Add(new List<int> { 3, 21, 4 });
        tasks.Add(new List<int> { 4, 28, 4 });
        tasks.Add(new List<int> { 5, 28, 4 });
        tasks.Add(new List<int> { 6, 28, 4 });
        tasks.Add(new List<int> { 7, 28, 4 });*/


        for (int z = 0; z < pumps.Count; z++)
        {
            pumps[z][3] = flowRates[z]; 
        }

        buttons[0].onClick.AddListener(button0click);
        buttons[1].onClick.AddListener(button1click);
        buttons[2].onClick.AddListener(button2click);
        buttons[3].onClick.AddListener(button3click);
        buttons[4].onClick.AddListener(button4click);
        buttons[5].onClick.AddListener(button5click);
        buttons[6].onClick.AddListener(button6click);
        buttons[7].onClick.AddListener(button7click);

        int i = 0;
        foreach (List<int> task in tasks)
        {
            tasks[i].Add('0');
            StartCoroutine(disablePumpSchedule(task[0], task[1], task[2], i));
            i++;
        }

        initialCapacity[0] = tankCapacity[0][1];
        initialCapacity[1] = tankCapacity[1][1];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f / 2f)
        {
            changeTankValues();
            timer = 0;
        }
    }

    private void changeTankValues()
    {
        for(int i = 0; i < 10; i++)
        {
            if (pumps[i][2] == 1)
            {
                int temp = pumps[i][3] / 120;
                if(pumps[i][1] == 42)
                {
                    if (tankCapacity[pumps[i][0]][1] > temp) //checks if other tank has enough units
                    {
                        addAndSubtractfromTank(pumps[i][1], pumps[i][0], temp);
                    }
                    else
                    {
                        //Tank Empty
                    }
                }
                else
                {
                    if ((tankCapacity[pumps[i][1]][1] + temp) < tankCapacity[pumps[i][1]][0]) //checks max 
                    {
                        if (pumps[i][0] == 42) //infinite pump
                        {
                            addAndSubtractfromTank(pumps[i][1], pumps[i][0], temp);
                        }
                        else
                        {
                            if (tankCapacity[pumps[i][0]][1] > temp) //checks if other tank has enough units
                            {
                                addAndSubtractfromTank(pumps[i][1], pumps[i][0], temp);
                            }
                            else
                            {
                                closePump(i);
                            }
                        }
                    }
                    else
                    {
                        closePump(i);
                    }
                }
            }
        }
        serializer.ResourceManagementAddRecord();
    }

    void closePump(int number)
    {
        //Debug.Log("CLOSE PUMP CALLED"); 
        pumps[number][2] = 0;

        //change color & Flow label below
        buttons[number].GetComponent<Image>().color = colors[2];
        pumpsFlows[number].GetComponent<Text>().text = "0";
    }

    void addAndSubtractfromTank(int toTank, int fromTank, int value)
    {
        //if 42 -> infinite tank i.e. just add
        if(fromTank != 42)
        {
            tankCapacity[fromTank][1] -= value;
            
            tanks[fromTank].GetComponent<Slider>().value = (float)tankCapacity[fromTank][1] / (float)tankCapacity[fromTank][0];
            tankLabel[fromTank].GetComponent<Text>().text = tankCapacity[fromTank][1].ToString();
            //Debug.Log(tankCapacity[fromTank][1]);
        }
        if(toTank != 42)
        {
            tankCapacity[toTank][1] += value;

            tanks[toTank].GetComponent<Slider>().value = (float)tankCapacity[toTank][1] / (float)tankCapacity[toTank][0];
            tankLabel[toTank].GetComponent<Text>().text = tankCapacity[toTank][1].ToString();
            //Debug.Log(tankCapacity[toTank][1]);
        }
    }

    void depricated()
        {
        //
        //private List<bool> activePump = new List<bool> { false, false, false, false, false, false, false, false };
        //List<List<List<int>>> connections = new List<List<List<int>>>
        //  { //inward // outward //
        //      new List<List<int>> {
        //          new List<int> { 0, 1, 7 },
        //          new List<int> { 6 }
        //          }, //Tank A
        //      new List<List<int>> {
        //          new List<int> { 2, 3, 6 },
        //          new List<int> { 7 }
        //          }, //Tank B
        //      new List<List<int>> {
        //          new List<int> { 4 },
        //          new List<int> { 0, 1 }
        //          },    //Tank C
        //      new List<List<int>> {
        //          new List<int> { 5 },
        //          new List<int> { 2, 3 }
        //          }     //Tank D
        //  };

        //this function will calculate the new tank values, every 10 seconds.
        //PS i know my algos are a bit on the complex side.... Dont change them else you might spend your day in debugging.

        //For Tank A
        //// int Tank1 = ((flowRates[0] * activePump[0]) + (flowRates[1] * activePump[1]) + (flowRates[7] * activePump[7]) - (flowRates[6] * activePump[6]));
        //// int Tank2 = ((flowRates[2] * activePump[2]) + (flowRates[3] * activePump[3]) + (flowRates[6] * activePump[6]) - (flowRates[7] * activePump[7]));
        //// int Tank3 = ((flowRates[4] * activePump[4]) - (flowRates[0] * activePump[0]) - (flowRates[1] * activePump[1]));
        //// int Tank4 = ((flowRates[5] * activePump[5]) - (flowRates[2] * activePump[2]) - (flowRates[3] * activePump[3]));

        /*for(int i = 0; i < 4; i++) 
        {
            bool sign = true;
            int total = 0;
            foreach(List<int> directions in connections[i])
            {
                int TempSum = 0;
                foreach (int pumpnum in directions) 
                {
                    if (sign)
                    {
                        if((flowRates[pumpnum] * activePump[pumpnum] / 600) > )
                        {

                        }
                    }


                    total += flowRates[pumpnum] * activePump[pumpnum] ;
                }
                total += TempSum;
                sign = false;
            }
        }*/

        //foreach(List<int> pump in pumps)
        //{
        //  if (pump[2] == 1)
        // {

        // }
        //}

        //TANK A

        // if (activePump[0])
        // {
        //     int temp = flowRates[0] / 600; //Pump 7 (6) connects TANK A (0) and C (2)
        //     if (temp < tankCapacity[2][1]) //Tank C -- Limited can exhaust.. checks if it has exhausted.
        //     {
        //         sum += temp;
        //         addAndSubtractfromTank(0, 2, temp);
        //     }
        //     else { closePump(0); }
        // }

        //temp = flowRates[1] * activePump[1] / 600; //Tank D -- Unlimited. //Pump 2 (1) connects TANK A (0) and  infinite tank
        //addAndSubtractfromTank(0, 99, temp);
        //sum += temp;

        //temp = flowRates[7] * activePump[7] / 600; //Pump 8 (7) connects TANK A (0) and B (1)
        //if (temp < tankCapacity[1][1]) //Tank B -- Limited
        //{
        //   sum += temp;
        //    addAndSubtractfromTank(0, 1, temp);
        //}
        //else { closePump(1); }

        //temp = flowRates[6] * activePump[6] / 600; //Pump 7 (6) connects TANK A (0) and B (1)
        //if (temp < tankCapacity[0][1])
        //{
        //    sum -= temp; //Subtract if resources going out of tank 
        //    addAndSubtractfromTank(1, 0, temp);
        //} 
        //else { closePump(0); }
    }

    void pumpTrigger(int number)
    {
        //change active status, change color, change flow label

        if(pumps[number][2] == 0)
        {
            pumps[number][2] = 1;
            buttons[number].GetComponent<Image>().color = colors[1];
            pumpsFlows[number].GetComponent<Text>().text = pumps[number][3].ToString();
        }
        else if (pumps[number][2] == 1)
        {
            pumps[number][2] = 0;
            buttons[number].GetComponent<Image>().color = colors[2];
            pumpsFlows[number].GetComponent<Text>().text = "0";
        }
    }

    void button0click()
    {
        pumpTrigger(0);
    }
    void button1click()
    {
        pumpTrigger(1);
    }
    void button2click()
    {
        pumpTrigger(2);
    }
    void button3click()
    {
        pumpTrigger(3);
    }
    void button4click()
    {
        pumpTrigger(4);
    }
    void button5click()
    {
        pumpTrigger(5);
    }
    void button6click()
    {
        pumpTrigger(6);
    }
    void button7click()
    {
        pumpTrigger(7);
    }

    IEnumerator disablePumpSchedule(int number, int startTime, int timeout, int id)
    {
        yield return new WaitForSeconds(startTime);
        pumps[number][2] = 2;
        buttons[number].GetComponent<Image>().color = colors[0];
        pumpsFlows[number].GetComponent<Text>().text = "0";

        yield return new WaitForSeconds(timeout);
        pumps[number][2] = 0;
        buttons[number].GetComponent<Image>().color = colors[2];
        pumpsFlows[number].GetComponent<Text>().text = "0";

        count--;
        if (count == 0)
        {
            loading.toTlx();
        }
    }
}
