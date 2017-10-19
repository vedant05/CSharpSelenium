using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.Generic;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium.Interactions;
using SWAUTCSharpFramework.Utilities;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SWAUTCSharpFramework
{
    public abstract class Page : Common
    {
        enum PropertyType
        {
            Id,
            Name,
            ClassName,
            CssClass
        }
        protected IWebDriver browser;
        protected abstract bool isValidPage();

        protected abstract void waitForPageLoad();

     // new  LogSetup();

       // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected Page(IWebDriver browser)
        {
            try
            {
                this.browser = browser;
                PageFactory.InitElements(browser, this);

                waitForPageLoad();
                verifyApplicationInCorrectPage();
            }
            catch (Exception)
            {
               
            }
        }



        private void verifyApplicationInCorrectPage()
        {
            if (!isValidPage())
            {
                String stepName = "Navigation to Page";
                String message = "The application is not in the expected page , current page: " +
                        browser.Title + "Page";

            }
        }

        public readonly TimeSpan ImplicitWait = new TimeSpan(0, 0, 0, 10);

        public bool ElementIsPresent(IWebElement element)
        {
            bool present = false;
            try
            {
                present = element.Displayed;
            }
            catch (Exception)
            {
            }
            browser.Manage().Timeouts().ImplicitlyWait(ImplicitWait);
            return present;
        }

        public bool WaitUntilElementIsPresent(IWebElement element, int timeout)
        {
            for (var i = 0; i < timeout; i++)
            {
                if (ElementIsPresent(element)) return true;
                Thread.Sleep(1000);
            }
            return false;
        }

        /***
	 * Method to click on a link(WebElement button)
	 * @param : WebElement
	 * @param : Element Name
	 ***/
        public void clickOn(IWebElement we, String elemName)
        {
            try
            {
                if (ElementIsPresent(we))
                {
                   HighLightElement(we);
                    we.Click();
                    Common.testStepPassed("Clicked on WebElement-" + elemName);
                    log.Info("Clicked on " + elemName);
                }
                else {
                    Common.testStepFailed("Unable to click on Element " + elemName);
                    log.Error("Unable to Click on " + elemName);
                }
            }
            catch (Exception ex)
            {
                Common.testStepFailed("Uanble to click on Element-" + elemName );
                log.Error("Exception Caught " + ex);
            }
        }

        /***
	 * Method to enter text in a textbox
	 * @param : WebElement - Textbox
	 * @param : Text to be entered
	 * @return :
	 ***/
        public bool enterText(IWebElement we, String text, String elemName)
        {
            try
            {
                waitForIsClickable(we);
                if (ElementIsPresent(we))
                {
                    we.Clear();
                    HighLightElement(we);
                    we.SendKeys(text);
                    Common.testStepPassed("Entered  " + elemName + " -> " + text );
                    log.Info("Entered Text " + text);
                    //TakeElementScreenshot( we);
                    return true;
                }
                else {
                    Common.testStepFailed("Element is not displayed, Unable to enter text->" + text);
                    log.Error("Unable to enter text " + text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception Caught while entering text  " + ex);
                Common.testStepFailed("Unable to enter text in the text field->" + text);
                return false;
            }
        }

        public void jsmoveToElement(IWebElement elem)
        {
            try
            {
                String str = elem.ToString();
                if (ElementIsPresent(elem))
                {
                    String mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('mouseover', true, false); arguments[0].dispatchEvent(evObj);} else if(document.createEventObject) { arguments[0].fireEvent('onmouseover');}";
                    ((IJavaScriptExecutor)browser).ExecuteScript(mouseOverScript, elem);
                }
                else {
                    Common.testStepFailed("Element is not displayed to mousehover ->" + str);
                }
            }
            catch (Exception ex)
            {
                Common.testStepFailed("Unable to mouse hover on the element,Exception is->" + ex.Message);
            }
        }

        public void navigateToNewWindow(String pageTitle)
        {
            bool blnNavigate = false;
            try
            {
                ReadOnlyCollection<string> handles = browser.WindowHandles;
                if (handles.Count == 1)
                {
                    handles = browser.WindowHandles;
                }
                if (handles.Count > 1)
                {
                    foreach (String windowHandle in handles)
                    {
                        String strActTitle = browser.SwitchTo().Window(windowHandle).Title;
                        if (strActTitle.Contains(pageTitle))
                        {
                            blnNavigate = true;
                            browser.Manage().Window.Maximize();
                            Console.WriteLine("navigated successfully");
                            log.Info("navigated successfully to the new window" + pageTitle);
                            break;
                        }
                    }
                    if (!blnNavigate)
                    {

                    }
                }
                else {
                    log.Error("Unable to navigate to new window" + pageTitle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                log.Error("Exception Caught while Navigating to the page" + pageTitle);
            }
        }

        //public void Selectdropdown(IWebElement element, string value, string elementtype)
        //{
        //    if (elementtype == "Id")
        //        new SelectElement(element).SelectByText(value);
        //    if (elementtype == "Name")
        //        new SelectElement(element).SelectByText(value);
        //}

        //public string GetText(IWebElement element, string elementtype)
        //{
        //    if (elementtype == "Id")
        //        return element.Text;
        //    if (elementtype == "Name")
        //        return element.Text;
        //    else return String.Empty;
        //}

        public String jsGetText(IWebElement we)
        {
            return (String)((IJavaScriptExecutor)browser).ExecuteScript(
                "return jQuery(arguments[0]).text();", we);
        }

        /**
	 * Method to click on a link(WebElement link)
	 * @param : WebElement
	 * @param : Element Name
	 */
        protected void jsClick(IWebElement we, String elemName)
        {
            try
            {
                ((IJavaScriptExecutor)browser).ExecuteScript("arguments[0].click();", we);
                Common.testStepPassed("Clicked on -" + elemName + "- Element");
                log.Info("Clicked on link " + elemName);
            }
            catch (Exception ex)
            {
                log.Error("Exception Caught while Clicking on link  " + ex);

                Common.testStepFailed("Uanble to click on Element-" + elemName + ", Exception is->" + ex.Message);
            }
        }

        /**
	 * Method to wait for element to load in the page
	 * @param WebElement
	 */
        protected Boolean waitForIsClickable(IWebElement we)
        {
            String str = null;
            try
            {
                new WebDriverWait(browser, TimeSpan.FromSeconds(30)).Until(ExpectedConditions
                        .ElementToBeClickable(we));
                if (ElementIsPresent(we))
                {
                    return true;
                }
                else {
                    Common.testStepFailed("Element is not visible after waiting for " + Common.elementLoadWaitTime + " Seconds");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.testStepFailed("Element is not visible after waiting for " + Common.elementLoadWaitTime + " Seconds, : " + str);
                return false;
            }
        }
        /***
	 * Method to Select value from dropdown by visible text
	 * @param       : we,strElemName,strVisibleText
	 * @return      : 
	 * @author      : 
	 * Modified By  :  
	 ***/

        public void selectByVisisbleText(IWebElement we, String strElemName, String strVisibleText)
        {
            try
            {
                if (we.Displayed)
                {
                    HighLightElement(we);
                    SelectElement sel = new SelectElement(we);
                    sel.SelectByText(strVisibleText);
                    Common.testStepPassed("Selected text -" + strVisibleText + " from dropdown->" + strElemName);
                    log.Info("Selected text from dropdown is " + strVisibleText);
                }
            }
            catch (Exception Ex)
            {
                log.Error("Exception Caught while selecting text from dropdown " + Ex);
                Common.testStepFailed("Unable to select text from the dropdown " + Ex);
            }
        }

/*** 
 * Method to Select value from dropdown by index
 * @param       : we,strElemName,index
 * @return      : 
 * @author      : 
 * Modified By  :  
 ***/

        public void selectByIndex(IWebElement we, String strElemName, int index)
        {
            try
            {
                if (we.Displayed)
                {
                    HighLightElement(we);
                    SelectElement sel = new SelectElement(we);
                    sel.SelectByIndex(index);
                    Common.testStepPassed("Selected value of index -" + index + " from dropdown->" + strElemName);
                    log.Info("Selected index value from dropdown is " + index);
                }
            }
            catch (Exception Ex)
            {
                log.Error("Exception Caught while selecting index from dropdown " + Ex);
                Common.testStepFailed("Unable to select index from the dropdown " + Ex);
            }
        }

        /***
* Method to Select value from dropdown by index
* @param       : we,strElemName,value
* @return      : 
* @author      : 
* Modified By  :  
***/

        public void selectByVisisbleValue(IWebElement we, String strElemName, string value)
        {
            try
            {
                if (we.Displayed)
                {
                    HighLightElement(we);
                    SelectElement sel = new SelectElement(we);
                    sel.SelectByValue(value);
                    Common.testStepPassed("Selected value  -" + value + " from dropdown->" + strElemName);
                    log.Info("Selected value from dropdown is " + value);
                }
            }
            catch (Exception Ex)
            {
                log.Error("Exception Caught while selecting value from dropdown " + Ex);
                Common.testStepFailed("Unable to select value from the dropdown " + Ex);
            }
        }

        //public Boolean waitForDropDown(IWebElement weDropDown)
        //{
        //    try
        //    {
        //        String str = weDropDown.ToString();
        //        if (waitForIsClickable(weDropDown))
        //        {
        //            for (int second = 0; ; second++)
        //            {
        //                if (second >= 20)
        //                {
        //                    Common.testStepFailed("Values in dropdown are not loaded after waiting for 20 seconds");
        //                    return false;
        //                }
        //                try
        //                {
        //                    SelectElement droplist = new SelectElement(weDropDown);
        //                    IList<IWebElement> s = droplist.Options;
        //                    int dropDownSize = s.size;
        //                    if (s.)
        //                    {
        //                        return true;
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    Common.testStepFailed("Exception Caught while waiting for dropdown loading,Message is->" + e);
        //                    return false;
        //                }
        //                Thread.Sleep(1000);
        //            }
        //        }
        //        else
        //        {
        //            Common.testStepFailed("Dropdown Element is not visible, Expected Property of DropDown is->" + str);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.testStepFailed("Exception Caught while waiting for dropdown loading,Message is->" + ex);
        //        return false;
        //    }
        //}

        /***
	 * Method to clear text in a textbox
	 * 
	 * @param : Element Name
	 * @return :
	 ***/
        public Boolean clearText(IWebElement we)
        {
            try
            {
                waitForIsClickable(we);
                if (isElementPresent(we))
                {
                    we.Clear();
                    log.Info("Able to clear text from " + we);
                    return true;
                }
                else
                {
                    Common.testStepFailed("Element is not displayed, Unable to Clear text->");
                    log.Error("Unable to clear text from " + we);
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception caught in clearText method " + ex);
                Common.testStepFailed("Unable to clear text in the text field");
                return false;
            }
        }

    /**
	 * Check if the element is present in the page
	 * @param element WebElement need to check
	 * @return True/False
	 */
        protected Boolean isElementPresent(IWebElement element)
        {
            try
            {
                new WebDriverWait(browser, TimeSpan.FromMinutes(1)).Until(ExpectedConditions
                        .ElementToBeClickable(element));
                if (element.Displayed)
                {
                    HighLightElement(element);
                    log.Info("Element is Present " + element);
                    return true;
                }
                else
                {
                    log.Error("Element is not Present " + element);
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception Caught in isElementPresent method " + ex);
                return false;
            }
        }

        /***
	 * Method to close a webpage
	 * @return      : 
	 ***/
        public void closeCurrentPage()
        {
            String str = null;
            try
            {
                 str=  browser.Title;
                browser.Close();
                Thread.Sleep(2000);
                ReadOnlyCollection<string> windows = browser.WindowHandles;
                foreach (String window in windows)
                {
                    browser.SwitchTo().Window(window);
                }
                Thread.Sleep(2000);
                Common.testStepPassed("Closed the current page with title->" + str);
            }
            catch (Exception e)
            {
                Common.testStepFailed("Unable to Close the current page with title->" + str);
            }
        }


        //*****************************************************************************************************************//
        //Start Alert pop ups
        //*****************************************************************************************************************//


        /***
         * Method to accept and close alert and return the text within the alert
         * @return :alert message
         ***/
        public String closeAlertAndReturnText()
        {
            String alertMessage = null;
            try
            {
                if (waitForAlert())
                {
                    IAlert alert = browser.SwitchTo().Alert();
                    alertMessage = alert.Text;
                    alert.Accept();
                    log.Info("Closed the alert successfully with text->" + alertMessage);
                    Common.testStepPassed("Closed the alert successfully with text->" + alertMessage);
                }
            }
            catch (Exception Ex)
            {
                log.Error("Unable to close the alert with text->" + alertMessage);
                Common.testStepFailed("Exception Caught while accepting the alert, Message is->" + Ex);
            }
            return alertMessage;
        }


        /***
         * Method to check for an alert for 20 seconds
         * @param       : Element Name
         * @return      : 
         * Modified By  :  
         ***/

        public Boolean isAlertPresent()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(50));
                wait.Until(ExpectedConditions.AlertIsPresent());
                log.Info("Alert is present");
                return true;
                
            }
            catch (Exception e)
            {
                log.Error("Alert is not present");
                Common.testStepFailed("Exception Caught while checking for the alert, Message is->" + e);
                return false;
            }
        }

        /**
	 * Method to wait for Alert present in the page
	 * @param 
	 */
        protected Boolean waitForAlert()
        {
            try
            {
                new WebDriverWait(browser, TimeSpan.FromSeconds(50)).Until(ExpectedConditions.AlertIsPresent());
                return true;
            }
            catch (Exception Ex)
            {
                Common.testStepFailed("Alert is not displayed after waiting for " + GenericKeywords.elementLoadWaitTime + " Seconds");
                return false;
            }
        }


        /**
         * Method to wait for Alert present in the page
         * @param 
         */
        protected Boolean waitForNewWindow(int expectedNumberOfWindows)
        {
            try
            {
               
                return true;
            }
            catch (Exception Ex)
            {
                Common.testStepFailed("New " + expectedNumberOfWindows + "th Window is not displayed after waiting for " + GenericKeywords.pageLoadWaitTime + " Seconds");
                return false;
            }
        }

        //*****************************************************************************************************************//
        //End Alert pop ups
        //*****************************************************************************************************************//


        //*****************************************************************************************************************//
        //Start Click 
        //*****************************************************************************************************************//

        /***
	 * Method to UnSelect the checkbox
	 * @param       : cbElement
	 * @return      : 
	 * Modified By  : 
	 ***/
        public Boolean unSelectCheckBox(IWebElement cbElement)
        {
            waitForIsClickable(cbElement);
            if (isElementPresent(cbElement))
            {
                try
                {
                    if (cbElement.Selected)
                    {
                        HighLightElement(cbElement);
                        cbElement.Click();
                    }
                    Common.testStepPassed("Unchecked the checkbox");
                    log.Info("Unchecked the checkbox");
                    return true;
                }
                catch (Exception e)
                {
                    Common.testStepFailed("Unable to check the checkbox->" + e);
                    log.Info("Unable to Unchecked the checkbox");
                    return false;
                }
            }
            else
            {
                Common.testStepFailed("Unable to UnSelect the checkbox(Element is not displayed)");
                return false;
            }
        }

        /***
         * Method to hover over an element
         * @param       : weMainMenuElement,weSubMenuElement
         * @return      : 
         * Modified By  :  
         ***/
        public void clickOnSubMenu(IWebElement weMain, IWebElement weSub)
        {
            try
            {
                String strMain = weMain.ToString();
                if (isElementPresent(weMain))
                {
                    HighLightElement(weMain);
                    Actions action = new Actions(browser);
                    action.MoveToElement(weMain).Click().Perform();
                    Common.testStepPassed("Hover over the Main menu item successfully");
                }
                else
                {
                    Common.testStepFailed("Unabel to hover Main menu(Element is not displayed), Expected Property of element is->" + strMain);
                }
            }
            catch (Exception Ex)
            {
                Common.testStepFailed("Exception Caught while hoverOver the main menu Item,Message is->" + Ex);
            }
            try
            {
                HighLightElement(weSub);
                String strSub = weSub.ToString();
                waitForIsClickable(weSub);
                if (isElementPresent(weSub))
                {
                    weSub.Click();
                    Common.testStepPassed("Clicked on the Sub menu item successfully");
                }
                else
                {
                    Common.testStepFailed("Sub Menu Element is not displayed, Expected Property of element is->" + strSub);
                }
            }
            catch (Exception ex)
            {
                Common.testStepFailed("Unable to Click on Sub menu Item,Exception is->" + ex);
            }
        }


        /***
         * Method to hover over an element
         * @param       : WebElement we
         * @return      : 
         * Modified By  :  
         ***/
        public Boolean moveToElement(IWebElement we)
        {
            try
            {
                String strMain = we.ToString();
                if (isElementPresent(we))
                {
                    HighLightElement(we);
                    Actions action = new Actions(browser);
                    action.MoveToElement(we).Build().Perform();
                    return true;
                }
                else
                {
                    Common.testStepFailed("Unable to move to element as element is not displayed, Expected Property of element is->" + strMain);
                    return false;
                }
            }
            catch (Exception e)
            {
                Common.testStepFailed("Error Occurred while Move to Element --> " + e);
                return false;
            }
        }

        /***
         * Method to drag and drop from source element to destination element
         * @param       : weSource,weDestination
         * @return      : 
         * Modified By  :  
         ***/
        public void dragAndDrop(IWebElement weSource, IWebElement weDestination)
        {
            String strSource = weSource.ToString();
            String strDest = weDestination.ToString();
            if (!isElementPresent(weSource))
            {
                Common.testStepFailed("Unable to perform DragAndDrop(Source element is not displayed), Expected Property of element is->" + strSource);
                return;
            }
            if (!isElementPresent(weDestination))
            {
                Common.testStepFailed("Unable to perform DragAndDrop(Destination element is not displayed), Expected Property of element is->" + strSource);
                return;
            }
            try
            {
                new Actions(browser).DragAndDrop(weSource, weDestination).Perform();
                Common.testStepPassed("Draged Source element and droped on Destination Element Successfully");
            }
            catch (Exception e)
            {
                Common.testStepFailed("Exception Caught while performing DragAndDrop, Mesage is->" + e);
            }
        }

        //*****************************************************************************************************************//
        //@@@@@@@@@@@@@@@@@@@@
        //*****************************************************************************************************************//
        public void TakeElementScreenshot(IWebElement element)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".Jpeg";
                Byte[] byteArray = ((ITakesScreenshot)browser).GetScreenshot().AsByteArray;
                System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(new System.IO.MemoryStream(byteArray));
                System.Drawing.Rectangle croppedImage = new System.Drawing.Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
                screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
                screenshot.Save(String.Format(@"C:\Volumes" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace + ' ' + e.Message);
            }
        }
        public void HighLightElement(IWebElement element)
        {
            try
            {
                var jsDriver = (IJavaScriptExecutor)browser;
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
            }
            catch (Exception e)
            {
                Common.testStepFailed("Exception Caught while performing DragAndDrop, Mesage is->" + e);
            }
        }






    }
}
