using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour
{
	public GameObject loadingScreenObj;
	public GameObject subject;
	public GameObject session;
	public TextAsset textAsset;

	public Slider slider;

	private LoadJSON loadJSON = new LoadJSON();

	AsyncOperation async;

	int count = 4;
	public void loadingScene()
	{
		PlayerPrefs.SetString("SNAME", subject.GetComponent<Text>().text);
		PlayerPrefs.SetString("SESSION", session.GetComponent<Text>().text);

		string folderName = PlayerPrefs.GetString("SNAME");
		string subFolderName = PlayerPrefs.GetString("SESSION");
		string DirectoryName;

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

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/config/" + DirectoryName + "/eeg.csv");
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string resp = reader.ReadToEnd();
		Debug.Log(resp);
		if (resp == "DONE")
		{
			textAsset = Resources.Load("Config") as TextAsset;
			string json = textAsset.text;
			loadJSON.LoadData(loadJSON, json);
			//string json = JsonUtility.ToJson(loadJSON, true);
			//loadJSON.WriteToFile(json); //used to generate the sample file, comment the file read part in LoadJSON and uncomment the placeholder tasks and these lines(2).
			StartCoroutine(AsynchronousLoad("TasksScene"));
		}	
	}

	public void loadingSceneWithoutWorkload()
	{
		PlayerPrefs.SetString("SNAME", subject.GetComponent<Text>().text);
		PlayerPrefs.SetString("SESSION", session.GetComponent<Text>().text);
		Graph.active = false;
		textAsset = Resources.Load("Config") as TextAsset;
		string json = textAsset.text;
		loadJSON.LoadData(loadJSON, json);
		//string json = JsonUtility.ToJson(loadJSON, true);
		//loadJSON.WriteToFile(json); //used to generate the sample file, comment the file read part in LoadJSON and uncomment the placeholder tasks and these lines(2).
		StartCoroutine(AsynchronousLoad("TasksScene"));
	}

	public void toTlx()
	{
		count--;
		if( count == 0)
		{
			StartCoroutine(AsynchronousLoad("TLX"));
		}
	}

	public void toReport()
	{
		StartCoroutine(AsynchronousLoad("Report"));
	}

	IEnumerator AsynchronousLoad(string scene)
	{
		loadingScreenObj.SetActive(true);
		async = SceneManager.LoadSceneAsync(scene);
		async.allowSceneActivation = false; // this stops changing to next scene 

		while(async.isDone == false)
		{
			slider.value = async.progress;
			if(async.progress == 0.9f)
			{
				slider.value = 1f;
				Cursor.lockState = CursorLockMode.None;
				async.allowSceneActivation = true;// changed to true when scene is completely loaded 
			}
			yield return null;
		}


	}
}
