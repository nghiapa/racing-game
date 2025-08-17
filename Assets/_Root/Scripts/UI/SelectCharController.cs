using SMPScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharController : MonoBehaviour
{
    public AssetCollection assetCollection;
    public PlayerProfile playerProfile;


    public AutoGetIk currentBike;
    public IkContaniner currentCharacter;

    public List<IkContaniner> characters;
    public List<AutoGetIk> bikes;

    public int currentCharIndex = 0;
    public int currentBikeIndex = 0;

    private void Start()
    {
        assetCollection=GameManager.Instance.assetCollection;
        playerProfile = GameManager.Instance.playerProfile;
    }

    public void OnNextChar()
    {
        currentCharacter.transform.SetParent(transform);

        currentCharacter.gameObject.SetActive(false);
        currentCharIndex++;
        if (currentCharIndex >= characters.Count)
        {
            currentCharIndex = 0;
        }
        currentCharacter = characters[currentCharIndex];
        currentCharacter.gameObject.SetActive(true);

        Refresh();
    }

    public void OnPrevChar()
    {
        currentCharacter.transform.SetParent(transform);

        currentCharacter.gameObject.SetActive(false);
        currentCharIndex--;
        if (currentCharIndex < 0)
        {
            currentCharIndex = characters.Count - 1;
        }
        currentCharacter = characters[currentCharIndex];
        currentCharacter.gameObject.SetActive(true);

        Refresh();
    }

    public void OnNextBike()
    {
        currentCharacter.transform.SetParent(transform);

        currentBike.gameObject.SetActive(false);
        currentBikeIndex++;
        if (currentBikeIndex >= bikes.Count)
        {
            currentBikeIndex = 0;
        }
        currentBike = bikes[currentBikeIndex];
        currentBike.gameObject.SetActive(true);

        Refresh();
    }

    public void OnPrevBike()
    {
        currentCharacter.transform.SetParent(transform);

        currentBike.gameObject.SetActive(false);
        currentBikeIndex--;
        if (currentBikeIndex < 0)
        {
            currentBikeIndex = bikes.Count - 1;
        }
        currentBike = bikes[currentBikeIndex];
        currentBike.gameObject.SetActive(true);

        Refresh();
    }

    public void Refresh()
    {
        currentCharacter.transform.position = currentBike.bikerPos.position;
        currentCharacter.transform.SetParent(currentBike.transform);
        currentBike.IkContainer = currentCharacter;

        MotoProceduralIKHandler motoProceduralIKHandler = currentCharacter.GetComponent<MotoProceduralIKHandler>();
        motoProceduralIKHandler.autoGetIk = currentBike;

        currentCharacter.GetComponent<MotoProceduralIKHandler>().InitIK();

        playerProfile.currentRider = currentCharacter.RiderType;
        playerProfile.currentBike = currentBike.BikeType;
    }

}
