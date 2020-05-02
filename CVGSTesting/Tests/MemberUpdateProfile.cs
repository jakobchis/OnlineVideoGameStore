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
	// Requirement 4 edit member profile
	[TestFixture]
	public class MemberUpdateProfile
	{
		IWebDriver driver;

		[Test]
		public void MemberUpdateProfilePositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			homePage.ViewAccount();

			AccountPage accountPage = new AccountPage(driver);
			Thread.Sleep(1000);
			accountPage.EnterFirstName("123");
			accountPage.EnterLastName("123");
			accountPage.SaveProfile();

			Assert.IsTrue(accountPage.CheckSavedProfile("Local123", "Admin123"));
			driver.Close();
		}
	}
}
