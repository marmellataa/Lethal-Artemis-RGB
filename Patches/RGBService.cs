using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LCArtemis
{
    public static class RGBService
    {
        private static readonly HttpClient client = new HttpClient();
        private static bool isSchemaRegistered = false;

        private const string MODULE_ID = "lethal-company-lcartemis-1a2b3c";
        private const string BASE_URL = "http://localhost:9696/json-modules/" + MODULE_ID;

        public static void SetDancingState(bool isDancing)
        {
            LCArtemis.Logger.LogInfo($"🎨 Stato ballo cambiato. isDancing = {isDancing}");
            _ = SendArtemisSignalAsync(isDancing);
        }

        private static async Task SendArtemisSignalAsync(bool isDancing)
        {
            try
            {
                // 1. Invia lo Schema (solo la prima volta)
                if (!isSchemaRegistered)
                {
                    string jsonSchema = @"{
                        ""$schema"": ""http://json-schema.org/draft-04/schema#"",
                        ""title"": ""Lethal Company"",
                        ""type"": ""object"",
                        ""properties"": {
                            ""isDancing"": {
                                ""title"": ""Player is dancing"",
                                ""type"": ""boolean""
                            }
                        }
                    }";

                    HttpContent schemaContent = new StringContent(jsonSchema, Encoding.UTF8, "application/json");
                    HttpResponseMessage schemaResp = await client.PostAsync(BASE_URL + "/schema", schemaContent);
                    if (schemaResp.IsSuccessStatusCode) isSchemaRegistered = true;
                }

                // 2. Invia l'accensione o lo spegnimento
                string json = isDancing ? "{\"isDancing\": true}" : "{\"isDancing\": false}";
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(BASE_URL + "/data", content);
            }
            catch (System.Exception ex)
            {
                LCArtemis.Logger.LogWarning($"Errore Artemis: {ex.Message}");
            }
        }
    }
}