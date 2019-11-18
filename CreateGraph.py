# -*- coding: utf-8 -*-

import glob
import matplotlib.pyplot as plt

filenames = []

for filename in glob.glob('*.txt'): #get every file that's has .txt in the folder
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
plt.savefig("graph.png")