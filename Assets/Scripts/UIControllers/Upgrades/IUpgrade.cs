using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public interface IUpgrade
    {
        PlayerUpgradeController PlayerUpgradeController { get; set; }
        int CurrentUpgradeLevel { get; set; }
        int MaxLevel { get; set; }
        int MinLevel { get; set; }

        void ApplyUpgrade();
    }
}