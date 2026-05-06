using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // * for some stuff - like health bar, cloud on edges during knockback mode, shield cast, etc.

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private RectTransform healthBarFill;
    
    [SerializeField]
    private UI_SO uiSO;

    [SerializeField]
    private TMP_Text cooldownText;
    [SerializeField]
    private float cooldownTextDuration = 2f;

    void OnEnable()
    {
        uiSO.setPlayerUI(this);
    }

    public void SetCooldownText(string message)
    {
        cooldownText.text = message;
        StartCoroutine(ClearCooldownTextAfterDelay(cooldownTextDuration));
    }

    private IEnumerator ClearCooldownTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        cooldownText.text = "";
    }

    public void UpdateHPBar(float currentHP, float maxHP)
    {
        float fillAmount = currentHP / maxHP;
        healthBarFill.localScale = new Vector3(fillAmount, 1f, 1f);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}