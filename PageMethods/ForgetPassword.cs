using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAUTCSharpFramework
{
    public class ForgetPassword : Page
    {
        protected static String HOME_PAGE_TITLE = "AdactIn.com - Forgot Password";


        public ForgetPassword(IWebDriver browser) : base(browser)
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
                new WebDriverWait(browser, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'index.php')]"))));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Forgot Password?')]")]
        public IWebElement lnkForgetPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='Submit']")]
        public IWebElement btnEmailPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='emailadd_recovery']")]
        public IWebElement txtEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Go back to Login page')]")]
        public IWebElement lnkGoToLoginPage { get; set; }

        public void SetPassword()
        {
            lnkForgetPassword.Click();
            WaitUntilElementIsPresent(btnEmailPassword, 1000);
            enterText(txtEmail,"vedantkumar05@gamil.com","Email");
            lnkGoToLoginPage.Click();
        }

    }
}
