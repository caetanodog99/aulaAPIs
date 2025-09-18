using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class aulapokemon : MonoBehaviour
{
    private HttpClient client = new HttpClient();
    private const string baseURL = "https://pokeapi.co/api/v2/pokemon/";
   
    // pokemon: chandelure
    async void Start()
    {
        Pokemon chandelure = await GetPokemon("chandelure");
        Debug.Log($"Nome:  {chandelure.name} / Peso: {chandelure.weight}");
    }
    public async Task<Pokemon> GetPokemon(string nomePokemon)
    {
      string url = baseURL + nome.ToLower;
      HttpResponseMessage response = await client.GetAsync(url));
      //response.EnsureSuccessStatusCode();
      string responseJson = await response.Content.ReadAsStringAsync();
      Pokemon pokemon = JsonUtility.FromJson<Pokemon>(responseJson);
      return pokemon;
    }

}
