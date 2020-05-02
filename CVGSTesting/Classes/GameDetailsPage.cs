using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Classes
{
	public class GameDetailsPage
	{
		IWebDriver driver;
		By textGameName = By.XPath("/html/body/div/main/div[2]/div[1]/h1");
		By textGameDescription = By.XPath("/html/body/div/main/div[2]/div[2]/div/div[1]/div/div[2]/p");
		By buttonAddToWishlist = By.XPath("/html/body/div/main/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[5]/div/form[1]/button");
        By textAreaGameReview = By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div/div[2]/form/div[2]/textarea");
        By buttonThumb = By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div/div[2]/form/div[1]/i");
        By spanReviewError = By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div/div[2]/form/div[2]/span/span");
        By buttonSubmitReview = By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div/div[2]/form/button");
        By divAdminReview = By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div/div[2]/div/div[1]");
        

        public GameDetailsPage(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void AddToWishlist()
		{
			driver.FindElement(buttonAddToWishlist).Click();
		}

        public void EnterShortReview(string review)
        {
            driver.FindElement(textAreaGameReview).SendKeys(review);
        }

        public void AddReview()
        {
            driver.FindElement(buttonSubmitReview).Click();
        }

        public void DislikeGame()
        {
            driver.FindElement(buttonThumb).Click();
        }

        public bool CheckCorrectGame(string gameName, string gameDescription)
		{
			if (driver.FindElement(textGameName).Text == gameName
				&& driver.FindElement(textGameDescription).Text == gameDescription)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        public bool CheckReviewError()
        {
            try
            {
                driver.FindElement(spanReviewError);
            }
            catch (NoSuchElementException e)
            {
                return true;
                throw;
            }
            return false;
        }

        public bool FindReview()
        {
            try
            {
                if (driver.FindElement(divAdminReview).Text == "admin's Review")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException e)
            {
                return false;
                throw;
            }
        }
    }
}
