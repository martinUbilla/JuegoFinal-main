using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    [SerializeField] private Transform upgradeUIContainer;          // Contenedor en la UI
    [SerializeField] private UpgradeIconUI upgradeIconPrefab;       // Prefab del �cono

    private Dictionary<UpgradeData, UpgradeIconUI> activeIcons = new();

    public void ShowUpgradeIcon(UpgradeData upgradeData)
    {
        // Si ya existe un �cono de esta mejora, actualiza el nivel visual
        if (activeIcons.ContainsKey(upgradeData))
        {
            activeIcons[upgradeData].SetLevel(upgradeData.currentLevel);
        }
        else
        {
            // Instancia el nuevo �cono y gu�rdalo
            UpgradeIconUI icon = Instantiate(upgradeIconPrefab, upgradeUIContainer);
            icon.Initialize(upgradeData.icon, upgradeData.currentLevel, upgradeData);
            activeIcons.Add(upgradeData, icon);
        }
    }
    //asd
    public void TriggerCooldown(UpgradeData upgradeData)
    {
        if (activeIcons.ContainsKey(upgradeData))
        {
            activeIcons[upgradeData].TriggerCooldown();
        }
    }
    

}
