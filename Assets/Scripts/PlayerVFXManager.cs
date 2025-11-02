using UnityEngine;
using System.Collections;

public class PlayerVFXManager : MonoBehaviour
{
    public GameObject speedEffect;
    public GameObject jumpEffect;
    public GameObject shieldEffect;

    public Renderer playerRenderer;

    public Material speedMaterial;
    public Material shieldMaterial;
    public Material jumpMaterial;

    private Material originalMaterial;

    void Start()
    {
        if (playerRenderer != null)
        {
            originalMaterial = playerRenderer.material;
        }
        DisableAllEffects();
    }

    public void ActivateEffect(PowerUp.PowerUpType type)
    {
        DisableAllEffects();

        switch (type)
        {
            case PowerUp.PowerUpType.Speed:
                if (speedEffect != null) speedEffect.SetActive(true);
                if (playerRenderer != null && speedMaterial != null)
                    playerRenderer.material = speedMaterial;
                break;

            case PowerUp.PowerUpType.Jump:
                if (jumpEffect != null) jumpEffect.SetActive(true);
                if (playerRenderer != null && jumpMaterial != null)
                    playerRenderer.material = jumpMaterial;
                break;

            case PowerUp.PowerUpType.Shield:
                if (shieldEffect != null) shieldEffect.SetActive(true);
                if (playerRenderer != null && shieldMaterial != null)
                    playerRenderer.material = shieldMaterial;
                break;
        }
        StopCoroutine("DisableAfterTime");
        StartCoroutine(DisableAfterTime(10f));
    }

    public void DisableAllEffects()
    {
        if (speedEffect != null) speedEffect.SetActive(false);
        if (jumpEffect != null) jumpEffect.SetActive(false);
        if (shieldEffect != null) shieldEffect.SetActive(false);

        if (playerRenderer != null && originalMaterial != null)
        {
            playerRenderer.material = originalMaterial;
        }
    }

    IEnumerator DisableAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        DisableAllEffects();
    }
}