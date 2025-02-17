﻿using System;
using INetApp.ViewModels;
using Xamarin.Forms;

namespace INetApp.Views.Components
{
    public partial class MessageRow : ContentView
    {
        public MessageRow()
        {
            InitializeComponent();

        }

        //public static readonly BindableProperty IsCheckedChangedProperty = BindableProperty.Create(nameof(IsCheckedChanged), typeof(EventHandler), typeof(CheckBox), false, BindingMode.TwoWay);

        //public EventHandler IsCheckedChanged
        //{
        //    get => (EventHandler)GetValue(IsCheckedChangedProperty);
        //    set => SetValue(IsCheckedChangedProperty, value);
        //}


        public static readonly BindableProperty IsRowCheckedProperty = BindableProperty.Create(nameof(IsRowChecked), typeof(bool), typeof(bool), false, BindingMode.TwoWay);

        public bool IsRowChecked
        {
            get => (bool)GetValue(IsRowCheckedProperty);
            set => SetValue(IsRowCheckedProperty, value);
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                if (Parent?.Parent?.BindingContext is MessageViewModel vm)
                {
                    IsRowChecked = vm.IsRowSelect();
                }
            }
            catch
            {
            }
        }
    }
}