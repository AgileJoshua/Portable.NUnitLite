// *****************************************************
// Copyright 2007, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using NUnit.Framework.Api;

namespace NUnitLite.Tests
{
	using NUnit.Framework.Internal;

	public class RecordingTestListener : ITestListener
    {
#if !PORTABLE
        public string Events = string.Empty;
#endif

        public void TestStarted(ITest test)
		{
#if PORTABLE
			if (test is TestAssembly)
			{
				TestAssembly testAssembly = test as TestAssembly;
				Console.WriteLine(testAssembly.Name);
			}
			else if (test is TestFixture)
			{
				TestFixture testFixture = test as TestFixture;
				Console.WriteLine("\t{0}", testFixture.Name);
			}
			else if(test is TestMethod)
			{
				TestMethod testMethod = test as TestMethod;
				Console.Write("\t\t{0} => ", testMethod.Name);
			}
#else
			Events += string.Format("<{0}:", test.Name);
#endif
		}

        public void TestFinished(ITestResult result)
        {
#if PORTABLE
	        if(result.Test is TestMethod)
	        {
		        if(result.ResultState == ResultState.Inconclusive)
		        {
			        Console.ForegroundColor = ConsoleColor.Yellow;
		        }
		        else if(result.ResultState != ResultState.Success)
		        {
			        Console.ForegroundColor = ConsoleColor.Red;
		        }
		        else if(result.ResultState == ResultState.Success)
		        {
			        Console.ForegroundColor = ConsoleColor.Green;
		        }

		        Console.WriteLine(result.ResultState);

		        Console.ResetColor();
	        }
#else
			Events += string.Format(":{0}>", result.ResultState);
#endif
        }

	    public void TestOutput(TestOutput output)
	    {

		}
    }
}
