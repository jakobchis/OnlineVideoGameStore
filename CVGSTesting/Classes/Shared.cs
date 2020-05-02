using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Classes
{
	public class Shared
	{
		public static ChromeDriver TestSetup()
		{
			ChromeDriver driver = new ChromeDriver();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			driver.Navigate().GoToUrl("https://localhost:44368");
			return driver;
		}
	}
}
