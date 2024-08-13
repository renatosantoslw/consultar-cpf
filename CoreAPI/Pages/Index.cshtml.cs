using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CoreAPI.Logs;
using CoreAPI.DataBase.SQLServer.Models;
using CoreAPI.DataBase.SQLServer.Repositories.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.wwwroot.Pages
{
    public class IndexModel : PageModel
    {       
        private readonly ErrosWiew? _errosFront = new();
        
        private RegistroPessoa? apiResponsePessoa = new();
        public RegistroPessoaDTO pessoa = new();

        private RegistroPessoaDatasus? apiResponseDataSUS = new();
        public RegistroPessoaDatasusDTO pessoaDataSUS = new();

        private string _cpf = string.Empty;

    

        [BindProperty(SupportsGet = true)]
        public string CPF
        {
            get { return _cpf; }   
            set { _cpf = value; }  
        }

        public IndexModel(ErrosWiew? erros)
        {
            _errosFront = erros;
        }


        public async Task OnGetAsync(string action)
        {
            if (action == "submit")
            {

                if (_errosFront != null && !string.IsNullOrEmpty(_errosFront.Descricao))
                {
                    Response.Redirect("/Erro");
                    return;
                }

                if (!string.IsNullOrEmpty(_cpf))
                {
                 
                    apiResponsePessoa = await GetApiDataAsync(_cpf);
                    apiResponseDataSUS = await GetApiDataAsyncDataSUS(_cpf);


                    if (apiResponsePessoa != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"CPF JBR_PF Localizado: {_cpf}");
                        Console.ResetColor();

                        pessoa = RegistroPessoaDTO.FromRegistroPessoa(apiResponsePessoa);
                        pessoa.Status = "1";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"CPF JBR_PF não Localizado: {_cpf}");
                        Console.ResetColor();
                        pessoa = RegistroPessoaDTO.FromRegistroPessoa(apiResponsePessoa);
                        pessoa.CPF = _cpf;
                    }

                    if (apiResponseDataSUS != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"CPF DATASUS Localizado: {_cpf}");
                        Console.ResetColor();

                        pessoaDataSUS = RegistroPessoaDatasusDTO.FromRegistroPessoaDatasus(apiResponseDataSUS);
                        pessoaDataSUS.Status = "1";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"CPF DATASUS não Localizado: {_cpf}");
                        Console.ResetColor();

                        pessoaDataSUS = RegistroPessoaDatasusDTO.FromRegistroPessoaDatasus(apiResponseDataSUS);
                        pessoaDataSUS.CPF = _cpf;
                    }

                
                }             

            }
      
        }



        private async Task<RegistroPessoa?> GetApiDataAsync(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPF/{cpf}";

            using (var httpClient = new HttpClient())
            {          
                
                var response = await httpClient.GetAsync(apiUrl);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<RegistroPessoa?>(jsonResponse);
                     
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






        private async Task<RegistroPessoaDatasus?> GetApiDataAsyncDataSUS(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPFDataSUS/{cpf}";

            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync(apiUrl);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<RegistroPessoaDatasus?>(jsonResponse);

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
