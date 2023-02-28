using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;

public class ScorePopupSpawner : MonoBehaviour
{
    // private TMP_Text totalPointsLabel;
    // public ScorePopupCanvas scorePopupCanvasPrefab;
    //
    // public void SpawnPopups(Tourist tourist, )
    // {
    //     transform.position = tourist.transform.position;
    //     StartCoroutine(_SpawnPopups());
    // }
    //
    // public void SpawnPopups(Vector2 pos, ScoreModBundle bundle)
    // {
    //     transform.position = pos;
    //     totalPointsLabel.gameObject.SetActive(false);
    //     
    //     StartCoroutine(_SpawnPopups(bundle));
    // }
    //
    // private IEnumerator Despawn()
    // {
    //     yield return new WaitForSeconds(0.75f);
    //     Destroy(gameObject);
    // }
    //
    // private static int SortByEnum(ScoreMod m1, ScoreMod m2)
    // {
    //     return Convert.ToInt32(m1.modType).CompareTo(Convert.ToInt32(m2.modType));
    // }
    //
    // private IEnumerator _SpawnPopups(Sprite icon, int points)
    // {
    //     bundle.mods.Sort(SortByEnum);
    //     
    //     int totalPoints = 0;
    //     List<ScoreMod> c_mods = new List<ScoreMod>(bundle.mods);
    //     float bonusPointRatio = 1;
    //     foreach (ScoreMod mod in c_mods)
    //     {
    //         ScorePopupCanvas canvas = Instantiate(scorePopupCanvasPrefab, transform.position, Quaternion.identity);
    //         
    //         int pointValue = mod.DetermineTotalPointValue(totalPoints);
    //         canvas.AssignTo(mod.icon, pointValue);
    //         scoreCounter.IncreaseScore(pointValue, mod);
    //         
    //         totalPoints += pointValue;
    //         if (entity) bonusPointRatio = totalPoints * (1.0f / entity.basePointsOnKO);
    //         
    //         totalPointsLabel.text = "+" + totalPoints;
    //         float scale = Mathf.Clamp(1 + ((bonusPointRatio - 1) / 3), 1, 2);
    //         transform.localScale = new Vector2(scale, scale);
    //         
    //         sfxPlayer.Trigger(1 + ((bonusPointRatio - 1) / 3));
    //         
    //         yield return new WaitForSeconds(0.25f);
    //     }
    //     
    //     scoreCounter.RegisterPoints(totalPoints);
    //     
    //     yield return new WaitForSeconds(0.75f);
    //     Destroy(gameObject);
    // }
}