using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreAPI.Models;
using CoreAPI.Classes;
using Newtonsoft.Json;

namespace CoreAPI.wwwroot.Pages
{
    public class IndexModel : PageModel
    {
        public Pessoas? pessoa = new();    
        RegistroPessoa? apiResponse = new();

        public async Task OnGetAsync()
        {

            var cpfQuery = Request.Query["Query"].ToString();

            if (!string.IsNullOrEmpty(cpfQuery))
            {
                apiResponse = await GetApiDataAsync(cpfQuery);
            
                if (apiResponse != null)
                {                  
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"CPF Localizado: {cpfQuery}");
                    Console.ResetColor();
                    pessoa.Status = apiResponse.Status = "CPF localizado.";
                    pessoa.CPF = apiResponse.CPF;
                    pessoa.Nome = apiResponse.Nome;
                    pessoa.Genero = apiResponse.Genero;
                    pessoa.DataNascimento = apiResponse.DataNascimento;                 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Não Localizado: {cpfQuery}");
                    Console.ResetColor();
                    pessoa.StatusObj = "CPF não localizado.";                
                }               
            }
        }

        private async Task<RegistroPessoa?> GetApiDataAsync(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPF/{cpf}"; // Substitua pela URL real

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    apiResponse = JsonConvert.DeserializeObject<Models.RegistroPessoa>(jsonResponse);
                    return apiResponse;
                }
                return null;
            }

        }
    }
}
