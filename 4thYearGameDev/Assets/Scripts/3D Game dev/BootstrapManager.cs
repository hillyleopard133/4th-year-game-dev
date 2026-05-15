using System.Threading.Tasks;
using UnityEngine;
using Fusion;

public class BootstrapManager : MonoBehaviour
{
    public AuthManager authManager;
    public NetworkRunner runner;
    public RunnerManager runnerManager;

    private async void Start()
    {
        await StartGameFlow();
    }

    private async Task StartGameFlow()
    {
        await authManager.InitialiseAndLogin();

        if (!authManager.IsReady)
            return;

        runner.AddCallbacks(runnerManager); 

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "CA3Session",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    
    private async Task<bool> ValidateWithAzure()
    {
        string token = authManager.GetAccessToken();

        using (UnityEngine.Networking.UnityWebRequest request =
               UnityEngine.Networking.UnityWebRequest.PostWwwForm("https://YOUR_AZURE_FUNCTION_URL", ""))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(token);
            request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "text/plain");

            await request.SendWebRequest();

            if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Auth proxy failed");
                return false;
            }

            return request.downloadHandler.text == "OK";
        }
    }
}