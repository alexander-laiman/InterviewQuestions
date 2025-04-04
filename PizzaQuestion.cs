
// Slightly less clapped quick code up of question from industry interview.
// Improvements could be to make enums for the components to ensure safety, or implement a lookup table for existing pricing / database
// This seperates business from software by allowing pricing to be set elsewhere.
// Can also pull out the price component from toppings etc using an interface ICostItem with a virtual function


public class Program{
    static void Main(){
        //Implementation here
        PizzaSize size = new PizzaSize(2,"small");
        Base base = new Base(1,"flat");
        Topping topping1 = new Topping(3,"pepperoni");
        List<Topping> toppings = new List<Topping>();
        toppings.Add(topping1);
        Pizza pizza1 = new Pizza(size, base, toppings);
        int price = pizza1.GetPrice();
        Console.WriteLine("Pizza price is {0}", price);

    }
}

// Pizza class
public class Pizza{
    PizzaSize pizzaSize;
    Base pizzaBase;
    List<Topping> Toppings;
    public Pizza(PizzaSize size, Base base, List<Topping> toppings){
        this.pizzaSize = size;
        this.pizzaBase = base;
        this.Toppings = toppings;
    }

    public int GetPrice(){
        int cost = 0;
        for(var topping in Toppings){
            cost += topping.GetPrice();
        }
        cost += (pizzaSize.GetPrice() + pizzaBase.GetPrice());
        return cost;
    }

    public int LoadPizzaConfig(){
        // implement stream writer to load pizza from file
    }
}

public abstract class FoodComponent{
    protected int price = 0;
    protected String name;

    protected FoodComponent(int price, String name){
        this.price = price;
        this.name = name;
    }
    public int GetPrice(){
        return price;
    }
}

public class Topping : FoodComponent{
    public Topping(int price, String name):base(price, name){}
}
public class Base : FoodComponent{
    public Base(int price, String name):base(price, name){}
}
public class PizzaSize : FoodComponent {
    public PizzaSize(int price, String name):base(price, name){}
}
