# -*- coding: utf-8 -*-

import glob
import matplotlib
import os
import matplotlib.pyplot as plt

filenames = []

#script = os.path.realpath(__file__)  
dirname, filename = os.path.split(os.path.abspath(__file__))
#print(dirname)
#print(filename)
for filename in glob.glob(dirname + '\*.txt'): #get every file that's has .txt in the folder
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
    base = os.path.basename(filename)
    filenames.append(os.path.splitext(base)[0])
plt.legend(filenames)
print(dirname)
plt.savefig(dirname + '\Assets\graph.png')
#print("Current working dir : %s" % os.getcwd())
#print("end")