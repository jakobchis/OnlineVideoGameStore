using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 2 member join test
	[TestFixture]
	class MemberJoin
	{
		IWebDriver driver;

		[Test]
		public void MemberJoinPositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Register();

			RegisterPage registerPage = new RegisterPage(driver);
			registerPage.EnterRegistrationInfo("test", "test", "test", "19991111", "Female", "me@me.com", "Password123!", "Password123!");
			registerPage.Register();

			Assert.IsTrue(homePage.CheckLoggedIn());
			driver.Close();
		}

		[Test]
		public void MemberJoinNegative()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Register();

			RegisterPage registerPage = new RegisterPage(driver);
			registerPage.EnterRegistrationInfo("test", "test", "test", "19991111", "Female", "me@me.com", "Password123!", "WrongPassword123!");
			registerPage.Register();

			Assert.IsFalse(homePage.CheckLoggedIn());
			driver.Close();
		}
	}
}
