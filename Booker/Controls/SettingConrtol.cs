using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.Controls
{
    public abstract class SettingControl : ContentView
    {
        public readonly static BindableProperty NameProperty =
                BindableProperty.Create(nameof(Name), typeof(string), typeof(SettingControl), string.Empty);
        public readonly static BindableProperty AliasProperty =
            BindableProperty.Create(nameof(Alias), typeof(string), typeof(SettingControl), string.Empty);
        public readonly static BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(SettingControl), string.Empty);

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string Alias
        {
            get => (string)GetValue(AliasProperty);
            set => SetValue(AliasProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
    }
}
