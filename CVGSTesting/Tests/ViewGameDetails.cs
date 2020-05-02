using System;
using System.Collections.Generic;
using System.Text;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests.Tests
{
	// Requirement 8 view game details
	[TestFixture]
	class ViewGameDetails
	{
		IWebDriver driver;

		[Test]
		public void ViewGameDetailsPositive()
		{
			driver = Shared.TestSetup();

			HomePage homePage = new HomePage(driver);
			homePage.ViewGameDetails();

			GameDetailsPage gameDetailsPage = new GameDetailsPage(driver);
			Assert.IsTrue(gameDetailsPage.CheckCorrectGame("Test Game", "Test game description"));
			driver.Close();
		}
	}
}
