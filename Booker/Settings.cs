using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Booker
{
    public abstract class Setting
    {
        public string Name { get; set; } = String.Empty;
        public string Alias { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }

    public class SwitchSetting : Setting, INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        private bool value;
        public bool Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class StepperSetting : Setting, INotifyPropertyChanged
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int value;
        public int Value
        {
            get => value;
            set { 
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class SettingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SwitchSettingTemplate { get; set; }
        public DataTemplate StepperSettingTemplate { get; set; }
        public DataTemplate NoControlSettingTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case SwitchSetting sw:
                    return SwitchSettingTemplate;
                case StepperSetting st:
                    return StepperSettingTemplate;
                default:
                    return NoControlSettingTemplate;
            }
        }
    }
}
