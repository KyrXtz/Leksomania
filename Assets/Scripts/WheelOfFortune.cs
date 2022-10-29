using Gravitons.UI.Modal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    public LevelManager lvlManager;
    public Rewarded rewardedAd;
    bool _isSpinning = false;
    float _rotationIterations = 0;
    int _fortuneSize = 8;
    int _randomSelectedChioceID = 0;
    float wheelSpeed = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartWheel()
    {
        if (_isSpinning) return;
        var startedToday = PrefsWrapper.GetString("hasStartedFortuneToday", "");
        Debug.Log(startedToday);
        if(startedToday!= DateTime.Now.Date.ToShortDateString())
        {
            StartCoroutine(StartFortune());
            PrefsWrapper.SetString("hasStartedFortuneToday", DateTime.Now.Date.ToShortDateString());
            PrefsWrapper.SetInt("hasExtraSpin", 0);
            PrefsWrapper.SetInt("timesExtraSpinnedToday", 0);

            PrefsWrapper.Save();

        }else if(PrefsWrapper.GetInt("hasExtraSpin") == 1)
        {
            StartCoroutine(StartFortune());
            PrefsWrapper.SetInt("timesExtraSpinnedToday", PrefsWrapper.GetInt("timesExtraSpinnedToday", 0) + 1);
            PrefsWrapper.SetInt("hasExtraSpin", 0);
            PrefsWrapper.Save();
        }
        else if(PrefsWrapper.GetInt("timesExtraSpinnedToday") <= 2)
        {
            ModalManager.Show("Ούπς!", "Δεν έχεις άλλες περιστροφές!\nΘες να δείς μια διαφήμιση για να ξαναστρίψεις;", lvlManager.iconsForModals[3], new[] {new ModalButton(){Text = "Οχι" } ,
                new ModalButton() { Text = "Ναι!" ,
                Callback = () => {
                    rewardedAd.ShowAdForSpin();
                    //timesSpined =  PrefsWrapper.getint("timesspinned")
                    //clearEverything();
                    //disableButtons();
                    //HideAndShow(false);
                }
                } });
        }else if(PrefsWrapper.GetInt("timesExtraSpinnedToday") > 2)
        {
            ModalManager.Show("Ούπς!", "Δεν έχεις άλλες περιστροφές! Αύριο πάλι!", lvlManager.iconsForModals[3], new[] {new ModalButton(){Text = "Οκ..." } });
        }

    }
    public IEnumerator StartFortune()
    {
        if (_isSpinning) yield break;

        _isSpinning = true;
        wheelSpeed = UnityEngine.Random.Range(700f,1000f);
        _rotationIterations = UnityEngine.Random.Range(3f, 5f);
        _randomSelectedChioceID = UnityEngine.Random.Range(0, _fortuneSize-1);
        //print(_randomSelectedChioceID);

        StartCoroutine(RollWheel());
        yield return new WaitUntil(() => !_isSpinning );
        StopCoroutine(RollWheel());
        Debug.Log(getResult());
        lvlManager.updateCoins(getResult() * 10);
        //_result = new Tuple<int, string>(_latestTickStats, _slicesStats[_latestTickStats]);
        //// Debug.Log(_result.Item2);
        //GetLatestResult();
        //print(_result);
    }
    IEnumerator RollWheel()
    {
        float randWait = (wheelSpeed / 50f) * _rotationIterations;
        float speed = wheelSpeed;
        yield return new WaitUntil(() => rotateWheel(ref speed, ref randWait));
        _isSpinning = false;
    }
    bool rotateWheel(ref float speed, ref float deceleration)
    {
        this.gameObject.transform.Rotate(0, 0, -speed * Time.deltaTime);
        speed -= Time.deltaTime * deceleration;
        speed = Mathf.Clamp(speed, 0, wheelSpeed);
        deceleration += 0.7f;
        //Debug.LogError(this.gameObject.transform.rotation.eulerAngles.z);

        return speed == 0;
    }
    int getResult()
    {
        var rot = this.gameObject.transform.rotation.eulerAngles.z;
        var res = Mathf.RoundToInt(rot / (360 / _fortuneSize)) + 1;
        return res>_fortuneSize?1:res; //yparxei periptwsh na einai to rot px 350 deg, dld na kanei round sto _fortuneSize+1  , ara na prepei na ginei 0+1
    }
}
