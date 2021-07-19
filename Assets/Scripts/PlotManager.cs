using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;

    public Sprite[] plantStages;
    int plantStage = 0;

    float timeBtwStage = 2f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            Harvest();
        }
        else
        {
            Plant();
        }
        Debug.Log("마우스 감지");
    }

    void Harvest()
    {
        Debug.Log("수확함");
        isPlanted = false;
        plant.gameObject.SetActive(false);
    }

    void Plant()
    {
        Debug.Log("심음");
        plantStage = 0;
        UpdatePlant();

        isPlanted = true;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }
}
