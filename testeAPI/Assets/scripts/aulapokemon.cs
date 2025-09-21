using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class aulapokemon : MonoBehaviour
{
    private readonly HttpClient httpClient = new HttpClient();
    private const string baseUrl = "https://pokeapi.co/api/v2/pokemon/";
    public Image pkmnImg;
    public TextMeshProUGUI pkmnText;


    async void Start()
    {
        Pokemon pkmn = await GetDadosPokemon("rayquaza");

        if (pkmn != null)
        {
            pkmnText.text = $"Nome: {pkmn.name}\nNúmero na pokedex: {pkmn.id}\nTipos: {pkmn.types[0].type.name}/{pkmn.types[1].type.name}";

            

            StartCoroutine(LoadPokemonSprite(pkmn.sprites.front_default));


        }
    }
    public async Task<Pokemon> GetDadosPokemon(string nome)
    {

        string url = baseUrl + nome.ToLower();

        HttpResponseMessage response = await httpClient.GetAsync(url);
        string jsonResponse = await response.Content.ReadAsStringAsync();
        Pokemon pokemon = JsonUtility.FromJson<Pokemon>(jsonResponse);
        return pokemon;
    }
    IEnumerator LoadPokemonSprite(string spriteUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(spriteUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                pkmnImg.sprite = sprite;
                Debug.Log("Imagem do pokémon carregada com sucesso");
            }
            else
            {
                Debug.LogError($"Erro ao carregar imagem: {request.error}");
            }
        }
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        httpClient?.Dispose();
    }
}
