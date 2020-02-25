using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blasting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private bool trespasser; //trespasser enemy has unique blasting move

    private GameObject _bullet;

    private bool _reloading;

    // Start is called before the first frame update
    void Start()
    {
        _reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_reloading)
        {
            if (trespasser)
            {
                GameObject bullet1 = Instantiate(bulletPrefab) as GameObject;
                bullet1.transform.position = transform.TransformPoint(0.75f, 0.75f, -1);
                bullet1.transform.rotation = transform.rotation;
                bullet1.transform.Rotate(0, 0, -45);
                GameObject bullet2 = Instantiate(bulletPrefab) as GameObject;
                bullet2.transform.position = transform.TransformPoint(-0.75f, 0.75f, -1);
                bullet2.transform.rotation = transform.rotation;
                bullet2.transform.Rotate(0, 0, 45);
            }
            else
            {
                _bullet = Instantiate(bulletPrefab) as GameObject;
                _bullet.transform.position = transform.TransformPoint(0, 0.75f, -1);
                _bullet.transform.rotation = transform.rotation;
            }
            _reloading = true;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        _reloading = false;
    }

    public IEnumerator BerserkMode() //berserk-mode increases attack speed and damage of player
    {
        reloadSpeed /= 2;
        Bullet bullet = bulletPrefab.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Damage *= 2;
        }

        yield return new WaitForSeconds(10); //time of berserk-mode

        reloadSpeed *= 2;
        if(bullet != null)
        {
            bullet.Damage /= 2;
        }
    }
}
