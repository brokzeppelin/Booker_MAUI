using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker
{
    public static class Filer
    {
        // New file type for picking
        public static FilePickerFileType TxtFileType
        {
            get => GetFilePickerFileTypeTxt();
        }

        private static FilePickerFileType GetFilePickerFileTypeTxt()
        {
            return new FilePickerFileType(
                        new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                       { DevicePlatform.iOS, new[] { "public.text" } }, // UTType values  
                       { DevicePlatform.Android, new[] { "text/plain" } }, // MIME type  
                       { DevicePlatform.WinUI, new[] { ".txt" } }, // file extension  
                       { DevicePlatform.macOS, new[] { "txt" } },
                        });
        }

        public static async Task<string> GetPickedFileFullPath()
        {
            var filePicked =  await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a .txt file",
                FileTypes = TxtFileType
            });

            return filePicked?.FullPath ?? String.Empty;
        }
    }
}
