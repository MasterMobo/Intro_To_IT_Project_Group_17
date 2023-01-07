using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float damage;
    public float knockbackForce;
    public float range = 15f;
    public float shootRate = 1f;
    private float shootCountdown = 0f;

    [Header("Unity Setup")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform shootPoint;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null &&  shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            return;
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.forward);
        lookRotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
        lookRotation.x = 0f;
        lookRotation.y = 0f;
        partToRotate.rotation = lookRotation;

        if (shootCountdown <= 0f)
        {   
            animator.SetBool("shootAni", true);
            Shoot();
            StartCoroutine(ExecuteAfterTime(0.15f));
            shootCountdown = 1f / shootRate;
        }

        shootCountdown -= Time.deltaTime;
    }

    void Shoot()
    {   
        GameObject projectileGO = (GameObject)Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        projectileGO.transform.parent = transform;
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
            projectile.Seek(target);
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    IEnumerator ExecuteAfterTime(float time)
            {
                yield return new WaitForSeconds(time);
                animator.SetBool("shootAni", false);
            }

}
