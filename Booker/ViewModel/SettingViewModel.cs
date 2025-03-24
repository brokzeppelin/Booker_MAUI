using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.ViewModel
{
    public class SettingViewModel
    {
        public ObservableCollection<Setting> Settings { get; set; }

        public SettingViewModel()
        {
            Settings = new ObservableCollection<Setting> {
                new SwitchSetting() { 
                    Alias = "DarkTheme", 
                    Name = "Dark theme", 
                    Value = 0, 
                    Description = "Choose color scheme for the app" 
                },

                new SwitchSetting() { 
                    Alias = "DailyReadingDose", 
                    Name = "Daily reading dose", 
                    Value = 0, 
                    Description = "Track your daily reading progress" 
                },

                new StepperSetting() { 
                    Alias = "PagesPerDay", 
                    Name = "Pages per day", 
                    Value = 20, 
                    Description = "Set a daily page goal for your reading",
                    Min = 1,
                    Max = 1000,
                    Step = 1
                },

                new SwitchSetting() { 
                    Alias = "EnableReadingGoalNotifications", 
                    Name = "Enable notifications when the reading goal is reached", 
                    Value = 0 
                },

                new StepperSetting() { 
                    Alias = "FontSize", 
                    Name = "Font size", 
                    Value = 18, 
                    Description = "Set the font size for the book",
                    Min = 5,
                    Max = 35,
                    Step = 1
                },

                new SwitchSetting() { 
                    Alias = "BoldText", 
                    Name = "Bold text", 
                    Value = 0, 
                    Description = "Adjust the font weight for the book" 
                }
            };
        }
    }
}
