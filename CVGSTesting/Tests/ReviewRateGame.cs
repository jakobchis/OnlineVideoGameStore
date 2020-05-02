using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CVGS_Selenium_Tests.Classes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CVGS_Selenium_Tests.Tests
{
    // Requirements 12 rate a game & 13 review a game
    [TestFixture]
    class ReviewRateGame
	{
        IWebDriver driver;

        [Test]
        //Trying to add a too short review. Should fail.
        public void AddReviewFail()
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
            gameDetailsPage.EnterShortReview("good");
            gameDetailsPage.AddReview();
            Thread.Sleep(3000);
            Assert.IsFalse(gameDetailsPage.CheckReviewError());
            driver.Close();
        }

        [Test]
        //Trying to add a review with a dislike/not-recommended. Should succeed.
        public void AddReviewSuccessWithDislike()
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
            gameDetailsPage.EnterShortReview("One of the worst games ever made. Don't waste your time.");
            gameDetailsPage.DislikeGame();
            gameDetailsPage.AddReview();
            Thread.Sleep(3000);
            Assert.IsTrue(gameDetailsPage.CheckReviewError());
            driver.Close();
        }

        [Test]
        //Trying to approve the review. Should succeed.
        public void ApproveReview()
        {
            driver = Shared.TestSetup();

            HomePage homePage = new HomePage(driver);
            homePage.Login();

            LoginPage loginPage = new LoginPage(driver);
            loginPage.EnterUsername("admin");
            loginPage.EnterPassword("Password123!");
            loginPage.Login();

            homePage.ViewAdminReviewPage();

            ReviewPage reviewPage = new ReviewPage(driver);
            reviewPage.EditReview();
            Thread.Sleep(1000);
            reviewPage.ChangeToApprove();
            reviewPage.ApproveReview();
            Thread.Sleep(1000);
            Assert.IsTrue(reviewPage.CheckApproved());
            driver.Close();
        }

        [Test]
        //Trying to view the newly approved review without logging in. Should succeed.
        public void FindReviewWithoutLogin()
        {
            driver = Shared.TestSetup();

            HomePage homePage = new HomePage(driver);

            homePage.ViewGameDetails();

            GameDetailsPage gameDetailsPage = new GameDetailsPage(driver);
            Assert.IsTrue(gameDetailsPage.FindReview());
            driver.Close();
        }
    }
}
