# -*- coding: utf-8 -*-

import glob
import matplotlib
import os
import matplotlib.pyplot as plt

filenames = []

for filename in glob.glob('C:/Users/sakob/Documents/BuisnessSimulation/*.txt'): #get every file that's has .txt in the folder
    xValue = 1;
    file = open(filename)
    fileInput = file.read().splitlines()
    yValues = []
    xValues = []
    for line in fileInput:
        #plotgraph, each line is one y - value *** todo
        
        xValues.append(xValue)
        yValue = float(line.replace(',','.'))
        yValues.append(yValue)
        xValue += 1
    plt.plot(xValues, yValues)
    filenames.append(filename)
plt.legend(filenames)
plt.savefig("C:/Users/sakob/Documents/BuisnessSimulation/Assets/graph.png")
#print("Current working dir : %s" % os.getcwd())
print("end")