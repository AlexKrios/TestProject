﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Battle.Units.Turn
{
    public class TurnEnd : MonoBehaviour
    {
        private Manager Manager { get => Manager.Instance; }
        private TurnStart TurnStart { get => Manager.turnStart; }
        private UnitsMark UnitsMark { get => Manager.unitsMark; }
        private List<UnitStatus> UnitsList { get => Manager.unitsList; }

        [NonSerialized] public UnityEvent OnStartExecute = new UnityEvent();
        [NonSerialized] public UnityEvent OnEndExecute = new UnityEvent();

        private void Start()
        {
            OnStartExecute.AddListener(StartExecute);
            OnEndExecute.AddListener(EndExecute);
        }

        public void StartExecute()
        {
            Manager.AddBattleStatus("TurnEnd");
        }

        public void EndExecute()
        {
            Manager.RemoveBattleStatus("TurnEnd");
            UnitsMark.DestroyTurnMark();

            if (EndBattle()) 
            {
                return;
            }

            TurnStart.StartExecute();
        }

        private bool EndBattle()
        {
            var isLose = !UnitsList.Any(x => x.status == "Live" && x.team == "Ally");
            var isWin = !UnitsList.Any(x => x.status == "Live" && x.team == "Enemy");

            return isLose || isWin;
        }
    }
}