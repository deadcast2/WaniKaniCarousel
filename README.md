# WaniKani Carousel

## Setup for Raspberry Pi

1. Install Chrome
2. Install chromedriver (https://chromedriver.storage.googleapis.com/index.html?path=114.0.5735.90/) *
3. pip install -U selenium

* For raspberry pi: `sudo apt-get install chromium-chromedriver`

## Deployment

Publish the web app to the published folder and secure shell copy it using:

`scp -r published/* pi@192.168.0.25:/home/pi/WaniKaniCarousel/published/`
