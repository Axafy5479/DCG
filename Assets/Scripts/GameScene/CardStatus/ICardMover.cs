using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードの移動が可能なインターフェース
/// </summary>
public interface ICardMover : ICardInfo
{
    /// <summary>
    /// カードのステータスのPosを変更する
    /// </summary>
    /// <param name="posTo"></param>
    void ChangePos(Pos posTo);

    /// <summary>
    /// 移動可能性を変更する
    /// </summary>
    /// <param name="canMove"></param>
    void ChangePlayable(bool canMove);
}
