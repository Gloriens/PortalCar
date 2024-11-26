using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    private GameObject[] portals = new GameObject[2];
    public GameObject bluePortal;
    public GameObject orangePortal;
    GameObject chosenPortal;
    private int portalIndexCheck;
    
    private Renderer portalRenderer;
    private float portalHeight;
    
    private AudioClip[] portalSounds = new AudioClip[2];
    public AudioClip enterSound; // Portala girildiğinde çalacak ses
    public AudioClip exitSound;
    private AudioSource audioSource;
    private int soundCheck;
    
    // Şarkı için AudioSource ve clip tanımlamaları
    public AudioClip backgroundMusic;  // Şarkı dosyasını burada tanımlıyoruz
    private AudioSource musicSource;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        portalIndexCheck = 0;
        portals[0] = bluePortal;
        portals[1] = orangePortal;
        
        portalSounds[0] = enterSound;
        portalSounds[1] = exitSound;
        
        // Müzik kaynağını başlatıyoruz
        musicSource = gameObject.AddComponent<AudioSource>();  // Aynı GameObject üzerine yeni bir AudioSource ekliyoruz
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;  // Şarkının sürekli çalması için loop modunu açıyoruz
        musicSource.Play();  // Müzik çalmaya başlar
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            int layerMask = ~LayerMask.GetMask("trigger");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                
                if (hit.collider.gameObject.tag == "Road" )
                {
                    if(hit.collider.gameObject.tag != "Kaldırım")
                    {
                        chosenPortal = bluePortal;
                        portalRenderer = chosenPortal.GetComponent<Renderer>();
                        portalHeight = portalRenderer.bounds.size.y;
                    
                        Vector3 portalPosition = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
                    
                        chosenPortal.transform.position = portalPosition;
                        audioSource.clip = portalSounds[0];
                        audioSource.Play();
                    }
                }
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            int layerMask = ~LayerMask.GetMask("trigger");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                
                if (hit.collider.gameObject.tag == "Road")
                {
                    if(hit.collider.gameObject.tag != "Kaldırım")
                    {
                        chosenPortal = orangePortal;
                        portalRenderer = chosenPortal.GetComponent<Renderer>();
                        portalHeight = portalRenderer.bounds.size.y;
                    
                        Vector3 portalPosition = new Vector3(hit.point.x, hit.point.y + (2), hit.point.z);
                    
                        chosenPortal.transform.position = portalPosition;
                        audioSource.clip = portalSounds[1];
                        audioSource.Play();
                    }
                    
                }
            }
        }
    }
    
    private GameObject portalcheck()
    {
        if (portalIndexCheck == 0)
        {
            soundCheck = 0;
            portalIndexCheck++;
            return portals[0];
        }
        else
        {
            portalIndexCheck--;
            soundCheck = 1;
            return portals[1];
        }
    }
}
