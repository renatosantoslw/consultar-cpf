using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CoreAPI.DataBase.SQLServer.Models;
using CoreAPI.DataBase.SQLServer.Repositories.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.Logs;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.wwwroot.Pages
{
    public class IndexNomeModel : PageModel
    {
        private readonly ErrosWiew? _errosFront = new();  
        private List<RegistroPessoa?> apiResponsePessoa  { get; set; } = new ();
        public List<RegistroPessoaDTO> _Pessoas { get; set; } = new();

        public static List<RegistroPessoaDTO> _PessoasPaginacao { get; set; } = new();
        public string Status = string.Empty;


        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
  

        private string _nome = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 5;

        public IndexNomeModel(ErrosWiew? erros)
        {
            _errosFront = erros;
        }

        public async Task OnGetAsync(string action, string Nome, int PageNumber = 1)
        {
            CurrentPage = PageNumber;
            _nome = Nome;
      

            if (!string.IsNullOrEmpty(_nome))
            {

                if (action == "submit")
                {
                    _PessoasPaginacao.Clear();
                }


                if (_PessoasPaginacao.Count == 0)
                {
                    apiResponsePessoa = await GetPessoasByNomeAsync(_nome);
                    _PessoasPaginacao = RegistroPessoaDTO.FromRegistroPessoaList(apiResponsePessoa);
                }

                if (_PessoasPaginacao.Count != 0 || apiResponsePessoa != null && apiResponsePessoa.Any())
                {
                    TotalPages = (int)Math.Ceiling(_PessoasPaginacao.Count / (double)PageSize);
                    
                    _Pessoas = _PessoasPaginacao
                        .Skip((PageNumber - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                    
                    Status = "1";

                    if (PageNumber > 1)
                    {
                        Status = "2";
                    }


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Nome localizado: {_nome}");
                    Console.ResetColor();
                }
                else
                {
                    Status = "0";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Nome não localizado: {_nome}");
                    Console.ResetColor();
                }
            }


            
        }






        private async Task<List<RegistroPessoa?>> GetPessoasByNomeAsync(string nome)
        {
            var apiUrl = $"http://localhost:5000/getByNome/{nome}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<RegistroPessoa?>>(jsonResponse) ?? new List<RegistroPessoa?>();
                }

                HandleErrorResponse(response.StatusCode);
                return new List<RegistroPessoa?>();
            }
        }









        private void HandleErrorResponse(System.Net.HttpStatusCode statusCode)
        {
            if (_errosFront != null)
            {
                switch (statusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        _errosFront.Codigo = "500";
                        _errosFront.Descricao = "Erro interno no servidor.";
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        _errosFront.Codigo = "400";
                        _errosFront.Descricao = "Solicitação inválida.";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        _errosFront.Codigo = "401";
                        _errosFront.Descricao = "Não autorizado. Verifique suas credenciais.";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        _errosFront.Codigo = "403";
                        _errosFront.Descricao = "Acesso proibido. Você não tem permissão.";
                        break;
                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        _errosFront.Codigo = "405";
                        _errosFront.Descricao = "Método não permitido.";
                        break;
                    default:
                        _errosFront.Codigo = "Unknown";
                        _errosFront.Descricao = "Erro desconhecido.";
                        break;
                }
            }
        }
    }
}
