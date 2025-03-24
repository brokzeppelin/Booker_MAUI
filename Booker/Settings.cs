using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker
{
    public abstract class Setting    
    {
        public string Name { get; set; } = String.Empty;
        public string Alias { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Value { get; set; }
    }

    public class SwitchSetting : Setting
    {
        
    }

    public class StepperSetting : Setting
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
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
