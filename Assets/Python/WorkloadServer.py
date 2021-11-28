import time
import threading
import csv
from multiprocessing import Process
from pylsl import StreamInlet, resolve_stream
from flask import Flask, redirect, url_for, render_template
from random import random as rand
import tensorflow as tf
from tensorflow.python.keras.layers import  Input, Embedding, Dot, Reshape, Dense
from tensorflow.python.keras.models import load_model

fileWriteStarted = False
# first resolve an EEG stream on the lab network
print("looking for an EEG stream...")
streams = resolve_stream('type', 'EEG')
model = load_model('model.h5')
directory="test.csv"

# create a new inlet to read from the stream
inlet = StreamInlet(streams[0])

app = Flask(__name__)

@app.route("/")
def home():
    return "Maybe you can use this to stream data on devices connected on same network"

@app.route("/config/<path:pth>")
def fileLocation(pth):
    global fileWriteStarted, directory
    if(fileWriteStarted):
        return "DONE"
    fileWriteStarted = True
    directory = pth
    p1.start()
    return "DONE"

@app.route("/taskbattery/workload")
def calculateWorkload():
    sample, timestamp = inlet.pull_sample()
    pred = model.predict([sample,])
    st = str(pred[0][0])
    return st

def getglobal():
    global directory
    return directory

def createCSV():
    directory = getglobal()
    with open(directory, 'w', newline='') as f:
        theWriter = csv.writer(f)
        while(True):
            if(not fileWriteStarted):
                break
            sample, timestamp = inlet.pull_sample()
            sample.insert(0, timestamp)
            theWriter.writerow(sample)

@app.route("/end")
def endSession(): #Will stop file write 
    global fileWriteStarted, p1
    fileWriteStarted = False
    p1 = threading.Thread(target=createCSV)
    return "DONE"

p1 = threading.Thread(target=createCSV)
app.run(debug=True, port=8080)