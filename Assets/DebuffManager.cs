// 1. Crear un DebuffManager singleton (nuevo script)
using UnityEngine;
using TMPro;
using System.Collections;

public class DebuffManager : MonoBehaviour
{
    public static DebuffManager Instance { get; private set; }

    [Header("UI References")]
    public TextMeshProUGUI debuffText;
    public float displayDuration = 3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Buscar el TextMeshPro en la escena actual
        FindDebuffTextInScene();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "GameplayScene")
        {
            FindDebuffTextInScene();
        }
    }

    private void FindDebuffTextInScene()
    {
        // Siempre buscar de nuevo cuando se carga la escena
        debuffText = null;

        // Método 1: Buscar por tag (más confiable)
        try
        {
            GameObject textObj = GameObject.FindGameObjectWithTag("DebuffUI");
            if (textObj != null)
            {
                debuffText = textObj.GetComponent<TextMeshProUGUI>();
                Debug.Log("✅ DebuffText encontrado por tag");
            }
        }
        catch
        {
            Debug.Log("Tag DebuffUI no existe, creando...");
        }

        // Método 2: Buscar en todos los Canvas
        if (debuffText == null)
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            Debug.Log("🔍 Buscando en " + canvases.Length + " Canvas encontrados");

            foreach (Canvas canvas in canvases)
            {
                // Buscar en todos los hijos del Canvas
                TextMeshProUGUI[] textsInCanvas = canvas.GetComponentsInChildren<TextMeshProUGUI>(true);
                foreach (var text in textsInCanvas)
                {
                    if (text.name.ToLower().Contains("debuff"))
                    {
                        debuffText = text;
                        Debug.Log("✅ DebuffText encontrado en Canvas: " + canvas.name + ", texto: " + text.name);
                        break;
                    }
                }
                if (debuffText != null) break;
            }
        }

        // Método 3: Buscar directamente por nombre en toda la escena
        if (debuffText == null)
        {
            TextMeshProUGUI[] allTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach (var text in allTexts)
            {
                if (text.gameObject.scene.name != null && text.name.ToLower().Contains("debuff"))
                {
                    debuffText = text;
                    Debug.Log("✅ DebuffText encontrado por Resources: " + text.name);
                    break;
                }
            }
        }

        // Ocultar el texto al inicio
        if (debuffText != null)
        {
            debuffText.gameObject.SetActive(false);
            Debug.Log("✅ DebuffText configurado correctamente");
        }
        else
        {
            Debug.LogWarning("❌ No se pudo encontrar DebuffText en GameplayScene");
            Debug.LogWarning("💡 Asegúrate de que el TextMeshPro contenga 'debuff' en su nombre");
        }
    }

    public void ShowDebuff(string debuffType)
    {
        if (debuffText == null)
        {
            Debug.LogWarning("DebuffText no encontrado en la escena GameplayScene");
            return;
        }

        string message = GetDebuffMessage(debuffType);
        StartCoroutine(DisplayDebuffCoroutine(message));
    }

    private string GetDebuffMessage(string debuffType)
    {
        switch (debuffType)
        {
            case "slow":
                return "¡RALENTIZADO!";
            case "auto-damage":
                return "¡DAÑO AUTOMÁTICO!";
            case "downgrade-skill":
                return "¡HABILIDAD DEGRADADA!";
            default:
                return "¡DEBUFF APLICADO!";
        }
    }

    private IEnumerator DisplayDebuffCoroutine(string message)
    {
        // Mostrar el texto
        debuffText.text = message;
        debuffText.gameObject.SetActive(true);

        // Esperar la duración especificada
        yield return new WaitForSeconds(displayDuration);

        // Ocultar el texto
        debuffText.gameObject.SetActive(false);
    }
}