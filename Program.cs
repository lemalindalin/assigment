using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    
    public static void Main()
    {
        //g
        List<Customer> myList = new List<Customer>();
        Customer cust1 = new Customer { FirstName = "Joe", LastName = " Smith" };
        Customer cust2 = new Customer { FirstName = "Jane", LastName = " Jones" };
        Customer cust3 = new Customer { FirstName = "Jeff", LastName = " Jump" };
        Customer cust4 = new Customer { FirstName = "Jill", LastName = " Hill" };
        Customer cust5 = new Customer { FirstName = "John", LastName = "Winstone" };
        myList.Add(cust1);
        myList.Add(cust2);
        myList.Add(cust3);
        myList.Add(cust4);
        myList.Add(cust5);
        Table table = new Table();
        

        Type meals = typeof(Customer.Meals);
        foreach (var cust in myList)
        {
            table.TableOpened += cust.OnTableOpenned;
            table.OpenTheTable();
            table.TableOpened -= cust.OnTableOpenned;


            foreach (string s in Enum.GetNames(meals))
            {

                MealOfCustomer(cust, s);
                
            }
            

        }
        Console.WriteLine("Everyone is Full!");
    }

    private static void MealOfCustomer(Customer cust, string s)
    {
        Console.WriteLine("{0} {1} is having {2}.", cust.FirstName, cust.LastName, s);
    }
}

public delegate void TableOpenEventHandler(object source, EventArgs args);
public class Table
{
    public event TableOpenEventHandler TableOpened;

    public void OpenTheTable()
    {
        OnTableOpened();
    }
    protected virtual void OnTableOpened()
    {
        Console.WriteLine("Table is open!");
        if (TableOpened != null)
        {
            TableOpened(this, EventArgs.Empty);
        }
    }

}

public delegate void MealsChangeEventHandler(object source, MealsChangeEventArgs meal);
public class Customer
{
    public event MealsChangeEventHandler MealChanged;
    public string FirstName { get; set; }
    public string LastName { get; set; }

    
    public enum Meals { appetizer, main, desert, done };

  

    public void OnTableOpenned(object source, EventArgs args)
    {
        Console.WriteLine("{0}{1} got a table.", this.FirstName, this.LastName);
    }

    public void ChangeTheMeal(string meal)
    {
        OnMealChanged(meal);
    }

    protected virtual void OnMealChanged(string meal)
    {
        if(MealChanged != null)
        {
            MealChanged(this, new MealsChangeEventArgs(meal));
        }
    }
}

public class MealsChangeEventArgs : EventArgs
{
    public string meal { get; set; }
    public MealsChangeEventArgs(string meal)
    {
        this.meal = meal;

    }
}