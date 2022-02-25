using UnityEngine;
using Photon.Pun;

public class BulletImpact : MonoBehaviour
{
    public GameObject ImpactPrefab;

    public string source;
    public int damage;

    public void SetSource(string sourceName)
    {
        source = sourceName;
    }

    public void SetDamage(int amount)
    {
        damage = amount;
    }

    private void OnDestroy()
    {
        GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 0.33f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        //Debug.Log(collision.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log(collision.gameObject.GetComponent<PhotonView>().Owner.NickName + " is hitted");
            collision.gameObject.GetComponent<PlayerPower>().TakeDamage(damage);
            collision.gameObject.GetComponent<PlayerPower>().SetLastImpact(source);
        }
    }
}
