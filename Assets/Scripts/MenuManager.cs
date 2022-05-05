using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] Menu[] menus;

    void Awake() {
        instance = this;
    }

    public void OpenMenu(string menuName) {
        for(int i = 0; i < menus.Length; i++) {
            if (menus[i].menuName == menuName) {
                menus[i].Open();
            }
            else if (menus[i].open) {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu) {
        for(int i = 0; i < menus.Length; i++) {
            if (menus[i].open) {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(string menuName) {
        for(int i = 0; i < menus.Length; i++) {
            if (menus[i].menuName == menuName) {
                menus[i].Close();
            }
        }
    }

    public void CloseMenu(Menu menu) {
        menu.Close();
        if (menu.menuName == "settings")
            Time.timeScale = 1;
    }

    public void CloseAllMenus()
    {
        for(int i = 0; i < menus.Length; i++) {
            if (menus[i].open) {
                CloseMenu(menus[i]);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
