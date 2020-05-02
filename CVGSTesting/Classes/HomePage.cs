using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests
{
	public class HomePage
	{ 
		IWebDriver driver;
		By buttonLogin = By.LinkText("Login");
		By buttonRegister = By.LinkText("Register");
		By buttonWishlist = By.LinkText("Wishlist");
		By buttonFriendsList = By.LinkText("Friends List");
		By buttonAccount = By.PartialLinkText("Hello");
		By buttonFirstGameDetails = By.XPath("/html/body/div/main/div[3]/div/div/div[1]/a");
        By buttonAdminPortal = By.XPath("/html/body/header/nav/div/div/ul[2]/li[4]/a");
        By buttonAdminReview = By.XPath("/html/body/header/nav/div/div/ul[2]/li[4]/div/a[5]");

        public HomePage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void Login()
		{
			driver.FindElement(buttonLogin).Click();
		}

		public void Register()
		{
			driver.FindElement(buttonRegister).Click();
		}

		public void Wishlist()
		{
			driver.FindElement(buttonWishlist).Click();
		}

		public void FriendsList()
		{
			driver.FindElement(buttonFriendsList).Click();
		}

		public void ViewAccount()
		{
			driver.FindElement(buttonAccount).Click();
		}

		public void ViewGameDetails()
		{
			driver.FindElement(buttonFirstGameDetails).Click();
		}

        public void ViewAdminReviewPage()
        {
            driver.FindElement(buttonAdminPortal).Click();
            driver.FindElement(buttonAdminReview).Click();
        }


        public bool CheckLoggedIn()
		{
			try
			{
				driver.FindElement(buttonWishlist);
				driver.FindElement(buttonFriendsList);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}
	}
}
