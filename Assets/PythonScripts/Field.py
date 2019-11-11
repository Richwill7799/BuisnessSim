# -*- coding: utf-8 -*-
"""
Created on Sun Nov 10 14:09:55 2019

@author: sakob
"""

class Field():
    __harvest = 0  #'in kilograms'
    __startValue = 0
    __multiplier = 0 #'changes every year'
    __variant = 0

    def __init__(self, variant):
        self.__harvest = 0
        self.__startValue = 0
        self.__multiplier = 0
        self.__variant = variant #'this is the variant of the field. means: every two fields are different, so we have variant 0 and variant 1 e.g. four fields, where each two are equivalent, so we have 2 fields with variant 0 and two fields with variant 1' 
    
    def SimulateField(self):
        if self.__harvest == 0:
            self.__harvest = self.__startValue * self.__multiplier 
        else:    
            self.__harvest *= self.__multiplier
            
    def GetHarvest(self):
        return self.__harvest
    
    def SetHarvest(self, harvest):
        self.__harvest = harvest
        
    
    def SetStartValue(self, startValue):
        self.__startValue = startValue
    
    def GetMultiplier(self):
        return self.__multiplier
     
    def SetMultiplier(self, multiplier):
            self.__multiplier = multiplier
            
    def GetVariant(self):
            return self.__variant