using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour 
{
    public bool destroy = true;

    ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine("ParticleProcess");
    }

    IEnumerator ParticleProcess()
    {
        yield return null;

        while( true )
        {
            if( particleSystem.IsAlive(true) == false )
            {
                if (destroy)
                    Destroy(gameObject);
                else
                    gameObject.SetActive(false);
                break;
            }            

            yield return new WaitForSeconds(0.5f);
        }
    }
}
