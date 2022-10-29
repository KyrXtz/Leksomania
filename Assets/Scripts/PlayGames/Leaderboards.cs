using GooglePlayGames;
using Gravitons.UI.Modal;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Leaderboards : MonoBehaviour
{
    public Sprite rank1;
    public Sprite rank2;
    public Sprite rank3;
    public Sprite rank0;
    const string scorePrefix = "ΣΚΟΡ : ";
    const string rankPrefix = "";
    public void UpdateAllScores()
    {
        UpdateExtraWords();
        UpdateNormalWords();
        UpdatePassedLevels();
        UpdateSearchedWords();
        UpdateUnlockedLetters();
        UpdateUnlockedPartners();
    }
    static void ShowLeaderboard(string leaderboardId)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
    }
    static void LoadScores(string leaderboardId, Action<IScore> callback)
    {
        Social.LoadScores(leaderboardId, scores => {
            if (scores.Length > 0)
            {
                var localUserId = Social.Active.localUser.id;
                foreach (IScore score in scores)
                {
                    if(score.userID == localUserId)
                    {
                        callback(score);
                    }
                }                   
            }
        });
    }
    static void PostToLeaderboard(long newScore,string leaderboardId)
    {
        Social.ReportScore(newScore, leaderboardId, (bool success) => {
            if (success) Debug.Log("Posted new score to leaderboard");
            else Debug.LogError("Unable to post new score to leaderboard");
        });
    }
    #region PassedLevels
    public void ShowLeaderboardPassedLevels()
    {
        ShowLeaderboard(GPGSIds.leaderboard);
    }
    public void SetScorePassedLevels(long newScore)
    {
        PostToLeaderboard(newScore,GPGSIds.leaderboard);
    }
    public TextMeshProUGUI passedLevelsScore;
    public TextMeshProUGUI passedLevelsRank;
    public Image passedLevelsImg;

    public void UpdatePassedLevels()
    {
        LoadScores(GPGSIds.leaderboard, (score) => { 
            passedLevelsScore.text = scorePrefix + score.value.ToString();
            passedLevelsRank.text = rankPrefix + score.rank.ToString();
            passedLevelsImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;
        });
    }
    #endregion  
    #region Words
    public void ShowLeaderboardWords()
    {
        ShowLeaderboard(GPGSIds.leaderboard_2);
    }
    public void SetScoreWords(long newScore)
    {
        PostToLeaderboard(newScore, GPGSIds.leaderboard_2);
    }
    public TextMeshProUGUI normalWordsScore;
    public TextMeshProUGUI normalWordsRank;
    public Image normalWordsImg;

    public void UpdateNormalWords()
    {
        LoadScores(GPGSIds.leaderboard_2, (score) => {
            normalWordsScore.text = scorePrefix + score.value.ToString();
            normalWordsRank.text = rankPrefix + score.rank.ToString();
            normalWordsImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;

        });
    }
    #endregion
    #region ExtraWords
    public void ShowLeaderboardExtraWords()
    {
        ShowLeaderboard(GPGSIds.leaderboard_3);
    }
    public void SetScoreExtraWords(long newScore)
    {
        PostToLeaderboard(newScore, GPGSIds.leaderboard_3);
    }
    public TextMeshProUGUI extraWordsScore;
    public TextMeshProUGUI extraWordsRank;
    public Image extraWordsImg;

    public void UpdateExtraWords()
    {
        LoadScores(GPGSIds.leaderboard_3, (score) => {
            extraWordsScore.text = scorePrefix + score.value.ToString();
            extraWordsRank.text = rankPrefix + score.rank.ToString();
            extraWordsImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;

        });
    }
    #endregion
    #region SearchedWords
    public void ShowLeaderboardSearchedWords()
    {
        ShowLeaderboard(GPGSIds.leaderboard_4);
    }
    public void SetScoreSearchedWords(long newScore)
    {
        PostToLeaderboard(newScore, GPGSIds.leaderboard_4);
    }
    public TextMeshProUGUI searchedWordsScore;
    public TextMeshProUGUI searchedWordsRank;
    public Image searchedWordsImg;

    public void UpdateSearchedWords()
    {
        LoadScores(GPGSIds.leaderboard_4, (score) => {
            searchedWordsScore.text = scorePrefix + score.value.ToString();
            searchedWordsRank.text = rankPrefix + score.rank.ToString();
            searchedWordsImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;

        });
    }
    #endregion
    #region UnlockedLetters
    public void ShowLeaderboardUnlockedLetters()
    {
        ShowLeaderboard(GPGSIds.leaderboard_5);
    }
    public void SetScoreUnlockedLetters(long newScore)
    {
        PostToLeaderboard(newScore, GPGSIds.leaderboard_5);
    }
    public TextMeshProUGUI unlockedLettersScore;
    public TextMeshProUGUI unlockedLettersRank;
    public Image unlockedLettersImg;

    public void UpdateUnlockedLetters()
    {
        LoadScores(GPGSIds.leaderboard_5, (score) => {
            unlockedLettersScore.text = scorePrefix + score.value.ToString();
            unlockedLettersRank.text = rankPrefix + score.rank.ToString();
            unlockedLettersImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;

        });
    }
    #endregion
    #region UnlockedPartners
    public void ShowLeaderboardUnlockedPartners()
    {
        ShowLeaderboard(GPGSIds.leaderboard_6);
    }
    public void SetScoreUnlockedPartners(long newScore)
    {
        PostToLeaderboard(newScore, GPGSIds.leaderboard_6);
    }
    public TextMeshProUGUI unlockedPartnersScore;
    public TextMeshProUGUI unlockedPartnersRank;
    public Image unlockedPartnersImg;

    public void UpdateUnlockedPartners()
    {
        LoadScores(GPGSIds.leaderboard_6, (score) => {
            unlockedPartnersScore.text = scorePrefix + score.value.ToString();
            unlockedPartnersRank.text = rankPrefix + score.rank.ToString();
            unlockedPartnersImg.sprite = score.rank == 1 ? rank1 : score.rank == 2 ? rank2 : score.rank == 3 ? rank3 : rank0;

        });
    }
    #endregion
}
