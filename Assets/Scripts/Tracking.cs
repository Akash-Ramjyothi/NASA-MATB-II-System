using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tracking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crosshair;
    public GameObject background; //might not need, will remove later.
    public GameObject button;
    public List<Color32> colors;

    public float mouseSensitivity = 100f;
    float xMovement, yMovement;
    float xTarget = 1;
    float yTarget = 1;

    public int count = -1;

    public Serializer serializer;
    public Loading loading;

    public static List<List<int>> tasks = new List<List<int>>();

    public static List<int> score = new List<int> { 0, 0 };

    void Start()
    {
        count = tasks.Count;
        // at time (seconds) // timeout //
        /*tasks.Add(new List<int> { 0, 4 });
        tasks.Add(new List<int> { 7, 4 });
        tasks.Add(new List<int> { 14, 4 });
        tasks.Add(new List<int> { 21, 4 });*/

        int i = 0;
        foreach (List<int> task in tasks)
        {
            tasks[i].Add('0');
            StartCoroutine(schedule(task[0], task[1], i));
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<Image>().color == colors[0])
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xMovement += mouseX;
            yMovement += mouseY;

            xMovement = Mathf.Clamp(xMovement, -100f, 100f);
            yMovement = Mathf.Clamp(yMovement, -100f, 100f);

            crosshair.GetComponent<RectTransform>().localPosition = new Vector3(xMovement, yMovement, 0f);
        }
        else if (button.GetComponent<Image>().color == colors[1])
        {
            float mouseX = Random.Range(-2.0f, 2.0f) * mouseSensitivity / 2 * Time.deltaTime;
            float mouseY = Random.Range(-2.0f, 2.0f) * mouseSensitivity / 2 * Time.deltaTime;
            xMovement += mouseX;
            yMovement += mouseY;

            xMovement = Mathf.Clamp(xMovement, -25f, 25f);
            yMovement = Mathf.Clamp(yMovement, -25f, 25f);

            crosshair.GetComponent<RectTransform>().localPosition = new Vector3(xMovement, yMovement, 0f);
        }
        else
        {
            float mouseX = xTarget * mouseSensitivity / 2 * Time.deltaTime;
            float mouseY = yTarget * mouseSensitivity / 2 * Time.deltaTime;
            xMovement += mouseX;
            yMovement += mouseY;

            xMovement = Mathf.Clamp(xMovement, -100f, 100f);
            yMovement = Mathf.Clamp(yMovement, -100f, 100f);

            crosshair.GetComponent<RectTransform>().localPosition = new Vector3(xMovement, yMovement, 0f);
        }

        if (Input.GetKeyDown("f10"))
        {
            if (button.GetComponent<Image>().color == colors[0])
            {
                Vector3 loc = crosshair.GetComponent<RectTransform>().localPosition;
                if (loc.x >= -25f && loc.x <= 25f && loc.y >= -25f && loc.y <= 25f)
                {
                    button.GetComponent<Image>().color = colors[1];
                }
                else
                {
                    button.GetComponent<Image>().color = colors[2];
                }
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                button.GetComponent<Image>().color = colors[0];
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }

    IEnumerator schedule(int startTime, int timeout, int id)
    {
        yield return new WaitForSeconds(startTime);
        if (button.GetComponent<Image>().color == colors[1])
        {
            button.GetComponent<Image>().color = colors[2];
        }
        int x = Random.Range(0, 2);
        int y = Random.Range(0, 2);
        if (x == 1)
        {
            xTarget = 1;
        }
        else
        {
            xTarget = -1;
        }
        if (y == 1)
        {
            yTarget = 1;
        }
        else
        {
            yTarget = -1;
        }

        yield return new WaitForSeconds(timeout);

        Vector3 loc = crosshair.GetComponent<RectTransform>().localPosition;
        if (loc.x >= -25f && loc.x <= 25f && loc.y >= -25f && loc.y <= 25f)
        {
            //user fixed it.
            tasks[id][2] = 1;
            serializer.TrackingTaskAddReord(tasks[id]);
            score[0]++;
            score[1]++;
        }
        else
        {
            tasks[id][2] = 0;
            serializer.TrackingTaskAddReord(tasks[id]);
            score[1]++;
        }
        count--;
        if (count == 0)
        {
            loading.toTlx();
        }
    }
}
