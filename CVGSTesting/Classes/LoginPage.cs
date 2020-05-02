using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Classes
{
	public class LoginPage
	{
		IWebDriver driver;

		By textUsername = By.Name("Input.Username");
		By textPassword = By.Name("Input.Password");
		By buttonLogin = By.XPath("//*[@id='account']/div[5]/button");

		public LoginPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void EnterUsername(string username)
		{
			driver.FindElement(textUsername).SendKeys(username);
		}

		public void EnterPassword(string password)
		{
			driver.FindElement(textPassword).SendKeys(password);
		}

		public void Login()
		{
			driver.FindElement(buttonLogin).Click();
		}
	}
}
