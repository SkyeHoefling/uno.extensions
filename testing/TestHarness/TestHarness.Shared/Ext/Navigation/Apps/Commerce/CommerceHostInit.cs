﻿namespace TestHarness.Ext.Navigation.Apps.Commerce;

public class CommerceHostInit : IHostInitialization
{
	public IHost InitializeHost()
	{

		return UnoHost
				.CreateDefaultBuilder()
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif

				// Add platform specific log providers
				.UseLogging(configure: (context, logBuilder) =>
				{
					var host = context.HostingEnvironment;
					// Configure log levels for different categories of logging
					logBuilder.SetMinimumLevel(host.IsDevelopment() ? LogLevel.Trace : LogLevel.Warning);
				})

				// Only use this syntax for UI tests - use UseConfiguration in apps
				.ConfigureAppConfiguration((ctx, b) =>
				{
					b.AddEmbeddedConfigurationFile<App>("TestHarness.Ext.Navigation.Apps.Commerce.appsettings.logging.json");
				})

				.UseSerilog(true, true)

				// Enable navigation, including registering views and viewmodels
				.UseNavigation(RegisterRoutes)

				.UseToolkitNavigation()

				.Build(enableUnoLogging: true);
	}


	private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
	{

		views.Register(
				new ViewMap(ViewModel: typeof(CommerceShellViewModel)),
				new ViewMap<CommerceLoginPage, CommerceLoginViewModel>(ResultData: typeof(CommerceCredentials)),
				new DataViewMap<CommerceHomePage,CommerceHomeViewModel, CommerceCredentials>(),
				new ViewMap<CommerceProductsPage, CommerceProductsViewModel>(),
				new DataViewMap<CommerceProductDetailsPage, CommerceProductDetailsViewModel, CommerceProduct>(),
				new ViewMap<CommerceDealsPage, CommerceDealsViewModel>(),
				new ViewMap<CommerceProfilePage, CommerceProfileViewModel>()
				);


		routes
			.Register(
				new RouteMap("", View: views.FindByViewModel<CommerceShellViewModel>(),
						Nested: new RouteMap[]
						{
							new RouteMap("Login", View: views.FindByViewModel<CommerceLoginViewModel>()),
							new RouteMap("Home", View: views.FindByView<CommerceHomePage>(),
									Nested: new RouteMap[]{
										new RouteMap("Deals",
											View: views.FindByViewModel<CommerceDealsViewModel>(),
											IsDefault: true,
												Nested: new RouteMap[]{
													new RouteMap("DealsTab", IsDefault: true),
													new RouteMap("FavoritesTab")
												}),
										new RouteMap("DealsProduct",
												View: views.FindByViewModel<CommerceProductDetailsViewModel>(),
												DependsOn:"Deals"),
										new RouteMap("Products",
												View: views.FindByViewModel<CommerceProductsViewModel>()),
										new RouteMap("Product",
												View: views.FindByViewModel<CommerceProductDetailsViewModel>(),
												DependsOn:"Products"),

										new RouteMap("Profile", View: views.FindByViewModel<CommerceProfileViewModel>())
									})
						}));
	}
}


