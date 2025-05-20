using System.ComponentModel;

namespace Booker.Controls;

public partial class StepperSettingControl : SettingControl, INotifyPropertyChanged
{
    public static BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(Stepper), 0.0,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is StepperSettingControl stepper)
                {
                    stepper.Value = (double)newValue;
                }
            }
            );
    public static BindableProperty MinProperty =
        BindableProperty.Create(nameof(Min), typeof(int), typeof(Stepper), 0);
    public static BindableProperty MaxProperty =
        BindableProperty.Create(nameof(Max), typeof(int), typeof(Stepper), 100);
    public static BindableProperty IncrementProperty =
        BindableProperty.Create(nameof(Increment), typeof(int), typeof(Stepper), 1);
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public double Min
    {
        get => (int)GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }
    public double Max
    {
        get => (int)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }
    public double Increment
    {
        get => (int)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }
    public StepperSettingControl()
	{
		InitializeComponent();
	}
}