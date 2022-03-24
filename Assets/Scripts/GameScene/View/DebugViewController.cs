//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DebugViewController : MonoSingleton<DebugViewController>
//{
//    private List<CardVisual> debugCardObj = new List<CardVisual>();

//    public void Reload()
//    {

//        PositionLocatorInfo.I.Resolve(true,Pos.Deck).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Deck>(true).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(false,Pos.Deck).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Deck>(false).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(true,Pos.Field).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Field>(true).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(false, Pos.Field).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Field>(false).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(true,Pos.Hand).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Hand>(true).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(false, Pos.Hand).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Hand>(false).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(true,Pos.Discard).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Discard>(true).MakeCard(card)));
//        PositionLocatorInfo.I.Resolve(false, Pos.Discard).Cards.ForEach(card => debugCardObj.Add(PosViewLocator.I.Resolve<PositionView_Discard>(false).MakeCard(card)));
//    }
//}
