using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToursApplication
{
    /// <summary>
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();

            var allTypes = ToursEntities.GetContext().Types.ToList();
            allTypes.Insert(0, new Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = allTypes;
            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            LViewTours.ItemsSource = ToursEntities.GetContext().Tours.ToList();
            UpdateTours();
        }

        private void UpdateTours()
        {
            var currentTours = ToursEntities.GetContext().Tours.ToList();

            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.Types.Contains(ComboType.SelectedItem as Type)).ToList();

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.IsActual).ToList();

            LViewTours.ItemsSource = currentTours.OrderBy(p => p.TicketCount).ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateTours();

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateTours();

        private void CheckActual_Checked(object sender, RoutedEventArgs e) => UpdateTours();

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e) => UpdateTours();
    }
}
