using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Classes
{
	public class AccountPage
	{
		IWebDriver driver;

		By buttonPassword = By.LinkText("Password");
		By textOldPassword = By.Name("Input.OldPassword");
		By textNewPassword = By.Name("Input.NewPassword");
		By textConfirmPassword = By.Name("Input.ConfirmPassword");
		By buttonUpdatePassword = By.XPath("//*[@id='change-password-form']/button");
		By buttonLogout = By.XPath("/html/body/header/nav/div/div/ul/li[2]/form/button");
		By textFirstName = By.Name("Input.FirstName");
		By textLastName = By.Name("Input.LastName");
		By buttonSaveProfile = By.XPath("//*[@id='update-profile-button']");

		public AccountPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void Password()
		{
			driver.FindElement(buttonPassword).Click();
		}

		public void UpdatePassword()
		{
			driver.FindElement(buttonUpdatePassword).Click();
		}

		public void EnterNewPassword(string oldPassword, string newPassword, string confirmPassword)
		{
			driver.FindElement(textOldPassword).SendKeys(oldPassword);
			driver.FindElement(textNewPassword).SendKeys(newPassword);
			driver.FindElement(textConfirmPassword).SendKeys(confirmPassword);
		}

		public void EnterFirstName(string firstName)
		{
			driver.FindElement(textFirstName).SendKeys(firstName);
		}

		public void EnterLastName(string lastName)
		{
			driver.FindElement(textLastName).SendKeys(lastName);
		}

		public void SaveProfile()
		{
			driver.FindElement(buttonSaveProfile).Click();
		}

		public bool CheckSavedProfile(string firstName, string lastName)
		{
			if (driver.FindElement(textFirstName).GetAttribute("Value") == firstName
				&& driver.FindElement(textLastName).GetAttribute("Value") == lastName)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Logout()
		{
			driver.FindElement(buttonLogout).Click();
		}
	}
}
