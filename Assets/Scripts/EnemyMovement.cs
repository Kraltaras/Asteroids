using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int deathDamage;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip baseBoom;

    private bool _exploded;

    public int bonusType;

    // Start is called before the first frame update
    void Start()
    {
        _exploded = false;
        bonusType = 0;

        if (tag == "Enemy" && name != "Bonus(Clone)")
        {
            float[] shipSize = { 0.8f, 1.0f, 1.2f };
            float thisShipSize = shipSize[Random.Range(0, shipSize.Length)];
            Vector3 scale = new Vector3(thisShipSize, thisShipSize);
            transform.localScale = scale;
            ShipHealth thisShip = GetComponent<ShipHealth>();
            thisShip.ChangeScore(2.0f - thisShipSize);
        }
        if(name == "Bonus(Clone)")
        {
            bonusType = Random.Range(1, 3); //there are still 2 type of bonuses
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            switch (bonusType)
            {
                case 1:
                    if (thisSprite != null)
                    {
                        thisSprite.sprite = Resources.Load("BonusBerserk", typeof(Sprite)) as Sprite;
                    }
                    break;
                case 2:
                    if (thisSprite != null)
                    {
                        thisSprite.sprite = Resources.Load("BonusMed", typeof(Sprite)) as Sprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if(transform.position.y < -4)
        {
            StartCoroutine(ShipBaseDestruction());
        }
    }

    IEnumerator ShipBaseDestruction()
    {
        if (name == "Asteroid(Clone)" || name == "Bonus(Clone)")
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (!_exploded)
            {
                soundSource.PlayOneShot(baseBoom);
            }

            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            if (thisSprite != null)
            {
                thisSprite.sprite = Resources.Load("Explosion", typeof(Sprite)) as Sprite;
                _exploded = true;
            }
            
            yield return new WaitForSeconds(0.2f);

            ShipHealth ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipHealth>();
            if (ship != null)
            {
                ship.Hurt(deathDamage);
            }

            Destroy(this.gameObject);
        }
    }
}
