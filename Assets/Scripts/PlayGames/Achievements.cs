using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(waitForAuthentication());
    }
    IEnumerator waitForAuthentication()
    {
        yield return new WaitUntil(() => PlayGamesController.isAuthenticated);
        yield return new WaitForSeconds(1); // gia sigouria
        checkForNotUpdatedAchievements();
    }
    public void showAchievements()
    {
        Social.ShowAchievementsUI();
    }
    public void UpdateAchievementProgress(string achievementName, float newProgress)
    {
        //failsafe , giati mporei na min mpei kan sto callback
        if (newProgress == 100.0f && !PrefsWrapper.GetString("NotSavedAchievements", "").Contains("$" + achievementName + "$"))
        {
            PrefsWrapper.SetString("NotSavedAchievements", PrefsWrapper.GetString("NotSavedAchievements", "") + "$" + achievementName + "$");
            PrefsWrapper.Save();
        }
        Social.ReportProgress(achievementName, newProgress, (bool success) => {
            // handle success or failure
            if (success) 
            {
                PrefsWrapper.SetString("NotSavedAchievements", PrefsWrapper.GetString("NotSavedAchievements", "").Replace("$"+achievementName+"$",""));
                PrefsWrapper.Save();
            }
        });
    }
    void checkForNotUpdatedAchievements()
    {
        var achievements = PrefsWrapper.GetString("NotSavedAchievements", "").Split('$');
        foreach (var ach in achievements)
        {
            if (ach == "") continue;
            UpdateAchievementProgress(ach, 100.0f);
        }
    }
    public void Rated()
    {
        checkForNotUpdatedAchievements();
        UpdateAchievementProgress(GPGSIds.achievement,100.0f);
    }
    public void BetaTester()
    {
        checkForNotUpdatedAchievements();
        UpdateAchievementProgress(GPGSIds.achievement_beta_tester, 100.0f);
    }
    public void PartnerUnlocked(int no)
    {
        checkForNotUpdatedAchievements();
        switch (no)
        {
            case 0:
                break;
            case 1:
                UpdateAchievementProgress(GPGSIds.achievement_malinois, 100.0f);
                break;
            case 2:
                UpdateAchievementProgress(GPGSIds.achievement_beagle, 100.0f);
                break;
            case 3:
                UpdateAchievementProgress(GPGSIds.achievement_doberman, 100.0f);
                break;
            case 4:
                UpdateAchievementProgress(GPGSIds.achievement_gold_chicken, 100.0f);
                break;
            default:
                break;
        }
    }
    public void LetterUnlocked(int letter)
    {
        checkForNotUpdatedAchievements();
        switch (letter)
        {
            case 4:
                UpdateAchievementProgress(GPGSIds.achievement_4, 100.0f);
                break;
            case 5:
                UpdateAchievementProgress(GPGSIds.achievement_5, 100.0f);
                break;
            case 6:
                UpdateAchievementProgress(GPGSIds.achievement_6, 100.0f);
                break;
            case 7:
                UpdateAchievementProgress(GPGSIds.achievement_7, 100.0f);
                break;
            case 8:
                UpdateAchievementProgress(GPGSIds.achievement_8, 100.0f);
                break;
            case 9:
                UpdateAchievementProgress(GPGSIds.achievement_9, 100.0f);
                break;
            case 10:
                UpdateAchievementProgress(GPGSIds.achievement_10, 100.0f);
                break;
            case 11:
                UpdateAchievementProgress(GPGSIds.achievement_11, 100.0f);
                break;
            case 12:
                UpdateAchievementProgress(GPGSIds.achievement_12, 100.0f);
                break;
            case 13:
                UpdateAchievementProgress(GPGSIds.achievement_13, 100.0f);
                break;
            case 14:
                UpdateAchievementProgress(GPGSIds.achievement_14, 100.0f);
                break;
            case 15:
                UpdateAchievementProgress(GPGSIds.achievement_15, 100.0f);
                break;
            case 16:
                UpdateAchievementProgress(GPGSIds.achievement_16, 100.0f);
                break;
            case 17:
                UpdateAchievementProgress(GPGSIds.achievement_17, 100.0f);
                break;
            case 18:
                UpdateAchievementProgress(GPGSIds.achievement_18, 100.0f);
                break;
            case 19:
                UpdateAchievementProgress(GPGSIds.achievement_19, 100.0f);
                break;
            case 20:
                UpdateAchievementProgress(GPGSIds.achievement_20, 100.0f);
                break;
            case 22:
                UpdateAchievementProgress(GPGSIds.achievement_22, 100.0f);
                break;
            default:
                break;
        }
    }
    public void ExtraWords(int count)
    {
        checkForNotUpdatedAchievements();
        switch (count)
        {
            case 1:
                UpdateAchievementProgress(GPGSIds.achievement_2, 100.0f);
                break;
            case 10:
                UpdateAchievementProgress(GPGSIds.achievement_10_2, 100.0f);
                break;
            case 50:
                UpdateAchievementProgress(GPGSIds.achievement_50, 100.0f);
                break;
            case 100:
                UpdateAchievementProgress(GPGSIds.achievement_100, 100.0f);
                break;
            case 300:
                UpdateAchievementProgress(GPGSIds.achievement_300, 100.0f);
                break;
            case 500:
                UpdateAchievementProgress(GPGSIds.achievement_500, 100.0f);
                break;
            case 1000:
                UpdateAchievementProgress(GPGSIds.achievement_1000, 100.0f);
                break;
            case 10000:
                UpdateAchievementProgress(GPGSIds.achievement_10000, 100.0f);
                break;
            case 100000:
                UpdateAchievementProgress(GPGSIds.achievement_100000, 100.0f);
                break;
        }
    }
    public void LevelsPassed(int count)
    {
        checkForNotUpdatedAchievements();
        switch (count)
        {
            case 1:
                UpdateAchievementProgress(GPGSIds.achievement_3, 100.0f);
                break;
            case 10:
                UpdateAchievementProgress(GPGSIds.achievement_10_3, 100.0f);
                break;
            case 20:
                UpdateAchievementProgress(GPGSIds.achievement_20_2, 100.0f);
                break;
            case 50:
                UpdateAchievementProgress(GPGSIds.achievement_50_2, 100.0f);
                break;
            case 100:
                UpdateAchievementProgress(GPGSIds.achievement_100_2, 100.0f);
                break;
            case 300:
                UpdateAchievementProgress(GPGSIds.achievement_300_2, 100.0f);
                break;
            case 500:
                UpdateAchievementProgress(GPGSIds.achievement_500_2, 100.0f);
                break;
            case 1000:
                UpdateAchievementProgress(GPGSIds.achievement_1000_2, 100.0f);
                break;
            case 2000:
                UpdateAchievementProgress(GPGSIds.achievement_2000, 100.0f);
                break;
            case 5000:
                UpdateAchievementProgress(GPGSIds.achievement_5000, 100.0f);
                break;
            case 10000:
                UpdateAchievementProgress(GPGSIds.achievement_10000_2, 100.0f);
                break;           
            case 20000:
                UpdateAchievementProgress(GPGSIds.achievement_20000, 100.0f);
                break;
        }
    }
    public void WordsSearched(int count)
    {
        checkForNotUpdatedAchievements();
        switch (count)
        {
            case 1:
                UpdateAchievementProgress(GPGSIds.achievement_word_search, 100.0f);
                break;
            case 10:
                UpdateAchievementProgress(GPGSIds.achievement_10_4, 100.0f);
                break;
            case 20:
                UpdateAchievementProgress(GPGSIds.achievement_20_3, 100.0f);
                break;
            case 50:
                UpdateAchievementProgress(GPGSIds.achievement_50_3, 100.0f);
                break;
            case 100:
                UpdateAchievementProgress(GPGSIds.achievement_100_3, 100.0f);
                break;
            case 300:
                UpdateAchievementProgress(GPGSIds.achievement_300_3, 100.0f);
                break;
            case 500:
                UpdateAchievementProgress(GPGSIds.achievement_500_3, 100.0f);
                break;
            case 1000:
                UpdateAchievementProgress(GPGSIds.achievement_1000_3, 100.0f);
                break;
            case 5000:
                UpdateAchievementProgress(GPGSIds.achievement_5000_2, 100.0f);
                break;
            case 10000:
                UpdateAchievementProgress(GPGSIds.achievement_10000_3, 100.0f);
                break;
            case 100000:
                UpdateAchievementProgress(GPGSIds.achievement_100000_2, 100.0f);
                break;
        }
    }

}
