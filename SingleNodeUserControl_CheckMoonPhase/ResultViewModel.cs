using Vishnu.Interchange;
using Vishnu.ViewModel;

namespace Vishnu_UserModules
{
    /// <summary>
    /// Funktion: ViewModel für das User-spezifische Result.
    /// Löst das ReturnObject eines Checkers in Properties auf.
    /// </summary>
    /// <remarks>
    /// File: ResultViewModel
    /// Autor: Erik Nagel
    ///
    /// 04.04.2020 Erik Nagel: erstellt
    /// </remarks>
    public class ResultViewModel : DynamicUserControlViewModelBase
    {
        /// <summary>
        /// Das Mondalter im Zyklus von Neumond bis Neumond in Tagen.
        /// </summary>
        public int MoonAge
        {
            get
            {
                int prop = this.GetResultProperty<int>(typeof(DateToMoonAge_ReturnObject), "MoonAge");
                return prop;
            }
        }

        /// <summary>
        /// Das Mondalter im Zyklus von Neumond bis Neumond in Tagen als Text aufbereitet.
        /// </summary>
        public string MoonAgeText
        {
            get
            {
                int age = this.MoonAge;
                string moonAgeText;
                if (age > 0)
                {
                    moonAgeText = string.Format($"Mondzyklus: {age}. Tag");
                }
                else
                {
                    moonAgeText = string.Format($"Mondzyklus-Beginn");
                }
                return moonAgeText;
            }
        }

        /// <summary>
        /// Konstruktor - übernimmt den DataContext des zugehörigen Vishnu-Knotens.
        /// </summary>
        /// <param name="parentViewModel">DataContext des zugehörigen Vishnu-Knotens.</param>
        public ResultViewModel(IVishnuViewModel parentViewModel)
        {
            this.ParentViewModel = parentViewModel;
            if (parentViewModel != null) // wg. ReferenceNullException im DesignMode
            {
                this.ParentViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(parentViewModel_PropertyChanged);
            }
            // TODO: Hier könnten zusätzliche, spezifische Buttons (Commands) initialisiert werden.
            // this._btnXYZ...RelayCommand = new RelayCommand(_btnXYZ...Execute, can_btnXYZ...Execute);
        }

        /// <summary>
        /// Wird ausgeführt, wenn sich die Result-Property des ViewModels
        /// des zugehörigen Vishnu-Knotens geändert hat.
        /// </summary>
        public void HandleResultPropertyChanged()
        {
            /*
            this.Dispatcher.Invoke(new Action(()
                => {
                        // Hier könnte zusätzliche, threadsichere Verarbeitung erfolgen.
                   })
            );
            */

            this.RaisePropertyChanged("MoonAge");
            this.RaisePropertyChanged("MoonAgeText");

            // TODO: Eventuelle zusätzliche Buttons müssten hier zum Update gezwungen werden,
            // da die Verarbeitung in einem anderen Thread läuft, z.B.:
            // this._btnXYZ...RelayCommand.UpdateCanExecuteState(this.Dispatcher);
        }

        private void parentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                this.HandleResultPropertyChanged();
            }
        }

        // TODO: Routinen für eventuelle zusätzliche, spezifische Buttons:
        // private void _btnXYZ...Execute(object parameter)
        // {
        //     string pattern = @"(\r\n)+";
        //     string replacement = "\n";
        //     string cleanSql = Regex.Replace(this.SQL, pattern, replacement, RegexOptions.IgnoreCase | RegexOptions.Singleline).Trim();
        // 
        //     MessageBox.Show(cleanSql, "Kopieren mit Strg-C");
        // }
        // 
        // private bool can_btnXYZ...Execute()
        // {
        //     return !String.IsNullOrEmpty(this....);
        // }

    }
}
