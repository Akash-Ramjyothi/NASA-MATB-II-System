<h1 align="center">NASA MATB-II System</h1>

<p align="center">
<img src="https://user-images.githubusercontent.com/54114888/143779609-5ed943f0-29cb-400c-94a6-d737f8f7ed98.png" width="200" height="">
</p>

## 📜 Description:
Designed, developed and implemented various improvements for the existing **NASA MATB-II** interface software (Multi-Attribute Task Battery) and demonstrated in a real-time scenario. The Multi-Attribute Task Battery (MATB-II) is a computer-based task designed to evaluate operator performance and workload. MATB provides a benchmark set of tasks and analogous to activities that aircraft crew-members perform in flight, with freedom to use by non-pilot subjects. The MATB requires the simultaneous performance of monitoring, dynamic resource management, and tracking tasks. The simultaneous performance of multiple tasks is a central feature of the MATB and it is this feature that is consistent with most operational systems and thus makes the task useful for our purposes as a research platform.
   
**Developed the project as a part of my Internship at DRDO (Defence Research and Development Organisation)'s Delhi headquarters.**

## 📽 Sample Demo:
https://user-images.githubusercontent.com/54114888/143836275-a3f25a1a-3bbe-48fa-bb54-ce2e057bc15c.mp4
   
## ℹ️ About Project:
### Task Battery
Project contins a NASA MATB like interface with 4 tasks:
1. System Monitoring Task
2. Tracking Task
3. Communications Task
4. Resource Management Task

The project also calculates live Workload using 14 channel EEG data and a workload model.

## 🧪 Steps to Install:
1. Make sure LSL is properly installed also please Allow any network or file access permission any of the files ask thoughout the setup or runtime. 
2. Create a pyhton 3 virtual environment using the command python3 -m venv env
3. Activate the virtual environment by env\Scripts\activate
4. install the requirements.txt file inside the Assets\Python by using pip install -r path_to_file\requirements.txt
5. Make sure Tensorflow is properly installed, also check for cuda libraries for better performance. 
6. Modify the eeg data stream (if required) at Assets\Python\WorkloadServer.py line 16 & Line 22.
7. That's it, Setup complete.
   
## 🧪 Steps to Run:
1. Activate the virtual environment you created
2. If testing without LSL stream (Skip this step is LSL is available) run the sendData.py file present in the Assets\Python folder, this file will simulate 14 channel eeg data in the LSL stream, keep it running in the background throughout the runtime.
3. Run the WorkloadServer.py stored in Assets\Python folder using Python path_to_file\WorkloadServer.py
4. wait for it to start, you can test the wotking by going to localhost:8080/taskbattery/workload on chrome and it'll return a float falue if everything is working fine.
5. Run the unity task. 
6. In case of any situation if Unity was quitted forcefully and have to reopen, please restart the server as well, Server can handle if the Task was closed by clicking the Exit button in the task but for any other way, you will require to restart the server as well.
7. Data will be stored at C:/Users/< Username >/AppData/LocalLow/INMAS-DRDO/Task Battery/

### ➕ Additional features implemented:
1. The grapth view can show multiple graphs if needed in future for plotting other data as well, some modifications to Graph.cs will be required. check Assets\DataDiagram\Documentation
2. Unity and Server codebase is independednt of number of EEG channels, If you want to change the model, you can simply replace the model.h5 file present in the Assets\Python folder and it'll work fine even if it is of different channel eeg, The only limitation is the input value, current model acceprts data in [[channel list],[channel list], ...] if the other model accepts data in some other form, you will be required to change the calculateWorkload function present in WorkloadServer.py file. 
3. Unity Code can run both with and wwithout the Server, but server is required to connect to lsl, calculate workload and to store EEG data for future training. 
4. Possibility to be used in the Dynamic automation allocation as suggested by Vishal sir many months back. The code can be developed up to allow tasks automation without much work, iv'e left the possible ways to upgrade and can explain how to if required. 

## 🧪 Steps to customize functionalities:
1. Unity will load the Config.json file in the Assets\Resources folder for the tasks.
2. Please refer to the ConfigHelp.txt in the Assets\Resources folder for help with editing the tasks.
3. Please note, Unity was not accepting the JSON file with spaces and line endings, so please use ConfigHelp.txt to design the task sequence and then use [this link](https://codebeautify.org/jsonminifier) to minify the JSON file and replace it with the Config.JSON
   
   ### Versions of softwares used:
- [x] Unity Version Used - 2019.3.12f1
- [x] Python Version 3.7.7

## 👦 Developed By:
<h2 align="center">Akash Ramjyothi</h2>
<p align="center">
  <a href="https://github.com/Akash-Ramjyothi"><img src="https://avatars.githubusercontent.com/u/54114888?v=4" width=150px height=150px /></a> 
    
<p align="center">
  <a target="_blank"href="https://www.linkedin.com/in/akash-ramjyothi/"><img src="https://img.shields.io/badge/linkedin-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
  <a href="mailto:akash.ramjyothi@gmail.com?subject=Hello%20Akash,%20From%20Github"><img src="https://img.shields.io/badge/gmail-%23D14836.svg?&style=for-the-badge&logo=gmail&logoColor=white" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
  <a href="https://www.instagram.com/akash.ramjyothi/"><img src="https://img.shields.io/badge/instagram-%23D14836.svg?&style=for-the-badge&logo=instagram&logoColor=pink" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
  ☎️ PH:+91 8939928002.    
   
## 📃 DRDO (Defence Research and Development Organisation) Internship Certificate:
<p align="center">
<img src="https://user-images.githubusercontent.com/54114888/143779771-97d3e745-3353-4376-b8e0-3af49cba23eb.png" width="600" height="">
</p>

## 📜 Internship/ Project Final Report link:
https://drive.google.com/file/d/1DNPE7wvUvXucpWZFQKODHrua9IkgJA7U/view?usp=sharing

## 💥 How to Contribute?

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)
[![Open Source Love svg2](https://badges.frapsoft.com/os/v2/open-source.svg?v=103)](https://github.com/ellerbrock/open-source-badges/) 

- Take a look at the Existing [Issues](https://github.com/Akash-Ramjyothi/NASA-MATB-II/issues) or create your own Issues!
- Wait for the Issue to be assigned to you after which you can start working on it.
- Fork the Repo and create a Branch for any Issue that you are working upon.
- Create a Pull Request which will be promptly reviewed and suggestions would be added to improve it.
- Add Screenshots to help me know what this Code is all about.

## 🌐 References Used:
- https://store.steampowered.com/app/502500/ACE_COMBAT_7_SKIES_UNKNOWN/
- https://software.nasa.gov/software/LAR-17835-1
- https://matb.larc.nasa.gov/
- https://ntrs.nasa.gov/api/citations/20110014456/downloads/20110014456.pdf   
