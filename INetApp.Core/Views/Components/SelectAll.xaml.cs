using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INetApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAll : ContentView
    {
        public SelectAll()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsChechedProperty = BindableProperty.Create("IsCheched", typeof(bool), typeof(bool), false);

        public bool IsCheched
        {
            get => (bool)GetValue(IsChechedProperty);
            set => SetValue(IsChechedProperty, value);
        }
    }
}