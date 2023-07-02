from selenium import webdriver
from time import sleep

chrome_options = webdriver.ChromeOptions(); 

chrome_options.add_experimental_option('excludeSwitches', ['enable-automation']);

driver = webdriver.Chrome(options=chrome_options)

driver.get('http://localhost:5000');

driver.set_window_size(800, 480)

driver.save_screenshot("screenshot.png")

driver.quit()
