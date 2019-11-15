# -*- coding: utf-8 -*-
"""
Created on Sun Nov 10 14:10:24 2019

@author: sakob
"""
import Field

class Farmer():
    __field = Field.Field(0) #his own field
    __collabFarmer = None
    name = None

    def __init__(self, field, startValue, name):
        self.__field = field
        self.__field.SetStartValue(startValue)
        self.__collabFarmer = None
        self.name = name
    

    def GetCollabFarmer(self):
        return self.__collabFarmer
    
    def GetField(self):  
        return self.__field
    
    def SetCollabFarmer(self, farmer):
        self.__collabFarmer = farmer
    
    def HasNoCollabFarmer(self):
        return bool(self.__collabFarmer is None)