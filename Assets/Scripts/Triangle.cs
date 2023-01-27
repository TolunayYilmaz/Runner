using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] public List<GameObject> players;
    public int range;
    private int height;
    public int Height { get { return height; } }   
    private bool isCalculated = true;
    private int increasing;
    public void RowCalculate()
    {
       if(isCalculated)
        {
            isCalculated = false;
            range = players.Count;
            for (int i = 1; i < range; i++)
            {
                range -= i * 2;
                if(range < 0)
                {
                    height = i-1;
                 
                    break;
                }
              else if (range >= 0)
                {

                    height = i;
                   if (players.Count <= 10)
                   {
                        increasing = 1;
                   }
                   else
                   {
                        increasing = range / height;
                   }
                    
                }
              
            }
        }
       
    }
    void BotPlayer()
       
    {
        if (players.Count >= height && range < 0|| range >= 0&& players.Count >= height)
        {

            for (int j = 0; j < increasing; j++)
            {
                for (int i = 0; i < height; i++)
                {
                    GameObject clone = players[0];
                    clone.transform.rotation = Quaternion.identity;
                    clone.transform.position = new Vector3(transform.position.x + -16f + i * 2, transform.position.y, transform.position.z);
                    players.Remove(clone);

                }
                transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            }
        }
    }
    void TopPlayer()
    {
        range = players.Count;
        if (range <= height)
        {
            transform.position = new Vector3(transform.position.x-1f, transform.position.y , transform.position.z);
            for (int j = players.Count; j >0 ; j--)
            {
                GameObject clone = players[0];
                clone.transform.position = new Vector3(transform.position.x -16f, transform.position.y, transform.position.z);
                clone.transform.rotation = Quaternion.identity;
                players.Remove(clone);
               // Instantiate(oyuncu, new Vector3(transform.position.x+2f, transform.position.y, transform.position.z), Quaternion.identity);
                transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            }
        }
    }
  public void ücgen()

    {
        RowCalculate();
        BotPlayer();
        for (int row = height; row >= 1; row--)
        {
           
            for (int column = 1; column <= row; column++)
            {
                //listeler 0 dan baþlar
                GameObject clone = players[0];
                clone.transform.position = new Vector3(transform.position.x -18f+ column * 2, transform.position.y, transform.position.z);
                clone.transform.rotation= Quaternion.identity;
                players.Remove(clone);

                // oyuncularda oyuncu çýkardðýmýz için ayný indexden eleman çaðýrýnca remove la bir azaldýðý için index of hatasý almayýz.
                GameObject clone2 = players[0];
                clone2.transform.position = new Vector3(transform.position.x-18f + column * 2, transform.position.y + 2f, transform.position.z);
                clone2.transform.rotation = Quaternion.identity;
                players.Remove(clone2);
               // Instantiate(oyuncu, new Vector3(transform.position.x + i * 2, transform.position.y , transform.position.z), Quaternion.identity);
               //Instantiate(oyuncu, new Vector3(transform.position.x + i * 2, transform.position.y+2f, transform.position.z), Quaternion.identity);

            }
               transform.position=new Vector3(transform.position.x+1f, transform.position.y + 4f, transform.position.z);

        }
        TopPlayer();

    }
}
