using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Inventory inv;

    [Header("Math Lock only")]
    public int questNum;
    public Canvas[] questionArray;

    public void QuestionChanger()
    {
        questionArray[questNum].gameObject.SetActive(false);
        questNum += 1;
        questionArray[questNum].gameObject.SetActive(true);
        inv.QCounter();
    }
}
