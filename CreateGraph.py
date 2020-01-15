# -*- coding: utf-8 -*-

import glob
import matplotlib
import os
import matplotlib.pyplot as plt

filenames = []

#script = os.path.realpath(__file__)  
dirname, filename = os.path.split(os.path.abspath(__file__))
colormap = matplotlib.cm.Dark2.colors
#print(dirname)
#print(filename)
i = 0
for filename in glob.glob(dirname + '\Assets\Resources\*.txt'): #get every file that's has .txt in the folder
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
    plt.plot(xValues, yValues, color = colormap[i])
    base = os.path.basename(filename)
    filenames.append(os.path.splitext(base)[0])
    i=i+1
plt.legend(filenames)
#print(dirname)
plt.savefig(dirname + '\Assets\Resources\graph.png')
#print("Current working dir : %s" % os.getcwd())
#print("end")