﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public interface IUpgrade
    {
        UpgardeTypes ID { get; }
        //PlayerUpgradeController PlayerUpgradeController { get; set; }
        int CurrentUpgradeLevel { get; set; }
        int MaxLevel { get; }
        int MinLevel { get; set; }

        float[] Values { get; set; }

        float CalculateValue(float _value);
    }

    public interface ISelectableUpgrade : ISelectable {
        void SetIUpgrade(IUpgrade _upgrade);

        IUpgrade GetData();
    }

    public enum UpgardeTypes {
        FireRateUpgrade
    }
}