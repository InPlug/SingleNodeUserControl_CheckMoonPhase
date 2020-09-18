using System.Windows;
using Vishnu.Interchange;

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
            DynamicUserControl_ContentRendered
              += new RoutedEventHandler(content_Rendered);
        }

        /// <summary>
        /// Das ViewModell für das Result des zugehörigen Checkers.
        /// </summary>
        public ResultViewModel UserResultViewModel { get; set; }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                this.UserResultViewModel = new ResultViewModel((IVishnuViewModel)this.DataContext);
                ((IVishnuViewModel)this.DataContext).UserDataContext = this.UserResultViewModel;
            }
        }

        private void content_Rendered(object sender, RoutedEventArgs e)
        {
            if (this.UserResultViewModel != null)
            {
                this.UserResultViewModel.HandleResultPropertyChanged();
            }
        }
    }
}
