using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image coverImage;
    [SerializeField] Text nameText;

    private UpgradeData upgradeData;
    private UpgradePanelManager panelManager;
    private int buttonIndex;
    private bool revealed = false;

    public void Set(UpgradeData data, UpgradePanelManager panel, int index)
    {
        upgradeData = data;
        panelManager = panel;
        buttonIndex = index;

        // Asegurar que el sprite se asigna correctamente
        if (upgradeData != null && upgradeData.icon != null)
        {
            icon.sprite = upgradeData.icon;
        }

        icon.enabled = false;
        coverImage.enabled = true;
        if (nameText != null) nameText.text = "???";
        revealed = false;
    }

    public void OnClick()
    {
        if (revealed) return;

        Debug.Log("=== BOT�N CLICKEADO ===");

        revealed = true;
        Reveal();

        // Notificar al panel manager que se seleccion� esta carta
        panelManager.CardSelected(buttonIndex);
    }

    private void Reveal()
    {
        if (upgradeData == null)
        {
            Debug.LogError("UpgradeData es null!");
            return;
        }

        Debug.Log("=== REVELANDO CARTA ===");

        // Asegurar que el sprite est� asignado
        if (upgradeData.icon != null)
        {
            icon.sprite = upgradeData.icon;
            Debug.Log("Sprite asignado: " + upgradeData.icon.name);
        }

        // Mostrar el �cono
        icon.enabled = true;
        icon.color = Color.white;

        // Ocultar el cover
        coverImage.enabled = false;

        if (nameText != null)
            nameText.text = upgradeData.Name;

        Debug.Log("Carta revelada: " + upgradeData.Name);
        Debug.Log("Icon enabled: " + icon.enabled);
        Debug.Log("CoverImage enabled: " + coverImage.enabled);

        // Forzar actualizaci�n
        Canvas.ForceUpdateCanvases();
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.enabled = false;
        coverImage.enabled = true;
        revealed = false;
        if (nameText != null) nameText.text = "???";
    }
}