using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreAPI.Models;
using CoreAPI.Classes;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CoreAPI.wwwroot.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Erros? _erros;

        public IndexModel(Erros? erros)
        {
            _erros = erros;
        }

        public string statusCode = string.Empty;
        public Pessoas? pessoa = new();    
        RegistroPessoa? apiResponse = new();

        public async Task OnGetAsync()
        {
          
            // Verifique se há erros e redirecione se necessário
            if (_erros != null && !string.IsNullOrEmpty(_erros.Descricao))           
            {
                // Redireciona para a página de erro, passando parâmetros se necessário
                Response.Redirect("/Erro");
                return;
            }

            
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
                
                if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    _erros.Cabecalho = $"Erro Critico no Servidor.";
                    _erros.Codigo = $"500";
                    _erros.Descricao = $"InternalServerError";
                    Response.Redirect("/Erro");
                    return null;
                }

               
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Models.RegistroPessoa>(jsonResponse);                  
                }           
                return null;
            }

        }

       



    }
}
