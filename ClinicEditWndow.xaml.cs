using DENTAL_WPF.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DENTAL_WPF
{
    public partial class ClinicEditWindow : Window
    {
        private bool isChanged = false;
        private DentalClinic den;
        private readonly DENTAL_Context dc;
        private bool isNewDentist = false;
        public List<string> Countries = new(){"Australia", "Austria", "Azerbaijan", "Albania", "Algeria", "Angola", "Andorra", "Antigua and Barbuda", "Argentina", "Armenia", "Afghanistan", "The Bahamas", "Bangladesh", "Barbados", "Bahrain", "Belarus", "Belize", "Belgium", "Benin", "Bulgaria", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Burkina Faso", "Burundi", "Bhutan", "Vanuatu", "Vatican City", "UK", "Hungary", "Venezuela", "East Timor", "Vietnam", "Gabon", "Haiti", "Guyana", "Gambia", "Ghana", "Guatemala", "Guinea", "Guinea-Bissau", "Germany", "Honduras", "Grenada", "Greece", "Georgia", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Egypt", "Zambia", "Zimbabwe", "Israel", "India", "Indonesia", "Jordan", "Iraq", "Iran", "Ireland", "Iceland", "Spain", "Italy", "Yemen", "Cabo Verde", "Kazakhstan", "Cambodia", "Cameroon", "Canada", "Qatar", "Kenya", "Cyprus", "Kyrgyzstan", "Kiribati", "China", "Colombia", "Comoros", "Republic of the Congo", "DR Congo", "Costa Rica", "Côte d'Ivoire", "Cuba", "Kuwait", "Laos", "Latvia", "Lesotho", "Liberia", "Lebanon", "Libya", "Lithuania", "Liechtenstein", "Luxembourg", "Mauritius", "Mauritania", "Madagascar", "Malawi", "Malaysia", "Mali", "Maldives", "Malta", "Morocco", "Marshall Islands", "Mexico", "Mozambique", "Moldova", "Monaco", "Mongolia", "Myanmar", "Namibia", "Nauru", "Nepal", "Niger", "Nigeria", "Netherlands", "Nicaragua", "New Zealand", "Norway", "UAE", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Poland", "Portugal", "South Korea", "North Macedonia", "Russia", "Rwanda", "Romania", "El Salvador", "Samoa", "San Marino", "São Tomé and Príncipe", "Saudi Arabia", "Eswatini", "Seychelles", "Senegal", "Saint Vincent and the Grenadines", "Saint Kitts and Nevis", "Saint Lucia", "Serbia", "Singapore", "Syria", "Slovakia", "Slovenia", "USA", "Solomon Islands", "Somalia", "Sudan", "Suriname", "Sierra Leone", "Tajikistan", "Thailand", "Tanzania", "Togo", "Tonga", "Trinidad and Tobago", "Tuvalu", "Tunisia", "Turkmenistan", "Turkey", "Uganda", "Uzbekistan", "Ukraine", "Uruguay", "Federated States of Micronesia", "Fiji", "Philippines", "Finland", "France", "Croatia", "Chad", "Montenegro", "Czech Republic", "Chile", "Switzerland", "Sweden", "Sri Lanka", "Ecuador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "South Africa", "South Korea", "South Sudan", "Jamaica", "Japan"};

        public ClinicEditWindow(DentalClinic d, DENTAL_Context context)
        {
            InitializeComponent();

            den = d ?? throw new ArgumentNullException(nameof(d));
            dc = context ?? throw new ArgumentNullException(nameof(context));
            isNewDentist = false;

            InitializeFields();
        }



        private void InitializeFields()
        {
            NameTextBox.Text = den.Name ?? string.Empty;
            CountryTextBox.Text = den.Country ?? string.Empty;
            CityTextBox.Text = den.City ?? string.Empty;

            if (dc.Licenses != null)
            {
                LicenseIdComboBox.ItemsSource = dc.Licenses.Select(c => c.Id).ToList();
                LicenseIdComboBox.SelectedValue = den.LicenseId;
            }
            else
            {
                LicenseIdComboBox.ItemsSource = new List<int>();
                LicenseIdComboBox.IsEnabled = false;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

            den.Name = NameTextBox.Text;
            den.Country = CountryTextBox.Text;
            den.City = CityTextBox.Text;
            den.LicenseId = (int)LicenseIdComboBox.SelectedValue;

            if (dc.DentalClinics.Find(den.Id) != null) dc.Update(den);
            else dc.Add(den);
        }

        void CancelClick(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = den.Name ?? string.Empty;
            CountryTextBox.Text = den.Country ?? string.Empty;
            CityTextBox.Text = den.City ?? string.Empty;
            if (dc.Licenses != null) LicenseIdComboBox.SelectedValue = den.LicenseId;

        }


        public void CanExecuteCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

            //if (NameTextBox?.Text != den?.Name)
            //    e.CanExecute = true;

            //if (SurnameTextBox?.Text != den?.Surname)
            //    e.CanExecute = true;

            //if (BeginDatePicker?.SelectedDate.HasValue == true && den.BeginDate.HasValue)
            //{
            //    if (DateOnly.FromDateTime(BeginDatePicker.SelectedDate.Value) != den.BeginDate.Value)
            //        e.CanExecute = true;
            //}
            //else if (BeginDatePicker?.SelectedDate.HasValue != den.BeginDate.HasValue)
            //{
            //    e.CanExecute = true;
            //}

            //if (SpecialityIdComboBox?.SelectedValue is int selectedSpecId && den.SpecialityId.HasValue)
            //{
            //    if (selectedSpecId != den.SpecialityId.Value)
            //        e.CanExecute = true;
            //}
            //else if (SpecialityIdComboBox?.SelectedValue is null && den.SpecialityId.HasValue)
            //{
            //    e.CanExecute = true;
            //}

            //if (CategoryIdComboBox?.SelectedValue is int selectedCatId && den.CategoryId.HasValue)
            //{
            //    if (selectedCatId != den.CategoryId.Value)
            //        e.CanExecute = true;
            //}
            //else if (CategoryIdComboBox?.SelectedValue is null && den.CategoryId.HasValue)
            //{
            //    e.CanExecute = true;
            //}

            //if (ClinicIdComboBox?.SelectedValue is int selectedClinicId && den.DentalClinicId.HasValue)
            //{
            //    if (selectedClinicId != den.DentalClinicId.Value)
            //        e.CanExecute = true;
            //}
            //else if (ClinicIdComboBox?.SelectedValue is null && den.DentalClinicId.HasValue)
            //{
            //    e.CanExecute = true;
            //}
            //if (e.CanExecute == true) isChanged = true;
            //else isChanged = false;
        }


        void CanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            if (NameTextBox is null || NameTextBox.Text == "")
            {
                e.CanExecute = false;
            }
            if (LicenseIdComboBox != null)
            {
                if (LicenseIdComboBox.SelectedValue is null)
                {
                    e.CanExecute = false;
                }
            }
        }

        private void EditWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChanged && !DialogResult.GetValueOrDefault())
            {
                var result = MessageBox.Show("Вы действительно хотите отменить изменения?",
                                             "Подтверждение",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        private void CountryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = CountryTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                SuggestionsPopup.IsOpen = false;
                return;
            }

            var filtered = Countries.Where(s => s.ToLower().Contains(input)).ToList();
            SuggestionsList.ItemsSource = filtered;
            SuggestionsPopup.IsOpen = filtered.Count > 0;
        }
        private void SuggestionsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SuggestionsList.SelectedItem is string selected)
            {
                CountryTextBox.Text = selected;
                CountryTextBox.CaretIndex = selected.Length;
                SuggestionsPopup.IsOpen = false;
            }
        }
    }
}