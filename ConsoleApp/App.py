from selenium import webdriver

driver = webdriver.Chrome()
driver.get('http://localhost:5000');
driver.set_window_size(800, 480)
driver.save_screenshot("screenshot.png")
driver.quit()
