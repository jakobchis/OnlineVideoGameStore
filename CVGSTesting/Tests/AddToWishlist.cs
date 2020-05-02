using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 9 add game to wishlist
	[TestFixture]
	class AddToWishlist
	{
		IWebDriver driver;

		[Test]
		public void AddToWishlistPositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			homePage.ViewGameDetails();

			GameDetailsPage gameDetailsPage = new GameDetailsPage(driver);
			gameDetailsPage.AddToWishlist();

			WishlistPage wishlistPage = new WishlistPage(driver);
			Assert.IsTrue(wishlistPage.CheckWishlistHasRow());
			wishlistPage.DeleteWishlistItem();

			driver.Close();
		}

		[Test]
		public void AddToWishlistNegative()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.Login();

			LoginPage loginPage = new LoginPage(driver);
			loginPage.EnterUsername("admin");
			loginPage.EnterPassword("Password123!");
			loginPage.Login();

			homePage.Wishlist();

			WishlistPage wishlistPage = new WishlistPage(driver);
			Assert.IsFalse(wishlistPage.CheckWishlistHasRow());

			driver.Close();
		}
	}
}
