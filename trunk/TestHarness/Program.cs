using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nova.UnitTests;

namespace TestHarness
{
    public class Program
    {
        static void Main(string[] args)
        {
            NewGameTest test = new NewGameTest();
            test.Map800x400Test();
        }
    }
}
