using MyProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAPI.DTO;
using XamarinAPI.Models;

namespace MyProject
{
    public partial class MainPage : ContentPage
    {
        EmployeeService _employeeService = new EmployeeService();
        private List<EmployeeDTO> _allEmployees = new List<EmployeeDTO>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadNhanViens();
        }

        private async Task LoadNhanViens()
        {
            _allEmployees = await _employeeService.GetAllAsync();
            NhanVienListView.ItemsSource = _allEmployees;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new FormPage());
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedEmployee = button?.CommandParameter as EmployeeDTO;

            if (selectedEmployee != null)
            {
                await Navigation.PushAsync(new FormPage(selectedEmployee));
            }
            else
            {
                await DisplayAlert("Lỗi", "Không lấy được nhân viên.", "OK");
            }
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedEmployee = button?.CommandParameter as EmployeeDTO;

            if (selectedEmployee != null)
            {
                bool confirm = await DisplayAlert("Xác nhận", $"Bạn có chắc muốn xoá nhân viên {selectedEmployee.Name}?", "Xoá", "Huỷ");

                if (confirm)
                {
                    bool result = await _employeeService.DeleteAsync(selectedEmployee.Id);

                    if (result)
                    {
                        await DisplayAlert("Thành công", "Xoá nhân viên thành công.", "OK");
                        await LoadNhanViens();
                    }
                    else
                    {
                        await DisplayAlert("Lỗi", "Xoá thất bại (API trả về lỗi).", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Lỗi", "Không lấy được nhân viên.", "OK");
            }
        }
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue?.ToLower() ?? "";

            var filtered = _allEmployees.Where(emp =>
                (emp.Name != null && emp.Name.ToLower().Contains(keyword)) ||
                (emp.Role != null && emp.Role.ToLower().Contains(keyword))
            ).ToList();

            NhanVienListView.ItemsSource = filtered;
        }


    }
}
