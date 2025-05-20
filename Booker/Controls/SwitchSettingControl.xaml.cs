using System.Collections.Specialized;
using System.ComponentModel;

namespace Booker.Controls;

public partial class SwitchSettingControl : SettingControl
{
    public static BindableProperty IsToggledProperty =
        BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(SwitchSettingControl), false);
    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }
    public SwitchSettingControl()
	{
		InitializeComponent();
	}
}