using DENTAL_WPF.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DENTAL_WPF
{
    public partial class EditWindow : Window
    {
        private bool isChanged = false;
        private Dentist den;
        private readonly DENTAL_Context dc;
        private bool isNewDentist = false;

        
        public EditWindow(Dentist d, DENTAL_Context context)
        {
            InitializeComponent();

            den = d ?? throw new ArgumentNullException(nameof(d));
            dc = context ?? throw new ArgumentNullException(nameof(context));
            isNewDentist = false;

            InitializeFields();
        }

        
        public EditWindow(DENTAL_Context context)
        {
            InitializeComponent();

            dc = context ?? throw new ArgumentNullException(nameof(context));
            den = new Dentist();
            isNewDentist = true;

            InitializeFields();
        }

        private void InitializeFields()
        {
            NameTextBox.Text = den.Name ?? string.Empty;
            SurnameTextBox.Text = den.Surname ?? string.Empty;

            if (den.BeginDate.HasValue)
            {
                BeginDatePicker.SelectedDate = new DateTime(
                    den.BeginDate.Value.Year,
                    den.BeginDate.Value.Month,
                    den.BeginDate.Value.Day);
            }
            else
            {
                BeginDatePicker.SelectedDate = null;
            }

            if (dc.Categories != null)
            {
                CategoryIdComboBox.ItemsSource = dc.Categories.Select(c => c.Id).ToList();
                CategoryIdComboBox.SelectedValue = den.CategoryId;
            }
            else
            {
                CategoryIdComboBox.ItemsSource = new List<int>();
                CategoryIdComboBox.IsEnabled = false;
            }

            if (dc.Specialities != null)
            {
                SpecialityIdComboBox.ItemsSource = dc.Specialities.Select(s => s.Id).ToList();
                SpecialityIdComboBox.SelectedValue = den.SpecialityId;
            }
            else
            {
                SpecialityIdComboBox.ItemsSource = new List<int>();
                SpecialityIdComboBox.IsEnabled = false;
            }

            if (dc.DentalClinics != null)
            {
                ClinicIdComboBox.ItemsSource = dc.DentalClinics.Select(cl => cl.Id).ToList();
                ClinicIdComboBox.SelectedValue = den.DentalClinicId;
            }
            else
            {
                ClinicIdComboBox.ItemsSource = new List<int>();
                ClinicIdComboBox.IsEnabled = false;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            
            den.Name = NameTextBox.Text;
            den.Surname = SurnameTextBox.Text;

            if (BeginDatePicker.SelectedDate.HasValue)
            {
                den.BeginDate = DateOnly.FromDateTime(BeginDatePicker.SelectedDate.Value);
            }
            else
            {
                den.BeginDate = null;
            }

            den.CategoryId = CategoryIdComboBox.SelectedValue as int?;
            den.SpecialityId = SpecialityIdComboBox.SelectedValue as int?;
            den.DentalClinicId = ClinicIdComboBox.SelectedValue as int?;

            if(dc.Dentists.Find(den.Id) != null) dc.Update(den);
            else dc.Add(den);
        }

        void CancelClick(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = den.Name ?? string.Empty;
            SurnameTextBox.Text = den.Surname ?? string.Empty;

            if (den.BeginDate.HasValue)
            {
                BeginDatePicker.SelectedDate = new DateTime(
                    den.BeginDate.Value.Year,
                    den.BeginDate.Value.Month,
                    den.BeginDate.Value.Day
                );
            }
            else BeginDatePicker.SelectedDate = null;
            if (dc.Categories != null) CategoryIdComboBox.SelectedValue = den.CategoryId;
            if (dc.Specialities != null) SpecialityIdComboBox.SelectedValue = den.SpecialityId;
            if (dc.DentalClinics != null) ClinicIdComboBox.SelectedValue = den.DentalClinicId;

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
            if (SurnameTextBox is null || SurnameTextBox.Text == "")
            {
                e.CanExecute = false;
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
    }
}