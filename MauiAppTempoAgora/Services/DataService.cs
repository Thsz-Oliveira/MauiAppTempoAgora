using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "7b26c92417fd3678d52eac12dc870222";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&appid={chave}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Cidade não encontrada.");
                }

                if (!resp.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao buscar dados da API.");
                }

                string json = await resp.Content.ReadAsStringAsync();


                if (resp.IsSuccessStatusCode)
                {                  
                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min =(double)rascunho["main"]["temp_min"],
                        temp_max =(double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),

                    }; //Fecha obj do tempo
                }//Fecha if se o status do servidor foi de sucesso
  

            }//fecha laço using

            return t;
        }
    }
}
