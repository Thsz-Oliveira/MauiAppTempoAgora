using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Tempo(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude:{t.lat}\n" +
                            $"Longitude:{t.lon}\n" +
                            $"Nascer do Sol:{t.sunrise}\n" +
                            $"Por do Sol:{t.sunset}\n" +
                            $"Temp  Máx:{t.temp_max}\n" +
                            $"Temp Min:{t.temp_min}\n" +
                            $"Descrição:{t.description}\n" +
                            $"Velocidade do vento:{t.speed}\n" +
                            $"Visibilidade:{t.visibility}\n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua internet", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
