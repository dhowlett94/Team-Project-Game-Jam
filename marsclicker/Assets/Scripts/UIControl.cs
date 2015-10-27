﻿using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Canvas landscapeCanvas;
    public Canvas portraitCanvas;
    public Canvas landscapeAchievementCanvas;
    public Canvas portraitAchievementCanvas;
    public GameObject planet;
    public bool CallUpdate;
    public static bool AchievementsShowing { get; private set; }

	public GameObject[] landscapeDetails,
						portraitDetails;

    void Start()
    {
        if(portraitAchievementCanvas != null)
        {
            portraitAchievementCanvas.enabled = false;
        }
        if(landscapeAchievementCanvas != null)
        {
            landscapeAchievementCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AchievementsShowing && CallUpdate)
        {
            Camera camera = GetComponent<Camera>();
            if (Screen.width >= Screen.height)
            {
                GameObject.FindGameObjectWithTag("backgroundImage").GetComponent<GUITexture>().pixelInset = new Rect(0, -Screen.height / 2.0f, 0, Screen.height);

                landscapeCanvas.enabled = true;
                portraitCanvas.enabled = false;

                if (landscapeDetails != null && landscapeDetails.Length != 0) {
                    if (GameControl.weaponManager.selectedWeapon != null) {
                        landscapeDetails[0].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.name;
                        landscapeDetails[1].GetComponent<Text>().text = "Damage: " + GameControl.weaponManager.selectedWeapon.damage;
                        landscapeDetails[2].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.type == WeaponType.AutoClicker ? "Own: " + GameControl.weaponManager.selectedWeapon.count : "";
                        if (GameControl.weaponManager.selectedWeapon.type == WeaponType.AutoClicker ||
                            (GameControl.weaponManager.selectedWeapon.type == WeaponType.PlayerWeapon &&
                             GameControl.weaponManager.selectedWeapon.count == 0)) {
                            landscapeDetails[3].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.cost.ToString("$0.00");
                            landscapeDetails[4].GetComponentInChildren<Text>().text = "Purchase";
                        } else {
                            landscapeDetails[3].GetComponent<Text>().text = "";
                            landscapeDetails[4].GetComponentInChildren<Text>().text = "Send";
                        }
                        landscapeDetails[4].GetComponent<Image>().enabled = true;
                        landscapeDetails[4].GetComponentInChildren<Text>().enabled = true;
                    } else {
                        landscapeDetails[0].GetComponent<Text>().text = "";
                        landscapeDetails[1].GetComponent<Text>().text = "";
                        landscapeDetails[2].GetComponent<Text>().text = "";
                        landscapeDetails[3].GetComponent<Text>().text = "";
                        landscapeDetails[4].GetComponent<Image>().enabled = false;
                        landscapeDetails[4].GetComponentInChildren<Text>().enabled = false;
                    }
                }

                if (planet != null)
                {
                    planet.transform.position = camera.ViewportToWorldPoint(new Vector3(0.333f, 0.5f, 0));
                    planet.transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y, 0);
                    planet.transform.localScale = new Vector3(5, 5, 5);
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("backgroundImage").GetComponent<GUITexture>().pixelInset = new Rect(-Screen.width / 2.0f, 0, Screen.width, 0);

                landscapeCanvas.enabled = false;
				portraitCanvas.enabled = true;

                if (portraitDetails != null && portraitDetails.Length != 0) {
                    if (GameControl.weaponManager.selectedWeapon != null) {
                        portraitDetails[0].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.name;
                        portraitDetails[1].GetComponent<Text>().text = "Damage: " + GameControl.weaponManager.selectedWeapon.damage;
                        portraitDetails[2].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.type == WeaponType.AutoClicker ? "Own: " + GameControl.weaponManager.selectedWeapon.count : "";
                        if (GameControl.weaponManager.selectedWeapon.type == WeaponType.AutoClicker || 
                            (GameControl.weaponManager.selectedWeapon.type == WeaponType.PlayerWeapon && 
                             GameControl.weaponManager.selectedWeapon.count == 0)) {
                            portraitDetails[3].GetComponent<Text>().text = GameControl.weaponManager.selectedWeapon.cost.ToString("$0.00");
                            portraitDetails[4].GetComponentInChildren<Text>().text = "Purchase";
                        } else {
                            portraitDetails[3].GetComponent<Text>().text = "";
                            portraitDetails[4].GetComponentInChildren<Text>().text = "Send";
                        }
                        portraitDetails[4].GetComponent<Image>().enabled = true;
                        portraitDetails[4].GetComponentInChildren<Text>().enabled = true;
                    } else {
                        portraitDetails[0].GetComponent<Text>().text = "";
                        portraitDetails[1].GetComponent<Text>().text = "";
                        portraitDetails[2].GetComponent<Text>().text = "";
                        portraitDetails[3].GetComponent<Text>().text = "";
                        portraitDetails[4].GetComponent<Image>().enabled = false;
                        portraitDetails[4].GetComponentInChildren<Text>().enabled = false;
                    }
                }

                if (planet != null)
                {
                    planet.transform.position = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.575f, 0));
                    planet.transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y, 0);
                    planet.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }
            }
        }
        else if(AchievementsShowing)
        {
            if (Screen.width > Screen.height)
            {
                landscapeAchievementCanvas.enabled = true;
                portraitAchievementCanvas.enabled = false;
            }
            else
            {
                landscapeAchievementCanvas.enabled = false;
                portraitAchievementCanvas.enabled = true;
            }
        }
    }

    public void LoadFacebook()
    {
        Application.OpenURL("https://www.facebook.com/pages/Elon-Musk/108250442531979?fref=ts");
    }

    public void LoadTwitter()
    {
        Application.OpenURL("https://twitter.com/elonmusk");
    }

    public void ShowAchievements()
    {
        AchievementsShowing = true;
        landscapeCanvas.enabled = false;
        portraitCanvas.enabled = false;
        if (Screen.width > Screen.height)
        {
            landscapeAchievementCanvas.enabled = true;
            portraitAchievementCanvas.enabled = false;
        }
        else
        {
            landscapeAchievementCanvas.enabled = false;
            portraitAchievementCanvas.enabled = true;
        }
    }

    public void CloseAchievements()
    {
        AchievementsShowing = false;
        landscapeAchievementCanvas.enabled = false;
        portraitAchievementCanvas.enabled = false;        
        
        if (Screen.width > Screen.height)
        {
            landscapeCanvas.enabled = false;
        }
        else
        {
            portraitCanvas.enabled = true;
        }
    }
}