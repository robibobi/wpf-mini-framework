using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Services.Dialog
{
    public interface IFileDialogService
    {

        /// <summary>
        /// Öffnet ein Dialogfenster zur Auswahl einer Datei
        /// </summary>
        /// <param name="path">Standard Pfad bei dem mit der Suche begonnen wird</param>
        /// <param name="allowedExtensions">Erlaubte Datei-Endungen z.B. {".png", ".bmp", ".jpg"}.
        /// Achtung, Endungen müssen mit Punkt angegeben werden</param>
        /// <returns>Pfad der ausgwählten Datei. String.Empty falls auf 
        /// "Abbrechen" geklickt wurde</returns>
        string OpenFileDialog(string path, string[] allowedExtensions);
    }
}
