using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlipCards : MonoBehaviour
{
    public float speed;
    public GameObject spawnCards;
    public float radius;
    public float numberOfSpawn;
    public Transform parentCards;
    public float degree = 360;
    public float spwnTimer=0;
    public float nextSpawntimer=2f;



    // Start is called before the first frame update
    void Start()
    {
        //spawn();
    }
    void spawn()
    {
        float acrdegree=(degree/360) * 2 * Mathf.PI;
        float nextAngle = acrdegree / numberOfSpawn;
        float angle = 0;

        for(var i=0;i<numberOfSpawn;i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            
            GameObject cards = Instantiate(spawnCards, transform.position, Quaternion.identity);
            cards.transform.SetParent(parentCards);
            //var rb = cards.GetComponent<Rigidbody2D>();
            //rb.bodyType = RigidbodyType2D.Kinematic;
            //rb.velocity = new Vector3(x, y) * speed;
            //cards.transform.localPosition = new Vector3(x, y,0) * speed;
            Debug.Log(angle);
            //Debug.Log(y);
            angle += nextAngle;

            Destroy(cards, 0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {

        spwnTimer -= Time.deltaTime;
        if(spwnTimer<=0)
        {
            spawn();
            spwnTimer = nextSpawntimer;
        }
        spawn();
    }
}
