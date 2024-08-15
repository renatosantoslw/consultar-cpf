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
        private readonly ErrosWiew? _errosFront;
        private readonly ILogger<IndexModel> _logger;

        public RegistroPessoaDTO pessoa = new();
        public RegistroPessoaDatasusDTO pessoaDataSUS = new();

        private string _cpf = string.Empty;
      
        [BindProperty(SupportsGet = true)]
        public string CPF
        {
            get { return _cpf; }   
            set { _cpf = value; }  
        }

        public IndexModel(ILogger<IndexModel> logger, ErrosWiew? erros)
        {
            _logger = logger;
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

                if (string.IsNullOrEmpty(_cpf) || !IsValidCPF(_cpf))
                {
                    SetErrorView(@"\", "Erro ao validar o campo: CPF", "", "-O Campo CPF deve conter: \n•Somente Números \n•Minimo de caracteres: 11 \n•Máximo de caracteres: 11");
                    return;             
                }

                var apiResponsePessoa = await GetApiDataAsync(_cpf);
                var apiResponseDataSUS = await GetApiDataAsyncDataSUS(_cpf);

                try
                {
                    if (apiResponsePessoa != null)
                    {
                        _logger.LogInformation($"CPF JBR_PF Localizado: {_cpf}");
                        pessoa = RegistroPessoaDTO.FromRegistroPessoa(apiResponsePessoa);
                        pessoa.Status = "1";
                    }
                    else
                    {
                        _logger.LogWarning($"CPF JBR_PF não Localizado: {_cpf}");
                        pessoa = RegistroPessoaDTO.FromRegistroPessoa(apiResponsePessoa);
                        pessoa.CPF = _cpf;
                    }

                    if (apiResponseDataSUS != null)
                    {
                        _logger.LogInformation($"CPF DATASUS Localizado: {_cpf}");
                        pessoaDataSUS = RegistroPessoaDatasusDTO.FromRegistroPessoaDatasus(apiResponseDataSUS);
                        pessoaDataSUS.Status = "1";
                    }
                    else
                    {
                        _logger.LogWarning($"CPF DATASUS não Localizado: {_cpf}");
                        pessoaDataSUS = RegistroPessoaDatasusDTO.FromRegistroPessoaDatasus(apiResponseDataSUS);
                        pessoaDataSUS.CPF = _cpf;
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocorreu um erro ao processar a solicitação.");
                    SetErrorView(@"\","","100", ex.Message);         
                }                                             
            }    
        }


        private async Task<RegistroPessoa?> GetApiDataAsync(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPF/{cpf}";

            using (var httpClient = new HttpClient())
            {          
                
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegistroPessoa?>(jsonResponse);
                }

                HandleErrorResponse(response.StatusCode);
                return null;                                     
            }

        }

        private async Task<RegistroPessoaDatasus?> GetApiDataAsyncDataSUS(string cpf)
        {
            var apiUrl = $"http://localhost:5000/getByCPFDataSUS/{cpf}";

            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegistroPessoaDatasus?>(jsonResponse);
                }

                HandleErrorResponse(response.StatusCode);
                return null;
            }

        }


        private void SetErrorView(string pagina, string cabecalho, string codigo, string descricao)
        {
            _errosFront.Codigo = codigo;
            _errosFront.Descricao = descricao;
            _errosFront.Cabecalho = cabecalho;
            _errosFront.Pagina = pagina;
            Response.Redirect("/Erro");
            return;
        }

        private bool IsValidCPF(string cpf)
        {
            return !string.IsNullOrEmpty(cpf) && cpf.Length == 11 && cpf.All(char.IsDigit);
        }

        private void HandleErrorResponse(System.Net.HttpStatusCode statusCode)
        {
            if (_errosFront != null)
            {
                switch (statusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        _logger.LogError("InternalServerError");
                        SetErrorView(@"\","Erro","500", "Erro interno no servidor.");
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        _logger.LogError("BadRequest");
                        SetErrorView(@"\", "Erro","400", "Solicitação inválida.");
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        _logger.LogError("Unauthorized");
                        SetErrorView(@"\", "Erro", "401", "Não autorizado. Verifique suas credenciais.");
                        break;

                    case System.Net.HttpStatusCode.Forbidden:
                        _logger.LogError("Forbidden");
                        SetErrorView(@"\", "Erro", "403", "Acesso proibido. Você não tem permissão.");
                        break;

                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        _logger.LogError("MethodNotAllowed");
                        SetErrorView(@"\", "Erro", "405", "Método não permitido.");
                        break;
                }
            }
        }

    }
}
