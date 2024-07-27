
namespace CoreAPI.Logs
{
    public class ErrosLogGravar
    {       
        public void GerarLogErro(Exception e, string formulario, string funcaoProcedimento)
        {
            DateTime now = DateTime.Now;
            
            try
            {
                string fullDateTime = now.ToString("dd-MM-yyyy-HH-mm-ss");
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + $"\\LogsErro\\Log_Erro-{fullDateTime}.txt", false))
                {
                    sw.WriteLine($"Data: {DateTime.Now.ToShortDateString()}");
                    sw.WriteLine($"Hora: {DateTime.Now.ToShortTimeString()}");
                    sw.WriteLine($"Descrição do erro: {e.Message}");
                    sw.WriteLine($"StackTrace: {e.StackTrace}");
                    sw.WriteLine($"Formulário: {formulario}");
                    sw.WriteLine($"Função/Procedimento: {funcaoProcedimento}");
                    sw.WriteLine($"Computador: {Environment.MachineName}");
                    sw.WriteLine($"Usuário: {Environment.UserName}");
                }
            }
            catch
            {
                throw;
            }
        }

        public void GerarLogErro(string descricao, string funcaoProcedimento, string formulario)
        {
            DateTime now = DateTime.Now;

            try
            {
                string fullDateTime = now.ToString("dd-MM-yyyy-HH-mm-ss");
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + $"\\LogsErro\\Log_Erro-{fullDateTime}.txt", false))
                {
                    sw.WriteLine($"Data: {DateTime.Now.ToShortDateString()}");
                    sw.WriteLine($"Hora: {DateTime.Now.ToShortTimeString()}");
                    sw.WriteLine($"Descrição do erro: {descricao}");                   
                    sw.WriteLine($"Formulário: {formulario}");
                    sw.WriteLine($"Função/Procedimento: {funcaoProcedimento}");
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
