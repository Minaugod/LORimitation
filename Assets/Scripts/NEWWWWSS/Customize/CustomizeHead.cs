using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
public class CustomizeHead : MonoBehaviour
{
    public CharacterState headState;

    public SpriteRenderer frontHairSr;

    public SpriteRenderer backHairSr;

    public SpriteRenderer eyeSr;

    public SpriteRenderer eyeBrowSr;

    public SpriteRenderer mouthSr;


    private CustomizeParts frontHairParts;
    private CustomizeParts backHairParts;
    private CustomizeParts eyeParts;
    private CustomizeParts eyeBrowParts;
    private CustomizeParts mouthParts;

    public void SetCustomizeData(HeadCustomizeData data)
    {
            
        switch (headState)
        {
            // 정면 기본
            case CharacterState.Default:

            case CharacterState.Move:

                SetFrontNormal(data);

                break;


            // 정면 공격
            case CharacterState.Guard:

            case CharacterState.Evade:

            case CharacterState.Slash:

                SetFrontAttack(data);

                break;


            // 측면 공격
            case CharacterState.Pierce:
            case CharacterState.Blunt:

                SetSideAttack(data);

                break;


            // 정면 피격
            case CharacterState.Damaged:

                SetFrontDamaged(data);

                break;

            default:

                SetFrontNormal(data);

                break;
        }


        SetSpritesAndPositions();
            
    }
        
    void SetSpritesAndPositions()
    {
        frontHairSr.sprite = frontHairParts.partsSprite;
        frontHairSr.transform.localPosition = frontHairParts.partsPos;

        backHairSr.sprite = backHairParts.partsSprite;
        backHairSr.transform.localPosition = backHairParts.partsPos;

        eyeSr.sprite = eyeParts.partsSprite;
        eyeSr.transform.localPosition = eyeParts.partsPos;

        eyeBrowSr.sprite = eyeBrowParts.partsSprite;
        eyeBrowSr.transform.localPosition = eyeBrowParts.partsPos;

        mouthSr.sprite = mouthParts.partsSprite;
        mouthSr.transform.localPosition = mouthParts.partsPos;
    }

    void SetFrontNormal(HeadCustomizeData data)
    {

        frontHairParts = data.frontHairParts.frontHair;

        backHairParts = data.backHairParts.frontHair;

        eyeParts = data.eyeParts.frontNormal;

        eyeBrowParts = data.eyeBrowParts.frontNormal;

        mouthParts = data.mouthParts.frontNormal;

    }

    void SetFrontAttack(HeadCustomizeData data)
    {
        frontHairParts = data.frontHairParts.frontHair;

        backHairParts = data.backHairParts.frontHair;

        eyeParts = data.eyeParts.frontAttack;

        eyeBrowParts = data.eyeBrowParts.frontAttack;

        mouthParts = data.mouthParts.frontAttack;

    }

    void SetSideAttack(HeadCustomizeData data)
    {
        frontHairParts = data.frontHairParts.sideHair;

        backHairParts = data.backHairParts.sideHair;

        eyeParts = data.eyeParts.sideAttack;

        eyeBrowParts = data.eyeBrowParts.sideAttack;

        mouthParts = data.mouthParts.sideAttack;

    }

    void SetFrontDamaged(HeadCustomizeData data)
    {
        frontHairParts = data.frontHairParts.frontHair;

        backHairParts = data.backHairParts.frontHair;

        eyeParts = data.eyeParts.frontDamaged;

        eyeBrowParts = data.eyeBrowParts.frontDamaged;

        mouthParts = data.mouthParts.frontDamaged;
    }
}
