using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Material pps_material;
    public RenderTexture pps_renderTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, pps_renderTexture);
        // Blit the source texture to the destination texture using the material's shader.
        Graphics.Blit(source, destination, pps_material, pps_material.passCount - 1);
    }
}
