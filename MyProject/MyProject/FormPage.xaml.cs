using MyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAPI.DTO;
using XamarinAPI.Models;

namespace MyProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        DepartmentService _departmentService = new DepartmentService();
        EmployeeService _employeeService = new EmployeeService();
        private EmployeeDTO _editingEmployee;
        public FormPage(EmployeeDTO employee = null)
        {

            InitializeComponent();
            _editingEmployee = employee;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDepartment();

            if (_editingEmployee != null)
            {
                // Điền dữ liệu nhân viên vào form
                Name.Text = _editingEmployee.Name;
                Role.Text = _editingEmployee.Role;

                // Chọn phòng ban tương ứng
                if (DepartmentPicker.ItemsSource is List<Department> depts)
                {
                    var selectedDept = depts.FirstOrDefault(d => d.Id == _editingEmployee.DepartmentId);
                    DepartmentPicker.SelectedItem = selectedDept;
                }
            }
        }

        private async Task LoadDepartment()
        {
            var list = await _departmentService.GetAllAsync();
            DepartmentPicker.ItemsSource = list;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var name = Name.Text?.Trim();
            var role = Role.Text?.Trim();
            var selectedDept = DepartmentPicker.SelectedItem as Department;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(role) || selectedDept == null)
            {
                await DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin.", "OK");
                return;
            }

            try
            {
                bool result = false;

                if (_editingEmployee == null)
                {
                    var newEmployee = new Employee
                    {
                        Name = name,
                        Role = role,
                        DepartmentId = selectedDept.Id
                    };
                    result = await _employeeService.CreateAsync(newEmployee);
                }
                else
                {
                    _editingEmployee.Name = name;
                    _editingEmployee.Role = role;
                    _editingEmployee.DepartmentId = selectedDept.Id;
                    result = await _employeeService.UpdateAsync(_editingEmployee);
                }

                if (result)
                {
                    await DisplayAlert("Thành công", "Lưu nhân viên thành công.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Lỗi", "Không lưu được nhân viên (API trả về thất bại).", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi Exception", ex.Message, "OK");
                System.Diagnostics.Debug.WriteLine("Lỗi lưu nhân viên: " + ex);
            }
        }
    }
}