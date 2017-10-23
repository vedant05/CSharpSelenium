using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace SWAUTCSharpFramework
{
    public class LaunchScript : TestCases
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<string> ListofTestCases()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            wanted_path = wanted_path.Replace("file:\\", "");
            string pathString = System.IO.Path.Combine(wanted_path, "TestData");
            string newPathString = System.IO.Path.Combine(pathString, "TestCaseSettings.xls");
            List<string> listOfCases = Common.TestCaseSettingExel(newPathString);
            return listOfCases;
        }
       
        public static void Main(string[] args)
        {
            try {
                Common.Reports();
                log.Info("Log File Started");
                // TestCases Scripts = new TestCases();
                List<string> NumberofCases = ListofTestCases();
                // ... Loop with the foreach keyword.
                foreach (string value in NumberofCases)
                {

                    Type type = typeof(TestCases);
                    MethodInfo method = type.GetMethod(value);
                    TestCases c = new TestCases();
                    string result = (string)method.Invoke(c, null);


                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }





    }
}

