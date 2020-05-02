using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Classes
{
	public class WishlistPage
	{
		IWebDriver driver;
		By textWishlistTitle = By.XPath("/html/body/div/main/h1");
		By wishlistRow = By.XPath("/html/body/div/main/table/tbody/tr");
		By buttonDeleteWishlistItem = By.LinkText("Delete");
		By buttonConfirmDelete = By.XPath("/html/body/div/main/div/form/input[2]");

		public WishlistPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public bool CheckWishlistHasRow()
		{
			try
			{
				driver.FindElement(wishlistRow);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		public void DeleteWishlistItem()
		{
			driver.FindElement(buttonDeleteWishlistItem).Click();
			driver.FindElement(buttonConfirmDelete).Click();
		}

		public bool CheckWishlistTitle()
		{
			if (driver.FindElement(textWishlistTitle).Text == "My Wishlist")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
