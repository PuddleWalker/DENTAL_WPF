using DENTAL_WPF.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DENTAL_WPF
{
    public partial class ShortEditWindow : Window
    {
        private bool isChanged = false;
        private DentalClinic den;
        private readonly DENTAL_Context dc;
        private bool isNewDentist = false;


        public ShortEditWindow(DentalClinic d, DENTAL_Context context)
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
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

            den.Name = NameTextBox.Text;

            if (dc.DentalClinics.Find(den.Id) != null) dc.Update(den);
            else dc.Add(den);
        }

        void CancelClick(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = den.Name ?? string.Empty;

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