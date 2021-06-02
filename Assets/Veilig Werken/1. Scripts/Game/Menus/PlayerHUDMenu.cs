﻿using MBevers.Menus;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace VeiligWerken.Menus
{
    /// <summary>
    /// 
    /// <para>Created by Mathias on 12-05-2021</para>
    /// </summary>
    public class PlayerHUDMenu : Menu
    {
        private RectTransform compassArrow = null;
        private Button openMapButton = null;

        protected override bool CanBeOpened() => !MenuManager.Instance.IsAnyOpen;
        protected override bool CanBeClosed() => true;

        protected override void Start()
        {
            IsHUD = true;
            
            base.Start();
            
            compassArrow = Content.Find("Compass").Find("Arrow") as RectTransform;
            Debug.Assert(compassArrow != null, "compassArrow is null");
            compassArrow.rotation = Quaternion.Euler(Vector3.back * GameManager.Instance.WindDirection);

            openMapButton = Content.GetComponentInChildren<Button>();
            openMapButton.onClick.AddListener(() => MenuManager.Instance.OpenMenu<FloorPlanMenu>());
            openMapButton.gameObject.SetActive(false);
            
            MenuManager.Instance.GetMenu<QuizMenu>().QuizCompletedEvent += OnQuizCompleted;
        }

        private void OnQuizCompleted()
        {
            openMapButton.gameObject.SetActive(true);
        }
    }
}