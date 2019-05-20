using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ParseCodeWars 
{
    static class WebProgram
    {
        private static readonly string allCompletedPath = $@"https://www.codewars.com/api/v1/users/goobering/code-challenges/completed?page=0";
        static HttpClient client = new HttpClient();

        public static async Task<List<Kata>> GetCompletedKata(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            var allKata = new List<Kata>();

            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStreamAsync();
                
                using(JsonDocument doc = await JsonDocument.ParseAsync(jsonString))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");

                    foreach(var element in data.EnumerateArray())
                    {
                        var id = element.GetProperty("id").ToString();
                        var name = element.GetProperty("slug").ToString();

                        var completedLanguagesElement = element.GetProperty("completedLanguages");
                        var completedLanguages = new string[completedLanguagesElement.GetArrayLength()];
                        for(int i = 0; i < completedLanguagesElement.GetArrayLength(); i++)
                        {
                            completedLanguages[i] = completedLanguagesElement[i].ToString();
                        }

                        var completedAt = DateTime.Parse(element.GetProperty("completedAt").ToString());

                        allKata.Add(new Kata(id, name, completedLanguages, completedAt));
                    }
                }
            }

            return allKata;
        }

    }
}