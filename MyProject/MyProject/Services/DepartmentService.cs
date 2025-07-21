using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinAPI.Models;

namespace MyProject.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //private string _apiUrl = "http://10.0.2.2:5172/api/Department/";
        private string _apiUrl = "http://192.168.100.34:5172/api/Department/";
        public async Task<List<Department>> GetAllAsync()
        {
            var response = await _httpClient.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<List<Department>>(response);
        }
    }
}
