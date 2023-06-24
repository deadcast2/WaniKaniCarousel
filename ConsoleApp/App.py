from selenium import webdriver

driver = webdriver.Chrome()
driver.get('https://localhost:7237/');
driver.set_window_size(800, 480)
driver.save_screenshot("screenshot.png")
driver.quit()