# -*- coding: utf-8 -*-
"""
Created on Sat Nov  9 13:01:35 2019

@author: sarah
"""

import pygame, sys, Field, Farmer, random, matplotlib, pylab
from pygame.locals import *

matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg

class Simulation():
    pygame.init()
    
    #//private variables
    __year = 0
    __countFarmers = 0
    __fields = [] 
    __farmers = []
    __variants = []
    
    
    #if (Input.GetKeyDown("space"))
    #        {
    #            PassYear();
    #            year++;
    #            Year.text = "Year " + year;
    #        }
    #//public variables
    #public Text Year;
    
    def __init__(self):
        self.__year = 0
        self.__countFarmers = 4 #//TODO: this should be editable for the user at the beginning via input field, MIN: 4, MAX: ?   
        print('startmethod')
        self.__InstantiateLists()
        
    
    #// Start is called before the first frame update
    def Start(self):
        pass
    
    
    def __InstantiateLists(self):
        #//first fields, second farmers with fields
        print('initiate')
        #//instantiate a field and it's variant
        variant = 0
        halfFarmers = int(self.__countFarmers / 2)
        for i in range(0, halfFarmers):
            #//each field
            for i in range(0, halfFarmers):
                #//each variant of a field, in this case two different variants, this can be extended in future
                field = Field.Field(variant)
                self.__fields.append(field)
                self.__variants.append(variant)
            variant += 1
        
        #//insantiate for each field a farmer, with startvalue as 1 kg
        bauernname = 1;
        for field in self.__fields:
            farmer = Farmer.Farmer(field, 1, "bauer" + str (bauernname))
            self.__farmers.append(farmer);
            bauernname += 1
    
        #//instantiate the CollabFarmers - FIRST VERSION only two collab. farmers - TODO this should be done by the User in future
        f = self.__farmers[0]
        for i in range(1, self.__countFarmers):
            if f.GetField().GetVariant() != self.__farmers[i].GetField().GetVariant():
                f.SetCollabFarmer(self.__farmers[i])
                self.__farmers[i].SetCollabFarmer(f)
                break; #this has to be rewrited, maybe with a lambda statement       
    
    def PassYear(self):
        self.SetMultiplier() #set multiplier each year - to be edited with weather ect
        for field in self.__fields:
            field.SimulateField()
    
        self.__Collaboration();
    
        #Debug 
        for f in self.__farmers:
            print(f.name + ": " + str(f.GetField().GetHarvest()))
    
        #Visualisierung(); #todo for everyone who wants this  xD
    
    def __Collaboration(self):
        #now combine the farmer's harvest if they collab and then halve it and give each collab farmer the half of the collab.Harvest
        fa = next(x for x in self.__farmers if not x.HasNoCollabFarmer()) #//this should find in any case a farmer. if not, the simulation is corrupt xD
        #get the collab farmer
        harvestF = fa.GetField().GetHarvest()
        harvestCollabF = fa.GetCollabFarmer().GetField().GetHarvest()
        collabHarvest = (harvestF + harvestCollabF) / 2
        fa.GetField().SetHarvest(collabHarvest) #set the harvest now to tzhe half of the collab harvest
        fa.GetCollabFarmer().GetField().SetHarvest(collabHarvest) # set the harvest now to tzhe half of the collab harvest
    
    def SetMultiplier(self): #set the multiplier for each variant of field, bc each field with equal variant has the same multiplier
        for variant in self.__variants:
            multiplier = random.uniform(0.6, 1.5)#TODO #changed the range from 0.001f/3.0f to this
            for field in self.__fields:
                if (field.GetVariant() == variant):
                    field.SetMultiplier(multiplier)
                
    
  
#This is the main game loop! here to do stuff ***** THIS IS NOT ANYMORE THE CLASS SIMULATION

fig = pylab.figure(figsize=[4, 4], dpi=100,)  # 100 dots per inch, so the resulting buffer is 400x400 pixel
ax = fig.gca() #get current axes
ax.plot([1, 2, 4]) #plot([x1, x2, x3], [y1, y2, y3])

canvas = agg.FigureCanvasAgg(fig) #get the fiqure as an rgb string and convert it to an array for rendering
canvas.draw()
renderer = canvas.get_renderer()
raw_data = renderer.tostring_rgb()

DISPLAYSURF = pygame.display.set_mode((1000, 1000), DOUBLEBUF)
pygame.display.set_caption('FarmersFable')

screen = pygame.display.get_surface()
size = canvas.get_width_height()

surf = pygame.image.fromstring(raw_data, size, "RGB")
screen.blit(surf, (0,0)) #draw one image onto another, blits() draws many images onto another
pygame.display.flip()
          #flip() -> This will update the contents of the entire display. If your display
          # mode is using the flags pygame.HWSURFACE and pygame.DOUBLEBUF, this
          # will wait for a vertical retrace and swap the surfaces. If you are
          # using a different type of display mode, it will simply update the
          # entire contents of the surface.
          # 
x = Simulation()

while True: # main game loop
     
    for event in pygame.event.get():
        if event.type == KEYDOWN:  
            if event.key == pygame.K_SPACE:
                #print('space pressed')
                x.PassYear()
        if event.type == QUIT:
            pygame.quit()
            sys.exit()