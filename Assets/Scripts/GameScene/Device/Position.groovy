@startuml Device


scale 2
skinparam DefaultFontName 源ノ角ゴシック Code JP Medium

package Device{

    class PositionBase
    {
        + IsPlayer<<get>>
        + Cards<<get>>
        + Pos<<get>>
        + TryMoveTo()
    }

    class Mana
    {
        + CurrentMana<<get>>
        + TryUseMana(cost)
        + NewTurn()
    }

    class TurnManager
    {
        + IsPlayerTurn
        + ChangeTurn()
        
    }
}






@enduml