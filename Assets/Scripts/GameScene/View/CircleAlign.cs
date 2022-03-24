using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAlign : MonoBehaviour
{
    [SerializeField] private float thetaMaxDegInit = 30;
    private float ThetaMaxInit => thetaMaxDegInit * Mathf.PI / 180;
    [SerializeField] private float radius = 500;
    private List<Transform> cardTrns = new List<Transform>();
    private Transform thisTrn;
    private int threshold = 5;

    private void Awake()
    {
        thisTrn = this.transform;
        thisTrn.position = new Vector3(Screen.width/2,-radius*Mathf.Cos(ThetaMaxInit/2)+30,0);
    }

    private void Start()
    {
        foreach (Transform item in this.transform)
        {
            var t = AddTransform(item);
            item.localPosition = t.Item1;
            item.localRotation = t.Item2;
        }
    }

    public void Remove(Transform trn)
    {
        cardTrns.Remove(trn);
        CalAllPos(cardTrns.Count);
        trn.rotation = Quaternion.Euler(Vector3.zero);
    }

    public Tuple<Vector3, Quaternion> AddTransform(Transform trn)
    {
        trn.SetParent(thisTrn, true);
        CalPos(cardTrns.Count + 1);
        cardTrns.Add(trn);
        return Calculate(cardTrns.Count-1, cardTrns.Count);
    }

    private void CalPos(int count)
    {
        if (count <= 1) return;

        for (int n = 0; n < cardTrns.Count; n++)
        {
            var t = Calculate(n, count);

            cardTrns[n].transform.localPosition = t.Item1;
            cardTrns[n].transform.rotation = t.Item2;
        }
    }

    private void CalAllPos(int count)
    {
        if (count <= 0) return;

        for (int n = 0; n < cardTrns.Count; n++)
        {
            var t = Calculate(n, count);

            cardTrns[n].transform.localPosition = t.Item1;
            cardTrns[n].transform.rotation = t.Item2;
        }
    }


    private Tuple<Vector3, Quaternion> Calculate(int index,int count)
    {
        if (count == 0) return null;
        else if (count == 1)
        {
            var pos1 = new Vector3(0, radius, 0);
            var angle = Quaternion.Euler(new Vector3(0, 0, 0));
            return new Tuple<Vector3, Quaternion>(pos1, angle);
        }
        else
        {
            float deltaTheta = Mathf.Min(ThetaMaxInit / threshold, ThetaMaxInit / (count - 1));
            float ThetaMax = deltaTheta * (count - 1);
            float theta = (Mathf.PI / 2) + (ThetaMax / 2) - index * deltaTheta;
            var pos = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * radius;
            var q = Quaternion.Euler(new Vector3(0, 0, (theta - (Mathf.PI / 2)) * 180 / Mathf.PI));
            return new Tuple<Vector3, Quaternion>(pos, q);
        }
    }
}
