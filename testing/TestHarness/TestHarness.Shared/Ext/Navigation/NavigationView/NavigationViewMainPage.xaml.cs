﻿namespace TestHarness.Ext.Navigation.NavigationView;

[TestSectionRoot("NavigationView",TestSections.NavigationView, typeof(NavigationViewHostInit))]
public sealed partial class NavigationViewMainPage : BaseTestSectionPage
{
	public NavigationViewMainPage()
	{
		this.InitializeComponent();
	}

	public async void NavigationViewHomeClick(object sender, RoutedEventArgs e)
	{
		await NavigationRoot.Navigator()!.NavigateViewModelAsync<NavigationViewHomeViewModel>(this);
	}

}
