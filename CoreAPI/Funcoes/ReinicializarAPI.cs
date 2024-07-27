using System.Diagnostics;
using System.Reflection;

namespace CoreAPI.Funcoes
{
    public class ReinicializarAPI
    {
        public void Reiniciar()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;

            // Cria e configura o processo para reiniciar a API
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"\"{exePath}\"",
                UseShellExecute = true,
                Verb = "runas",  // Executa com privilégios administrativos
                CreateNoWindow = true
            };
            // Inicia o processo
            Process.Start(processStartInfo);
            // Encerra a aplicação atual
            Environment.Exit(0);
        }
    }
}
