# WaniKani Carousel

## Setup for Raspberry Pi

1. Install Chrome
2. Install chromedriver (https://chromedriver.storage.googleapis.com/index.html?path=114.0.5735.90/) *
3. pip install -U selenium
4. Clon`git clone https://github.com/waveshare/e-Paper.git` and put into home directory.
* For raspberry pi: `sudo apt-get install chromium-chromedriver`

## Deployment

Publish the web app to the published folder and secure shell copy it using:

`scp -r published/* pi@192.168.0.25:/home/pi/WaniKaniCarousel/published/`

`DISPLAY=:0`
`@reboot cd ~/WaniKaniCarousel/published/ && ./WebApp`
`* * * * *           cd ~/WaniKaniCarousel/ConsoleApp && python3 App.py >> ~/wanikanicarousel.log 2>&1`


