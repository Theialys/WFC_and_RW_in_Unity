using UnityEngine;

public class stoopid_tests : MonoBehaviour
{
    public Sprite default3r4;

    public Sprite defaultD;
    // Start is called before the first frame update
    void Start()
    {
        var a = new GameObject();
        a.transform.position = new Vector3(0, 0, 0);
        var b =a.AddComponent<SpriteRenderer>();
        b.sprite = default3r4;
       // print(default3r4.bounds);
       var c = new GameObject();
       //x y <- those two improtant for 2D z
       var d = default3r4.bounds.size;
       var e = default3r4.bounds.center;
       Vector3 pos = new Vector3(e.x+d.x*2/3,e.y+d.y*1/2,2);
       c.transform.position = pos;
       var z = c.AddComponent<SpriteRenderer>();
       z.sprite = defaultD;
       var u = defaultD.bounds.extents.y;
       c.transform.position += new Vector3(0, u, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
