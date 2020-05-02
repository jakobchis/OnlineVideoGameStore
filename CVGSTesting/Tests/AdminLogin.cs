using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 2 member login test
	[TestFixture]
	public class AdminLogin
	{
		IWebDriver driver;

		[Test]
		public void AdminLoginPositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			Assert.IsTrue(homePage.CheckLoggedIn());
			driver.Close();
		}

		[Test]
		public void AdminLoginNegative()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("wrongname");
			loginPage.EnterPassword("wrongpassword");
			loginPage.Login();

			Assert.IsFalse(homePage.CheckLoggedIn());
			driver.Close();
		}
	}
}
