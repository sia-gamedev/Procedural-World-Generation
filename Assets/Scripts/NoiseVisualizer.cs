using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NoiseVisualizer : MonoBehaviour
{
    [SerializeField]
    private NoiseGenerator noiseGenerator;
    [SerializeField]
    private Renderer textureRenderer;
    [SerializeField]
    private Vector2Int textureSize;

    private void Start()
    {
        RegenerateNoiseTexture();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RegenerateNoiseTexture();
        }
    }

    private void RegenerateNoiseTexture()
    {
        Vector3[] positions = GeneratePositions();

        float[] noiseValues = noiseGenerator.GetHeightNoiseValues(positions);
        NormalizeNoiseValues(noiseValues);

        Color[] colors = GenerateColors(noiseValues);

        SetTexture(colors);
    }

    private Color[] GenerateColors(float[] noiseValues)
    {
        Color[] colors = new Color[noiseValues.Length];
        for (int i = 0; i < noiseValues.Length; i++)
        {
            colors[i] = Color.Lerp(Color.black, Color.white, noiseValues[i]);
        }
        return colors;
    }

    private Vector3[] GeneratePositions()
    {
        Vector3[] positions = new Vector3[textureSize.x * textureSize.y];

        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                positions[(y * textureSize.x) + x] = new Vector3(x, 0f, y);
            }
        }

        return positions;
    }

    private void SetTexture(Color[] colors)
    {
        Texture2D texture2D = new Texture2D(textureSize.x, textureSize.y);
        texture2D.SetPixels(colors);
        texture2D.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture2D;
        textureRenderer.transform.localScale = new Vector3(-textureSize.x / 10f, 0f, -textureSize.y / 10f);
    }

    private void NormalizeNoiseValues(float[] noiseValues)
    {
        float maxValue = noiseValues.Max();
        float minValue = noiseValues.Min();

        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Mathf.InverseLerp(minValue, maxValue, noiseValues[i]);
        }
    }
}
