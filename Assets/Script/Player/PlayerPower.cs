using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Hastable = ExitGames.Client.Photon.Hashtable;

public class PlayerPower : MonoBehaviourPunCallbacks
{
    [Header("Stat:")]
    public bool alive;
    public int currentHP;
    public int maxHP;
    public float moveSpeed;
    public int damage;

    [Header("Effect:")]
    public bool slow;
    public bool blind;
    public bool stun;
    float slowTime;
    float blindTime;
    float stunTime;
    public bool isBleeding;

    [Header("Inventory:")]
    public int bullet;
    public int medicine;
    public float armor;
    public int slowBomb;
    public int blindBomb;
    public int stunBomb;

    [Header("GUI:")]
    public Image hpImage;
    public Image armorImage;
    public Text bulletTxt;
    public Text medicineTxt;
    public Text slowTxt;
    public Text blindTxt;
    public Text stunTxt;


    [Header("Other:")]
    public GameObject sight;
    
    string lastImpact = "nu11";
    PhotonView view;
    public GameObject skull;

    GameObject[] PlayerUI;
    Vector3 killingNotifPos;
    public GameObject killingNotifPrefab;
    public GameObject deathExplosionPrefab;
    

    public GameObject stunFx;
    public GameObject slowFx;
    public GameObject blindFx;
    public GameObject armorFx;
    public GameObject bleedingFx;

    private float bleedingDelay = 0.5f;
    private float currenBleedingDelay;
    private int bleedingDamage = 1;

    public GameObject settingMenu;
    public bool settingMenuEnabled;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        SetDefaultValue();

        PlayerUI = GameObject.FindGameObjectsWithTag("PlayerUI");
    }

    void SetDefaultValue()
    {
        maxHP = 100;
        currentHP = maxHP;
        moveSpeed = 5f;
        damage = 5;

        slow = false;
        blind = false;
        stun = false;
        isBleeding = false;

        bullet = 99;
        medicine = 5;
        armor = 0f;
        slowBomb = 1;
        blindBomb = 1;
        stunBomb = 1;

        alive = true;

        currenBleedingDelay = bleedingDelay;

        settingMenuEnabled = false;
    }

    void UpdateGUI()
    {
        hpImage.fillAmount = (float)currentHP / maxHP;
        armorImage.fillAmount = Mathf.Max(armor, 0) / 20f;

        medicineTxt.text = medicine.ToString();
        bulletTxt.text = bullet.ToString();
        slowTxt.text = slowBomb.ToString();
        blindTxt.text = blindBomb.ToString();
        stunTxt.text = stunBomb.ToString();
        
        settingMenu.SetActive(settingMenuEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        
        if (Input.GetKeyDown(KeyCode.C) && medicine > 0)
        {
            Healing(20);
            medicine--;
        }

        //armor effect
        if (armor > 0)
        {
            armor -= Time.deltaTime;
            armorFx.SetActive(true);
        }
        else
        {
            armorFx.SetActive(false);
        }


        //Slow effect
        slowFx.SetActive(slow);
        
        if (stun) moveSpeed = 0;
        else if (slow) moveSpeed = 2f;
        else moveSpeed = 5f;

        if (slowTime > 0)
        {
            slowTime -= Time.deltaTime;
            slow = true;
        }
        else slow = false;

        //Blind Effect
        blindFx.SetActive(blind);

        sight.SetActive(!blind);

        if (blindTime > 0)
        {
            blindTime -= Time.deltaTime;
            blind = true;
        }
        else blind = false;

        //Stun Effect
        stunFx.SetActive(stun);
        
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            stun = true;
        }
        else stun = false;
        
        //Bleeding Effect
        bleedingFx.SetActive(isBleeding && alive);
        
        if (isBleeding)
        {
            currenBleedingDelay -= Time.deltaTime;
            if (currenBleedingDelay <= 0)
            {
                TakeDamage(bleedingDamage);
                currenBleedingDelay = bleedingDelay;
            }
        }
        else currenBleedingDelay = 0;
        
        
        if (view.IsMine && Input.GetKeyDown(KeyCode.P) && !GetComponent<Player>().endGame) TakeDamage(100);
        if (view.IsMine && Input.GetKeyDown(KeyCode.Escape)) settingMenuEnabled = !settingMenuEnabled;
    }

    public void Healing(int amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }

    public void GetArmor(float time)
    {
        armor = time;
    }

    public void TakeDamage(int amount)
    {
        if (!alive) return;
        
        if (armor > 0) amount = amount / 2;

        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }

    }

    public void TakeSlow(float time)
    {
        slowTime += time;
    }

    public void TakeBlind(float time)
    {
        blindTime += time;
    }

    public void TakeStun(float time)
    {
        stunTime += time;
    }

    public void SetLastImpact(string name)
    {
        lastImpact = name;
    }

    public void Die()
    {
        if (!alive) return;
        
        Debug.Log("Die");
        if (view.IsMine)
            view.RPC("KillingNotification", RpcTarget.All, 
                view.Owner.NickName, lastImpact);

        alive = false;

        GetComponent<Player>().LoseGame();
        GetComponent<PlayerDeath>().ActiveDisabling();
        UpdateAlivePlayer();
    }


    [PunRPC]
    void KillingNotification(string victim, string killer)
    {
        string notif_s;

        if (killer == "nu11") notif_s = victim + " is executed";
        else notif_s = victim + " is killed by " + killer;
        
        for (int i = 0; i < PlayerUI.Length; i++)
        {
            killingNotifPos = PlayerUI[i].transform.position + new Vector3(0,3,0);
            
            GameObject notif = Instantiate(killingNotifPrefab, killingNotifPos,
                Quaternion.identity);
            
            notif.transform.parent = PlayerUI[i].transform;
            notif.GetComponent<TextMeshPro>().text = notif_s;
            Destroy(notif, 2f);
        }

        GameObject deathExplosion = Instantiate(deathExplosionPrefab,
            transform.position, Quaternion.identity);
        Destroy(deathExplosion, 1f);

        Instantiate(skull, transform.position, Quaternion.identity);
    }


    public void UpdateAlivePlayer()
    {
        if (view.IsMine)
        {
            Debug.Log("Alive -1");

            int alivePlayer = int.Parse(PhotonNetwork.CurrentRoom.CustomProperties["alivePlayer"].ToString()) - 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hastable() {{"alivePlayer", alivePlayer}});
        }
    }
}
