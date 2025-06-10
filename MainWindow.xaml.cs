using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using Theme.WPF.Themes;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Data;
using DENTAL_WPF.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DENTAL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage AddBitmap;
        BitmapImage InfoBitmap;
        About? a;
        EditWindow? D;
        ClinicEditWindow? CdD;
        bool isDark = true;
        DENTAL_Context dc;
        public ObservableCollection<Dentist> Dentists { get; set; }
        public ObservableCollection<DentalClinic> DentalClinics { get; set; }
        public ObservableCollection<Models.License> Licenses { get; set; }
        public ObservableCollection<Models.Category> Categories { get; set; }
        public ObservableCollection<Models.Speciality> Specialities { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            dc = new DENTAL_Context();
            Dentists = new(dc.Dentists.ToList());
            DentalClinics = new(dc.DentalClinics.ToList());
            Licenses = new(dc.Licenses.ToList());
            Categories = new(dc.Categories.ToList());
            Specialities = new(dc.Specialities.ToList());
            DataContext = this;
            RefreshDentists();
        }

        private void RefreshDentists()
        {
            Dentists.Clear();
            foreach (var d in dc.Dentists.ToList())
            {
                Dentists.Add(d);
            }
        }
        void CanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        

        private void NumberFilter(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            a = new About(isDark);
            a.ShowDialog();
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            IQueryable<int> ids;
            int nextMissingId;
            object emp;
            object editWindow;
            switch (ProgTabControl.SelectedIndex)
            {
                case 0:
                    ids = dc.Dentists.Select(d => d.Id);
                    nextMissingId = 1;

                    foreach (var id in ids)
                    {
                        if (id == nextMissingId)
                            nextMissingId++;
                        else
                            break;
                    }
                    emp = new Dentist
                    {
                        Id = nextMissingId,
                        Name = "",
                        Surname = "",
                        BeginDate = null,
                        CategoryId = null,
                        SpecialityId = null,
                        DentalClinicId = null
                    };

                    editWindow = new EditWindow((Dentist)emp, dc);
                    ((EditWindow)editWindow).ShowDialog();
                    dc.SaveChanges();
                    DentistsDataGrid.ItemsSource = dc?.Dentists.ToList();
                    break;
                case 1:
                    ids = dc.DentalClinics.Select(d => d.Id);
                    nextMissingId = 1;

                    foreach (var id in ids)
                    {
                        if (id == nextMissingId)
                            nextMissingId++;
                        else
                            break;
                    }
                    emp = new DentalClinic
                    {
                        Id = nextMissingId,
                        Name = "",
                        Country = "",
                        City = null,
                        LicenseId = null
                    };

                    editWindow = new ClinicEditWindow((DentalClinic)emp, dc);
                    ((ClinicEditWindow)editWindow).ShowDialog();
                    dc.SaveChanges();
                    ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList();
                    break;
                default:
                    MessageBox.Show("Пока не работает",
                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
            }
            
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            switch (((MenuItem)sender).Uid)
            {
                case "0":
                    ThemesController.SetTheme(ThemeType.DeepDark);
                    SetImages(true);
                    break;
                case "1":
                    ThemesController.SetTheme(ThemeType.SoftDark);
                    SetImages(true);
                    break;
                case "2":
                    ThemesController.SetTheme(ThemeType.DarkGreyTheme);
                    SetImages(true);
                    break;
                case "3":
                    ThemesController.SetTheme(ThemeType.GreyTheme);
                    SetImages(true);
                    break;
                case "4":
                    ThemesController.SetTheme(ThemeType.LightTheme);
                    SetImages(false);
                    break;
                case "5":
                    ThemesController.SetTheme(ThemeType.RedBlackTheme);
                    SetImages(true);
                    break;
                case "6":
                    SetImages(true);
                    break;
            }

            e.Handled = true;
        }
        private void SetImages(bool b)
        {
            AddBitmap = new();
            InfoBitmap = new();
            AddBitmap.BeginInit();
            if (b) AddBitmap.UriSource = new Uri("/Dadd.png", UriKind.Relative);
            else AddBitmap.UriSource = new Uri("/Ladd.png", UriKind.Relative);
            AddBitmap.EndInit();
            
            InfoBitmap.BeginInit();
            if (b) InfoBitmap.UriSource = new Uri("/Dinfo.png", UriKind.Relative);
            else InfoBitmap.UriSource = new Uri("/Linfo.png", UriKind.Relative);
            InfoBitmap.EndInit();

        }

        private void SQLiteClick(object sender, RoutedEventArgs e)
        {
            dc.Dispose();
            dc = new();
            DentistsDataGrid.ItemsSource = dc.Dentists.ToList();
        }
        private void SQLServerClick(object sender, RoutedEventArgs e)
        {
            dc.Dispose();
            dc = new(1);
            DentistsDataGrid.ItemsSource = dc.Dentists.ToList();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var emp = DentistsDataGrid.SelectedItem as Dentist;
            if (emp is not null)
            {
                D = new(emp, dc);
                D.ShowDialog();
                dc.SaveChanges();
                DentistsDataGrid.ItemsSource = dc?.Dentists.ToList();
            }
            else
            {
                Status.Content = "Для редактирования дантиста выберите его !!!";
            }
        }
        private void ClinicEditClick(object sender, RoutedEventArgs e)
        {
            var emp = ClinicsDataGrid.SelectedItem as DentalClinic;
            if (emp is not null)
            {
                CdD = new(emp, dc);
                CdD.ShowDialog();
                dc.SaveChanges();
                ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList();
            }
            else
            {
                Status.Content = "Для удаления редактирования клиники выберите её !!!";
            }
        }
        private void CategoryEditClick(object sender, RoutedEventArgs e)
        {
            var emp = ClinicsDataGrid.SelectedItem as DentalClinic;
            if (emp is not null)
            {
                CdD = new(emp, dc);
                CdD.ShowDialog();
                dc.SaveChanges();
                ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList();
            }
            else
            {
                Status.Content = "Для удаления редактирования клиники выберите её !!!";
            }
        }
        private void SpecialityEditClick(object sender, RoutedEventArgs e)
        {
            var emp = ClinicsDataGrid.SelectedItem as DentalClinic;
            if (emp is not null)
            {
                CdD = new(emp, dc);
                CdD.ShowDialog();
                dc.SaveChanges();
                ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList();
            }
            else
            {
                Status.Content = "Для удаления редактирования клиники выберите её !!!";
            }
        }
        private void LicenseEditClick(object sender, RoutedEventArgs e)
        {
            var emp = ClinicsDataGrid.SelectedItem as DentalClinic;
            if (emp is not null)
            {
                CdD = new(emp, dc);
                CdD.ShowDialog();
                dc.SaveChanges();
                ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList();
            }
            else
            {
                Status.Content = "Для удаления редактирования клиники выберите её !!!";
            }
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные ?",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // SelectedIndex
                var emp = DentistsDataGrid.SelectedItem as Dentist;
                if (emp is not null)
                {
                    dc.Dentists.Remove(emp);
                    dc.SaveChanges();
                    DentistsDataGrid.ItemsSource = dc?.Dentists.ToList();
                }
                else
                {
                    Status.Content = "Для удаления сотрудника выберите его !!!";
                }
            }
        }
        private void ClinicDeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные ?",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // SelectedIndex
                var emp = ClinicsDataGrid.SelectedItem as DentalClinic;
                if (emp is not null)
                {
                    dc.DentalClinics.Remove(emp);
                    dc.SaveChanges();
                    ClinicsDataGrid.ItemsSource = dc?.DentalClinics.ToList(); // обновляем список
                }
                else
                {
                    Status.Content = "Для удаления клиники выберите её !!!";
                }
            }
        }
        private void CategoryDeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные ?",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // SelectedIndex
                var emp = CategoriesDataGrid.SelectedItem as Models.Category;
                if (emp is not null)
                {
                    dc.Categories.Remove(emp);
                    dc.SaveChanges();
                    CategoriesDataGrid.ItemsSource = dc?.Categories.ToList(); // обновляем список
                }
                else
                {
                    Status.Content = "Для удаления категории выберите её !!!";
                }
            }
        }
        private void SpecialityDeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные ?",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // SelectedIndex
                var emp = SpecialitiesDataGrid.SelectedItem as Models.Speciality;
                if (emp is not null)
                {
                    dc.Specialities.Remove(emp);
                    dc.SaveChanges();
                    SpecialitiesDataGrid.ItemsSource = dc?.Specialities.ToList(); // обновляем список
                }
                else
                {
                    Status.Content = "Для удаления специальности выберите её !!!";
                }
            }
        }
        private void LicenseDeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные ?",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // SelectedIndex
                var emp = LicensesDataGrid.SelectedItem as Models.License;
                if (emp is not null)
                {
                    dc.Licenses.Remove(emp);
                    dc.SaveChanges();
                    LicensesDataGrid.ItemsSource = dc?.Licenses.ToList(); // обновляем список
                }
                else
                {
                    Status.Content = "Для удаления лицензии выберите её !!!";
                }
            }
        }
    }
}