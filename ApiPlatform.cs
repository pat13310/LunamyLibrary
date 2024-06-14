namespace ApiPlateform.ApiPlateForm
{
    using Newtonsoft.Json;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

        public class ApiPlatform
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl, _baseAdmin;

        public ApiPlatform(string baseUrl, string baseAdmin)
        {
            _baseUrl = baseUrl;
            _baseAdmin = baseAdmin;
            _client = new HttpClient();
        }

        // Méthode d'authentification pour obtenir un JWT
        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            string endpoint = "auth"; // Utilisez le bon endpoint ici 
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));

            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            // Créez le corps de la requête selon le format attendu par l'API
            var requestData = new
            {
                username,
                password
            };
            var jsonRequest = JsonConvert.SerializeObject(requestData);
            StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(_baseUrl + endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}. Details: {errorContent}");
                    return null; // Assurez-vous d'avoir un 'return' ici
                }

                string result = await response.Content.ReadAsStringAsync();
                // Traitez le résultat ici, puis retournez la valeur appropriée
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                return tokenResponse?.Token; // Assurez-vous d'avoir un 'return' ici
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null; // Assurez-vous d'avoir un 'return' ici
            }
            // Si d'autres exceptions peuvent être levées, elles doivent aussi être gérées ou une valeur doit être retournée à la fin.
        }


        // On décode notre JWT et on met l'ensemble dans un dictionnaire
        public static Dictionary<string, string>? DecodeJwtToken(string? jwtToken)
        {
            if (jwtToken == null)
                return null;

            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken token = handler.ReadJwtToken(jwtToken); // Lit le token sans valider la signature

            Dictionary<string, string> claims = [];
            foreach (var claim in token.Claims)
            {
                if (!claims.ContainsKey(claim.Type))
                    claims.Add(claim.Type, claim.Value); // Ajoute chaque claim au dictionnaire
            }
            return claims;
        }

        public async Task<List<T>?> GetDataAsync<T>(string jwtToken, string endpoint, Dictionary<string, string> queryParams)
        {
            // Préparation de l'entête
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));

            // Construction de l'URL avec les paramètres de la requête
            StringBuilder url = new();
            url.Append(_baseAdmin + endpoint + "?");

            StringBuilder queryBuilder = new();
            foreach (var param in queryParams)
            {
                // Append each parameter key and value to the string builder, URL encoded.
                queryBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(param.Key), Uri.EscapeDataString(param.Value));
            }

            // Récupération de l'url de base et mise en forme
            string queryString = queryBuilder.ToString().TrimEnd('&');
            url.Append(queryString);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}. Details: {errorContent}");
                    return default; // Assurez-vous d'avoir un 'return' ici si la requete n'aboutit pas
                }
                string result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LunamyLibrary.HydraResponse<T>>(result);
                if (data?.Members == null)
                {
                    Console.WriteLine("Members is null. Response JSON: " + result);
                }
                return data?.Members.ToList();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return default; // Cela retournera null pour les types de référence et une liste vide pour les types de valeur
            }
        }

        // Suppression des ressources (ClientHTTP)
        public void Dispose()
        {
            _client?.Dispose();
        }

        // Obtention du token
        private class TokenResponse
        {
            public string? Token { get; set; }
        }
    }

}
