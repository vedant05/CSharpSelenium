using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SWAUTCSharpFramework.MyFirstWebServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWAUTCSharpFramework
{

    public class AdactinHomePage : Page
    {
       // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static String HOME_PAGE_TITLE = "AdactIn.com - Hotel Reservation System";


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
                
                new WebDriverWait(browser, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists((By.XPath("//input[@id='login']"))));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
        public AdactinHomePage(IWebDriver browser) : base(browser)
        {
            //other stuff here
        }


        [FindsBy(How = How.XPath, Using = "//input[@name='username']")]
        public IWebElement weUsername { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement wePassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='login']")]
        public IWebElement btnLogin { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'Register.php')]")]
        public IWebElement Registerlink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'ForgotPassword.php')]")]
        public IWebElement weForgetpswdlink { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='emailadd_recovery']")]
        public IWebElement weEmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Submit']")]
        public IWebElement weSubmit { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'Register.php')]")]
        public IWebElement weNewRegister { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='username']")]
        public IWebElement weNewusername { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='full_name']")]
        public IWebElement weFullName { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='password']")]
        public IWebElement weNewPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='re_password']")]
        public IWebElement weRePassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='email_add']")]
        public IWebElement weEmailAdd { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='tnc_box']")]
        public IWebElement chkAgree { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Submit']")]
        public IWebElement btnRegister { get; set; }



       

        

        public ForgetPassword forgetpassword()
        {
            clickOn(weForgetpswdlink, "ForgetPassword");
            Thread.Sleep(2000);
            enterText(weEmailAddress, Common.retrieve("EmailAddress"),"Email");
            clickOn(weSubmit, "Submit");
            return new ForgetPassword(browser);


        }

        public SearchHotel Login()
        {
            enterText(weUsername, Common.retrieve("UserName"),"UserName");
            enterText(wePassword, Common.retrieve("Password"),"Password");
            Common.takeScreenshot("EnteredUsernamePassword");
            clickOn(btnLogin, "login");
            return new SearchHotel(browser);
        }


        public NewUserRegistration UserRegistration()
        {
            clickOn(weNewRegister, "New Registration");
            Thread.Sleep(3000);
            Common.testStepPassed("Clicked on link->New Registration");
            enterText(weNewusername, Common.retrieve("UserName"),"UserName");
            enterText(weNewPassword, Common.retrieve("Password"),"Password");
            enterText(weRePassword, Common.retrieve("RePassword"),"Confirm Password");
            enterText(weFullName, Common.retrieve("FullName"),"Full Name");
            enterText(weEmailAdd, Common.retrieve("EmailAddress"),"Email Address");
            clickOn(chkAgree, "Agree");
            Common.testStepPassed("Clicked on Agree checkbox");
            Common.takeScreenshot("EnteredAllMandatoryFields");
            clickOn(btnRegister, "Register");
            Common.testStepPassed("Clicked on Register Button");
            return new NewUserRegistration(browser);
        }

        //public Calculator CalculateValue()
        //{

        //    return new Calculator(browser);
        //}

    }
}