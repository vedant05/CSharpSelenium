using System;
using OpenQA.Selenium;
using System.Threading;
using NUnit.Framework;
using SWAUTCSharpFramework.Utilities;

namespace SWAUTCSharpFramework
{

    public class AdactinApplication
    {
      
        private String url;
        private String env;
        private Page page;

        public static IWebDriver browser { get; set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

      
        public AdactinApplication()
        {
            String strbrowser = Common.GetConfigProperty("Browser");
            browser = Common.getdriver(strbrowser);
            Common.browser = browser;
            this.url = Common.GetConfigProperty("url");
          
        }



        public AdactinHomePage openAdactinApplication()
        {
            try
            {
                Common.browser = browser;
                browser.Navigate().GoToUrl(Common.GetConfigProperty("url"));
                browser.Manage().Window.Maximize();
               			
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new AdactinHomePage(browser);

        }

        //=================================================================

        /**
         * method to make a thread sleep for customized time in milliseconds
         * @param milliseconds
         */
        protected void sleep(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

            void deleteAllCookies()
            {
                try
                {
                browser.Manage().Cookies.DeleteAllCookies();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            /**
             * Get relative path of the framework
             * @return
             */
            public string getRelativePath()
            {
                string relativePath = System.AppDomain.CurrentDomain.BaseDirectory;

                return relativePath;
            }

            public void close()
            {
                try {
                deleteAllCookies();
                browser.Quit();
                    if (GenericKeywords.testFailure)
                    {
                        Assert.Fail("Testcasefailed");
                    }
                } catch (Exception Ex)
                {
                    Console.WriteLine("Unable to Close Application" + Ex);
                }
            }

        //public static string TASKLIST = "tasklist";
        //public static string KILL = "taskkill /IM ";

        //public static void IsProcessRunging(String serviceName)
        //{


        //    foreach (var process in Process.GetProcessesByName("whatever"))
        //    {
        //        process.Kill();
        //    }

        //    //          Process p = Runtime.getRuntime().exec(TASKLIST);
        //    //              BufferedReader reader = new BufferedReader(new InputStreamReader(
        //    //                      p.getInputStream()));
        //    //	String line;
        //    //	while ((line = reader.readLine()) != null) {
        //    //		if (line.Contains(serviceName)) {
        //    //			return true;
        //    //		}
        //    //	}

        //    //	return false;

        //    //}

        //    public static void KillProcess(String serviceName)
        //    {

        //        Runtime.getRuntime().exec(KILL + serviceName);

        //    }
        }
    }