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
    public class SearchHotel : Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SearchHotel(IWebDriver browser) : base(browser)
        {
            //other stuff here
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='location']")]
        public IWebElement weLocation { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='hotels']")]
        public IWebElement weHotels { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id='room_type']")]
        public IWebElement weRoomtype { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='room_nos']")]
        public IWebElement weRoomnos { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='datepick_in']")]
        public IWebElement Checkindate { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='datepick_out']")]
        public IWebElement Checkoutdate { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id='adult_room']")]
        public IWebElement weAdults { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='child_room']")]
        public IWebElement weChildren { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='Submit']")]
        public IWebElement btnSearch { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='Reset']")]
        public IWebElement Reset { get; set; }


        [FindsBy(How = How.XPath, Using = "//a[text()='Logout']")]
        public IWebElement lnkLogout { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Click here to login again']")]
        public IWebElement lnkLoginAgain { get; set; }




    protected static String HOME_PAGE_TITLE = "AdactIn.com - Search Hotel";




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
                new WebDriverWait(browser, TimeSpan.FromSeconds(50)).Until(ExpectedConditions.ElementExists((By.XPath("//input[@id='Reset']"))));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public SelectHotel BookHotel()
        {
            selectByVisisbleText(weLocation, "Location", Common.retrieve("Location"));
            selectByVisisbleText(weHotels, "Hotel", Common.retrieve("Hotel"));
            selectByVisisbleText(weRoomtype, "RoomType", Common.retrieve("RoomType"));
            selectByVisisbleText(weRoomnos, "NumRooms", Common.retrieve("RoomNos"));
            enterText(Checkindate, Common.retrieve("CheckInDate"),"Check In Date");
            enterText(Checkoutdate, Common.retrieve("CheckOutDate"),"Check Out Date");
            selectByVisisbleText(weAdults, "Adults", Common.retrieve("Adults"));
            selectByVisisbleText(weChildren, "Children", Common.retrieve("Children"));
            clickOn(btnSearch, "Search");
            Common.takeScreenshot("SearchResults");
            return new SelectHotel(browser);
        }

        public Boolean VerifyLoginpage()
        {
            try
            {
                if (Reset.Displayed)
                {
                    Common.testStepPassed("Successfully logged in to the application");
                    Common.takeScreenshot("LoggedIn");
                    return true;
                }
                else
                {
                    Common.testStepFailed("Unable to login to application, Check Username and password");
                    return false;
                }
            }
            catch (Exception ex)
            {                
                Common.testStepFailed("Exception Caught, Message is->" + ex);
                return false;
            }
        }


        public AdactinHomePage logoutFromApp()
        {
            try
            {
                clickOn(lnkLogout, "Logout Link");
                clickOn(lnkLoginAgain, "Login Again Link");
            }
            catch (Exception ex)
            {
                Common.testStepFailed("Exception Caught, Message is->" + ex);
            }
            return new AdactinHomePage(browser);
        }

    }
}
