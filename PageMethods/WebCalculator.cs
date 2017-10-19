using SWAUTCSharpFramework;
using SWAUTCSharpFramework.MyFirstWebServiceReference;
using System;
using System.Web.Services;

[WebService(Namespace = "http://microsoft.com/webservices/")]
public class WebCalculator: Calculator
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public  void Substraction()
    {
        try
        {
            Calculator cal = new Calculator();
            int FirstNum = Convert.ToInt32(Common.retrieve("FirstNumber"));
            log.Info("First number for substraction on " + FirstNum);
            int SecondNum = Convert.ToInt32(Common.retrieve("SecondNumber"));
            log.Info("Second number for substraction on " + SecondNum);
            int res = cal.Subtract(FirstNum, SecondNum);
            log.Info("Result for Substraction is " + res);
            Common.testStepPassed("Entered FirstNumber " + FirstNum + " and SecondNumber " + SecondNum + " Substraction Result is -> " + res);
        }
        catch
        {
            log.Error("Invalid number for substraction");
            Common.testStepFailed("Invalid number for substraction");
        }
    }

    public void Addition()
    {
        try
        {
            Calculator cal = new Calculator();
            int FirstNum = Convert.ToInt32(Common.retrieve("FirstNumber"));
            log.Info("First number for Addition on " + FirstNum);
            int SecondNum = Convert.ToInt32(Common.retrieve("SecondNumber"));
            log.Info("Second number for Addition on " + SecondNum);
            int res = cal.Add(FirstNum, SecondNum);
            log.Info("Result for Addition is " + res);
            Common.testStepPassed("Entered firstNumber " + FirstNum + " and SecondNumber " + SecondNum + " Addition Result is -> " + res);
        }
        catch
        {
            log.Error("Invalid number for Addition");
            Common.testStepFailed("Invalid number for Addition");
        }
    }

    public void Multiplication()
    {
        try
        {
            Calculator cal = new Calculator();
            int FirstNum = Convert.ToInt32(Common.retrieve("FirstNumber"));
            log.Info("First number for Multiplication on " + FirstNum);
            int SecondNum = Convert.ToInt32(Common.retrieve("SecondNumber"));
            log.Info("Second number for Multiplication on " + SecondNum);
            int res = cal.Multiply(FirstNum, SecondNum);
            log.Info("Result for Multiplication is " + res);
            Common.testStepPassed("Entered firstNumber " + FirstNum + " and SecondNumber " + SecondNum + " Multiplication Result is -> " + res);
        }
        catch
        {
            log.Error("Invalid number for Multiplication");
            Common.testStepFailed("Invalid number for Multiplication");
        }
    }

    public void Division()
    {
        try
        {
        Calculator cal = new Calculator();
        int FirstNum = Convert.ToInt32(Common.retrieve("FirstNumber"));
        log.Info("First number for Division on " + FirstNum);
        int SecondNum = Convert.ToInt32(Common.retrieve("SecondNumber"));
        log.Info("Second number for Division on " + SecondNum);
        int res = cal.Divide(FirstNum, SecondNum);
        log.Info("Result for Division is " + res);
        Common.testStepPassed("Entered firstNumber " + FirstNum + " and SecondNumber " + SecondNum + " Division Result is -> " + res);
        }
        catch
        {
            log.Error("Invalid number for Division");
            Common.testStepFailed("Invalid number for Division");
        }

    }



}