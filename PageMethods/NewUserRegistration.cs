using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace SWAUTCSharpFramework
{
    public class NewUserRegistration : Page
    {


        protected static String HOME_PAGE_TITLE = "AdactIn.com - New User Registration";


        public NewUserRegistration(IWebDriver browser) : base(browser)
        {
            //other stuff here
        }


        protected override bool isValidPage()
        {
            try
            {
                if (browser.Title.Trim().Contains(HOME_PAGE_TITLE))
                {

                    return true;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return false;
        }


        protected override void waitForPageLoad()
        {
            try
            {
                new WebDriverWait(browser, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists((By.XPath("//input[@id='Submit']"))));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }




    }
}

