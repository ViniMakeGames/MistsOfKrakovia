using Mirror;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))] public string playerName;

    public float speed = 5f;
    [SerializeField] private TextMeshPro nameTag;

    public override void OnStartLocalPlayer()
    {
        CmdSetName(PlayerPrefs.GetString("PlayerName", "Guest"));
    }

    [Command]
    private void CmdSetName(string name)
    {
        playerName = name;
    }

    private void OnNameChanged(string oldName, string newName)
    {
        nameTag.text = newName;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;

        transform.Translate(dir * speed * Time.deltaTime);
    }
}
