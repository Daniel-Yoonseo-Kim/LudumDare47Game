﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject playerObject;

    public Hashtable playerSkills = new Hashtable(); //tracks which skills the player has acquierd
    public GameObject[] skillRepository; // edit in editor, put skill prefabs here
    private GameObject skill_1 = null;
    private GameObject skill_2 = null;

    // cool down system /////////////////////////////////////////////////////////////
    private float castTime1 = -1000000;
    private float castTime2 = -1000000;

    Hashtable liveBosses = new Hashtable(); //Tracks if bosses are alive or not
    Hashtable dialogueFlags = new Hashtable(); //tracks which actions that trigger new dialogue have occurred   ??Mayybe use some other kind of list
    public Header.Skills[] neededSkills = new Header.Skills[2]; //holds the 2 skills needed to avert the catastrophe
    public Header.Skills skill1 { get; set; }
    public Header.Skills skill2 { get; set; }
    public int day { get; private set; }
    public string name;
    public int actions;

    public bool dayWin;

    public Header.Bosses currentBoss;

    //change to true if the player uses the skill where it is needed
    /*    public bool skill1Used;
        public bool skill2Used;*/
    public int skillsUsed;
    
    // test only features ////////////////////////////////////////////////////////////
    public void GetBubbleShield() {
        playerSkills[Header.Skills.SHIELD] = true;
        SelectSkill1(Header.Skills.SHIELD);
    }

    public void GetHeal() {
        playerSkills[Header.Skills.HEAL] = true;
        SelectSkill2(Header.Skills.HEAL);
    }

    public void GetBlock() {
        playerSkills[Header.Skills.BLOCK] = true;
        SelectSkill2(Header.Skills.BLOCK);
    }

    public void GetAirHike() {
        playerSkills[Header.Skills.JUMP] = true;
        SelectSkill2(Header.Skills.JUMP);
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);

        liveBosses.Add(Header.Bosses.ZOMBIE, true);
        liveBosses.Add(Header.Bosses.GOAT, true);
        liveBosses.Add(Header.Bosses.DOG, true);
        liveBosses.Add(Header.Bosses.SHARK, true);

        playerSkills.Add(Header.Skills.SHIELD, false);
        playerSkills.Add(Header.Skills.HEAL, false);
        playerSkills.Add(Header.Skills.BLOCK, false);
        playerSkills.Add(Header.Skills.JUMP, false);

        dialogueFlags.Add(Header.Flags.Boss0Die, false);
        dialogueFlags.Add(Header.Flags.Boss0Win, false);
        dialogueFlags.Add(Header.Flags.Boss1Die, false);
        dialogueFlags.Add(Header.Flags.Boss1Win, false);
        dialogueFlags.Add(Header.Flags.Boss2Die, false);
        dialogueFlags.Add(Header.Flags.Boss2Win, false);
        dialogueFlags.Add(Header.Flags.Boss3Die, false);
        dialogueFlags.Add(Header.Flags.Boss3Win, false);


        skill1 = Header.Skills.NULL;
        skill2 = Header.Skills.NULL;

        actions = 0;

        int randInt = Random.Range(0, 4);
        int randInt2;
        do {
            randInt2 = Random.Range(0, 4);
        }while (randInt2 == randInt);
        neededSkills[0] = (Header.Skills)randInt;
        neededSkills[1] = (Header.Skills)randInt2;

        day = 0;
        dayWin = false;
        /*        skill1Used = false;
                skill2Used = false;*/
        skillsUsed = 2;
    }


    /*public void setSkill1Used()
    {
        skill1Used = true;
    }

    public void setSkill2Used()
    {
        skill2Used = true;
    }*/

    public Header.Skills[] getNeededSkills()
    {
        return neededSkills;
    }

    public void changeName(string str)
    {
        name = str;
    }

    public void doAction()
    {

        actions++;
    }

    public bool IsDead(Header.Bosses boss)
    {
        return !(bool)liveBosses[boss];
    }


    //might not need this method
    public void AcquireSkill(Header.Skills skill)
    {
        playerSkills[skill] = true;
    }

    public bool  HasSkill(Header.Skills skill)
    {
        if (skill == Header.Skills.NULL)
            return false;
        return (bool)playerSkills[skill];
    }

    //Might need to make seperate for skill1 and skill2
    public void SelectSkill1(Header.Skills skill)
    {
        skill1 = skill;
        if (skill_1)
            skill_1.GetComponent<SkillTemplate>().OnRemove(1);
        skill_1 = skillRepository[(int)skill];
        skill_1.GetComponent<SkillTemplate>().OnEquipment(1);
    }

    public void SelectSkill2(Header.Skills skill)
    {
        skill2 = skill;
        if (skill_2)
            skill_2.GetComponent<SkillTemplate>().OnRemove(2);
        skill_2 = skillRepository[(int)skill];
        skill_2.GetComponent<SkillTemplate>().OnEquipment(2);
    }

    // casting //////////////////////////////////////////////////////////////
    public void CastSkill_1() {
        if (skill_1 && Time.time - castTime1 >= skill_1.GetComponent<SkillTemplate>().coolDown) {
            Instantiate(skill_1);
            castTime1 = Time.time;
        }
    }

    public void CastSkill_2() {
        if (skill_2 && Time.time - castTime2 >= skill_2.GetComponent<SkillTemplate>().coolDown) {
            Instantiate(skill_2);
            castTime2 = Time.time;
        }
    }

    public Header.Skills GetSkill1()
    {
        return skill1;
    }

    public Header.Skills GetSkill2()
    {
        return skill2;
    }

    //might not need
    public void SetFlag(Header.Flags flag)
    {
        dialogueFlags[flag] = true;
    }

    public bool GetFlag(Header.Flags flag)
    {
        return (bool)dialogueFlags[flag];
    }

    public void AdvanceDay()
    {
        day++;
    }

    public void PlayerDie()
    {
        Debug.Log(playerSkills[Header.Skills.BLOCK]);
        Debug.Log(playerSkills[Header.Skills.HEAL]);
        Debug.Log(playerSkills[Header.Skills.SHIELD]);
        Debug.Log(playerSkills[Header.Skills.JUMP]);
        dayWin = false;
        day++;
        actions = 0;
        if (day >= 4)
        {
            Restart();
        }
        else
        {
            Header.Flags flag;
            switch (currentBoss)
            {
                case Header.Bosses.ZOMBIE:
                    flag = Header.Flags.Boss0Die;
                    break;
                case Header.Bosses.GOAT:
                    flag = Header.Flags.Boss1Die;
                    break;
                case Header.Bosses.DOG:
                    flag = Header.Flags.Boss2Die;
                    break;
                default:
                    flag = Header.Flags.Boss3Die;
                    break;
            }
            SetFlag(flag);
            /*UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");*/
            toTown();
        }
    }

    public void KillBoss(Header.Bosses boss)
    {
            dayWin = true;
        day++;
        actions = 0;
        if (day >= 4)
        {
            Restart();
        }
        else
        {
            switch (boss)
            {
                case Header.Bosses.ZOMBIE:
                    liveBosses[boss] = false;
                   
                    playerSkills[Header.Skills.SHIELD] = true;
                    dialogueFlags[Header.Flags.Boss0Win] = true;
                    break;
                case Header.Bosses.GOAT:
                    liveBosses[boss] = false;
                    playerSkills[Header.Skills.HEAL] = true;
                    dialogueFlags[Header.Flags.Boss1Win] = true;
                    break;
                case Header.Bosses.DOG:
                    liveBosses[boss] = false;
                    playerSkills[Header.Skills.BLOCK] = true;
                    dialogueFlags[Header.Flags.Boss2Win] = true;
                    break;
                case Header.Bosses.SHARK:
                    liveBosses[boss] = false;
                     playerSkills[Header.Skills.JUMP] = true;
                    dialogueFlags[Header.Flags.Boss3Win] = true;
                    break;
            }
            //UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueBoxTesting");
        }
    }

    public void toTown()
    {
        if (dayWin)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Day2Win");
        else if (!dayWin)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Day2Loss");
/*        else if (day < 4)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Day3");*/
        else
            Restart();
    }

    public void Restart()
    {
        Debug.Log("s");
        liveBosses[Header.Bosses.ZOMBIE] = true;
        liveBosses[Header.Bosses.GOAT] = true;
        liveBosses[Header.Bosses.SHARK] = true;
        liveBosses[Header.Bosses.DOG] = true;

        playerSkills[Header.Skills.SHIELD] = false;
        playerSkills[Header.Skills.BLOCK] = false;
        playerSkills[Header.Skills.HEAL] = false;
        playerSkills[Header.Skills.JUMP] = false;

        day = 0;
        actions = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

        skill1 = Header.Skills.NULL;
        skill2 = Header.Skills.NULL;

        dayWin = false;
        /*        skill1Used = false;
                skill2Used = false;*/
        skillsUsed = 2;
        //some flags will be switched



    }

    public void Win()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }

    public void Update()
    {
        if(skillsUsed == 0)
        {
            Win();
        }
        if(day >= 4)
        {
            Restart();
        }
    }
}
