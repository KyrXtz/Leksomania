using Google.Play.Review;
using Gravitons.UI.Modal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppReview : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelManager lvlManager;
    public PlayGamesController playGamesController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void askForReview()
    {
        ModalManager.Show("Ευκαιρία!", "Βαθμολόγησε μας με 5 αστέρια και κέρδισε 300 νομίσματα!\nΘα ανοίξει ένα παράθυρο μέσα στην εφαρμογή, δεν χρειάζεται καν να την κλείσεις!", lvlManager.iconsForModals[7], new[] {new ModalButton(){Text = "Αργότερα" } ,
                new ModalButton() { Text = "Οκ!" ,
                Callback = () => {
                    if (!PrefsWrapper.HasKey("HasRated"))
                    {
                        if(LevelManager.checkInternetConnection()){
                           StartCoroutine(startRate());

                        }
                        else
                        {
                            ModalManager.Show("Ούπς!", "Κάτι πήγε στραβά , ελέγξτε την σύνδεση στο ίντερνετ.", lvlManager.iconsForModals[6], new[] {new ModalButton(){Text = "Οκ..." } ,
                                    });
                        }
                        
                        
                    }


                } } });
    }

    IEnumerator startRate()
    {
        ModalManager.Show("...", "Μισό λεπτό...", lvlManager.iconsForModals[7], null);
        yield return new WaitForSeconds(1f);
        var _reviewManager = new ReviewManager();
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            ModalManager.Close();
            ModalManager.Show("Ούπς!", "Κάτι πήγε στραβά , ξαναδοκίμασε αργότερα.", lvlManager.iconsForModals[6], new[] {new ModalButton(){Text = "Οκ..." } ,
        });
            yield break;
        }
        var _playReviewInfo = requestFlowOperation.GetResult();
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            ModalManager.Close();
            ModalManager.Show("Ούπς!", "Κάτι πήγε στραβά , ξαναδοκίμασε αργότερα.", lvlManager.iconsForModals[6], new[] {new ModalButton(){Text = "Οκ..." } ,
        });
            yield break;
        }
        ModalManager.Close();
        ModalManager.Show("Ευχαριστούμε!", "Κέρδισες 300 νομίσματα!", lvlManager.iconsForModals[7], new[] {new ModalButton(){Text = "Τέλεια!" } ,
        });
        lvlManager.updateCoins(300,false);
        PrefsWrapper.SetInt("HasRated", 1);
        PrefsWrapper.Save();
        playGamesController.GetComponent<Achievements>().Rated();
    }


}
