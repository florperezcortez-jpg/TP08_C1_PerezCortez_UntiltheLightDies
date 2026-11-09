using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private GameObject ramenPrefab;
    [SerializeField] private GameObject chakraPrefab;
    [SerializeField, Range(0f, 1f)] private float lifeChance = 0.15f;
    [SerializeField, Range(0f, 1f)] private float ramenChance = 0.4f;
    [SerializeField, Range(0f, 1f)] private float chakraChance = 0.07f;
    private Transform container;

    private void Start()
    {
        GameObject pickables = GameObject.Find("Pickables");
        if (pickables != null)
        {
            container = pickables.transform;
        }
    }

    public void TryDrop()
    {
        float roll = Random.value;

        if (roll < lifeChance)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity, container);
        }
        else if (roll < lifeChance + ramenChance)
        {
            Instantiate(ramenPrefab, transform.position, Quaternion.identity, container);
        }
        else if (roll < lifeChance + ramenChance + chakraChance)
        {
            Instantiate(chakraPrefab, transform.position, Quaternion.identity, container);
        }
    }
}
