using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 10 add friend
	[TestFixture]
	class AddFriend
	{
		IWebDriver driver;

		[Test]
		public void AddFriendPositive()
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
			friendsListPage.EnterFriendName("user");
			friendsListPage.AddFriend();
			Assert.IsTrue(friendsListPage.CheckFriendsListHasRow());
			friendsListPage.DeleteFriendsListItem();
			driver.Close();
		}

		[Test]
		public void AddFriendNegative()
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
			friendsListPage.EnterFriendName("otheruser");
			friendsListPage.AddFriend();
			Assert.IsFalse(friendsListPage.CheckFriendsListHasRow());
			driver.Close();
		}
	}
}
