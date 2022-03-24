using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityInfo
{
    public string AbilityName { get; }
    //public int AbilityId { get; }
    public bool Selectable { get; }
}
