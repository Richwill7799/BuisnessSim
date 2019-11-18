# -*- coding: utf-8 -*-
"""
Created on Sat Nov  9 13:01:35 2019

@author: sarah
"""

import pygame, Field, Farmer, random, pylab, matplotlib
from pygame.locals import *
matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg


class Simulation():
    pygame.init()
    
    #Graph example
    
    
    #//private variables
    __year = 0
    __countFarmers = 0
    __fields = [] 
    __variants = []
    __xValue = 0
    
    #public variables
    collabValues = []
    xValues = []
    farmers = []
    
    def __init__(self, countfarmer):
        self.__year = 0
        self.__countFarmers = countfarmer #//TODO: this should be editable for the user at the beginning via input field, MIN: 4, MAX: ?   
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
            self.farmers.append(farmer);
            bauernname += 1
    
        #//instantiate the CollabFarmers - FIRST VERSION only two collab. farmers - TODO this should be done by the User in future
        f = self.farmers[0]
        for i in range(1, self.__countFarmers):
            if f.GetField().GetVariant() != self.farmers[i].GetField().GetVariant():
                f.SetCollabFarmer(self.farmers[i])
                self.farmers[i].SetCollabFarmer(f)
                break; #this has to be rewrited, maybe with a lambda statement       
    
    def PassYear(self):
        self.xValues.append(self.__xValue)
        self.__SetMultiplier() #set multiplier each year - to be edited with weather ect
        for field in self.__fields:
            field.SimulateField()
    
        self.__Collaboration()
    
        #Debug 
        for f in self.farmers:
            print(f.name + ": " + str(f.GetField().GetHarvest()))
            
        
        #Visualisierung(); #todo for everyone who wants this  xD
        self.__xValue += 1
        
    
    def __Collaboration(self):
        #now combine the farmer's harvest if they collab and then halve it and give each collab farmer the half of the collab.Harvest
        fa = next(x for x in self.farmers if not x.HasNoCollabFarmer()) #//this should find in any case a farmer. if not, the simulation is corrupt xD
        #get the collab farmer
        harvestF = fa.GetField().GetHarvest()
        harvestCollabF = fa.GetCollabFarmer().GetField().GetHarvest()
        collabHarvest = (harvestF + harvestCollabF) / 2
        fa.GetField().SetHarvest(collabHarvest) #set the harvest now to tzhe half of the collab harvest
        fa.GetCollabFarmer().GetField().SetHarvest(collabHarvest) # set the harvest now to tzhe half of the collab harvest
        self.collabValues.append(collabHarvest)
        
    def __SetMultiplier(self): #set the multiplier for each variant of field, bc each field with equal variant has the same multiplier
        for variant in self.__variants:
            multiplier = random.uniform(0.6, 1.5)#TODO #changed the range from 0.001f/3.0f to this
            for field in self.__fields:
                if (field.GetVariant() == variant):
                    field.SetMultiplier(multiplier)
                    