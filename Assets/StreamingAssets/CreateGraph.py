# -*- coding: utf-8 -*-

import glob
import matplotlib
import os
import matplotlib.pyplot as plt

filenames = [] #naming for the graph
dirname, filename = os.path.split(os.path.abspath(__file__)) #get the relative path
colormap = matplotlib.cm.tab20.colors

i = 0 #the index for the colormap
for filename in glob.glob(dirname + '/*.txt'): #get every file that's has .txt in the folder
    xValue = 1;
    file = open(filename)
    fileInput = file.read().splitlines()
    yValues = []
    xValues = []
    for line in fileInput:
        #plotgraph, each line is one y - value
        
        xValues.append(xValue)
        yValue = float(line.replace(',','.'))
        yValues.append(yValue)
        xValue += 1
        plt.plot(xValues, yValues, color = colormap[i]) #plot one line in a specific color
    base = os.path.basename(filename)
    filenames.append(os.path.splitext(base)[0])
    i = i+1
    #filenames.sort(key=len)

plt.legend(filenames, loc ='upper left', bbox_to_anchor = (1.05,1), borderaxespad=0.)
plt.savefig(dirname + '/graph.png')

#plt.savefig(dirname + '\Assets\StreamingAssets\graph.png')
#print("Current working dir : %s" % os.getcwd())
#print("end")
#print(dirname)
#script = os.path.realpath(__file__)
#print(filename)
#print(dirname)
