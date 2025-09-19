using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private NetworkManager networkManager;

    private void Awake()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
    }

    private void OnLoginClicked()
    {
        string playerName = string.IsNullOrWhiteSpace(nameInput.text) ? "Guest" : nameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        networkManager.StartClient();

        gameObject.SetActive(false);
    }
}
