using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AtmosphereBackgroundGradient : MonoBehaviour
{
    [Header("Altitude")]
    public float minHeight = 0f;
    public float maxHeight = 1000f;

    [Header("Gradient de Cores")]
    public Gradient atmosphereGradient;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    private void Update()
    {
        float t = Mathf.InverseLerp(minHeight, maxHeight, transform.position.y);
        t = Mathf.Clamp01(t);

        cam.backgroundColor = atmosphereGradient.Evaluate(t);
    }

#if UNITY_EDITOR
    private void Reset()
    {
        GradientColorKey[] colorKeys = new GradientColorKey[5];
        colorKeys[0] = new GradientColorKey(new Color(0.53f, 0.81f, 0.98f), 0.0f);  // céu azul
        colorKeys[1] = new GradientColorKey(new Color(0.20f, 0.50f, 0.90f), 0.25f); // azul forte
        colorKeys[2] = new GradientColorKey(new Color(0.05f, 0.10f, 0.30f), 0.55f); // alta atmosfera
        colorKeys[3] = new GradientColorKey(new Color(0.02f, 0.02f, 0.08f), 0.80f); // quase espaço
        colorKeys[4] = new GradientColorKey(Color.black, 1.0f);                      // espaço

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        atmosphereGradient = new Gradient();
        atmosphereGradient.SetKeys(colorKeys, alphaKeys);
    }
#endif
}