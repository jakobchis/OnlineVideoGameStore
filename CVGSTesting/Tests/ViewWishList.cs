using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 11 view wish list
	[TestFixture]
	class ViewWishList
	{
		IWebDriver driver;

		[Test]
		public void ViewWishListPositive()
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
			Assert.IsTrue(wishlistPage.CheckWishlistTitle());
			driver.Close();
		}

		[Test]
		public void ViewWishlistNegative()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);

			try
			{
				homePage.Wishlist();
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
