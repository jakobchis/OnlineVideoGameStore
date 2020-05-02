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
	// Requirement 3 edit member account info
	[TestFixture]
	public class MemberChangePassword
	{
		IWebDriver driver;

		[Test]
		public void MemberChangePasswordPositive()
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
			accountPage.Password();
			accountPage.EnterNewPassword("Password123!", "NewPassword123!", "NewPassword123!");
			Thread.Sleep(1000);
			accountPage.UpdatePassword();
			accountPage.Logout();

			homePage.Login();

			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("NewPassword123!");
			loginPage.Login();

			Assert.IsTrue(homePage.CheckLoggedIn());
			driver.Close();

			ResetPassword();
		}

		[Test]
		public void MemberChangePasswordNegative()
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
			accountPage.Password();
			accountPage.EnterNewPassword("Password123!", "NewPassword123!", "NewPassword123!");
			Thread.Sleep(1000);
			accountPage.UpdatePassword();
			accountPage.Logout();

			homePage.Login();

			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			Assert.IsFalse(homePage.CheckLoggedIn());
			driver.Close();

			ResetPassword();
		}

		public void ResetPassword()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("NewPassword123!");
			loginPage.Login();

			homePage.ViewAccount();

			AccountPage accountPage = new AccountPage(driver);
			Thread.Sleep(1000);
			accountPage.Password();
			accountPage.EnterNewPassword("NewPassword123!", "Password123!", "Password123!");
			Thread.Sleep(1000);
			accountPage.UpdatePassword();
			accountPage.Logout();

			driver.Close();
		}
	}
}
