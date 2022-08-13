using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotCreator : MonoBehaviour
{
    [SerializeField]
    private Slot slot;
    [SerializeField]
    private Vector2Int gridSize;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Vector2 gap;
    [SerializeField]
    private Vector2 offset;
    

    private bool workOnce;



    void Start()
    {
        if (workOnce)
        {
            return;
        }

        gameController.slotDictionary = new Dictionary<int, Slot>();
        var cnt = 0;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var slotInstance = Instantiate(slot, new Vector3((y * gap.y) + offset.y, (x * gap.x)+offset.x, 0), Quaternion.identity, transform);
                slotInstance.id = cnt;
                gameController.slots.Add(slotInstance);
                gameController.slotDictionary.Add(cnt, slotInstance);
                cnt++;
            }
        }
        workOnce = true;
    }

}
