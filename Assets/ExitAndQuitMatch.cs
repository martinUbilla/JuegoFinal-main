using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAndQuitMatch : MonoBehaviour
{
    public void SalirYEnviarQuitMatch()
    {
        if (ChatManager.Instance != null)
        {
            if (ChatManager.Instance.yaJugamosUnaPartida)
            {
                // Jugador estuvo en partida multijugador → mandar quit y volver al lobby
                ChatManager.Instance.SalirDePartida();
                SceneManager.LoadScene("ChatScene");
            }
            else
            {
                // Nunca se jugó una partida online → salir directamente al menú
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            // Por seguridad, si ChatManager no existe, salimos al menú
            SceneManager.LoadScene("MainMenu");
        }
    }
}
