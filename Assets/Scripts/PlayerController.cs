﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Shape
{
    public ProjectileController projectilePrefab;
    private float currentTime=0;
    private float step = 0.5f;

    //private void Awake()
    //{
    //    Debug.Log(gameSceneController.screenBounds);
    //}

    IEnumerator AutoShooting () //otomatik ateş
    {
        while(currentTime<1000000000000)
        {
            yield return new WaitForSeconds(step);
            currentTime += step;
            FireProjectile();
        }
    }


    protected override void Start()
    {
        StartCoroutine(AutoShooting ());
        
        base.Start();

        SetColor(Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        
        MovePlayer();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

   

    private void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if(Mathf.Abs(horizontalMovement) > Mathf.Epsilon)
        {
            horizontalMovement = horizontalMovement * Time.deltaTime * gameSceneController.playerSpeed;
            horizontalMovement += transform.position.x;

            float right = gameSceneController.screenBounds.x - halfWidth;
            float left = -right;

            float limit =
                Mathf.Clamp(horizontalMovement, left, right);

            transform.position = new Vector2(limit, transform.position.y); 
        }
    }

    private void FireProjectile()
    {
        Vector2 spawnPosition = transform.position;

        ProjectileController projectile =
            Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        projectile.projectileSpeed = 2;
        projectile.projectileDirection = Vector2.up;

        
    }
}
