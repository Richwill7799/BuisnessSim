# -*- coding: utf-8 -*-

import Simulation, Field, Farmer, pygame, sys, pylab
from pygame.locals import *
matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg

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
          # entire contents of the surface
countfarmer = 4          
x = Simulation.Simulation(countfarmer)

while True: # main game loop
     
    for event in pygame.event.get():
        if event.type == KEYDOWN:  
            if event.key == pygame.K_SPACE:
                #print('space pressed')
                x.PassYear()
        if event.type == QUIT:
            pygame.quit()
            sys.exit()