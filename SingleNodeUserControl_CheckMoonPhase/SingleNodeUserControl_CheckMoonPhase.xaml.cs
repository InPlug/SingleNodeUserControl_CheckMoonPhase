using System.Windows;
using Vishnu.Interchange;
using Vishnu.ViewModel;

namespace Vishnu_UserModules
{
    /// <summary>
    /// Interaktionslogik für UserView_CheckServer.xaml
    /// </summary>
    public partial class SingleNodeUserControl_CheckMoonPhase : DynamicUserControlBase
    {
        /// <summary>
        /// Konstruktor - hängt einen EventHandler in ContentRendered.
        /// </summary>
        public SingleNodeUserControl_CheckMoonPhase()
        {
            InitializeComponent();
            DynamicUserControl_ContentRendered += new RoutedEventHandler(content_Rendered);
        }

        /// <summary>
        /// Konkrete Überschreibung von GetUserResultViewModel, returnt ein spezifisches ResultViewModel.
        /// </summary>
        /// <param name="vishnuViewModel">Ein spezifisches ResultViewModel.</param>
        /// <returns></returns>
        protected override DynamicUserControlViewModelBase GetUserResultViewModel(IVishnuViewModel vishnuViewModel)
        {
            return new ResultViewModel((IVishnuViewModel)this.DataContext);
        }

        /// <summary>
        /// Hier wird aufgeräumt: ruft für alle User-Elemente, die Disposable sind, Dispose() auf.
        /// </summary>
        /// <param name="disposing">Bei true wurde diese Methode von der öffentlichen Dispose-Methode
        /// aufgerufen; bei false vom internen Destruktor.</param>
        protected override void Dispose(bool disposing)
        {
            DynamicUserControl_ContentRendered -= new RoutedEventHandler(content_Rendered);
            base.Dispose(disposing);
        }

        private void content_Rendered(object sender, RoutedEventArgs e)
        {
            if (this.UserResultViewModel != null)
            {
                ((ResultViewModel)this.UserResultViewModel).HandleResultPropertyChanged();
            }
        }
    }
}
