using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Classes
{
	public class FriendsListPage
	{
		IWebDriver driver;
		By textFriendsListTitle = By.XPath("/html/body/div/main/h1");
		By textEnterFriendName = By.Name("Username");
		By buttonAddFriend = By.XPath("/html/body/div/main/form/input");
		By friendRow = By.XPath("/html/body/div/main/table/tbody/tr");
		By buttonRemoveFriend = By.LinkText("Remove Friend");
		By buttonConfirmRemoveFriend = By.XPath("/html/body/div/main/div/form/input[2]");

		public FriendsListPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void EnterFriendName(string friendName)
		{
			driver.FindElement(textEnterFriendName).SendKeys(friendName);
		}

		public void AddFriend()
		{
			driver.FindElement(buttonAddFriend).Click();
		}

		public bool CheckFriendsListHasRow()
		{
			try
			{
				driver.FindElement(friendRow);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		public void DeleteFriendsListItem()
		{
			driver.FindElement(buttonRemoveFriend).Click();
			driver.FindElement(buttonConfirmRemoveFriend).Click();
		}

		public bool CheckFriendsListTitle()
		{
			if (driver.FindElement(textFriendsListTitle).Text == "Friends")
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
