namespace NUnitLite.Runner
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Reflection;
	using NUnit.Framework.Api;
	using NUnit.Framework.Internal;
	using NUnit.Framework.Internal.Filters;

	public class PortableUI
	{
		private readonly ITestAssemblyRunner runner;
		private readonly ITestListener listener;
		private readonly Assembly testAssembly;

		public PortableUI(ITestListener listener, Assembly testAssembly)
		{
			this.runner = new NUnitLiteTestAssemblyRunner(new NUnitLiteTestAssemblyBuilder());
			this.listener = listener;
			this.testAssembly = testAssembly;
		}

		public ITestResult Execute()
		{
			IDictionary loadOptions = new System.Collections.Generic.Dictionary<string, string>();
			if (!runner.Load(this.testAssembly, loadOptions))
			{
				AssemblyName assemblyName = AssemblyHelper.GetAssemblyName(this.testAssembly);
				throw new Exception(string.Format("No tests found in assembly {0}", assemblyName.Name));
			}

			return runner.Run(this.listener, TestFilter.Empty);
		}
	}
}