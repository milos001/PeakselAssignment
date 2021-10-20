using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int[] bonusItemSlots;
    public bool gameEnded;
    private int score;
    private int time = 120;
    private int bonusSlotCurrent;
    
    private int allSame = 40;
    private int allDiff = 30;
    private int threeTwo = 35;

    public TextMeshProUGUI scoreNumber;
    public TextMeshProUGUI scoreNumberGameEnd;
    public TextMeshProUGUI timeNumber;
    private Animator bonusNotif;

    public GameObject[] bonusSlots;
    public GameObject endGameCanvas;
    public GameObject[] bonusItems;
    public GameObject girlAnimation;
    public GameObject confetti;

    private void Start()
    {
        //since timescale goes to 0.001 at the end of the game, it needs to be reset at the start of each
        Time.timeScale = 1;
        bonusNotif = GameObject.FindWithTag("Notification").GetComponent<Animator>();

        //define array for caught items and their corresponding spots
        bonusItems = new GameObject[5] ;
        bonusItemSlots = new []{0,0,0,0,0};

        InvokeRepeating(nameof(LoseTime), 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            //if a item is caught by chest : 
            GameObject temp = other.gameObject;
            score += temp.GetComponent<ItemFall>().itemScore;

            //move it to one of the available slots going left to right
            temp.GetComponent<ItemFall>().caught = true;
            temp.transform.localScale *= .5f;
            temp.transform.position = bonusSlots[bonusSlotCurrent].transform.position;
            
            /*if item caught is the first out of 5 place it in the first slot, else check if there is another of the same item in any slots
            if so we add 1 to the corresponding int value in bonusItemSlots, otherwise place it in an empty slot */
            for (int i = 0; i < 5; i++)
            {
                if (bonusItemSlots[i] == 0 )
                {
                    bonusItemSlots[i] = 1;
                    break;
                }
                if (bonusItems[i].GetComponent<ItemFall>().itemScore == temp.GetComponent<ItemFall>().itemScore)
                {
                    bonusItemSlots[i]++;
                    break;
                }
            }

            bonusItems[bonusSlotCurrent] = temp;
            //add it's itemScore value to the overall score
            scoreNumber.text = "" + score;
            //move on to the next slot
            bonusSlotCurrent++;

            //if all 5 bonusSlots are full check for bonus
            //if 5 of the same item add 40 points, if 3 and 2 add 35 points, if all 5 items are different add 30 points to the score
            if (bonusSlotCurrent == 5)
            {
                int tempOnes = 0;
                
                for (int i = 0; i < 5; i++)
                {
                    if (bonusItemSlots[i] == 5)
                    {
                        score += allSame;
                        bonusNotif.Play("BonusNotifAni");
                        bonusNotif.GetComponentInChildren<TextMeshProUGUI>().text = "+" + allSame;
                        scoreNumber.text = "" + score;
                    }
                    else if (bonusItemSlots[i] == 3)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (bonusItemSlots[j] == 2)
                            {
                                score += threeTwo;
                                bonusNotif.Play("BonusNotifAni");
                                bonusNotif.GetComponentInChildren<TextMeshProUGUI>().text = "+" + threeTwo;
                                scoreNumber.text = "" + score;
                            }
                        }
                    }
                    else if (bonusItemSlots[i] == 1) tempOnes++;
                    
                }

                if (tempOnes == 5)
                {
                    score += allDiff;
                    bonusNotif.Play("BonusNotifAni");
                    bonusNotif.GetComponentInChildren<TextMeshProUGUI>().text = "+" + allDiff;
                    scoreNumber.text = "" + score;
                }
                bonusItemSlots = new []{0,0,0,0,0};
                
                //clear slots
                foreach (var item in bonusItems)
                {
                    Destroy(item);
                }

                //reset count
                bonusSlotCurrent = 0;
            }
        }
    }

    //function that ends game
    private void LoseTime()
    {
        time--;
        timeNumber.text = "" + time;

        if (time < 1)
        {
            //show end game screen
            if(PlayerPrefs.GetInt("highscore") < score) PlayerPrefs.SetInt("highscore", score);
            scoreNumberGameEnd.text = "" + score;
            endGameCanvas.SetActive(true);
            //play girl clapping animation and create confetti
            Instantiate(girlAnimation, girlAnimation.transform.position, quaternion.identity);
            Instantiate(confetti, confetti.transform.position, quaternion.identity);
            gameEnded = true;
            //setting timescale to 0.001f so we can play the girl clapping animation without unscaled time
            Time.timeScale = 0.001f;
        }
    }
}