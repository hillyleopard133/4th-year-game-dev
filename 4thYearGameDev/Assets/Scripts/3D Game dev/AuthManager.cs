using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class AuthManager : MonoBehaviour
{
    public bool IsReady { get; private set; }

    private bool hasInitialised = false;

    public async Task InitialiseAndLogin()
    {
        if (hasInitialised) return;   

        hasInitialised = true;

        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Debug.Log("Authenticated: " + AuthenticationService.Instance.PlayerId);

        IsReady = true;
    }

    public string GetAccessToken()
    {
        return AuthenticationService.Instance.AccessToken;
    }
}