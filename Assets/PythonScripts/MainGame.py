# -*- coding: utf-8 -*-

import Simulation, Field, Farmer, pygame, sys, pylab, matplotlib
from pygame.locals import *
matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg

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

#Graph example
fig = pylab.figure(figsize=[4, 4], dpi=100,)  # 100 dots per inch, so the resulting buffer is 400x400 pixel
ax = fig.gca() #get current axes
ax.plot([1, 2, 4]) #plot([x1, x2, x3], [y1, y2, y3])
canvas = agg.FigureCanvasAgg(fig) #get the fiqure as an rgb string and convert it to an array for rendering
canvas.draw()
renderer = canvas.get_renderer()
raw_data = renderer.tostring_rgb()

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

screen = pygame.display.get_surface()
size = canvas.get_width_height()

#put the graph-image onto the game screen
graph = pygame.image.fromstring(raw_data, size, "RGB")

pygame.display.flip()
          #flip() -> This will update the contents of the entire display. If your display
          # mode is using the flags pygame.HWSURFACE and pygame.DOUBLEBUF, this
          # will wait for a vertical retrace and swap the surfaces. If you are
          # using a different type of display mode, it will simply update the
          # entire contents of the surface


#Simulation variables
countfarmer = 4    #doto this is user input      
x = Simulation.Simulation(countfarmer)

while True: # main game loop
    
    
    screen.blit(graph, (0,0)) #draw one image onto another, blits() draws many images onto another
    screen.blit(field,(0,0))
    for event in pygame.event.get():
        if event.type == KEYDOWN:  
            if event.key == pygame.K_SPACE:
                #print('space pressed')
                x.PassYear()
        if event.type == QUIT:
            pygame.quit() #deactivate pygame library
            sys.exit() #terminates the program
    pygame.display.update()
    fpsClock.tick(FPS) #this ensures that the game doesnt run too fast