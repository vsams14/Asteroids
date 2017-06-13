using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public int radius;
    private List<int> generated = new List<int>();
    private List<string> names = new List<string>();
    private int curr;
    
    public void Generate()
    {
        Texture2D planetTexture = new Texture2D(2 * radius, 2 * radius, TextureFormat.ARGB32, false);
        int baseColor = (int) Random.Range(0.0f, 6f); //yellow, orange, red, purple, blue, green
        generated.Add(baseColor);
        curr = generated.Count-1;
        names.Add(genName());
        for (int x = -radius; x < radius; x++)
        {
            for (int y = -radius; y < radius; y++)
            {
                planetTexture.SetPixel(x + radius, y + radius, new Color(0f, 0f, 0f, 0f));
                if (x*x + y*y < radius*radius)
                {
                    switch (baseColor)
                    {
                        case 0: //yellow
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.7f,1.0f), Random.Range(0.7f,1.0f), Random.Range(0.0f,0.5f), 1f));
                                break;
                            }
                        case 1: //orange
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.9f, 1.0f), Random.Range(0.2f, 0.7f), Random.Range(0.0f, 0.5f), 1f));
                                break;
                            }
                        case 2: //red
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.9f, 1.0f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), 1f));
                                break;
                            }
                        case 3: //purple
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.5f, 0.9f), Random.Range(0.0f, 0.5f), Random.Range(0.7f, 1.0f), 1f));
                                break;
                            }
                        case 4: //blue
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.0f, 0.5f), Random.Range(0.5f, 0.7f), Random.Range(0.7f, 1.0f), 1f));
                                break;
                            }
                        case 5: //green
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.0f, 0.5f), Random.Range(0.7f, 1.0f), Random.Range(0.5f, 0.7f), 1f));
                                break;
                            }
                    }
                }
            }
        }
        planetTexture.Apply();
        Sprite planetSprite = Sprite.Create(planetTexture, new Rect(0, 0, 2 * radius, 2 * radius), new Vector2(0.5f, 0.5f), 100);
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        SR.sprite = planetSprite;
    }

    private void Generate(int baseColor)
    {
        Texture2D planetTexture = new Texture2D(2 * radius, 2 * radius, TextureFormat.ARGB32, false);
        for (int x = -radius; x < radius; x++)
        {
            for (int y = -radius; y < radius; y++)
            {
                planetTexture.SetPixel(x + radius, y + radius, new Color(0f, 0f, 0f, 0f));
                if (x * x + y * y < radius * radius)
                {
                    switch (baseColor)
                    {
                        case 0: //yellow
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.7f, 1.0f), Random.Range(0.7f, 1.0f), Random.Range(0.0f, 0.5f), 1f));
                                break;
                            }
                        case 1: //orange
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.9f, 1.0f), Random.Range(0.2f, 0.7f), Random.Range(0.0f, 0.5f), 1f));
                                break;
                            }
                        case 2: //red
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.9f, 1.0f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), 1f));
                                break;
                            }
                        case 3: //purple
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.5f, 0.9f), Random.Range(0.0f, 0.5f), Random.Range(0.7f, 1.0f), 1f));
                                break;
                            }
                        case 4: //blue
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.0f, 0.5f), Random.Range(0.5f, 0.7f), Random.Range(0.7f, 1.0f), 1f));
                                break;
                            }
                        case 5: //green
                            {
                                planetTexture.SetPixel(x + radius, y + radius, new Color(Random.Range(0.0f, 0.5f), Random.Range(0.7f, 1.0f), Random.Range(0.5f, 0.7f), 1f));
                                break;
                            }
                    }
                }
            }
        }
        planetTexture.Apply();
        Sprite planetSprite = Sprite.Create(planetTexture, new Rect(0, 0, 2 * radius, 2 * radius), new Vector2(0.5f, 0.5f), 100);
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        SR.sprite = planetSprite;
    }

    public void Prev()
    {
        if (curr > 0)
        {
            curr--;
            Generate(generated[curr]);
        }
    }

    public void Next()
    {
        if (curr < (generated.Count-1))
        {
            curr++;
            Generate(generated[curr]);
        }
        else
        {
            Generate();
        }
    }

    public string genName()
    {
        int catalog = (int)Random.Range(0f, 1.999f); //SAO or GSC
        string alpha = "abcdefghijklmnopqrstuvwxyz";
        int planet = (int)Random.Range(0f, 14.999f);
        switch (catalog)
        {
            case 0: //SAO NNNNNN A
                {
                    int star = (int)Random.Range(0f, 999999f);
                    return "SAO\n" + star + " " + alpha[planet];
                }
            case 1: //GSC FFFFF - NNNNN A
                {
                    int fcode = (int)Random.Range(0f, 99999f);
                    int star = (int)Random.Range(0f, 99999f);
                    return "GSC\n" + fcode + "-" + star + " " + alpha[planet];
                }
            default:
                return "";
        }
    }

    public string getName()
    {
        return names[curr];
    }
}
