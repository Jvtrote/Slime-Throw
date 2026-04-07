using UnityEngine;
// Passo 1: Importar o namespace da LevelPlay
using Unity.Services.LevelPlay;

public class LevelPlayManager : MonoBehaviour
{
    // Substitua pelo seu App Key do painel ironSource/LevelPlay
    private string appKey = "6084494";

    void Start()
    {
        InitializeLevelPlay();
    }

    private void InitializeLevelPlay()
    {
        // Passo 2: Registrar os listeners de eventos ANTES da inicialização
        // Isso garante que você capture o status do carregamento
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;

        // Passo 3: Inicializar o SDK
        Debug.Log("Iniciando LevelPlay SDK...");
        LevelPlay.Init(appKey);
    }

    // Callback disparado quando o SDK inicializa com sucesso
    private void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay inicializado com sucesso! Agora você pode carregar anúncios.");
        // Exemplo: Carregar um Banner ou Interstitial aqui
    }

    // Callback disparado quando ocorre erro na inicialização
    private void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.LogError($"Erro na inicialização do LevelPlay: {error.ErrorMessage}");
        // Dica: Verifique a conexão com a internet ou se a App Key está correta
    }

    // Boa prática: Remover os listeners quando o objeto for destruído
    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
    }
}