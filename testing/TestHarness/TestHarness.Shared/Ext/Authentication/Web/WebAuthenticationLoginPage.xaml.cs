﻿
namespace TestHarness.Ext.Authentication.Web;
public sealed partial class WebAuthenticationLoginPage : Page
{
	public WebAuthenticationLoginViewModel? ViewModel => DataContext as WebAuthenticationLoginViewModel;
	public WebAuthenticationLoginPage()
	{
		this.InitializeComponent();
	}
}
