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
    public class SelectHotel : Page
    {
        protected static String HOME_PAGE_TITLE = "AdactIn.com - Select Hotel";
        public SelectHotel(IWebDriver browser) : base(browser)
        {
            //other stuff here
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='radiobutton_0']")]
        public IWebElement rbSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='continue']")]
        public IWebElement btnContinue { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='cancel']")]
        public IWebElement btnCancel { get; set; }




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
                new WebDriverWait(browser, TimeSpan.FromSeconds(50)).Until(ExpectedConditions.ElementExists((By.XPath("//input[@id='cancel']"))));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }


        public void Selecthotel()
        {
            clickOn(rbSelect, "Selected");
           // Common.testStepPassed("Selected radio button");
            clickOn(btnContinue, "Continue");
           // Common.testStepPassed("Clicked on Continue Button");
        }

    }
}
