using CoreAPI.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreAPI.wwwroot.Pages
{
    public class ErroModel : PageModel
    {
        public readonly ErrosWiew? _erros;

        public string? pDescricao = null;
        public string? pCabecalho = null;
        public string? pCodigo = null;   
        
        public ErroModel(ErrosWiew erros)
        {
            _erros = erros;

            pDescricao = _erros.Descricao;
            pCabecalho = _erros.Cabecalho;
            pCodigo = _erros.Codigo;    
        }

        public void OnGet()
        {
            
            if (_erros.Descricao == null) 
            {
                Response.Redirect("/");
            }

            _erros.Descricao = null;
            _erros.Cabecalho = null;
            _erros.Codigo = null;
            _erros.Tipo = null;
        }
    }
}
