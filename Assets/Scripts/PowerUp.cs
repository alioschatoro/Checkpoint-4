using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Speed,
        Jump,
        Shield
    }

    public PowerUpType type;
    public float reappearTime = 5.0f;

    private MeshRenderer meshRenderer;
    private Collider itemCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        itemCollider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (itemCollider.enabled && other.CompareTag("Player"))
        {
            PlayerVFXManager vfxManager = other.GetComponent<PlayerVFXManager>();

            if (vfxManager != null)
            {
                vfxManager.ActivateEffect(type);

                SetItemActive(false);

                Invoke(nameof(Reappear), reappearTime);
            }
        }
    }

    void Reappear()
    {
        SetItemActive(true);
    }

    void SetItemActive(bool active)
    {
        if (meshRenderer != null)
            meshRenderer.enabled = active;

        if (itemCollider != null)
            itemCollider.enabled = active;
    }
}