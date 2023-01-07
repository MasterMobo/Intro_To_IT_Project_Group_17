using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public Tower tower;
    public float speed = 70f;
    public GameObject impactEffect;
    public void Seek (Transform _target)
    {
        tower = gameObject.GetComponentInParent<Tower>();
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget(tower.damage, dir * tower.knockbackForce);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    
    }

    void HitTarget(float damage, Vector3 knockback)
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 0.1f);
        //Destroy(target.gameObject);
        Destroy(gameObject);

        target.gameObject.GetComponent<Enemy>().OnHit(damage, knockback);
    }
}
