using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Dialog
{
    public class FileDialogService : IFileDialogService
    {

        public string OpenFileDialog(string path, string[] allowedExtensions)
        {
            string result = String.Empty;
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = CreateDialogFilter(allowedExtensions);

            if(!String.IsNullOrEmpty(path))
                dialog.FileName = path;

            bool? b = dialog.ShowDialog();

            return b.HasValue && b.Value ? dialog.FileName : String.Empty;
        }


        private string CreateDialogFilter(string [] allowedExtensions)
        {
            string filter = String.Empty;
            foreach (string extension in allowedExtensions)
                filter += " " + extension + " " + "|*" + extension + "|";
            // Letzen | entfernen
            if(filter != String.Empty)
                filter = filter.Remove(filter.Length - 1);
            return filter;
        }
    }
}
