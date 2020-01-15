# -*- coding: utf-8 -*-
# Libraries
import glob, os, numpy
import matplotlib.pyplot as plt
import pandas as pd
from math import pi

#1,2,3,
#0,0,0,
#0,0,0,
#26,22,21,
#18,26,23,
#24,20,24,

dirname, filename = os.path.split(os.path.abspath(__file__))
file = open(dirname + '\Assets\Resources\star.x')
fileInput = file.read().splitlines()
group=fileInput[0][:-1].split(',')
var1=fileInput[1][:-1].split(',')
var1= [int(i) for i in var1]
var2=fileInput[2][:-1].split(',')
var2= [int(i) for i in var2]
var3=fileInput[3][:-1].split(',')
var3= [int(i) for i in var3]
var4=fileInput[4][:-1].split(',')
var4= [int(i) for i in var4]
var5=fileInput[5][:-1].split(',')
var5= [int(i) for i in var5]

#print(var1)
# Set data
df = pd.DataFrame({
'group': group,
'Goblinattack': var1,
'Storm': var2,
'Rain': var3,
'Cloudy': var4,
'Sun': var5
})
 
# ------- PART 1: Define a function that do a plot for one line of the dataset!
 
def make_spider( row, title, color):
 
    # number of variable
    categories = list(df)[1:]
    N = len(categories)
     
    # What will be the angle of each axis in the plot? (we divide the plot / number of variable)
    angles = [n / float(N) * 2 * pi + 0.65 for n in range(N)]
    angles += angles[:1]
     
    # Initialise the spider plot
    ax = plt.subplot(2,2,row+1, polar=True, )
     
    # If you want the first axis to be on top:
    ax.set_theta_offset(pi / 2)
    ax.set_theta_direction(-1)
     
    # Draw one axe per variable + add labels labels yet
    plt.xticks(angles[:-1], categories, color='grey', size=8)
     
    # Draw ylabels
    ax.set_rlabel_position(0)
    plt.yticks([0,20,40], ["0","20","40"], color="grey", size=7)
    plt.ylim(0,60)
     
    # Ind1
    values=df.loc[row].drop('group').values.flatten().tolist()
    values += values[:1]
    ax.plot(angles, values, color=color, linewidth=2, linestyle='solid')
    ax.fill(angles, values, color=color, alpha=0.4)
     
    # Add a title
    plt.title(title, size=11, color=color, y=1.1)
     
# ------- PART 2: Apply to all individuals
# initialize the figure
my_dpi=96
plt.figure(figsize=(1000/my_dpi, 1000/my_dpi), dpi=my_dpi)
     
# Create a color palette:
my_palette = plt.cm.get_cmap("Dark2", len(df.index))
     
# Loop to plot
for row in range(0, len(df.index)):
    make_spider(row=row, title='Variante '+df['group'][row], color=my_palette(row))
plt.savefig(dirname + '\Assets\Resources\spider.png')
