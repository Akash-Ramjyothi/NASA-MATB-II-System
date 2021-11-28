# Task Battery
Project contins a NASA MATB like interface with 4 tasks:
1. System Monitoring Task
2. Tracking Task
3. Communications Task
4. Resource Management Task

The project also calculates live Workload using 14 channel EEG data and a workload model.

## How to Install?
0. Make sure LSL is properly installed also please Allow any network or file access permission any of the files ask thoughout the setup or runtime. 
1. Create a pyhton 3 virtual environment using the command pyhton3 -m venv env
2. Activate the virtual environment by env\Scripts\activate
3. install the requirements.txt file inside the Assets\Python by using pip install -r path_to_file\requirements.txt
4. Make sure Tensorflow is properly installed, also check for cuda libraries for better performance. 
5. Modify the eeg data stream (if required) at Assets\Python\WorkloadServer.py line 16 & Line 22
6. That's it, Setup complete
   
## How to Run?
1. Activate the virtual environment you created
2. If testing without LSL stream (Skip this step is LSL is available) run the sendData.py file present in the Assets\Python folder, this file will simulate 14 channel eeg data in the LSL stream, keep it running in the background throughout the runtime.
3. Run the WorkloadServer.py stored in Assets\Python folder using Python path_to_file\WorkloadServer.py
4. wait for it to start, you can test the wotking by going to localhost:8080/taskbattery/workload on chrome and it'll return a float falue if everything is working fine.
5. Run the unity task. 
6. In case of any situation if Unity was quitted forcefully and have to reopen, please restart the server as well, Server can handle if the Task was closed by clicking the Exit button in the task but for any other way, you will require to restart the server as well.
7. Data will be stored at C:/Users/< Username >/AppData/LocalLow/INMAS-DRDO/Task Battery/

## Special Features?
1. The grapth view can show multiple graphs if needed in future for plotting other data as well, some modifications to Graph.cs will be required. check Assets\DataDiagram\Documentation
2. Unity and Server codebase is independednt of number of EEG channels, If you want to change the model, you can simply replace the model.h5 file present in the Assets\Python folder and it'll work fine even if it is of different channel eeg, The only limitation is the input value, current model acceprts data in [[channel list],[channel list], ...] if the other model accepts data in some other form, you will be required to change the calculateWorkload function present in WorkloadServer.py file. 
3. Unity Code can run both with and wwithout the Server, but server is required to connect to lsl, calculate workload and to store EEG data for future training. 
4. Possibility to be used in the Dynamic automation allocation as suggested by Vishal sir many months back. The code can be developed up to allow tasks automation without much work, iv'e left the possible ways to upgrade and can explain how to if required. 

## How to edit Tasks?
1. Unity will load the Config.json file in the Assets\Resources folder for the tasks.
2. Please refer to the ConfigHelp.txt in the Assets\Resources folder for help with editing the tasks.
3. Please note, Unity was not accepting the JSON file with spaces and line endings, so please use ConfigHelp.txt to design the task sequence and then use [this link](https://codebeautify.org/jsonminifier) to minify the JSON file and replace it with the Config.JSON
   
- [x] Unity Version Used - 2019.3.12f1
- [x] Python Version 3.7.7

## Rest code is properly structured with comments where required, so there shouldn't be any problem understand given a few days to study the code. 