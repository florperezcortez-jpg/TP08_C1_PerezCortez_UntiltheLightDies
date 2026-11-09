using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject prefab;
    [Range(0f, 1f)] public float chance = 0.2f;
}

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private DropItem[] drops;

    public void TryDrop()
    {
      
        if (keyPrefab != null && GameStats.Instance != null && GameStats.Instance.ShouldDropLastKey)
        {
            Instantiate(keyPrefab, transform.position + Vector3.up * 0.3f, Quaternion.identity);
            GameStats.Instance.MarkLastKeyDropped();

            if (HUDMessage.Instance != null)
                HUDMessage.Instance.Show("¡El esqueleto soltó una llave!", 3f);
            return;
        }

      
        float roll = Random.value;
        float accumulated = 0f;

        foreach (DropItem item in drops)
        {
            accumulated += item.chance;
            if (roll < accumulated)
            {
                if (item.prefab != null)
                    Instantiate(item.prefab, transform.position + Vector3.up * 0.3f, Quaternion.identity);
                return;
            }
        }
    }
}