using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private bool playerBullet;

    public int Damage { get { return damage; } set { damage = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if ((transform.position.y > Camera.main.orthographicSize) ||  (transform.position.y < - Camera.main.orthographicSize))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
            if(playerBullet || (!playerBullet && collision.tag != "Enemy"))
            {
                ShipHealth ship = collision.GetComponent<ShipHealth>();
                if(ship != null)
                {
                    ship.Hurt(damage);
                }
                Destroy(this.gameObject);
            }
    }
}
