# -*- coding: utf-8 -*-

import Simulation, Field, Farmer, pygame, sys, matplotlib
from pygame.locals import *
matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg
import matplotlib.pyplot as ppl
import numpy as np

#This is the main game loop! here to do stuff 
#1. Handles events.
#2. Updates the game state.
#3. Draws the game state to the screen
fpsClock = pygame.time.Clock() 
FPS = 40
 # set up the colors
BLACK = (  0,   0,   0)
WHITE = (255, 255, 255)
RED   = (255,   0,   0)
GREEN = (  0, 255,   0)
BLUE  = (  0,   0, 255)



#initiating gameWindow
DISPLAYSURF = pygame.display.set_mode((1000, 1000), DOUBLEBUF)
DISPLAYSURF.fill(WHITE)
pygame.display.set_caption('FarmersFable')
pygame.draw.rect(DISPLAYSURF, RED, (500, 500, 100, 50)) #examplefield (?)
field = pygame.image.load('Field_normal.png')
#fontObj = pygame.font.Font('freesansbold.ttf', 32)
#textSurfaceObj = fontObj.render('Hello world!', True, GREEN, BLUE) #blue is background, green is font color
#textRectObj = textSurfaceObj.get_rect()
#textRectObj.center = (200, 150)
          #flip() -> This will update the contents of the entire display. If your display
          # mode is using the flags pygame.HWSURFACE and pygame.DOUBLEBUF, this
          # will wait for a vertical retrace and swap the surfaces. If you are
          # using a different type of display mode, it will simply update the
          # entire contents of the surface

#put the graph-image onto the game screen
fig = ppl.figure(figsize=[4, 4], dpi=100,)  # 100 dots per inch, so the resulting buffer is 400x400 pixel
#ax = fig.gca() #get current axes
    #ax.plot([1, 2, 4]) #plot([x1, x2, x3], [y1, y2, y3])



countfarmer = int(input("Enter a number of farmers (for beginning just 4 please) : ")) #this is done in the console at first 
x = Simulation.Simulation(countfarmer)
while True: # main game loop
     #draw one image onto another, blits() draws many images onto another
    
    for event in pygame.event.get():
        if event.type == KEYDOWN:  
            if event.key == pygame.K_SPACE:
                screen = pygame.display.get_surface()
                canvas = agg.FigureCanvasAgg(fig) #get the fiqure as an rgb string and convert it to an array for rendering
                canvas.draw()
                renderer = canvas.get_renderer()
                raw_data = renderer.tostring_rgb()
                size = canvas.get_width_height()
                graph = pygame.image.fromstring(raw_data, size, "RGB")
                #print('space pressed')
                x.PassYear()
                for f in x.farmers:
                    a = np.array(x.xValues)
                    if f.HasNoCollabFarmer():
                        b = np.array(f.GetField().allHarvest)
                        ppl.plot([a], [b], '-b') #plot values of each farmer who has no collabFarmer
                    c = np.array(x.collabValues)
                    ppl.plot([a], [c], '-r') #plot the values of collabFarmer
                screen.blit(field,(0,0))
                screen.blit(graph, (0,0))
                ppl.show()
                pygame.display.update()
        if event.type == QUIT:
            pygame.quit() #deactivate pygame library
            sys.exit() #terminates the program
    #
    pygame.display.flip()
    fpsClock.tick(FPS) #this ensures that the game doesnt run too fast