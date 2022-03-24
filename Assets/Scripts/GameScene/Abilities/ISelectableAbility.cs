using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectableAbility
{
    public void Set(int gameId);
    public int SelectedGameId { get; }
}
