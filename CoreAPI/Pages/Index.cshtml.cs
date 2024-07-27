using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CoreAPI.Logs;
using CoreAPI.Data.Entity;
using CoreAPI.Data.Models;

namespace CoreAPI.wwwroot.Pages
{
    public class IndexModel : PageModel
    {       
        private readonly ErrosWiew? _errosFront;
        private RegistroPessoa? apiResponse = new();
        public Pessoas? pessoa = new();


        public IndexModel(ErrosWiew? erros)
        {
            _errosFront = erros;
        }

        public async Task OnGetAsync()
        {       
  
            if (_errosFront != null && !string.IsNullOrEmpty(_errosFront.Descricao))           
            {
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

                    pessoa = Pessoas.FromRegistroPessoa(apiResponse);                 
                    pessoa.Status  = "CPF localizado.";          
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Não Localizado: {cpfQuery}");
                    Console.ResetColor();
                    pessoa.Status = "CPF não localizado.";                
                }               
            }
        }

        private async Task<RegistroPessoa?> GetApiDataAsync(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPF/{cpf}"; // Substitua pela URL real

            using (var httpClient = new HttpClient())
            {          
                
                var response = await httpClient.GetAsync(apiUrl);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<RegistroPessoa>(jsonResponse);
                     
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"InternalServerError");
                        Console.ResetColor();

                        _errosFront.Codigo = "500";
                        _errosFront.Descricao = "Erro interno no servidor.";
                        _errosFront.Cabecalho = "Erro";
                        Response.Redirect("/Erro");
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"BadRequest");
                        Console.ResetColor();

                        _errosFront.Codigo = "400";
                        _errosFront.Descricao = "Solicitação inválida.";
                        _errosFront.Cabecalho = "Erro";
                        Response.Redirect("/Erro");
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unauthorized");
                        Console.ResetColor();

                        _errosFront.Codigo = "401";
                        _errosFront.Descricao = "Não autorizado. Verifique suas credenciais.";
                        _errosFront.Cabecalho = "Erro";
                        Response.Redirect("/Erro");
                        break;

                    case System.Net.HttpStatusCode.Forbidden:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Forbidden");
                        Console.ResetColor();

                        _errosFront.Codigo = "403";
                        _errosFront.Descricao = "Acesso proibido. Você não tem permissão.";
                        _errosFront.Cabecalho = "Erro";
                        Response.Redirect("/Erro");
                        break;

                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"MethodNotAllowed");
                        Console.ResetColor();

                        _errosFront.Codigo = "405";
                        _errosFront.Descricao = "Método não permitido.";
                        _errosFront.Cabecalho = "Erro";
                        Response.Redirect("/Erro");
                        break;
                          
                }

                return null;                                     
            }

        }
  
    }
}
