using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowStage 
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowStage currentStage;

    public SpriteRenderer theSR;
    public Sprite soilTilled, soilWatered;
    
    public SpriteRenderer cropSr;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2,cropRipe;

    public bool isWatered;

    public bool preventUse;

    private Vector2Int gridPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Keyboard.current.eKey.wasPressedThisFrame)
         {
             AdvanceStage();

             SetSoilSprite();
         }*/
        //permet le test seulement dans unity
#if UNITY_EDITOR
        if(Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif
    }


    public void SetSoilSprite()
    {
        if(currentStage == GrowStage.barren)
        {
            theSR.sprite = null;
        }
        else
        {
            if(isWatered)
            {
                theSR.sprite = soilWatered;
            }
            else
            {
                theSR.sprite = soilTilled;
            }
        }
        UpdateGridInfo();
    }

    public void PloughSoil()
    {
        if(currentStage == GrowStage.barren && preventUse == false)
        {
            currentStage = GrowStage.ploughed;
            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        if(preventUse==false)
        {
            isWatered = true;
            SetSoilSprite();
        }
        
    }

    public void PlantCrop()
    {
        if(currentStage == GrowStage.ploughed && isWatered == true && preventUse==false)
        {
            currentStage = GrowStage.planted;

            UpdateCropSprite();
        }
    }

    public void UpdateCropSprite()
    {
        switch(currentStage)
        {
            case GrowStage.planted:
                cropSr.sprite = cropPlanted;
                break;
            case GrowStage.growing1:
                cropSr.sprite = cropGrowing1;
                break;
            case GrowStage.growing2:
                cropSr.sprite = cropGrowing2;
                break;
            case GrowStage.ripe:
                cropSr.sprite = cropRipe;
                break;
        }
        UpdateGridInfo();
    }

    public void AdvanceCrop()
    {
        if (isWatered == true && preventUse==false)
        {
            if(currentStage == GrowStage.planted || currentStage == GrowStage.growing1 || currentStage == GrowStage.growing2)
            {
                currentStage++;
                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if(currentStage == GrowStage.ripe && preventUse == false)
        {
            currentStage = GrowStage.ploughed;
            SetSoilSprite();
            cropSr.sprite = null;
        }
    }

    public void setGridPoistion(int x,int y)
    {
        gridPosition = new Vector2Int(x, y);
    }

    void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPosition.x, gridPosition.y);
    }
}
