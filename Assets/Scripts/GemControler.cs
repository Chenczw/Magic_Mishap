﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemControler : MonoBehaviour
{
    [SerializeField] private GameObject itemFeedback;

    private LevelController levelController;

    public void Start()
    {
        levelController = LevelController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play an "item pickup" animations
            GameObject.Instantiate(itemFeedback, this.transform.position, this.transform.rotation);

            // Increase player item pickup counter
            levelController.PickedUpItem();

            Destroy(this.gameObject);
        }
    }
}
