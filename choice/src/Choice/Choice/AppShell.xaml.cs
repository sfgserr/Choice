﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
    }
}