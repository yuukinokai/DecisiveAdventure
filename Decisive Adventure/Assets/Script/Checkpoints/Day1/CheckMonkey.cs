using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMonkey : Checkpoint{

    [SerializeField] private GameObject monkey;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("GiveBanana", GiveBanana);
    }

    void GiveBanana()
    {
        Monkey monkeyHero = monkey.GetComponent<Monkey>();
        if(monkeyHero == null)
        {
            Debug.Log("Error, monkey not found");
            return;
        }
        hero.PartyJoin(monkeyHero);
        dialogController.Notice("You gave the monkey your banana and Monkey joined you!");
        hero.RemoveItem("Banana");
        Destroy(monkey);
    }

    override protected void Event1(){
		if(isActive){
			if(hero == null){
				Debug.Log("No Hero");
				return;
			}
			hero.AddItem("Meat");
            Destroy(monkey);
            base.Event1();
		}
	} 

}
