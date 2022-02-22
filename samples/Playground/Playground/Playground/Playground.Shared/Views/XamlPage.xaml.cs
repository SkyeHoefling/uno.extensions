﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Playground.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class XamlPage : Page
    {
		public XamlViewModel ViewModel { get; private set; }
        public XamlPage()
        {
            this.InitializeComponent();

			DataContextChanged += XamlPage_DataContextChanged;
        }

		private void XamlPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
		{
			ViewModel = args.NewValue as XamlViewModel;
		}
	}
}