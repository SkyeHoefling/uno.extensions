﻿using NUnit.Framework;

namespace TestHarness.UITest;

public class Given_MainPage : TestBase
{
	[Test]
	public void When_SmokeTest()
	{
		App.WaitForElement(q => q.Marked("TestHarnessMainPageTitle"));
	}
}
