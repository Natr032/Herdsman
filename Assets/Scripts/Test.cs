using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    [SerializeField]
    private DroppedWeapon _weapon;

    private void Start()
    {
        
    }

    private void Update()
    {
        TestDroppedWeapon();
    }

    public string TestFactorial()
    {
        int result = 1;
        for (int i = 1; i <= 6; i++)
        {
            result *= i;
        }
        Thread.Sleep(8000);
        return $"Факториал равен {result}";
    }

    public async void TestFactorialAsync()
    {
        var s = await Task.Run(() => TestFactorial()); // вызов асинхронной операции
        TestPrintString(s);
    }

    public void TestPrintString(string s)
    {
        Debug.Log("Print " + s);
    }

    private void TestDroppedWeapon()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var temp = Instantiate(_weapon, this.transform);
            temp.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, temp.transform.position.z);
        }
    }

    private async void TestM()
    {
        int test = 0;
        await Task.Run(() => {
            while (true)
            {
                Debug.Log(test);
                test++;
                Task.Delay(1000);
            }
        });
    }
}
