using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CVGS_Selenium_Tests
{
	public class ReviewPage
	{ 
		IWebDriver driver;
        By tdApproved = By.XPath("/html/body/div/main/table/tbody/tr/td[6]");
        By anchorEdit = By.XPath("/html/body/div/main/table/tbody/tr/td[7]/a[1]");
        By chkApprove = By.XPath("/html/body/div/main/div[1]/div/form/div[6]/label/input");
        By btnSaveReview = By.XPath("/html/body/div/main/div[1]/div/form/div[7]/input");

        public ReviewPage(IWebDriver driver)
		{
			this.driver = driver;
		}

        public bool CheckApproved()
        {
            if (driver.FindElement(tdApproved).Text == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EditReview()
        {
            driver.FindElement(anchorEdit).Click();
        }

        public void ChangeToApprove()
        {
            driver.FindElement(chkApprove).Click();
        }

        public void ApproveReview()
        {
            driver.FindElement(btnSaveReview).Click();
        }
    }
}
