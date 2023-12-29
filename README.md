# WaniKani Carousel

I'm using WaniKani to learn a lot of Japanese vocabulary and thought it would be cool to have a display always showing me the newest Kanji/Radical/Vocab I've been learning on the site. Using an e-ink display was a great choice since I don't need to see new vocabulary that often so a slow refreshing screen is adequate. The build is very simple containing only a picture frame bought from the American store Hobby Lobby, an e-ink screen from Waveshare and a Raspberry PI board. I consider this a prototype and may hire someone on Etsy to make me a nicer frame in the future. If you have any questions let me know.

![IMG_1195_cropped](https://github.com/deadcast2/WaniKaniCarousel/assets/45521946/70aed16d-1f2b-4480-8238-f38ae4f7d505)

## Setup for Raspberry Pi

1. Install Chrome
2. Install chromedriver (https://chromedriver.storage.googleapis.com/index.html?path=114.0.5735.90/) **
3. pip install -U selenium
4. Clone: `git clone https://github.com/waveshare/e-Paper.git` and put into home directory.

** For raspberry pi run: `sudo apt-get install chromium-chromedriver`

## Deployment

Publish the web app to the published folder and secure shell copy it using:

`scp -r published/* pi@192.168.0.25:/home/pi/WaniKaniCarousel/published/`

For me the above address for my local connected pi is 192.168.0.25.

## Post Deployment

When you finish deploying the .net core app, update the published appsettings.json file to contain your WaniKani API key:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "WaniKani": {
    "UserApiKey": "your wanikani api key"
  }
}
```
## Crontab

Add this crontab entry to start up the .net core web app and to have the python app grab the most recent vocabulary.

`DISPLAY=:0`

`@reboot cd ~/WaniKaniCarousel/published/ && ./WebApp`

`* * * * *           cd ~/WaniKaniCarousel/ConsoleApp && python3 App.py >> ~/wanikanicarousel.log 2>&1`


