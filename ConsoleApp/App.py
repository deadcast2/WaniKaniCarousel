import sys
import os

libdir = '/home/pi/e-Paper/RaspberryPi_JetsonNano/python/lib'
if os.path.exists(libdir):
    sys.path.append(libdir)
    
from waveshare_epd import epd7in5_V2
from PIL import Image,ImageDraw,ImageFont
from selenium import webdriver
from time import sleep

chrome_options = webdriver.ChromeOptions(); 

chrome_options.add_experimental_option('excludeSwitches', ['enable-automation']);

driver = webdriver.Chrome(options=chrome_options)

driver.get('http://localhost:5000');

driver.set_window_size(800, 480)

sleep(1)

driver.save_screenshot('screenshot.png')

driver.quit()

epd = epd7in5_V2.EPD()   
 
epd.init()

epd.Clear()

image = Image.open('screenshot.png')

epd.display(epd.getbuffer(image.resize((epd.width, epd.height))))

epd.sleep()
