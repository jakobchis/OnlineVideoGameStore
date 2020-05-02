using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 10 view friends list
	[TestFixture]
	class ViewFriendsList
	{
		IWebDriver driver;

		[Test]
		public void ViewFriendsListPositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			homePage.FriendsList();

			FriendsListPage friendsListPage = new FriendsListPage(driver);
			Assert.IsTrue(friendsListPage.CheckFriendsListTitle());
			driver.Close();
		}

		[Test]
		public void ViewFriendsListNegative()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);

			try
			{
				homePage.FriendsList();
				Assert.Fail();
			}
			catch (NoSuchElementException)
			{
				Assert.Pass();
			}

			driver.Close();
		}
	}
}
