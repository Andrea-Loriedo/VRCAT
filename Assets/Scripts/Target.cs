using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public bool randomColor = true;

    [ColorUsage(true, true)]
    public Color[] colors;

    public Transform box;
    public MeshRenderer outline;
    public MeshRenderer dot;
    public GameObject particles;

    Rigidbody rb;

    float width, height;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Regenerate(float width, float height)
    {   
        this.width = width;
        this.height = height;

        box.localScale = new Vector3(width, height, box.localScale.z);
        
        if (randomColor)
        {
            Color c = colors[Random.Range(0, colors.Length)];
            outline.material.SetColor("_EmissionColor", c);
            outline.material.EnableKeyword("_EMISSION"); 
            dot.material.SetColor("_EmissionColor", c);
            dot.material.EnableKeyword("_EMISSION"); 

            particles.GetComponentInChildren<ParticleSystemRenderer>().material.color = c;
        }
    }

    public void Launch(float speed)
    {
        rb.velocity = Vector3.down * speed;
    }

    public bool CheckHit(Vector3 position, Vector3 velocity)
    {
        float errX = transform.position.x - position.x;
        float errY = transform.position.y - position.y;
        if (Mathf.Abs(errX) <= width / 2f & Mathf.Abs(errY) <= height / 2f)
        {
            Hit(position, velocity);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Hit(Vector3 position, Vector3 velocity)
    {
        rb.AddForceAtPosition(velocity, position, ForceMode.VelocityChange);
        particles.SetActive(true);
        Invoke("DestroyGameObject", 20f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    } 
}
