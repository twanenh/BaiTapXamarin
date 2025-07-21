using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinAPI.DTO;
using XamarinAPI.Models;

namespace MyProject.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //private string _apiUrl = "http://10.0.2.2:5172/api/Employee/";
        private string _apiUrl = "http://192.168.100.34:5172/api/Employee/";

        public async Task<List<EmployeeDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<List<EmployeeDTO>>(response);
        }

        public async Task<bool> CreateAsync(Employee nv)
        {
            var json = JsonConvert.SerializeObject(nv);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API Error: " + errorContent);
            }

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateAsync(EmployeeDTO nv)
        {
            var json = JsonConvert.SerializeObject(nv);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{nv.Id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API Error: " + errorContent);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API Error: " + errorContent);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
