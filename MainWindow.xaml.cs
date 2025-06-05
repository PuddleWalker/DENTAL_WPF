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

namespace DENTAL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog loader = new();
        SaveFileDialog saver = new();
        BitmapImage SaveBitmap;
        BitmapImage LoadBitmap;
        BitmapImage InfoBitmap;
        About? a;
        bool isDark = true;
        DENTAL_Context dc;
        public MainWindow()
        {
            InitializeComponent();
            dc = new DENTAL_Context();
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Идентификатор", Binding = new Binding("Id") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Имя", Binding = new Binding("Name") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Фамилия", Binding = new Binding("Surname") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Дата начала работы", Binding = new Binding("BeginDate") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Место работы", Binding = new Binding("DentalClinicId") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Категория", Binding = new Binding("CategoryId") });
            DentistsGrid.Columns.Add(new DataGridTextColumn()
            { Header = "Специальность", Binding = new Binding("SpecialityId") });
            DentistsGrid.ItemsSource = dc.Dentists.ToList();
            
        }
        void CanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        void CanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = true;
            //if (CodeTextBox is null || CodeTextBox.Text == "")
            //{
            //    e.CanExecute = false;
            //}
            //if (NameTextBox is null || NameTextBox.Text == "")
            //{
            //    e.CanExecute = false;
            //}
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            //AnimateButton(saveButton);
            //saver.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
            //if (saver.ShowDialog() == true)
            //{
            //    p.Name = NameTextBox.Text;
            //    p.Group = GroupComboBox.SelectedIndex;
            //    p.ProductCode = CodeTextBox.Text;
            //    p.Manufacturer = ManufacturerTextBox.Text;
            //    p.CountryOfOrigin = CountryComboBox.SelectedIndex;
            //    p.Weight = int.Parse(WeightTextBox.Text);
            //    p.Package = PackageComboBox.SelectedIndex;
            //    p.CaloricValue = int.Parse(CaloricValueTextBox.Text);
            //    p.ProductionDate = ProductionDatePicker.SelectedDate;
            //    p.ShelfLife = ShelfLifeDatePicker.SelectedDate;
            //    p.Price = int.Parse(PriceTextBox.Text);

            //    json = JsonConvert.SerializeObject(p, Formatting.Indented);
            //    File.WriteAllText(saver.FileName, json);
            //    Status.Content = "Файл " + saver.FileName + " сохранен";
            //}
        }
        private void AnimateButton(Button button)
        {
            ScaleTransform scaleTransform = new ScaleTransform(1, 1);
            button.RenderTransform = scaleTransform;
            DoubleAnimation scaleXAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.8,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true
            };
            DoubleAnimation scaleYAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.8,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true
            };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            //AnimateButton(loadButton);
            //loader.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
            //if (loader.ShowDialog() == true)
            //{
            //    try
            //    {
            //        json = File.ReadAllText(loader.FileName);
            //        p = JsonConvert.DeserializeObject<Product>(json) ?? new();

            //        NameTextBox.Text = p.Name;
            //        GroupComboBox.SelectedIndex = p.Group;
            //        CodeTextBox.Text = p.ProductCode;
            //        ManufacturerTextBox.Text = p.Manufacturer;
            //        CountryComboBox.SelectedIndex = p.CountryOfOrigin;
            //        WeightTextBox.Text = p.Weight.ToString();
            //        PackageComboBox.SelectedIndex = p.Package;
            //        CaloricValueTextBox.Text = p.CaloricValue.ToString();
            //        ProductionDatePicker.SelectedDate = p.ProductionDate;
            //        ShelfLifeDatePicker.SelectedDate = p.ShelfLife;
            //        PriceTextBox.Text = p.Price.ToString();
            //        Status.Content = "Файл " + loader.FileName + " загружен";
            //    }
            //    catch
            //    {
            //        Status.Content = "Ошибка открытия файла " + loader.FileName;
            //    }
            //}
        }

        private void NumberFilter(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            a = new About(isDark);
            a.Show();
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (a != null) if (a.IsLoaded) a.Close();
        }

        private void Window_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (a != null) if (a.IsLoaded) a.Close();
        }
        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            isDark = true;
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
                    isDark = false;
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
            SaveBitmap = new();
            LoadBitmap = new();
            InfoBitmap = new();
            SaveBitmap.BeginInit();
            if (b) SaveBitmap.UriSource = new Uri("/Dsave.png", UriKind.Relative);
            else SaveBitmap.UriSource = new Uri("/Lsave.png", UriKind.Relative);
            SaveBitmap.EndInit();
            //SaveImage.Source = SaveBitmap;
            LoadBitmap.BeginInit();
            if (b) LoadBitmap.UriSource = new Uri("/Dload.png", UriKind.Relative);
            else LoadBitmap.UriSource = new Uri("/Lload.png", UriKind.Relative);
            LoadBitmap.EndInit();
            //LoadImage.Source = LoadBitmap;
            InfoBitmap.BeginInit();
            if (b) InfoBitmap.UriSource = new Uri("/Dinfo.png", UriKind.Relative);
            else InfoBitmap.UriSource = new Uri("/Linfo.png", UriKind.Relative);
            InfoBitmap.EndInit();
            //InfoImage.Source = InfoBitmap;

        }

        private void DentistsGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

        private void SQLiteClick(object sender, RoutedEventArgs e)
        {
            dc.Dispose();
            dc = new();
            DentistsGrid.ItemsSource = dc.Dentists.ToList();
        }
        private void SQLServerClick(object sender, RoutedEventArgs e)
        {
            dc.Dispose();
            dc = new(1);
            DentistsGrid.ItemsSource = dc.Dentists.ToList();
        }
    }
    public class Product
    {
        public string? Name { get; set; }
        public int Group { get; set; }
        public string? ProductCode { get; set; }
        public string? Manufacturer { get; set; }
        public int CountryOfOrigin { get; set; }
        public int Weight { get; set; }
        public int CaloricValue { get; set; }
        public int Package { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ShelfLife { get; set; }
        public int Price { get; set; }
    }
}