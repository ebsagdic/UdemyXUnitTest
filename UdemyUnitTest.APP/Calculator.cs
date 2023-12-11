using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyUnitTest.APP
{
    public class Calculator
    {
        public ICalculatorService _calculatorService { get; set; }
        public Calculator(ICalculatorService calculatorService)
        {
            this._calculatorService = calculatorService;
        }
        public int Add(int a, int b)
        {
            return _calculatorService.Add(a,b);
        }
        public int  Multip(int a , int b)
        {
            return _calculatorService.Multip(a,b);
        }
    }
}
