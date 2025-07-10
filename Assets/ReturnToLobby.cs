using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLobby : MonoBehaviour
{
    public void VolverAlLobby()
    {
        if (ChatManager.Instance != null)
        {
            ChatManager.Instance.volverDesdeDerrota = true;
        }

        SceneManager.LoadScene("ChatScene");
    }
}
