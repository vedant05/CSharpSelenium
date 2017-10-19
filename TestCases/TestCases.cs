using SWAUTCSharpFramework.MyFirstWebServiceReference;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace SWAUTCSharpFramework
{
    
    public  class TestCases : Common
    {
        public TestCases()
        {

        }

       
        private AdactinApplication adactinApplication;
        private AdactinHomePage adactinHomePage;
        private SearchHotel searchHotel;
        private SelectHotel selectHotel;
        private ForgetPassword ForgetPswd;
        private NewUserRegistration Registrationpage;
        public void testStart(String testCaseName, String testCaseDescription)
        {

            Common.testFailure = false;
            Common.currentStep = 0;

            adactinApplication = new AdactinApplication();
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            wanted_path = wanted_path.Replace("file:\\", "");
            string pathString = System.IO.Path.Combine(wanted_path, "TestData");
            string newPathString = System.IO.Path.Combine(pathString, "TestData.xls");
            Common.updateTestDataSet(testCaseName);

            Common.parent = Common.extent.StartTest(testCaseName, "<font size=4 color=black>" + testCaseDescription + "</font><br/>");
            adactinHomePage = adactinApplication.openAdactinApplication();

        }
        



        public   void TC_001()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "Login to Adactin Application");
            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);

                searchHotel = adactinHomePage.Login();
                if (searchHotel.VerifyLoginpage())
                { 
                    searchHotel.logoutFromApp();
                }
                else
                {
                    Common.testStepFailed("Unable to login to application, Check Username and password");
                }
                testStepInfoEnd(testDataSet);
            }
            testEnd();
        }



        public void TC_002()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "Search a hotel");
            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);

                searchHotel = adactinHomePage.Login();
                selectHotel = searchHotel.BookHotel();
               
                testStepInfoEnd(testDataSet);
            }
            testEnd();
        }

        public void TC_003()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "Book a hotel");
            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);
                searchHotel = adactinHomePage.Login();
                selectHotel = searchHotel.BookHotel();
                selectHotel.Selecthotel();

                testStepInfoEnd(testDataSet);
            }
            testEnd();
        }

        public void TC_004()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "Forget Password functionality");
            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);

                ForgetPswd = adactinHomePage.forgetpassword();

                testStepInfoEnd(testDataSet);
            }
            testEnd();
        }

        public void TC_005()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "New User Register functionality");
            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);

             

                Registrationpage = adactinHomePage.UserRegistration();

                testStepInfoEnd(testDataSet);
            }
            testEnd();
        }
        public void TC_006()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();
            String strTestCaseName = currentMethodName.Name;
            testStart(strTestCaseName, "WebService Calculator");


            foreach (String testDataSet in Common.testCaseDataSets)
            {
                Common.testCaseDataRow = returnRowNumber(testDataSet);
                testStepInfoStart(testDataSet);

                WebCalculator calculate= new WebCalculator();
                try
                {
                    calculate.Substraction();
                    calculate.Addition();
                  
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                testStepInfoEnd(testDataSet);
            }
            testEnd();

        }


        public void testEnd()
        {
            try
            {
                adactinApplication.close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Expception : " + e.Message);
            }
            finally
            {
                Common.extent.EndTest(Common.parent);
                Common.extent.Flush();
            }

        }
    }
}
