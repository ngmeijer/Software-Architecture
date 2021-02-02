using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewSwitchHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _shopGridViewObjects;
    [SerializeField] private List<GameObject> _shopListViewObjects;

    [SerializeField] private List<GameObject> _inventoryGridViewObjects;
    [SerializeField] private List<GameObject> _inventoryListViewObjects;

    public void SwitchView(int pIndex)
    {
        switch (pIndex)
        {
            case 0:
                foreach (GameObject go in _shopListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _shopGridViewObjects)
                    go.SetActive(true);
                break;

            case 1:
                foreach (GameObject go in _shopGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _shopListViewObjects)
                    go.SetActive(true);
                break;

            case 2:
                foreach (GameObject go in _shopGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _shopListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryGridViewObjects)
                    go.SetActive(true);
                break;

            case 3:
                foreach (GameObject go in _shopGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _shopListViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryGridViewObjects)
                    go.SetActive(false);

                foreach (GameObject go in _inventoryListViewObjects)
                    go.SetActive(true);
                break;
        }
    }
}
