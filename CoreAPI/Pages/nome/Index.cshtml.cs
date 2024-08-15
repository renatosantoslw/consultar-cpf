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
        private readonly ErrosWiew? _errosFront;
        private readonly ILogger<IndexNomeModel> _logger;

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
        public int Pagina { get; set; } = 1;

        private const int PageSize = 5;

        public IndexNomeModel(ILogger<IndexNomeModel> logger, ErrosWiew? erros)
        {
            _logger = logger;
            _errosFront = erros;
        }


        public void OnGetLimpar()
        {
            // Lógica para o método Clear
            _PessoasPaginacao.Clear();
        }

        public async Task OnGetAsync(string action, string Nome, int Pagina = 1)
        {
            CurrentPage = Pagina;
            _nome = Nome;

            if (!string.IsNullOrEmpty(_nome))
            {

                if (action == "submit")
                {
                    _PessoasPaginacao.Clear();

                    if (string.IsNullOrEmpty(_nome) || !IsValidNOME(_nome))
                    {
                        SetErrorView(@"\nome","Erro ao validar o campo: Nome", "", "-O Campo Nome deve conter: \n•Somente Letras \n•Minimo de caracteres: 3 \n•Máximo de caracteres: 100");
                        return;
                    }
                }


                if (_PessoasPaginacao.Count == 0)
                {
                    apiResponsePessoa = await GetPessoasByNomeAsync(_nome);
                    _PessoasPaginacao = RegistroPessoaDTO.FromRegistroPessoaList(apiResponsePessoa);
                }

                if (_PessoasPaginacao.Count != 0 || apiResponsePessoa != null && apiResponsePessoa.Any())
                {
             
                    TotalPages = (int)Math.Ceiling(_PessoasPaginacao.Count / (double)PageSize);
                    TotalPages = TotalPages > 0 ? TotalPages : 1; // Ensure TotalPages is at least 1



                    _Pessoas = _PessoasPaginacao
                        .Skip((CurrentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();


                    Status = "1";

                    if (Pagina > 1)
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

                // Adjust Pagina if it's out of range
                if (CurrentPage < 1) CurrentPage = 1;              

                if (CurrentPage >= TotalPages) CurrentPage = TotalPages;

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
                        _logger.LogError("InternalServerError");
                        SetErrorView(@"\nome", "Erro", "500", "Erro interno no servidor.");
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        _logger.LogError("BadRequest");
                        SetErrorView(@"\nome","Erro", "400", "Solicitação inválida.");
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        _logger.LogError("Unauthorized");
                        SetErrorView(@"\nome","Erro", "401", "Não autorizado. Verifique suas credenciais.");
                        break;

                    case System.Net.HttpStatusCode.Forbidden:
                        _logger.LogError("Forbidden");
                        SetErrorView(@"\nome","Erro", "403", "Acesso proibido. Você não tem permissão.");
                        break;

                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        _logger.LogError("MethodNotAllowed");
                        SetErrorView(@"\nome","Erro", "405", "Método não permitido.");
                        break;
                }
            }
        }

        private bool IsValidNOME(string nome)
        {
            int minLength = 3;  // Defina o comprimento mínimo desejado
            int maxLength = 100; // Defina o comprimento máximo desejado

            // Verifica se o comprimento da string está dentro dos limites e se contém apenas letras e espaços em branco
            return nome.Length >= minLength && nome.Length <= maxLength &&
                   nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
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
    }
}
