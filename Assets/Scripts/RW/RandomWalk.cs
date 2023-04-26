using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    public int steps;
    public Sprite defaultSprite;
    private List<Vector2> dirs = new List<Vector2>()
        {new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};

    public void Start()
    {
        randomWalk(steps);
    }

    public void randomWalk(int steps)
    {
        GameObject pointer = new GameObject();
        for (int i = 0; i < steps; i++)
        {
            paint_tile(pointer);
            Vector2 vec = randomDir();
            pointer.transform.position += new Vector3(vec.x, vec.y, 0);
        }
    }

    public Vector2 randomDir()
    {
        return dirs[Random.Range(0, dirs.Count)];
    }

    public void paint_tile(GameObject pointer)
    {
        GameObject g = new GameObject();
        var a=g.AddComponent<SpriteRenderer>();
        a.sprite = defaultSprite;
        g.transform.position = pointer.transform.position;
    }
}