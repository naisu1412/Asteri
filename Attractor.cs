    using System.Collections;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

public class Attractor : MonoBehaviour
{

    public Rigidbody rb;
    //Gravitational Constant
    float G = 667.4f;   //Remove this and add to database if available
   // float G = 6.674f * ((float)Math.Pow(10,-11));   //Remove this and add to database if available
    public GameObject boom; // variable for the holder of the explosion
    static List<Attractor> Attractors;
    GameObject destroyedObject;



    void OnEnable()
    {
        // Once the script is enabled, default will always be enabled
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);


        //if (power)
        //{

        //    //rbObject.AddForce(transform.right * thrust);
        //    //rbObject.AddForce(transform.up * thrust);
        //    GetComponent<Rigidbody>().AddForce(transform.forward * thrust); // blue
        //}
    }

    void OnDisable()
    {
        //Once the script is disabled
        Attractors.Remove(this);
    }

    void FixedUpdate()  //Fixed update since 
    {
        if (EditPlanet.atrractorBool)
        {
                
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            foreach (Attractor attractor in Attractors)
            {

                if (attractor != this)
                    Attract(attractor);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        
        

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
            Debug.Log(gameObject.name);

            EditPlanet.lastTouchPlanet = gameObject.name;

        }
    }

    void OnClick()
    {
        Debug.Log("Hi There");
        if (EditPlanet.destroyCounter)
        {
            var cloneBoom = Instantiate(boom, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

            ParticleSystem parts = boom.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(cloneBoom, totalDuration);
            destroyedObject = cloneBoom;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (EditPlanet.collisionBool)
        {
            if (boom)
            {

                var cloneBoom = Instantiate(boom, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);

                ParticleSystem parts = boom.GetComponent<ParticleSystem>();
                float totalDuration = parts.duration + parts.startLifetime;
                Destroy(cloneBoom, totalDuration);
                destroyedObject = cloneBoom;

            }
        }
        else
        {
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(20, 100, 0);
        }
      


    }   //for the explosion

    void Attract(Attractor objToAttract)
    {


        Rigidbody rbToAttract = objToAttract.rb;
        Vector3 direction = rb.position - rbToAttract.position;

        float distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        }

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;



        rbToAttract.AddForce(force);


    }
}
