﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	public float proximidad = 5.0f;
	Animator animacion;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		animacion = target.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(target.position);
		if (!agent.pathPending && agent.remainingDistance < proximidad)
        {
            Debug.Log("Peligro");
            // cambiamos a true la variable del animator
            animacion.SetBool("isNear", true);
        }
        else
        {
            Debug.Log("Tranqui");
            // cambiamos a false la variable del animator
            animacion.SetBool("isNear", false);
        }
	}
}