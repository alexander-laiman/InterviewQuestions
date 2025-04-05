
using System;

using System.Collections;
using System.Collections.Generic;

// Slightly less clapped quick code up of question from industry interview.
// Improvements could be to make enums for the components to ensure safety, or implement a lookup table for existing pricing / database
// This seperates business from software by allowing pricing to be set elsewhere.
// Can also pull out the price component from toppings etc using an interface ICostItem with a virtual function

public enum PizzaSizes {Small, Medium, Large}
public enum BaseType { Flat, Thin, Stuffed}
public enum ToppingClass {Standard, Premium}

public class PriceDataBase{
    private Dictionary<PizzaSizes, int> sizePrices = new Dictionary<PizzaSizes, int> {
        {PizzaSizes.Small, 2},
        {PizzaSizes.Medium, 4},
        {PizzaSizes.Large, 6}
    };

    private Dictionary<BaseType, int> basePrices = new Dictionary<BaseType, int> {
        {BaseType.Flat, 3},
        {BaseType.Thin, 2},
        {BaseType.Stuffed, 4}
    };

    private Dictionary<ToppingClass, int> toppingPrices = new Dictionary<ToppingClass, int> {
        {ToppingClass.Standard, 1},
        {ToppingClass.Premium, 2}
    };

    public int GetSizePrice(PizzaSizes size) => sizePrices[size];
    public int GetBasePrice(BaseType bases) => basePrices[bases];
    public int GetToppingPrice(ToppingClass top) => toppingPrices[top];

    // Could add methods for updating prices from API, but ideally id seperate out prices to be accessed from a database and have a seperate api 
}

public class PriceCalculator{
    private PriceDataBase db;
    public PriceCalculator(PriceDataBase db){
        this.db=db;
    }
    public int CalcPizzaPrice(Pizza pizza){
        if(pizza==null){
            return 0;
        }
        int price = 0;
        price += db.GetBasePrice(pizza.GetBase()) + db.GetSizePrice(pizza.GetSize());
        foreach(var topping in pizza.GetToppings()){
            price += db.GetToppingPrice(topping);
        }
        return price;
    }
}
public class Program{
    static void Main(){
        //Implementation here
        // Default db for test
        PriceDataBase db = new PriceDataBase();
        // Should add check for incorrectly created calculator if db not provided
        PriceCalculator pCalc = new PriceCalculator(db);

        // Make pizza!
        // Should have toppings that can be classes as premium or not to maintain readability
        List<ToppingClass> toppings = new List<ToppingClass>();
        toppings.Add(ToppingClass.Premium);

        Pizza pizza1 = new Pizza(PizzaSizes.Medium, BaseType.Thin, toppings);
        int price = pCalc.CalcPizzaPrice(pizza1);
        System.Console.WriteLine("Pizza price is {0}", price);

    }
}

// Pizza class
public class Pizza{
    PizzaSizes pizzaSize;
    BaseType pizzaBase;
    List<ToppingClass> Toppings;

    public PizzaSizes GetSize() => this.pizzaSize;
    public BaseType GetBase() => this.pizzaBase;
    public List<ToppingClass> GetToppings() => this.Toppings;

    public Pizza(PizzaSizes size, BaseType bases, List<ToppingClass> toppings){
        this.pizzaSize = size;
        this.pizzaBase = bases;
        this.Toppings = toppings;
    }
    public int LoadPizzaConfig(){
        // implement stream writer to load pizza from file
        return 0;
    }
}
