using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CVGS_Selenium_Tests.Classes
{
	public class RegisterPage
	{
		IWebDriver driver;

		By textUsername = By.Name("Input.Username");
		By textFirstName = By.Name("Input.FirstName");
		By textLastName = By.Name("Input.LastName");
		By textDateOfBirth = By.Name("Input.DoB");
		By selectGender = By.Name("Input.GenderId");
		By textEmail = By.Name("Input.Email");
		By textPassword = By.Name("Input.Password");
		By textConfirmPassword = By.Name("Input.ConfirmPassword");
		By buttonRegister = By.XPath("/html/body/div/main/div/div/form/button");

		public RegisterPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void EnterRegistrationInfo(string username, string firstName, string lastName,
			string dateOfBirth, string gender, string email, string password, string confirmPassword)
		{
			driver.FindElement(textUsername).SendKeys(username);
			driver.FindElement(textFirstName).SendKeys(firstName);
			driver.FindElement(textLastName).SendKeys(lastName);
			driver.FindElement(textDateOfBirth).SendKeys(dateOfBirth);
			SelectElement selectGenderElement = new SelectElement(driver.FindElement(selectGender));
			selectGenderElement.SelectByText(gender);
			driver.FindElement(textEmail).SendKeys(email);
			driver.FindElement(textPassword).SendKeys(password);
			driver.FindElement(textConfirmPassword).SendKeys(confirmPassword);
		}

		public void Register()
		{
			driver.FindElement(buttonRegister).Click();
		}
	}
}
