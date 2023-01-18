using Newtonsoft.Json;

namespace DTOs;

public class FundoResponse
{
    [JsonProperty("codigo_fundo")]
    public string CodigoFundo { get; set; }

    [JsonProperty("nome_fantasia")]
    public string NomeFantasia { get; set; }

    [JsonProperty("cnpj_fundo")]
    public string CnpjFundo { get; set; }

    [JsonProperty("classe_anbima")]
    public string ClasseAnbima { get; set; }

    [JsonProperty("situacao_atual")]
    public string SituacaoAtual { get; set; }


    [JsonProperty("data_atualizacao")]
    public DateTime DataAtualizacao { get; set; }

    [JsonProperty("data_inicio_divulgacao_cota")]
    public DateTime DataInicioDivulgacaoCota { get; set; }
}