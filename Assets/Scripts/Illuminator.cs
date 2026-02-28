using UnityEngine;

public class Illuminator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float intensity = 10f;
    public GameObject aoe;
    void Start()
    {
        GameObject aoePrefab = Resources.Load<GameObject>("Prefabs/LightAoe");
        aoe = Instantiate(aoePrefab, transform.position, Quaternion.identity);
        aoe.transform.localScale = Vector3.one * radiusFromIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        aoe.transform.localScale = Vector3.one * radiusFromIntensity();
    }

    private float radiusFromIntensity()
    {
        return intensity * 0.5f;
    }
}
