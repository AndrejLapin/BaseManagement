using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    bool m_damageResources = false;
    bool m_damageEnemies = false;
    bool m_singular = true;
    float m_damage = 0.0f;
    float m_presistance = 0.0f;
    float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > m_presistance)
        {
            Destroy(gameObject);
        }
    }

    public void Init(float damage = 0.0f, float presistance = 0.0f, bool singular = true, bool damageResources = true, bool damageEnemies = false)
    {
        m_damage = damage;
        m_presistance = presistance;
        m_singular = singular;
        m_damageResources = damageResources;
        m_damageEnemies = damageEnemies;
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(m_damageResources)
        {
            ResourceNode resourceNodeComponent = collider.GetComponent<ResourceNode>();
            if(resourceNodeComponent)
            {
                resourceNodeComponent.Damage(m_damage);
                if(m_singular)
                {
                    Destroy(gameObject);
                }
            }
        }

        if(m_damageEnemies)
        {
            if(m_singular)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(m_damageResources)
        {
            ResourceNode resourceNodeComponent = collider.GetComponent<ResourceNode>();
            if(resourceNodeComponent)
            {
                resourceNodeComponent.Damage(m_damage);
                if(m_singular)
                {
                    Destroy(gameObject);
                }
            }
        }

        if(m_damageEnemies)
        {
            if(m_singular)
            {
                Destroy(gameObject);
            }
        }
    }
}
