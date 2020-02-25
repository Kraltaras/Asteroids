using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float score;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip cosmicBoom;

    private int _maxHealth;
    private bool _exploded;

    public int Health
    {
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = health;
        _exploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0 && tag != "Player")
        {
                StartCoroutine(ShipDestruction());
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public void Heal(int healing)
    {
        health += healing;
        if (health >= _maxHealth) health = _maxHealth;
    }

    public void ChangeScore(float scoreModifier)
    {
        float newScore = score * scoreModifier;
        score = (int)newScore;
    }

    IEnumerator ShipDestruction()
    {
        if (name != "Bonus(Clone)")
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            if (!_exploded)
            {
                soundSource.PlayOneShot(cosmicBoom);
            }
            if (thisSprite != null)
            {
                thisSprite.sprite = Resources.Load("Explosion", typeof(Sprite)) as Sprite;
                _exploded = true;
            }

            yield return new WaitForSeconds(0.2f);

            GameController gameScore = FindObjectOfType<GameController>();
            gameScore.IncreaseScore((int)score);

            Destroy(this.gameObject);
        }
        else
        {
            if (!_exploded)
            {
                AudioSource audioController = FindObjectOfType<EnemyController>().GetComponent<AudioSource>();
                audioController.PlayOneShot(cosmicBoom);
                _exploded = true;
            }
            switch (GetComponent<EnemyMovement>().bonusType)
            {
                case 1:
                    Blasting playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Blasting>();
                    playerShooting.StartCoroutine(playerShooting.BerserkMode());
                    break;
                case 2:
                    ShipHealth playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipHealth>();
                    if (playerHP != null)
                    {
                        playerHP.Heal(5);
                    }
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
