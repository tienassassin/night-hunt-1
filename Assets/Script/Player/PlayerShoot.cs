using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletSparkPrefab;

    public GameObject slowBombPrefab;
    public GameObject blindBombPrefab;
    public GameObject stunBombPrefab;

    private float bulletForce = 20f;

    private PhotonView view;

    private float fireDelay = 0.2f;
    private float currentfireDelay;

    private float slowDelay = 0f;
    private float currentSlowDelay;
    private float blindDelay = 0f;
    private float currentBlindDelay;
    private float stunDelay = 0f;
    private float currentStunDelay;

    PlayerPower pw;

    
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        pw = GetComponent<PlayerPower>();

        SetDefaultValue();
    }

    void SetDefaultValue()
    {
        currentfireDelay = fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        currentfireDelay -= Time.deltaTime;

        if (view.IsMine)
        {
            if (Input.GetMouseButton(0) && currentfireDelay <= 0 && pw.bullet > 0 && !pw.stun && !pw.settingMenuEnabled)
            {
                //Shoot();
                view.RPC("Shoot", RpcTarget.All);
                currentfireDelay = fireDelay;
                pw.bullet--;
            }

            if (Input.GetKeyDown(KeyCode.Q) && currentSlowDelay <= 0 && pw.slowBomb > 0)
            {
                view.RPC("ThrowSlowBomb", RpcTarget.All);
                pw.slowBomb--;
                currentSlowDelay = slowDelay;
            }

            if (Input.GetKeyDown(KeyCode.E) && currentBlindDelay <= 0 && pw.blindBomb > 0)
            {
                view.RPC("ThrowBlindBomb", RpcTarget.All);
                pw.blindBomb--;
                currentBlindDelay = blindDelay;
            }

            if (Input.GetKeyDown(KeyCode.F) && currentStunDelay <= 0 && pw.stunBomb > 0)
            {
                view.RPC("ThrowStunBomb", RpcTarget.All);
                pw.stunBomb--;
                currentStunDelay = stunDelay;
            }
        }
        
    }

    [PunRPC]
    void Shoot()
    {      
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position,
            firePoint.rotation);

        bullet.GetComponent<BulletImpact>().SetDamage(pw.damage);
        bullet.GetComponent<BulletImpact>().SetSource(view.Owner.NickName);

        GameObject bulletSpark = Instantiate(bulletSparkPrefab, firePoint.position,
            firePoint.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce,
            ForceMode2D.Impulse);

        Destroy(bullet, 0.5f);
        Destroy(bulletSpark, 0.2f);

    }

    [PunRPC]
    void ThrowSlowBomb()
    {
        GameObject slowBomb = Instantiate(slowBombPrefab, firePoint.position,
            firePoint.rotation);

        Rigidbody2D slowRb = slowBomb.GetComponent<Rigidbody2D>();
        slowRb.AddForce(firePoint.up * bulletForce * 0.5f,
            ForceMode2D.Impulse);
    }

    [PunRPC]
    void ThrowBlindBomb()
    {
        GameObject blindBomb = Instantiate(blindBombPrefab, firePoint.position,
            firePoint.rotation);

        Rigidbody2D blindRb = blindBomb.GetComponent<Rigidbody2D>();
        blindRb.AddForce(firePoint.up * bulletForce * 0.5f,
            ForceMode2D.Impulse);
    }

    [PunRPC]
    void ThrowStunBomb()
    {
        GameObject stunBomb = Instantiate(stunBombPrefab, firePoint.position,
            firePoint.rotation);

        Rigidbody2D stunRb = stunBomb.GetComponent<Rigidbody2D>();
        stunRb.AddForce(firePoint.up * bulletForce * 0.5f,
            ForceMode2D.Impulse);
    }
}
