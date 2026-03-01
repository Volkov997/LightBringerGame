using UnityEngine;

public class Illuminator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static readonly float lightRatio = 0.8f;

    public float intensity = 10f;
    private GameObject aoe;
    private Light lightinstance;
    private float radius;
    private Color materialColor;
    void Start()
    {
        GameObject aoePrefab = Resources.Load<GameObject>("Prefabs/LightAoe");
        aoe = Instantiate(aoePrefab, transform.position, Quaternion.identity);
        aoe.transform.localScale = Vector3.one * radiusFromIntensity();
        lightinstance = gameObject.AddComponent<Light>();
        materialColor = aoe.GetComponent<Renderer>().material.color;
        lightinstance.color = materialColor;
        updateRadius();
    }

    // Update is called once per frame
    void Update()
    {
        updateRadius();
    }
    void updateRadius()
    {   
        lightinstance.intensity = intensity*lightRatio;
        radius = radiusFromIntensity();
        aoe.transform.localScale = Vector3.one * radius;
        lightinstance.range = radius*1.2f;
    }

    private float radiusFromIntensity()
    {
        return Mathf.Sqrt(intensity);
    }
}
